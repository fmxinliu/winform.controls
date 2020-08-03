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
using System.Linq;
using System.Reflection.Emit;
using System.Text;

namespace System.Reflection {
    /// <summary>
    /// Extension methods for inspecting and working with fields.
    /// </summary>
    public static class FieldInfoExtensions {
        /// <summary>
        /// Sets the static field identified by <paramref name="fieldInfo"/> with <paramref name="value" />.
        /// </summary>
        public static void Set(this FieldInfo fieldInfo, object value) {
            fieldInfo.DelegateForSetStaticFieldValue()(value);
        }

        /// <summary>
        /// Sets the instance field identified by <paramref name="fieldInfo"/> of the given <paramref name="obj"/>
        /// with <paramref name="value" />.
        /// </summary>
        public static void Set(this FieldInfo fieldInfo, object obj, object value) {
            fieldInfo.DelegateForSetFieldValue()(obj, value);
        }

        /// <summary>
        /// Gets the value of the static field identified by <paramref name="fieldInfo"/>.
        /// </summary>
        public static object Get(this FieldInfo fieldInfo) {
            return fieldInfo.DelegateForGetStaticFieldValue()();
        }

        /// <summary>
        /// Gets the value of the instance field identified by <paramref name="fieldInfo"/> of the given <paramref name="obj"/>.
        /// </summary>
        public static object Get(this FieldInfo fieldInfo, object obj) {
            return fieldInfo.DelegateForGetFieldValue()(obj);
        }

        /// <summary>
        /// Creates a delegate which can set the value of the static field identified by <paramref name="fieldInfo"/>.
        /// </summary>
        public static StaticMemberSetter DelegateForSetStaticFieldValue(this FieldInfo fieldInfo) {
            return (StaticMemberSetter) new MemberSetEmitter(fieldInfo, Flags.StaticAnyVisibility).GetDelegate();
        }

        /// <summary>
        /// Creates a delegate which can set the value of the instance field identified by <paramref name="fieldInfo"/>.
        /// </summary>
        public static MemberSetter DelegateForSetFieldValue(this FieldInfo fieldInfo) {
            return (MemberSetter) new MemberSetEmitter(fieldInfo, Flags.InstanceAnyVisibility).GetDelegate();
        }

        /// <summary>
        /// Creates a delegate which can get the value of the static field identified by <paramref name="fieldInfo"/>.
        /// </summary>
        public static StaticMemberGetter DelegateForGetStaticFieldValue(this FieldInfo fieldInfo) {
            return (StaticMemberGetter) new MemberGetEmitter(fieldInfo, Flags.StaticAnyVisibility).GetDelegate();
        }

        /// <summary>
        /// Creates a delegate which can set the value of the static field identified by <paramref name="fieldInfo"/>.
        /// </summary>
        public static MemberGetter DelegateForGetFieldValue(this FieldInfo fieldInfo) {
            return (MemberGetter) new MemberGetEmitter(fieldInfo, Flags.InstanceAnyVisibility).GetDelegate();
        }
    }
}
