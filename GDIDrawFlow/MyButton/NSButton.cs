using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;

namespace System.MyControl
{
	public enum eButtonForm
	{
		Rectangle,
		Ellipse
	}

	public enum eTextAlign
	{
		Bottom,
		Right,
		None
	}
	public class NSButton : System.Windows.Forms.Control
	{
		private enum MouseState
		{
			Normal = 0,
			Hoover,
			Pressed
		}
		private System.ComponentModel.Container components = null;

		private eButtonForm m_ButtonForm = eButtonForm.Rectangle;
		private MouseState m_MouseState = MouseState.Normal;
		private Color m_HighlightColor = SystemColors.HotTrack;
		private Image m_HottrackImage = null;
		private Image m_NormalImage = null;
		private Image m_PressedImage = null;
		private eTextAlign m_TextAlign = eTextAlign.Bottom;
		private ToolTip m_ToolTip = new ToolTip();
		private string m_ToolTipText;
		private bool m_OnlyShowBitmap = false;

		public NSButton()
		{
			InitializeComponent();
			SetStyle(ControlStyles.DoubleBuffer | 
				ControlStyles.UserPaint | 
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.ResizeRedraw |
				ControlStyles.StandardDoubleClick |
				ControlStyles.UserMouse |
				ControlStyles.StandardClick |
				ControlStyles.SupportsTransparentBackColor,
				true);
			Size = new Size(23, 23);
			TabStop = false;
			m_ToolTip.Active = true;
		}

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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// NSButton
			// 
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.NSButton_MouseUp);
			this.MouseHover += new System.EventHandler(this.NSButton_MouseHover);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.NSButton_MouseMove);
			this.MouseLeave += new System.EventHandler(this.NSButton_MouseLeave);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.NSButton_MouseDown);
			this.MouseEnter += new System.EventHandler(this.NSButton_MouseEnter);

		}
		#endregion

		private void DrawBorder(Graphics g)
		{
			if (m_OnlyShowBitmap)
			{
				return;
			}
			if (m_MouseState == MouseState.Normal)
			{
				switch (m_ButtonForm)
				{
				case eButtonForm.Rectangle:
					break;
				case eButtonForm.Ellipse:
					break;
				}
				return;
			}

			if (m_MouseState == MouseState.Hoover || m_MouseState == MouseState.Pressed)
			{
				switch (m_ButtonForm)
				{
				case eButtonForm.Rectangle:
					g.FillRectangle(new SolidBrush(m_HighlightColor), 0, 0, Width - 1, Height - 1);
					g.DrawRectangle(SystemPens.ControlDarkDark, 0, 0, Width - 1, Height - 1);
					break;
				case eButtonForm.Ellipse:
					g.FillEllipse(new SolidBrush(m_HighlightColor), 0, 0, Width - 1, Height - 1);
					g.DrawEllipse(SystemPens.ControlDarkDark, 0, 0, Width - 1, Height - 1);
					break;
				}
				return;
			}
		}

		protected void DrawImage(Graphics g)
		{
			if (m_NormalImage == null)
			{
				return;
			}

			int x = 3, y = 3;
			if (m_TextAlign == eTextAlign.Right)
			{
				y = (Height - m_NormalImage.Height) / 2;
			}
			if (m_TextAlign == eTextAlign.Bottom)
			{
				x = (Width - m_NormalImage.Width) / 2;
			}
			if (m_TextAlign == eTextAlign.None)
			{
				x = (Width - m_NormalImage.Width) / 2;
				y = (Height - m_NormalImage.Height) / 2;
			}

			if (m_MouseState == MouseState.Normal)
			{
				if (Enabled)
				{
					if (m_NormalImage != null)
					{
						g.DrawImage(m_NormalImage, x, y, m_NormalImage.Width, m_NormalImage.Height);
					}
				}
				else
				{
					if (m_NormalImage != null)
					{
						ControlPaint.DrawImageDisabled(g, m_NormalImage, x, y, BackColor);
					}
				}
				return;
			}
			if (m_MouseState == MouseState.Hoover)
			{
				if (m_HottrackImage != null)
				{
					g.DrawImage(m_HottrackImage, x, y, m_HottrackImage.Width, m_HottrackImage.Height);
				}
				else if (m_NormalImage != null)
				{
					g.DrawImage(m_NormalImage, x, y, m_NormalImage.Width, m_NormalImage.Height);
				}
				return;
			}
			if (m_MouseState == MouseState.Pressed)
			{
				if (m_PressedImage != null)
				{
					g.DrawImage(m_PressedImage, x + 1, y + 1, m_PressedImage.Width, m_PressedImage.Height);
				}
				else if (m_NormalImage != null)
				{
					g.DrawImage(m_NormalImage, x + 1, y + 1, m_NormalImage.Width, m_NormalImage.Height);
				}
				return;
			}
		}

		public void DrawText(Graphics g)
		{
			if (m_OnlyShowBitmap)
			{
				return;
			}
			SizeF w = g.MeasureString(Text, Font);
			SizeF h = g.MeasureString("X", Font);

			int x = 0, y = 0;

			if (m_TextAlign == eTextAlign.Bottom)
			{
				x = (Width - (int)w.Width) / 2;
				y = Height - (int)h.Height - 2;
			}
			if (m_TextAlign == eTextAlign.Right)
			{
				if (m_NormalImage != null)
				{
					x = m_NormalImage.Width + 5;
				}
				else
				{
					x = 5;
				}
				y =(Height - (int)h.Height) / 2;
			}

			if (m_MouseState == MouseState.Pressed)
			{
				x++;
				y++;
			}

			Brush b;
			if (Enabled)
			{
				b = SystemBrushes.ControlText;
			}
			else
			{
				b = SystemBrushes.ControlDark;
			}

			if (m_TextAlign == eTextAlign.Bottom)
			{
				g.DrawString(Text, Font, b, x, y);
				return;
			}
			if (m_TextAlign == eTextAlign.Right)
			{
				g.DrawString(Text, Font, b, x, y);
				return;
			}
		}

		protected override void OnPaintBackground(PaintEventArgs pevent)
		{
			base.OnPaintBackground(pevent);
		}

		protected override void OnPaint(PaintEventArgs pevent)
		{
			if (m_ButtonForm == eButtonForm.Ellipse)
			{
				System.Drawing.Drawing2D.GraphicsPath buttonPath = new System.Drawing.Drawing2D.GraphicsPath();
				buttonPath.AddEllipse(0, 0, Width, Height);
				Region = new System.Drawing.Region(buttonPath);
			}
			Graphics g = pevent.Graphics;
			DrawBorder(g);
			DrawImage(g);
			DrawText(g);
		}

		private void NSButton_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			m_MouseState = MouseState.Pressed;
			Invalidate();
		}

		private void NSButton_MouseEnter(object sender, System.EventArgs e)
		{
			m_MouseState = MouseState.Hoover;
			Invalidate();
		}

		private void NSButton_MouseHover(object sender, System.EventArgs e)
		{
		
		}

		private void NSButton_MouseLeave(object sender, System.EventArgs e)
		{
			m_MouseState = MouseState.Normal;
			Invalidate();
		}

		private void NSButton_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (m_MouseState == MouseState.Normal)
			{
			m_MouseState = MouseState.Hoover;
			Invalidate();
			}
		}

		private void NSButton_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			m_MouseState = MouseState.Normal;
			Invalidate();
		}

		[
		Category("Appearance"),
		Description("控件形状")
		]
		public eButtonForm ButtonForm
		{
			get
			{
				return m_ButtonForm;
			}
			set
			{
				m_ButtonForm = value;
				Invalidate();
			}
		}

		[
		Category("Appearance"),
		Description("鼠标在按钮区域上时的颜色")
		]
		public Color HighlightColor
		{
			get
			{
				return m_HighlightColor;
			}
			set
			{
				m_HighlightColor = value;
			}
		}

		[
		Category("Appearance"),
		Description("文字显示位置")
		]
		public eTextAlign TextAlign
		{
			get
			{
				return m_TextAlign;
			}
			set
			{
				m_TextAlign = value;
				Invalidate();
			}
		}

		[
		Category("Appearance"),
		Description("是否只显示是图片.")
		]
		public bool OnlyShowBitmap
		{
			get
			{
				return m_OnlyShowBitmap;
			}
			set
			{
				m_OnlyShowBitmap = value;
				Invalidate();
			}
		}

		[
		Category("Appearance"),
		Description("显示ToolTip")
		]
		public string ToolTip
		{
			get
			{
				return m_ToolTipText;
			}
			set
			{
				m_ToolTipText = value;
				m_ToolTip.SetToolTip(this, m_ToolTipText);
			}
		}

		[
		Category("Images"),
		Description("正常显示的控件图片")
		]
		public Image NormalImage
		{
			get
			{
				return m_NormalImage;
			}
			set
			{
				m_NormalImage = value;
				Invalidate();
			}
		}

		[
		Category("Images"),
		Description("鼠标在控件区域上时显示的图片")
		]
		public Image HottrackImage
		{
			get
			{
				return m_HottrackImage;
			}
			set
			{
				m_HottrackImage = value;
				Invalidate();
			}
		}

		[
		Category("Images"),
		Description("曾按下过的控钮显示的图片")
		]
		public Image PressedImage
		{
			get
			{
				return m_PressedImage;
			}
			set
			{
				m_PressedImage = value;
				Invalidate();
			}
		}

	}

	public class NSButtonDesigner : System.Windows.Forms.Design.ControlDesigner 
	{
	
		public NSButtonDesigner()
		{
		}
		/// <summary>
		///移除该控件不能支持的按钮和控件属性
		/// </summary>
		/// <param name="Properties"></param>
		protected override void PostFilterProperties( IDictionary Properties )
		{
			Properties.Remove("AllowDrop");
			Properties.Remove("BackColor");
			Properties.Remove("BackgroundImage");
			Properties.Remove("ContextMenu");
			Properties.Remove("FlatStyle");
			Properties.Remove("ForeColor");
			Properties.Remove("Image");
			Properties.Remove("ImageAlign");
			Properties.Remove("ImageIndex");
			Properties.Remove("ImageList");
			//Properties.Remove("Size");
			Properties.Remove("RightToLeft");
			Properties.Remove("TabIndex");
			Properties.Remove("TabStop");
			Properties.Remove("CausesValidation");
		}
	}
}


























