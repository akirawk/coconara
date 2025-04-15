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
            /// 検索キーワード取得
            var keyList = new List<string[]>();
            if (chxNameCsv.Checked)
            {
                keyList.Clear();
                if (Path.GetExtension(txtName.Text) != ".csv") return;
                string l;
                using (var sr = new StreamReader(txtName.Text))
                {
                    while (sr.EndOfStream == false)
                    {
                        l = sr.ReadLine();
                        if (l == null) continue;
                        keyList.Add(ParseInput.ToRPN(ParseInput.Tokenize(l)).ToArray());
                    }
                }
            }
            else
            {
                // 直接入力の場合も Tokenize と ToRPN を適用
                keyList.Add(ParseInput.ToRPN(ParseInput.Tokenize(txtName.Text)).ToArray());
            }
            keyword = keyList.ToArray();
            if (keyword == null || keyword.Length == 0) return;

            string outputPath = null;
            var tmpFilePath = Path.GetTempPath() + TMP_FILE_NAME;
            foreach (var keyString in keyword)
            {
                // 検索結果初期化
                searchStack.Clear();
                tmpResultFiles = new Dictionary<string, int>();

                foreach (var file in searchFiles)
                {
                    bool isHit = false;

                    try
                    {
                        // ファイル中身を読む（拡張子によって簡略化OK）
                        string[] content = File.Exists(file) ? File.ReadAllLines(file) : Array.Empty<string>();

                        // 評価する（keyString は RPN トークン配列）
                        isHit = EvaluateRpnForFile(file, content, new List<string>(keyString));
                    }
                    catch (Exception ex)
                    {
                        WriteErrorLog(ex.Message, ex.StackTrace);
                        isHit = false;
                    }

                    if (isHit)
                    {
                        tmpResultFiles[file] = 1;
                    }
                }

                if (reserveForm == null || (reserveForm != null && reserveForm.chkOutCsv.Checked))
                {
                    outputPath = OutputCsv(tmpResultFiles);
                    int hitCount = tmpResultFiles.Count;
                    MessageBox.Show($"{hitCount} 件の検索結果を保存しました。",
                        "保存完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        KeyValuePair<string, int> SearchList(KeyValuePair<string, int> file, bool isAnd, string keyword)
        {
            try
            {
                var ret = isHitKeyword(file.Key, keyword);

                if (isAnd == true)
                {
                    if (file.Value > 0 && ret != -1)
                    {
                        ret = int.MaxValue;
                    }
                }
                else
                {
                    if (ret == -1)
                    {
                        ret = file.Value;
                    }
                }
                return new KeyValuePair<string, int>(file.Key, ret);
            }
            catch (Exception e)
            {
                WriteErrorLog(e.Message, e.StackTrace);
                return new KeyValuePair<string, int>(file.Key, -1);
            }
        }


        private bool EvaluateRpnForFile(string filePath, string[] fileContentLines, List<string> rpn)
        {
            Func<string, bool> isHit = (keyword) =>
            {
                bool isExclude = keyword.StartsWith("-");
                if (isExclude) keyword = keyword.Substring(1);

                string fileName = Path.GetFileName(filePath);

                bool matchFileName = fileName.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0;
                bool matchContent = fileContentLines.Any(line => line.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0);
                bool result = matchFileName || matchContent;

                return isExclude ? !result : result;
            };

            Stack<bool> stack = new Stack<bool>();
            foreach (var token in rpn)
            {
                switch (token)
                {
                    case "+":
                        {
                            var b = stack.Pop();
                            var a = stack.Pop();
                            stack.Push(a && b);
                            break;
                        }
                    case ",":
                        {
                            var b = stack.Pop();
                            var a = stack.Pop();
                            stack.Push(a || b);
                            break;
                        }
                    default:
                        stack.Push(isHit(token));
                        break;
                }
            }

            return stack.Count > 0 && stack.Pop();
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
            bool isExclude = keyword.StartsWith("-");
            if (isExclude) keyword = keyword.Substring(1);

            string ext = Path.GetExtension(filePath).ToLower().TrimStart('.');

            // ファイル名にマッチ？
            bool matchFileName = Path.GetFileName(filePath).IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0;

            if (isExclude)
            {
                if (matchFileName) return -1;  // 除外対象に一致したら即NG
            }
            else
            {
                if (matchFileName) return -2;  // 通常一致
            }

            // ファイルの種類に応じて中身検索
            if (FileTypeDic.ContainsKey(ext))
            {
                switch (FileTypeDic[ext])
                {
                    case EFileType.ALL:
                        switch (ext)
                        {
                            case "txt":
                            case "csv":
                                return contentMatchResult(SearchText(filePath, keyword), isExclude);
                            case "xls":
                            case "xlsx":
                                return contentMatchResult(SearchExcel(filePath, keyword), isExclude);
                            case "doc":
                            case "docx":
                            case "ppt":
                            case "pptx":
                                return contentMatchResult(SearchWordPpt(filePath, keyword), isExclude);
                            case "pdf":
                                return contentMatchResult(SearchPdf(filePath, keyword), isExclude);
                            case "html":
                            case "htm":
                                return contentMatchResult(SearchHtml(filePath, keyword), isExclude);
                            case "rtf":
                                return contentMatchResult(SearchRtf(filePath, keyword), isExclude);
                        }
                        break;

                    case EFileType.EXIF:
                        return contentMatchResult(SearchExif(filePath, keyword) ? 0 : -1, isExclude);
                }
            }

            return -1;}

        int contentMatchResult(int result, bool isExclude)
        {
            if (isExclude)
            {
                return result != -1 ? -1 : -2;  // 除外ならマッチしたらNG、しなければ仮ヒット
            }
            return result;
        }
        int SearchHtml(string filePath, string keyword)
        {
            try
            {
                string content = File.ReadAllText(filePath);
                string textOnly = Regex.Replace(content, "<.*?>", string.Empty);  // HTMLタグ除去
                return textOnly.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0 ? 0 : -1;
            }
            catch
            {
                return -1;
            }
        }

        int SearchRtf(string filePath, string keyword)
        {
            try
            {
                using (var box = new System.Windows.Forms.RichTextBox())
                {
                    box.LoadFile(filePath);
                    return box.Text.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0 ? 0 : -1;
                }
            }
            catch
            {
                return -1;
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
