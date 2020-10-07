using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using TX.Framework.WindowUI.Properties;

namespace TX.Framework.WindowUI.Controls {
    /// <summary>
    /// 指示灯排序
    /// </summary>
    public enum LightSeq {
        RYG,
        RGY,
        GYR,
        GRY,
        YRG,
        YGR,
    }
    /// <summary>
    /// 指示灯排序
    /// </summary>
    public enum LightLayout {
        Horizontal,
        Vertical,
    }
    [Description("三色指示灯（红、绿、黄）")]
    public partial class TXLightsTricolor : UserControl {
        private Light redLight, greenLight, yellowLight;
        private LightLayout lightLayout;
        private LightSeq lightSeq;
        private bool r, g, y;

        public TXLightsTricolor() {
            InitializeComponent();
            TurnOn(r, g, y);
        }

        [Browsable(true)]
        [Category("控制")]
        [Description("指示灯顺序，默认红、黄、绿")]
        public LightSeq Seq {
            get {
                return lightSeq;
            }
            set {
                lightSeq = value;
                TurnOn(r, g, y);
            }
        }

        [Browsable(true)]
        [Category("控制")]
        [Description("指示灯布局，默认水平")]
        public new LightLayout Layout {
            get {
                return lightLayout;
            }
            set {
                if (lightLayout != value) {
                    lightLayout = value;
                    int w = this.Width;
                    int h = this.Height;
                    this.Width = h;
                    this.Height = w;
                }
            }
        }

        public void TurnOn(bool r, bool g, bool y) {
            if (r) {
                redLight.FillColor = Color.FromArgb(255, 0, 0);
            }
            else {
                redLight.FillColor = Color.FromArgb(127, 0, 0);
            }

            if (g) {
                greenLight.FillColor = Color.FromArgb(0, 255, 0);
            }
            else {
                greenLight.FillColor = Color.FromArgb(0, 127, 0);
            }

            if (y) {
                yellowLight.FillColor = Color.FromArgb(255, 255, 0);
            }
            else {
                yellowLight.FillColor = Color.FromArgb(127, 127, 0);
            }

            this.r = r;
            this.g = g;
            this.y = y;
            this.Invalidate();
        }

        protected void ResizeLights() {
            int w = this.Width;
            int h = this.Height;
            int d; // 半径
            if (LightLayout.Vertical == Layout) {
                d = Math.Min(h / 4, w);
                int x = (w - d) / 2;
                int y1 = d / 4;
                int y2 = d + d / 2;
                int y3 = 2 * d + 3 * d / 4;
                if (LightSeq.RGY == Seq) {
                    redLight.Move(x, y1, d, d);
                    greenLight.Move(x, y2, d, d);
                    yellowLight.Move(x, y3, d, d);
                }
                else if (LightSeq.GYR == Seq) {
                    greenLight.Move(x, y1, d, d);
                    yellowLight.Move(x, y2, d, d);
                    redLight.Move(x, y3, d, d);
                }
                else if (LightSeq.GRY == Seq) {
                    greenLight.Move(x, y1, d, d);
                    redLight.Move(x, y2, d, d);
                    yellowLight.Move(x, y3, d, d);
                }
                else if (LightSeq.YRG == Seq) {
                    yellowLight.Move(x, y1, d, d);
                    redLight.Move(x, y2, d, d);
                    greenLight.Move(x, y3, d, d);
                }
                else if (LightSeq.YGR == Seq) {
                    yellowLight.Move(x, y1, d, d);
                    greenLight.Move(x, y2, d, d);
                    redLight.Move(x, y3, d, d);
                }
                else {
                    redLight.Move(x, y1, d, d);
                    yellowLight.Move(x, y2, d, d);
                    greenLight.Move(x, y3, d, d);
                }
            }
            else {
                d = Math.Min(w / 4, h);
                int y = (h - d) / 2;
                int x1 = d / 4;
                int x2 = d + d / 2;
                int x3 = 2 * d + 3 * d / 4;
                if (LightSeq.RGY == Seq) {
                    redLight.Move(x1, y, d, d);
                    greenLight.Move(x2, y, d, d);
                    yellowLight.Move(x3, y, d, d);
                }
                else if (LightSeq.GYR == Seq) {
                    greenLight.Move(x1, y, d, d);
                    yellowLight.Move(x2, y, d, d);
                    redLight.Move(x3, y, d, d);
                }
                else if (LightSeq.GRY == Seq) {
                    greenLight.Move(x1, y, d, d);
                    redLight.Move(x2, y, d, d);
                    yellowLight.Move(x3, y, d, d);
                }
                else if (LightSeq.YRG == Seq) {
                    yellowLight.Move(x1, y, d, d);
                    redLight.Move(x2, y, d, d);
                    greenLight.Move(x3, y, d, d);
                }
                else if (LightSeq.YGR == Seq) {
                    yellowLight.Move(x1, y, d, d);
                    greenLight.Move(x2, y, d, d);
                    redLight.Move(x3, y, d, d);
                }
                else {
                    redLight.Move(x1, y, d, d);
                    yellowLight.Move(x2, y, d, d);
                    greenLight.Move(x3, y, d, d);
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
            ResizeLights();
            this.redLight.Paint(e.Graphics);
            this.greenLight.Paint(e.Graphics);
            this.yellowLight.Paint(e.Graphics);
        }

        private struct Light {
            private int left, top, width, height;
            public Color FillColor;
            public void Move(int left, int top, int width, int height) {
                this.left = left;
                this.width = width;
                this.top = top;
                this.height = height;
            }
            // 绘制
            public void Paint(Graphics g) {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.DrawEllipse(Pens.Black, left, top, width, height);
                g.FillEllipse(new SolidBrush(this.FillColor), left, top, width, height);
            }
        }
    }
}
