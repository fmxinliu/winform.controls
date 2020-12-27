using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Win32;
using System.Windows.Forms;
using TX.Framework.WindowUI;
using TX.Framework.WindowUI.Forms;


namespace WindowsTest {
    public partial class WinMain : MainForm {
        public WinMain() {
            InitializeComponent();

            this.txTreeComboBox1.DataSource = TestData.GetTreeData(10);
            this.txTreeComboBox1.MultiLevelDataSourceMember = "Users";
            this.txTreeComboBox1.DisplayMember = "Name";
            this.txTreeComboBox1.ValueMember = "Value";
            this.txTreeComboBox1.BindData();

            this.ComboBoxInit();
            this.ComboBoxSimInit();
            this.ComboBoxBindInit();
            this.TextBoxAutoCompleteInit();
        }

        private void txButton1_Click(object sender, EventArgs e) {
            //BaseForm
            BaseForm win = new BaseForm();
            win.ShowDialog();
        }

        private void txButton4_Click(object sender, EventArgs e) {
            FrmErrorBox.ShowError(new Exception("some error", new ArgumentException("argument error", "test")));
        }

        private void txButton2_Click(object sender, EventArgs e) {
            PopForm win = new PopForm();
            win.Show();
        }

        private void txButton7_Click(object sender, EventArgs e) {
            this.Waiting(() => {
                System.Threading.Thread.Sleep(12000);
            });
        }

        private void txButton3_Click(object sender, EventArgs e) {
            this.Info("提示消息！");
        }

        private void txButton5_Click(object sender, EventArgs e) {
            this.Warning("客官，请自重！");
        }

        private void txButton6_Click(object sender, EventArgs e) {
            this.Error("出错了！");
        }

        private void txButton8_Click(object sender, EventArgs e) {
            this.Question("你确定要退出吗？");
        }

        private void txButton9_Click(object sender, EventArgs e) {
            FrmList win = new FrmList();
            win.ShowDialog();
        }

        private void winMain_Load(object sender, EventArgs e) {

        }

        private void tabPage3_Click(object sender, EventArgs e) {

        }

        private void txTextBox1_ImageButtonClick(object sender, EventArgs e) {
        }

        ToolTip toolTip = new ToolTip();
        private void txTextBox1_TextChanged(object sender, EventArgs e) {
            this.txTextBox1.ToolTipIsBalloon = true;
            this.txTextBox1.ToolTipIcon = ToolTipIcon.Error;
            this.txTextBox1.ToolTipTitle = "错误";
            //toolTip.IsBalloon = true;
            //toolTip.ToolTipIcon = ToolTipIcon.Error;
            //toolTip.ToolTipTitle = "错误";
            if (this.txTextBox1.Text.Length % 2 == 0) {
                int x = txTextBox1.ToolTipCursorPosition.X;
                int y = txTextBox1.ToolTipCursorPosition.Y;
                this.txTextBox1.ShowToolTip("123hjgj dgdgsdvgdfg打广告电饭锅到发", x, -100);
                //int x = txTextBox1.GetCurrentCursorPosition().X;
                //toolTip.Show("123hjgj dgdgsdvgdfg打广告电饭锅到发", this.txTextBox1,
                //    x,
                //    -100);
            }
            else {
                //toolTip.Hide(this.txTextBox1);
                this.txTextBox1.HideToolTip();
            }
        }

        private void txTextBox1_LostFocus(object sender, EventArgs e) {
        }

