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
    /// Extension methods for inspecting and working with properties.
    /// </summary>
    public static class PropertyInfoExtensions {
        /// <summary>
        /// Sets the static property identified by <paramref name="propInfo"/> with <paramref name="value" />.
        /// </summary>
        public static void Set(this PropertyInfo propInfo, object value) {
            propInfo.DelegateForSetStaticPropertyValue()(value);
        }

        /// <summary>
        /// Sets the instance property identified by <paramref name="propInfo"/> of the given <paramref name="obj"/>
        /// with <paramref name="value" />.
        /// </summary>
        public static void Set(this PropertyInfo propInfo, object obj, object value) {
            propInfo.DelegateForSetPropertyValue()(obj, value);
        }

        /// <summary>
        /// Gets the value of the static property identified by <paramref name="propInfo"/>.
        /// </summary>
        public static object Get(this PropertyInfo propInfo) {
            return propInfo.DelegateForGetStaticPropertyValue()();
        }

        /// <summary>
        /// Gets the value of the instance property identified by <paramref name="propInfo"/> of the given <paramref name="obj"/>.
        /// </summary>
        public static object Get(this PropertyInfo propInfo, object obj) {
            return propInfo.DelegateForGetPropertyValue()(obj);
        }

        /// <summary>
        /// Creates a delegate which can set the value of the static property identified by <paramref name="propInfo"/>.
        /// </summary>
        public static StaticMemberSetter DelegateForSetStaticPropertyValue(this PropertyInfo propInfo) {
            return (StaticMemberSetter) new MemberSetEmitter(propInfo, Flags.StaticAnyVisibility).GetDelegate();
        }

        /// <summary>
        /// Creates a delegate which can set the value of the instance property <paramref name="propInfo"/>.
        /// </summary>
        public static MemberSetter DelegateForSetPropertyValue(this PropertyInfo propInfo) {
            return (MemberSetter) new MemberSetEmitter(propInfo, Flags.InstanceAnyVisibility).GetDelegate();
        }

        /// <summary>
        /// Creates a delegate which can get the value of the static property <paramref name="propInfo"/>.
        /// </summary>
        public static StaticMemberGetter DelegateForGetStaticPropertyValue(this PropertyInfo propInfo) {
            return (StaticMemberGetter) new MemberGetEmitter(propInfo, Flags.StaticAnyVisibility).GetDelegate();
        }

        /// <summary>
        /// Creates a delegate which can set the value of the static property <paramref name="propInfo"/>.
        /// </summary>
        public static MemberGetter DelegateForGetPropertyValue(this PropertyInfo propInfo) {
            return (MemberGetter) new MemberGetEmitter(propInfo, Flags.InstanceAnyVisibility).GetDelegate();
        }
    }
}
