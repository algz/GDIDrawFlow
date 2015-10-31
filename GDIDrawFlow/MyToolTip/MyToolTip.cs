using System;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using System.Windows.Forms.Design;

namespace System.MyControl.MyToolTip
{

	#region MyToolTip
	[
	ProvideProperty("ToolTip",typeof(Control)),
	ProvideProperty("TipTitle",typeof(Control)),
	ProvideProperty("IconType", typeof(Control)),
	ToolboxBitmap(typeof(System.Windows.Forms.ToolTip))
	]
	public class MyToolTip : Component,IExtenderProvider
	{
		private NativeToolTipWindow m_window;

		private Hashtable m_controls;

		private ArrayList m_addedList;

		private ToolTipStyle m_style;

		#region 构造方法

		/// <summary>
		/// 初始化一个ToolTip实例
		/// </summary>
		public MyToolTip()
		{
			m_window = new NativeToolTipWindow(this);
			m_controls = new Hashtable();
			m_addedList = new ArrayList();
			m_style = ToolTipStyle.Balloon;
		}
		/// <summary>
		/// 初始化一个ToolTip实例
		/// </summary>
		public MyToolTip(System.ComponentModel.IContainer container) : this()
		{
			container.Add(this);
		}

		#endregion

		protected override void Dispose(bool disposing)
		{
			if (m_window != null && m_window.Handle != IntPtr.Zero)
			{
				m_window.DestroyHandle();
			}
			base.Dispose (disposing);
		}

		#region 公有属性方法

		#region ToolTip

		/// <summary>
		/// 重新得到关联到别的控件上的文本
		/// </summary>
		/// <param name="control"></param>
		/// <returns></returns>
		[DefaultValue("")]
		[Editor(typeof(GWinfoMultilineEditor),typeof(UITypeEditor))]
		public virtual string GetToolTip(Control control)
		{
			string text = "";
			if (this.m_controls.Contains(control))
			{
				text = ((ToolTipInfo)this.m_controls[control]).TipText;
			}
			return text;
		}

		/// <summary>
		/// 关联到别的控件
		/// </summary>
		/// <param name="control"></param>
		/// <param name="text"></param>
		public virtual void SetToolTip(Control control, string text)
		{
			bool bNewControl,bValidText;

			if (control == null)
			{
				throw new ArgumentNullException("control");
			}

			bNewControl = !m_controls.ContainsKey(control);
			bValidText = (text != null && text.Length > 0);

			if (bNewControl)
			{
				if (bValidText)
				{
					ToolTipInfo info = new ToolTipInfo(text,"",ToolTipIconType.None);
					m_controls.Add(control,info);
					control.HandleCreated += new EventHandler(HandleCreated);
					control.HandleDestroyed += new EventHandler(HandleDestroyed);
					if (control.IsHandleCreated)
					{
						this.HandleCreated(control,EventArgs.Empty);
					}
				}
			}
			else
			{
				ToolTipInfo info = (ToolTipInfo)m_controls[control];
				info.TipText = text;
				if (info.TipText.Length == 0 && info.TipTitle.Length == 0 && info.IconType == ToolTipIconType.None)
				{
					m_controls.Remove(control);
					control.HandleCreated -= new EventHandler(HandleCreated);
					control.HandleDestroyed -= new EventHandler(HandleDestroyed);
					
					if (m_addedList.Contains(control))
					{
						DestroyRegion(control);
						m_addedList.Remove(control);
					}
				}
			}
		}

		#endregion

		#region TipTitle属性

		[DefaultValue("")]
		public virtual string GetTipTitle(Control control)
		{
			string text = "";
			if (this.m_controls.Contains(control))
			{
				text = ((ToolTipInfo)this.m_controls[control]).TipTitle;
			}
			return text;
		}

		public virtual void SetTipTitle(Control control,string title)
		{
			bool bNewControl,bValidText;

			if (control == null)
			{
				throw new ArgumentNullException("control");
			}

			bNewControl = !m_controls.ContainsKey(control);
			bValidText = (title != null && title.Length > 0);

			if (bNewControl)
			{
				if (bValidText)
				{
					ToolTipInfo info = new ToolTipInfo("",title,ToolTipIconType.None);
					m_controls.Add(control,info);
					control.HandleCreated += new EventHandler(HandleCreated);
					control.HandleDestroyed += new EventHandler(HandleDestroyed);
					if (control.IsHandleCreated)
					{
						this.HandleCreated(control,EventArgs.Empty);
					}
				}
			}
			else
			{
				ToolTipInfo info = (ToolTipInfo)m_controls[control];
				info.TipTitle = title;
				if (info.TipText.Length == 0 && info.TipTitle.Length == 0 && info.IconType == ToolTipIconType.None)
				{
					m_controls.Remove(control);
					control.HandleCreated -= new EventHandler(HandleCreated);
					control.HandleDestroyed -= new EventHandler(HandleDestroyed);
					
					if (m_addedList.Contains(control))
					{
						DestroyRegion(control);
						m_addedList.Remove(control);
					}
				}
			}
		}

