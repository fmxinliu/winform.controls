namespace WindowsTest {
    partial class FrmLogin {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnRegister = new System.Windows.Forms.Button();
            this.lblErrorInfo = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblPassWord = new System.Windows.Forms.Label();
            this.pnlBody = new TX.Framework.WindowUI.Controls.TXPanel();
            this.txtUserName = new TX.Framework.WindowUI.Controls.TXTextBox();
            this.txtPassWord = new TX.Framework.WindowUI.Controls.TXTextBox();
            this.pnlBody.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLogin
            // 
            this.btnLogin.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnLogin.Location = new System.Drawing.Point(223, 146);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(76, 33);
            this.btnLogin.TabIndex = 5;
            this.btnLogin.Text = "登录";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnRegister
            // 
            this.btnRegister.Location = new System.Drawing.Point(109, 146);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(76, 33);
            this.btnRegister.TabIndex = 4;
            this.btnRegister.Text = "注册";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // lblErrorInfo
            // 
            this.lblErrorInfo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblErrorInfo.ForeColor = System.Drawing.Color.Red;
            this.lblErrorInfo.Location = new System.Drawing.Point(110, 108);
            this.lblErrorInfo.Name = "lblErrorInfo";
            this.lblErrorInfo.Size = new System.Drawing.Size(196, 23);
            this.lblErrorInfo.TabIndex = 6;
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(34, 32);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(52, 15);
            this.lblUserName.TabIndex = 0;
            this.lblUserName.Text = "用户名";
            // 
            // lblPassWord
            // 
            this.lblPassWord.AutoSize = true;
            this.lblPassWord.Location = new System.Drawing.Point(42, 73);
            this.lblPassWord.Name = "lblPassWord";
            this.lblPassWord.Size = new System.Drawing.Size(37, 15);
            this.lblPassWord.TabIndex = 2;
            this.lblPassWord.Text = "密码";
            // 
            // pnlBody
            // 
            this.pnlBody.AutoScroll = true;
            this.pnlBody.BackBeginColor = System.Drawing.Color.Transparent;
            this.pnlBody.BackColor = System.Drawing.Color.Transparent;
            this.pnlBody.BackEndColor = System.Drawing.Color.Transparent;
            this.pnlBody.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(168)))), ((int)(((byte)(192)))));
            this.pnlBody.Controls.Add(this.btnLogin);
            this.pnlBody.Controls.Add(this.btnRegister);
            this.pnlBody.Controls.Add(this.lblErrorInfo);
            this.pnlBody.Controls.Add(this.lblUserName);
            this.pnlBody.Controls.Add(this.lblPassWord);
            this.pnlBody.Controls.Add(this.txtUserName);
            this.pnlBody.Controls.Add(this.txtPassWord);
            this.pnlBody.CornerRadius = 0;
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.ForeColor = System.Drawing.SystemColors.ControlText;
            this.pnlBody.Location = new System.Drawing.Point(3, 27);
            this.pnlBody.MinimumSize = new System.Drawing.Size(22, 22);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Size = new System.Drawing.Size(365, 197);
            this.pnlBody.TabIndex = 1;
            // 
            // txtUserName
            // 
            this.txtUserName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtUserName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtUserName.BackColor = System.Drawing.Color.Transparent;
            this.txtUserName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(168)))), ((int)(((byte)(192)))));
            this.txtUserName.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtUserName.DisableIME = false;
            this.txtUserName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtUserName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtUserName.HeightLightBolorColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(67)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.txtUserName.Image = null;
            this.txtUserName.ImageSize = new System.Drawing.Size(0, 0);
            this.txtUserName.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtUserName.Location = new System.Drawing.Point(109, 29);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Padding = new System.Windows.Forms.Padding(2);
            this.txtUserName.PasswordChar = '\0';
            this.txtUserName.Required = false;
            this.txtUserName.Size = new System.Drawing.Size(202, 25);
            this.txtUserName.TabIndex = 1;
            this.txtUserName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtUserName.ToolTipIcon = System.Windows.Forms.ToolTipIcon.None;
            // 
            // txtPassWord
            // 
            this.txtPassWord.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtPassWord.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtPassWord.BackColor = System.Drawing.Color.Transparent;
            this.txtPassWord.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(168)))), ((int)(((byte)(192)))));
            this.txtPassWord.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtPassWord.DisableIME = false;
            this.txtPassWord.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPassWord.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtPassWord.HeightLightBolorColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(67)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.txtPassWord.Image = null;
            this.txtPassWord.ImageSize = new System.Drawing.Size(0, 0);
            this.txtPassWord.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtPassWord.Location = new System.Drawing.Point(109, 70);
            this.txtPassWord.Name = "txtPassWord";
            this.txtPassWord.Padding = new System.Windows.Forms.Padding(2);
            this.txtPassWord.PasswordChar = '●';
            this.txtPassWord.Required = false;
            this.txtPassWord.ShortcutsEnabled = false;
            this.txtPassWord.Size = new System.Drawing.Size(202, 25);
            this.txtPassWord.TabIndex = 3;
            this.txtPassWord.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtPassWord.ToolTipIcon = System.Windows.Forms.ToolTipIcon.None;
            this.txtPassWord.UseSystemPasswordChar = true;
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(371, 227);
            this.Controls.Add(this.pnlBody);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MaximizeBox = false;
            this.Name = "FrmLogin";
            this.ResizeEnable = false;
            this.Text = "登 录";
            this.pnlBody.ResumeLayout(false);
            this.pnlBody.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Label lblErrorInfo;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblPassWord;
        private TX.Framework.WindowUI.Controls.TXPanel pnlBody;
        private TX.Framework.WindowUI.Controls.TXTextBox txtUserName;
        private TX.Framework.WindowUI.Controls.TXTextBox txtPassWord;
    }
}