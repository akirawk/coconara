using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace RedundantFileSearch
{
    public partial class PanelExFrame : Panel
    {
        public PanelExFrame()
        {
            pen = new Pen(_borderColor);
            pen.DashStyle = BorderDrawStyle;
            InitializeComponent();
        }

        private Pen pen;

        /// <summary>
        /// 枠線
        /// </summary>
        private Color _borderColor = Color.Black;

        /// <summary>
        /// 枠線
        /// </summary>
        public Color BorderColor
        {
            get { return _borderColor; }
            set
            {
                _borderColor = value;
                pen?.Dispose();
                pen = new Pen(_borderColor);
                pen.DashStyle = BorderDrawStyle;
            }
        }

        public DashStyle BorderDrawStyle = DashStyle.Solid;

        /// <summary>
        /// OnPaintイベント
        /// </summary>
        /// <param name="e">イベントデータ</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            int right = ClientRectangle.Right - 1;
            int bottom = ClientRectangle.Bottom - 1;

            // 四角を描画
            var g = e.Graphics;
            g.DrawLine(pen, 0, 0, right, 0); // 上辺
            g.DrawLine(pen, 0, 0, 0, bottom); // 左辺
            g.DrawLine(pen, right, 0, right, bottom); // 右辺
            g.DrawLine(pen, 0, bottom, right, bottom); // 下辺
        }

        /// <summary>
        /// OnSizeChangedイベント
        /// </summary>
        /// <param name="e">イベントデータ</param>
        protected override void OnSizeChanged(EventArgs e)
        {
            // 描画をクリア
            Refresh();

            base.OnSizeChanged(e);
        }
    }
}
