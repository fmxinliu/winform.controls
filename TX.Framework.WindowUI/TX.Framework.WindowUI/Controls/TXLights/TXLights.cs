using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace TX.Framework.WindowUI.Controls {
    [Description("指示灯")]
    [ToolboxBitmap(typeof(Resfinder), "TX.Framework.WindowUI.Resources.light.bmp")]
    public partial class TXLights : UserControl {
        protected Color centerColor = Color.White;
        protected Color lightColor = Color.Green;
        protected Color darkColor = Color.Gray;
        private Color surroundColor = Color.Gray;
        private Color currentColor = Color.Gray;
        private bool turnOn = false;
        private int interval = 0;

        [Browsable(true)]
        [Category("控制")]
        [Description("闪烁间隔(ms)")]
        public int Interval {
            get { return interval; }
            set {
                if (value > 0) {
                    twinkleTimer.Interval = value;
                    interval = value;
                }
                else {
                    interval = 0;
                }
                UpdateTimerStatus();
            }
        }

        [Browsable(true)]
        [Category("控制")]
        [Description("是否显示指示灯边框。选择true，可修改Padding.All调整边框")]
        public bool IsDrawBorder { get; set; }

        [Browsable(true)]
        [Category("控制")]
        [Description("是否渐变显示点亮状态。选择true，可修改CenterColor调整中心颜色")]
        public bool IsGradient { get; set; }

        [Browsable(true)]
        [Category("控制")]
        [Description("开/关指示灯")]
        public bool TurnOn {
            get {
                return turnOn;
            }
            set {
                turnOn = value;
                UpdateTimerStatus();
            }
        }

        [Browsable(true)]
        [Category("控制")]
        [Description("指示灯点亮后的颜色")]
        public virtual Color LightColor {
            get { return lightColor; }
            set { lightColor = value; }
        }

        [Browsable(true)]
        [Category("控制")]
        [Description("指示灯熄灭后的颜色")]
        public virtual Color DarkColor {
            get { return darkColor; }
            set { darkColor = value; }
        }

        [Browsable(true)]
        [Category("控制")]
        [Description("指示灯中心颜色")]
        public virtual Color CenterColor {
            get { return centerColor; }
            set { centerColor = value; }
        }

        private Color SurroundColor {
            get {
                return LightColor;
            }
        }

        private Color CurrentColor {
            get {
                if (Interval > 0) {
                    currentColor = (TurnOn && currentColor == DarkColor) ? SurroundColor : DarkColor;
                }
                else {
                    currentColor = TurnOn ? SurroundColor : DarkColor;
                }
                return currentColor;
            }
        }

        public TXLights() {
            InitializeComponent();
        }

        private void UpdateTimerStatus() {
            if (turnOn && interval > 0) {
                twinkleTimer.Enabled = true;
            }
            else {
                twinkleTimer.Enabled = false;
            }
        }

        protected virtual void Light_Paint(object sender, PaintEventArgs e) {
            int padding = this.IsDrawBorder ? Padding.All : 0;
            int width = Width - padding;
            int height = Height - padding;
            if (this.IsGradient && this.TurnOn) {
                // 绘制中心渐变效果的圆形图案
                using (GraphicsPath path = new GraphicsPath()) {
                    path.AddEllipse(0, 0, Width, Height);
                    PathGradientBrush pthGrBrush = new PathGradientBrush(path);
                    pthGrBrush.CenterColor = CenterColor;
                    Color[] colors = { CurrentColor };
                    pthGrBrush.SurroundColors = colors;
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    e.Graphics.FillEllipse(pthGrBrush, 0, 0, width, height);
                }
            }
            else {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.FillEllipse(new SolidBrush(this.CurrentColor), 0, 0, width, height);
            }

            if (this.IsDrawBorder) {
                e.Graphics.DrawEllipse(Pens.Black, 0, 0, width, height);
            }
        }

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
            if (!this.IsDrawBorder) {
                using (GraphicsPath path = new GraphicsPath()) {
                    // 绘制圆形控件背景框
                    path.AddEllipse(0, 0, Width, Height);
                    this.Region = new Region(path);
                }
            }
        }

        protected override CreateParams CreateParams {
            get {
                var parms = base.CreateParams;
                parms.Style &= ~0x02000000;  // Turn off WS_CLIPCHILDREN
                return parms;
            }
        }

        private void TwinkleTimer_Tick(object sender, EventArgs e) {
            this.Invalidate();
        }
    }
}
