using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Windows.Forms;
using TX.Framework.WindowUI.Controls;

namespace TX.Framework.WindowUI.Forms {
    internal partial class TXMessageBox : BaseForm {
        #region fileds

        private readonly int _MaxWidth = 600;

        private readonly int _MaxHeight = 400;

        private string _CaptionText;

        private string _Message;

        private EnumMessageBox _MessageMode;

        private bool _playSound;
        #endregion

        public TXMessageBox() {
            InitializeComponent();
            //this.ShowInTaskbar = false;
            this.ResizeEnable = false;
            this.TopMost = true;
            this._CaptionText = "天下酒店网";
            this._Message = "天天开心，身体健康！";
            this._MessageMode = EnumMessageBox.Info;
            this.MaximumSize = new Size(this._MaxWidth, this._MaxHeight);
            ControlHelper.BindMouseMoveEvent(this.labMessage);
            ControlHelper.BindMouseMoveEvent(this.panel1);
            //this.StartPosition = FormStartPosition.CenterParent;
            //this.StartPosition = FormStartPosition.Manual;
            //this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width / 2 - this.Width / 2, Screen.PrimaryScreen.WorkingArea.Height / 2 - this.Height / 2);
        }

        public TXMessageBox(string captionText, string message, EnumMessageBox messageBoxMode, bool playSound) : this() {
            this._CaptionText = captionText;
            this._MessageMode = messageBoxMode;
            if (messageBoxMode == EnumMessageBox.Error) {
                this.CapitionLogo = Properties.Resources.logo3;
            }

            this._playSound = playSound;
            this._Message = message;
            this.ResetSize();
        }

        private void BtnOK_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.OK;
        }

        private void BtnCancel_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.Cancel;
        }

        private void TXMessageBox_Load(object sender, EventArgs e) {
            this.BindData();
        }

        private void BindData() {
            this.Text = this._CaptionText;
            this.labMessage.Text = this._Message;
            switch (this._MessageMode) {
                case EnumMessageBox.Info:
                    this.pbImage.Image = Properties.Resources.info;
                    btnCancel.Visible = false;
                    btnOK.Location = new Point((this.Width - btnOK.Width) / 2, btnOK.Location.Y);
                    this.PlaySound(Properties.Resources.Music_Info);
                    break;
                case EnumMessageBox.Question:
                    this.pbImage.Image = Properties.Resources.question;
                    this.PlaySound(Properties.Resources.Music_Question);
                    break;
                case EnumMessageBox.Warning:
                    this.pbImage.Image = Properties.Resources.warning;
                    btnCancel.Visible = false;
                    btnOK.Location = new Point((this.Width - btnOK.Width) / 2, btnOK.Location.Y);
                    this.PlaySound(Properties.Resources.Music_Warning);
                    break;
                case EnumMessageBox.Error:
                    this.pbImage.Image = Properties.Resources.error;
                    btnCancel.Visible = false;
                    btnOK.Location = new Point((this.Width - btnOK.Width) / 2, btnOK.Location.Y);
                    this.PlaySound(Properties.Resources.Music_Error);
                    break;
            }
        }

        #region //播放声音

        private void PlaySound(Stream fileStream) {
            if (_playSound) {
                using (SoundPlayer sp = new SoundPlayer()) {
                    sp.Stream = fileStream;
                    sp.Play();
                }
            }
        }
        #endregion

        private void ResetSize() {
            Size labSize = labMessage.Size;
            Size minSize = TextRenderer.MeasureText("鄺", this.Font);
            int maxLineCount = labSize.Height / minSize.Height;
            int maxWordCount = (labSize.Width * labSize.Height) / (minSize.Width * minSize.Width);
            using (Graphics g = labMessage.CreateGraphics()) {
                int wordCount, lineCount;
                g.MeasureString(this._Message, this.Font,
                    new SizeF(labSize.Width, this._MaxHeight),
                    StringFormat.GenericDefault, out wordCount, out lineCount);

                if (lineCount > maxLineCount) {
                    this.Size = new SizeF(this.Size.Width, this.Size.Height + minSize.Height * (lineCount - maxLineCount)).ToSize();
                }

                if (wordCount > maxWordCount) {
                    float rate = this._Message.Length / maxWordCount;
                    rate = 1 + (rate - 1) / 8;
                    this.Size = new SizeF(this.Size.Width * rate, this.Size.Height * rate).ToSize();
                }
            }
        }
    }
}
