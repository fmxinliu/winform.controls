using System;

namespace System.Runtime.InteropServices.APIs {
    public static class APIsUser32 {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern uint SendInput(uint nInputs, APIsStructs.INPUT[] inputs, int cbSize);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool ScreenToClient(IntPtr hWnd, ref APIsStructs.POINTAPI point);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int DrawFrameControl(IntPtr hDc, APIsStructs.RECT rect, APIsEnums.DrawFrameControlFlags nType, APIsEnums.DrawFrameControlStateFlags nState);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetSysColor(APIsEnums.SystemColors nIndex);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool DestroyIcon(IntPtr hIcon);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetComboBoxInfo(IntPtr hWnd, ref APIsStructs.PCOMBOBOXINFO cbi);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool DestroyWindow(IntPtr hWnd);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetDC(IntPtr hWnd);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetDesktopWindow();
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool ShowWindow(IntPtr hWnd, APIsEnums.ShowWindowStyles State);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool UpdateWindow(IntPtr hWnd);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int Width, int Height, uint flags);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool OpenClipboard(IntPtr hWndNewOwner);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool CloseClipboard();
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool EmptyClipboard();
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetClipboardData(uint Format, IntPtr hData);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetMenuItemRect(IntPtr hWnd, IntPtr hMenu, uint Item, ref APIsStructs.RECT rc);
        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern IntPtr GetParent(IntPtr hWnd);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, APIsEnums.HeaderControlMessages msg, int wParam, ref APIsStructs.HDITEM lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, APIsEnums.HeaderControlMessages msg, IntPtr wParam, ref APIsStructs.HD_HITTESTINFO hd_hittestinfo);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, APIsEnums.WindowMessages msg, int wParam, int lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, IntPtr lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern void SendMessage(IntPtr hWnd, int msg, IntPtr wParam, ref APIsStructs.NMHDR lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern void SendMessage(IntPtr hWnd, int msg, IntPtr wParam, ref APIsStructs.NMLVDISPINFO lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern void SendMessage(IntPtr hWnd, int msg, int wParam, ref APIsStructs.RECT lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, ref APIsStructs.POINTAPI lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr PostMessage(IntPtr hWnd, int msg, int wParam, int lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr PostMessage(IntPtr hWnd, APIsEnums.WindowMessages msg, int wParam, int lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetFocus(IntPtr hWnd);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int DrawText(IntPtr hdc, string lpString, int nCount, ref APIsStructs.RECT lpRect, APIsEnums.DrawTextFormatFlags flags);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetParent(IntPtr hChild, IntPtr hParent);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetDlgItem(IntPtr hDlg, int nControlID);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClientRect(IntPtr hWnd, ref APIsStructs.RECT rc);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int InvalidateRect(IntPtr hWnd, ref APIsStructs.RECT rc, int bErase);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int InvalidateRect(IntPtr hWnd, IntPtr rc, int bErase);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool WaitMessage();
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool PeekMessage(ref APIsStructs.MSG msg, int hWnd, uint wFilterMin, uint wFilterMax, APIsEnums.PeekMessageFlags flags);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetMessage(ref APIsStructs.MSG msg, int hWnd, uint wFilterMin, uint wFilterMax);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool TranslateMessage(ref APIsStructs.MSG msg);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool DispatchMessage(ref APIsStructs.MSG msg);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr LoadCursor(IntPtr hInstance, APIsEnums.CursorTypes cursor);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetCursor(IntPtr hCursor);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetFocus();
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetCapture(IntPtr hWnd);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr BeginPaint(IntPtr hWnd, ref APIsStructs.PAINTSTRUCT ps);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool EndPaint(IntPtr hWnd, ref APIsStructs.PAINTSTRUCT ps);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref APIsStructs.POINTAPI pptDst, ref APIsStructs.SIZE psize, IntPtr hdcSrc, ref APIsStructs.POINTAPI pprSrc, Int32 crKey, ref APIsStructs.BLENDFUNCTION pblend, APIsEnums.UpdateLayeredWindowFlags dwFlags);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetWindowRect(IntPtr hWnd, ref APIsStructs.RECT rect);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool ClientToScreen(IntPtr hWnd, ref APIsStructs.POINTAPI pt);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool TrackMouseEvent(ref APIsStructs.TRACKMOUSEEVENTS tme);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool redraw);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern ushort GetKeyState(int virtKey);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool MoveWindow(IntPtr hWnd, int x, int y, int width, int height, bool repaint);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassName(IntPtr hWnd, System.Text.StringBuilder ClassName, int nMaxCount);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetDCEx(IntPtr hWnd, IntPtr hRegion, uint flags);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int FillRect(IntPtr hDC, ref APIsStructs.RECT rect, IntPtr hBrush);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowPlacement(IntPtr hWnd, ref APIsStructs.WINDOWPLACEMENT wp);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SetWindowText(IntPtr hWnd, string text);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowText(IntPtr hWnd, System.Text.StringBuilder text, int maxCount);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetClipboardViewer(IntPtr hWndNewViewer);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetSystemMetrics(APIsEnums.SystemMetricsCodes code);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SetScrollInfo(IntPtr hwnd, int bar, ref APIsStructs.SCROLLINFO si, int fRedraw);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int ShowScrollBar(IntPtr hWnd, int bar, int show);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int EnableScrollBar(IntPtr hWnd, uint flags, uint arrows);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int BringWindowToTop(IntPtr hWnd);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetScrollInfo(IntPtr hwnd, int bar, ref APIsStructs.SCROLLINFO si);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int ScrollWindowEx(IntPtr hWnd, int dx, int dy, ref APIsStructs.RECT rcScroll, ref APIsStructs.RECT rcClip, IntPtr UpdateRegion, ref APIsStructs.RECT rcInvalidated, uint flags);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool IsWindow(IntPtr hWnd);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int LockWindowUpdate(IntPtr hWnd);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool ValidateRect(IntPtr hWnd, ref APIsStructs.RECT rcInvalidated);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool ValidateRect(IntPtr hWnd, IntPtr rc);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetScrollBarInfo(IntPtr hWnd, APIsEnums.SystemObjects id, ref APIsStructs.SCROLLBARINFO sbi);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SetProp(IntPtr hWnd, IntPtr atom, IntPtr hData);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int CallWindowProc(IntPtr hOldProc, IntPtr hWnd, uint message, int wParam, int lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool SendMessage(IntPtr hWnd, APIsEnums.ListViewMessages msg, Int32 wParam, ref APIsStructs.LV_ITEM lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, Int32 msg, Int32 wParam, ref APIsStructs.LVHITTESTINFO lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool SendMessage(IntPtr hWnd, int msg, IntPtr wParam, ref IntPtr lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool FlashWindow(IntPtr hWnd, bool bInvert);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool FlashWindowEx(ref APIsStructs.FLASHWINFO pwfi);
    }
}
