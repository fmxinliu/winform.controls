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
using System.Text;

namespace System.Text.Template {
    public class CallExpression : Expression {
        private readonly Expression _methodExpression;
        private readonly Expression[] _parameters;

        public CallExpression(Expression methodExpression, Expression[] parameters) {
            _methodExpression = methodExpression;
            _parameters = parameters;
        }

        public override ValueExpression Evaluate(ITemplateContext context) {
            object methodObject = _methodExpression.Evaluate(context).Value;

            if (methodObject == null) {
                return null;
            }

            ValueExpression[] parameters = EvaluateExpressionArray(_parameters, context);
            Type[] parameterTypes = Array.ConvertAll<ValueExpression, Type>(parameters, delegate(ValueExpression expr) { return expr.Type; });
            object[] parameterValues = Array.ConvertAll<ValueExpression, object>(parameters, delegate(ValueExpression expr) { return expr.Value; });

            if (methodObject is MethodDefinition) {
                Type returnType;

                return Expression.Value(((MethodDefinition) methodObject).Invoke(parameterTypes, parameterValues, out returnType), returnType);
            }

            if (methodObject is ConstructorInfo[]) {
                ConstructorInfo[] constructors = (ConstructorInfo[]) methodObject;

                MethodBase method = new LazyBinder().SelectMethod(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance, constructors, parameterTypes, null);

                if (method == null)
                    throw new MissingMethodException();

                object value = ((ConstructorInfo) method).Invoke(parameterValues);

                return Expression.Value(value, method.ReflectedType);
            }

            if (methodObject is Delegate[]) {
                Delegate[] delegates = (Delegate[]) methodObject;
                MethodBase[] methods = Array.ConvertAll<Delegate, MethodBase>(delegates, delegate(Delegate d) { return d.Method; });

                MethodBase method = new LazyBinder().SelectMethod(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance, methods, parameterTypes, null);

                if (method == null)
                    throw new MissingMethodException();

                object value = method.Invoke(delegates[Array.IndexOf(methods, method)].Target, parameterValues);

                return Expression.Value(value, ((MethodInfo) method).ReturnType);
            }

            if (methodObject is Delegate) {
                Delegate method = (Delegate) methodObject;

                object value = method.Method.Invoke(method.Target, parameterValues);

                return new ValueExpression(value, method.Method.ReturnType);
            }

            throw new Exception("Illegal function call");
        }

        public override string ToString() {
            string[] parameters = Array.ConvertAll<Expression, string>(_parameters,
                delegate(Expression expr) { return expr.ToString(); });
            return "(" + _methodExpression + "(" + String.Join(",", parameters) + "))";
        }
    }
}
