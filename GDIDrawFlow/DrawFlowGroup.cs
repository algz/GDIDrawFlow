using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Reflection;
using System.IO;
namespace GDIDrawFlow
{
	/// <summary>
	/// DrawFlowGroup 的摘要说明。
	/// </summary>
	[Guid("CB5BDC81-93C1-11CF-8F20-00805F2CD064"),InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IObjectSafety 
	{ 
		void GetInterfacceSafyOptions( 
			System.Int32 riid, 
			out System.Int32 pdwSupportedOptions, 
			out System.Int32 pdwEnabledOptions); 
		void SetInterfaceSafetyOptions( 
			System.Int32 riid, 
			System.Int32 dwOptionsSetMask, 
			System.Int32 dwEnabledOptions);         
	} 
	[Guid("4AC5490A-AF58-4733-8E54-3C1CC04FEC42"),ProgId("GDIDrawFlow.DrawFlowGroup")]
	public class DrawFlowGroup : System.Windows.Forms.UserControl,IObjectSafety
	{
		private System.Windows.Forms.ImageList tabImage;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.ContextMenu context;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.MenuItem mi_new;
		private System.Windows.Forms.MenuItem mi_close;
		private System.Windows.Forms.MenuItem mi_rename;
		private System.Windows.Forms.MenuItem mi_read;
		private System.Windows.Forms.MenuItem mi_save;
		private System.Windows.Forms.ImageList toolBarImage;
		private System.Windows.Forms.Panel panel1;
		private System.MyControl.NSButton nsButton2;
		private System.MyControl.NSButton nsButton3;
		private System.MyControl.NSButton mtb_read;
		private System.MyControl.NSButton mtb_save;
		private System.MyControl.NSButton mtb_new;
		Random r=new Random ();
		private System.MyControl.MyToolTip.MyToolTip myToolTip;
		private System.MyControl.NSButton nsButton4;
		private System.MyControl.NSButton mtb_captures;
		private System.MyControl.NSButton nsButton9;
		private System.MyControl.NSButton nsButton7;
		private System.MyControl.NSButton mtb_close;
		private System.MyControl.NSButton nsButton5;
		private System.MyControl.NSButton mtb_Cls;
		private System.MyControl.NSButton nsButton1;
		private System.MyControl.NSButton mtb_table;
		private System.Windows.Forms.Timer tableshow;
		private System.MyControl.MyTabControl.FlatTabControl flatTabControl1;
		private System.Windows.Forms.Timer tablehide;
		private System.MyControl.NSButton mtb_fullScr;
		public static LoadingForm loadingForm=new LoadingForm();
		public DrawFlowGroup()
		{
			// 该调用是 Windows.Forms 窗体设计器所必需的。
			InitializeComponent();

			// TODO: 在 InitializeComponent 调用后添加任何初始化

		}

		/// <summary> 
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region 组件设计器生成的代码
		/// <summary> 
		/// 设计器支持所需的方法 - 不要使用代码编辑器 
		/// 修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(DrawFlowGroup));
			this.flatTabControl1 = new System.MyControl.MyTabControl.FlatTabControl();
			this.context = new System.Windows.Forms.ContextMenu();
			this.mi_new = new System.Windows.Forms.MenuItem();
			this.mi_read = new System.Windows.Forms.MenuItem();
			this.mi_close = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.mi_save = new System.Windows.Forms.MenuItem();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.mi_rename = new System.Windows.Forms.MenuItem();
			this.tabImage = new System.Windows.Forms.ImageList(this.components);
			this.toolBarImage = new System.Windows.Forms.ImageList(this.components);
			this.panel1 = new System.Windows.Forms.Panel();
			this.mtb_table = new System.MyControl.NSButton();
			this.nsButton1 = new System.MyControl.NSButton();
			this.nsButton7 = new System.MyControl.NSButton();
			this.nsButton5 = new System.MyControl.NSButton();
			this.nsButton4 = new System.MyControl.NSButton();
			this.nsButton3 = new System.MyControl.NSButton();
			this.nsButton2 = new System.MyControl.NSButton();
			this.mtb_captures = new System.MyControl.NSButton();
			this.nsButton9 = new System.MyControl.NSButton();
			this.mtb_fullScr = new System.MyControl.NSButton();
			this.mtb_close = new System.MyControl.NSButton();
			this.mtb_Cls = new System.MyControl.NSButton();
			this.mtb_read = new System.MyControl.NSButton();
			this.mtb_save = new System.MyControl.NSButton();
			this.mtb_new = new System.MyControl.NSButton();
			this.myToolTip = new System.MyControl.MyToolTip.MyToolTip(this.components);
			this.tableshow = new System.Windows.Forms.Timer(this.components);
			this.tablehide = new System.Windows.Forms.Timer(this.components);
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// flatTabControl1
			// 
			this.flatTabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
			this.flatTabControl1.ContextMenu = this.context;
			this.flatTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flatTabControl1.ImageList = this.tabImage;
			this.flatTabControl1.Location = new System.Drawing.Point(0, 24);
			this.flatTabControl1.myBackColor = System.Drawing.Color.FromArgb(((System.Byte)(117)), ((System.Byte)(183)), ((System.Byte)(57)));
			this.flatTabControl1.Name = "flatTabControl1";
			this.flatTabControl1.SelectedIndex = 0;
			this.flatTabControl1.Size = new System.Drawing.Size(840, 496);
			this.flatTabControl1.TabIndex = 0;
			this.flatTabControl1.SelectedIndexChanged += new System.EventHandler(this.flatTabControl1_SelectedIndexChanged);
			// 
			// context
			// 
			this.context.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					this.mi_new,
																					this.mi_read,
																					this.mi_close,
																					this.menuItem7,
																					this.mi_save,
																					this.menuItem8,
																					this.mi_rename});
			// 
			// mi_new
			// 
			this.mi_new.Index = 0;
			this.mi_new.Shortcut = System.Windows.Forms.Shortcut.CtrlN;
			this.mi_new.Text = "新建";
			this.mi_new.Click += new System.EventHandler(this.mtb_new_Click);
			// 
			// mi_read
			// 
			this.mi_read.Index = 1;
			this.mi_read.Shortcut = System.Windows.Forms.Shortcut.CtrlR;
			this.mi_read.Text = "读取";
			this.mi_read.Click += new System.EventHandler(this.mtb_read_Click);
			// 
			// mi_close
			// 
			this.mi_close.Index = 2;
			this.mi_close.Shortcut = System.Windows.Forms.Shortcut.CtrlW;
			this.mi_close.Text = "关闭";
			this.mi_close.Click += new System.EventHandler(this.mtb_close_Click);
			// 
			// menuItem7
			// 
			this.menuItem7.Index = 3;
			this.menuItem7.Text = "-";
			// 
			// mi_save
			// 
			this.mi_save.Index = 4;
			this.mi_save.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
			this.mi_save.Text = "保存";
			this.mi_save.Click += new System.EventHandler(this.mtb_save_Click);
			// 
			// menuItem8
			// 
			this.menuItem8.Index = 5;
			this.menuItem8.Text = "-";
			// 
			// mi_rename
			// 
			this.mi_rename.Index = 6;
			this.mi_rename.Text = "重命名";
			this.mi_rename.Click += new System.EventHandler(this.mi_rename_Click);
			// 
			// tabImage
			// 
			this.tabImage.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.tabImage.ImageSize = new System.Drawing.Size(16, 16);
			this.tabImage.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("tabImage.ImageStream")));
			this.tabImage.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// toolBarImage
			// 
			this.toolBarImage.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.toolBarImage.ImageSize = new System.Drawing.Size(24, 24);
			this.toolBarImage.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("toolBarImage.ImageStream")));
			this.toolBarImage.TransparentColor = System.Drawing.Color.Black;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(116)), ((System.Byte)(183)), ((System.Byte)(57)));
			this.panel1.Controls.Add(this.mtb_table);
			this.panel1.Controls.Add(this.nsButton1);
			this.panel1.Controls.Add(this.nsButton7);
			this.panel1.Controls.Add(this.nsButton5);
			this.panel1.Controls.Add(this.nsButton4);
			this.panel1.Controls.Add(this.nsButton3);
			this.panel1.Controls.Add(this.nsButton2);
			this.panel1.Controls.Add(this.mtb_captures);
			this.panel1.Controls.Add(this.nsButton9);
			this.panel1.Controls.Add(this.mtb_fullScr);
			this.panel1.Controls.Add(this.mtb_close);
			this.panel1.Controls.Add(this.mtb_Cls);
			this.panel1.Controls.Add(this.mtb_read);
			this.panel1.Controls.Add(this.mtb_save);
			this.panel1.Controls.Add(this.mtb_new);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(840, 24);
			this.panel1.TabIndex = 2;
			// 
			// mtb_table
			// 
			this.mtb_table.ButtonForm = System.MyControl.eButtonForm.Rectangle;
			this.mtb_table.Dock = System.Windows.Forms.DockStyle.Right;
			this.mtb_table.HighlightColor = System.Drawing.Color.FromArgb(((System.Byte)(216)), ((System.Byte)(229)), ((System.Byte)(252)));
			this.mtb_table.HottrackImage = null;
			this.myToolTip.SetIconType(this.mtb_table, System.MyControl.MyToolTip.ToolTipIconType.Information);
			this.mtb_table.Location = new System.Drawing.Point(676, 0);
			this.mtb_table.Name = "mtb_table";
			this.mtb_table.NormalImage = ((System.Drawing.Image)(resources.GetObject("mtb_table.NormalImage")));
			this.mtb_table.OnlyShowBitmap = false;
			this.mtb_table.PressedImage = null;
			this.mtb_table.Size = new System.Drawing.Size(88, 24);
			this.mtb_table.TabIndex = 24;
			this.mtb_table.TabStop = false;
			this.mtb_table.Text = "即时表格";
			this.mtb_table.TextAlign = System.MyControl.eTextAlign.Right;
			this.myToolTip.SetTipTitle(this.mtb_table, "即时表格");
			this.myToolTip.SetToolTip(this.mtb_table, "流程图用表格的形式即时表现出来，点击显示/隐藏即时表格.");
			this.mtb_table.ToolTip = "";
			this.mtb_table.Click += new System.EventHandler(this.mtb_table_Click);
			// 
			// nsButton1
			// 
			this.nsButton1.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(116)), ((System.Byte)(183)), ((System.Byte)(57)));
			this.nsButton1.ButtonForm = System.MyControl.eButtonForm.Rectangle;
			this.nsButton1.Dock = System.Windows.Forms.DockStyle.Right;
			this.nsButton1.Enabled = false;
			this.nsButton1.HighlightColor = System.Drawing.SystemColors.HotTrack;
			this.nsButton1.HottrackImage = null;
			this.nsButton1.Location = new System.Drawing.Point(764, 0);
			this.nsButton1.Name = "nsButton1";
			this.nsButton1.NormalImage = null;
			this.nsButton1.OnlyShowBitmap = false;
			this.nsButton1.PressedImage = null;
			this.nsButton1.Size = new System.Drawing.Size(76, 24);
			this.nsButton1.TabIndex = 23;
			this.nsButton1.TabStop = false;
			this.nsButton1.TextAlign = System.MyControl.eTextAlign.Right;
			this.nsButton1.ToolTip = null;
			// 
			// nsButton7
			// 
			this.nsButton7.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(116)), ((System.Byte)(183)), ((System.Byte)(57)));
			this.nsButton7.ButtonForm = System.MyControl.eButtonForm.Rectangle;
			this.nsButton7.Dock = System.Windows.Forms.DockStyle.Left;
			this.nsButton7.Enabled = false;
			this.nsButton7.HighlightColor = System.Drawing.SystemColors.HotTrack;
			this.nsButton7.HottrackImage = null;
			this.nsButton7.Location = new System.Drawing.Point(540, 0);
			this.nsButton7.Name = "nsButton7";
			this.nsButton7.NormalImage = null;
			this.nsButton7.OnlyShowBitmap = false;
			this.nsButton7.PressedImage = null;
			this.nsButton7.Size = new System.Drawing.Size(10, 24);
			this.nsButton7.TabIndex = 17;
			this.nsButton7.TabStop = false;
			this.nsButton7.TextAlign = System.MyControl.eTextAlign.Right;
			this.nsButton7.ToolTip = null;
			// 
			// nsButton5
			// 
			this.nsButton5.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(116)), ((System.Byte)(183)), ((System.Byte)(57)));
			this.nsButton5.ButtonForm = System.MyControl.eButtonForm.Rectangle;
			this.nsButton5.Dock = System.Windows.Forms.DockStyle.Left;
			this.nsButton5.Enabled = false;
			this.nsButton5.HighlightColor = System.Drawing.SystemColors.HotTrack;
			this.nsButton5.HottrackImage = null;
			this.nsButton5.Location = new System.Drawing.Point(530, 0);
			this.nsButton5.Name = "nsButton5";
			this.nsButton5.NormalImage = null;
			this.nsButton5.OnlyShowBitmap = false;
			this.nsButton5.PressedImage = null;
			this.nsButton5.Size = new System.Drawing.Size(10, 24);
			this.nsButton5.TabIndex = 15;
			this.nsButton5.TabStop = false;
			this.nsButton5.TextAlign = System.MyControl.eTextAlign.Right;
			this.nsButton5.ToolTip = null;
			// 
			// nsButton4
			// 
			this.nsButton4.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(116)), ((System.Byte)(183)), ((System.Byte)(57)));
			this.nsButton4.ButtonForm = System.MyControl.eButtonForm.Rectangle;
			this.nsButton4.Dock = System.Windows.Forms.DockStyle.Left;
			this.nsButton4.Enabled = false;
			this.nsButton4.HighlightColor = System.Drawing.SystemColors.HotTrack;
			this.nsButton4.HottrackImage = null;
			this.nsButton4.Location = new System.Drawing.Point(520, 0);
			this.nsButton4.Name = "nsButton4";
			this.nsButton4.NormalImage = null;
			this.nsButton4.OnlyShowBitmap = false;
			this.nsButton4.PressedImage = null;
			this.nsButton4.Size = new System.Drawing.Size(10, 24);
			this.nsButton4.TabIndex = 13;
			this.nsButton4.TabStop = false;
			this.nsButton4.TextAlign = System.MyControl.eTextAlign.Right;
			this.nsButton4.ToolTip = null;
			// 
			// nsButton3
			// 
			this.nsButton3.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(116)), ((System.Byte)(183)), ((System.Byte)(57)));
			this.nsButton3.ButtonForm = System.MyControl.eButtonForm.Rectangle;
			this.nsButton3.Dock = System.Windows.Forms.DockStyle.Left;
			this.nsButton3.Enabled = false;
			this.nsButton3.HighlightColor = System.Drawing.SystemColors.HotTrack;
			this.nsButton3.HottrackImage = null;
			this.nsButton3.Location = new System.Drawing.Point(510, 0);
			this.nsButton3.Name = "nsButton3";
			this.nsButton3.NormalImage = null;
			this.nsButton3.OnlyShowBitmap = false;
			this.nsButton3.PressedImage = null;
			this.nsButton3.Size = new System.Drawing.Size(10, 24);
			this.nsButton3.TabIndex = 3;
			this.nsButton3.TabStop = false;
			this.nsButton3.TextAlign = System.MyControl.eTextAlign.Right;
			this.nsButton3.ToolTip = null;
			// 
			// nsButton2
			// 
			this.nsButton2.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(116)), ((System.Byte)(183)), ((System.Byte)(57)));
			this.nsButton2.ButtonForm = System.MyControl.eButtonForm.Rectangle;
			this.nsButton2.Dock = System.Windows.Forms.DockStyle.Left;
			this.nsButton2.Enabled = false;
			this.nsButton2.HighlightColor = System.Drawing.SystemColors.HotTrack;
			this.nsButton2.HottrackImage = null;
			this.nsButton2.Location = new System.Drawing.Point(500, 0);
			this.nsButton2.Name = "nsButton2";
			this.nsButton2.NormalImage = null;
			this.nsButton2.OnlyShowBitmap = false;
			this.nsButton2.PressedImage = null;
			this.nsButton2.Size = new System.Drawing.Size(10, 24);
			this.nsButton2.TabIndex = 1;
			this.nsButton2.TabStop = false;
			this.nsButton2.TextAlign = System.MyControl.eTextAlign.Right;
			this.nsButton2.ToolTip = null;
			// 
			// mtb_captures
			// 
			this.mtb_captures.ButtonForm = System.MyControl.eButtonForm.Rectangle;
			this.mtb_captures.Dock = System.Windows.Forms.DockStyle.Left;
			this.mtb_captures.HighlightColor = System.Drawing.Color.FromArgb(((System.Byte)(220)), ((System.Byte)(228)), ((System.Byte)(213)));
			this.mtb_captures.HottrackImage = null;
			this.myToolTip.SetIconType(this.mtb_captures, System.MyControl.MyToolTip.ToolTipIconType.Information);
			this.mtb_captures.Location = new System.Drawing.Point(432, 0);
			this.mtb_captures.Name = "mtb_captures";
			this.mtb_captures.NormalImage = ((System.Drawing.Image)(resources.GetObject("mtb_captures.NormalImage")));
			this.mtb_captures.OnlyShowBitmap = false;
			this.mtb_captures.PressedImage = null;
			this.mtb_captures.Size = new System.Drawing.Size(68, 24);
			this.mtb_captures.TabIndex = 20;
			this.mtb_captures.TabStop = false;
			this.mtb_captures.Text = "截图";
			this.mtb_captures.TextAlign = System.MyControl.eTextAlign.Right;
			this.myToolTip.SetTipTitle(this.mtb_captures, "截图");
			this.myToolTip.SetToolTip(this.mtb_captures, "将当前页面流程绘制成图片并将其保存到本地。以便浏览");
			this.mtb_captures.ToolTip = "";
			this.mtb_captures.Click += new System.EventHandler(this.mtb_captures_Click);
			// 
			// nsButton9
			// 
			this.nsButton9.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(116)), ((System.Byte)(183)), ((System.Byte)(57)));
			this.nsButton9.ButtonForm = System.MyControl.eButtonForm.Rectangle;
			this.nsButton9.Dock = System.Windows.Forms.DockStyle.Left;
			this.nsButton9.Enabled = false;
			this.nsButton9.HighlightColor = System.Drawing.SystemColors.HotTrack;
			this.nsButton9.HottrackImage = null;
			this.nsButton9.Location = new System.Drawing.Point(422, 0);
			this.nsButton9.Name = "nsButton9";
			this.nsButton9.NormalImage = null;
			this.nsButton9.OnlyShowBitmap = false;
			this.nsButton9.PressedImage = null;
			this.nsButton9.Size = new System.Drawing.Size(10, 24);
			this.nsButton9.TabIndex = 19;
			this.nsButton9.TabStop = false;
			this.nsButton9.TextAlign = System.MyControl.eTextAlign.Right;
			this.nsButton9.ToolTip = null;
			// 
			// mtb_fullScr
			// 
			this.mtb_fullScr.ButtonForm = System.MyControl.eButtonForm.Rectangle;
			this.mtb_fullScr.Dock = System.Windows.Forms.DockStyle.Left;
			this.mtb_fullScr.HighlightColor = System.Drawing.Color.FromArgb(((System.Byte)(220)), ((System.Byte)(228)), ((System.Byte)(213)));
			this.mtb_fullScr.HottrackImage = null;
			this.myToolTip.SetIconType(this.mtb_fullScr, System.MyControl.MyToolTip.ToolTipIconType.Information);
			this.mtb_fullScr.Location = new System.Drawing.Point(340, 0);
			this.mtb_fullScr.Name = "mtb_fullScr";
			this.mtb_fullScr.NormalImage = ((System.Drawing.Image)(resources.GetObject("mtb_fullScr.NormalImage")));
			this.mtb_fullScr.OnlyShowBitmap = false;
			this.mtb_fullScr.PressedImage = null;
			this.mtb_fullScr.Size = new System.Drawing.Size(82, 24);
			this.mtb_fullScr.TabIndex = 25;
			this.mtb_fullScr.TabStop = false;
			this.mtb_fullScr.Text = "全屏模式";
			this.mtb_fullScr.TextAlign = System.MyControl.eTextAlign.Right;
			this.myToolTip.SetTipTitle(this.mtb_fullScr, "关闭流程图");
			this.mtb_fullScr.ToolTip = "";
			this.myToolTip.SetToolTip(this.mtb_fullScr, "将当前页面的流程图关闭");
			this.mtb_fullScr.Click += new System.EventHandler(this.mtb_fullScr_Click);
			// 
			// mtb_close
			// 
			this.mtb_close.ButtonForm = System.MyControl.eButtonForm.Rectangle;
			this.mtb_close.Dock = System.Windows.Forms.DockStyle.Left;
			this.mtb_close.HighlightColor = System.Drawing.Color.FromArgb(((System.Byte)(220)), ((System.Byte)(228)), ((System.Byte)(213)));
			this.mtb_close.HottrackImage = null;
			this.myToolTip.SetIconType(this.mtb_close, System.MyControl.MyToolTip.ToolTipIconType.Information);
			this.mtb_close.Location = new System.Drawing.Point(272, 0);
			this.mtb_close.Name = "mtb_close";
			this.mtb_close.NormalImage = ((System.Drawing.Image)(resources.GetObject("mtb_close.NormalImage")));
			this.mtb_close.OnlyShowBitmap = false;
			this.mtb_close.PressedImage = null;
			this.mtb_close.Size = new System.Drawing.Size(68, 24);
			this.mtb_close.TabIndex = 16;
			this.mtb_close.TabStop = false;
			this.mtb_close.Text = "关闭";
			this.mtb_close.TextAlign = System.MyControl.eTextAlign.Right;
			this.myToolTip.SetTipTitle(this.mtb_close, "关闭流程图");
			this.myToolTip.SetToolTip(this.mtb_close, "将当前页面的流程图关闭");
			this.mtb_close.ToolTip = "";
			this.mtb_close.Click += new System.EventHandler(this.mtb_close_Click);
			// 
			// mtb_Cls
			// 
			this.mtb_Cls.ButtonForm = System.MyControl.eButtonForm.Rectangle;
			this.mtb_Cls.Dock = System.Windows.Forms.DockStyle.Left;
			this.mtb_Cls.HighlightColor = System.Drawing.Color.FromArgb(((System.Byte)(220)), ((System.Byte)(228)), ((System.Byte)(213)));
			this.mtb_Cls.HottrackImage = null;
			this.myToolTip.SetIconType(this.mtb_Cls, System.MyControl.MyToolTip.ToolTipIconType.Information);
			this.mtb_Cls.Location = new System.Drawing.Point(204, 0);
			this.mtb_Cls.Name = "mtb_Cls";
			this.mtb_Cls.NormalImage = ((System.Drawing.Image)(resources.GetObject("mtb_Cls.NormalImage")));
			this.mtb_Cls.OnlyShowBitmap = false;
			this.mtb_Cls.PressedImage = null;
			this.mtb_Cls.Size = new System.Drawing.Size(68, 24);
			this.mtb_Cls.TabIndex = 14;
			this.mtb_Cls.TabStop = false;
			this.mtb_Cls.Text = "清空";
			this.mtb_Cls.TextAlign = System.MyControl.eTextAlign.Right;
			this.myToolTip.SetTipTitle(this.mtb_Cls, "清空");
			this.myToolTip.SetToolTip(this.mtb_Cls, "将当前页面清空。");
			this.mtb_Cls.ToolTip = "";
			this.mtb_Cls.Click += new System.EventHandler(this.mtb_Cls_Click);
			// 
			// mtb_read
			// 
			this.mtb_read.ButtonForm = System.MyControl.eButtonForm.Rectangle;
			this.mtb_read.Dock = System.Windows.Forms.DockStyle.Left;
			this.mtb_read.HighlightColor = System.Drawing.Color.FromArgb(((System.Byte)(220)), ((System.Byte)(228)), ((System.Byte)(213)));
			this.mtb_read.HottrackImage = null;
			this.myToolTip.SetIconType(this.mtb_read, System.MyControl.MyToolTip.ToolTipIconType.Information);
			this.mtb_read.Location = new System.Drawing.Point(136, 0);
			this.mtb_read.Name = "mtb_read";
			this.mtb_read.NormalImage = ((System.Drawing.Image)(resources.GetObject("mtb_read.NormalImage")));
			this.mtb_read.OnlyShowBitmap = false;
			this.mtb_read.PressedImage = null;
			this.mtb_read.Size = new System.Drawing.Size(68, 24);
			this.mtb_read.TabIndex = 4;
			this.mtb_read.TabStop = false;
			this.mtb_read.Text = "读取";
			this.mtb_read.TextAlign = System.MyControl.eTextAlign.Right;
			this.myToolTip.SetTipTitle(this.mtb_read, "读取流程图");
			this.myToolTip.SetToolTip(this.mtb_read, "读取己有的流程图或流程图模板");
			this.mtb_read.ToolTip = "";
			this.mtb_read.Click += new System.EventHandler(this.mtb_read_Click);
			// 
			// mtb_save
			// 
			this.mtb_save.ButtonForm = System.MyControl.eButtonForm.Rectangle;
			this.mtb_save.Dock = System.Windows.Forms.DockStyle.Left;
			this.mtb_save.HighlightColor = System.Drawing.Color.FromArgb(((System.Byte)(220)), ((System.Byte)(228)), ((System.Byte)(213)));
			this.mtb_save.HottrackImage = null;
			this.myToolTip.SetIconType(this.mtb_save, System.MyControl.MyToolTip.ToolTipIconType.Information);
			this.mtb_save.Location = new System.Drawing.Point(68, 0);
			this.mtb_save.Name = "mtb_save";
			this.mtb_save.NormalImage = ((System.Drawing.Image)(resources.GetObject("mtb_save.NormalImage")));
			this.mtb_save.OnlyShowBitmap = false;
			this.mtb_save.PressedImage = null;
			this.mtb_save.Size = new System.Drawing.Size(68, 24);
			this.mtb_save.TabIndex = 2;
			this.mtb_save.TabStop = false;
			this.mtb_save.Text = "保存";
			this.mtb_save.TextAlign = System.MyControl.eTextAlign.Right;
			this.myToolTip.SetTipTitle(this.mtb_save, "保存流程图");
			this.myToolTip.SetToolTip(this.mtb_save, "将绘制的流程图保存到服务器");
			this.mtb_save.ToolTip = "";
			this.mtb_save.Click += new System.EventHandler(this.mtb_save_Click);
			// 
			// mtb_new
			// 
			this.mtb_new.ButtonForm = System.MyControl.eButtonForm.Rectangle;
			this.mtb_new.Dock = System.Windows.Forms.DockStyle.Left;
			this.mtb_new.HighlightColor = System.Drawing.Color.FromArgb(((System.Byte)(220)), ((System.Byte)(228)), ((System.Byte)(213)));
			this.mtb_new.HottrackImage = null;
			this.myToolTip.SetIconType(this.mtb_new, System.MyControl.MyToolTip.ToolTipIconType.Information);
			this.mtb_new.Location = new System.Drawing.Point(0, 0);
			this.mtb_new.Name = "mtb_new";
			this.mtb_new.NormalImage = ((System.Drawing.Image)(resources.GetObject("mtb_new.NormalImage")));
			this.mtb_new.OnlyShowBitmap = false;
			this.mtb_new.PressedImage = null;
			this.mtb_new.Size = new System.Drawing.Size(68, 24);
			this.mtb_new.TabIndex = 0;
			this.mtb_new.TabStop = false;
			this.mtb_new.Text = "新建";
			this.mtb_new.TextAlign = System.MyControl.eTextAlign.Right;
			this.myToolTip.SetTipTitle(this.mtb_new, "新建流程图");
			this.myToolTip.SetToolTip(this.mtb_new, "新建一个空的流程图.");
			this.mtb_new.ToolTip = null;
			this.mtb_new.Click += new System.EventHandler(this.mtb_new_Click);
			// 
			// tableshow
			// 
			this.tableshow.Interval = 1;
			this.tableshow.Tick += new System.EventHandler(this.tableshow_Tick);
			// 
			// tablehide
			// 
			this.tablehide.Interval = 1;
			this.tablehide.Tick += new System.EventHandler(this.tablehide_Tick);
			// 
			// DrawFlowGroup
			// 
			this.Controls.Add(this.flatTabControl1);
			this.Controls.Add(this.panel1);
			this.Name = "DrawFlowGroup";
			this.Size = new System.Drawing.Size(840, 520);
			this.Load += new System.EventHandler(this.DrawFlowGroup_Load);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		//实例一个绘制流程图控件
		private void CreateTabPage(string PageName,bool isLoadDefault)
		{
			//实例一个选项卡页面
			TabPage tabPage = new TabPage(PageName);
			tabPage.BackColor=Color.FromArgb(100,100,100);
			tabPage.ImageIndex=0;
			GDIDrawFlow.DrawFlowControl dfControl=new DrawFlowControl();
			dfControl.Dock=DockStyle.Fill;
			tabPage.Controls.Add(dfControl);
			this.flatTabControl1.Controls.Add(tabPage);
			dfControl.FormRefrash();
			this.flatTabControl1.SelectedIndex=this.flatTabControl1.TabCount-1;
			if(isLoadDefault)
			{
				this.GetCurrentFlowControl.cb_limit.Checked=true;
				selectFileName="Default.xml";
				userOpenType="OpenDefault";
				userOperate="Open";
			}
		}
		private void DrawFlowGroup_Load(object sender, System.EventArgs e)
		{
			if(!Directory.Exists(Application.StartupPath+"\\DefaultXml"))
			{
				Directory.CreateDirectory(Application.StartupPath+"\\DefaultXml");
			}
			if(!Directory.Exists(Application.StartupPath+"\\WorkFlowMode"))
			{
				Directory.CreateDirectory(Application.StartupPath+"\\WorkFlowMode");
			}
			if(!Directory.Exists(Application.StartupPath+"\\WorkFlowXml"))
			{
				Directory.CreateDirectory(Application.StartupPath+"\\WorkFlowXml");
			}
			CreateTabPage("Default",true);
			LoadDefaulFlow();
			System.MyControl.MyMenu.Menus menus=new System.MyControl.MyMenu.Menus();
			menus.Start(this);
		}
		private void LoadDefaulFlow()
		{
			if(File.Exists(Application.StartupPath+"\\DefaultXml\\Default.xml"))
			{
				StreamReader sr=new StreamReader(Application.StartupPath+"\\DefaultXml\\Default.xml");
				DrawForXml(sr.ReadToEnd(),"默认流程图");
				sr.Close();
			}
		}
		/// <summary>
		/// 得到当前画布
		/// </summary>
		private GDIDrawFlow.DrawFlowControl GetCurrentFlowControl
		{
			get
			{
				if(this.flatTabControl1.SelectedIndex>=0)
				{
					return (GDIDrawFlow.DrawFlowControl)this.flatTabControl1.SelectedTab.Controls[0];
				}
				else
				{
					return null;
				}
			}
		}
		//新建
		public void CreateFlow()
		{
			NewDrawDialog ndd=new NewDrawDialog();
			if(ndd.ShowDialog()==DialogResult.OK)
			{
				CreateTabPage(ndd.FlowName,ndd.isLoadDefaultFlow);
				this.GetCurrentFlowControl.isNodeLimit=ndd.isNodeLimit;
				this.GetCurrentFlowControl.isIntegrality=ndd.isIntegrality;
				this.GetCurrentFlowControl.cb_integrality.Checked=ndd.isIntegrality;
				this.GetCurrentFlowControl.cb_limit.Checked=ndd.isNodeLimit;
				this.GetCurrentFlowControl.AddToolTip(ndd.isNodeLimit);
				GC.Collect();
			}
		}
		//关闭
		public void CloseFlow()
		{
			if(this.flatTabControl1.TabCount>0)
			{
				DialogResult dr=MessageForm.Show("是否需要保存\""+this.flatTabControl1.SelectedTab.Text+"\"",ButtonType.YesAndNo);
				if(dr==DialogResult.Cancel)
				{
					return;
				}
				else if(dr==DialogResult.Yes)
				{
					mtb_save_Click(null,null);
				}
				if(this.flatTabControl1.TabCount==1)
				{
					this.GetCurrentFlowControl.drawObject.DeleteAll();
					this.flatTabControl1.SelectedTab.Text="Default";
					return;
				}
				this.flatTabControl1.SelectedTab.Hide();
				this.flatTabControl1.SelectedTab.Controls[0].Dispose();
				this.flatTabControl1.SelectedTab.Controls.Clear();
				this.flatTabControl1.Controls.RemoveAt(this.flatTabControl1.SelectedIndex);
				//this.flatTabControl1.SelectedIndex=this.flatTabControl1.TabCount-1;
			}
		}
		//重命名
		private void Rename()
		{
			//将重命名流程图窗体与新建流程图窗体结合
			NewDrawDialog ndd=new NewDrawDialog();
			ndd.DialogCaption="重命名流程图";
			ndd.FlowName=this.flatTabControl1.SelectedTab.Text;
			if(ndd.ShowDialog()==DialogResult.OK)
			{
				if(this.flatTabControl1.SelectedIndex>=0)
				{
					this.flatTabControl1.SelectedTab.Text=ndd.FlowName;
				}
			}
		}
		//截取图片
		public void GetCurrentFlowBitmap()
		{
			if(this.flatTabControl1.SelectedIndex>=0)
			{
				GDIDrawFlow.DrawFlowControl dfControl=this.GetCurrentFlowControl;
				dfControl.SaveBitmap();
			}
		}
		//得到缩略图
		public void GetMiniature()
		{
			if(this.flatTabControl1.SelectedIndex>=0)
			{
				GDIDrawFlow.DrawFlowControl dfControl=this.GetCurrentFlowControl;
				dfControl.GetBitmap();
			}
		}

		//		public void RefreshWorkFlow()
		//		{
		//			if(this.flatTabControl1.SelectedIndex>=0)
		//			{
		//				GDIDrawFlow.DrawFlowControl dfControl=(GDIDrawFlow.DrawFlowControl)this.flatTabControl1.SelectedTab.Controls[0];
		//				dfControl.FormRefrash();
		//			}
		//		}
		#region IObjectSafety 成员 
 
		public void GetInterfacceSafyOptions(Int32 riid, out Int32 pdwSupportedOptions, out Int32 pdwEnabledOptions) 
		{ 
			// TODO:  添加 WebCamControl.GetInterfacceSafyOptions 实现 
			pdwSupportedOptions = 1; 
			pdwEnabledOptions = 2; 
		} 
 
		public void SetInterfaceSafetyOptions(Int32 riid, Int32 dwOptionsSetMask, Int32 dwEnabledOptions) 
		{ 
			// TODO:  添加 WebCamControl.SetInterfaceSafetyOptions 实现             
		} 
 
		#endregion 
		private void mi_rename_Click(object sender, System.EventArgs e)
		{
			Rename();
		}
		/////////////////////////////**********************/////////////////////

		
		string userOperate="Default";
		string xmlFileName="";
		string selectFileName="";

		/// <summary>
		///返回给客户端的XML
		/// </summary>
		/// <returns></returns>
		public string GetControlXml()
		{
			GDIDrawFlow.DrawFlowControl dfControl=this.GetCurrentFlowControl;
			return dfControl.GetControlXml();			
		}
		public string GetUserOperate()
		{
			return userOperate;
		}

		public void SetUserOperate()
		{
			userOperate="Default";
		}

		/// <summary>
		/// 将客户端传进来的XML进行绘制流程图
		/// </summary>
		/// <param name="inXml"></param>
		public void DrawForXml(string inXml,string flowName)
		{
			this.GetCurrentFlowControl.DarwServerXml(inXml,flowName);
			this.flatTabControl1.SelectedTab.Text=flowName;
		}

		/// <summary>
		/// 组件获取客户的Xml文件列表
		/// </summary>
		/// <param name="filenames"></param>
		public void GetXmlNames(string filenames)
		{
			SaveFlowDialog sfd=new SaveFlowDialog(this.flatTabControl1.SelectedTab.Text);
			if(sfd.ShowDialog()==DialogResult.OK)
			{
				xmlFileName=sfd.FileName;
				this.userOperate="Save";
			}
			else
			{
				userOperate="Default";
			}
		}
		

		/// <summary>
		/// 客户端获取用户的Xml文件名
		/// </summary>
		/// <returns></returns>
		public string GetXmlName()
		{
			return xmlFileName;
		}

		/// <summary>
		/// 获取流程图和模板名称
		/// </summary>
		/// <param name="fileandmode"></param>
		/// <returns></returns>
		public void GetFilesAndModes(string fileandmode)
		{
			loadingForm.Hide();
			OpenFlowDialog ofd=new OpenFlowDialog();
			if(ofd.ShowDialog()==DialogResult.OK)
			{
				
				selectFileName=ofd.OpenFileName;
			}
		}
		string userOpenType="";

		public string GetOpenType()
		{
			if(userOpenType=="OpenDefault")
			{
				return "OpenDefault";
			}
			else if(userOpenType=="OpenMode")
			{
				return "OpenMode";
			}
			return "OpenXml";
		}
		/// <summary>
		/// 获取用户选中的Xml文件
		/// </summary>
		/// <returns></returns>
		public string GetSelectXml()
		{
			return selectFileName;
		}

		public string GetImage()
		{
			GDIDrawFlow.DrawFlowControl dfControl=this.GetCurrentFlowControl;
			return dfControl.GetBitmap();

		}
		private void mtb_new_Click(object sender, System.EventArgs e)
		{
			CreateFlow();
		}

		private void mtb_save_Click(object sender, System.EventArgs e)
		{
			//检测流程图的完整性
			if(this.GetCurrentFlowControl.isIntegrality)
			{
				if(!this.GetCurrentFlowControl.drawObject.CheckIntegrity())
				{
					return;
				}
			}
			SaveFlowDialog sfd=new SaveFlowDialog(this.GetXmlName());
			if(sfd.ShowDialog()==DialogResult.OK)
			{
				if(sfd.SaveOperate.Equals("Save"))
				{
					StreamWriter sw=new StreamWriter(Application.StartupPath+"\\WorkFlowXml\\"+sfd.FileName);
					sw.WriteLine(this.GetControlXml());
					sw.Close();
				}
			}
		}
		private void mtb_read_Click(object sender, System.EventArgs e)
		{
			OpenFlowDialog ofd=new OpenFlowDialog();
			if(ofd.ShowDialog()==DialogResult.OK)
			{
				this.selectFileName=ofd.OpenFileName;
				if(ofd.OpenType.Equals("OpenXml"))
				{
					StreamReader sr=new StreamReader(Application.StartupPath+"\\WorkFlowXml\\"+this.selectFileName);
					DrawForXml(sr.ReadToEnd(),this.selectFileName);
					sr.Close();
				}
				else
				{
					StreamReader sr=new StreamReader(Application.StartupPath+"\\WorkFlowMode\\"+this.selectFileName);
					DrawForXml(sr.ReadToEnd(),this.selectFileName);
					sr.Close();
				}
			}
		}

		private void mtb_close_Click(object sender, System.EventArgs e)
		{
			CloseFlow();
		}

		private void mtb_captures_Click(object sender, System.EventArgs e)
		{
			GetCurrentFlowBitmap();
		}

		private void mtb_Cls_Click(object sender, System.EventArgs e)
		{
			this.GetCurrentFlowControl.drawObject.DeleteAll();
		}

		private Panel GetCurrentTable
		{
			get
			{
				return this.GetCurrentFlowControl.tablePanel;
			}
		}

		private void mtb_table_Click(object sender, System.EventArgs e)
		{
			if(this.GetCurrentTable.Visible==false)
			{
				this.tablehide.Stop();
				this.GetCurrentTable.Height=0;
				this.GetCurrentTable.Show();
				tableshow.Start();
			}
			else
			{
				tablehide.Start();
			}
		}

		private void tableshow_Tick(object sender, System.EventArgs e)
		{
			if(this.GetCurrentTable.Height<180)
			{
				this.GetCurrentTable.Height+=4;
			}
			else
			{
				this.tableshow.Stop();
			}
		}

		private void tablehide_Tick(object sender, System.EventArgs e)
		{
			if(this.GetCurrentTable.Height>0)
			{
				this.tableshow.Stop();
				this.GetCurrentTable.Height-=10;
			}
			else
			{
				this.GetCurrentTable.Hide();
				this.tablehide.Stop();
			}
		}
		public void ShowForm(string str)
		{
			MessageForm.Show(str);
		}

		private void flatTabControl1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			for(int i=0;i<this.flatTabControl1.TabCount;i++)
			{
				if(this.flatTabControl1.SelectedTab==this.flatTabControl1.TabPages[i])
				{
					this.flatTabControl1.SelectedTab.BackColor=Color.FromArgb(100,100,100);
				}
				else
				{
					this.flatTabControl1.TabPages[i].BackColor=Color.FromArgb(220, 228, 213);
				}
			}
		}
    bool isFullScr=false;
		private void mtb_fullScr_Click(object sender, System.EventArgs e)
		{
			if(this.Parent is System.Windows.Forms.Form)
			{
				Form form=((System.Windows.Forms.Form)this.Parent);
				if(isFullScr)
				{
					//form.FormBorderStyle=FormBorderStyle.Sizable;
					form.WindowState=FormWindowState.Normal;
					form.StartPosition=FormStartPosition.CenterScreen;
					this.mtb_fullScr.Text="全屏模式";
				}
				else
				{
					//form.FormBorderStyle=FormBorderStyle.None;
					form.WindowState=FormWindowState.Maximized;
					this.mtb_fullScr.Text="默认模式";
				}
				isFullScr=!isFullScr;
			}
		}
	}
}
