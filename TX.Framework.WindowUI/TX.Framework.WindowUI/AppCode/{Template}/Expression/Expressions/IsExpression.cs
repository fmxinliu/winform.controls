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
using System.Text;

namespace System.Text.Template {
    public class IsExpression : Expression {
        private readonly Expression _objectExpression;
        private readonly Expression _typeExpression;

        public IsExpression(Expression objectExpression, Expression typeExpression) {
            _objectExpression = objectExpression;
            _typeExpression = typeExpression;
        }

        public override ValueExpression Evaluate(ITemplateContext context) {
            ClassName className = _typeExpression.Evaluate(context).Value as ClassName;
            ValueExpression objectValue = _objectExpression.Evaluate(context);
            Type objectType = objectValue.Type;

            if (objectValue.Value == null)
                return Expression.Value(false);

            objectType = Nullable.GetUnderlyingType(objectType) ?? objectType;

            if (className == null)
                throw new Exception("is operator requires type");

            Type checkType = className.Type;

            if (!objectType.IsValueType)
                return Expression.Value(checkType.IsAssignableFrom(objectType));

            checkType = Nullable.GetUnderlyingType(checkType) ?? checkType;

            return Expression.Value(checkType == objectType);
        }
    }
}