		#endregion

		#region IconType属性

		[DefaultValue(ToolTipIconType.None)]
		public ToolTipIconType GetIconType(Control control)
		{
			ToolTipIconType type = ToolTipIconType.None;
			if (this.m_controls.Contains(control))
			{
				type = ((ToolTipInfo)this.m_controls[control]).IconType;
			}
			return type;
		}

		public void SetIconType(Control control,ToolTipIconType type)
		{
			bool bNewControl;

			if (control == null)
			{
				throw new ArgumentNullException("control");
			}

			bNewControl = !m_controls.ContainsKey(control);

			if (bNewControl)
			{
				if (type != ToolTipIconType.None)
				{
					ToolTipInfo info = new ToolTipInfo("","",type);
					m_controls.Add(control,info);
					control.HandleCreated += new EventHandler(HandleCreated);
					control.HandleDestroyed += new EventHandler(HandleDestroyed);
					if (control.IsHandleCreated)
					{
						this.HandleCreated(control,EventArgs.Empty);
					}
				}
			}
			else
			{
				ToolTipInfo info = (ToolTipInfo)m_controls[control];
				info.IconType = type;
				if (info.TipText.Length == 0 && info.TipTitle.Length == 0 && info.IconType == ToolTipIconType.None)
				{
					m_controls.Remove(control);
					control.HandleCreated -= new EventHandler(HandleCreated);
					control.HandleDestroyed -= new EventHandler(HandleDestroyed);
					
					if (m_addedList.Contains(control))
					{
						DestroyRegion(control);
						m_addedList.Remove(control);
					}
				}
			}
		}

		#endregion

		#endregion

		#region WndProc 方法

		internal virtual void WndProc(ref Message m)
		{
			if (m.Msg == NativeMethods.WM_NOTIFY)
			{
				NativeMethods.NMHDR hdr = (NativeMethods.NMHDR)Marshal.PtrToStructure(m.LParam,typeof(NativeMethods.NMHDR));

				if (hdr.code == NativeMethods.TTN_NEEDTEXT)
				{
					Control control = Control.FromHandle(new IntPtr(hdr.idFrom));
					if (control != null)
					{
						string text = GetToolTip(control);
						string title = GetTipTitle(control);
						int icon = (int)GetIconType(control);
						NativeMethods.SendMessage(new HandleRef(this,this.Handle),NativeMethods.TTM_SETTITLE,icon,title);

						NativeMethods.NMTTDISPINFO info = (NativeMethods.NMTTDISPINFO)Marshal.PtrToStructure(m.LParam,typeof(NativeMethods.NMTTDISPINFO));
						info.lpszText = text;
						Marshal.StructureToPtr(info,m.LParam,true);
					}
					return;
				}
			}
			this.m_window.DefWndProc(ref m);
		}

		#endregion

		#region IExtenderProvider 部分

		public bool CanExtend(object extendee)
		{
			if (extendee is Control)
			{
				return true;
			}
			return false;
		}

		#endregion

		#region 私有方法

		/// <summary>
		/// 当控件创建时将其它控件注册关联
		/// </summary>
		private void HandleCreated(object sender, EventArgs e)
		{
			this.CreateRegion((Control) sender);
		}

		private void HandleDestroyed(object sender, EventArgs e)
		{
			this.DestroyRegion((Control) sender);
		}

		private void CreateRegion(Control ctl)
		{
			if (!m_controls.ContainsKey(ctl))
			{
				return;
			}
			ToolTipInfo info = (ToolTipInfo)this.m_controls[ctl];

			bool flag = info.TipText.Length > 0 || info.TipTitle.Length > 0 || info.IconType != ToolTipIconType.None;
			if (flag && !this.m_addedList.Contains(ctl) && !base.DesignMode)
			{
				int num1 = (int) NativeMethods.SendMessage(new HandleRef(this, this.Handle), NativeMethods.TTM_ADDTOOL, 0, this.GetTOOLINFO(ctl));
				if (num1 == 0)
				{
					throw new InvalidOperationException("error:" + NativeMethods.GetLastError());
				}
				this.m_addedList.Add(ctl);
			}
		}

