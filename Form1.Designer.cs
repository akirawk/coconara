namespace RedundantFileSearch
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtOutputPath = new System.Windows.Forms.TextBox();
            this.btnOutputPath = new System.Windows.Forms.Button();
            this.GrpOutput = new System.Windows.Forms.GroupBox();
            this.rbnUpdate = new System.Windows.Forms.RadioButton();
            this.rbnFileSize = new System.Windows.Forms.RadioButton();
            this.rbnName = new System.Windows.Forms.RadioButton();
            this.grpKeyword = new System.Windows.Forms.GroupBox();
            this.grpSearchExt = new System.Windows.Forms.GroupBox();
            this.panelExFrame1 = new RedundantFileSearch.PanelExFrame();
            this.chxDoc = new System.Windows.Forms.CheckBox();
            this.chxDb = new System.Windows.Forms.CheckBox();
            this.chxXls = new System.Windows.Forms.CheckBox();
            this.chxDocx = new System.Windows.Forms.CheckBox();
            this.chxTiff = new System.Windows.Forms.CheckBox();
            this.chxPptx = new System.Windows.Forms.CheckBox();
            this.chxTif = new System.Windows.Forms.CheckBox();
            this.chxMkv = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.chxAll = new System.Windows.Forms.CheckBox();
            this.chxWmf = new System.Windows.Forms.CheckBox();
            this.chxHtm = new System.Windows.Forms.CheckBox();
            this.chxBmp = new System.Windows.Forms.CheckBox();
            this.chxJpg = new System.Windows.Forms.CheckBox();
            this.chxTxt = new System.Windows.Forms.CheckBox();
            this.chxIso = new System.Windows.Forms.CheckBox();
            this.chxPdf = new System.Windows.Forms.CheckBox();
            this.chxMp3 = new System.Windows.Forms.CheckBox();
            this.chxImg = new System.Windows.Forms.CheckBox();
            this.chxRtf = new System.Windows.Forms.CheckBox();
            this.chxHtml = new System.Windows.Forms.CheckBox();
            this.chxZip = new System.Windows.Forms.CheckBox();
            this.chxXlsx = new System.Windows.Forms.CheckBox();
            this.chxPpt = new System.Windows.Forms.CheckBox();
            this.chxJpeg = new System.Windows.Forms.CheckBox();
            this.chxMov = new System.Windows.Forms.CheckBox();
            this.chxAvi = new System.Windows.Forms.CheckBox();
            this.chxWmv = new System.Windows.Forms.CheckBox();
            this.chxMp4 = new System.Windows.Forms.CheckBox();
            this.chxGif = new System.Windows.Forms.CheckBox();
            this.chxPng = new System.Windows.Forms.CheckBox();
            this.chxLzh = new System.Windows.Forms.CheckBox();
            this.txtExt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblTilda = new System.Windows.Forms.Label();
            this.dtpMax = new System.Windows.Forms.DateTimePicker();
            this.dtpMin = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.chxNameCsv = new System.Windows.Forms.CheckBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnNameFile = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnAuto = new System.Windows.Forms.Button();
            this.cbxSaveTmp = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.lbxPath = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAddPath = new System.Windows.Forms.Button();
            this.btnAllClear = new System.Windows.Forms.Button();
            this.btnRemovePath = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.chxWriteAccess = new System.Windows.Forms.CheckBox();
            this.chxWriteUpdate = new System.Windows.Forms.CheckBox();
            this.chxWriteCreate = new System.Windows.Forms.CheckBox();
            this.GrpOutput.SuspendLayout();
            this.grpKeyword.SuspendLayout();
            this.grpSearchExt.SuspendLayout();
            this.panelExFrame1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("MS UI Gothic", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTitle.Location = new System.Drawing.Point(125, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(207, 20);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "重要ファイル検索システム";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(5, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(192, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "CSVファイルの出力先を指定してください";
            // 
            // txtOutputPath
            // 
            this.txtOutputPath.Location = new System.Drawing.Point(6, 31);
            this.txtOutputPath.Name = "txtOutputPath";
            this.txtOutputPath.Size = new System.Drawing.Size(191, 19);
            this.txtOutputPath.TabIndex = 1;
            // 
            // btnOutputPath
            // 
            this.btnOutputPath.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnOutputPath.Location = new System.Drawing.Point(208, 30);
            this.btnOutputPath.Margin = new System.Windows.Forms.Padding(2);
            this.btnOutputPath.Name = "btnOutputPath";
            this.btnOutputPath.Size = new System.Drawing.Size(77, 20);
            this.btnOutputPath.TabIndex = 2;
            this.btnOutputPath.Text = "フォルダ選択";
            this.btnOutputPath.UseVisualStyleBackColor = true;
            this.btnOutputPath.Click += new System.EventHandler(this.btnOutputPath_Click);
            // 
            // GrpOutput
            // 
            this.GrpOutput.Controls.Add(this.rbnUpdate);
            this.GrpOutput.Controls.Add(this.rbnFileSize);
            this.GrpOutput.Controls.Add(this.rbnName);
            this.GrpOutput.Controls.Add(this.btnOutputPath);
            this.GrpOutput.Controls.Add(this.txtOutputPath);
            this.GrpOutput.Controls.Add(this.label1);
            this.GrpOutput.Location = new System.Drawing.Point(12, 255);
            this.GrpOutput.Name = "GrpOutput";
            this.GrpOutput.Size = new System.Drawing.Size(296, 78);
            this.GrpOutput.TabIndex = 2;
            this.GrpOutput.TabStop = false;
            this.GrpOutput.Text = "出力先";
            // 
            // rbnUpdate
            // 
            this.rbnUpdate.AutoSize = true;
            this.rbnUpdate.Location = new System.Drawing.Point(197, 57);
            this.rbnUpdate.Name = "rbnUpdate";
            this.rbnUpdate.Size = new System.Drawing.Size(83, 16);
            this.rbnUpdate.TabIndex = 5;
            this.rbnUpdate.TabStop = true;
            this.rbnUpdate.Text = "更新日時順";
            this.rbnUpdate.UseVisualStyleBackColor = true;
            // 
            // rbnFileSize
            // 
            this.rbnFileSize.AutoSize = true;
            this.rbnFileSize.Location = new System.Drawing.Point(93, 57);
            this.rbnFileSize.Name = "rbnFileSize";
            this.rbnFileSize.Size = new System.Drawing.Size(98, 16);
            this.rbnFileSize.TabIndex = 4;
            this.rbnFileSize.TabStop = true;
            this.rbnFileSize.Text = "ファイルサイズ順";
            this.rbnFileSize.UseVisualStyleBackColor = true;
            // 
            // rbnName
            // 
            this.rbnName.AutoSize = true;
            this.rbnName.Checked = true;
            this.rbnName.Location = new System.Drawing.Point(6, 57);
            this.rbnName.Name = "rbnName";
            this.rbnName.Size = new System.Drawing.Size(81, 16);
            this.rbnName.TabIndex = 3;
            this.rbnName.TabStop = true;
            this.rbnName.Text = "ファイル名順";
            this.rbnName.UseVisualStyleBackColor = true;
            // 
            // grpKeyword
            // 
            this.grpKeyword.Controls.Add(this.grpSearchExt);
            this.grpKeyword.Controls.Add(this.lblTilda);
            this.grpKeyword.Controls.Add(this.dtpMax);
            this.grpKeyword.Controls.Add(this.dtpMin);
            this.grpKeyword.Controls.Add(this.label3);
            this.grpKeyword.Location = new System.Drawing.Point(12, 339);
            this.grpKeyword.Name = "grpKeyword";
            this.grpKeyword.Size = new System.Drawing.Size(441, 244);
            this.grpKeyword.TabIndex = 3;
            this.grpKeyword.TabStop = false;
            this.grpKeyword.Text = "検索条件";
            // 
            // grpSearchExt
            // 
            this.grpSearchExt.Controls.Add(this.panelExFrame1);
            this.grpSearchExt.Controls.Add(this.txtExt);
            this.grpSearchExt.Controls.Add(this.label4);
            this.grpSearchExt.Location = new System.Drawing.Point(6, 55);
            this.grpSearchExt.Name = "grpSearchExt";
            this.grpSearchExt.Size = new System.Drawing.Size(425, 185);
            this.grpSearchExt.TabIndex = 4;
            this.grpSearchExt.TabStop = false;
            this.grpSearchExt.Text = "検索拡張子";
            // 
            // panelExFrame1
            // 
            this.panelExFrame1.BorderColor = System.Drawing.Color.Black;
            this.panelExFrame1.Controls.Add(this.chxDoc);
            this.panelExFrame1.Controls.Add(this.chxDb);
            this.panelExFrame1.Controls.Add(this.chxXls);
            this.panelExFrame1.Controls.Add(this.chxDocx);
            this.panelExFrame1.Controls.Add(this.chxTiff);
            this.panelExFrame1.Controls.Add(this.chxPptx);
            this.panelExFrame1.Controls.Add(this.chxTif);
            this.panelExFrame1.Controls.Add(this.chxMkv);
            this.panelExFrame1.Controls.Add(this.checkBox4);
            this.panelExFrame1.Controls.Add(this.chxAll);
            this.panelExFrame1.Controls.Add(this.chxWmf);
            this.panelExFrame1.Controls.Add(this.chxHtm);
            this.panelExFrame1.Controls.Add(this.chxBmp);
            this.panelExFrame1.Controls.Add(this.chxJpg);
            this.panelExFrame1.Controls.Add(this.chxTxt);
            this.panelExFrame1.Controls.Add(this.chxIso);
            this.panelExFrame1.Controls.Add(this.chxPdf);
            this.panelExFrame1.Controls.Add(this.chxMp3);
            this.panelExFrame1.Controls.Add(this.chxImg);
            this.panelExFrame1.Controls.Add(this.chxRtf);
            this.panelExFrame1.Controls.Add(this.chxHtml);
            this.panelExFrame1.Controls.Add(this.chxZip);
            this.panelExFrame1.Controls.Add(this.chxXlsx);
            this.panelExFrame1.Controls.Add(this.chxPpt);
            this.panelExFrame1.Controls.Add(this.chxJpeg);
            this.panelExFrame1.Controls.Add(this.chxMov);
            this.panelExFrame1.Controls.Add(this.chxAvi);
            this.panelExFrame1.Controls.Add(this.chxWmv);
            this.panelExFrame1.Controls.Add(this.chxMp4);
            this.panelExFrame1.Controls.Add(this.chxGif);
            this.panelExFrame1.Controls.Add(this.chxPng);
            this.panelExFrame1.Controls.Add(this.chxLzh);
            this.panelExFrame1.Location = new System.Drawing.Point(6, 18);
            this.panelExFrame1.Name = "panelExFrame1";
            this.panelExFrame1.Size = new System.Drawing.Size(413, 116);
            this.panelExFrame1.TabIndex = 32;
            // 
            // chxDoc
            // 
            this.chxDoc.AutoSize = true;
            this.chxDoc.Location = new System.Drawing.Point(157, 27);
            this.chxDoc.Name = "chxDoc";
            this.chxDoc.Size = new System.Drawing.Size(44, 16);
            this.chxDoc.TabIndex = 5;
            this.chxDoc.Text = ".doc";
            this.chxDoc.UseVisualStyleBackColor = true;
            // 
            // chxDb
            // 
            this.chxDb.AutoSize = true;
            this.chxDb.Location = new System.Drawing.Point(203, 93);
            this.chxDb.Name = "chxDb";
            this.chxDb.Size = new System.Drawing.Size(38, 16);
            this.chxDb.TabIndex = 21;
            this.chxDb.Text = ".db";
            this.chxDb.UseVisualStyleBackColor = true;
            // 
            // chxXls
            // 
            this.chxXls.AutoSize = true;
            this.chxXls.Location = new System.Drawing.Point(61, 27);
            this.chxXls.Name = "chxXls";
            this.chxXls.Size = new System.Drawing.Size(41, 16);
            this.chxXls.TabIndex = 2;
            this.chxXls.Text = ".xls";
            this.chxXls.UseVisualStyleBackColor = true;
            // 
            // chxDocx
            // 
            this.chxDocx.AutoSize = true;
            this.chxDocx.Location = new System.Drawing.Point(109, 27);
            this.chxDocx.Name = "chxDocx";
            this.chxDocx.Size = new System.Drawing.Size(50, 16);
            this.chxDocx.TabIndex = 6;
            this.chxDocx.Text = ".docx";
            this.chxDocx.UseVisualStyleBackColor = true;
            // 
            // chxTiff
            // 
            this.chxTiff.AutoSize = true;
            this.chxTiff.Location = new System.Drawing.Point(61, 71);
            this.chxTiff.Name = "chxTiff";
            this.chxTiff.Size = new System.Drawing.Size(41, 16);
            this.chxTiff.TabIndex = 11;
            this.chxTiff.Text = ".tiff";
            this.chxTiff.UseVisualStyleBackColor = true;
            // 
            // chxPptx
            // 
            this.chxPptx.AutoSize = true;
            this.chxPptx.Location = new System.Drawing.Point(203, 27);
            this.chxPptx.Name = "chxPptx";
            this.chxPptx.Size = new System.Drawing.Size(48, 16);
            this.chxPptx.TabIndex = 25;
            this.chxPptx.Text = ".pptx";
            this.chxPptx.UseVisualStyleBackColor = true;
            // 
            // chxTif
            // 
            this.chxTif.AutoSize = true;
            this.chxTif.Location = new System.Drawing.Point(13, 71);
            this.chxTif.Name = "chxTif";
            this.chxTif.Size = new System.Drawing.Size(37, 16);
            this.chxTif.TabIndex = 11;
            this.chxTif.Text = ".tif";
            this.chxTif.UseVisualStyleBackColor = true;
            // 
            // chxMkv
            // 
            this.chxMkv.AutoSize = true;
            this.chxMkv.Location = new System.Drawing.Point(157, 93);
            this.chxMkv.Name = "chxMkv";
            this.chxMkv.Size = new System.Drawing.Size(47, 16);
            this.chxMkv.TabIndex = 20;
            this.chxMkv.Text = ".mkv";
            this.chxMkv.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Font = new System.Drawing.Font("MS UI Gothic", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkBox4.Location = new System.Drawing.Point(157, 5);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(162, 14);
            this.checkBox4.TabIndex = 0;
            this.checkBox4.Text = "拡張子問わず全てのファイルを選択";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // chxAll
            // 
            this.chxAll.AutoSize = true;
            this.chxAll.Font = new System.Drawing.Font("MS UI Gothic", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chxAll.Location = new System.Drawing.Point(13, 5);
            this.chxAll.Name = "chxAll";
            this.chxAll.Size = new System.Drawing.Size(142, 14);
            this.chxAll.TabIndex = 0;
            this.chxAll.Text = "下記拡張子30項目全て選択";
            this.chxAll.UseVisualStyleBackColor = true;
            // 
            // chxWmf
            // 
            this.chxWmf.AutoSize = true;
            this.chxWmf.Location = new System.Drawing.Point(253, 71);
            this.chxWmf.Name = "chxWmf";
            this.chxWmf.Size = new System.Drawing.Size(47, 16);
            this.chxWmf.TabIndex = 15;
            this.chxWmf.Text = ".wmf";
            this.chxWmf.UseVisualStyleBackColor = true;
            // 
            // chxHtm
            // 
            this.chxHtm.AutoSize = true;
            this.chxHtm.Location = new System.Drawing.Point(13, 49);
            this.chxHtm.Name = "chxHtm";
            this.chxHtm.Size = new System.Drawing.Size(45, 16);
            this.chxHtm.TabIndex = 26;
            this.chxHtm.Text = ".htm";
            this.chxHtm.UseVisualStyleBackColor = true;
            // 
            // chxBmp
            // 
            this.chxBmp.AutoSize = true;
            this.chxBmp.Location = new System.Drawing.Point(309, 49);
            this.chxBmp.Name = "chxBmp";
            this.chxBmp.Size = new System.Drawing.Size(47, 16);
            this.chxBmp.TabIndex = 10;
            this.chxBmp.Text = ".bmp";
            this.chxBmp.UseVisualStyleBackColor = true;
            // 
            // chxJpg
            // 
            this.chxJpg.AutoSize = true;
            this.chxJpg.Location = new System.Drawing.Point(157, 49);
            this.chxJpg.Name = "chxJpg";
            this.chxJpg.Size = new System.Drawing.Size(41, 16);
            this.chxJpg.TabIndex = 7;
            this.chxJpg.Text = ".jpg";
            this.chxJpg.UseVisualStyleBackColor = true;
            // 
            // chxTxt
            // 
            this.chxTxt.AutoSize = true;
            this.chxTxt.Location = new System.Drawing.Point(356, 27);
            this.chxTxt.Name = "chxTxt";
            this.chxTxt.Size = new System.Drawing.Size(40, 16);
            this.chxTxt.TabIndex = 4;
            this.chxTxt.Text = ".txt";
            this.chxTxt.UseVisualStyleBackColor = true;
            // 
            // chxIso
            // 
            this.chxIso.AutoSize = true;
            this.chxIso.Location = new System.Drawing.Point(356, 93);
            this.chxIso.Name = "chxIso";
            this.chxIso.Size = new System.Drawing.Size(41, 16);
            this.chxIso.TabIndex = 12;
            this.chxIso.Text = ".iso";
            this.chxIso.UseVisualStyleBackColor = true;
            // 
            // chxPdf
            // 
            this.chxPdf.AutoSize = true;
            this.chxPdf.Location = new System.Drawing.Point(309, 27);
            this.chxPdf.Name = "chxPdf";
            this.chxPdf.Size = new System.Drawing.Size(42, 16);
            this.chxPdf.TabIndex = 1;
            this.chxPdf.Text = ".pdf";
            this.chxPdf.UseVisualStyleBackColor = true;
            // 
            // chxMp3
            // 
            this.chxMp3.AutoSize = true;
            this.chxMp3.Location = new System.Drawing.Point(13, 93);
            this.chxMp3.Name = "chxMp3";
            this.chxMp3.Size = new System.Drawing.Size(47, 16);
            this.chxMp3.TabIndex = 17;
            this.chxMp3.Text = ".mp3";
            this.chxMp3.UseVisualStyleBackColor = true;
            // 
            // chxImg
            // 
            this.chxImg.AutoSize = true;
            this.chxImg.Location = new System.Drawing.Point(109, 71);
            this.chxImg.Name = "chxImg";
            this.chxImg.Size = new System.Drawing.Size(44, 16);
            this.chxImg.TabIndex = 22;
            this.chxImg.Text = ".img";
            this.chxImg.UseVisualStyleBackColor = true;
            // 
            // chxRtf
            // 
            this.chxRtf.AutoSize = true;
            this.chxRtf.Location = new System.Drawing.Point(109, 49);
            this.chxRtf.Name = "chxRtf";
            this.chxRtf.Size = new System.Drawing.Size(38, 16);
            this.chxRtf.TabIndex = 29;
            this.chxRtf.Text = ".rtf";
            this.chxRtf.UseVisualStyleBackColor = true;
            // 
            // chxHtml
            // 
            this.chxHtml.AutoSize = true;
            this.chxHtml.Location = new System.Drawing.Point(61, 49);
            this.chxHtml.Name = "chxHtml";
            this.chxHtml.Size = new System.Drawing.Size(48, 16);
            this.chxHtml.TabIndex = 27;
            this.chxHtml.Text = ".html";
            this.chxHtml.UseVisualStyleBackColor = true;
            // 
            // chxZip
            // 
            this.chxZip.AutoSize = true;
            this.chxZip.Location = new System.Drawing.Point(311, 93);
            this.chxZip.Name = "chxZip";
            this.chxZip.Size = new System.Drawing.Size(40, 16);
            this.chxZip.TabIndex = 29;
            this.chxZip.Text = ".zip";
            this.chxZip.UseVisualStyleBackColor = true;
            // 
            // chxXlsx
            // 
            this.chxXlsx.AutoSize = true;
            this.chxXlsx.Location = new System.Drawing.Point(13, 27);
            this.chxXlsx.Name = "chxXlsx";
            this.chxXlsx.Size = new System.Drawing.Size(47, 16);
            this.chxXlsx.TabIndex = 3;
            this.chxXlsx.Text = ".xlsx";
            this.chxXlsx.UseVisualStyleBackColor = true;
            // 
            // chxPpt
            // 
            this.chxPpt.AutoSize = true;
            this.chxPpt.Location = new System.Drawing.Point(253, 27);
            this.chxPpt.Name = "chxPpt";
            this.chxPpt.Size = new System.Drawing.Size(42, 16);
            this.chxPpt.TabIndex = 24;
            this.chxPpt.Text = ".ppt";
            this.chxPpt.UseVisualStyleBackColor = true;
            // 
            // chxJpeg
            // 
            this.chxJpeg.AutoSize = true;
            this.chxJpeg.Location = new System.Drawing.Point(203, 49);
            this.chxJpeg.Name = "chxJpeg";
            this.chxJpeg.Size = new System.Drawing.Size(47, 16);
            this.chxJpeg.TabIndex = 8;
            this.chxJpeg.Text = ".jpeg";
            this.chxJpeg.UseVisualStyleBackColor = true;
            // 
            // chxMov
            // 
            this.chxMov.AutoSize = true;
            this.chxMov.Location = new System.Drawing.Point(109, 93);
            this.chxMov.Name = "chxMov";
            this.chxMov.Size = new System.Drawing.Size(47, 16);
            this.chxMov.TabIndex = 19;
            this.chxMov.Text = ".mov";
            this.chxMov.UseVisualStyleBackColor = true;
            // 
            // chxAvi
            // 
            this.chxAvi.AutoSize = true;
            this.chxAvi.Location = new System.Drawing.Point(157, 71);
            this.chxAvi.Name = "chxAvi";
            this.chxAvi.Size = new System.Drawing.Size(41, 16);
            this.chxAvi.TabIndex = 13;
            this.chxAvi.Text = ".avi";
            this.chxAvi.UseVisualStyleBackColor = true;
            // 
            // chxWmv
            // 
            this.chxWmv.AutoSize = true;
            this.chxWmv.Location = new System.Drawing.Point(203, 71);
            this.chxWmv.Name = "chxWmv";
            this.chxWmv.Size = new System.Drawing.Size(49, 16);
            this.chxWmv.TabIndex = 14;
            this.chxWmv.Text = ".wmv";
            this.chxWmv.UseVisualStyleBackColor = true;
            // 
            // chxMp4
            // 
            this.chxMp4.AutoSize = true;
            this.chxMp4.Location = new System.Drawing.Point(61, 93);
            this.chxMp4.Name = "chxMp4";
            this.chxMp4.Size = new System.Drawing.Size(47, 16);
            this.chxMp4.TabIndex = 18;
            this.chxMp4.Text = ".mp4";
            this.chxMp4.UseVisualStyleBackColor = true;
            // 
            // chxGif
            // 
            this.chxGif.AutoSize = true;
            this.chxGif.Location = new System.Drawing.Point(253, 49);
            this.chxGif.Name = "chxGif";
            this.chxGif.Size = new System.Drawing.Size(39, 16);
            this.chxGif.TabIndex = 9;
            this.chxGif.Text = ".gif";
            this.chxGif.UseVisualStyleBackColor = true;
            // 
            // chxPng
            // 
            this.chxPng.AutoSize = true;
            this.chxPng.Location = new System.Drawing.Point(356, 49);
            this.chxPng.Name = "chxPng";
            this.chxPng.Size = new System.Drawing.Size(44, 16);
            this.chxPng.TabIndex = 23;
            this.chxPng.Text = ".png";
            this.chxPng.UseVisualStyleBackColor = true;
            // 
            // chxLzh
            // 
            this.chxLzh.AutoSize = true;
            this.chxLzh.Location = new System.Drawing.Point(253, 93);
            this.chxLzh.Name = "chxLzh";
            this.chxLzh.Size = new System.Drawing.Size(40, 16);
            this.chxLzh.TabIndex = 28;
            this.chxLzh.Text = ".lzh";
            this.chxLzh.UseVisualStyleBackColor = true;
            // 
            // txtExt
            // 
            this.txtExt.Location = new System.Drawing.Point(6, 159);
            this.txtExt.Name = "txtExt";
            this.txtExt.Size = new System.Drawing.Size(413, 19);
            this.txtExt.TabIndex = 31;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 143);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(338, 12);
            this.label4.TabIndex = 30;
            this.label4.Text = "その他（複数の場合は半角カンマで分ける 例)ファイル名A,ファイル名B）";
            // 
            // lblTilda
            // 
            this.lblTilda.AutoSize = true;
            this.lblTilda.Location = new System.Drawing.Point(137, 35);
            this.lblTilda.Name = "lblTilda";
            this.lblTilda.Size = new System.Drawing.Size(17, 12);
            this.lblTilda.TabIndex = 2;
            this.lblTilda.Text = "～";
            // 
            // dtpMax
            // 
            this.dtpMax.Location = new System.Drawing.Point(160, 30);
            this.dtpMax.Name = "dtpMax";
            this.dtpMax.Size = new System.Drawing.Size(125, 19);
            this.dtpMax.TabIndex = 3;
            // 
            // dtpMin
            // 
            this.dtpMin.Location = new System.Drawing.Point(6, 30);
            this.dtpMin.Name = "dtpMin";
            this.dtpMin.Size = new System.Drawing.Size(125, 19);
            this.dtpMin.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(253, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "検索対象ファイルの最終更新日時を指定してください";
            // 
            // chxNameCsv
            // 
            this.chxNameCsv.AutoSize = true;
            this.chxNameCsv.Location = new System.Drawing.Point(12, 82);
            this.chxNameCsv.Name = "chxNameCsv";
            this.chxNameCsv.Size = new System.Drawing.Size(261, 28);
            this.chxNameCsv.TabIndex = 2;
            this.chxNameCsv.Text = "キーワードをCSVから読み込む\r\n（複数キーワードの場合はこちらを使用してください）";
            this.chxNameCsv.UseVisualStyleBackColor = true;
            this.chxNameCsv.CheckedChanged += new System.EventHandler(this.chxNameCsv_CheckedChanged);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(12, 57);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(425, 19);
            this.txtName.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(352, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "ファイル名・ファイル内容のキーワード(一部/全部)を下記に入力してください";
            // 
            // btnNameFile
            // 
            this.btnNameFile.Enabled = false;
            this.btnNameFile.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnNameFile.Location = new System.Drawing.Point(314, 85);
            this.btnNameFile.Margin = new System.Windows.Forms.Padding(2);
            this.btnNameFile.Name = "btnNameFile";
            this.btnNameFile.Size = new System.Drawing.Size(123, 20);
            this.btnNameFile.TabIndex = 3;
            this.btnNameFile.Text = "ファイル選択";
            this.btnNameFile.UseVisualStyleBackColor = true;
            this.btnNameFile.Click += new System.EventHandler(this.btnNameFile_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("MS UI Gothic", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnSearch.ForeColor = System.Drawing.Color.Red;
            this.btnSearch.Location = new System.Drawing.Point(150, 588);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(165, 45);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "検索開始";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnClear.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnClear.Location = new System.Drawing.Point(365, 588);
            this.btnClear.Margin = new System.Windows.Forms.Padding(2);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(88, 45);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "入力項目\r\n全てクリア";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnAuto
            // 
            this.btnAuto.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnAuto.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnAuto.Location = new System.Drawing.Point(12, 588);
            this.btnAuto.Margin = new System.Windows.Forms.Padding(2);
            this.btnAuto.Name = "btnAuto";
            this.btnAuto.Size = new System.Drawing.Size(88, 45);
            this.btnAuto.TabIndex = 5;
            this.btnAuto.Text = "予約検索";
            this.btnAuto.UseVisualStyleBackColor = true;
            this.btnAuto.Click += new System.EventHandler(this.btnAuto_Click);
            // 
            // cbxSaveTmp
            // 
            this.cbxSaveTmp.AutoSize = true;
            this.cbxSaveTmp.Location = new System.Drawing.Point(150, 636);
            this.cbxSaveTmp.Name = "cbxSaveTmp";
            this.cbxSaveTmp.Size = new System.Drawing.Size(135, 16);
            this.cbxSaveTmp.TabIndex = 4;
            this.cbxSaveTmp.Text = "中断時にも結果を保存";
            this.cbxSaveTmp.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Orange;
            this.panel1.Controls.Add(this.label6);
            this.panel1.Location = new System.Drawing.Point(12, 116);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(425, 29);
            this.panel1.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.Location = new System.Drawing.Point(2, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(420, 22);
            this.label6.TabIndex = 0;
            this.label6.Text = "キーワードをCSVから読み込んだ場合のみ、下記の機能が使えます\r\n完全一致したキーワードのみを検出したい場合は、ダブルクォーテーション「“”」で囲む  例) \"A" +
    "BC\"\r\n";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbxPath
            // 
            this.lbxPath.FormattingEnabled = true;
            this.lbxPath.ItemHeight = 12;
            this.lbxPath.Location = new System.Drawing.Point(12, 194);
            this.lbxPath.Name = "lbxPath";
            this.lbxPath.Size = new System.Drawing.Size(425, 52);
            this.lbxPath.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(10, 157);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(212, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "検索先のフォルダを選択して追加してください";
            // 
            // btnAddPath
            // 
            this.btnAddPath.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnAddPath.Location = new System.Drawing.Point(12, 171);
            this.btnAddPath.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddPath.Name = "btnAddPath";
            this.btnAddPath.Size = new System.Drawing.Size(139, 20);
            this.btnAddPath.TabIndex = 7;
            this.btnAddPath.Text = "フォルダをリストに追加";
            this.btnAddPath.UseVisualStyleBackColor = true;
            this.btnAddPath.Click += new System.EventHandler(this.btnAddPath_Click);
            // 
            // btnAllClear
            // 
            this.btnAllClear.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnAllClear.Location = new System.Drawing.Point(314, 171);
            this.btnAllClear.Margin = new System.Windows.Forms.Padding(2);
            this.btnAllClear.Name = "btnAllClear";
            this.btnAllClear.Size = new System.Drawing.Size(123, 20);
            this.btnAllClear.TabIndex = 9;
            this.btnAllClear.Text = "クリア";
            this.btnAllClear.UseVisualStyleBackColor = true;
            this.btnAllClear.Click += new System.EventHandler(this.btnAllClear_Click);
            // 
            // btnRemovePath
            // 
            this.btnRemovePath.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnRemovePath.Location = new System.Drawing.Point(155, 171);
            this.btnRemovePath.Margin = new System.Windows.Forms.Padding(2);
            this.btnRemovePath.Name = "btnRemovePath";
            this.btnRemovePath.Size = new System.Drawing.Size(155, 20);
            this.btnRemovePath.TabIndex = 8;
            this.btnRemovePath.Text = "選択フォルダをリストから削除";
            this.btnRemovePath.UseVisualStyleBackColor = true;
            this.btnRemovePath.Click += new System.EventHandler(this.btnRemovePath_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.chxWriteAccess);
            this.groupBox1.Controls.Add(this.chxWriteUpdate);
            this.groupBox1.Controls.Add(this.chxWriteCreate);
            this.groupBox1.Location = new System.Drawing.Point(314, 255);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(138, 133);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "出力項目の追加";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label7.Location = new System.Drawing.Point(17, 84);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(102, 36);
            this.label7.TabIndex = 1;
            this.label7.Text = "※ファイルによって\r\n取得できない場合が\r\nあります。";
            // 
            // chxWriteAccess
            // 
            this.chxWriteAccess.AutoSize = true;
            this.chxWriteAccess.Location = new System.Drawing.Point(26, 61);
            this.chxWriteAccess.Name = "chxWriteAccess";
            this.chxWriteAccess.Size = new System.Drawing.Size(84, 16);
            this.chxWriteAccess.TabIndex = 0;
            this.chxWriteAccess.Text = "アクセス日時";
            this.chxWriteAccess.UseVisualStyleBackColor = true;
            // 
            // chxWriteUpdate
            // 
            this.chxWriteUpdate.AutoSize = true;
            this.chxWriteUpdate.Location = new System.Drawing.Point(26, 39);
            this.chxWriteUpdate.Name = "chxWriteUpdate";
            this.chxWriteUpdate.Size = new System.Drawing.Size(72, 16);
            this.chxWriteUpdate.TabIndex = 0;
            this.chxWriteUpdate.Text = "更新日時";
            this.chxWriteUpdate.UseVisualStyleBackColor = true;
            // 
            // chxWriteCreate
            // 
            this.chxWriteCreate.AutoSize = true;
            this.chxWriteCreate.Location = new System.Drawing.Point(26, 17);
            this.chxWriteCreate.Name = "chxWriteCreate";
            this.chxWriteCreate.Size = new System.Drawing.Size(72, 16);
            this.chxWriteCreate.TabIndex = 0;
            this.chxWriteCreate.Text = "作成日時";
            this.chxWriteCreate.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 651);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lbxPath);
            this.Controls.Add(this.chxNameCsv);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnAddPath);
            this.Controls.Add(this.btnNameFile);
            this.Controls.Add(this.btnAllClear);
            this.Controls.Add(this.btnRemovePath);
            this.Controls.Add(this.cbxSaveTmp);
            this.Controls.Add(this.btnAuto);
            this.Controls.Add(this.grpKeyword);
            this.Controls.Add(this.GrpOutput);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.lblTitle);
            this.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "有効期限 YYYY/MM/DD-YYYY/MM/DD";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.GrpOutput.ResumeLayout(false);
            this.GrpOutput.PerformLayout();
            this.grpKeyword.ResumeLayout(false);
            this.grpKeyword.PerformLayout();
            this.grpSearchExt.ResumeLayout(false);
            this.grpSearchExt.PerformLayout();
            this.panelExFrame1.ResumeLayout(false);
            this.panelExFrame1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtOutputPath;
        private System.Windows.Forms.Button btnOutputPath;
        private System.Windows.Forms.GroupBox GrpOutput;
        private System.Windows.Forms.RadioButton rbnUpdate;
        private System.Windows.Forms.RadioButton rbnFileSize;
        private System.Windows.Forms.RadioButton rbnName;
        private System.Windows.Forms.GroupBox grpKeyword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpMin;
        private System.Windows.Forms.Label lblTilda;
        private System.Windows.Forms.DateTimePicker dtpMax;
        private System.Windows.Forms.GroupBox grpSearchExt;
        private System.Windows.Forms.CheckBox chxPdf;
        private System.Windows.Forms.CheckBox chxDoc;
        private System.Windows.Forms.CheckBox chxTxt;
        private System.Windows.Forms.CheckBox chxXlsx;
        private System.Windows.Forms.CheckBox chxXls;
        private System.Windows.Forms.CheckBox chxAll;
        private System.Windows.Forms.CheckBox chxBmp;
        private System.Windows.Forms.CheckBox chxGif;
        private System.Windows.Forms.CheckBox chxJpeg;
        private System.Windows.Forms.CheckBox chxJpg;
        private System.Windows.Forms.CheckBox chxDocx;
        private System.Windows.Forms.CheckBox chxMkv;
        private System.Windows.Forms.CheckBox chxWmf;
        private System.Windows.Forms.CheckBox chxMov;
        private System.Windows.Forms.CheckBox chxWmv;
        private System.Windows.Forms.CheckBox chxMp4;
        private System.Windows.Forms.CheckBox chxAvi;
        private System.Windows.Forms.CheckBox chxMp3;
        private System.Windows.Forms.CheckBox chxIso;
        private System.Windows.Forms.CheckBox chxTiff;
        private System.Windows.Forms.CheckBox chxPptx;
        private System.Windows.Forms.CheckBox chxZip;
        private System.Windows.Forms.CheckBox chxPpt;
        private System.Windows.Forms.CheckBox chxLzh;
        private System.Windows.Forms.CheckBox chxPng;
        private System.Windows.Forms.CheckBox chxHtml;
        private System.Windows.Forms.CheckBox chxImg;
        private System.Windows.Forms.CheckBox chxHtm;
        private System.Windows.Forms.CheckBox chxDb;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtExt;
        private System.Windows.Forms.CheckBox chxNameCsv;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnNameFile;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.CheckBox chxRtf;
        private System.Windows.Forms.CheckBox chxTif;
        private System.Windows.Forms.Button btnAuto;
        private System.Windows.Forms.CheckBox cbxSaveTmp;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListBox lbxPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAddPath;
        private System.Windows.Forms.Button btnAllClear;
        private System.Windows.Forms.Button btnRemovePath;
        private System.Windows.Forms.Label label6;
        private PanelExFrame panelExFrame1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chxWriteAccess;
        private System.Windows.Forms.CheckBox chxWriteUpdate;
        private System.Windows.Forms.CheckBox chxWriteCreate;
        private System.Windows.Forms.CheckBox checkBox4;
    }
}

