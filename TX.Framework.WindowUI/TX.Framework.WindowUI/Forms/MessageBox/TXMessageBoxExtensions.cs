using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TX.Framework.WindowUI.Forms {

    /// <summary>
    /// MessageBox的扩展类
    /// </summary>
    /// User:Ryan  CreateTime:2011-08-10 10:56.
    public class TXMessageBoxExtensions {
        #region MessageBox

        public static DialogResult Info(string captionText, string message) {
            return ShowMessageBox(captionText, message, EnumMessageBox.Info, false);
        }

        public static DialogResult Info(string captionText, string message, bool playSound) {
            return ShowMessageBox(captionText, message, EnumMessageBox.Info, playSound);
        }

        public static DialogResult Info(string message) {
            return Info("提示信息", message);
        }

        public static DialogResult Error(string captionText, string message) {
            return ShowMessageBox(captionText, message, EnumMessageBox.Error, false);
        }

        public static DialogResult Error(string captionText, string message, bool playSound) {
            return ShowMessageBox(captionText, message, EnumMessageBox.Error, playSound);
        }

        public static DialogResult Error(string message) {
            return Error("错误信息", message);
        }

        public static DialogResult Question(string captionText, string message) {
            return ShowMessageBox(captionText, message, EnumMessageBox.Question, false);
        }

        public static DialogResult Question(string captionText, string message, bool playSound) {
            return ShowMessageBox(captionText, message, EnumMessageBox.Question, playSound);
        }

        public static DialogResult Question(string message) {
            return Question("询问信息", message);
        }

        public static DialogResult Warning(string captionText, string message) {
            return ShowMessageBox(captionText, message, EnumMessageBox.Warning, false);
        }

        public static DialogResult Warning(string captionText, string message, bool playSound) {
            return ShowMessageBox(captionText, message, EnumMessageBox.Warning, playSound);
        }

        public static DialogResult Warning(string message) {
            return Warning("警告信息", message);
        }

        private static DialogResult ShowMessageBox(string captionText, string message, EnumMessageBox infoType, bool playSound) {
            TXMessageBox frm = new TXMessageBox(captionText, message, infoType, playSound);
            DialogResult result = frm.ShowDialog();
            frm.Dispose();
            return result;
        }
        #endregion
    }
}
