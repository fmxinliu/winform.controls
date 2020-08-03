using System;

namespace System.Runtime.InteropServices.APIs {
    public class APIsUxTheme {
        [DllImport("UxTheme.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr OpenThemeData(
            IntPtr hWnd, string pszClassList);
        [DllImport("UxTheme.dll", CharSet = CharSet.Auto)]
        public static extern int CloseThemeData(IntPtr hTheme);
        [DllImport("UxTheme.dll", CharSet = CharSet.Auto)]
        public static extern bool IsThemeActive();
        [DllImport("UxTheme.dll", CharSet = CharSet.Auto)]
        public static extern bool IsAppThemed();
        [DllImport("UxTheme.dll", CharSet = CharSet.Auto)]
        public static extern int DrawThemeBackground(
            IntPtr hWnd, IntPtr hDc, int iPartId, int iStateId,
            APIsStructs.RECT pRect, APIsStructs.RECT pClipRect);
        [DllImport("UxTheme.dll", CharSet = CharSet.Auto)]
        public static extern int GetCurrentThemeName(
            Text.StringBuilder pszThemeFileName, int dwMaxNameChars,
            Text.StringBuilder pszColorBuff, int cchMaxColorChars,
            Text.StringBuilder pszSizeBuff, int cchMaxSizeChars);
    }
}
