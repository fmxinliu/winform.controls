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
    internal class TemplateParser {
        private TemplateConfig _Config;

        public TemplateParser(TemplateConfig config) {
            _Config = config;
        }

        internal TokenNode Parse(string template) {
            //1. do parse
            List<TokenMatch> matches = this.InternalParse(template, _Config);
            //2. do lex
            TokenNode tree = this.InternalLexer(template, matches);

            return tree;
        }

        private List<TokenMatch> InternalParse(string template, TemplateConfig config) {
            List<TokenMatch> tokenMatches = new List<TokenMatch>();
            Regex regex = config.Regex;
            foreach (Match match in regex.Matches(template)) {
                TokenMatch tokenMatch = config.FindTokenMatch(match);
                if (tokenMatch != null) {
                    tokenMatches.Add(tokenMatch);
                }
            }
            return tokenMatches;
        }

        private TokenNode InternalLexer(string template, List<TokenMatch> tokenMatches) {
            Stack<TokenNode> nodeStack = new Stack<TokenNode>();
            TokenNode currentNode = new TokenNode();
            int lastIndex = 0;
            TextNode lastTextNode = null;
            bool checkEmptyLine = false;

            foreach (TokenMatch tokenMatch in tokenMatches) {
                Match match = tokenMatch.Match;

                #region For TextNode

                if (match.Index > lastIndex) {
                    string text = template.Substring(lastIndex, match.Index - lastIndex);
                    if (checkEmptyLine) {
                        text = CheckEmptyLine(lastTextNode, text);
                    }
                    lastTextNode = (TextNode) currentNode.ChildNodes.Add((TextNode) text);
                    checkEmptyLine = false;
                }
                lastIndex = match.Index + match.Length;
                checkEmptyLine = tokenMatch.RemoveEmptyLine && !checkEmptyLine;

                #endregion

                switch (tokenMatch.TokenType) {
                    #region switch TemplateTokenType

                    case TemplateTokenType.Set:
                        currentNode.ChildNodes.Add((SetNode) tokenMatch);
                        break;
                    case TemplateTokenType.Expression:
                        currentNode.ChildNodes.Add((ExpressionNode) tokenMatch);
                        break;
                    case TemplateTokenType.ForEach:
                        nodeStack.Push(currentNode);
                        currentNode = currentNode.ChildNodes.Add((ForeachNode) tokenMatch);
                        break;
                    case TemplateTokenType.While:
                        nodeStack.Push(currentNode);
                        currentNode = currentNode.ChildNodes.Add((WhileNode) tokenMatch);
                        break;
                    case TemplateTokenType.If:
                        nodeStack.Push(currentNode);
                        IfNode ifNode = currentNode.ChildNodes.Add((IfNode) tokenMatch);
                        currentNode = ifNode.TrueNode;
                        nodeStack.Push(ifNode);
                        break;
                    case TemplateTokenType.ElseIf:
                        IfNode last = nodeStack.Peek() as IfNode;
                        if (last == null) throw new Exception("error syntax: #elseif can't use without the #if");
                        currentNode = last.FalseNode;
                        last = currentNode.ChildNodes.Add((IfNode) tokenMatch);
                        currentNode = last.TrueNode;
                        nodeStack.Push(last);
                        break;
                    case TemplateTokenType.Else:
                        IfNode ifNode2 = nodeStack.Peek() as IfNode;
                        if (ifNode2 == null) throw new Exception("error syntax: #else can't use without the #if");
                        currentNode = ifNode2.FalseNode;
                        break;
                    case TemplateTokenType.EndBlock:
                        while (nodeStack.Peek() is IfNode) {
                            nodeStack.Pop();
                        }
                        currentNode = nodeStack.Pop();
                        break;

                        #endregion
                }
            }

            #region For TextNode

            if (lastIndex < template.Length - 1) {
                string text = template.Substring(lastIndex);
                if (checkEmptyLine) {
                    text = CheckEmptyLine(lastTextNode, text);
                }
                currentNode.ChildNodes.Add((TextNode) text);
            }

            #endregion

            return currentNode;
        }

        private string CheckEmptyLine(TextNode lastTextNode, string text) {
            string prevText = lastTextNode == null ? "" : lastTextNode.Text;

            Match m1 = Regex.Match(prevText, @"\n[\x20\t]*$", RegexOptions.Singleline);

            if (m1.Success) {
                Match m2 = Regex.Match(text, @"^[\x20\t\r]*?\n", RegexOptions.Singleline);
                if (m2.Success) {
                    if (lastTextNode != null)
                        lastTextNode.Text = lastTextNode.Text.Substring(0, m1.Index + 1);
                    text = text.Substring(m2.Length);
                }
            }
            return text;
        }
    }
}