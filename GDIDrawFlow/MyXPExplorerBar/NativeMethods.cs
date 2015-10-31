using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
namespace System.MyControl.MyXPExplorerBar
{


	public sealed class NativeMethods
	{
		
		[DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr LoadLibrary(string lpFileName);


	
		public static IntPtr LoadLibraryEx(string lpfFileName, LoadLibraryExFlags flags)
		{
			return NativeMethods.InternalLoadLibraryEx(lpfFileName, IntPtr.Zero, (int)flags);
		}

		[DllImport("Kernel32.dll", EntryPoint = "LoadLibraryEx")]
		private static extern IntPtr InternalLoadLibraryEx(string lpfFileName, IntPtr hFile, int dwFlags);


		[DllImport("Kernel32.dll")]
		public static extern bool FreeLibrary(IntPtr hModule);



		[DllImport("Kernel32.dll")]
		public static extern IntPtr FindResource(IntPtr hModule, string lpName, int lpType);


		[DllImport("Kernel32.dll")]
		public static extern IntPtr FindResource(IntPtr hModule, string lpName, string lpType);

		[DllImport("Kernel32.dll")]
		public static extern int SizeofResource(IntPtr hModule, IntPtr hResInfo);

		[DllImport("Kernel32.dll")]
		public static extern System.IntPtr LoadResource(IntPtr hModule, IntPtr hResInfo);



		[DllImport("Kernel32.dll")]
		public static extern int FreeResource(IntPtr hglbResource);

		[DllImport("Kernel32.dll")]
		public static extern void CopyMemory(IntPtr Destination, IntPtr Source, int Length);



		[DllImport("User32.dll")]
		public static extern IntPtr LoadBitmap(IntPtr hInstance, long lpBitmapName);


		[DllImport("User32.dll")]
		public static extern IntPtr LoadBitmap(IntPtr hInstance, string lpBitmapName);

		[DllImport("Gdi32.dll")]
		public static extern int GdiFlush();



		[DllImport("User32.dll")]
		public static extern int LoadString(IntPtr hInstance, int uID, StringBuilder lpBuffer, int nBufferMax);


		public static int SendMessage(IntPtr hwnd, WindowMessageFlags msg, IntPtr wParam, IntPtr lParam)
		{
			return NativeMethods.InternalSendMessage(hwnd, (int)msg, wParam, lParam);
		}

		[DllImport("User32.dll", EntryPoint = "SendMessage")]
		private static extern int InternalSendMessage(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam);


		[DllImport("Comctl32.dll")]
		public static extern int DllGetVersion(ref DLLVERSIONINFO pdvi);



		[DllImport("Kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);


		public static int SetErrorMode(SetErrorModeFlags uMode)
		{
			return NativeMethods.InternalSetErrorMode((int)uMode);
		}

		[DllImport("Kernel32.dll", EntryPoint = "SetErrorMode")]
		private static extern int InternalSetErrorMode(int uMode);



		[DllImport("User32.dll")]
		internal static extern int GetSystemMetrics(int nIndex);


		[DllImport("User32.dll")]
		internal static extern IntPtr GetDC(IntPtr hWnd);


		[DllImport("User32.dll")]
		internal static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);


	
		[DllImport("Gdi32.dll")]
		internal static extern int GetDeviceCaps(IntPtr hDC, int nIndex);


		[DllImport("User32.dll")]
		internal static extern unsafe IntPtr CreateIconFromResourceEx(byte* pbIconBits, int cbIconBits, bool fIcon, int dwVersion, int csDesired, int cyDesired, int flags);

		[DllImport("Gdi32.dll")]
		internal static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);


		[DllImport("Gdi32.dll")]
		internal static extern bool DeleteObject(IntPtr hObject);


	
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		internal static extern int DrawText(IntPtr hdc, string lpString, int nCount, ref RECT lpRect, DrawTextFlags uFormat);


		[DllImport("Gdi32.dll")]
		internal static extern int SetBkMode(IntPtr hdc, int iBkMode);


		[DllImport("Gdi32.dll")]
		internal static extern int SetTextColor(IntPtr hdc, int crColor);
	}


	[Serializable(),
	StructLayout(LayoutKind.Sequential)]
	public struct POINT
	{
	
		public int x;
	
