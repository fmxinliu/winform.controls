using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace TX.Framework.WindowUI {
    public static class SystemIcon {
        #region 获取系统图标
        public static Icon GetIconByFileName(string fileName, bool isLargeIcon) {
            return Win32.GetFileIcon(fileName, isLargeIcon);
        }

        public static Icon GetIconByFileType(string fileType, bool isLargeIcon) {
            return Win32.ExtractIcon(fileType, isLargeIcon);
        }

        public static Icon GetIconByIndex(int index, bool isLargeIcon) {
            return Win32.ExtractIcon(index, isLargeIcon);
        }

        public static List<Icon> GetAllIcons(bool isLargeIcon) {
            return Win32.ExtractIcon(isLargeIcon);
        }
        #endregion

        #region 保存图标
        public static void Save(Icon icon, string path) {
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate)) {
                icon.Save(fs);
            }
        }

        public static void SaveToImage(Icon icon, string path) {
            icon.ToBitmap().Save(path);
        }

        public static void SaveByIndex(int index, bool isLargeIcon, string path) {
            Icon icon = GetIconByIndex(index, isLargeIcon);
            if (icon != null) {
                Save(icon, path);
            }
        }

        public static void SaveByFileType(string fileType, bool isLargeIcon, string path) {
            Icon icon = GetIconByFileType(fileType, isLargeIcon);
            if (icon != null) {
                Save(icon, path);
            }
        }

        public static void SaveByFileName(string fileName, bool isLargeIcon, string path) {
            Icon icon = GetIconByFileName(fileName, isLargeIcon);
            if (icon != null) {
                Save(icon, path);
            }
        }

        public static void SaveAllIcon(bool isLargeIcon, string path) {
            if (!Directory.Exists(path)) {
                Directory.CreateDirectory(path);
                if (!path.EndsWith(@"\")) {
                    path += @"\";
                }
            }
            List<Icon> icons = GetAllIcons(isLargeIcon);
            for (int i = 0; i < icons.Count; ++i) {
                Save(icons[i], path + i + ".ico");
            }
        }
        #endregion

        #region 内部类
        private static class Win32 {
            public const uint SHGFI_ICON = 0x100;
            public const uint SHGFI_LARGEICON = 0x0; // 大图标
            public const uint SHGFI_SMALLICON = 0x1; // 小图标
            public const uint SHGFI_USEFILEATTRIBUTES = 0x10;

            [DllImport("shell32.dll")]
            private static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);
            [DllImport("shell32.dll")]
            private static extern uint ExtractIconEx(string lpszFile, int nIconIndex, int[] phiconLarge, int[] phiconSmall, uint nIcons);
            [DllImport("User32.dll")]
            private static extern int DestroyIcon(IntPtr hIcon);

            [StructLayout(LayoutKind.Sequential)]
            private struct SHFILEINFO {
                public IntPtr hIcon;         // 文件的图标句柄
                public IntPtr iIcon;         // 图标的系统索引号
                public uint dwAttributes;    // 文件的属性值
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
                public string szDisplayName; // 文件的显示名
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
                public string szTypeName;    // 文件的类型名
            }

            /// <summary>
            /// 获取文件关联图标
            /// </summary>
            public static Icon GetFileIcon(string fileName, bool isLargeIcon) {
                SHFILEINFO shinfo = new SHFILEINFO();
                // Icon types
                uint uFlags = isLargeIcon ? SHGFI_LARGEICON : SHGFI_SMALLICON;
                //Use this to get the icon
                IntPtr hi = SHGetFileInfo(fileName, 0, ref shinfo, (uint)Marshal.SizeOf(shinfo), SHGFI_USEFILEATTRIBUTES | SHGFI_ICON | uFlags);
                if (hi == IntPtr.Zero) {
                    return null;
                }
                //The icon is returned in the hIcon member of the shinfo struct
                Icon icon = Icon.FromHandle(shinfo.hIcon);
                return icon;
            }

            /// <summary>
            /// 读取注册表，获取文件类型注册图标
            /// </summary>
            public static Icon ExtractIcon(string fileType, bool isLargeIcon) {
                if (string.IsNullOrWhiteSpace(fileType)) {
                    return null;
                }

                RegistryKey regVersion = null;
                string regFileType = null;
                string regIconString = null;
                string systemDirectory = Environment.SystemDirectory + "\\";

                if (fileType.Trim().StartsWith(".")) {
                    // 读系统注册表中文件类型信息
                    try {
                        regVersion = Registry.ClassesRoot.OpenSubKey(fileType);
                        if (regVersion != null) {
                            regFileType = regVersion.GetValue("") as string;
                            regVersion.Close();
                            regVersion = Registry.ClassesRoot.OpenSubKey(regFileType + @"\DefaultIcon");
                            if (regVersion != null) {
                                regIconString = regVersion.GetValue("") as string;
                                regVersion.Close();
                            }
                        }
                    }
                    catch {
                        if (regVersion != null) {
                            regVersion.Close();
                        }
                    }
                    finally {
                        if (regIconString == null) {
                            // 没有读取到文件类型注册信息，指定为未知文件类型的图标
                            regIconString = systemDirectory + "shell32.dll,0";
                        }
                    }
                }
                else {
                    // 直接指定为文件夹图标
                    regIconString = systemDirectory + "shell32.dll,3";
                }

                string[] fileIcon = regIconString.Split(new char[] { ',' });
                if (fileIcon.Length != 2) {
                    // 系统注册表中注册的标图不能直接提取，则返回可执行文件的通用图标
                    fileIcon = new string[] { systemDirectory + "shell32.dll", "2" };
                }

                // 调用API方法读取图标
                int[] phiconLarge = new int[1];
                int[] phiconSmall = new int[1];
                uint count = ExtractIconEx(fileIcon[0], Int32.Parse(fileIcon[1]), phiconLarge, phiconSmall, 1);
                if (count != 2) {
                    return null;
                }
                IntPtr hIcon = new IntPtr(isLargeIcon ? phiconLarge[0] : phiconSmall[0]);
                Icon icon = Icon.FromHandle(hIcon);
                return icon;
            }

            /// <summary>
            /// 根据图标索引，从shell32.dll中获取图标
            /// </summary>
            public static Icon ExtractIcon(int index, bool isLargeIcon) {
                int[] phiconLarge = new int[1];
                int[] phiconSmall = new int[1];
                uint count = ExtractIconEx("shell32.dll", index, phiconLarge, phiconSmall, 1);
                if (count != 2) {
                    return null;
                }
                IntPtr hIcon = new IntPtr(isLargeIcon ? phiconLarge[0] : phiconSmall[0]);
                Icon icon = Icon.FromHandle(hIcon);
                return icon;
            }

            /// <summary>
            /// 获取 shell32.dll 中所有图标
            /// </summary>
            public static List<Icon> ExtractIcon(bool isLargeIcon) {
                List<Icon> icons = new List<Icon>();

                int[] phiconLarge = new int[1000];
                int[] phiconSmall = new int[1000];
                uint count = ExtractIconEx("shell32.dll", 0, phiconLarge, phiconSmall, 1000);

                for (int i = 0; i < count; i++) {
                    IntPtr hIcon = new IntPtr(isLargeIcon ? phiconLarge[i] : phiconSmall[i]);
                    Icon icon = Icon.FromHandle(hIcon);
                    icons.Add(icon);
                }

                return icons;
            }
        }
        #endregion
    }
}