		private void DestroyRegion(Control control)
		{
			bool flag = control.IsHandleCreated && this.IsHandleCreated;
			if (this.m_addedList.Contains(control) && flag && !base.DesignMode)
			{
				NativeMethods.SendMessage(new HandleRef(this, this.Handle), NativeMethods.TTM_DELTOOL, 0, this.GetTOOLINFO(control));
				this.m_addedList.Remove(control);
			}
		}
		private NativeMethods.TOOLINFO GetTOOLINFO(Control ctl)
		{
			NativeMethods.TOOLINFO toolinfo = new NativeMethods.TOOLINFO();
			toolinfo.cbSize = Marshal.SizeOf(typeof(NativeMethods.TOOLINFO));
			toolinfo.hwnd = this.Handle;
			toolinfo.uFlags = NativeMethods.TTF_TRANSPARENT | NativeMethods.TTF_SUBCLASS | NativeMethods.TTF_IDISHWND;
			toolinfo.uId = ctl.Handle;
			toolinfo.lpszText = NativeMethods.LPSTR_TEXTCALLBACKW;
			return toolinfo;
		}

		private void CreateHandle()
		{
			if (!this.IsHandleCreated)
			{
				NativeMethods.INITCOMMONCONTROLSEX initcommoncontrolsex1 = new NativeMethods.INITCOMMONCONTROLSEX();
				initcommoncontrolsex1.dwICC = 8; // ICC_TAB_CLASSES : Load tab and ToolTip control classes. 
				NativeMethods.InitCommonControlsEx(initcommoncontrolsex1);
				this.m_window.CreateHandle(this.CreateParams);
				NativeMethods.SetWindowPos(new HandleRef(this, this.Handle), NativeMethods.HWND_TOPMOST, 0, 0, 0, 0, NativeMethods.SWP_NOACTIVATE | NativeMethods.SWP_NOMOVE | NativeMethods.SWP_NOSIZE);
				NativeMethods.SendMessage(new HandleRef(this, this.Handle), NativeMethods.TTM_SETMAXTIPWIDTH, 0, SystemInformation.MaxWindowTrackSize.Width);
				NativeMethods.SendMessage(new HandleRef(this, this.Handle), NativeMethods.TTM_ACTIVATE, 1,0);
			}
		}

		private System.Windows.Forms.CreateParams CreateParams
		{
			get
			{
				System.Windows.Forms.CreateParams params1 = new System.Windows.Forms.CreateParams();
				params1.Parent = IntPtr.Zero;
				params1.ClassName = NativeMethods.TOOLTIPS_CLASS;
				params1.Style |= NativeMethods.TTS_ALWAYSTIP;
				if (this.m_style == ToolTipStyle.Balloon)
				{
					params1.Style |= NativeMethods.TTS_BALLOON;
				}

				params1.ExStyle = 0;
				params1.Caption = null;
				return params1;
			}
		}

		private bool IsHandleCreated
		{
			get
			{
				return (this.m_window.Handle != IntPtr.Zero);
			}
		}

		private IntPtr Handle
		{
			get
			{
				if (!this.IsHandleCreated)
				{
					this.CreateHandle();
				}
				return this.m_window.Handle;
			}
		}

		#endregion
	}

	#endregion

	public enum ToolTipIconType : int
	{
		None = 0,
		Information,
		Warning,
		Error
	}


	public enum ToolTipStyle
	{
		Standard,
		Balloon
	}


	#region NativeToolTipWindow
	internal class  NativeToolTipWindow: NativeWindow
	{
		private MyToolTip m_toolTip;
		public NativeToolTipWindow(MyToolTip toolTip)
		{
			m_toolTip = toolTip;
		}

		protected override void WndProc(ref Message m)
		{
			m_toolTip.WndProc (ref m);
		}
	}
	#endregion

	#region ToolTipInfo
	internal class ToolTipInfo
	{
		private string m_tooltip;
		private string m_title;
		private ToolTipIconType m_iconType;

		public ToolTipInfo() : this("","",ToolTipIconType.None)
		{

		}

		public ToolTipInfo(string text,string title,ToolTipIconType icon)
		{
			m_tooltip = text;
			m_title = title;
			m_iconType = icon;
		}

		/// <summary>
		/// 获取和设置文本
		/// </summary>
		public string TipText
		{
			get
			{
				return m_tooltip;
			}
			set
			{
				m_tooltip = value;
			}
		}

		/// <summary>
		/// 获取和设置标题
		/// </summary>
		public string TipTitle
		{
			get
			{
				return m_title;
			}
			set
			{
				m_title = value;
			}
		}

		/// <summary>
		/// 获取和设置图标类型
		/// </summary>
		public ToolTipIconType IconType
		{
			get
			{
				return m_iconType;
			}
			set
			{
				m_iconType = value;
			}
		}
	}

	#endregion

	#region GWinfoMultilineEditor
	public class GWinfoMultilineEditor : UITypeEditor
	{
		public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.DropDown;
		}

