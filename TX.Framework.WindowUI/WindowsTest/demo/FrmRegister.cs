using System;
using System.Windows.Forms;

namespace WindowsTest {
    public partial class FrmRegister : TX.Framework.WindowUI.Forms.BaseForm {
        public FrmRegister() {
            InitializeComponent();
        }

        private bool verifying = false;
        private void btnRegister_Click(object sender, System.EventArgs e) {
            this.verifying = true;
            bool inputValid = true;
            if (inputValid && string.IsNullOrEmpty(this.txtUserName.Text)) {
                this.lblErrorInfo.Text = "请输入用户名";
                this.txtUserName.Focus();
                inputValid = false;
            }
            if (inputValid && string.IsNullOrWhiteSpace(this.txtUserName.Text)) {
                this.lblErrorInfo.Text = "用户名不能为空白符";
                this.txtUserName.Focus();
                this.txtUserName.SelectAll();
                inputValid = false;
            }
            if (inputValid && string.IsNullOrEmpty(this.txtPassWord.Text)) {
                this.lblErrorInfo.Text = "请输入密码";
                this.txtConfirmPasswd.Clear();
                this.txtPassWord.Focus();
                inputValid = false;
            }
            if (inputValid && string.IsNullOrEmpty(this.txtConfirmPasswd.Text)) {
                this.lblErrorInfo.Text = "请再次输入密码";
                this.txtConfirmPasswd.Focus();
                inputValid = false;
            }
            if (inputValid && this.txtPassWord.Text != this.txtConfirmPasswd.Text) {
                this.lblErrorInfo.Text = "两次输入的密码不一致";
                this.txtConfirmPasswd.Clear();
                this.txtPassWord.Clear();
                this.txtPassWord.Focus();
                inputValid = false;
            }
            if (inputValid) {
                if (DbHelper.IsUserExist(this.txtUserName.Text)) {
                    this.lblErrorInfo.Text = "用户名已经被注册";
                    this.txtUserName.Focus();
                    this.txtUserName.SelectAll();
                    return;
                }
                if (!DbHelper.Register(this.txtUserName.Text, this.txtPassWord.Text)) {
                    this.lblErrorInfo.Text = "注册失败";
                    return;
                }
                this.dr = DialogResult.OK;
                this.Close();
            }
            else {
                this.btnRegister.DialogResult = System.Windows.Forms.DialogResult.None;
            }
            this.verifying = false;
        }

        private void InputInfo_TextChanged(object sender, System.EventArgs e) {
            if (!this.verifying && this.lblErrorInfo.Text.Length > 0) {
                this.lblErrorInfo.Text = string.Empty;
            }
        }

        private DialogResult dr = DialogResult.Cancel;
        public new DialogResult ShowDialog() {
            base.ShowDialog();
            return this.dr;
        }
    }
}
