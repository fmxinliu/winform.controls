#region COPYRIGHT
//
//     THIS IS GENERATED BY TEMPLATE
//     
//     AUTHOR  :     ROYE
//     DATE       :     2010
//
//     COPYRIGHT (C) 2010, TIANXIAHOTEL TECHNOLOGIES CO., LTD. ALL RIGHTS RESERVED.
//
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Win32 {
    public partial class NativeMethods {
        public static int GetMajorVersion() {
            DLLVERSIONINFO2 pdvi = new DLLVERSIONINFO2();
            pdvi.info1.cbSize = Marshal.SizeOf(typeof(DLLVERSIONINFO2));
            DllGetVersion(ref pdvi);
            return pdvi.info1.dwMajorVersion;
        }

        [DllImport("Comctl32.dll", CharSet = CharSet.Auto)]
        private static extern int DllGetVersion(ref DLLVERSIONINFO2 pdvi);
    }
}