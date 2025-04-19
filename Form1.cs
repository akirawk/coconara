using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using UglyToad.PdfPig;
using NPOI.XWPF.UserModel;
using NPOI.XWPF.Extractor;
using System.Drawing;
using System.Security.Principal;
using System.Threading.Tasks;
using NPOI.SS.Formula.Functions;
using System.Diagnostics;

namespace RedundantFileSearch
{
    public partial class Form1 : Form
    {
        public static string MAIL_SUBJECT_HEADER = "[全部調べる君]";

        private Task ReserveSearch = null;
        private const string TMP_FILE_NAME = "redundantFileSearch.csv";
        private const string ERROR_LOG = "errorLog.txt";

        private Dictionary<string, int> tmpResultFiles;
        private string[][] keyword;
        private bool isSearch = false;
        private int searchPos = 0;

        private String baseTitleText;

        public Form1()
        {
            InitializeComponent();
            panelExFrame1.BorderDrawStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            panelExFrame1.BorderColor = Color.Red;

            Shown += (o, e) => 
            {
                var validDay = CheckValidSoftware.GetValidDate();
                if (validDay.Item1 == DateTime.MinValue)
                {
                    MessageBox.Show(this, "設定値異常：コード01", "起動エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }
                if (validDay.Item2 < DateTime.Now || validDay.Item1 > DateTime.Now)
                {
                    MessageBox.Show(this, "有効期限が切れました。", "起動エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }

                var i = CheckValidSoftware.StartAndGetUseTime();
                Console.WriteLine(i);
                var d = validDay.Item1.AddSeconds(i);
                if (d > DateTime.Now)
                {
                    MessageBox.Show(this, "設定値異常：コード02", "起動エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }

                baseTitleText = "有効期限 " + validDay.Item1.ToString("yyyy/MM/dd") + "-" + validDay.Item2.ToString("yyyy/MM/dd");
                Text = baseTitleText;

                ReserveSearch = Task.Run(async () =>
                {
                    while (IsDisposed == false)
                    {
                        await Task.Delay(10 * 1000);
                        var reserveType = reserveForm.GetReserveType();
                        if (reserveType == ReserveForm.EReserveType.None) continue;
                        if (isSearch == true) continue;


                        var now = DateTime.Now;
                        switch (reserveType)
                        {
                            case ReserveForm.EReserveType.Month:
                                if (now.Day == reserveForm.GetMonth())
                                {
                                    if (now.Hour == 0 && now.Minute == 0 && (now.Second < 20))
                                    {
                                        btnSearch_Click(null, null);
                                        await Task.Delay(60 * 1000);
                                    }
                                }
                                break;

                            case ReserveForm.EReserveType.Week:
                                if (reserveForm.GetEDayOfWeeks().Contains(now.DayOfWeek))
                                {
                                    if (now.Hour == 0 && now.Minute == 0 && (now.Second < 20))
                                    {
                                        btnSearch_Click(null, null);
                                        await Task.Delay(60 * 1000);
                                    }
                                }
                                break;

                            default:
                                {
                                    var r = reserveForm.GetDay();
                                    if (r.Hours == now.Hour && r.Minutes == now.Minute && now.Second < 20)
                                    {
                                        btnSearch_Click(null, null);
                                        await Task.Delay(60 * 1000);
                                    }
                                }
                                break;
                        }
                    }
                });

                Resume();
            };
        }

        /// <summary>
        /// 中間ファイル読み出し
        /// ファイル書式
        /// 1行目：キーワード文字列
        /// 2行目：どこまで検索したか、int
        /// 3行目：出力csvファイル場所
        /// 4行目：検索ファイル更新期間
        /// 5行目以降：検索結果
        /// </summary>
        private void Resume()
        {
            var tmpFile = Directory.GetFiles(Path.GetTempPath(), TMP_FILE_NAME);
            if (tmpFile.Length == 0) return;

            using (var sr = new StreamReader(tmpFile[0]))
            {
                Invoke(new Action(() =>
                {
                    var l = sr.ReadLine();
                    if (string.IsNullOrEmpty(l)) return;
                    txtName.Text = l;

                    l = sr.ReadLine();
                    if (string.IsNullOrEmpty(l)) return;
                    searchPos = int.Parse(l);

                    l = sr.ReadLine();
                    if (string.IsNullOrEmpty(l)) return;
                    txtOutputPath.Text = l;

                    l = sr.ReadLine();
                    if (string.IsNullOrEmpty(l)) return;
                    dtpMin.Text = l;

                    l = sr.ReadLine();
                    if (string.IsNullOrEmpty(l)) return;
                    dtpMax.Text = l;

                    var ll = new List<string>();
                    while(true)
                    {
                        l = sr.ReadLine();
                        if (string.IsNullOrEmpty(l)) break;
                        if (lbxPath.Items.Contains(l)) continue;
                        ll.Add(l);
                    }
                    lbxPath.Items.AddRange(ll.ToArray());

                    ll = new List<string>();
                    while(true)
                    {
                        l = sr.ReadLine();
                        if (string.IsNullOrEmpty(l)) break;
                        ll.Add(l);
                    }
                    foreach (Control c in grpSearchExt.Controls)
                    {
                        if ((c is CheckBox) && ll.Contains(c.Text))
                        {
                            ((CheckBox)c).Checked = true;
                        }
                    }
                }));

                tmpResultFiles = new Dictionary<string, int>();
                while (sr.EndOfStream == false)
                {
                    var l = sr.ReadLine().Split('\"');
                    if (l.Length <= 1) continue;
                    tmpResultFiles.Add(l[0], int.Parse(l[1]));
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //CheckValidSoftware.testAll();
            //ParseInput.TestFunc();
        }


        private string FolderSelect(string title)
        {
            var fbd = new FolderBrowserDialog();
            fbd.Description = title;
            fbd.RootFolder = Environment.SpecialFolder.Desktop;
            //ダイアログを表示する
            if (fbd.ShowDialog(this) != DialogResult.OK) return null;
            return fbd.SelectedPath;
        }

        private void btnAddPath_Click(object sender, EventArgs e)
        {
            var ret = FolderSelect("検索フォルダを指定してください。");
            if (ret == null) return;
            lbxPath.Items.Add(ret);
        }

        private void btnRemovePath_Click(object sender, EventArgs e)
        {
            if (lbxPath.SelectedIndex < 0) return;
            lbxPath.Items.Remove(lbxPath.Items[lbxPath.SelectedIndex]);
        }


        private void btnAllClear_Click(object sender, EventArgs e)
        {
            lbxPath.Items.Clear();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            lbxPath.Items.Clear();
            txtOutputPath.Clear();
            foreach (Control c in grpSearchExt.Controls)
            {
                if ((c is CheckBox))
                {
                    ((CheckBox)c).Checked = false;
                }
            }
            txtExt.Clear();
            txtName.Clear();
            chxNameCsv.Checked = false;
        }

        private void btnOutputPath_Click(object sender, EventArgs e)
        {
            var ret = FolderSelect("検索結果出力フォルダを指定してください。");
            if (ret == null) return;

            // 書き込み権限をチェック
            if (!IsDirectoryWritable(ret))
            {
                MessageBox.Show("このフォルダには書き込み権限がありません。別のフォルダを選んでください。",
                                "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            txtOutputPath.Text = ret;
        }

        private void btnNameFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "キーワードファイルを指定してください。";
                openFileDialog.Filter = "csvファイル(*.csv)|*.csv";
                openFileDialog.InitialDirectory = Environment.SpecialFolder.Desktop.ToString();
                if (openFileDialog.ShowDialog() != DialogResult.OK) return;

                txtName.Text = openFileDialog.FileName;
            }
        }

        string CreateRegexExtension()
        {
            string ret = "";
            if (chxAll.Checked)
            {
                foreach (var ext in FileTypeDic.Keys)
                {
                    ret += @"|.*\." + ext;
                }
            }
            else
            {
                foreach (Control c in grpSearchExt.Controls)
                {
                    if ((c is CheckBox) && ((CheckBox)c).Checked)
                    {
                        ret += @"|.*\" + c.Text;
                    }
                }
            }
            if (string.IsNullOrEmpty(ret)) return ret;
            return ret.Substring(1);
        }

        private Stack<Tuple<Dictionary<string, int>, bool>> searchStack = new Stack<Tuple<Dictionary<string, int>, bool>>();

        private void btnSearch_Click(object sender, EventArgs e)
        {
            isSearch = !isSearch;
            if (isSearch == false)
            {
                btnSearch.Text = "検索開始";
                return;
            }

            Task.Run(()=>
            {
                try
                {
                    Resume();
                    DoSearch();
                    BeginInvoke(new Action(() => { btnSearch.Text = "検索開始"; }));
                }
                catch (Exception ex)
                {
                    isSearch = false;
                    BeginInvoke(new Action(() => { btnSearch.Text = "検索開始"; }));
                    BeginInvoke(new Action(() => { Text = baseTitleText + " [異常終了]"; }));
                    WriteErrorLog(ex.Message, ex.StackTrace);
                }
            });
        }

        private void updateRemainTime(long contentSearchFileCount, long fileCount = 0)
        {
            BeginInvoke(new Action(() =>
            {
                var totalSec = (((contentSearchFileCount * 35) + (fileCount*5)) / 1000) + 10;
                Text = baseTitleText + " [検索中] 残り時間" + (totalSec / 60).ToString("00") + ":" + (totalSec % 60).ToString("00");
            }));
        }
        // 書き込み権限チェック用のヘルパーメソッド
        private bool IsDirectoryWritable(string folderPath)
        {
            try
            {
                // フォルダが存在するか確認
                if (!Directory.Exists(folderPath))
                {
                    return false;
                }

                // テストファイルで書き込み権限をチェック
                string testFile = Path.Combine(folderPath, "test_write_permissions.tmp");
                using (FileStream fs = File.Create(testFile))
                {
                    fs.WriteByte(0); // ダミーデータ
                }
                File.Delete(testFile); // テストファイルを削除
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
            catch (IOException)
            {
                return false;
            }
            catch (Exception ex)
            {
                // その他のエラー（例: ネットワーク切断）もログに記録
                WriteErrorLog(ex.Message, ex.StackTrace);
                return false;
            }
        }
        void DoSearch()
        {
            if (lbxPath.Items.Count == 0) return;
            if (string.IsNullOrEmpty(txtOutputPath.Text)) return;
            if (chxNameCsv.Checked)
            {
                if (File.Exists(txtName.Text) == false) return;
            }
            else
            {
                if (string.IsNullOrEmpty(txtName.Text)) return;
            }
            BeginInvoke(new Action(() =>
            {
                Text = baseTitleText + " [検索中] 残り時間 -";
                btnSearch.Text = "中断";
            }));

            var updateTime = DateTime.Now;
            var UPDATE_SPAN = new TimeSpan(TimeSpan.TicksPerSecond);

            /// 検索ファイルリスト生成
            var searchFiles = new List<string>();
            {
                var minDate = dtpMin.Value.Date;
                var maxDate = dtpMax.Value.Date;
                var r = CreateRegexExtension();
                var reg = new Regex(r);

                /// ファイル更新日チェック
                foreach (string path in lbxPath.Items)
                {
                    var files = new List<string>();
                    var updateCheckFunc = new Func<String, int, bool>((item, index) =>
                    {
                        if (DateTime.Now - updateTime > UPDATE_SPAN)
                        {
                            var fileNum = searchFiles.Count + files.Count;
                            updateRemainTime((long)(fileNum * 0.4), fileNum - index);
                            updateTime = DateTime.Now;
                        }

                        if (reg.IsMatch(item) == false) return false;
                            var t = File.GetLastWriteTime(item).Date;
                            return t >= minDate && t <= maxDate;
                    });
                        AddFiles(path, files);
                    searchFiles.AddRange(files.Where(updateCheckFunc));
                }
            }

            updateRemainTime(searchFiles.Count);

            /// 検索キーワード取得
            var keyList = new List<string[]>
            {
                new string[] { txtName.Text }
            };
            if (chxNameCsv.Checked)
            {
                keyList.Clear();

            if (Path.GetExtension(txtName.Text) != ".csv") return;
                string l;
                using (var sr = new StreamReader(txtName.Text))
                {
                while(sr.EndOfStream == false)
                    {
                        l = sr.ReadLine();
                    if (l == null) continue;
                    keyList.Add(ParseInput.SplitWords(l));
                    }
                }
            }
            keyword = keyList.ToArray();
            if (keyword == null || keyword.Length == 0) return;


            string outputPath = null;
            var tmpFilePath = Path.GetTempPath() + TMP_FILE_NAME;
            foreach (var keyString in keyword)
            {
                //// 結果バッファ初期化
                searchStack.Clear();
                tmpResultFiles = new Dictionary<string, int>();

                foreach (var file in searchFiles)
                {
                    tmpResultFiles.Add(file, -1);
                }

                using (var tmpOutputFile = new StreamWriter(tmpFilePath))
                {
                    for (int i = searchPos; i < searchFiles.Count; i++)
                    {
                        // 中断のため、ループを抜ける
                        if (isSearch == false)
                        {
                            if (cbxSaveTmp.Checked == false)
                            {
                                tmpOutputFile.Close();
                                File.Delete(tmpFilePath);
                            }
                            return;
                        }

                        bool isAnd = false;
                        foreach (var item in keyString)
                        {
                            switch (item)
                            {
                                case ")":
                                    if (searchStack.Count != 0)
                                    {
                                        var p = searchStack.Pop();
                                        if (p != null)
                                        {
                                            tmpResultFiles = AndOr(p.Item1, p.Item2, tmpResultFiles);
                                        }
                                    }
                                    searchStack.Push(new Tuple<Dictionary<string, int>, bool>(tmpResultFiles, isAnd));
                                    continue;

                                case "+":
                                    isAnd = true;
                                    continue;

                                case "(":
                                case ",":
                                    continue;

                                default:
                                    {
                                        var r = SearchList(tmpResultFiles.ElementAt(i), isAnd, item);
                                        tmpResultFiles[r.Key] = r.Value;
                                        isAnd = false;
                                    }
                                    break;
                            }
                    }

                        if (cbxSaveTmp.Checked == true)
                        {
                            // 中間ファイル出力
                            OutputTmpFile(tmpResultFiles, i, tmpOutputFile);
                        }

                        // 残り時間表示更新
                        if (DateTime.Now - updateTime > UPDATE_SPAN)
                        {
                            updateRemainTime(searchFiles.Count - i);
                            updateTime = DateTime.Now;
                        }
                    }
                }

                if (reserveForm == null || (reserveForm != null && reserveForm.chkOutCsv.Checked))
                {
                        outputPath = OutputCsv(tmpResultFiles);
                }
            }

            if (reserveForm != null && (reserveForm.chkMailInfo.Checked || reserveForm.chkMailCsv.Checked))
            {
                    reserveForm.SendMail(Form1.MAIL_SUBJECT_HEADER + "検索完了通知", "全部調べる君での検索が完了しました。", reserveForm.chkMailCsv.Checked ? outputPath : null);
                }

            searchPos = 0;
            tmpResultFiles.Clear();
            File.Delete(tmpFilePath);
            isSearch = false;
            BeginInvoke(new Action(() =>
            {
                Text = baseTitleText + " [検索終了]";
            }));
        }
        void OutputTmpFile(Dictionary<string, int> tmpResults, int pos, StreamWriter sw)
        {
            sw.WriteLine(txtName.Text);
            sw.WriteLine(pos);
            sw.WriteLine(txtOutputPath.Text);
            sw.WriteLine(dtpMin.Text);
            sw.WriteLine(dtpMax.Text);
            foreach (var item in lbxPath.Items)
            {
                sw.WriteLine(item);
            }
            sw.WriteLine();
            foreach (Control c in grpSearchExt.Controls)
            {
                if ((c is CheckBox) && ((CheckBox)c).Checked == true)
                {
                    sw.WriteLine(c.Text);
                }
            }
            sw.WriteLine();


            foreach (var item in tmpResults)
            {
                sw.WriteLine(item.Key + '\"' + item.Value);
            }
            sw.Flush();
            sw.BaseStream.Seek(0, SeekOrigin.Begin);
        }


        private static void AddFiles(string path, IList<string> files)
        {
            try
            {
                Directory.GetFiles(path)
                    .ToList()
                    .ForEach(s => { files.Add(s); });

                Directory.GetDirectories(path)
                    .ToList()
                    .ForEach(s => AddFiles(s, files));
            }
            catch (UnauthorizedAccessException)
            {
                // ok, so we are not allowed to dig into that directory. Move on.
            }
            catch (DirectoryNotFoundException)
            {
                // Do Nothing
            }
        }

        /// <summary>
        /// and / 
        ///   -1, -1 = -1
        ///   *(-2), *(-2) = int.MaxValue
        ///   -1, *(-2) = -1
        /// or  / 
        ///   -1, -1 = -1
        ///   *(-2), *(-2) = int.MaxValue
        ///   -1, *(-2) = *(-2)
        /// </summary>
        /// <param name="alpha"></param>
        /// <param name="isAnd"></param>
        /// <param name="beta"></param>
        /// <returns></returns>
        Dictionary<string, int> AndOr(Dictionary<string, int> alpha, bool isAnd, Dictionary<string, int> beta)
        {
            var ret = new Dictionary<string, int>();
            foreach (var item in alpha)
            {
                var retInt = -1;
                // and
                if (isAnd)
                {
                    if (beta.TryGetValue(item.Key, out int resultAnd))
                    {
                        if (resultAnd != -1 && item.Value != -1)
                        {
                            retInt = -3;
                        }
                    }
                    else
                    {
                        retInt = item.Value;
                    }
                    ret.Add(item.Key, retInt);
                    continue;
                }

                // or
                if (beta.TryGetValue(item.Key, out int resultOr))
                {
                    if (resultOr == -1 || item.Value == -1)
                    {
                        if (resultOr != -1)
                        {
                            retInt = resultOr;
                        }
                        else if(item.Value != -1)
                        {
                            retInt = item.Value;
                        }
                    }
                }
                else
                {
                    retInt = item.Value;
                }
                ret.Add(item.Key, retInt);
            }
            return ret;
        }
        KeyValuePair<string, int> SearchList(
                KeyValuePair<string, int> file,
                bool isAnd,
                string keyword)
        {
            try
            {
                var raw = keyword;   // ログ用に元文字列を保持
                
                // ① NOT かどうか判定
                bool isNot = false;
                if (!string.IsNullOrEmpty(keyword) && keyword[0] == '!')
                {
                    isNot = true;
                    keyword = keyword.Substring(1);   // '!' を外す
                }

                // ② 通常ヒット判定
                var ret = isHitKeyword(file.Key, keyword);

                // ③ NOT の場合は真偽を反転
                //    ヒットなし(-1) → 成功(0) , ヒットあり → 失格(-1)
                if (isNot)
                {
                    ret = (ret == -1) ? 0 : -1;
                }

                // ④ 既存の AND／OR 合成ロジック
                if (isAnd)
                {
                    if (file.Value > 0 && ret != -1)
                    {
                        ret = int.MaxValue;   // 両方ヒット → AND 成功
                    }
                    // file.Value が失敗していても ret の結果が残ってしまう
                    // どちらかが失敗なら全体失敗
                    if (file.Value == -1 || ret == -1)
                    {
                        ret = -1;
                    }
                    else
                    {
                        // どちらもヒット（行番号・-2・0 など -1 以外なら可）
                        ret = int.MaxValue;
                    }
                }
                else
                {
                    if (ret == -1)           // OR で失敗したら前回結果を残す
                    {
                        ret = file.Value;
                    }
                }
#if DEBUG
                Debug.WriteLine($"[SearchList] file=\"{Path.GetFileName(file.Key)}\" "
                +$"keyword=\"{raw}\" isNot={isNot} result={ret}");
#endif
                return new KeyValuePair<string, int>(file.Key, ret);
            }
            catch (Exception e)
            {
                WriteErrorLog(e.Message, e.StackTrace);
                return new KeyValuePair<string, int>(file.Key, -1);
            }
        }
        // ----------------- NEW: 演算子テーブル -----------------
        static readonly Dictionary<string, int> OP_PRIORITY = new Dictionary<string, int>
        {
            { "+", 2 },   // AND
            { ",", 1 }    // OR
        };

        // ----------------- NEW: トークン列 → RPN -----------------
        static Queue<string> ToRpn(string[] tokens)
        {
            var output = new Queue<string>();
            var opStack = new Stack<string>();

            foreach (var tk in tokens)
            {
                if (tk == "+" || tk == ",")
                {   // 演算子
                    while (opStack.Count > 0
                           && OP_PRIORITY.ContainsKey(opStack.Peek())
                           && OP_PRIORITY[opStack.Peek()] >= OP_PRIORITY[tk])
                    {
                        output.Enqueue(opStack.Pop());
                    }
                    opStack.Push(tk);
                }
                else if (tk == "(")
                {
                    opStack.Push(tk);
                }
                else if (tk == ")")
                {
                    while (opStack.Peek() != "(")
                        output.Enqueue(opStack.Pop());
                    opStack.Pop();            // '(' を捨てる
                }
                else
                {   // オペランド（キーワード）
                    output.Enqueue(tk);
                }
            }
            while (opStack.Count > 0) output.Enqueue(opStack.Pop());
            return output;
        }

        // ----------------- NEW: RPN 評価（１ファイル分） ---------------
        bool EvalRpn(Queue<string> rpn, string filePath)
        {
            var st = new Stack<bool>();

            foreach (var tk in rpn)
            {
                switch (tk)
                {
                    case "+":
                        {   // AND
                            var b = st.Pop();
                            var a = st.Pop();
                            st.Push(a && b);
                            break;
                        }
                    case ",":
                        {   // OR
                            var b = st.Pop();
                            var a = st.Pop();
                            st.Push(a || b);
                            break;
                        }
                    default:
                        {   // キーワード（! 対応）
                            bool isNot = tk.StartsWith("!");
                            var word = isNot ? tk.Substring(1) : tk;
                            bool hit = (isHitKeyword(filePath, word) != -1);
                            st.Push(isNot ? !hit : hit);
                            break;
                        }
                }
            }
            return st.Pop();   // 最終結果
        }
        enum EFileType
        {
            ALL,
            EXIF,
            OTHER
        }

        static Dictionary<string, EFileType> FileTypeDic = new Dictionary<string, EFileType>()
        {
            {"xlsx", EFileType.ALL},
            {"xls", EFileType.ALL},
            {"docx", EFileType.ALL},
            {"doc", EFileType.ALL},
            {"pptx", EFileType.ALL},
            {"ppt", EFileType.ALL},
            {"pdf", EFileType.ALL},
            {"txt", EFileType.ALL},
            {"htm", EFileType.ALL},
            {"html", EFileType.ALL},
            {"rtf", EFileType.ALL},
            {"jpg", EFileType.EXIF},
            {"jpeg", EFileType.EXIF},
            {"gif", EFileType.EXIF},
            {"bmp", EFileType.EXIF},
            {"png", EFileType.EXIF},
            {"tif", EFileType.EXIF},
            {"tiff", EFileType.EXIF},
            {"img", EFileType.EXIF},
            {"avi", EFileType.EXIF},
            {"wmv", EFileType.EXIF},
            {"wmf", EFileType.EXIF},
            {"mp3", EFileType.EXIF},
            {"mp4", EFileType.EXIF},
            {"mov", EFileType.EXIF},
            {"mkv", EFileType.EXIF},
            {"db", EFileType.OTHER},
            {"lzh", EFileType.OTHER},
            {"zip", EFileType.OTHER},
            {"iso", EFileType.OTHER},
        };

        /// <summary>
        /// 
        /// </summary>
        /// <returns>-1 : 見つからなかった、-2 : ファイル名ヒット、int.MaxValue ; 複数行ヒット</returns>
        /// <exception cref="NotImplementedException"></exception>
        int isHitKeyword(string filePath, string keyword)
        {
            if (Path.GetFileName(filePath).IndexOf(keyword) >= 0)
            {
                return -2;
            }

            var ext = Path.GetExtension(filePath);
            if (ext.Length == 0)
            {
                return -1;
            }

            ext = ext.Substring(1);
            if (FileTypeDic.ContainsKey(ext) == false)
            {
                return -1;
            }
            if (!CheckFileCanOpen(filePath))
            {
                // ユーザーが中断（キャンセル）を選んだ → 検索全体を中止
                return -1;
            }
            switch (FileTypeDic[ext])
            {
                case EFileType.ALL:
                    switch (ext)
                    {
                        case "txt":
                    case "htm":
                    case "html":
                        return SearchText(filePath, keyword);


                    case "xlsx":
                        case "xls":
                        return SearchExcel(filePath, keyword);

                    case "pdf":
                        return SearchPdf(filePath, keyword);

                    case "docx":
                        case "doc":
                    case "pptx":
                        case "ppt":
                        return SearchWordPpt(filePath, keyword);
                    case "rtf":
                        return SearchRtf(filePath, keyword);

                    default:
                        throw new NotImplementedException();
                    }

                case EFileType.EXIF:
                    if (SearchExif(filePath, keyword) == true)
                    {
                        return 0;
                    }
                    break;

                case EFileType.OTHER:
                    // DO NOTHING.
                default:
                    break;
            }
            return -1;
        }
        private bool CheckFileCanOpen(string filePath)
        {
            while (true)
            {
                try
                {
                    using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {
                        // 何も読まない。開けるか確認だけ。
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    var result = MessageBox.Show(
                        $"ファイル「{filePath}」を開くことができませんでした。\n\n{ex.Message}\n\nリトライしますか？",
                        "ファイルオープンエラー",
                        MessageBoxButtons.RetryCancel,
                        MessageBoxIcon.Warning);

                    if (result == DialogResult.Retry)
                    {
                        continue; // 再試行
                    }
                    else
                    {
                        // スキップとして false を返す
                        return false;
                    }
                }
            }
        }
        private int SearchText(string filePath, string keyword)
        {
            var findRowNum = new List<int>();
            int p = 0;
            using (var r = new StreamReader(filePath))
            {
                while(r.EndOfStream == false)
                {
                    p++;
                    var l = r.ReadLine();
                    if (l.Contains(keyword))
                    {
                        findRowNum.Add(p);
                    }
                }
            }

            if (findRowNum.Count == 0)
            {
                return -1;
            }

            if (findRowNum.Count == 1)
            {
                return findRowNum[0];
            }
            return int.MaxValue;
        }

        private bool SearchExif(string filePath, string keyword)
        {
            try
            {
                var account = File.GetAccessControl(filePath).GetOwner(typeof(NTAccount));

                var file = new FileInfo(filePath);
                var properties = new string[]
                {
                    file.Length.ToString(),                    // ファイルサイズ
                    File.GetCreationTime(filePath).ToString(), // 作成日時
                    File.GetAttributes(filePath).ToString(),   // 属性
                    account.Value                              // 所有者
                };

                if (properties.FirstOrDefault((x) => { return x.Contains(keyword); }) != null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {

                return false;
            }
        }

        private int SearchExcel(string filePath, string searchText)
        {
            DataSet dataSet = null;
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                dataSet = reader.AsDataSet();
            }

            var findRowNum = new List<int>();
            foreach (DataTable sheet in dataSet.Tables)
            {
                for (var row = 0; row < sheet.Rows.Count; row++)
                {
                    for (var col = 0; col < sheet.Columns.Count; col++)
                    {
                        var cellValue = sheet.Rows[row][col].ToString();
                        if (cellValue.IndexOf(searchText) >= 0)
                        {
                            findRowNum.Add(row + 1);
                        }
                    }
                }
            }

            if (findRowNum.Count == 0)
            {
                return -1;
            }

            if (findRowNum.Count == 1)
            {
                return findRowNum[0];
            }
            return int.MaxValue;
        }

        private int SearchPdf(string filePath, string searchText)
        {
            var findRowNum = new List<int>();
            var count = 1;
            using (var document = PdfDocument.Open(filePath))
            {
                foreach (var page in document.GetPages())
                {
                    var pageText = page.Text;
                    if (pageText.IndexOf(searchText) >= 0)
                    {
                        findRowNum.Add(count);
                    }
                    count++;
                }
            }
            if (findRowNum.Count == 0) return -1;

            if (findRowNum.Count == 1)
            {
                return findRowNum[0];
            }
            return int.MaxValue;
        }

        private int SearchWordPpt(string filePath, string searchText)
        {
            var findRowNum = new List<int>();
            var count = 1;
            using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                var doc = new XWPFDocument(file);
                var ex = new XWPFWordExtractor(doc);
                if (ex.Text.IndexOf(searchText) >= 0)
                {
                    findRowNum.Add(count);
                }
            }

            if (findRowNum.Count == 0) return -1;
            if (findRowNum.Count == 1)
            {
                return findRowNum[0];
            }
            return int.MaxValue;
        }

        private int SearchRtf(string filePath, string searchText)
        {
            var findRowNum = new List<int>();
            int lineCount = 1;

            using (var rtb = new RichTextBox())
            {
                rtb.LoadFile(filePath, RichTextBoxStreamType.RichText);
                var allText = rtb.Text;
                using (var reader = new StringReader(allText))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.IndexOf(searchText, System.StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            findRowNum.Add(lineCount);
                        }
                        lineCount++;
                    }
                }
            }

            if (findRowNum.Count == 0) return -1;
            if (findRowNum.Count == 1) return findRowNum[0];
            return int.MaxValue;
        }

        private void chxNameCsv_CheckedChanged(object sender, EventArgs e)
        {
            btnNameFile.Enabled = chxNameCsv.Checked;
            txtName.Enabled = !chxNameCsv.Checked;
        }


        string OutputCsv(Dictionary<string, int> resultSearch)
        {
            var outputPath = txtOutputPath.Text;
            if (Path.GetExtension(outputPath).Contains("csv") == false)
            {
                outputPath += "\\" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_SearchResult.csv";
            }

            //  sort
            var sortedResult = resultSearch.ToList();
            if (rbnFileSize.Checked)
            {
                sortedResult.Sort((x, y) =>
                {
                    var xi = new FileInfo(x.Key);
                    var yi = new FileInfo(y.Key);
                    return (int)(xi.Length - yi.Length);
                });
            }
            else if (rbnUpdate.Checked)
            {
                sortedResult.Sort((x, y) =>
                {
                    return (int)(File.GetLastWriteTime(x.Key) - File.GetLastWriteTime(y.Key)).TotalSeconds;
                });
            }

            using (var file = new StreamWriter(outputPath, false, System.Text.Encoding.GetEncoding("Shift_jis")))
            {
                foreach (KeyValuePair<string, int> item in sortedResult)
                {
                    if (item.Value == -1) continue;

                    var accessString = "";
                    try
                    {
                        var account = File.GetAccessControl(item.Key).GetOwner(typeof(NTAccount));
                        accessString = account.Value;
                    }
                    catch (Exception)
                    {
                        // Do Notiong
                    }

                    string outputVal = item.Value.ToString();
                    if (item.Value == int.MaxValue) outputVal = "over";
                    if (item.Value == -2) outputVal = "0";

                    var f = new FileInfo(item.Key);
                    var line = outputVal + "," +
                        Path.GetFileName(item.Key) + "," +
                        item.Key + "," +
                        f.Length + "," +
                        Path.GetExtension(item.Key) + ",";
                    if (chxWriteCreate.Checked) line += File.GetCreationTime(item.Key) + ",";
                    if (chxWriteUpdate.Checked) line += File.GetLastWriteTime(item.Key) + ",";
                    if (chxWriteAccess.Checked) line += File.GetLastAccessTime(item.Key) + ",";
                    line += accessString;

                    file.WriteLine(line);
                }

                long totalSize = 0;
                foreach (var fi in sortedResult)
                {
                    try
                    {
                        var filePath = fi.Key;
                        var f = new FileInfo(filePath);
                        totalSize += f.Length;
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
                file.WriteLine("検索総ファイル数"+ sortedResult.Count +"個、総データ容量"+ totalSize + "バイト");
            }
            return outputPath;
        }


        ReserveForm reserveForm = new ReserveForm();

        private void btnAuto_Click(object sender, EventArgs e)
        {
            if (reserveForm.Visible) return;
            reserveForm.Show(this);
        }

        private void WriteErrorLog(string msg, string trace)
                    {
            if (File.Exists(ERROR_LOG))
                    {
                var f = new FileInfo(ERROR_LOG);
                if (f.Length >= 10 * 1024 * 1024) File.Delete(f.FullName);
                    }

            using (var sw = new StreamWriter(ERROR_LOG, true))
            {
                sw.WriteLine(msg);
                sw.WriteLine(trace);
            }
        }
    }
}
