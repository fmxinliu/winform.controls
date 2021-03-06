﻿using System;
using System.Windows.Forms;

namespace WindowsTest {
    public partial class FrmLogin : TX.Framework.WindowUI.Forms.BaseForm {
        public FrmLogin() {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, System.EventArgs e) {
            //this.Hide();
            this.RealHide();
            FrmRegister f = new FrmRegister();
            f.StartPosition = FormStartPosition.Manual;
            f.Location = this.Location;
            var dr = f.ShowDialog();
            this.Location = f.Location;
            this.Show();
            //if (dr != DialogResult.Cancel) {
            //    this.Info("注册成功！");
            //}
        }

        private void btnLogin_Click(object sender, System.EventArgs e) {
            this.DialogResult = DialogResult.None;
            if (string.IsNullOrEmpty(this.txtUserName.Text) ||
                string.IsNullOrEmpty(this.txtPassWord.Text)) {
                this.lblErrorInfo.Text = "用户名或密码为空。";
                this.txtUserName.Clear();
                this.txtPassWord.Clear();
                this.txtUserName.Focus();
                return;
            }

            bool ret = DbHelper.Login(this.txtUserName.Text, this.txtPassWord.Text);
            if (ret) {
                this.lblErrorInfo.Text = "登录成功";
                this.DialogResult = DialogResult.OK;
            }
            else {
                this.lblErrorInfo.Text = "登录失败";
            }
        }
    }
}
