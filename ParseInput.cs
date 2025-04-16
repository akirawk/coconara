using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace RedundantFileSearch
{
    public static class ParseInput
    {
        // トークン化（スペース無視、括弧・否定対応）
        public static List<string> Tokenize(string expr)
        {
            var tokens = new List<string>();
            var matches = Regex.Matches(expr, @"-?\S+|[+,\(\)]");
            foreach (Match m in matches)
            {
                tokens.Add(m.Value);
            }
            return tokens;
        }

        // RPN 変換（括弧優先、左から順、否定対応）
        public static List<string> ToRPN(string expr)
        {
            List<string> tokens = Tokenize(expr);
            Stack<string> opStack = new Stack<string>();
            List<string> rpn = new List<string>();

            for (int i = 0; i < tokens.Count; i++)
            {
                string token = tokens[i];
                if (token == "+" || token == ",")
                {
                    // 同優先順位で左から順
                    while (opStack.Count > 0 && opStack.Peek() != "(")
                    {
                        rpn.Add(opStack.Pop());
                    }
                    opStack.Push(token);
                }
                else if (token == "(")
                {
                    opStack.Push(token);
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
                    // 否定条件が後ろ（例: (大阪+名古屋)-沖縄）
                    if (i + 1 < tokens.Count && tokens[i + 1].StartsWith("-"))
                    {
                        string exclude = tokens[i + 1];
                        i++; // 否定をスキップ
                        if (rpn.Count > 0)
                        {
                            string last = rpn[rpn.Count - 1];
                            rpn.RemoveAt(rpn.Count - 1);
                            rpn.Add($"({last} {exclude})");
                        }
                    }
                }
                else if (token.StartsWith("-") && i > 0 &&
                         tokens[i - 1] != "+" && tokens[i - 1] != "," && tokens[i - 1] != "(")
                {
                    // 単独否定（例: 名古屋 -大阪）
                    if (rpn.Count > 0)
                    {
                        string prevKeyword = rpn[rpn.Count - 1];
                        rpn.RemoveAt(rpn.Count - 1);
                        rpn.Add($"({prevKeyword} {token})");
                    }
                }
                else
                {
                    rpn.Add(token);
                }
            }

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
                bool isExclude = keyword.StartsWith("-");
                if (isExclude) keyword = keyword.Substring(1);
                string fileName = Path.GetFileName(filePath);
                bool matched = fileName.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0 ||
                               fileContentLines.Any(line => line.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0);
                return isExclude ? !matched : matched;
            }

            Stack<bool> stack = new Stack<bool>();
            foreach (var token in rpn)
            {
                if (token == "+")
                {
                    if (stack.Count < 2) return false;
                    bool b = stack.Pop();
                    bool a = stack.Pop();
                    stack.Push(a && b);
                }
                else if (token == ",")
                {
                    if (stack.Count < 2) return false;
                    bool b = stack.Pop();
                    bool a = stack.Pop();
                    stack.Push(a || b);
                }
                else if (token.StartsWith("(") && token.EndsWith(")"))
                {
                    // 否定グループ（例: "(大阪+名古屋 -沖縄)"）
                    string inner = token.Substring(1, token.Length - 2);
                    var subTokens = Tokenize(inner);
                    if (subTokens.Count >= 2 && subTokens[subTokens.Count - 1].StartsWith("-"))
                    {
                        // 最後のトークンが否定
                        string exclude = subTokens[subTokens.Count - 1];
                        subTokens.RemoveAt(subTokens.Count - 1);
                        bool groupResult;
                        if (subTokens.Count == 1)
                        {
                            groupResult = Match(subTokens[0]);
                        }
                        else
                        {
                            // サブグループを評価
                            List<string> subRpn = ToRPN(string.Join(" ", subTokens));
                            groupResult = EvaluateRpnForFile(filePath, fileContentLines, subRpn);
                        }
                        bool excludeResult = Match(exclude);
                        stack.Push(groupResult && excludeResult);
                    }
                    else
                    {
                        // 通常の括弧グループ
                        List<string> subRpn = ToRPN(inner);
                        stack.Push(EvaluateRpnForFile(filePath, fileContentLines, subRpn));
                    }
                }
                else
                {
                    stack.Push(Match(token));
                }
            }

            return stack.Count == 1 && stack.Pop();
        }
    }
}