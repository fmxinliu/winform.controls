using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace TX.Framework.WindowUI.Controls {
    /// <summary>
    /// 指示灯颜色
    /// </summary>
    public enum Colors {
        Red,
        Green,
        Yellow,
        Gray,
    }
    [Description("单色指示灯（红、绿、黄、灰）")]
    [ToolboxBitmap(typeof(Resfinder), "TX.Framework.WindowUI.Resources.green.bmp")]
    public partial class TXLightsMono : TXLights {
        private Colors showColor;
        [Browsable(true)]
        [Category("控制")]
        [Description("指示灯颜色，默认灰色")]
        public Colors ShowColor {
            get { return showColor; }
            set {
                showColor = value;
                if (Colors.Red == showColor) {
                    LightColor = Color.FromArgb(255, 0, 0);
                    DarkColor = Color.FromArgb(127, 0, 0);
                }
                else if (Colors.Green == showColor) {
                    LightColor = Color.FromArgb(0, 255, 0);
                    DarkColor = Color.FromArgb(0, 127, 0);
                }
                else if (Colors.Yellow == showColor) {
                    LightColor = Color.FromArgb(255, 255, 0);
                    DarkColor = Color.FromArgb(127, 127, 0);
                }
                else {
                    LightColor = DarkColor = Color.Gray;
                }
            }
        }

        [Browsable(false)]
        [Category("控制")]
        [Description("指示灯点亮后的颜色")]
        public override Color LightColor {
            get { return lightColor; }
            set { lightColor = value; }
        }

        [Browsable(false)]
        [Category("控制")]
        [Description("指示灯熄灭后的颜色")]
        public override Color DarkColor {
            get { return darkColor; }
            set { darkColor = value; }
        }

        [Browsable(false)]
        [Category("控制")]
        [Description("指示灯中心颜色")]
        public override Color CenterColor {
            get { return centerColor; }
            set { centerColor = value; }
        }

        public TXLightsMono() {
            InitializeComponent();
            ShowColor = Colors.Gray;
        }

        protected override CreateParams CreateParams {
            get {
                var parms = base.CreateParams;
                parms.Style &= ~0x02000000;  // Turn off WS_CLIPCHILDREN
                return parms;
            }
        }
    }
}
