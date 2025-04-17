using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace RedundantFileSearch
{
    public static class ParseInput
    {
        // トークン化（スペース無視、&&, ||, !!, 括弧対応）
        public static List<string> Tokenize(string expr)
        {
            var tokens = new List<string>();
            // !が付いたキーワードを1つのトークンとして認識
            var matches = Regex.Matches(expr, @"(!\w+)|(\w+)|(&&|\|\|)");
            foreach (Match m in matches)
            {
                tokens.Add(m.Value);
            }
            return tokens;
        }
        // RPN 変換（左から順評価、括弧・否定対応）
        public static List<string> ToRPN(string expr)
        {
            List<string> tokens = Tokenize(expr);
            Stack<string> opStack = new Stack<string>();
            List<string> rpn = new List<string>();
            bool expectOperand = true; // 演算子/括弧の後にオペランドを期待

            foreach (var token in tokens)
            {
                if (token == "&&" || token == "||")
                {
                    // 演算子はスタックに積む
                    while (opStack.Count > 0 && opStack.Peek() != "(")
                    {
                        rpn.Add(opStack.Pop());
                    }
                    opStack.Push(token);
                    expectOperand = true;
                }
                else if (token == "(")
                {
                    opStack.Push(token);
                    expectOperand = true;
                }
                else if (token == ")")
                {
                    while (opStack.Count > 0 && opStack.Peek() != "(")
                    {
                        rpn.Add(opStack.Pop());
                    }
                    if (opStack.Count > 0 && opStack.Peek() == "(")
                    {
                        opStack.Pop(); // 括弧を削除
                    }
                    expectOperand = false;
                }
                else
                {
                    // キーワード（通常または!付き）
                    rpn.Add(token);
                    if (!expectOperand && rpn.Count > 1)
                    {
                        // キーワードが連続 → 暗黙の AND
                        while (opStack.Count > 0 && opStack.Peek() != "(")
                        {
                            rpn.Add(opStack.Pop());
                        }
                        opStack.Push("&&");
                    }
                    expectOperand = false;
                }
            }

            // 残りの演算子をポップ
            while (opStack.Count > 0)
            {
                string op = opStack.Pop();
                if (op != "(") rpn.Add(op);
            }

            return rpn;
        }
        // ファイル評価
        public static bool EvaluateRpnForFile(string filePath, string[] fileContentLines, List<string> rpn)
        {
            bool Match(string keyword)
            {
                bool isExclude = keyword.StartsWith("!");
                if (isExclude) keyword = keyword.Substring(1);
                string fileName = Path.GetFileName(filePath);
                bool matched = fileName.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0 ||
                               fileContentLines.Any(line => line.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0);
                return isExclude ? !matched : matched;
            }

            Stack<bool> stack = new Stack<bool>();
            foreach (var token in rpn)
            {
                if (token == "&&")
                {
                    if (stack.Count < 2) return false;
                    bool b = stack.Pop();
                    bool a = stack.Pop();
                    stack.Push(a && b);
                }
                else if (token == "||")
                {
                    if (stack.Count < 2) return false;
                    bool b = stack.Pop();
                    bool a = stack.Pop();
                    stack.Push(a || b);
                }
                else
                {
                    // キーワード（通常または!付き）
                    stack.Push(Match(token));
                }
            }

            return stack.Count == 1 && stack.Pop();
        }
    }
}