using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace RedundantFileSearch
{
    public partial class ReserveForm : Form
    {
        public ReserveForm()
        {
            InitializeComponent();

            weeks = new CheckBox[] { cbxMon, cbxTues, cbxWend, cbxThurs, cbxFri, cbxSata, cbxSun };

            UpdateDisplay();
        }

        private void ReserveForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        CheckBox[] weeks;

        private void UpdateDisplay()
        {
            nudMonth.Enabled = rbnMonth.Checked;
            foreach (var item in weeks)
            {
                item.Enabled = rbnWeek.Checked;
            }
            nudHour.Enabled = rbnDay.Checked;
            nudMin.Enabled = rbnDay.Checked;
        }

        private void rbnMonth_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDisplay();
        }

        private void rbnWeek_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDisplay();
        }

        private void rbnDay_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDisplay();
        }

        public bool IsReserve()
        {
            return GetReserveType() != EReserveType.None;
        }

        public enum EReserveType
        {
            Month,
            Week,
            Day,
            None
        }

        public EReserveType GetReserveType()
        {
            if (rbnMonth.Checked) return EReserveType.Month;
            if (rbnWeek.Checked) return EReserveType.Week;
            if (rbnDay.Checked) return EReserveType.Day;
            return EReserveType.None;
        }

        public int GetMonth()
        {
            return (int)nudMonth.Value;
        }

        public TimeSpan GetDay()
        {
            return new TimeSpan((int)nudHour.Value, (int)nudMin.Value, 0);
        }

        public DayOfWeek[] GetEDayOfWeeks()
        {
            var ret = new List<DayOfWeek>();
            if (cbxMon.Checked) ret.Add(DayOfWeek.Monday);
            if (cbxTues.Checked) ret.Add(DayOfWeek.Tuesday);
            if (cbxWend.Checked) ret.Add(DayOfWeek.Wednesday);
            if (cbxThurs.Checked) ret.Add(DayOfWeek.Thursday);
            if (cbxFri.Checked) ret.Add(DayOfWeek.Friday);
            if (cbxSata.Checked) ret.Add(DayOfWeek.Saturday);
            if (cbxSun.Checked) ret.Add(DayOfWeek.Sunday);
            return ret.ToArray();
        }

        private void btnTestMail_Click(object sender, EventArgs e)
        {
            var ret = SendMail(Form1.MAIL_SUBJECT_HEADER + "テストメール", "全部調べる君のテストメールです");
            if (string.IsNullOrEmpty(ret) == false)
            {
                MessageBox.Show(this, ret, "メール送信エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            rbnNone.Checked = true;
            chkOutCsv.Checked = true;
            chkMailInfo.Checked = false;
            chkMailCsv.Checked = false;
            tbxMail.Text = "";
            tbxId.Text = "";
            tbxPass.Text = "";
            tbxSmtp.Text = "";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        public string SendMail(string subject, string body, string filePath = null)
        {
            return SendMail(tbxMail.Text, tbxSmtp.Text, tbxId.Text, tbxPass.Text, subject, body, filePath);
        }

        private static string SendMail(string address, string smtpHost, string id, string password, string subject, string body, string filePath)
        {
            int smtpPort = 25;
            if (smtpHost.Contains(":"))
            {
                var s = smtpHost.Split(':');
                smtpHost = s[0];
                smtpPort = int.Parse(s[1]);
            }

            var message = new MimeKit.MimeMessage();
            message.From.Add(new MimeKit.MailboxAddress("from", address));
            message.To.Add(new MimeKit.MailboxAddress("to", address));
            message.Subject = subject;

            var textPart = new MimeKit.TextPart(MimeKit.Text.TextFormat.Plain);
            textPart.Text = body;
            if (string.IsNullOrEmpty(filePath))
            {
                message.Body = textPart;
            }
            else
            {
                var attachment = new MimeKit.MimePart("text", "csv")
                {
                    Content = new MimeKit.MimeContent(File.OpenRead(filePath)),
                    ContentDisposition = new MimeKit.ContentDisposition(),
                    ContentTransferEncoding = MimeKit.ContentEncoding.Base64,
                    FileName = Path.GetFileName(filePath)
                };
                var multipart = new MimeKit.Multipart("mixed")
                {
                    textPart,
                    attachment
                };
                message.Body = multipart;
            }


            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    client.Connect(smtpHost, smtpPort);
                    if (string.IsNullOrEmpty(id) == false)
                    {
                        client.Authenticate(id, password);
                    }
                    client.Send(message);
                    client.Disconnect(true);
                }
                catch (ArgumentException ex)
                {
                    return ex.Message;
                }
                catch (MailKit.Security.AuthenticationException ex)
                {
                    return ex.Message;
                }
                catch (MailKit.Net.Smtp.SmtpCommandException ex)
                {
                    return ex.Message;
                }
            }
            return null;
        }
    }
}
