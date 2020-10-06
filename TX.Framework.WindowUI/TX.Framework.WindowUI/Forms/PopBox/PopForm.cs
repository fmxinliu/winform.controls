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

namespace TX.Framework.WindowUI.Forms {
    public partial class PopForm : BaseForm {
        private SoundPlayer _player;
        private bool _playSound;

        public PopForm() {
            InitializeComponent();
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Opacity = 0.95f;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.CapitionLogo = Properties.Resources.naruto;
            this.CloseFormTimeLag = 1000;
            this.ShowFormTimelag = 500;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width, Screen.PrimaryScreen.WorkingArea.Height - this.Height);
        }

        public PopForm(bool playSound) : this() {
            this._playSound = playSound;
        }

        /// <summary>
        ///  关闭窗口的动画时间(CloseFormTimeLag.)
        /// </summary>
        /// <value>CloseFormTimeLag</value>
        /// User:Ryan  CreateTime:2012-11-26 11:00.
        public int CloseFormTimeLag { get; set; }

        /// <summary>
        ///  打开窗口的动画时间(ShowFormTimelag.)
        /// </summary>
        /// <value>ShowFormTimelag</value>
        /// User:Ryan  CreateTime:2012-11-26 11:00.
        public int ShowFormTimelag { get; set; }

        protected override void OnFormClosing(FormClosingEventArgs e) {
            Win32.AnimateWindow(this.Handle, this.CloseFormTimeLag, (int) AnimateWindowFlag.AW_BLEND | (int) AnimateWindowFlag.AW_HIDE);
            base.OnFormClosing(e);
        }

        protected override void OnFormClosed(FormClosedEventArgs e) {
            if (this._player != null) {
                this._player.Stop();
                this._player.Dispose();
            }
            base.OnFormClosed(e);
        }

        protected override void OnLoad(EventArgs e) {
            Win32.AnimateWindow(this.Handle, this.ShowFormTimelag, (int) EnumShowWindowMode.BottomToTop);
            PlaySound(Properties.Resources.Music_Question);
            base.OnLoad(e);
        }

        #region //播放声音

        /// <summary>
        /// 播放声音.
        /// </summary>
        /// <param name="fileStream">The file stream.</param>
        /// User:Ryan  CreateTime:2012-11-26 11:00.
        protected void PlaySound(Stream fileStream) {
            if (_playSound && this._player == null) {
                this._player = new SoundPlayer();
                this._player.Stream = fileStream;
                this._player.Play();
            }
        }
        #endregion
    }
}
