using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanter.Trader.Connector
{
    public class Win32Code
    {
        #region .ctor()
        // No need to construct this object
        private Win32Code()
        {
        }
        #endregion

        #region tree view 

        public const int TV_FIRST = 0x1100;
        public const int TVM_GETCOUNT = TV_FIRST + 5;
        public const int TVM_GETNEXTITEM = TV_FIRST + 10;
        public const int TVM_SELECTITEM = TV_FIRST + 11;
        public const int TVM_GETITEMA = TV_FIRST + 12;
        public const int TVM_GETITEMW = TV_FIRST + 62;

        public const int TVGN_ROOT = 0x0000;
        public const int TVGN_NEXT = 0x0001;
        public const int TVGN_PREVIOUS = 0x0002;
        public const int TVGN_PARENT = 0x0003;
        public const int TVGN_CHILD = 0x0004;
        public const int TVGN_FIRSTVISIBLE = 0x0005;
        public const int TVGN_NEXTVISIBLE = 0x0006;
        public const int TVGN_PREVIOUSVISIBLE = 0x0007;
        public const int TVGN_DROPHILITE = 0x0008;
        public const int TVGN_CARET = 0x0009;
        public const int TVGN_LASTVISIBLE = 0x000A;

        public const int TVIF_TEXT = 0x0001;
        public const int TVIF_IMAGE = 0x0002;
        public const int TVIF_PARAM = 0x0004;
        public const int TVIF_STATE = 0x0008;
        public const int TVIF_HANDLE = 0x0010;
        public const int TVIF_SELECTEDIMAGE = 0x0020;
        public const int TVIF_CHILDREN = 0x0040;
        public const int TVIF_INTEGRAL = 0x0080;

        #endregion

        public const string TOOLBARCLASSNAME = "ToolbarWindow32";
        public const string REBARCLASSNAME = "ReBarWindow32";
        public const string PROGRESSBARCLASSNAME = "msctls_progress32";
        public const string SCROLLBAR = "SCROLLBAR";

        public const int WM_SETFOCUS = 0x0007;
        public const int WM_KILLFOCUS = 0x0008;
        public const int WM_SETTEXT = 0x000C;
        public const int WM_GETTEXT = 0x000D;
        public const int WM_KEYDOWN = 0x0100;
        public const int WM_KEYUP = 0x0101;
        public const int WM_CHAR = 0x00102;
        public const int WM_SYSKEYDOWN = 0x0104;
        public const int WM_SYSKEYUP = 0x0105;
        public const int WM_SYSCHAR = 0x0106;


        public const int WM_COPY = 0x0301;
        public const int WM_RENDERFORMAT = 0x0305;
        public const int CF_UNICODETEXT = 13;
        public const int WM_COPYDATA = 0x004A;

        // MOUSE EVENT
        public const int WM_MOUSEMOVE = 0x0200;
        public const int WM_LBUTTONDOWN = 0x0201;
        public const int WM_LBUTTONUP = 0x0202;
        public const int WM_LBUTTONDBLCLK = 0x0203;
        public const int WM_RBUTTONDOWN = 0x0204;
        public const int WM_RBUTTONUP = 0x0205;
        public const int WM_RBUTTONDBLCLK = 0x0206;
        public const int WM_MBUTTONDOWN = 0x0207;
        public const int WM_MBUTTONUP = 0x0208;
        public const int WM_MBUTTONDBLCLK = 0x0209;
        public const int WM_MOUSEWHEEL = 0x020A;

        #region 256 virtual key
        public const int VK_LBUTTON = 0x1;
        public const int VK_RBUTTON = 0x2;
        public const int VK_CANCEL = 0x3;
        public const int VK_MBUTTON = 0x4;
        public const int VK_BACK = 0x8;
        public const int VK_TAB = 0x9;
        public const int VK_CLEAR = 0xC;
        public const int VK_RETURN = 0xD;
        public const int VK_SHIFT = 0x10;
        public const int VK_CONTROL = 0x11;
        public const int VK_MENU = 0x12;
        public const int VK_ALT = 0X12;
        public const int VK_PAUSE = 0x13;
        public const int VK_CAPITAL = 0x14;
        public const int VK_ESCAPE = 0x1B;
        public const int VK_SPACE = 0x20;
        public const int VK_PRIOR = 0x21;
        public const int VK_NEXT = 0x22;
        public const int VK_END = 0x23;
        public const int VK_HOME = 0x24;
        public const int VK_LEFT = 0x25;
        public const int VK_UP = 0x26;
        public const int VK_RIGHT = 0x27;
        public const int VK_DOWN = 0x28;
        public const int VK_Select = 0x29;
        public const int VK_PRINT = 0x2A;
        public const int VK_EXECUTE = 0x2B;
        public const int VK_SNAPSHOT = 0x2C;
        public const int VK_Insert = 0x2D;
        public const int VK_Delete = 0x2E;
        public const int VK_HELP = 0x2F;
        public const int VK_0 = 0x30;
        public const int VK_1 = 0x31;
        public const int VK_2 = 0x32;
        public const int VK_3 = 0x33;
        public const int VK_4 = 0x34;
        public const int VK_5 = 0x35;
        public const int VK_6 = 0x36;
        public const int VK_7 = 0x37;
        public const int VK_8 = 0x38;
        public const int VK_9 = 0x39;
        public const int VK_A = 0x41;
        public const int VK_B = 0x42;
        public const int VK_C = 0x43;
        public const int VK_D = 0x44;
        public const int VK_E = 0x45;
        public const int VK_F = 0x46;
        public const int VK_G = 0x47;
        public const int VK_H = 0x48;
        public const int VK_I = 0x49;
        public const int VK_J = 0x4A;
        public const int VK_K = 0x4B;
        public const int VK_L = 0x4C;
        public const int VK_M = 0x4D;
        public const int VK_N = 0x4E;
        public const int VK_O = 0x4F;
        public const int VK_P = 0x50;
        public const int VK_Q = 0x51;
        public const int VK_R = 0x52;
        public const int VK_S = 0x53;
        public const int VK_T = 0x54;
        public const int VK_U = 0x55;
        public const int VK_V = 0x56;
        public const int VK_W = 0x57;
        public const int VK_X = 0x58;
        public const int VK_Y = 0x59;
        public const int VK_Z = 0x5A;
        //public const int VK_STARTKEY = 0x5B
        //public const int VK_CONTEXTKEY = 0x5D
        //public const int VK_NUMPAD0 = 0x60
        //public const int VK_NUMPAD1 = 0x61
        //public const int VK_NUMPAD2 = 0x62
        //public const int VK_NUMPAD3 = 0x63
        //public const int VK_NUMPAD4 = 0x64
        //public const int VK_NUMPAD5 = 0x65
        //public const int VK_NUMPAD6 = 0x66
        //public const int VK_NUMPAD7 = 0x67
        //public const int VK_NUMPAD8 = 0x68
        //public const int VK_NUMPAD9 = 0x69
        //public const int VK_MULTIPLY = 0x6A
        //public const int VK_ADD = 0x6B
        //public const int VK_SEPARATOR = 0x6C
        //public const int VK_SUBTRACT = 0x6D
        //public const int VK_DECIMAL = 0x6E
        //public const int VK_DIVIDE = 0x6F
        public const int VK_F1 = 0x70;
        public const int VK_F2 = 0x71;
        public const int VK_F3 = 0x72;
        public const int VK_F4 = 0x73;
        public const int VK_F5 = 0x74;
        public const int VK_F6 = 0x75;
        public const int VK_F7 = 0x76;
        public const int VK_F8 = 0x77;
        public const int VK_F9 = 0x78;
        public const int VK_F10 = 0x79;
        public const int VK_F11 = 0x7A;
        public const int VK_F12 = 0x7B;
        //public const int VK_F13 = 0x7C;
        //public const int VK_F14 = 0x7D;
        //public const int VK_F15 = 0x7E;
        //public const int VK_F16 = 0x7F;
        //public const int VK_F17 = 0x80;
        //public const int VK_F18 = 0x81;
        //public const int VK_F19 = 0x82
        //public const int VK_F20 = 0x83
        //public const int VK_F21 = 0x84
        //public const int VK_F22 = 0x85
        //public const int VK_F23 = 0x86
        //public const int VK_F24 = 0x87
        //public const int VK_NUMLOCK = 0x90
        //public const int VK_OEM_SCROLL = 0x91
        //public const int VK_OEM_1 = 0xBA
        //public const int VK_OEM_PLUS = 0xBB
        //public const int VK_OEM_COMMA = 0xBC
        //public const int VK_OEM_MINUS = 0xBD
        //public const int VK_OEM_PERIOD = 0xBE
        //public const int VK_OEM_2 = 0xBF
        //public const int VK_OEM_3 = 0xC0
        //public const int VK_OEM_4 = 0xDB
        //public const int VK_OEM_5 = 0xDC
        //public const int VK_OEM_6 = 0xDD
        //public const int VK_OEM_7 = 0xDE
        //public const int VK_OEM_8 = 0xDF
        //public const int VK_ICO_F17 = 0xE0
        //public const int VK_ICO_F18 = 0xE1
        //public const int VK_OEM102 = 0xE2
        //public const int VK_ICO_HELP = 0xE3
        //public const int VK_ICO_00 = 0xE4
        //public const int VK_ICO_CLEAR = 0xE6
        //public const int VK_OEM_RESET = 0xE9
        //public const int VK_OEM_JUMP = 0xEA
        //public const int VK_OEM_PA1 = 0xEB
        //public const int VK_OEM_PA2 = 0xEC
        //public const int VK_OEM_PA3 = 0xED
        //public const int VK_OEM_WSCTRL = 0xEE
        //public const int VK_OEM_CUSEL = 0xEF
        //public const int VK_OEM_ATTN = 0xF0
        //public const int VK_OEM_FINNISH = 0xF1
        //public const int VK_OEM_COPY = 0xF2
        //public const int VK_OEM_AUTO = 0xF3
        //public const int VK_OEM_ENLW = 0xF4
        //public const int VK_OEM_BACKTAB = 0xF5
        //public const int VK_ATTN = 0xF6
        //public const int VK_CRSEL = 0xF7
        //public const int VK_EXSEL = 0xF8
        //public const int VK_EREOF = 0xF9
        //public const int VK_PLAY = 0xFA
        //public const int VK_ZOOM = 0xFB
        //public const int VK_NONAME = 0xFC
        //public const int VK_PA1 = 0xFD
        //public const int VK_OEM_CLEAR = 0xFE
        #endregion

        public const int MK_LBUTTON = 0x01;
        public const int MK_RBUTTON = 0x02;
        public const int MK_SHIFT = 0x04;
        public const int MK_CONTROL = 0x08;
        public const int MK_MBUTTON = 0x10;
        public const int WM_COMMAND = 0x0111;
        public const uint PM_REMOVE = 0x0001;
    }
}
