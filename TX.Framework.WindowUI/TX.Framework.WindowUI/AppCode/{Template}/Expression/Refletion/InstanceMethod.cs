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
using System.Reflection;

namespace System.Text.Template {
    internal class InstanceMethod : MethodDefinition {
        private readonly object _object;

        public InstanceMethod(MethodInfo methodInfo, object @object) : base(methodInfo) {
            _object = @object;
        }

        public InstanceMethod(Type type, string methodName, object @object) : base(type, methodName) {
            _object = @object;
        }

        public override object Invoke(Type[] types, object[] parameters, out Type returnType) {
            MethodInfo methodInfo = GetMethodInfo(types);

            returnType = methodInfo.ReturnType;

            return methodInfo.Invoke(_object, BindingFlags.Default, new LazyBinder(), parameters, null);
        }
    }
}