		public int y;

		public POINT(int x, int y)
		{
			this.x = x;
			this.y = y;
		}



		public static POINT FromPoint(Point p)
		{
			return new POINT(p.X, p.Y);
		}


		public Point ToPoint()
		{
			return new Point(this.x, this.y);
		}
	}


	[Serializable(),
	StructLayout(LayoutKind.Sequential)]
	public struct RECT
	{
	
		public int left;

		public int top;

		public int right;

		public int bottom;



		public RECT(int left, int top, int right, int bottom)
		{
			this.left = left;
			this.top = top;
			this.right = right;
			this.bottom = bottom;
		}


	
		public static RECT FromRectangle(Rectangle rect)
		{
			return new RECT(rect.Left, rect.Top, rect.Right, rect.Bottom);
		}


	
		public static RECT FromXYWH(int x, int y, int width, int height)
		{
			return new RECT(x, y, x + width, y + height);
		}


	
		public Rectangle ToRectangle()
		{
			return new Rectangle(this.left, this.top, this.right - this.left, this.bottom - this.top);
		}
	}


	[Serializable(),
	StructLayout(LayoutKind.Sequential)]
	public struct DLLVERSIONINFO
	{
		
		public int cbSize;

		public int dwMajorVersion;

		public int dwMinorVersion;

		public int dwBuildNumber;

		public int dwPlatformID;
	}


	[StructLayout(LayoutKind.Sequential, Pack = 2)]
	internal struct ICONFILE
	{
		
		public short reserved;

		public short resourceType;

		public short iconCount;

		
		public ICONENTRY entries;
	}


	[StructLayout(LayoutKind.Sequential)]
	internal struct ICONENTRY
	{
		
		public byte width;

		public byte height;

		public byte numColors;

		public byte reserved;

		public short numPlanes;

		public short bitsPerPixel;

		public int dataSize;

		public int dataOffset;
	}

	
	public enum WindowMessageFlags
	{
		
		WM_PRINT = 791,

		WM_PRINTCLIENT = 792,
	}

	
	public enum WmPrintFlags
	{
		
		PRF_CHECKVISIBLE = 1,

		PRF_NONCLIENT = 2,

		PRF_CLIENT = 4,

		PRF_ERASEBKGND = 8,

		PRF_CHILDREN = 16,

		PRF_OWNED = 32
	}

	
	public enum LoadLibraryExFlags
	{
		
		DONT_RESOLVE_DLL_REFERENCES = 1,

		
		LOAD_LIBRARY_AS_DATAFILE = 2,

		
		LOAD_WITH_ALTERED_SEARCH_PATH = 8,

		
		LOAD_IGNORE_CODE_AUTHZ_LEVEL = 16
	}

	
	public enum SetErrorModeFlags
	{
		
		SEM_DEFAULT = 0,

		SEM_FAILCRITICALERRORS = 1,

		
		SEM_NOGPFAULTERRORBOX = 2,

	
		SEM_NOALIGNMENTFAULTEXCEPT = 4,

		SEM_NOOPENFILEERRORBOX = 32768
	}

	public enum DrawTextFlags
	{
		
		DT_TOP = 0x00000000,

		DT_LEFT = 0x00000000,

		
		DT_CENTER = 0x00000001,

	
		DT_RIGHT = 0x00000002,

		DT_VCENTER = 0x00000004,

		
		DT_BOTTOM = 0x00000008,

		
		DT_WORDBREAK = 0x00000010,

		
		DT_SINGLELINE = 0x00000020,

		
		DT_EXPANDTABS = 0x00000040,

		DT_TABSTOP = 0x00000080,

		
		DT_NOCLIP = 0x00000100,


		DT_EXTERNALLEADING = 0x00000200,


		DT_CALCRECT = 0x00000400,


		DT_NOPREFIX = 0x00000800,

		DT_INTERNAL = 0x00001000,

	
		DT_EDITCONTROL = 0x00002000,

	
		DT_PATH_ELLIPSIS = 0x00004000,

		
		DT_END_ELLIPSIS = 0x00008000,

	
		DT_MODIFYSTRING = 0x00010000,

	
		DT_RTLREADING = 0x00020000,

	
		DT_WORD_ELLIPSIS = 0x00040000
	}


}
