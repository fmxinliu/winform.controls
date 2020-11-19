using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
    }
}
