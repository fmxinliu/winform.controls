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
using System.Globalization;
using System.Reflection;
using System.Text;

namespace System.Text.Template {
    public class LazyBinder : Binder {
        public override MethodBase BindToMethod(BindingFlags bindingAttr, MethodBase[] match, ref object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] names, out object state) {
            return Type.DefaultBinder.BindToMethod(bindingAttr, match, ref args, modifiers, culture, names, out state);
        }

        public override FieldInfo BindToField(BindingFlags bindingAttr, FieldInfo[] match, object value, CultureInfo culture) {
            return Type.DefaultBinder.BindToField(bindingAttr, match, value, culture);
        }

        public override MethodBase SelectMethod(BindingFlags bindingAttr, MethodBase[] match, Type[] types, ParameterModifier[] modifiers) {
            MethodBase matchingMethod = Type.DefaultBinder.SelectMethod(bindingAttr, match, types, modifiers);

            if (matchingMethod != null)
                return matchingMethod;

            foreach (MethodBase method in match) {
                if (ParametersMatch(types, method.GetParameters()))
                    return method;
            }

            return null;
        }

        public override PropertyInfo SelectProperty(BindingFlags bindingAttr, PropertyInfo[] match, Type returnType, Type[] indexes, ParameterModifier[] modifiers) {
            return Type.DefaultBinder.SelectProperty(bindingAttr, match, returnType, indexes, modifiers);
        }

        public override object ChangeType(object value, Type type, CultureInfo culture) {
            if (value.GetType() == type)
                return value;

            MethodInfo conversionMethod = type.GetMethod("op_Implicit", new Type[] { value.GetType() });

            if (conversionMethod == null)
                return Type.DefaultBinder.ChangeType(value, type, culture);

            return conversionMethod.Invoke(null, new object[] { value });
        }

        public override void ReorderArgumentArray(ref object[] args, object state) {
            Type.DefaultBinder.ReorderArgumentArray(ref args, state);
        }

        private bool ParametersMatch(Type[] inputParameters, ParameterInfo[] expectedParameters) {
            if (inputParameters.Length != expectedParameters.Length)
                return false;

            for (int i = 0; i < inputParameters.Length; i++)
                if (!CanConvert(inputParameters[i], expectedParameters[i].ParameterType))
                    return false;
            return true;
        }

        private bool CanConvert(Type from, Type to) {
            MethodInfo conversionMethod = to.GetMethod("op_Implicit", new Type[] { from });
            return conversionMethod != null;
        }
    }
}
