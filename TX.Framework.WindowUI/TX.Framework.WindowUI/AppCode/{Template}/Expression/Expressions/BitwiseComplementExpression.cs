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
    public class BitwiseComplementExpression : Expression {
        private readonly Expression _value;

        public BitwiseComplementExpression(Expression value) {
            _value = value;
        }

        public override ValueExpression Evaluate(ITemplateContext context) {
            ValueExpression value = _value.Evaluate(context);

            if (value.Type == typeof(int))
                return Expression.Value(~(int) value.Value);

            if (value.Type == typeof(uint))
                return Expression.Value(~(uint) value.Value);

            if (value.Type == typeof(long))
                return Expression.Value(~(long) value.Value);

            if (value.Type == typeof(ulong))
                return Expression.Value(~(ulong) value.Value);

            throw new OverflowException();
        }
    }
}