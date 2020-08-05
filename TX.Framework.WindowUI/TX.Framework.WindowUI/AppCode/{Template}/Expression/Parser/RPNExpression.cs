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
    /// <summary>
    /// (Reverse Polish Notation)�沨������ʽ
    /// </summary>
    internal class RPNExpression {
        private readonly Stack<Token> _OperatorStack = new Stack<Token>();
        private readonly TokenQueue _TokenQueue = new TokenQueue();
        private readonly Stack<Token> _FunctionStack = new Stack<Token>();
        private readonly TokenEvaluator _FunctionEvaluator;
        private readonly int _FunctionCallPrecedence;
        private bool _LastWasFunction = false;
        private bool _LastWasOperator = true;
        private Token[] _TokenList = null;

        public RPNExpression(TokenEvaluator functionEvaluator, int functionCallPrecedence) {
            _FunctionEvaluator = functionEvaluator;
            _FunctionCallPrecedence = functionCallPrecedence;
        }

        internal void DoWork(CallbackVoidHandler callBack) {
            try {
                _OperatorStack.Clear();
                _TokenQueue.Clear();
                _FunctionStack.Clear();
                _LastWasFunction = false;

                callBack();
            } finally {
                while (_OperatorStack.Count > 0) {
                    _TokenQueue.Enqueue(_OperatorStack.Pop());
                }
                _TokenList = _TokenQueue.ToArray();
                _TokenQueue.Clear();
            }
        }

        internal void ApplyToken(Token token) {
            if (token.IsRightParen && _LastWasFunction) {
                _FunctionStack.Peek().NumTerms = 0;
            }

            _LastWasFunction = false;

            if (token.IsFunction) {
                _FunctionStack.Push(token);
                _LastWasFunction = true;
                token.NumTerms = 1;
            }

            if (token.IsLeftParen) {
                if (!_LastWasOperator) {
                    Token callToken = new Token(new TokenDefinition(TokenType.FunctionCall, _FunctionCallPrecedence, _FunctionEvaluator), token.Text);
                    ApplyToken(callToken);
                }
                _OperatorStack.Push(token);
                _LastWasOperator = true;
                return;
            }

            if (token.IsTerm) {
                _TokenQueue.Enqueue(token);
                _LastWasOperator = false;
                return;
            }

            if (token.IsArgumentSeparator) {
                while (_OperatorStack.Count > 0 && !_OperatorStack.Peek().IsLeftParen) {
                    _TokenQueue.Enqueue(_OperatorStack.Pop());
                }
                _FunctionStack.Peek().NumTerms++;
                _LastWasOperator = true;
                return;
            }

            if (token.IsRightParen) {
                while (_OperatorStack.Count > 0) {
                    Token stackOperator = _OperatorStack.Pop();
                    if (stackOperator.IsLeftParen) {
                        if (_OperatorStack.Count > 0 && _OperatorStack.Peek().IsFunction) {
                            _TokenQueue.Enqueue(_OperatorStack.Pop());
                            _FunctionStack.Pop();
                        }
                        break;
                    }
                    _TokenQueue.Enqueue(stackOperator);
                }
                _LastWasOperator = false;
                return;
            }

            if (_LastWasOperator != token.IsUnary) {
                if (token.Alternate != null) {
                    ApplyToken(token.Alternate);
                    return;
                }
                throw new Exception("Misplaced operator");
            }

            // When we get here, it certainly is an operator or function call
            while (_OperatorStack.Count > 0) {
                Token stackOperator = _OperatorStack.Peek();

                if ((token.Associativity == OperatorAssociativity.Right && token.Precedence < stackOperator.Precedence) ||
                    (token.Associativity == OperatorAssociativity.Left && token.Precedence <= stackOperator.Precedence)) {
                    _TokenQueue.Enqueue(_OperatorStack.Pop());
                }
                else {
                    break;
                }
            }
            _OperatorStack.Push(token);
            _LastWasOperator = true;
        }

        public Expression Compile() {
            Stack<Expression> tempStack = new Stack<Expression>();
            Expression currentExpression = null;

            foreach (Token token in _TokenList) {
                Expression[] parameters = null;
                if (!token.IsTerm) {
                    int numTerms = token.NumTerms;
                    if (token.IsFunction)
                        numTerms++;

                    parameters = new Expression[numTerms];
                    for (int i = 0; i < numTerms; i++) {
                        parameters[i] = tempStack.Pop();
                    }
                    Array.Reverse(parameters);
                }
                currentExpression = token.TokenEvaluator(token.Text, parameters);
                tempStack.Push(currentExpression);
            }

            return currentExpression;
        }

        private class TokenQueue : Queue<Token> {
            private Token _waiting;

            public new void Enqueue(Token token) {
                if (token.IsPartial) {
                    if (_waiting != null) {
                        if (_waiting.Root == token.Root)
                            base.Enqueue(new Token(token.Root, _waiting.Text + token.Text));
                        else
                            throw new Exception("Mismatched ternary operators");

                        _waiting = null;
                    }
                    else {
                        _waiting = token;
                    }

                    return;
                }

                base.Enqueue(token);
            }
        }
    }
}