        // 文本框输入补全
        private void TextBoxAutoCompleteInit() {
            string[] names = new string[] { "张三丰", "独孤求败", "风清扬", "扫地僧" };
            var source = new AutoCompleteStringCollection();
            source.AddRange(names);
            this.txTextBox3.AutoCompleteCustomSource = source;
            this.txTextBox3.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.txTextBox3.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void ComboBoxInit() {
            this.cboSelect.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.cboSelect.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void ComboBoxSimInit() {
            string[] names = new string[] { "张三丰", "独孤求败", "风清扬", "扫地僧" };
            var source = new AutoCompleteStringCollection();
            source.AddRange(names);
            this.txtSelect.AutoCompleteCustomSource = source;
            this.txtSelect.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.txtSelect.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void lstSelect_Click(object sender, EventArgs e) {
            this.txtSelect.Text = this.lstSelect.Text;
            this.txtSelect.Focus();
            this.txtSelect.SelectAll(); // 获取输入焦点，才能选中
        }

        internal class ComboBoxObject {
            public string Name { get; set; }
            public string Value { get; set; }
        }

        private void ComboBoxBindInit() {
            //this.txtSelectText.ReadOnly = true; // 文本可选
            //this.txtSelectText.Enabled = false; // 文本不可选
            ControlHelper.SetEnable(this.txtSelectText, false);
            this.txtSelectText.Text = "测试数据绑定";

            List<ComboBoxObject> items = new List<ComboBoxObject>();
            for (int i = 0; i < 10; i++) {
                ComboBoxObject o = new ComboBoxObject();
                o.Name = "ComboBox" + i;
                o.Value = i.ToString();
                items.Add(o);
            }

            this.lstItems.DataSource = items; // 绑定数据源
            this.lstItems.DisplayMember = "Name"; // 列表项显示的属性值
            this.lstItems.ValueMember = "Value";  // 列表项选中时，SelectedValue显示的属性值

            this.lstItems.SelectionMode = SelectionMode.MultiExtended; // 设置可多选
        }

        private void lstItems_Click(object sender, EventArgs e) {
            var items = this.lstItems.SelectedItems;

            // 设定了ValueMember，这里显示设定的属性值；否则，显示对象本身
            object o = this.lstItems.SelectedValue;
        }

        private void txtLoginInput_TextChanged(object sender, EventArgs e) {
            //this.lblLoginInfo.Text = string.Empty;
            this.lblLoginInfo.Visible = false;
        }

        private void btnLogin_Click(object sender, EventArgs e) {
            this.lblLoginInfo.Visible = true;
            if (string.IsNullOrWhiteSpace(this.txtUserName.Text)) {
                this.lblLoginInfo.Text = "请输入用户名";
            }
            else if (string.IsNullOrWhiteSpace(this.txtPassWord.Text)) {
                this.lblLoginInfo.Text = "请输入密码";
            }
            else {
                this.lblLoginInfo.Text = "登录成功!";
            }
        }

        private void ToolTipsInForm_Popup(object sender, PopupEventArgs e) {
            if (e.AssociatedControl.Name == "lblLoginInfo" &&
                string.Empty != this.lblLoginInfo.Text &&
                "登录成功!" != this.lblLoginInfo.Text) {
                this.ToolTipsInForm.ToolTipTitle = "错误";
                this.ToolTipsInForm.ToolTipIcon = ToolTipIcon.Error;
                this.ToolTipsInForm.SetToolTip(this.lblLoginInfo, "登录失败");
            }
            else {
                this.ToolTipsInForm.ToolTipTitle = "提示";
                this.ToolTipsInForm.ToolTipIcon = ToolTipIcon.Info;
                this.ToolTipsInForm.SetToolTip(this.lblLoginInfo, "登录成功");
            }
        }

        private void ToolStripMenuItemStart_Click(object sender, EventArgs e) {
            this.txNotifyIcon.Icon = TX.Framework.WindowUI.Properties.Resources.start;
        }

        private void ToolStripMenuItemStop_Click(object sender, EventArgs e) {
            this.txNotifyIcon.Icon = TX.Framework.WindowUI.Properties.Resources.stop;
        }

        private void ToolStripMenuItemFlash_Click(object sender, EventArgs e) {
            if (this.ToolStripMenuItemFlash.Text != "停止闪烁") {
                this.txNotifyIcon.FlashEnabled = true;
                this.ToolStripMenuItemFlash.Text = "停止闪烁";
            }
            else {
                this.txNotifyIcon.FlashEnabled = false;
                this.ToolStripMenuItemFlash.Text = "闪烁";
                //this.txNotifyIcon.Icon = TX.Framework.WindowUI.Properties.Resources.logo;
            }
        }

        private void ToolStripMenuItemExit_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void TXToolStripMenuItemExit_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void WinMain_FormClosed(object sender, FormClosedEventArgs e) {
            this.txNotifyIcon.Visible = false; // 退出前，隐藏托盘图标，否则会有残漏
        }

        // 单击托盘图标
        private void txNotifyIcon_MouseClick(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                this.WindowState = this.state; // 显示窗体
                this.Activate(); // 激活窗体
            }
        }

        private FormWindowState state;
        private void WinMain_SizeChanged(object sender, EventArgs e) {
            if (this.WindowState != FormWindowState.Minimized) {
                this.state = this.WindowState; // 记录窗体最小化前的状态
            }
        }

        // 捕获 Form 最大/最小化事件
        protected override void WndProc(ref Message m) {
            if (m.Msg == WM_SYSCOMMAND) {
                switch (m.WParam.ToInt32()) {
                    case SC_MINIMIZE:
                        break;
                    case SC_MAXIMIZE:
                        break;
                    case SC_RESTORE:
                        break;
                    case SC_CLOSE:
                        break;
                }
            }

            base.WndProc(ref m);
        }

        private void rdoFlashWindow_CheckedChanged(object sender, EventArgs e) {
            this.nupFreq.Enabled = this.rdoFlashWindowEx.Checked;
            this.nupCount.Enabled = this.rdoFlashWindowEx.Checked;
        }

        private void btnFlash_Click(object sender, EventArgs e) {
            if (this.rdoFlashWindow.Checked) {
                NativeMethods.FlashWindow(this.Handle, true);
            }
            else {
                NativeMethods.FlashWindow(this.Handle, (int)this.nupCount.Value, (int)this.nupFreq.Value,
                    System.Runtime.InteropServices.APIs.APIsEnums.FlashWindowFlags.FLASHW_TRAY);
            }
        }

        private void btnShowLoginForm_Click(object sender, EventArgs e) {
            FrmLogin f = new FrmLogin();
            if (DialogResult.OK == f.ShowDialog()) {
                // 显示主窗体
            }
        }
    }
}
