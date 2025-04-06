using System;
using System.Collections.Generic;
using System.Linq;

namespace RedundantFileSearch
{
    internal class ParseInput
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns>"で囲まれていればtrue, 要素</returns>
        private Tuple<bool, string> GetWord(string src)
        {
            var find = src.IndexOf("\"");
            var s = src.Substring(0, find);
            if (!string.IsNullOrWhiteSpace(s)) return new Tuple<bool, string>(false, s);

            src = src.Substring(find + 1);
            return new Tuple<bool, string>(true, src.Substring(0, src.IndexOf("\"")));
        }

        enum EParseState
        {
            NORMAL,
            ENCLOSE_DOUBLEQUATE
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string[] SplitWords(string src)
        {
            if (src == null) return null;
            var ret = new List<string>();
            string buf = "";
            Stack<EParseState> state = new Stack<EParseState>();
            state.Push(EParseState.NORMAL);
            for (int i = 0; i<src.Length; i++)
            {
                switch (src[i])
                {
                    case ' ':
                        switch (state.Peek())
                        {
                            case EParseState.ENCLOSE_DOUBLEQUATE:
                                buf += src[i];
                                break;

                            default:
                                // 何もしない
                                break;
                        }
                        break;

                    case ',':
                    case '+':
                    case '(':
                    case ')':
                        {
                            switch (state.Peek())
                            {
                                case EParseState.ENCLOSE_DOUBLEQUATE:
                                    buf += src[i];
                                    break;

                                default:
                                    if (string.IsNullOrWhiteSpace(buf) == false) ret.Add(buf);
                                    buf = "";
                                    ret.Add(buf + src[i]);
                                    buf = "";
                                    break;
                            }
                        }
                        break;

                    case '\"':
                        if (string.IsNullOrWhiteSpace(buf) == false) ret.Add(buf);
                        buf = "";
                        switch (state.Peek())
                        {
                            case EParseState.ENCLOSE_DOUBLEQUATE:
                                state.Pop();
                                break;

                            default:
                                state.Push(EParseState.ENCLOSE_DOUBLEQUATE);
                                break;
                        }
                    break;

                    default:
                        buf += src[i];
                        break;
                }
            }
            if (string.IsNullOrWhiteSpace(buf) == false) ret.Add(buf);
            return ret.ToArray();
        }


        public static void OrderWords(string[] words, ref Queue<string> ret)
        {
            if (words.Length == 0) return;

            var parse = new Func<string[], int, int, Queue<string>, Queue<string>>((w, startPos, endPos, r) =>
            {
                if (startPos == 1) r.Enqueue(w[0]);
                OrderWords(w.Skip(startPos + 1).Take(endPos - startPos - 1).ToArray(), ref r);

                // 前方パース
                if (startPos >= 2)
                {
                    r.Enqueue(w[startPos - 1]);
                    OrderWords(w.Take(startPos - 1).ToArray(), ref r);
                }
                // 後方パース
                OrderWords(w.Skip(endPos + 1).ToArray(), ref r);
                return r;
            });

            var p = Array.FindIndex(words, x => x == "(");
            if (p == -1)
            {
                foreach (var item in words)
                {
                    ret.Enqueue(item);
                }
                return;
            }


            var np = Array.FindIndex(words, p + 1, x => x == "(");
            var cp = Array.FindIndex(words, p + 1, x => x == ")");
            if (np == -1 || cp < np)
            {
                ret = parse(words, p, cp, ret);
            }
            else
            {
                var c = words.Count(x => x == "(");
                for (var i = 0; i < c - 2; i++)
                {
                    p = Array.FindIndex(words, p + 1, x => x == "(");
                }
                var ccp = Array.FindIndex(words, cp + 1, x => x == ")");
                ret = parse(words, p, ccp, ret);
            }
        }


        public static void TestFunc()
        {
            Action<string[], string[]> check = (r, a) =>
            {
                for (int i = 0; i < a.Length; i++)
                {
                    if (r[i] != a[i]) throw new Exception("コード誤り");
                }
            };

            var ret = SplitWords("1 + (2 + 3) + 4,\"(56+,)\"");
            var ans = new string[] { "1", "+" , "(", "2", "+", "3", ")", "+", "4", ",", "(56+,)" };
            ret = SplitWords("(1+2) + (3,4)+5");
            ans = new string[] { "(", "1", "+", "2", ")", "+", "(", "3", ",", "4", ")", "+", "5" };
            check(ret, ans);

            ret = SplitWords("((1+2)+3),4+5");
            ans = new string[] { "(", "(", "1", "+", "2", ")", "+", "3", ")", ",", "4", "+", "5" };
            check(ret, ans);

            ret = SplitWords("4,((1+2)+3)+5");
            ans = new string[] { "4", ",", "(", "(", "1", "+", "2", ")", "+", "3", ")", "+", "5" };
            check(ret, ans);

            ret = SplitWords("(4,((1+2)+3))+5");
            ans = new string[] {"(", "4", ",", "(", "(", "1", "+", "2", ")", "+", "3", ")", ")", "+", "5" };
            check(ret, ans);

            Console.WriteLine("Success");
        }
    }
}
