using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TX.Framework.WindowUI.Controls {
    [ToolboxBitmap(typeof(NotifyIcon))]
    public class TXNotifyIcon : Component {
        #region fileds

        private Icon _FlashIcon;
        private Timer _FlashTimer;
        private NotifyIcon _NotifyIcon;

        private bool visible = true;
        private bool flashFlag = true;

        #endregion

        #region enums

        public enum PopupMenuAlignment { LeftBottom, RightBottom, LeftTop, RightTop }

        #endregion

        #region Initializes

        public TXNotifyIcon() {
            TaskBarHelper.RefreshNotification();
            this._NotifyIcon = new NotifyIcon();
            this._NotifyIcon.BalloonTipClicked += new EventHandler(_NotifyIcon_BalloonTipClicked);
            this._NotifyIcon.BalloonTipClosed += new EventHandler(_NotifyIcon_BalloonTipClosed);
            this._NotifyIcon.Click += new EventHandler(_NotifyIcon_Click);
            this._NotifyIcon.DoubleClick += new EventHandler(_NotifyIcon_DoubleClick);
            this._NotifyIcon.MouseClick += new MouseEventHandler(_NotifyIcon_MouseClick);
            this._NotifyIcon.MouseDoubleClick += new MouseEventHandler(_NotifyIcon_MouseDoubleClick);
            this._NotifyIcon.MouseDown += new MouseEventHandler(_NotifyIcon_MouseDown);
            this._NotifyIcon.MouseMove += new MouseEventHandler(_NotifyIcon_MouseMove);
            this._NotifyIcon.MouseUp += new MouseEventHandler(_NotifyIcon_MouseUp);
            this._FlashTimer = new Timer();
            this._FlashTimer.Tick += new EventHandler(_FlashTimer_Tick);
        }

        ~TXNotifyIcon() {
            TaskBarHelper.RefreshNotification();
        }

        #endregion

        #region Properties

        [Category("TXProperties")]
        [Description("与气球状工具提示关联的图标。")]
        public ToolTipIcon BalloonTipIcon {
            get { return this._NotifyIcon.BalloonTipIcon; }
            set { this._NotifyIcon.BalloonTipIcon = value; }
        }

        [DefaultValue("")]
        [Localizable(true)]
        [Category("TXProperties")]
        [Description("与气球状工具提示关联的文本。")]
        public string BalloonTipText {
            get { return this._NotifyIcon.BalloonTipText; }
            set { this._NotifyIcon.BalloonTipText = value; }
        }

        [DefaultValue("")]
        [Localizable(true)]
        [Category("TXProperties")]
        [Description("气球状工具提示的标题。")]
        public string BalloonTipTitle {
            get { return this._NotifyIcon.BalloonTipTitle; }
            set { this._NotifyIcon.BalloonTipTitle = value; }
        }

        [Browsable(false)]
        [DefaultValue("")]
        [Category("TXProperties")]
        [Description("图标的快捷菜单。")]
        public ContextMenu ContextMenu {
            get { return this._NotifyIcon.ContextMenu; }
            set { this._NotifyIcon.ContextMenu = value; }
        }

        [DefaultValue("")]
        [Category("TXProperties")]
        [Description("当用户右击该图标时显示的快捷菜单。")]
        public ContextMenuStrip ContextMenuStrip {
            get { return this._NotifyIcon.ContextMenuStrip; }
            set {
                this._NotifyIcon.ContextMenuStrip = value;
                if (value != null) {
                    this._NotifyIcon.ContextMenuStrip.Opening -= new CancelEventHandler(this.ContextMenuStrip_Opening);
                    this._NotifyIcon.ContextMenuStrip.Opening += new CancelEventHandler(this.ContextMenuStrip_Opening);
                }
            }
        }

        [DefaultValue(null)]
        [Localizable(true)]
        [Category("TXProperties")]
        [Description("将在系统栏中显示的图标。")]
        public Icon Icon {
            get { return this._NotifyIcon.Icon; }
            set {
                bool isFlashing = this.FlashEnabled;
                if (isFlashing) {
                    this.FlashEnabled = false;
                }
                this._NotifyIcon.Icon = value;
                if (isFlashing) {
                    this.FlashEnabled = true;
                }
            }
        }

        [TypeConverter(typeof(StringConverter))]
        [Localizable(false)]
        [DefaultValue("")]
        [Bindable(true)]
        [Category("TXProperties")]
        [Description("与对象关联的用户定义数据。")]
        public object Tag {
            get { return this._NotifyIcon.Tag; }
            set { this._NotifyIcon.Tag = value; }
        }

        [Localizable(true)]
        [DefaultValue("")]
        [Category("TXProperties")]
        [Description("当鼠标悬停在该图标上时显示的文本。")]
        public string Text {
            get { return this._NotifyIcon.Text; }
            set { this._NotifyIcon.Text = value; }
        }

        [DefaultValue(false)]
        [Localizable(true)]
        [Category("TXProperties")]
        [Description("确定该控件是可见的还是隐藏的。")]
        public bool Visible {
            get { return this.visible; }
            set {
                this.visible = value;
                if (!this.IsDesignMode) {
                    this._NotifyIcon.Visible = value;
                }
            }
        }

        [Browsable(false)]
        [Category("TXProperties")]
        [Description("当前是否处于设计模式。")]
        public bool IsDesignMode {
            get {
                return
                    (this.GetService(typeof(IDesignerHost)) != null) ||
                    (LicenseManager.UsageMode == LicenseUsageMode.Designtime);
            }
        }

        [DefaultValue(false)]
        [Category("TXProperties")]
        [Description("是否使能闪烁。")]
        public bool FlashEnabled {
            get { return this._FlashTimer.Enabled; }
            set {
                this._FlashTimer.Enabled = value;
                if (!value && this._FlashIcon != null) {
                    this._NotifyIcon.Icon = this._FlashIcon; // 停止闪烁，显示初始图标
                    this._FlashIcon = null;
                    this.flashFlag = true;
                }
            }
        }

        [DefaultValue(100)]
        [Category("TXProperties")]
        [Description("闪烁频率（ms）。")]
        public int FlashInterval {
            get { return this._FlashTimer.Interval; }
            set { this._FlashTimer.Interval = value; }
        }

        [DefaultValue(PopupMenuAlignment.LeftBottom)]
        [Category("TXProperties")]
        [Description("右键弹出菜单对齐方式。")]
        public PopupMenuAlignment ContextPopupMenuAlign { get; set; }

        #endregion

        #region Events

        [Category("TXEvents")]
        [Description("单击气球状工具提示时发生。")]
        public event EventHandler BalloonTipClicked {
            add { base.Events.AddHandler("BalloonTipClicked", value); }
            remove { base.Events.RemoveHandler("BalloonTipClicked", value); }
        }

        [Category("TXEvents")]
        [Description("关闭气球状工具提示时发生。")]
        public event EventHandler BalloonTipClosed {
            add { base.Events.AddHandler("BalloonTipClosed", value); }
            remove { base.Events.RemoveHandler("BalloonTipClosed", value); }
        }

        [Category("TXEvents")]
        [Description("显示气球状提示时发生。")]
        public event EventHandler BalloonTipShown {
            add { base.Events.AddHandler("BalloonTipShown", value); }
            remove { base.Events.RemoveHandler("BalloonTipShown", value); }
        }

        [Category("TXEvents")]
        [Description("单击组件时发生。")]
        public new event EventHandler Click {
            add { base.Events.AddHandler("Click", value); }
            remove { base.Events.RemoveHandler("Click", value); }
        }

        [Category("TXEvents")]
        [Description("双击组件时发生。")]
        public new event EventHandler DoubleClick {
            add { base.Events.AddHandler("DoubleClick", value); }
            remove { base.Events.RemoveHandler("DoubleClick", value); }
        }

        [Category("TXEvents")]
        [Description("用鼠标单击组件时发生。")]
        public new event MouseEventHandler MouseClick {
            add { base.Events.AddHandler("MouseClick", value); }
            remove { base.Events.RemoveHandler("MouseClick", value); }
        }

        [Category("TXEvents")]
        [Description("用鼠标双击组件时发生。")]
        public new event MouseEventHandler MouseDoubleClick {
            add { base.Events.AddHandler("MouseDoubleClick", value); }
            remove { base.Events.RemoveHandler("MouseDoubleClick", value); }
        }

        [Category("TXEvents")]
        [Description("当鼠标指针在组件上方并按下鼠标按钮时发生。")]
        public new event MouseEventHandler MouseDown {
            add { base.Events.AddHandler("MouseDown", value); }
            remove { base.Events.RemoveHandler("MouseDown", value); }
        }

        [Category("TXEvents")]
        [Description("鼠标指针移过组件时发生。")]
        public new event MouseEventHandler MouseMove {
            add { base.Events.AddHandler("MouseMove", value); }
            remove { base.Events.RemoveHandler("MouseMove", value); }
        }

        [Category("TXEvents")]
        [Description("当鼠标指针在组件上方并释放鼠标按钮时发生。")]
        public new event MouseEventHandler MouseUp {
            add { base.Events.AddHandler("MouseUp", value); }
            remove { base.Events.RemoveHandler("MouseUp", value); }
        }

        #endregion

        #region private Events

        private void _NotifyIcon_BalloonTipClicked(object sender, EventArgs e) {
            this.EventHandlerImpl("BalloonTipClicked", e);
        }

        private void _NotifyIcon_BalloonTipClosed(object sender, EventArgs e) {
            this.EventHandlerImpl("BalloonTipClosed", e);
        }

        private void _NotifyIcon_Click(object sender, EventArgs e) {
            this.EventHandlerImpl("Click", e);
        }

        private void _NotifyIcon_DoubleClick(object sender, EventArgs e) {
            this.EventHandlerImpl("DoubleClick", e);
        }

        private void _NotifyIcon_MouseClick(object sender, MouseEventArgs e) {
            this.MouseEventHandlerImpl("MouseClick", e);
        }

        private void _NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e) {
            this.MouseEventHandlerImpl("MouseDoubleClick", e);
        }

        private void _NotifyIcon_MouseDown(object sender, MouseEventArgs e) {
            this.MouseEventHandlerImpl("MouseDown", e);
        }

        private void _NotifyIcon_MouseMove(object sender, MouseEventArgs e) {
            this.MouseEventHandlerImpl("MouseMove", e);
        }

        private void _NotifyIcon_MouseUp(object sender, MouseEventArgs e) {
            this.MouseEventHandlerImpl("MouseUp", e);
        }

        private void _FlashTimer_Tick(object sender, EventArgs e) {
            if (this.IsDesignMode || this.Icon == null) {
                return; // 未设置图标，不闪烁
            }
            if (this._FlashIcon == null) {
                this._FlashIcon = this.Icon; // 记录原始图标
            }
            this._NotifyIcon.Icon = flashFlag ? Properties.Resources.transparent : this._FlashIcon;
            flashFlag = !flashFlag;
        }

        private void ContextMenuStrip_Opening(object sender, CancelEventArgs e) {
            Point p = Cursor.Position;
            Point menuPosition = new Point();
            if (this.ContextPopupMenuAlign == PopupMenuAlignment.LeftTop) {
                menuPosition.X = p.X;
                menuPosition.Y = p.Y;
            }
            else if (this.ContextPopupMenuAlign == PopupMenuAlignment.RightTop) {
                menuPosition.X = p.X - this.ContextMenuStrip.Size.Width;
                menuPosition.Y = p.Y;
            }
            else if (this.ContextPopupMenuAlign == PopupMenuAlignment.LeftBottom) {
                menuPosition.X = p.X;
                menuPosition.Y = p.Y - this.ContextMenuStrip.Size.Height;
            }
            else if (this.ContextPopupMenuAlign == PopupMenuAlignment.RightBottom) {
                menuPosition.X = p.X - this.ContextMenuStrip.Size.Width;
                menuPosition.Y = p.Y - this.ContextMenuStrip.Size.Height;
            }
            else {
                return;
            }
            this.ContextMenuStrip.Show(menuPosition);
        }

        #endregion

        #region private methods

        private void EventHandlerImpl(object key, EventArgs e) {
            var handler = base.Events[key] as EventHandler;
            if (handler != null) {
                handler(this, e);
            }
        }

        private void MouseEventHandlerImpl(object key, MouseEventArgs e) {
            var handler = base.Events[key] as MouseEventHandler;
            if (handler != null) {
                handler(this, e);
            }
        }

        #endregion

        #region public methods

        public void ShowBalloonTip(int timeout) {
            this._NotifyIcon.ShowBalloonTip(timeout);
        }

        public void ShowBalloonTip(int timeout, string tipTitle, string tipText, ToolTipIcon tipIcon) {
            this._NotifyIcon.ShowBalloonTip(timeout, tipTitle, tipText, tipIcon);
        }

        #endregion
    }
}
