using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TX.Framework.WindowUI.Controls {
    [ToolboxBitmap(typeof(ContextMenuStrip))]
    public class TXContextMenuStrip : ContextMenuStrip {
        //TXToolStripRenderer renderer;
        #region enums

        public enum PopupMenuAlignment { LeftBottom, RightBottom, LeftTop, RightTop }

        #endregion

        #region Initializes

        public TXContextMenuStrip() : base() {
            base.BackColor = SkinManager.CurrentSkin.BaseColor;
            //renderer = new TXToolStripRenderer();
            //this.Renderer = renderer;
            base.RenderMode = ToolStripRenderMode.ManagerRenderMode;
            this.Opening += new CancelEventHandler(this.TXContextMenuStrip_Opening);
        }

        #endregion

        #region Properties

        [Category("TXProperties")]
        [DefaultValue(typeof(ToolStripRenderMode), "ManagerRenderMode")]
        public new ToolStripRenderMode RenderMode {
            get { return base.RenderMode; }
            set {

                base.RenderMode = value;
                base.Invalidate();
            }
        }

        //public new Color BackColor
        //{
        //    get { return base.BackColor; }
        //    set { base.BackColor = value; this.renderer.BackColor = value; base.Invalidate(); }
        //}

        [DefaultValue(PopupMenuAlignment.LeftBottom)]
        [Category("TXProperties")]
        [Description("右键弹出菜单对齐方式。")]
        public PopupMenuAlignment ContextPopupMenuAlign { get; set; }

        #endregion

        #region private Events

        private void TXContextMenuStrip_Opening(object sender, CancelEventArgs e) {
            Point p = Cursor.Position;
            Point menuPosition = new Point();
            if (this.ContextPopupMenuAlign == PopupMenuAlignment.LeftTop) {
                menuPosition.X = p.X;
                menuPosition.Y = p.Y;
            }
            else if (this.ContextPopupMenuAlign == PopupMenuAlignment.RightTop) {
                menuPosition.X = p.X - this.Size.Width;
                menuPosition.Y = p.Y;
            }
            else if (this.ContextPopupMenuAlign == PopupMenuAlignment.LeftBottom) {
                menuPosition.X = p.X;
                menuPosition.Y = p.Y - this.Size.Height;
            }
            else if (this.ContextPopupMenuAlign == PopupMenuAlignment.RightBottom) {
                menuPosition.X = p.X - this.Size.Width;
                menuPosition.Y = p.Y - this.Size.Height;
            }
            else {
                return;
            }
            this.Show(menuPosition);
        }

        #endregion
    }
}
