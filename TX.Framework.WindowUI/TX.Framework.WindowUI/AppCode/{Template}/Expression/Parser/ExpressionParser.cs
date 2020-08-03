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
using System.Text.RegularExpressions;

namespace System.Text.Template {
    public delegate Expression TokenEvaluator(string token, Expression[] terms);

    public abstract class ExpressionParser : IExpressionParser {
        private static readonly Dictionary<string, IExpression> _ExpressionCache = new Dictionary<string, IExpression>();
        private static readonly object _CacheLock = new object();
        private readonly List<TokenDefinition> _TokenDefinitions = new List<TokenDefinition>();
        private TokenEvaluator _FunctionEvaluator;
        private int _FunctionCallPrecedence;
        private Regex _RegEx = null;
        private ITemplateContext _DefaultContext;

        public IExpression Parse(string expression) {
            lock (_CacheLock) {
                if (_ExpressionCache.ContainsKey(expression)) {
                    return _ExpressionCache[expression];
                }
                else {
                    //1. parse to reverse polish notation expression
                    RPNExpression rpn = ParseToRPN(expression);
                    //2.
                    IExpression iexpression = rpn.Compile();
                    //3. into cache
                    _ExpressionCache[expression] = iexpression;

                    return iexpression;
                }
            }
        }

        private RPNExpression ParseToRPN(string expression) {
            RPNExpression rpnExpression = new RPNExpression(_FunctionEvaluator, _FunctionCallPrecedence);

            rpnExpression.DoWork(delegate {
                Token token = null;
                MatchCollection matches = this.Regex.Matches(expression);
                foreach (Match match in matches) {
                    int groupIndex = -1;
                    for (int index = 0; index < _TokenDefinitions.Count; index++) {
                        if (match.Groups[index + 1].Success) {
                            groupIndex = index;
                            break;
                        }
                    }
                    token = new Token(_TokenDefinitions[groupIndex], match.Value);
                    rpnExpression.ApplyToken(token);
                }
            });

            return rpnExpression;
        }

        #region Add TokenDefinition

        public void AddLeftParen(string pattern) {
            _TokenDefinitions.Add(new TokenDefinition(TokenType.LeftParen, _FunctionCallPrecedence, pattern));
        }

        public void AddRightParen(string pattern) {
            _TokenDefinitions.Add(new TokenDefinition(TokenType.RightParen, pattern));
        }

        public void AddArgumentSeparator(string pattern) {
            _TokenDefinitions.Add(new TokenDefinition(TokenType.ArgumentSeparator, pattern));
        }

        private void AddTokenDefinition(TokenDefinition tokenDefinition) {
            AddTokenDefinition(tokenDefinition, null);
        }

        private void AddTokenDefinition(TokenDefinition tokenDefinition, TokenEvaluator evaluator) {
            tokenDefinition.Evaluator = evaluator;

            TokenDefinition existing = _TokenDefinitions.Find(delegate(TokenDefinition td) {
                return td.Pattern == tokenDefinition.Pattern && !string.IsNullOrEmpty(td.Pattern);
            });

            if (existing != null)
                existing.Alternate = tokenDefinition;
            else
                _TokenDefinitions.Add(tokenDefinition);
        }

        public void AddUnaryOperator(string pattern, int precedence, TokenEvaluator evaluator) {
            AddTokenDefinition(new TokenDefinition(TokenType.UnaryOperator, precedence, OperatorAssociativity.Right, pattern), evaluator);
        }

        public void AddBinaryOperator(string pattern, int precedence, TokenEvaluator evaluator) {
            AddTokenDefinition(new TokenDefinition(TokenType.Operator, precedence, pattern), evaluator);
        }

        public void AddBinaryOperator(string pattern, int precedence, OperatorAssociativity associativity, TokenEvaluator evaluator) {
            AddTokenDefinition(new TokenDefinition(TokenType.Operator, precedence, associativity, pattern), evaluator);
        }

        public void AddTernaryOperator(string pattern1, string pattern2, int precedence, OperatorAssociativity associativity, TokenEvaluator evaluator) {
            TokenDefinition root = new TokenDefinition(TokenType.TernaryOperator, evaluator);

            TokenDefinition partial1 = new TokenDefinition(TokenType.TernaryOperator1, precedence, associativity, pattern1);
            TokenDefinition partial2 = new TokenDefinition(TokenType.TernaryOperator2, precedence, associativity, pattern2);

            partial1.Root = root;
            partial2.Root = root;

            AddTokenDefinition(partial1);
            AddTokenDefinition(partial2);
        }

        public void AddTerm(string pattern, TokenEvaluator evaluator) {
            AddTokenDefinition(new TokenDefinition(TokenType.Term, pattern), evaluator);
        }

        #endregion

        private Regex Regex {
            get {
                if (_RegEx == null) {
                    StringBuilder s = new StringBuilder();
                    for (int i = 0; i < _TokenDefinitions.Count; i++) {
                        if (!string.IsNullOrEmpty(_TokenDefinitions[i].Pattern)) {
                            if (s.Length > 0)
                                s.Append("|");
                            s.AppendFormat("(?<{0}>{1})", i + 1, _TokenDefinitions[i].Pattern);
                        }
                    }
                    _RegEx = new Regex(s.ToString(), RegexOptions.ExplicitCapture);
                }

                return _RegEx;
            }
        }

        public TokenEvaluator FunctionEvaluator {
            get { return _FunctionEvaluator; }
            set { _FunctionEvaluator = value; }
        }

        public int FunctionCallPrecedence {
            get { return _FunctionCallPrecedence; }
            set { _FunctionCallPrecedence = value; }
        }

        public ITemplateContext DefaultContext {
            get { return _DefaultContext; }
            set { _DefaultContext = value; }
        }
    }
}
