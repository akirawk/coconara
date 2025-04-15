using System;
using System.Collections.Generic;
using System.Linq;

// ParseInput.cs に追加する RPN 変換ロジック（C#）

using System.Text.RegularExpressions;

// ParseInput.cs に追加する RPN 変換ロジック（C#）

using System.IO;


namespace RedundantFileSearch
{
    public static class ParseInput
    {
        public static List<string> Tokenize(string expr)
        {
            expr = Regex.Replace(expr, "\\s*,\\s*", ",");
            expr = Regex.Replace(expr, ",", " , ");
            var tokens = new List<string>();
            var matches = Regex.Matches(expr, "\\(|\\)|\\+|,|-\\S+|\\S+");
            foreach (Match m in matches)
            {
                tokens.Add(m.Value);
            }
            Console.WriteLine("Tokens: " + string.Join(", ", tokens)); // デバッグ用
            return tokens;
        }

        public static List<string> ToRPN(List<string> tokens)
        {
            var output = new List<string>();
            var ops = new Stack<string>();

            Dictionary<string, int> precedence = new Dictionary<string, int>
            {
                { "+", 2 },
                { ",", 1 }
            };

            foreach (var token in tokens)
            {
                if (token == "(")
                {
                    ops.Push(token);
                }
                else if (token == ")")
                {
                    while (ops.Count > 0 && ops.Peek() != "(")
                    {
                        output.Add(ops.Pop());
                    }
                    if (ops.Count > 0 && ops.Peek() == "(")
                    {
                        ops.Pop();
                    }
                }
                else if (precedence.ContainsKey(token))
                {
                    while (ops.Count > 0 && precedence.ContainsKey(ops.Peek()) && precedence[ops.Peek()] >= precedence[token])
                    {
                        output.Add(ops.Pop());
                    }
                    ops.Push(token);
                }
                else
                {
                    output.Add(token);
                }
            }

            while (ops.Count > 0)
            {
                output.Add(ops.Pop());
            }

            return output;
        }

        public static bool EvaluateRpnForFile(string filePath, string[] fileContentLines, List<string> rpn)
        {
            bool Match(string keyword)
            {
                bool isExclude = keyword.StartsWith("-");
                if (isExclude) keyword = keyword.Substring(1);

                string fileName = Path.GetFileName(filePath);
                bool matchFileName = fileName.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0;
                bool matchContent = fileContentLines.Any(line => line.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0);
                bool matched = matchFileName || matchContent;

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
                else
                {
                    bool result = Match(token);
                    stack.Push(result);
                }
            }

            return stack.Count == 1 && stack.Pop();
        }
    }
}
