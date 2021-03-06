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
    public class NegationExpression : Expression {
        private readonly Expression _value;

        public NegationExpression(Expression value) {
            _value = value;
        }

        public override ValueExpression Evaluate(ITemplateContext context) {
            ValueExpression value = _value.Evaluate(context);
            if (context == null)
                return Expression.Value(!((bool) value.Value));
            else
                return Expression.Value(!context.ToBoolean(value.Value));
        }

        public override string ToString() {
            return "(!" + _value + ")";
        }
    }
}
