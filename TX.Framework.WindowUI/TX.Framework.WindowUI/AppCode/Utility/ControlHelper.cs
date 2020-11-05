using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.APIs;
using System.Text;
using System.Windows.Forms;
using TX.Framework.WindowUI.Forms;

namespace TX.Framework.WindowUI {
    /// <summary>
    /// 控件处理的基本帮助类
    /// </summary>
    public static class ControlHelper {
        /// <summary>
        /// Not applicable
        /// </summary>
        public const string NA = "0.0";

        #region GetTextFormatFlags

        /// <summary>
        /// 获取文本的格式
        /// </summary>
        public static TextFormatFlags GetTextFormatFlags(ContentAlignment alignment, bool rightToleft) {
            TextFormatFlags flags = TextFormatFlags.WordBreak | TextFormatFlags.SingleLine;
            if (rightToleft) {
                flags |= TextFormatFlags.RightToLeft | TextFormatFlags.Right;
            }

            switch (alignment) {
                case ContentAlignment.BottomCenter:
                    flags |= TextFormatFlags.Bottom | TextFormatFlags.HorizontalCenter;
                    break;
                case ContentAlignment.BottomLeft:
                    flags |= TextFormatFlags.Bottom | TextFormatFlags.Left;
                    break;
                case ContentAlignment.BottomRight:
                    flags |= TextFormatFlags.Bottom | TextFormatFlags.Right;
                    break;
                case ContentAlignment.MiddleCenter:
                    flags |= TextFormatFlags.HorizontalCenter |
                        TextFormatFlags.VerticalCenter;
                    break;
                case ContentAlignment.MiddleLeft:
                    flags |= TextFormatFlags.VerticalCenter | TextFormatFlags.Left;
                    break;
                case ContentAlignment.MiddleRight:
                    flags |= TextFormatFlags.VerticalCenter | TextFormatFlags.Right;
                    break;
                case ContentAlignment.TopCenter:
                    flags |= TextFormatFlags.Top | TextFormatFlags.HorizontalCenter;
                    break;
                case ContentAlignment.TopLeft:
                    flags |= TextFormatFlags.Top | TextFormatFlags.Left;
                    break;
                case ContentAlignment.TopRight:
                    flags |= TextFormatFlags.Top | TextFormatFlags.Right;
                    break;
            }
            return flags;
        }
        #endregion

        #region BindMouseMoveEvent

        /// <summary>
        /// 给控件绑定鼠标移动的处理事件
        /// </summary>
        /// <param name="control">The control.</param>
        /// User:Ryan  CreateTime:2011-08-19 16:52.
        public static void BindMouseMoveEvent(Control control) {
            if (control != null) {
                control.MouseDown +=
                    delegate {
                        Win32.ReleaseCapture();
                        BaseForm fb = control.FindForm() as BaseForm;
                        if (fb != null && fb.CaptionHeight > 0 && fb.WindowState != FormWindowState.Maximized) {
                            Win32.SendMessage(control.FindForm().Handle, (int)WindowMessages.WM_SYSCOMMAND, (int)SystemCommands.SC_MOVE + (int)NCHITTEST.HTCAPTION, 0);
                        }
                    };
            }
        }

        #endregion

        #region SetEnable

        private const int GWL_STYLE = -16;
        private const int WS_DISABLED = 0x8000000;

        [System.Runtime.InteropServices.DllImport("user32.dll ")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int wndproc);

        [System.Runtime.InteropServices.DllImport("user32.dll ")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        /// <summary>
        /// 设置控件Enable = false，控件颜色不变
        /// </summary>
        public static void SetEnable(Control c, bool enabled) {
            c.Enabled = true; // 先使能控件

            if (enabled) {
                SetWindowLong(c.Handle, GWL_STYLE, (~WS_DISABLED) & GetWindowLong(c.Handle, GWL_STYLE));
            }
            else {
                SetWindowLong(c.Handle, GWL_STYLE, WS_DISABLED + GetWindowLong(c.Handle, GWL_STYLE));
            }

            c.Enabled = enabled; // 复原控件使能状态
        }
        #endregion

        #region

        #endregion
    }
}
