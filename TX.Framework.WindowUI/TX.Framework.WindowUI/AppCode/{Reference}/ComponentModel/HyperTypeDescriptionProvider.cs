#region COPYRIGHT
//
//     THIS IS GENERATED BY TEMPLATE
//     
//     AUTHOR  :     ROYE
//     DATE       :     2010
//
//     COPYRIGHT (C) 2010, TIANXIAHOTEL TECHNOLOGIES CO., LTD. ALL RIGHTS RESERVED.
//
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Permissions;
using System.Text;

namespace System.ComponentModel {
    public sealed class HyperTypeDescriptionProvider : TypeDescriptionProvider {
        private static readonly Dictionary<Type, ICustomTypeDescriptor> _Descriptors = new Dictionary<Type, ICustomTypeDescriptor>();

        public HyperTypeDescriptionProvider() : this(typeof(object)) { }

        public HyperTypeDescriptionProvider(Type type) : this(TypeDescriptor.GetProvider(type)) { }

        public HyperTypeDescriptionProvider(TypeDescriptionProvider parent) : base(parent) { }

        public static void Add(Type type) {
            TypeDescriptionProvider parent = TypeDescriptor.GetProvider(type);
            TypeDescriptor.AddProvider(new HyperTypeDescriptionProvider(parent), type);
        }

        public static void Clear(Type type) {
            lock (_Descriptors) {
                _Descriptors.Remove(type);
            }
        }

        public static void Clear() {
            lock (_Descriptors) {
                _Descriptors.Clear();
            }
        }

        public sealed override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance) {
            ICustomTypeDescriptor descriptor;
            lock (_Descriptors) {
                if (!_Descriptors.TryGetValue(objectType, out descriptor)) {
                    try {
                        descriptor = BuildDescriptor(objectType);
                    }
                    catch {
                        return base.GetTypeDescriptor(objectType, instance);
                    }
                }
                return descriptor;
            }
        }

        [ReflectionPermission(SecurityAction.Assert, Flags = ReflectionPermissionFlag.AllFlags)]
        private ICustomTypeDescriptor BuildDescriptor(Type objectType) {
            // NOTE: "descriptors" already locked here

            // get the parent descriptor and add to the dictionary so that
            // building the new descriptor will use the base rather than recursing
            ICustomTypeDescriptor descriptor = base.GetTypeDescriptor(objectType, null);
            _Descriptors.Add(objectType, descriptor);
            try {
                // build a new descriptor from this, and replace the lookup
                descriptor = new HyperTypeDescriptor(descriptor);
                _Descriptors[objectType] = descriptor;
                return descriptor;
            }
            catch { // rollback and throw
                // (perhaps because the specific caller lacked permissions;
                // another caller may be successful)
                _Descriptors.Remove(objectType);
                throw;
            }
        }
    }
}
