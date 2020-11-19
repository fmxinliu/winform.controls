using System;
using System.Runtime.InteropServices;

namespace TX.Framework.WindowUI {
    public class TaskBarHelper {
        private struct RECT {
            public int left, top, right, bottom;
        }

        /// <summary>
        /// 清理僵尸图标
        /// </summary>
        public static void RefreshNotification() {
            var NotifyAreaHandle = GetNotifyAreaHandle();
            if (NotifyAreaHandle != IntPtr.Zero) {
                RefreshWindow(NotifyAreaHandle);
            }

            var NotifyOverHandle = GetNotifyOverHandle();
            if (NotifyOverHandle != IntPtr.Zero) {
                RefreshWindow(NotifyOverHandle);
            }
        }

        private static void RefreshWindow(IntPtr windowHandle) {
            const uint WM_MOUSEMOVE = 0x0200;
            RECT rect;
            GetClientRect(windowHandle, out rect);
            for (var x = 0; x < rect.right; x += 5) {
                for (var y = 0; y < rect.bottom; y += 5) {
                    SendMessage(windowHandle, WM_MOUSEMOVE, 0, (y << 16) + x);
                }
            }
        }

        private static IntPtr GetNotifyAreaHandle() {
            var hTrayWnd = FindWindowEx(IntPtr.Zero, IntPtr.Zero, "Shell_TrayWnd", string.Empty);
            var hTrayNotifyWnd = FindWindowEx(hTrayWnd, IntPtr.Zero, "TrayNotifyWnd", string.Empty);
            var hSysPager = FindWindowEx(hTrayNotifyWnd, IntPtr.Zero, "SysPager", string.Empty);
            var hToolBar32 = FindWindowEx(hSysPager, IntPtr.Zero, "ToolbarWindow32", string.Empty);
            if (hToolBar32 == IntPtr.Zero) {
                hToolBar32 = FindWindowEx(hSysPager, IntPtr.Zero, "ToolbarWindow32", "用户升级的通知区域");
            }
            return hToolBar32;
        }

        private static IntPtr GetNotifyOverHandle() {
            var hNotifyOver = FindWindowEx(IntPtr.Zero, IntPtr.Zero, "NotifyIconOverflowWindow", string.Empty);
            var hToolBar32 = FindWindowEx(hNotifyOver, IntPtr.Zero, "ToolbarWindow32", string.Empty);
            if (hToolBar32 == IntPtr.Zero) {
                hToolBar32 = FindWindowEx(hNotifyOver, IntPtr.Zero, "ToolbarWindow32", "溢出通知区域");
            }
            return hToolBar32;
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, string windowTitle);
        [DllImport("user32.dll")]
        private static extern bool GetClientRect(IntPtr handle, out RECT rect);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr handle, UInt32 message, Int32 wParam, Int32 lParam);
    }
}