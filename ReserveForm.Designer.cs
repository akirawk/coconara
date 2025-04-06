namespace RedundantFileSearch
{
    partial class ReserveForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.rbnMonth = new System.Windows.Forms.RadioButton();
            this.rbnWeek = new System.Windows.Forms.RadioButton();
            this.rbnDay = new System.Windows.Forms.RadioButton();
            this.rbnNone = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxMon = new System.Windows.Forms.CheckBox();
            this.cbxTues = new System.Windows.Forms.CheckBox();
            this.cbxWend = new System.Windows.Forms.CheckBox();
            this.cbxThurs = new System.Windows.Forms.CheckBox();
            this.cbxFri = new System.Windows.Forms.CheckBox();
            this.cbxSata = new System.Windows.Forms.CheckBox();
            this.cbxSun = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nudMonth = new System.Windows.Forms.NumericUpDown();
            this.nudHour = new System.Windows.Forms.NumericUpDown();
            this.nudMin = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnTestMail = new System.Windows.Forms.Button();
            this.tbxPass = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbxId = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbxSmtp = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbxMail = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkMailCsv = new System.Windows.Forms.CheckBox();
            this.chkMailInfo = new System.Windows.Forms.CheckBox();
            this.chkOutCsv = new System.Windows.Forms.CheckBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.nudMonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHour)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMin)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbnMonth
            // 
            this.rbnMonth.AutoSize = true;
            this.rbnMonth.Location = new System.Drawing.Point(12, 12);
            this.rbnMonth.Name = "rbnMonth";
            this.rbnMonth.Size = new System.Drawing.Size(47, 16);
            this.rbnMonth.TabIndex = 0;
            this.rbnMonth.Text = "毎月";
            this.rbnMonth.UseVisualStyleBackColor = true;
            this.rbnMonth.CheckedChanged += new System.EventHandler(this.rbnMonth_CheckedChanged);
            // 
            // rbnWeek
            // 
            this.rbnWeek.AutoSize = true;
            this.rbnWeek.Location = new System.Drawing.Point(12, 53);
            this.rbnWeek.Name = "rbnWeek";
            this.rbnWeek.Size = new System.Drawing.Size(47, 16);
            this.rbnWeek.TabIndex = 1;
            this.rbnWeek.Text = "毎週";
            this.rbnWeek.UseVisualStyleBackColor = true;
            this.rbnWeek.CheckedChanged += new System.EventHandler(this.rbnWeek_CheckedChanged);
            // 
            // rbnDay
            // 
            this.rbnDay.AutoSize = true;
            this.rbnDay.Location = new System.Drawing.Point(12, 212);
            this.rbnDay.Name = "rbnDay";
            this.rbnDay.Size = new System.Drawing.Size(47, 16);
            this.rbnDay.TabIndex = 2;
            this.rbnDay.Text = "毎日";
            this.rbnDay.UseVisualStyleBackColor = true;
            this.rbnDay.CheckedChanged += new System.EventHandler(this.rbnDay_CheckedChanged);
            // 
            // rbnNone
            // 
            this.rbnNone.AutoSize = true;
            this.rbnNone.Checked = true;
            this.rbnNone.Location = new System.Drawing.Point(12, 234);
            this.rbnNone.Name = "rbnNone";
            this.rbnNone.Size = new System.Drawing.Size(76, 16);
            this.rbnNone.TabIndex = 3;
            this.rbnNone.TabStop = true;
            this.rbnNone.Text = "予約しない";
            this.rbnNone.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(139, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "日";
            // 
            // cbxMon
            // 
            this.cbxMon.AutoSize = true;
            this.cbxMon.Location = new System.Drawing.Point(96, 54);
            this.cbxMon.Name = "cbxMon";
            this.cbxMon.Size = new System.Drawing.Size(36, 16);
            this.cbxMon.TabIndex = 6;
            this.cbxMon.Text = "月";
            this.cbxMon.UseVisualStyleBackColor = true;
            // 
            // cbxTues
            // 
            this.cbxTues.AutoSize = true;
            this.cbxTues.Location = new System.Drawing.Point(96, 76);
            this.cbxTues.Name = "cbxTues";
            this.cbxTues.Size = new System.Drawing.Size(36, 16);
            this.cbxTues.TabIndex = 6;
            this.cbxTues.Text = "火";
            this.cbxTues.UseVisualStyleBackColor = true;
            // 
            // cbxWend
            // 
            this.cbxWend.AutoSize = true;
            this.cbxWend.Location = new System.Drawing.Point(96, 98);
            this.cbxWend.Name = "cbxWend";
            this.cbxWend.Size = new System.Drawing.Size(36, 16);
            this.cbxWend.TabIndex = 6;
            this.cbxWend.Text = "水";
            this.cbxWend.UseVisualStyleBackColor = true;
            // 
            // cbxThurs
            // 
            this.cbxThurs.AutoSize = true;
            this.cbxThurs.Location = new System.Drawing.Point(96, 120);
            this.cbxThurs.Name = "cbxThurs";
            this.cbxThurs.Size = new System.Drawing.Size(36, 16);
            this.cbxThurs.TabIndex = 6;
            this.cbxThurs.Text = "木";
            this.cbxThurs.UseVisualStyleBackColor = true;
            // 
            // cbxFri
            // 
            this.cbxFri.AutoSize = true;
            this.cbxFri.Location = new System.Drawing.Point(96, 142);
            this.cbxFri.Name = "cbxFri";
            this.cbxFri.Size = new System.Drawing.Size(36, 16);
            this.cbxFri.TabIndex = 6;
            this.cbxFri.Text = "金";
            this.cbxFri.UseVisualStyleBackColor = true;
            // 
            // cbxSata
            // 
            this.cbxSata.AutoSize = true;
            this.cbxSata.Location = new System.Drawing.Point(96, 164);
            this.cbxSata.Name = "cbxSata";
            this.cbxSata.Size = new System.Drawing.Size(36, 16);
            this.cbxSata.TabIndex = 6;
            this.cbxSata.Text = "土";
            this.cbxSata.UseVisualStyleBackColor = true;
            // 
            // cbxSun
            // 
            this.cbxSun.AutoSize = true;
            this.cbxSun.Location = new System.Drawing.Point(96, 186);
            this.cbxSun.Name = "cbxSun";
            this.cbxSun.Size = new System.Drawing.Size(36, 16);
            this.cbxSun.TabIndex = 6;
            this.cbxSun.Text = "日";
            this.cbxSun.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(138, 216);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "時";
            // 
            // nudMonth
            // 
            this.nudMonth.Location = new System.Drawing.Point(96, 12);
            this.nudMonth.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.nudMonth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMonth.Name = "nudMonth";
            this.nudMonth.Size = new System.Drawing.Size(37, 19);
            this.nudMonth.TabIndex = 7;
            this.nudMonth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nudHour
            // 
            this.nudHour.Location = new System.Drawing.Point(95, 212);
            this.nudHour.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.nudHour.Name = "nudHour";
            this.nudHour.Size = new System.Drawing.Size(37, 19);
            this.nudHour.TabIndex = 7;
            // 
            // nudMin
            // 
            this.nudMin.Location = new System.Drawing.Point(161, 212);
            this.nudMin.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.nudMin.Name = "nudMin";
            this.nudMin.Size = new System.Drawing.Size(37, 19);
            this.nudMin.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(204, 216);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "分";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.chkMailCsv);
            this.groupBox1.Controls.Add(this.chkMailInfo);
            this.groupBox1.Controls.Add(this.chkOutCsv);
            this.groupBox1.Location = new System.Drawing.Point(12, 267);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(240, 293);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "【予約検索】結果の出力先の選択";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnTestMail);
            this.groupBox2.Controls.Add(this.tbxPass);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.tbxId);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.tbxSmtp);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.tbxMail);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(6, 123);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(227, 164);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "メール設定";
            // 
            // btnTestMail
            // 
            this.btnTestMail.Location = new System.Drawing.Point(61, 128);
            this.btnTestMail.Name = "btnTestMail";
            this.btnTestMail.Size = new System.Drawing.Size(113, 23);
            this.btnTestMail.TabIndex = 4;
            this.btnTestMail.Text = "テストメール送信";
            this.btnTestMail.UseVisualStyleBackColor = true;
            this.btnTestMail.Click += new System.EventHandler(this.btnTestMail_Click);
            // 
            // tbxPass
            // 
            this.tbxPass.Location = new System.Drawing.Point(111, 96);
            this.tbxPass.Name = "tbxPass";
            this.tbxPass.Size = new System.Drawing.Size(110, 19);
            this.tbxPass.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 99);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 12);
            this.label7.TabIndex = 2;
            this.label7.Text = "PASSWORD";
            // 
            // tbxId
            // 
            this.tbxId.Location = new System.Drawing.Point(111, 71);
            this.tbxId.Name = "tbxId";
            this.tbxId.Size = new System.Drawing.Size(110, 19);
            this.tbxId.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(16, 12);
            this.label6.TabIndex = 2;
            this.label6.Text = "ID";
            // 
            // tbxSmtp
            // 
            this.tbxSmtp.Location = new System.Drawing.Point(111, 46);
            this.tbxSmtp.Name = "tbxSmtp";
            this.tbxSmtp.Size = new System.Drawing.Size(110, 19);
            this.tbxSmtp.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "SMTPサーバ（送信）";
            // 
            // tbxMail
            // 
            this.tbxMail.Location = new System.Drawing.Point(111, 21);
            this.tbxMail.Name = "tbxMail";
            this.tbxMail.Size = new System.Drawing.Size(110, 19);
            this.tbxMail.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "通知先メールアドレス";
            // 
            // chkMailCsv
            // 
            this.chkMailCsv.AutoSize = true;
            this.chkMailCsv.Location = new System.Drawing.Point(7, 85);
            this.chkMailCsv.Name = "chkMailCsv";
            this.chkMailCsv.Size = new System.Drawing.Size(214, 16);
            this.chkMailCsv.TabIndex = 4;
            this.chkMailCsv.Text = "【予約検索】結果をメールアドレスに送信";
            this.chkMailCsv.UseVisualStyleBackColor = true;
            // 
            // chkMailInfo
            // 
            this.chkMailInfo.AutoSize = true;
            this.chkMailInfo.Location = new System.Drawing.Point(25, 41);
            this.chkMailInfo.Name = "chkMailInfo";
            this.chkMailInfo.Size = new System.Drawing.Size(187, 28);
            this.chkMailInfo.TabIndex = 1;
            this.chkMailInfo.Text = "フォルダに保存完了の通知メールを\r\n受け取る";
            this.chkMailInfo.UseVisualStyleBackColor = true;
            // 
            // chkOutCsv
            // 
            this.chkOutCsv.AutoSize = true;
            this.chkOutCsv.Checked = true;
            this.chkOutCsv.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOutCsv.Location = new System.Drawing.Point(7, 19);
            this.chkOutCsv.Name = "chkOutCsv";
            this.chkOutCsv.Size = new System.Drawing.Size(173, 16);
            this.chkOutCsv.TabIndex = 0;
            this.chkOutCsv.Text = "【予約検索】結果をCSVに出力";
            this.chkOutCsv.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(12, 566);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(113, 44);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "設定項目\r\nクリア";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(139, 566);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(113, 44);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "設定";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(11, 257);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(243, 1);
            this.panel1.TabIndex = 9;
            // 
            // ReserveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(264, 622);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.nudMin);
            this.Controls.Add(this.nudHour);
            this.Controls.Add(this.nudMonth);
            this.Controls.Add(this.cbxSun);
            this.Controls.Add(this.cbxSata);
            this.Controls.Add(this.cbxFri);
            this.Controls.Add(this.cbxThurs);
            this.Controls.Add(this.cbxWend);
            this.Controls.Add(this.cbxTues);
            this.Controls.Add(this.cbxMon);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rbnNone);
            this.Controls.Add(this.rbnDay);
            this.Controls.Add(this.rbnWeek);
            this.Controls.Add(this.rbnMonth);
            this.MaximizeBox = false;
            this.Name = "ReserveForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "予約設定";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ReserveForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.nudMonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHour)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMin)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbnMonth;
        private System.Windows.Forms.RadioButton rbnWeek;
        private System.Windows.Forms.RadioButton rbnDay;
        private System.Windows.Forms.RadioButton rbnNone;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbxMon;
        private System.Windows.Forms.CheckBox cbxTues;
        private System.Windows.Forms.CheckBox cbxWend;
        private System.Windows.Forms.CheckBox cbxThurs;
        private System.Windows.Forms.CheckBox cbxFri;
        private System.Windows.Forms.CheckBox cbxSata;
        private System.Windows.Forms.CheckBox cbxSun;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudMonth;
        private System.Windows.Forms.NumericUpDown nudHour;
        private System.Windows.Forms.NumericUpDown nudMin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbxMail;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnTestMail;
        private System.Windows.Forms.TextBox tbxPass;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbxId;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbxSmtp;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.CheckBox chkMailCsv;
        public System.Windows.Forms.CheckBox chkMailInfo;
        public System.Windows.Forms.CheckBox chkOutCsv;
    }
}