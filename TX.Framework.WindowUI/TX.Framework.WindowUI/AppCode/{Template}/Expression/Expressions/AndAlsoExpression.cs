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
    public class AndAlsoExpression : BinaryExpression {
        public AndAlsoExpression(Expression left, Expression right) : base(left, right) { }

        public override ValueExpression Evaluate(ITemplateContext context) {
            object left = Left.Evaluate(context).Value;

            if (!context.ToBoolean(left))
                return Expression.Value(false);

            object right = Right.Evaluate(context).Value;

            return Expression.Value(context.ToBoolean(right));
        }

        public override string ToString() {
            return "(" + Left + " && " + Right + ")";
        }
    }
}