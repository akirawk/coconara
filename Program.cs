using System;
using System.Windows.Forms;

namespace RedundantFileSearch
{
    internal static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            catch (Exception ex)
            {
                MessageBox.Show("起動時にエラーが発生しました: " + ex.Message, "例外", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
