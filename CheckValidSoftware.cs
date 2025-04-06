using RedundantFileSearch.Properties;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace RedundantFileSearch
{
    internal class CheckValidSoftware
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns>異常時はDateTime.MinValueを返す</returns>
        public static Tuple<DateTime, DateTime> GetValidDate()
        {
            var input = Settings.Default.キー2;

            string retStr = EncryptKey.Dec(input, Settings.Default.キー1);
            if (retStr.Length != 21) return new Tuple<DateTime, DateTime>(DateTime.MinValue, DateTime.MinValue);

            string p = "" + retStr[1] + retStr[3] + retStr[5] + retStr[7] + retStr[9] + retStr[11] + retStr[13];
            if (p != "Private") return new Tuple<DateTime, DateTime>(DateTime.MinValue, DateTime.MinValue);

            string i = "" + retStr[0] + retStr[2] + retStr[4] + retStr[6] + retStr[8] + retStr[10] + retStr[12] + retStr[14];

            var v = DateTime.ParseExact(i, "yyyyMMdd", null);

            var n = DateTime.Now.ToString("yyyy");
            var st = n.Substring(0, 2) + retStr.Substring(15);
            var s = DateTime.ParseExact(st, "yyyyMMdd", null);
            return new Tuple<DateTime, DateTime>(s, v);
        }

        private static System.Timers.Timer t = new System.Timers.Timer();

        /// <summary>
        /// 使用時間更新開始
        /// </summary>
        /// <returns>使用秒数</returns>
        public static double StartAndGetUseTime()
        {
            if (t.Enabled == false)
            {
                t.Interval = 10 * 1000;
                t.Elapsed += (o, e) =>
                {
                    Settings.Default.キー3 += 10;
                    Settings.Default.Save();
                };
                t.Enabled = true;
            }
            return Settings.Default.キー3;
        }
    }
}