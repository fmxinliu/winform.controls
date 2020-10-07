﻿namespace TX.Framework.WindowUI.Controls {
    partial class TXLights {
        /// <summary>
        /// 必需的设计器变量
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.twinkleTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            //
            // twinkleTimer
            //
            this.twinkleTimer.Tick += new System.EventHandler(this.TwinkleTimer_Tick);
            //
            // TXLights
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "TXLights";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.Size = new System.Drawing.Size(32, 32);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Light_Paint);
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Timer twinkleTimer;
    }
}