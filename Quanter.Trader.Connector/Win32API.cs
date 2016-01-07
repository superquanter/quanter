using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace Quanter.Trader.Connector
{
    public class Win32API
    {
        #region .ctor()
        // No need to construct this object
        private Win32API()
        {
        }
        #endregion

        #region CallBacks
        public delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);
        public delegate bool EnumWindowsProc(IntPtr hWnd, int lParam);
        public delegate bool EnumWindowsProc1(IntPtr hWnd, ref IntPtr lParam);
        #endregion

        #region Kernel32.dll functions
        [DllImport("kernel32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern int GetCurrentThreadId();
        [DllImport("kernel32.dll")]
        public static extern IntPtr LoadLibrary(string path);
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetProcAddress(IntPtr lib, string funcName);

        #endregion

        #region Gdi32.dll functions
        [DllImport("gdi32.dll")]
        static public extern bool StretchBlt(IntPtr hDCDest, int XOriginDest, int YOriginDest, int WidthDest, int HeightDest,
            IntPtr hDCSrc, int XOriginScr, int YOriginSrc, int WidthScr, int HeightScr, uint Rop);
        [DllImport("gdi32.dll")]
        static public extern IntPtr CreateCompatibleDC(IntPtr hDC);
        [DllImport("gdi32.dll")]
        static public extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int Width, int Heigth);
        [DllImport("gdi32.dll")]
        static public extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);
        [DllImport("gdi32.dll")]
        static public extern bool BitBlt(IntPtr hDCDest, int XOriginDest, int YOriginDest, int WidthDest, int HeightDest,
            IntPtr hDCSrc, int XOriginScr, int YOriginSrc, uint Rop);
        [DllImport("gdi32.dll")]
        static public extern IntPtr DeleteDC(IntPtr hDC);
        [DllImport("gdi32.dll")]
        static public extern bool PatBlt(IntPtr hDC, int XLeft, int YLeft, int Width, int Height, uint Rop);
        [DllImport("gdi32.dll")]
        static public extern bool DeleteObject(IntPtr hObject);
        [DllImport("gdi32.dll")]
        static public extern uint GetPixel(IntPtr hDC, int XPos, int YPos);
        [DllImport("gdi32.dll")]
        static public extern int SetMapMode(IntPtr hDC, int fnMapMode);
        [DllImport("gdi32.dll")]
        static public extern int GetObjectType(IntPtr handle);
        [DllImport("gdi32")]
        public static extern IntPtr CreateDIBSection(IntPtr hdc, ref BITMAPINFO_FLAT bmi,
            int iUsage, ref int ppvBits, IntPtr hSection, int dwOffset);
        [DllImport("gdi32")]
        public static extern int GetDIBits(IntPtr hDC, IntPtr hbm, int StartScan, int ScanLines, int lpBits, BITMAPINFOHEADER bmi, int usage);
        [DllImport("gdi32")]
        public static extern int GetDIBits(IntPtr hdc, IntPtr hbm, int StartScan, int ScanLines, int lpBits, ref BITMAPINFO_FLAT bmi, int usage);

        [DllImport("kernel32.dll")]
        public static extern uint GetTickCount();

        [DllImport("gdi32")]
        public static extern IntPtr GetPaletteEntries(IntPtr hpal, int iStartIndex, int nEntries, byte[] lppe);
        [DllImport("gdi32")]
        public static extern IntPtr GetSystemPaletteEntries(IntPtr hdc, int iStartIndex, int nEntries, byte[] lppe);
        [DllImport("gdi32")]
        public static extern uint SetDCBrushColor(IntPtr hdc, uint crColor);
        [DllImport("gdi32")]
        public static extern IntPtr CreateSolidBrush(uint crColor);
        [DllImport("gdi32")]
        public static extern int SetBkMode(IntPtr hDC, BackgroundMode mode);
        [DllImport("gdi32")]
        public static extern int SetViewportOrgEx(IntPtr hdc, int x, int y, int param);
        [DllImport("gdi32")]
        public static extern uint SetTextColor(IntPtr hDC, uint colorRef);
        [DllImport("gdi32")]
        public static extern int SetStretchBltMode(IntPtr hDC, int StrechMode);
        #endregion

        #region Uxtheme.dll functions
        [DllImport("uxtheme.dll")]
        static public extern int SetWindowTheme(IntPtr hWnd, string AppID, string ClassID);
        #endregion

        #region User32.dll functions
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("User32.dll", EntryPoint = "FindWindowEx")]
        public static extern IntPtr FindWindowEx(IntPtr hWnd, IntPtr hwndChildAfter, string lpClassName, string lpWindowName);
        [DllImport("User32.dll", EntryPoint = "EnumWindows")]
        public static extern IntPtr EnumWindows(EnumWindowsProc ewp, int lParam);
        [DllImport("User32.dll", EntryPoint = "EnumWindows")]
        public static extern IntPtr EnumWindows(EnumWindowsProc1 ewp, ref IntPtr lParam);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumChildWindows(IntPtr hwndParent, EnumWindowsProc lpEnumFunc, IntPtr lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetDC(IntPtr hWnd);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static public extern IntPtr GetDesktopWindow();
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static public extern bool ShowWindow(IntPtr hWnd, short State);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static public extern bool UpdateWindow(IntPtr hWnd);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static public extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static public extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int Width, int Height, uint flags);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static public extern bool OpenClipboard(IntPtr hWndNewOwner);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static public extern IntPtr GetClipboardData(int p);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static public extern bool CloseClipboard();
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static public extern bool EmptyClipboard();
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static public extern IntPtr SetClipboardData(uint Format, IntPtr hData);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static public extern bool GetMenuItemRect(IntPtr hWnd, IntPtr hMenu, uint Item, ref RECT rc);
        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern IntPtr GetParent(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "keybd_event")]
        public static extern void keybd_event(
            byte bVk,    //虚拟键值
            byte bScan,// 一般为0
            int dwFlags,  //这里是整数类型  0 为按下，2为释放
            int dwExtraInfo  //这里是整数类型 一般情况下设成为 0
        );
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowThreadProcessId(IntPtr hwnd, out int ID);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, byte[] lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, StringBuilder lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, string lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, IntPtr lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern void SendMessage(IntPtr hWnd, int msg, int wParam, ref RECT lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, ref POINT lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern void SendMessage(IntPtr hWnd, int msg, int wParam, ref TBBUTTON lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern void SendMessage(IntPtr hWnd, int msg, int wParam, ref TBBUTTONINFO lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, ref REBARBANDINFO lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern void SendMessage(IntPtr hWnd, int msg, int wParam, ref TVITEM lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern void SendMessage(IntPtr hWnd, int msg, int wParam, ref LVITEM lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern void SendMessage(IntPtr hWnd, int msg, int wParam, ref HDITEM lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern void SendMessage(IntPtr hWnd, int msg, int wParam, ref HD_HITTESTINFO hti);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr PostMessage(IntPtr hWnd, int msg, int wParam, int lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int PostMessage(IntPtr hWnd, int msg, int wParam, string lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int PostMessage(IntPtr hWnd, int msg, int wParam, ref POINT lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetWindowsHookEx(int hookid, HookProc pfnhook, IntPtr hinst, int threadid);
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhook);
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr CallNextHookEx(IntPtr hhook, int code, IntPtr wparam, IntPtr lparam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetFocus(IntPtr hWnd);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public extern static int DrawText(IntPtr hdc, string lpString, int nCount, ref RECT lpRect, int uFormat);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public extern static IntPtr SetParent(IntPtr hChild, IntPtr hParent);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public extern static IntPtr GetDlgItem(IntPtr hDlg, int nControlID);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public extern static int GetClientRect(IntPtr hWnd, ref RECT rc);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public extern static int InvalidateRect(IntPtr hWnd, IntPtr rect, int bErase);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool WaitMessage();
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool PeekMessage(ref MSG msg, int hWnd, uint wFilterMin, uint wFilterMax, uint wFlag);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetMessage(ref MSG msg, int hWnd, uint wFilterMin, uint wFilterMax);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool TranslateMessage(ref MSG msg);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool DispatchMessage(ref MSG msg);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr LoadCursor(IntPtr hInstance, uint cursor);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetCursor(IntPtr hCursor);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetFocus();
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool ReleaseCapture();
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr BeginPaint(IntPtr hWnd, ref PAINTSTRUCT ps);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool EndPaint(IntPtr hWnd, ref PAINTSTRUCT ps);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref POINT pptDst, ref SIZE psize, IntPtr hdcSrc, ref POINT pprSrc, Int32 crKey, ref BLENDFUNCTION pblend, Int32 dwFlags);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetWindowRect(IntPtr hWnd, ref RECT rect);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool ClientToScreen(IntPtr hWnd, ref POINT pt);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool TrackMouseEvent(ref TRACKMOUSEEVENTS tme);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool redraw);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern ushort GetKeyState(int virtKey);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool MoveWindow(IntPtr hWnd, int x, int y, int width, int height, bool repaint);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassName(IntPtr hWnd, out STRINGBUFFER ClassName, int nMaxCount);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetDCEx(IntPtr hWnd, IntPtr hRegion, uint flags);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int FillRect(IntPtr hDC, ref RECT rect, IntPtr hBrush);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT wp);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int SetWindowText(IntPtr hWnd, string text);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowText(IntPtr hWnd, out STRINGBUFFER text, int maxCount);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static public extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendDlgItemMessage(IntPtr hPanel, int id, int Msg, int wParam, string lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static public extern IntPtr SetClipboardViewer(IntPtr hWndNewViewer);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static public extern int ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static public extern int GetSystemMetrics(int nIndex);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static public extern int SetScrollInfo(IntPtr hwnd, int bar, ref SCROLLINFO si, int fRedraw);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int ShowScrollBar(IntPtr hWnd, int bar, int show);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int EnableScrollBar(IntPtr hWnd, uint flags, uint arrows);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int BringWindowToTop(IntPtr hWnd);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetScrollInfo(IntPtr hwnd, int bar, ref SCROLLINFO si);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static public extern int ScrollWindowEx(IntPtr hWnd, int dx, int dy,
            ref RECT rcScroll, ref RECT rcClip, IntPtr UpdateRegion, ref RECT rcInvalidated, uint flags);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int IsWindow(IntPtr hWnd);
        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern int GetKeyboardState(byte[] pbKeyState);
        [DllImport("user32")]
        public static extern int ToAscii(int uVirtKey, //[in] Specifies the virtual-key code to be translated. 
            int uScanCode, // [in] Specifies the hardware scan code of the key to be translated. The high-order bit of this value is set if the key is up (not pressed). 
            byte[] lpbKeyState, // [in] Pointer to a 256-byte array that contains the current keyboard state. Each element (byte) in the array contains the state of one key. If the high-order bit of a byte is set, the key is down (pressed). The low bit, if set, indicates that the key is toggled on. In this function, only the toggle bit of the CAPS LOCK key is relevant. The toggle state of the NUM LOCK and SCROLL LOCK keys is ignored.
            byte[] lpwTransKey, // [out] Pointer to the buffer that receives the translated character or characters. 
            int fuState); // [in] Specifies whether a menu is active. This parameter must be 1 if a menu is active, or 0 otherwise.
        #endregion

        #region Common Controls functions
        [DllImport("comctl32.dll")]
        public static extern bool InitCommonControlsEx(INITCOMMONCONTROLSEX icc);
        [DllImport("comctl32.dll")]
        public static extern bool InitCommonControls();
        [DllImport("comctl32.dll", EntryPoint = "DllGetVersion")]
        public extern static int GetCommonControlDLLVersion(ref DLLVERSIONINFO dvi);
        [DllImport("comctl32.dll")]
        public static extern IntPtr ImageList_Create(int width, int height, uint flags, int count, int grow);
        [DllImport("comctl32.dll")]
        public static extern bool ImageList_Destroy(IntPtr handle);
        [DllImport("comctl32.dll")]
        public static extern int ImageList_Add(IntPtr imageHandle, IntPtr hBitmap, IntPtr hMask);
        [DllImport("comctl32.dll")]
        public static extern bool ImageList_Remove(IntPtr imageHandle, int index);
        [DllImport("comctl32.dll")]
        public static extern bool ImageList_BeginDrag(IntPtr imageHandle, int imageIndex, int xHotSpot, int yHotSpot);
        [DllImport("comctl32.dll")]
        public static extern bool ImageList_DragEnter(IntPtr hWndLock, int x, int y);
        [DllImport("comctl32.dll")]
        public static extern bool ImageList_DragMove(int x, int y);
        [DllImport("comctl32.dll")]
        public static extern bool ImageList_DragLeave(IntPtr hWndLock);
        [DllImport("comctl32.dll")]
        public static extern void ImageList_EndDrag();
        #endregion

        #region Win32 Macro-Like helpers
        public static int GET_X_LPARAM(int lParam)
        {
            return (lParam & 0xffff);
        }


        public static int GET_Y_LPARAM(int lParam)
        {
            return (lParam >> 16);
        }

        public static Point GetPointFromLPARAM(int lParam)
        {
            return new Point(GET_X_LPARAM(lParam), GET_Y_LPARAM(lParam));
        }

        public static int LOW_ORDER(int param)
        {
            return (param & 0xffff);
        }

        public static int HIGH_ORDER(int param)
        {
            return (param >> 16);
        }

        #endregion
    }
    }