		public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, System.IServiceProvider provider, object value)
		{
			if (provider == null)
				return value;

			IWindowsFormsEditorService edSvc = provider.GetService( typeof(IWindowsFormsEditorService) ) as IWindowsFormsEditorService;

			if (edSvc == null)
				return value;

			MultilineTextBox textBox = new MultilineTextBox();
			textBox.BorderStyle = BorderStyle.None;
			textBox.Size = new System.Drawing.Size(150,80);
			textBox.Text = value == null ? string.Empty : value.ToString();

			edSvc.DropDownControl(textBox);

			if (!textBox.Cancelled)
				value = textBox.Text;

			textBox.Dispose();

			return value;
		}

		private class MultilineTextBox : TextBox
		{
			private bool cancelled = false;

			internal MultilineTextBox()
			{
				this.Multiline = true;
				this.AcceptsTab = true;
			}

			internal bool Cancelled
			{
				get { return this.cancelled; }
			}
			
			protected override bool ProcessDialogKey(System.Windows.Forms.Keys keyData)
			{
				if (keyData == Keys.Escape)
					this.cancelled = true;
				return base.ProcessDialogKey(keyData);
			}
		}
	}

	#endregion

	#region NativeMethods
	internal sealed class NativeMethods
	{
		public const string	TOOLTIPS_CLASS	= "tooltips_class32";

		public const int WM_NOTIFY          = 0x004E;
		public const int WM_USER			= 0x0400;

		public const int GWL_STYLE          = -16;
		public const int GCL_STYLE			= -26;
		public const int CS_DROPSHADOW		= 0x00020000;

		public const int HWND_TOPMOST       = -1;

		public const int LPSTR_TEXTCALLBACKW= -1;

		// ToolTip Icons
		public const int TTI_NONE           = 0;
		public const int TTI_INFO           = 1;
		public const int TTI_WARNING        = 2;
		public const int TTI_ERROR          = 3;

		//
		public const int TTN_FIRST          = -520;
		public const int TTN_NEEDTEXT       = (TTN_FIRST - 10);

		//
		public const int SWP_NOSIZE		    = 0x0001;
		public const int SWP_NOMOVE		    = 0x0002;
		public const int SWP_NOACTIVATE	    = 0x0010;

		public const int TTS_ALWAYSTIP		= 0x01;
		public const int TTS_BALLOON		= 0x40;

		public const int TTF_IDISHWND       = 0x0001;
		public const int TTF_SUBCLASS       = 0x0010;
		public const int TTF_TRANSPARENT	= 0x0100;

		public const int TTM_ACTIVATE       = WM_USER + 1;
		public const int TTM_SETMAXTIPWIDTH	= WM_USER + 24;
		public const int TTM_SETTITLE		= WM_USER + 33;
		public const int TTM_ADDTOOL		= WM_USER + 50;
		public const int TTM_DELTOOL        = WM_USER + 51;

		#region Structures

		[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
			public class TOOLINFO
		{
			public int cbSize;
			public int uFlags;
			public IntPtr hwnd;
			public IntPtr uId;
			public RECT rect;
			public IntPtr hinst;
			//[MarshalAs(UnmanagedType.LPTStr)]
			public int lpszText;
			public IntPtr lParam;

			public TOOLINFO()
			{
				this.cbSize = Marshal.SizeOf(typeof(TOOLINFO));
				this.rect = new RECT(0,0,0,0);
				this.hinst = IntPtr.Zero;
				this.lParam = IntPtr.Zero;
			}
		}

		[StructLayout(LayoutKind.Sequential)]
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

			public static RECT FromXYWH(int x, int y, int width, int height)
			{
				return new RECT(x, y, x + width, y + height);
			}
		}

		[StructLayout(LayoutKind.Sequential, Pack=1)]
			public class INITCOMMONCONTROLSEX
		{
			public int dwSize;
			public int dwICC;

			public INITCOMMONCONTROLSEX()
			{
				this.dwSize = 8;
			}
		}

		[StructLayout(LayoutKind.Sequential)]
			public struct NMTTDISPINFO 
		{
			public NMHDR     hdr;
			[MarshalAs(UnmanagedType.LPTStr)]
			public string    lpszText;
			[MarshalAs(UnmanagedType.ByValTStr,SizeConst=80)]
			public string    szText;
			public IntPtr    hinst;
			public int       uFlags;
			public IntPtr    lParam;
		}

		[StructLayout(LayoutKind.Sequential)]
			public struct NMHDR
		{
			public IntPtr hwndFrom;
			public int idFrom;
			public int code;
		}

		#endregion

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam,TOOLINFO lParam);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam,string lParam);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, int lParam);

		[DllImport("comctl32.dll")]
		public static extern bool InitCommonControlsEx(INITCOMMONCONTROLSEX icc);

		[DllImport("Kernel32.dll")]
		public static extern int GetLastError();

		[DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
		public static extern bool SetWindowPos(HandleRef hWnd, int hWndInsertAfter, int x, int y, int cx, int cy, int flags);
	}
	#endregion
}
