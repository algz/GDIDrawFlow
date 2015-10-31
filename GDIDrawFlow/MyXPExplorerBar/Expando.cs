using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.Globalization;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Xml.Serialization;

namespace System.MyControl.MyXPExplorerBar
{
	public delegate void ExpandoEventHandler(object sender, ExpandoEventArgs e);

	[ToolboxItem(true), 
	DefaultEvent("StateChanged"), 
	DesignerAttribute(typeof(ExpandoDesigner))]
	public class Expando : Control, ISupportInitialize
	{
		#region 变量事件定义
		public event ExpandoEventHandler StateChanged;
		

		public event ExpandoEventHandler TitleImageChanged;
		

		public event ExpandoEventHandler SpecialGroupChanged;
		

		public event ExpandoEventHandler WatermarkChanged;


		public event ControlEventHandler ItemAdded;


		public event ControlEventHandler ItemRemoved;


		public event EventHandler CustomSettingsChanged;

		private Container components = null;

		private ExplorerBarInfo systemSettings;

		private bool specialGroup;

		private int expandedHeight;

		private Image titleImage;

		private int headerHeight;

		private bool collapsed;

		private FocusStates focusState;

		private int titleBarHeight;

		private bool animate;

		private bool animatingFade;

		private bool animatingSlide;

		private Image animationImage;

		private AnimationHelper animationHelper;

		private TaskPane taskpane;

		private bool autoLayout;

		private int oldWidth;

		private bool initialising;

		private ItemCollection itemList;

		private ArrayList hiddenControls;

		private AnimationPanel dummyPanel;

		private bool canCollapse;

		private int slideEndHeight;

		private Image watermark;

		private bool showFocusCues;

		private bool layout = false;

		private ExpandoInfo customSettings;

		private HeaderInfo customHeaderSettings;

		private int[] fadeHeights;

		private bool useDefaultTabHandling;

		private int beginUpdateCount;

		private bool slideAnimationBatched;

		private bool dragging;

		private Point dragStart;
		
		#endregion

		#region 构造函数
		public Expando() : base()
		{
			this.components = new System.ComponentModel.Container();

			this.SetStyle(ControlStyles.DoubleBuffer, true);
			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			this.SetStyle(ControlStyles.UserPaint, true);
			this.SetStyle(ControlStyles.ResizeRedraw, true);
			this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
			this.SetStyle(ControlStyles.Selectable, true);
			this.TabStop = true;

			this.systemSettings = ThemeManager.GetSystemExplorerBarSettings();

			this.customSettings = new ExpandoInfo();
			this.customSettings.Expando = this;
			this.customSettings.SetDefaultEmptyValues();

			this.customHeaderSettings = new HeaderInfo();
			this.customHeaderSettings.Expando = this;
			this.customHeaderSettings.SetDefaultEmptyValues();

			this.BackColor = this.systemSettings.Expando.NormalBackColor;

			this.expandedHeight = 100;

			this.animate = false;
			this.animatingFade = false;
			this.animatingSlide = false;
			this.animationImage = null;
			this.slideEndHeight = -1;
			this.animationHelper = null;
			this.fadeHeights = new int[AnimationHelper.NumAnimationFrames];

			this.Size = new Size(this.systemSettings.Header.BackImageWidth, this.expandedHeight);
			this.titleBarHeight = this.systemSettings.Header.BackImageHeight;
			this.headerHeight = this.titleBarHeight;
			this.oldWidth = this.Width;

			this.collapsed = false;
			
			this.specialGroup = false;

			this.focusState = FocusStates.None;

			this.titleImage = null;
			this.watermark = null;

			this.Font = new Font(this.TitleFont.Name, 8.25f, FontStyle.Regular);

			this.autoLayout = false;

			this.taskpane = null;

			this.itemList = new ItemCollection(this);
			this.hiddenControls = new ArrayList();

			this.dummyPanel = new AnimationPanel();
			this.dummyPanel.Size = this.Size;
			this.dummyPanel.Location = new Point(-1000, 0);

			this.canCollapse = true;

			this.showFocusCues = false;
			this.useDefaultTabHandling = true;

			this.CalcAnimationHeights();

			this.slideAnimationBatched = false;

			this.dragging = false;
			this.dragStart = Point.Empty;

			this.beginUpdateCount = 0;

			this.initialising = false;
			this.layout = false;
		}

		#endregion

		#region 方法



		public void Collapse()
		{
			this.collapsed = true;
			
			if (!this.Animating && this.Height != this.HeaderHeight)
			{
				this.Height = this.headerHeight;

		
				this.OnStateChanged(new ExpandoEventArgs(this));
			}
		}



		public void Expand()
		{
			this.collapsed = false;
			
			if (!this.Animating && this.Height != this.ExpandedHeight)
			{
				this.Height = this.ExpandedHeight;

				this.OnStateChanged(new ExpandoEventArgs(this));
			}
		}



		protected void StartFadeAnimation()
		{
			//
			this.animatingFade = true;

			//
			this.SuspendLayout();


			this.animationImage = this.GetFadeAnimationImage();


			foreach (Control control in this.Controls)
			{
				control.Visible = false;
			}

			this.ResumeLayout(false);
		}


		protected void UpdateFadeAnimation(int animationStepNum, int numAnimationSteps)
		{

			if (this.collapsed)
			{
				this.Height = this.fadeHeights[animationStepNum-1] + this.headerHeight;
			}
			else
			{
				this.Height = (this.ExpandedHeight - this.HeaderHeight) - this.fadeHeights[animationStepNum-1] + this.HeaderHeight - 1;
			}

			if (this.TaskPane != null)
			{
				this.TaskPane.DoLayout();
			}
			else
			{
				this.Invalidate();
			}
		}


		protected void StopFadeAnimation()
		{
			//
			this.animatingFade = false;

			//
			this.SuspendLayout();


			this.animationImage.Dispose();
			this.animationImage = null;


			if (this.collapsed)
			{
				this.Height = this.HeaderHeight;
			}
			else
			{
				this.Height = this.ExpandedHeight;
			}


			foreach (Control control in this.Controls)
			{
				control.Visible = !this.hiddenControls.Contains(control);
			}

			//
			this.ResumeLayout(true);

			if (this.TaskPane != null)
			{
				this.TaskPane.DoLayout();
			}
		}



		protected Image GetFadeAnimationImage()
		{
			if (this.Height == this.ExpandedHeight)
			{
				return this.GetExpandedImage();
			}
			else
			{
				return this.GetCollapsedImage();
			}
		}



		protected Image GetExpandedImage()
		{
			Image image = new Bitmap(this.Width, this.Height);

			Graphics g = Graphics.FromImage(image);
			IntPtr hDC = g.GetHdc();

			IntPtr flags = (IntPtr) (WmPrintFlags.PRF_CLIENT | WmPrintFlags.PRF_CHILDREN | WmPrintFlags.PRF_ERASEBKGND);
			
			NativeMethods.SendMessage(this.Handle, WindowMessageFlags.WM_PRINT, hDC, flags);

		
			g.ReleaseHdc(hDC);
			g.Dispose();

			
			return image;
		}


		protected Image GetCollapsedImage()
		{


			int width = this.Width;
			int height = this.ExpandedHeight;
			
			
			
			Image backImage = new Bitmap(width, height);

		
			Graphics g = Graphics.FromImage(backImage);

	
			this.PaintTransparentBackground(g, new Rectangle(0, 0, width, height));

		
			this.OnPaintTitleBarBackground(g);
			this.OnPaintTitleBar(g);

		
			using (SolidBrush brush = new SolidBrush(this.BorderColor))
			{
			
				g.FillRectangle(brush, 
					this.Border.Left, 
					this.HeaderHeight, 
					width - this.Border.Left - this.Border.Right, 
					this.Border.Top); 
				
			
				g.FillRectangle(brush, 
					0, 
					this.HeaderHeight, 
					this.Border.Left, 
					height - this.HeaderHeight); 
				
				g.FillRectangle(brush, 
					width - this.Border.Right, 
					this.HeaderHeight, 
					this.Border.Right, 
					height - this.HeaderHeight); 
				
				
				g.FillRectangle(brush, 
					this.Border.Left, 
					height - this.Border.Bottom, 
					width - this.Border.Left - this.Border.Right, 
					this.Border.Bottom); 
			}

			
			using (SolidBrush brush = new SolidBrush(this.BackColor))
			{
				g.FillRectangle(brush, 
					this.Border.Left, 
					this.HeaderHeight, 
					width - this.Border.Left - this.Border.Right,
					height - this.HeaderHeight - this.Border.Bottom - this.Border.Top);
			}
			
			if (this.BackImage != null)
			{
				
				using (TextureBrush brush = new TextureBrush(this.BackImage, WrapMode.Tile))
				{
					g.FillRectangle(brush, 
						this.Border.Left, 
						this.HeaderHeight, 
						width - this.Border.Left - this.Border.Right,
						height - this.HeaderHeight - this.Border.Bottom - this.Border.Top);
				}
			}

			
			if (this.Watermark != null)
			{
				
				Rectangle rect = new Rectangle(0, 0, this.Watermark.Width, this.Watermark.Height);
				rect.X = width - this.Border.Right - this.Watermark.Width;
				rect.Y = height - this.Border.Bottom - this.Watermark.Height;

			
				
				if (rect.X < 0)
				{
					rect.X = 0;
				}

				if (rect.Width > this.ClientRectangle.Width)
				{
					rect.Width = this.ClientRectangle.Width;
				}

				if (rect.Y < this.DisplayRectangle.Top)
				{
					rect.Y = this.DisplayRectangle.Top;
				}

				if (rect.Height > this.DisplayRectangle.Height)
				{
					rect.Height = this.DisplayRectangle.Height;
				}

			
				g.DrawImage(this.Watermark, rect);
			}

	
			g.Dispose();


	
			this.dummyPanel.Size = new Size(width, height);
			this.dummyPanel.HeaderHeight = this.HeaderHeight;
			this.dummyPanel.Border = this.Border;
			
		
			this.dummyPanel.BackImage = backImage;


			while (this.Controls.Count > 0)
			{
				Control control = this.Controls[0];

				this.Controls.RemoveAt(0);
				this.dummyPanel.Controls.Add(control);

				control.Visible = !this.hiddenControls.Contains(control);
			}
			this.Controls.Add(this.dummyPanel);


		
			Image image = new Bitmap(width, height);

		
			g = Graphics.FromImage(image);
			IntPtr hDC = g.GetHdc();


			IntPtr flags = (IntPtr) (WmPrintFlags.PRF_CLIENT | WmPrintFlags.PRF_CHILDREN);
			
		
			NativeMethods.SendMessage(this.dummyPanel.Handle, WindowMessageFlags.WM_PRINT, hDC, flags);

		
			g.ReleaseHdc(hDC);
			g.Dispose();

			this.Controls.Remove(this.dummyPanel);

		
			while (this.dummyPanel.Controls.Count > 0)
			{
				Control control = this.dummyPanel.Controls[0];

				control.Visible = false;
				
				this.dummyPanel.Controls.RemoveAt(0);
				this.Controls.Add(control);
			}

			this.dummyPanel.BackImage = null;
			backImage.Dispose();

			return image;
		}


		
		internal void CalcAnimationHeights()
		{
			
			using (Bitmap bitmap = new Bitmap(this.fadeHeights.Length, this.ExpandedHeight - this.HeaderHeight))
			{
			
				using (Graphics g = Graphics.FromImage(bitmap))
				{
					g.Clear(Color.White);
					g.DrawBezier(new Pen(Color.Black),
						0,
						bitmap.Height - 1,
						bitmap.Width / 4 * 3,
						bitmap.Height / 4 * 3,
						bitmap.Width / 4,
						bitmap.Height / 4,
						bitmap.Width - 1,
						0);
				}

				
				for (int i=0; i<bitmap.Width; i++)
				{
					int j = bitmap.Height - 1;

					for (; j>0; j--)
					{
						if (bitmap.GetPixel(i, j).R == 0)
						{
							break;
						}
					}

					this.fadeHeights[i] = j;
				}
			}
		}

		#endregion

		#region Slide Show/Hide

		
		protected internal void StartSlideAnimation()
		{
			this.animatingSlide = true;
			
			this.slideEndHeight = this.CalcHeightAndLayout();
		}


		protected internal void UpdateSlideAnimation(int animationStepNum, int numAnimationSteps)
		{
			
			double step = (1.0 - Math.Cos(Math.PI * (double) animationStepNum / (double) numAnimationSteps)) / 2.0;
			
			
			this.Height = this.expandedHeight + (int) ((this.slideEndHeight - this.expandedHeight) * step);

			if (this.TaskPane != null)
			{
				this.TaskPane.DoLayout();
			}
			else
			{
				this.Invalidate();
			}
		}


		protected internal void StopSlideAnimation()
		{
			this.animatingSlide = false;

			
			this.Height = this.slideEndHeight;
			this.slideEndHeight = -1;

			this.DoLayout();
		}

		#endregion


		#region Controls


		public void HideControl(Control control)
		{
			this.HideControl(new Control[] {control});
		}



		public void HideControl(Control[] controls)
		{
			
			if (this.Animating || this.Collapsed)
			{
				return;
			}
			
			this.SuspendLayout();
			
			
			bool anyHidden = false;
			
			foreach (Control control in controls)
			{
				
				if (this.Controls.Contains(control) && !this.hiddenControls.Contains(control))
				{
					anyHidden = true;

					control.Visible = false;
					this.hiddenControls.Add(control);
				}
			}

			this.ResumeLayout(false);

			if (!anyHidden)
			{
				return;
			}

			if (this.beginUpdateCount > 0)
			{
				this.slideAnimationBatched = true;
				
				return;
			}
			
			
			if (!this.AutoLayout || !this.Animate)
			{
				
				this.DoLayout();
			}
			else
			{
				if (this.animationHelper != null)
				{
					this.animationHelper.Dispose();
					this.animationHelper = null;
				}

				this.animationHelper = new AnimationHelper(this, AnimationHelper.SlideAnimation);

				this.animationHelper.StartAnimation();
			}
		}


		public void ShowControl(Control control)
		{
			this.ShowControl(new Control[] {control});
		}


		
		public void ShowControl(Control[] controls)
		{
		
			if (this.Animating || this.Collapsed)
			{
				return;
			}
			
			this.SuspendLayout();
			
			bool anyHidden = false;
			
			foreach (Control control in controls)
			{
			
				if (this.Controls.Contains(control) && this.hiddenControls.Contains(control))
				{
					anyHidden = true;

					control.Visible = true;
					this.hiddenControls.Remove(control);
				}
			}

			this.ResumeLayout(false);

			if (!anyHidden)
			{
				return;
			}

			
			if (this.beginUpdateCount > 0)
			{
				this.slideAnimationBatched = true;
				
				return;
			}

			if (!this.AutoLayout || !this.Animate)
			{
				
				this.DoLayout();
			}
			else
			{
				if (this.animationHelper != null)
				{
					this.animationHelper.Dispose();
					this.animationHelper = null;
				}

				this.animationHelper = new AnimationHelper(this, AnimationHelper.SlideAnimation);

				this.animationHelper.StartAnimation();
			}
		}

		#endregion

		#region Dispose

	
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}

				if (this.systemSettings != null)
				{
					this.systemSettings.Dispose();
					this.systemSettings = null;
				}

				if (this.animationHelper != null)
				{
					this.animationHelper.Dispose();
					this.animationHelper = null;
				}
			}

			base.Dispose(disposing);
		}

		#endregion
		
		#region Invalidation

		protected void InvalidateTitleBar()
		{
			this.Invalidate(new Rectangle(0, 0, this.Width, this.headerHeight), false);
		}

		#endregion

		#region ISupportInitialize Members

		public void BeginInit()
		{
			this.initialising = true;
		}


		public void EndInit()
		{
			this.initialising = false;

			this.DoLayout();

			this.CalcAnimationHeights();
		}


		
		[Browsable(false)]
		public bool Initialising
		{
			get
			{
				return this.initialising;
			}
		}

		#endregion

		#region Keys

	
		protected override bool ProcessDialogKey(Keys keyData)
		{
			if (this.UseDefaultTabHandling || this.Parent == null || !(this.Parent is TaskPane))
			{
				return base.ProcessDialogKey(keyData);
			}
			
			Keys key = keyData & Keys.KeyCode;

			if (key != Keys.Tab)
			{
				switch (key)
				{
					case Keys.Left:
					case Keys.Up:
					case Keys.Right:
					case Keys.Down:
					{
						if (this.ProcessArrowKey(((key == Keys.Right) ? true : (key == Keys.Down))))
						{
							return true;
						}
						
						break;
					}
				}

				return base.ProcessDialogKey(keyData);
			}
			
			if (key == Keys.Tab)
			{
				if (this.ProcessTabKey(((keyData & Keys.Shift) == Keys.None)))
				{
					return true;
				}
			}
			
			return base.ProcessDialogKey(keyData);
		}


		protected virtual bool ProcessTabKey(bool forward)
		{
			if (forward)
			{
				if ((this.Focused && !this.Collapsed) || this.Items.Count == 0)
				{
					return base.SelectNextControl(this, forward, true, true, false);
				}
				else
				{
					return this.Parent.SelectNextControl(this.Items[this.Items.Count-1], forward, true, true, false);
				}
			}
			else
			{
				if (this.Focused || this.Items.Count == 0 || this.Collapsed)
				{
					return this.Parent.SelectNextControl(this, forward, true, true, false);
				}
				else
				{
					this.Select();
					
					return this.Focused;
				}
			}
		}


		protected virtual bool ProcessArrowKey(bool forward)
		{
			if (forward)
			{
				if (this.Focused && !this.Collapsed)
				{
					return base.SelectNextControl(this, forward, true, true, false);
				}
				else if ((this.Items.Count > 0 && this.Items[this.Items.Count-1].Focused) || this.Collapsed)
				{
					int index = this.TaskPane.Expandos.IndexOf(this);
					
					if (index < this.TaskPane.Expandos.Count-1)
					{
						this.TaskPane.Expandos[index+1].Select();

						return this.TaskPane.Expandos[index+1].Focused;
					}
					else
					{
						return true;
					}
				}
			}
			else
			{
				if (this.Focused)
				{
					int index = this.TaskPane.Expandos.IndexOf(this);
					
					if (index > 0)
					{
						return this.Parent.SelectNextControl(this, forward, true, true, false);
					}
					else
					{
						return true;
					}
				}
				else if (this.Items.Count > 0)
				{
					if (this.Items[0].Focused)
					{
						this.Select();
					
						return this.Focused;
					}
					else
					{
						return this.Parent.SelectNextControl(this.FindFocusedChild(), forward, true, true, false);
					}
				}
			}

			return false;
		}


		protected Control FindFocusedChild()
		{
			if (this.Controls.Count == 0)
			{
				return null;
			}

			foreach (Control control in this.Controls)
			{
				if (control.ContainsFocus)
				{
					return control;
				}
			}

			return null;
		}

		#endregion

	
		public void BeginUpdate()
		{
			this.beginUpdateCount++;
		}


		
		public void EndUpdate()
		{
			this.beginUpdateCount = Math.Max(--this.beginUpdateCount, 0);

			if (beginUpdateCount == 0)
			{
				if (this.slideAnimationBatched)
				{
					this.slideAnimationBatched = false;

					if (this.Animate && this.AutoLayout)
					{
						if (this.animationHelper != null)
						{
							this.animationHelper.Dispose();
							this.animationHelper = null;
						}

						this.animationHelper = new AnimationHelper(this, AnimationHelper.SlideAnimation);

						this.animationHelper.StartAnimation();
					}
					else
					{
						this.DoLayout(true);
					}
				}
				else
				{
					this.DoLayout(true);
				}
			}
		}


	
		public void DoLayout()
		{
			this.DoLayout(true);
		}


		public virtual void DoLayout(bool performRealLayout)
		{
			if (this.layout)
			{
				return;
			}

			this.layout = true;
			
			this.SuspendLayout();

			if (this.titleImage != null)
			{
				
				if (this.titleImage.Height > this.titleBarHeight)
				{
					this.headerHeight = this.titleImage.Height;
				}
					
				else if (this.titleImage.Height < this.titleBarHeight)
				{
					this.headerHeight = this.titleBarHeight;
				}
					
				else if (this.titleImage.Height < this.headerHeight)
				{
					this.headerHeight = this.titleImage.Height;
				}
			}
			else
			{
				this.headerHeight = this.titleBarHeight;
			}

			
			if (this.AutoLayout)
			{
				Control c;
				Point p;

				
				int y = this.DisplayRectangle.Y + this.Padding.Top;
				int width = this.PseudoClientRect.Width - this.Padding.Left - this.Padding.Right;

				
				for (int i=0; i<this.itemList.Count; i++)
				{
					c = (Control) this.itemList[i];

					if (this.hiddenControls.Contains(c))
					{
						continue;
					}

					
					p = new Point(this.Padding.Left, y);

				}

				if (this.Collapsed)
				{
					this.Height = this.HeaderHeight;
				}

				this.ResumeLayout(performRealLayout);

				this.layout = false;
			}
		}


	
		internal int CalcHeightAndLayout()
		{
			
			this.SuspendLayout();

		
			if (this.titleImage != null)
			{
				
				if (this.titleImage.Height > this.titleBarHeight)
				{
					this.headerHeight = this.titleImage.Height;
				}
					
				else if (this.titleImage.Height < this.titleBarHeight)
				{
					this.headerHeight = this.titleBarHeight;
				}
					
				else if (this.titleImage.Height < this.headerHeight)
				{
					this.headerHeight = this.titleImage.Height;
				}
			}
			else
			{
				this.headerHeight = this.titleBarHeight;
			}

			int y = -1;

			if (this.AutoLayout)
			{
				Control c;
				Point p;

			
				y = this.DisplayRectangle.Y + this.Padding.Top;
				int width = this.PseudoClientRect.Width - this.Padding.Left - this.Padding.Right;

			
				for (int i=0; i<this.itemList.Count; i++)
				{
					c = (Control) this.itemList[i];

					if (this.hiddenControls.Contains(c))
					{
						continue;
					}

		
					p = new Point(this.Padding.Left, y);

				}

			
				y += this.Padding.Bottom + this.Border.Bottom;
			}

			this.ResumeLayout(true);

			return y;
		}


	
		internal void UpdateItems()
		{
			if (this.Items.Count == this.Controls.Count)
			{
				
				this.MatchControlCollToItemColl();				
				
				return;
			}

			if (this.Items.Count > this.Controls.Count)
			{
				for (int i=0; i<this.Items.Count; i++)
				{
					if (!this.Controls.Contains(this.Items[i]))
					{
						this.OnItemAdded(new ControlEventArgs(this.Items[i]));
					}
				}
			}
			else
			{
				
				int i = 0;
				Control control;

				
				while (i < this.Controls.Count)
				{
					control = (Control) this.Controls[i];
					
					if (!this.Items.Contains(control))
					{
						this.OnItemRemoved(new ControlEventArgs(control));
					}
					else
					{
						i++;
					}
				}
			}

			this.Invalidate(true);
		}


	
		internal void MatchControlCollToItemColl()
		{
			this.SuspendLayout();
				
			for (int i=0; i<this.Items.Count; i++)
			{
				this.Controls.SetChildIndex(this.Items[i], i);
			}

			this.ResumeLayout(false);
				
			this.DoLayout();

			this.Invalidate(true);
		}


	
		[Obsolete]
		protected override void ScaleCore(float dx, float dy)
		{
			

			base.ScaleCore(dx, dy);

			this.expandedHeight = (int)(expandedHeight * dy);
		}

	

		[Browsable(false)]
		public ContentAlignment TitleAlignment
		{
			get
			{
				if (this.SpecialGroup)
				{
					if (this.CustomHeaderSettings.SpecialAlignment != ContentAlignment.MiddleLeft)
					{		
						return this.CustomHeaderSettings.SpecialAlignment;
					}

					return this.SystemSettings.Header.SpecialAlignment;
				}
				
				if (this.CustomHeaderSettings.NormalAlignment != ContentAlignment.MiddleLeft)
				{		
					return this.CustomHeaderSettings.NormalAlignment;
				}

				return this.SystemSettings.Header.NormalAlignment;
			}
		}

	

		[Category("Appearance"), 
		DefaultValue(false),
		Description("Specifies whether the Expando is allowed to animate")]
		public bool Animate
		{
			get
			{
				return this.animate;
			}

			set
			{
				if (this.animate != value)
				{
					this.animate = value;
				}
			}
		}


		[Browsable(false)]
		public bool Animating
		{
			get
			{
				return (this.animatingFade || this.animatingSlide);
			}
		}


		
		protected Image AnimationImage
		{
			get
			{
				return this.animationImage;
			}
		}


		protected int SlideEndHeight
		{
			get
			{
				return this.slideEndHeight;
			}
		}

		

		
		[Browsable(false)]
		public Border Border
		{
			get
			{
				if (this.SpecialGroup)
				{
					if (this.CustomSettings.SpecialBorder != Border.Empty)
					{
						return this.CustomSettings.SpecialBorder;
					}

					return this.SystemSettings.Expando.SpecialBorder;
				}

				if (this.CustomSettings.NormalBorder != Border.Empty)
				{
					return this.CustomSettings.NormalBorder;
				}

				return this.SystemSettings.Expando.NormalBorder;
			}
		}


	
		[Browsable(false)]
		public Color BorderColor
		{
			get
			{
				if (this.SpecialGroup)
				{
					if (this.CustomSettings.SpecialBorderColor != Color.Empty)
					{
						return this.CustomSettings.SpecialBorderColor;
					}

					return this.SystemSettings.Expando.SpecialBorderColor;
				}

				if (this.CustomSettings.NormalBorderColor != Color.Empty)
				{
					return this.CustomSettings.NormalBorderColor;
				}

				return this.SystemSettings.Expando.NormalBorderColor;
			}
		}


	
		[Browsable(false)]
		public Border TitleBorder
		{
			get
			{
				if (this.SpecialGroup)
				{
					if (this.CustomHeaderSettings.SpecialBorder != Border.Empty)
					{
						return this.CustomHeaderSettings.SpecialBorder;
					}

					return this.SystemSettings.Header.SpecialBorder;
				}

				if (this.CustomHeaderSettings.NormalBorder != Border.Empty)
				{
					return this.CustomHeaderSettings.NormalBorder;
				}

				return this.SystemSettings.Header.NormalBorder;
			}
		}


		[Browsable(false)]
		public Color TitleBackColor
		{
			get
			{
				if (this.SpecialGroup)
				{
					if (this.CustomHeaderSettings.SpecialBackColor != Color.Empty && 
						this.CustomHeaderSettings.SpecialBackColor != Color.Transparent)
					{
						return this.CustomHeaderSettings.SpecialBackColor;
					}
					else if (this.CustomHeaderSettings.SpecialBorderColor != Color.Empty)
					{
						return this.CustomHeaderSettings.SpecialBorderColor;
					}

					if (this.SystemSettings.Header.SpecialBackColor != Color.Transparent)
					{
						return this.systemSettings.Header.SpecialBackColor;
					}
					
					return this.SystemSettings.Header.SpecialBorderColor;
				}

				if (this.CustomHeaderSettings.NormalBackColor != Color.Empty && 
					this.CustomHeaderSettings.NormalBackColor != Color.Transparent)
				{
					return this.CustomHeaderSettings.NormalBackColor;
				}
				else if (this.CustomHeaderSettings.NormalBorderColor != Color.Empty)
				{
					return this.CustomHeaderSettings.NormalBorderColor;
				}

				if (this.SystemSettings.Header.NormalBackColor != Color.Transparent)
				{
					return this.systemSettings.Header.NormalBackColor;
				}
					
				return this.SystemSettings.Header.NormalBorderColor;
			}
		}


		protected bool AnyCustomTitleGradientsEmpty
		{
			get
			{
				if (this.SpecialGroup)
				{
					if (this.CustomHeaderSettings.SpecialGradientStartColor == Color.Empty)
					{
						return true;
					}
					else if (this.CustomHeaderSettings.SpecialGradientEndColor == Color.Empty)
					{
						return true;
					}
				}
				else
				{
					if (this.CustomHeaderSettings.NormalGradientStartColor == Color.Empty)
					{
						return true;
					}
					else if (this.CustomHeaderSettings.NormalGradientEndColor == Color.Empty)
					{
						return true;
					}
				}

				return false;
			}
		}

	
		protected Rectangle PseudoClientRect
		{
			get
			{
				return new Rectangle(this.Border.Left, 
					this.HeaderHeight + this.Border.Top,
					this.Width - this.Border.Left - this.Border.Right,
					this.Height - this.HeaderHeight - this.Border.Top - this.Border.Bottom);
			}
		}


		protected int PseudoClientHeight
		{	
			get
			{
				return this.Height - this.HeaderHeight - this.Border.Top - this.Border.Bottom;
			}
		}

	
		[Browsable(false)]
		public override Rectangle DisplayRectangle
		{
			get
			{
				return new Rectangle(this.Border.Left, 
					this.HeaderHeight + this.Border.Top,
					this.Width - this.Border.Left - this.Border.Right,
					this.ExpandedHeight - this.HeaderHeight - this.Border.Top - this.Border.Bottom);
			}
		}


		protected Rectangle TitleBarRectangle
		{
			get
			{
				return new Rectangle(0,
					this.HeaderHeight - this.TitleBarHeight,
					this.Width,
					this.TitleBarHeight);
			}
		}

		
		[Category("Appearance"),
		DefaultValue(false),
		Description("Determines whether the Expando should display a focus rectangle.")]
		public new bool ShowFocusCues
		{
			get
			{
				return this.showFocusCues;
			}

			set
			{
				if (this.showFocusCues != value)
				{
					this.showFocusCues = value;

					if (this.Focused)
					{
						this.InvalidateTitleBar();
					}
				}
			}
		}


		
		[Category("Appearance"), 
		DefaultValue(true),
		Description("Specifies whether the Expando should use Windows default Tab handling mechanism")]
		public bool UseDefaultTabHandling
		{
			get
			{
				return this.useDefaultTabHandling;
			}

			set
			{
				this.useDefaultTabHandling = value;
			}
		}

		
		[Browsable(false)]
		public Color TitleForeColor
		{
			get
			{
				if (this.SpecialGroup)
				{
					if (this.CustomHeaderSettings.SpecialTitleColor != Color.Empty)
					{
						return this.CustomHeaderSettings.SpecialTitleColor;
					}

					return this.SystemSettings.Header.SpecialTitleColor;
				}

				if (this.CustomHeaderSettings.NormalTitleColor != Color.Empty)
				{
					return this.CustomHeaderSettings.NormalTitleColor;
				}

				return this.SystemSettings.Header.NormalTitleColor;
			}
		}


		[Browsable(false)]
		public Color TitleHotForeColor
		{
			get
			{
				if (this.SpecialGroup)
				{
					if (this.CustomHeaderSettings.SpecialTitleHotColor != Color.Empty)
					{
						return this.CustomHeaderSettings.SpecialTitleHotColor;
					}

					return this.SystemSettings.Header.SpecialTitleHotColor;
				}

				if (this.CustomHeaderSettings.NormalTitleHotColor != Color.Empty)
				{
					return this.CustomHeaderSettings.NormalTitleHotColor;
				}

				return this.SystemSettings.Header.NormalTitleHotColor;
			}
		}


		[Browsable(false)]
		public Color TitleColor
		{
			get
			{
				if (this.FocusState == FocusStates.Mouse)
				{
					return this.TitleHotForeColor;
				}

				return this.TitleForeColor;
			}
		}


		[Browsable(false)]
		public Font TitleFont
		{
			get
			{
				if (this.CustomHeaderSettings.TitleFont != null)
				{
					return this.CustomHeaderSettings.TitleFont;
				}

				return this.SystemSettings.Header.TitleFont;
			}
		}

		[Browsable(false)]
		public Image ArrowImage
		{
			get
			{
			
				if(!this.CanCollapse)
				{
					return null;
				}
				
				if (this.SpecialGroup)
				{
					if (this.collapsed)
					{
						if (this.FocusState == FocusStates.None)
						{
							if (this.CustomHeaderSettings.SpecialArrowDown != null)
							{
								return this.CustomHeaderSettings.SpecialArrowDown;
							}

							return this.SystemSettings.Header.SpecialArrowDown;
						}
						else
						{
							if (this.CustomHeaderSettings.SpecialArrowDownHot != null)
							{
								return this.CustomHeaderSettings.SpecialArrowDownHot;
							}

							return this.SystemSettings.Header.SpecialArrowDownHot;
						}
					}
					else
					{
						if (this.FocusState == FocusStates.None)
						{
							if (this.CustomHeaderSettings.SpecialArrowUp != null)
							{
								return this.CustomHeaderSettings.SpecialArrowUp;
							}

							return this.SystemSettings.Header.SpecialArrowUp;
						}
						else
						{
							if (this.CustomHeaderSettings.SpecialArrowUpHot != null)
							{
								return this.CustomHeaderSettings.SpecialArrowUpHot;
							}

							return this.SystemSettings.Header.SpecialArrowUpHot;
						}
					}
				}
				else
				{
					if (this.collapsed)
					{
						if (this.FocusState == FocusStates.None)
						{
							if (this.CustomHeaderSettings.NormalArrowDown != null)
							{
								return this.CustomHeaderSettings.NormalArrowDown;
							}

							return this.SystemSettings.Header.NormalArrowDown;
						}
						else
						{
							if (this.CustomHeaderSettings.NormalArrowDownHot != null)
							{
								return this.CustomHeaderSettings.NormalArrowDownHot;
							}

							return this.SystemSettings.Header.NormalArrowDownHot;
						}
					}
					else
					{
						if (this.FocusState == FocusStates.None)
						{
							if (this.CustomHeaderSettings.NormalArrowUp != null)
							{
								return this.CustomHeaderSettings.NormalArrowUp;
							}

							return this.SystemSettings.Header.NormalArrowUp;
						}
						else
						{
							if (this.CustomHeaderSettings.NormalArrowUpHot != null)
							{
								return this.CustomHeaderSettings.NormalArrowUpHot;
							}

							return this.SystemSettings.Header.NormalArrowUpHot;
						}
					}
				}
			}
		}


		protected int ArrowImageWidth
		{
			get
			{
				if (this.ArrowImage == null)
				{
					return 0;
				}

				return this.ArrowImage.Width;
			}
		}


		protected int ArrowImageHeight
		{
			get
			{
				if (this.ArrowImage == null)
				{
					return 0;
				}
			
				return this.ArrowImage.Height;
			}
		}


		
		[Browsable(false)]
		public Image TitleBackImage
		{
			get
			{
				if (this.SpecialGroup)
				{
					if (this.CustomHeaderSettings.SpecialBackImage != null)
					{
						return this.CustomHeaderSettings.SpecialBackImage;
					}

					return this.SystemSettings.Header.SpecialBackImage;
				}

				if (this.CustomHeaderSettings.NormalBackImage != null)
				{
					return this.CustomHeaderSettings.NormalBackImage;
				}

				return this.SystemSettings.Header.NormalBackImage;
			}
		}


		protected int TitleBackImageHeight
		{
			get
			{
				return this.SystemSettings.Header.BackImageHeight;
			}
		}


		[Category("Appearance"),
		DefaultValue(null),
		Description("The image used on the left side of the Title Bar.")]
		public Image TitleImage
		{
			get
			{
				return this.titleImage;
			}

			set
			{
				this.titleImage = value;

				this.DoLayout();

				this.InvalidateTitleBar();

				OnTitleImageChanged(new ExpandoEventArgs(this));
			}
		}


		protected int TitleImageWidth
		{
			get
			{
				if (this.TitleImage == null)
				{
					return 0;
				}
	
				return this.TitleImage.Width;
			}
		}


		protected int TitleImageHeight
		{
			get
			{
				if (this.TitleImage == null)
				{
					return 0;
				}
			
				return this.TitleImage.Height;
			}
		}


		[Category("Appearance"),
		DefaultValue(null),
		Description("The Image used as a watermark in the client area of the Expando.")]
		public Image Watermark
		{
			get
			{
				return this.watermark;
			}

			set
			{
				if (this.watermark != value)
				{
					this.watermark = value;

					this.Invalidate();

					OnWatermarkChanged(new ExpandoEventArgs(this));
				}
			}
		}


		[Browsable(false)]
		public Image BackImage
		{
			get
			{
				if (this.SpecialGroup)
				{
					if (this.CustomSettings.SpecialBackImage != null)
					{
						return this.CustomSettings.SpecialBackImage;
					}

					return this.SystemSettings.Expando.SpecialBackImage;
				}

				if (this.CustomSettings.NormalBackImage != null)
				{
					return this.CustomSettings.NormalBackImage;
				}

				return this.SystemSettings.Expando.NormalBackImage;
			}
		}

	
		[Category("Behavior"),
		DefaultValue(null), 
		Description("The Controls contained in the Expando"), 
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content), 
		Editor(typeof(ItemCollectionEditor), typeof(UITypeEditor))]
		public Expando.ItemCollection Items
		{
			get
			{
				return this.itemList;
			}
		}


		
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Control.ControlCollection Controls
		{
			get
			{
				return base.Controls;
			}
		}

		[Bindable(true),
		Category("Layout"),
		DefaultValue(false),
		Description("The AutoLayout property determines whether the Expando will automagically layout its items.")]
		public bool AutoLayout
		{
			get
			{
				return this.autoLayout;
			}

			set
			{
				this.autoLayout = value;

				if (this.autoLayout)
				{
					this.DoLayout();
				}
			}
		}

		

	
		[Browsable(false)]
		public Padding Padding
		{
			get
			{
				if (this.SpecialGroup)
				{
					if (this.CustomSettings.SpecialPadding != Padding.Empty)
					{
						return this.CustomSettings.SpecialPadding;
					}

					return this.SystemSettings.Expando.SpecialPadding;
				}

				if (this.CustomSettings.NormalPadding != Padding.Empty)
				{
					return this.CustomSettings.NormalPadding;
				}

				return this.SystemSettings.Expando.NormalPadding;
			}
		}


		[Browsable(false)]
		public Padding TitlePadding
		{
			get
			{
				if (this.SpecialGroup)
				{
					if (this.CustomHeaderSettings.SpecialPadding != Padding.Empty)
					{
						return this.CustomHeaderSettings.SpecialPadding;
					}

					return this.SystemSettings.Header.SpecialPadding;
				}

				if (this.CustomHeaderSettings.NormalPadding != Padding.Empty)
				{
					return this.CustomHeaderSettings.NormalPadding;
				}

				return this.SystemSettings.Header.NormalPadding;
			}
		}

	
		public new Size Size
		{
			get
			{
				return base.Size;
			}

			set
			{
				if (!this.Size.Equals(value))
				{
					if (!this.Animating)
					{
						this.Width = value.Width;

						if (!this.Initialising)
						{
							this.ExpandedHeight = value.Height;
						}
					}
				}
			}
		}


		
		private bool ShouldSerializeSize()
		{
			return this.TaskPane != null;
		}

		
		
		[Bindable(true),
		Category("Layout"),
		DefaultValue(100),
		Description("The height of the Expando in its expanded state.")]
		public int ExpandedHeight
		{
			get
			{
				return this.expandedHeight;
			}

			set
			{
				this.expandedHeight = value;

				this.CalcAnimationHeights();
						
				if (!this.Collapsed && !this.Animating)
				{
					this.Height = this.expandedHeight;

					if (this.TaskPane != null)
					{
						this.TaskPane.DoLayout();
					}
				}
			}
		}


		protected int HeaderHeight
		{
			get
			{
				return this.headerHeight;
			}
		}

		protected int TitleBarHeight
		{
			get
			{
				return this.titleBarHeight;
			}
		}



		[Bindable(true), 
		Category("Appearance"),
		DefaultValue(false),
		Description("The SpecialGroup property determines whether the Expando will be rendered as a SpecialGroup.")]
		public bool SpecialGroup
		{
			get
			{
				return this.specialGroup;
			}

			set
			{
				this.specialGroup = value;

				this.DoLayout();

				if (this.specialGroup)
				{
					if (this.CustomSettings.SpecialBackColor != Color.Empty)
					{
						this.BackColor = this.CustomSettings.SpecialBackColor;
					}
					else
					{
						this.BackColor = this.SystemSettings.Expando.SpecialBackColor;
					}
				}
				else
				{
					if (this.CustomSettings.NormalBackColor != Color.Empty)
					{
						this.BackColor = this.CustomSettings.NormalBackColor;
					}
					else
					{
						this.BackColor = this.SystemSettings.Expando.NormalBackColor;
					}
				}
				
				this.Invalidate();

				OnSpecialGroupChanged(new ExpandoEventArgs(this));
			}
		}

		[Bindable(true), 
		Category("Appearance"),
		DefaultValue(false),
		Description("The Collapsed property determines whether the Expando is collapsed.")]
		public bool Collapsed
		{
			get
			{
				return this.collapsed;
			}

			set
			{
				if (this.collapsed != value)
				{
				
					if (value && !this.CanCollapse)
					{
						
						return;
					}
					
					this.collapsed = value;

			
					if (this.Animate && !this.DesignMode && !this.Initialising)
					{
						if (this.animationHelper != null)
						{
							this.animationHelper.Dispose();
							this.animationHelper = null;
						}
							
						this.animationHelper = new AnimationHelper(this, AnimationHelper.FadeAnimation);

						this.OnStateChanged(new ExpandoEventArgs(this));

						this.animationHelper.StartAnimation();
					}
					else
					{
						if (this.collapsed)
						{
							this.Collapse();
						}
						else
						{
							this.Expand();
						}


					}
				}
			}
		}


		[Browsable(false)]
		protected internal FocusStates FocusState
		{
			get
			{
				return this.focusState;
			}

			set
			{
				
				if (!this.CanCollapse)
				{
					value = FocusStates.None;
				}
				
				if (this.focusState != value)
				{
					this.focusState = value;

					this.InvalidateTitleBar();

					if (this.focusState == FocusStates.Mouse)
					{
						this.Cursor = Cursors.Hand;
					}
					else
					{
						this.Cursor = Cursors.Default;
					}
				}
			}
		}


		[Bindable(true), 
		Category("Behavior"),
		DefaultValue(true),
		Description("The CanCollapse property determines whether the Expando is able to collapse.")]
		public bool CanCollapse
		{
			get
			{ 
				return this.canCollapse; 
			}
			
			set
			{ 
				if (this.canCollapse != value)
				{
					this.canCollapse = value; 

				
					if (!this.canCollapse && this.Collapsed)
					{
						this.Collapsed = false;
					}

					this.InvalidateTitleBar();
				}
			}
		}

		[Browsable(false)]
		protected internal ExplorerBarInfo SystemSettings
		{
			get
			{
				return this.systemSettings;
			}
			
			set
			{
			
				if (this.systemSettings != value)
				{
					this.SuspendLayout();
					
				
					if (this.systemSettings != null)
					{
						this.systemSettings.Dispose();
						this.systemSettings = null;
					}

				
					this.systemSettings = value;

					this.titleBarHeight = this.systemSettings.Header.BackImageHeight;

				
					if (this.titleImage != null)
					{
					
						if (this.titleImage.Height > this.titleBarHeight)
						{
							this.headerHeight = this.titleImage.Height;
						}
							
						else if (this.titleImage.Height < this.titleBarHeight)
						{
							this.headerHeight = this.titleBarHeight;
						}
							
						else if (this.titleImage.Height < this.headerHeight)
						{
							this.headerHeight = this.titleImage.Height;
						}
					}
					else
					{
						this.headerHeight = this.titleBarHeight;
					}

					if (this.SpecialGroup)
					{
						if (this.CustomSettings.SpecialBackColor != Color.Empty)
						{
							this.BackColor = this.CustomSettings.SpecialBackColor;
						}
						else
						{
							this.BackColor = this.SystemSettings.Expando.SpecialBackColor;
						}
					}
					else
					{
						if (this.CustomSettings.NormalBackColor != Color.Empty)
						{
							this.BackColor = this.CustomSettings.NormalBackColor;
						}
						else
						{
							this.BackColor = this.SystemSettings.Expando.NormalBackColor;
						}
					}

				
					for (int i=0; i<this.itemList.Count; i++)
					{
						Control control = (Control) this.itemList[i];
					}

					this.ResumeLayout(false);

				
					if (this.TaskPane == null)
					{
						this.DoLayout();
					}
				}
			}
		}


		[Category("Appearance"),
		Description(""),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		TypeConverter(typeof(ExpandoInfoConverter))]
		public ExpandoInfo CustomSettings
		{
			get
			{
				return this.customSettings;
			}
		}


	
		[Category("Appearance"),
		Description(""),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		TypeConverter(typeof(HeaderInfoConverter))]
		public HeaderInfo CustomHeaderSettings
		{
			get
			{
				return this.customHeaderSettings;
			}
		}


	
		public void ResetCustomSettings()
		{
			this.CustomSettings.SetDefaultEmptyValues();
			this.CustomHeaderSettings.SetDefaultEmptyValues();

			this.FireCustomSettingsChanged(EventArgs.Empty);
		}

		protected internal TaskPane TaskPane
		{
			get
			{
				return this.taskpane;
			}

			set
			{
				this.taskpane = value;

				if (value != null)
				{
					this.SystemSettings = this.TaskPane.SystemSettings;
				}
			}
		}

	
		public override string Text
		{
			get
			{
				return base.Text;
			}

			set
			{
				base.Text = value;

				this.InvalidateTitleBar();
			}
		}

	
		public new bool Visible
		{
			get
			{
				return base.Visible;
			}

			set
			{
				
				if (base.Visible != value || (!value && this.Parent != null && !this.Parent.Visible))
				{
					base.Visible = value;

					if (this.TaskPane != null)
					{
						this.TaskPane.DoLayout();
					}
				}
			}
		}
	
	
		protected override void OnControlAdded(ControlEventArgs e)
		{
			if (this.Animating)
			{
				return;
			}
			
			base.OnControlAdded(e);
	
			if (!this.Items.Contains(e.Control))
			{
				this.Items.Add(e.Control);
			}
		}


	
		protected override void OnControlRemoved(ControlEventArgs e)
		{
			
			if (this.Animating)
			{
				return;
			}
			
			base.OnControlRemoved(e);

			if (this.Items.Contains(e.Control))
			{
				this.Items.Remove(e.Control);
			}

			this.DoLayout();
		}

		internal void FireCustomSettingsChanged(EventArgs e)
		{
			this.titleBarHeight = this.TitleBackImageHeight;

			
			if (this.titleImage != null)
			{
				
				if (this.titleImage.Height > this.titleBarHeight)
				{
					this.headerHeight = this.titleImage.Height;
				}
					
				else if (this.titleImage.Height < this.titleBarHeight)
				{
					this.headerHeight = this.titleBarHeight;
				}
				
				else if (this.titleImage.Height < this.headerHeight)
				{
					this.headerHeight = this.titleImage.Height;
				}
			}
			else
			{
				this.headerHeight = this.titleBarHeight;
			}

			if (this.SpecialGroup)
			{
				if (this.CustomSettings.SpecialBackColor != Color.Empty)
				{
					this.BackColor = this.CustomSettings.SpecialBackColor;
				}
				else
				{
					this.BackColor = this.SystemSettings.Expando.SpecialBackColor;
				}
			}
			else
			{
				if (this.CustomSettings.NormalBackColor != Color.Empty)
				{
					this.BackColor = this.CustomSettings.NormalBackColor;
				}
				else
				{
					this.BackColor = this.SystemSettings.Expando.NormalBackColor;
				}
			}

			this.DoLayout();

			this.Invalidate(true);

			this.OnCustomSettingsChanged(e);
		}


	
		protected virtual void OnCustomSettingsChanged(EventArgs e)
		{
			if (CustomSettingsChanged != null)
			{
				CustomSettingsChanged(this, e);
			}
		}

		protected virtual void OnStateChanged(ExpandoEventArgs e)
		{
			if (StateChanged != null)
			{
				StateChanged(this, e);
			}
		}



		protected virtual void OnTitleImageChanged(ExpandoEventArgs e)
		{
			if (TitleImageChanged != null)
			{
				TitleImageChanged(this, e);
			}
		}


		protected virtual void OnSpecialGroupChanged(ExpandoEventArgs e)
		{
			if (SpecialGroupChanged != null)
			{
				SpecialGroupChanged(this, e);
			}
		}


	
		protected virtual void OnWatermarkChanged(ExpandoEventArgs e)
		{
			if (WatermarkChanged != null)
			{
				WatermarkChanged(this, e);
			}
		}

		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);

			this.InvalidateTitleBar();
		}


		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);

			this.InvalidateTitleBar();
		}

	
		protected virtual void OnItemAdded(ControlEventArgs e)
		{
		
			if (!this.Controls.Contains(e.Control))
			{
				this.Controls.Add(e.Control);
			}


			
			this.DoLayout();

			if (ItemAdded != null)
			{
				ItemAdded(this, e);
			}
		}


	
		protected virtual void OnItemRemoved(ControlEventArgs e)
		{
			
			if (this.Controls.Contains(e.Control))
			{
				this.Controls.Remove(e.Control);
			}

			this.DoLayout();

		
			if (ItemRemoved != null)
			{
				ItemRemoved(this, e);
			}
		}


		
		protected override void OnKeyUp(KeyEventArgs e)
		{
			
			base.OnKeyUp(e);

			if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
			{
				this.Collapsed = !this.Collapsed;
			}
		}


		protected override void OnLocationChanged(EventArgs e)
		{
			base.OnLocationChanged(e);

			if (this.TitleImage != null && this.TitleImageHeight > this.TitleBarHeight)
			{
				this.InvalidateTitleBar();
			}
		}

	
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);

			
			if (e.Button == MouseButtons.Left)
			{
				if (this.dragging)
				{
					this.Cursor = Cursors.Default;

					this.dragging = false;

					this.TaskPane.DropExpando(this);
				}
				else
				{
					
					if (e.Y < this.HeaderHeight && e.Y > (this.HeaderHeight - this.TitleBarHeight))
					{
						
						if (!this.Animating)
						{
							
							this.Collapsed = !this.Collapsed;
						}

						if (this.CanCollapse)
						{
							this.Select();
						}
					}
				}

				this.dragStart = Point.Empty;
			}
		}


		
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);

		

			if (e.Button == MouseButtons.Left)
			{
				if (this.TaskPane != null && this.TaskPane.AllowExpandoDragging && !this.Animating)
				{
					this.dragStart = this.PointToScreen(new Point(e.X, e.Y));
				}
			}
		}


	
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);

			if (e.Button == MouseButtons.Left && this.dragStart != Point.Empty)
			{
				Point p = this.PointToScreen(new Point(e.X, e.Y));

				if (!this.dragging)
				{
					if (Math.Abs(this.dragStart.X - p.X) > 8 || Math.Abs(this.dragStart.Y - p.Y) > 8)
					{
						this.dragging = true;

						this.FocusState = FocusStates.None;
					}
				}

				if (this.dragging)
				{
					if (this.TaskPane.ClientRectangle.Contains(this.TaskPane.PointToClient(p)))
					{
						this.Cursor = Cursors.Default;
					}
					else
					{
						this.Cursor = Cursors.No;
					}

					this.TaskPane.UpdateDropPoint(p);
					
					return;
				}
			}

			
			if (e.Y < this.HeaderHeight && e.Y > (this.HeaderHeight - this.TitleBarHeight))
			{
				
				this.FocusState = FocusStates.Mouse;
			}
			else
			{
			
				this.FocusState = FocusStates.None;
			}
		}


		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);

			this.FocusState = FocusStates.None;
		}

	
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			
			this.PaintTransparentBackground(e.Graphics, e.ClipRectangle);

			
			if (this.TitleBarRectangle.IntersectsWith(e.ClipRectangle))
			{
				this.OnPaintTitleBarBackground(e.Graphics);
			}

			
			if (this.Height != this.headerHeight)
			{
				if (this.PseudoClientRect.IntersectsWith(e.ClipRectangle))
				{
					this.OnPaintBorder(e.Graphics);

					this.OnPaintDisplayRect(e.Graphics);
				}
			}
		}


		
		protected override void OnPaint(PaintEventArgs e)
		{
		
			if (this.TitleBarRectangle.IntersectsWith(e.ClipRectangle))
			{
				this.OnPaintTitleBar(e.Graphics);
			}
		}


		
		protected void OnPaintTitleBarBackground(Graphics g)
		{
			
			int y = 0;
			
			
			if (this.HeaderHeight > this.TitleBarHeight)
			{
				y = this.HeaderHeight - this.TitleBarHeight;
			}

			if (this.CustomHeaderSettings.TitleGradient && !this.AnyCustomTitleGradientsEmpty)
			{
			
				Color start = this.CustomHeaderSettings.NormalGradientStartColor;
				if (this.SpecialGroup)
				{
					start = this.CustomHeaderSettings.SpecialGradientStartColor;
				}

				Color end = this.CustomHeaderSettings.NormalGradientEndColor;
				if (this.SpecialGroup)
				{
					end = this.CustomHeaderSettings.SpecialGradientEndColor;
				}

				if (!this.Enabled)
				{
				
					start = Color.FromArgb((int) (start.GetBrightness() * 255), 
						(int) (start.GetBrightness() * 255), 
						(int) (start.GetBrightness() * 255));
					end = Color.FromArgb((int) (end.GetBrightness() * 255), 
						(int) (end.GetBrightness() * 255), 
						(int) (end.GetBrightness() * 255));
				}

				using (LinearGradientBrush brush = new LinearGradientBrush(this.TitleBarRectangle, start, end, LinearGradientMode.Horizontal))
				{
					
					if (this.CustomHeaderSettings.GradientOffset > 0f && this.CustomHeaderSettings.GradientOffset < 1f)
					{
						ColorBlend colorBlend = new ColorBlend() ;
						colorBlend.Colors = new Color [] {brush.LinearColors[0], brush.LinearColors[0], brush.LinearColors[1]} ;
						colorBlend.Positions = new float [] {0f, this.CustomHeaderSettings.GradientOffset, 1f} ;
						brush.InterpolationColors = colorBlend ;
					}
						
					
					if (this.CustomHeaderSettings.TitleRadius > 0)
					{
						GraphicsPath path = new GraphicsPath();
							
						
						path.AddLine(this.TitleBarRectangle.Left + this.CustomHeaderSettings.TitleRadius, 
							this.TitleBarRectangle.Top, 
							this.TitleBarRectangle.Right - (this.CustomHeaderSettings.TitleRadius * 2) - 1, 
							this.TitleBarRectangle.Top);
							
						
						path.AddArc(this.TitleBarRectangle.Right - (this.CustomHeaderSettings.TitleRadius * 2) - 1, 
							this.TitleBarRectangle.Top, 
							this.CustomHeaderSettings.TitleRadius * 2, 
							this.CustomHeaderSettings.TitleRadius * 2, 
							270, 
							90);
							
						
						path.AddLine(this.TitleBarRectangle.Right, 
							this.TitleBarRectangle.Top + this.CustomHeaderSettings.TitleRadius, 
							this.TitleBarRectangle.Right, 
							this.TitleBarRectangle.Bottom);
							
						
						path.AddLine(this.TitleBarRectangle.Right, 
							this.TitleBarRectangle.Bottom, 
							this.TitleBarRectangle.Left - 1, 
							this.TitleBarRectangle.Bottom);
						
						path.AddArc(this.TitleBarRectangle.Left, 
							this.TitleBarRectangle.Top, 
							this.CustomHeaderSettings.TitleRadius * 2, 
							this.CustomHeaderSettings.TitleRadius * 2, 
							180, 
							90);
							
						g.SmoothingMode = SmoothingMode.AntiAlias;

						g.FillPath(brush, path);

						g.SmoothingMode = SmoothingMode.Default;
					}
					else
					{
						g.FillRectangle(brush, this.TitleBarRectangle);
					}
				}
			}
			else if (this.TitleBackImage != null)
			{
				
				if ((this.RightToLeft == RightToLeft.Yes && !this.SystemSettings.Header.RightToLeft) || 
					(this.RightToLeft == RightToLeft.No && this.SystemSettings.Header.RightToLeft))
				{
					if (this.SystemSettings.Header.NormalBackImage != null)
					{
						this.SystemSettings.Header.NormalBackImage.RotateFlip(RotateFlipType.RotateNoneFlipX);
					}

					if (this.SystemSettings.Header.SpecialBackImage != null)
					{
						this.SystemSettings.Header.SpecialBackImage.RotateFlip(RotateFlipType.RotateNoneFlipX);
					}

					this.SystemSettings.Header.RightToLeft = (this.RightToLeft == RightToLeft.Yes);
				}
					
				if (this.Enabled)
				{
					if (this.SystemSettings.OfficialTheme)
					{
					
						g.DrawImage(this.TitleBackImage, 
							new Rectangle(0, y, 5, this.TitleBarHeight),
							new Rectangle(0, 0, 5, this.TitleBackImage.Height), 
							GraphicsUnit.Pixel);

						
						g.DrawImage(this.TitleBackImage, 
							new Rectangle(this.Width-5, y, 5, this.TitleBarHeight),
							new Rectangle(this.TitleBackImage.Width-5, 0, 5, this.TitleBackImage.Height), 
							GraphicsUnit.Pixel);

			
						g.DrawImage(this.TitleBackImage, 
							new Rectangle(5, y, this.Width-10, this.TitleBarHeight),
							new Rectangle(5, 0, this.TitleBackImage.Width-10, this.TitleBackImage.Height), 
							GraphicsUnit.Pixel);
					}
					else
					{
						g.DrawImage(this.TitleBackImage, 0, y, this.Width, this.TitleBarHeight);
					}
				}
				else
				{
					if (this.SystemSettings.OfficialTheme)
					{
						using (Image image = new Bitmap(this.Width, this.TitleBarHeight))
						{
							using (Graphics g2 = Graphics.FromImage(image))
							{
								
								g2.DrawImage(this.TitleBackImage, 
									new Rectangle(0, y, 5, this.TitleBarHeight),
									new Rectangle(0, 0, 5, this.TitleBackImage.Height), 
									GraphicsUnit.Pixel);
						

							
								g2.DrawImage(this.TitleBackImage, 
									new Rectangle(this.Width-5, y, 5, this.TitleBarHeight),
									new Rectangle(this.TitleBackImage.Width-5, 0, 5, this.TitleBackImage.Height), 
									GraphicsUnit.Pixel);

								g2.DrawImage(this.TitleBackImage, 
									new Rectangle(5, y, this.Width-10, this.TitleBarHeight),
									new Rectangle(5, 0, this.TitleBackImage.Width-10, this.TitleBackImage.Height), 
									GraphicsUnit.Pixel);
							}

							ControlPaint.DrawImageDisabled(g, image, 0, y, this.TitleBackColor);
						}
					}
					else
					{
						
						using (Image image = new Bitmap(this.TitleBackImage, this.Width, this.TitleBarHeight))
						{
							ControlPaint.DrawImageDisabled(g, image, 0, y, this.TitleBackColor);
						}
					}
				}
			}		
			else
			{
				
				using (SolidBrush brush = new SolidBrush(this.TitleBackColor))
				{
					g.FillRectangle(brush, 0, y, this.Width, this.TitleBarHeight);
				}
			}
		}



		protected void OnPaintTitleBar(Graphics g)
		{
			int y = 0;
			
			
			if (this.HeaderHeight > this.TitleBarHeight)
			{
				y = this.HeaderHeight - this.TitleBarHeight;
			}

			
			if (this.TitleImage != null)
			{
				int x = 0;
				
				
				if (this.RightToLeft == RightToLeft.Yes)
				{
					x = this.Width - this.TitleImage.Width;
				}
				
				if (this.Enabled)
				{
					g.DrawImage(this.TitleImage, x, 0);
				}
				else
				{
					ControlPaint.DrawImageDisabled(g, TitleImage, x, 0, this.TitleBackColor);
				}
			}

			
			Image arrowImage = this.ArrowImage;

		
			Border border = this.TitleBorder;
			Padding padding = this.TitlePadding;

		
			if (arrowImage != null)
			{
			
				int x = this.Width - arrowImage.Width - border.Right - padding.Right;
				y += border.Top + padding.Top;

				if (this.RightToLeft == RightToLeft.Yes)
				{
					x = border.Right + padding.Right;
				}

				
				if (this.Enabled)
				{
					g.DrawImage(arrowImage, x, y);
				}
				else
				{
					ControlPaint.DrawImageDisabled(g, arrowImage, x, y, this.TitleBackColor);
				}
			}

			
			if (this.Text.Length > 0)
			{
				
				Rectangle rect = new Rectangle();
				
				
				if (this.TitleImage == null)
				{
					rect.X = border.Left + padding.Left;
				}
				else
				{
					rect.X = this.TitleImage.Width + border.Left;
				}

			
				ContentAlignment alignment = this.TitleAlignment;

				switch (alignment)
				{
					case ContentAlignment.MiddleLeft:
					case ContentAlignment.MiddleCenter:
					case ContentAlignment.MiddleRight:	rect.Y = ((this.HeaderHeight - this.TitleFont.Height) / 2) + ((this.HeaderHeight - this.TitleBarHeight) / 2) + border.Top + padding.Top;
						break;

					case ContentAlignment.TopLeft:
					case ContentAlignment.TopCenter:
					case ContentAlignment.TopRight:		rect.Y = (this.HeaderHeight - this.TitleBarHeight) + border.Top + padding.Top;
						break;

					case ContentAlignment.BottomLeft:
					case ContentAlignment.BottomCenter:
					case ContentAlignment.BottomRight:	rect.Y = this.HeaderHeight - this.TitleFont.Height;
						break;
				}

				rect.Height = this.TitleFont.Height;

				
				if (rect.Bottom > this.HeaderHeight)
				{
					rect.Y -= rect.Bottom - this.HeaderHeight;
				}
					
				
				if (arrowImage != null)
				{
					rect.Width = this.Width - arrowImage.Width - border.Right - padding.Right - rect.X;
				}
				else
				{
					rect.Width = this.Width - border.Right - padding.Right - rect.X;
				}

				
				StringFormat sf = new StringFormat();
				sf.FormatFlags = StringFormatFlags.NoWrap;
				sf.Trimming = StringTrimming.EllipsisCharacter;

				
				switch (alignment)
				{
					case ContentAlignment.MiddleLeft:
					case ContentAlignment.TopLeft:
					case ContentAlignment.BottomLeft:	sf.Alignment = StringAlignment.Near;
						break;

					case ContentAlignment.MiddleCenter:
					case ContentAlignment.TopCenter:
					case ContentAlignment.BottomCenter:	sf.Alignment = StringAlignment.Center;
						break;

					case ContentAlignment.MiddleRight:
					case ContentAlignment.TopRight:
					case ContentAlignment.BottomRight:	sf.Alignment = StringAlignment.Far;
						break;
				}

				if (this.RightToLeft == RightToLeft.Yes)
				{
					sf.FormatFlags |= StringFormatFlags.DirectionRightToLeft;

					if (this.TitleImage == null)
					{
						rect.X = this.Width - rect.Width - border.Left - padding.Left;
					}
					else
					{
						rect.X = this.Width - rect.Width - this.TitleImage.Width - border.Left;
					}
				}

				
				using (SolidBrush brush = new SolidBrush(this.TitleColor))
				{
					
					if (this.Enabled)
					{
						g.DrawString(this.Text, this.TitleFont, brush, rect, sf);
					}
					else
					{
						ControlPaint.DrawStringDisabled(g, this.Text, this.TitleFont, SystemColors.ControlLightLight, rect, sf);
					}
				}
			}

			
			if (this.Focused && base.ShowFocusCues)
			{
				if (this.ShowFocusCues)
				{
					if (!this.CanCollapse)
					{
						y += 2;
					}
					
					ControlPaint.DrawFocusRectangle(g, new Rectangle(2, y, this.Width - 4, this.TitleBarHeight - 3));
				}
			}
		}

	
		protected void OnPaintDisplayRect(Graphics g)
		{
			if (this.animatingFade && this.AnimationImage != null)
			{
				
				float alpha = (((float) (this.Height - this.HeaderHeight)) / ((float) (this.ExpandedHeight - this.HeaderHeight)));
				
				float[][] ptsArray = {new float[] {1, 0, 0, 0, 0},
										 new float[] {0, 1, 0, 0, 0},
										 new float[] {0, 0, 1, 0, 0},
										 new float[] {0, 0, 0, alpha, 0}, 
										 new float[] {0, 0, 0, 0, 1}}; 

				ColorMatrix colorMatrix = new ColorMatrix(ptsArray);
				ImageAttributes imageAttributes = new ImageAttributes();
				imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

				
				int y = this.AnimationImage.Height - this.PseudoClientHeight - this.Border.Bottom;

				g.DrawImage(this.AnimationImage,
					new Rectangle(0, this.HeaderHeight, this.Width, this.Height - this.HeaderHeight),
					0,
					y,
					this.AnimationImage.Width, 
					this.AnimationImage.Height - y,
					GraphicsUnit.Pixel,
					imageAttributes);
			}
			
			else if (this.animatingSlide)
			{
				
				if (this.BackImage != null)
				{
					
					using (TextureBrush brush = new TextureBrush(this.BackImage, WrapMode.Tile))
					{
						g.FillRectangle(brush, this.DisplayRectangle);
					}
				}
				else
				{
					
					using (SolidBrush brush = new SolidBrush(this.BackColor))
					{
						g.FillRectangle(brush, 
							this.Border.Left, 
							this.HeaderHeight + this.Border.Top, 
							this.Width - this.Border.Left - this.Border.Right,
							this.Height - this.HeaderHeight - this.Border.Top - this.Border.Bottom);
					}
				}
			}
			else
			{
				
				if (this.BackImage != null)
				{
					
					using (TextureBrush brush = new TextureBrush(this.BackImage, WrapMode.Tile))
					{
						g.FillRectangle(brush, this.DisplayRectangle);
					}
				}
				else
				{
					
					using (SolidBrush brush = new SolidBrush(this.BackColor))
					{
						g.FillRectangle(brush, this.DisplayRectangle);
					}
				}

				if (this.Watermark != null)
				{
					
					Rectangle rect = new Rectangle(0, 0, this.Watermark.Width, this.Watermark.Height);
					rect.X = this.PseudoClientRect.Right - this.Watermark.Width;
					rect.Y = this.DisplayRectangle.Bottom - this.Watermark.Height;

				
				
					if (rect.X < 0)
					{
						rect.X = 0;
					}

					if (rect.Width > this.ClientRectangle.Width)
					{
						rect.Width = this.ClientRectangle.Width;
					}

					if (rect.Y < this.DisplayRectangle.Top)
					{
						rect.Y = this.DisplayRectangle.Top;
					}

					if (rect.Height > this.DisplayRectangle.Height)
					{
						rect.Height = this.DisplayRectangle.Height;
					}

					
					g.DrawImage(this.Watermark, rect);
				}
			}
		}

	
		protected void OnPaintBorder(Graphics g)
		{
			
			Border border = this.Border;
			Color c = this.BorderColor;

		
			if (this.animatingFade)
			{
			
				int alpha = (int) (255 * (((float) (this.Height - this.HeaderHeight)) / ((float) (this.ExpandedHeight - this.HeaderHeight))));

				
				if (alpha < 0)
				{
					alpha = 0;
				}
				else if (alpha > 255)
				{
					alpha = 255;
				}

				
				c = Color.FromArgb(alpha, c.R, c.G, c.B);
			}
			
		
			using (SolidBrush brush = new SolidBrush(c))
			{
				g.FillRectangle(brush, border.Left, this.HeaderHeight, this.Width-border.Left-border.Right, border.Top); // top border
				g.FillRectangle(brush, 0, this.HeaderHeight, border.Left, this.Height-this.HeaderHeight); // left border
				g.FillRectangle(brush, this.Width-border.Right, this.HeaderHeight, border.Right, this.Height-this.HeaderHeight); // right border
				g.FillRectangle(brush, border.Left, this.Height-border.Bottom, this.Width-border.Left-border.Right, border.Bottom); // bottom border
			}
		}

		
		protected void PaintTransparentBackground(Graphics g, Rectangle clipRect)
		{
			
			if (this.Parent != null)
			{
				
				clipRect.Offset(this.Location);

				PaintEventArgs e = new PaintEventArgs(g, clipRect);

				
				GraphicsState state = g.Save();

				try
				{
					
					g.TranslateTransform((float) -this.Location.X, (float) -this.Location.Y);
					
					
					this.InvokePaintBackground(this.Parent, e);
					this.InvokePaint(this.Parent, e);

					return;
				}
				finally
				{
					
					g.Restore(state);
					clipRect.Offset(-this.Location.X, -this.Location.Y);
				}
			}

			
		
			g.FillRectangle(SystemBrushes.Control, clipRect);
		}

	
		protected override void OnParentChanged(EventArgs e)
		{
			if (this.Parent == null)
			{
				this.TaskPane = null;
			}
			else if (this.Parent is TaskPane)
			{
				this.TaskPane = (TaskPane) this.Parent;

				this.Location = this.TaskPane.CalcExpandoLocation(this);
			}
			
			base.OnParentChanged(e);
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
			
			
			if (this.Animating && this.Width != this.oldWidth)
			{
				if (this.Width == 0)
				{
					this.animationHelper.StopAnimation();
				}
				else
				{
					this.oldWidth = this.Width;
				
					if (this.AnimationImage != null)
					{
						
						this.animationImage = this.GetFadeAnimationImage();
					}
				}
			}
			
			else if (this.Width != this.oldWidth)
			{
				this.oldWidth = this.Width;
				
				this.DoLayout();
			}
		}

	
		public class ItemCollection : CollectionBase
		{
			private Expando owner;

		


			public ItemCollection(Expando owner) : base()
			{
				if (owner == null)
				{
					throw new ArgumentNullException("owner");
				}
				
				this.owner = owner;
			}

		
			public void Add(Control value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}

				this.List.Add(value);
				this.owner.Controls.Add(value);

				this.owner.OnItemAdded(new ControlEventArgs(value));
			}


			
			public void AddRange(Control[] controls)
			{
				if (controls == null)
				{
					throw new ArgumentNullException("controls");
				}

				for (int i=0; i<controls.Length; i++)
				{
					this.Add(controls[i]);
				}
			}
			
			

			public new void Clear()
			{
				while (this.Count > 0)
				{
					this.RemoveAt(0);
				}
			}


	
			public bool Contains(Control control)
			{
				if (control == null)
				{
					throw new ArgumentNullException("control");
				}

				return (this.IndexOf(control) != -1);
			}


		
			public int IndexOf(Control control)
			{
				if (control == null)
				{
					throw new ArgumentNullException("control");
				}
				
				for (int i=0; i<this.Count; i++)
				{
					if (this[i] == control)
					{
						return i;
					}
				}

				return -1;
			}

			public void Remove(Control value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}

				this.List.Remove(value);
				this.owner.Controls.Remove(value);

				this.owner.OnItemRemoved(new ControlEventArgs(value));
			}

			public new void RemoveAt(int index)
			{
				this.Remove(this[index]);
			}

			public void Move(Control value, int index)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}

				if (index < 0)
				{
					index = 0;
				}
				else if (index > this.Count)
				{
					index = this.Count;
				}

	
				if (!this.Contains(value) || this.IndexOf(value) == index)
				{
					return;
				}

				this.List.Remove(value);

			
				if (index > this.Count)
				{
					this.List.Add(value);
				}
				else
				{
					this.List.Insert(index, value);
				}

			
				this.owner.MatchControlCollToItemColl();
			}


		
			public void MoveToTop(Control value)
			{
				this.Move(value, 0);
			}


			public void MoveToBottom(Control value)
			{
				this.Move(value, this.Count);
			}



		
			public virtual Control this[int index]
			{
				get
				{
					return this.List[index] as Control;
				}
			}

			
		}

	

		internal class ItemCollectionEditor : CollectionEditor
		{
			
			public ItemCollectionEditor(Type type) : base(type)
			{
			
			}

			public override object EditValue(ITypeDescriptorContext context, IServiceProvider isp, object value)
			{
				Expando originalControl = (Expando) context.Instance;

				object returnObject = base.EditValue(context, isp, value);

				originalControl.UpdateItems();

				return returnObject;
			}
		}


		internal class AnimationPanel : Panel
		{
			
			protected int headerHeight;
 
			protected Border border;

			
			protected Image backImage;



	

		
			public AnimationPanel() : base()
			{
				this.headerHeight = 0;
				this.border = new Border();
				this.backImage = null;
			}

	


		
			public new bool AutoScroll
			{
				get
				{
					return false;
				}

				set
				{

				}
			}


		
			public int HeaderHeight
			{
				get
				{
					return this.headerHeight;
				}

				set
				{
					this.headerHeight = value;
				}
			}


		
			public Border Border
			{
				get
				{
					return this.border;
				}

				set
				{
					this.border = value;
				}
			}


			public Image BackImage
			{
				get
				{
					return this.backImage;
				}

				set
				{
					this.backImage = value;
				}
			}


			public override Rectangle DisplayRectangle
			{
				get
				{
					return new Rectangle(this.Border.Left, 
						this.HeaderHeight + this.Border.Top,
						this.Width - this.Border.Left - this.Border.Right,
						this.Height - this.HeaderHeight - this.Border.Top - this.Border.Bottom);
				}
			}

	
			protected override void OnPaint(PaintEventArgs e)
			{
				base.OnPaint(e);

				if (this.BackImage != null)
				{
					e.Graphics.DrawImageUnscaled(this.BackImage, 0, 0);
				}
			}
			
		}

		
		public class AnimationHelper : IDisposable
		{
		
			public static readonly int NumAnimationFrames = 23;

			public static int FadeAnimation = 1;
	
			public static int SlideAnimation = 2;

			
			private int animationType;

		
			private Expando expando;

			private int animationStepNum;

		
			private int numAnimationSteps;

		
			private int animationFrameInterval;

			private bool animating;

			private System.Windows.Forms.Timer animationTimer;




			public AnimationHelper(Expando expando, int animationType)
			{
				this.expando = expando;
				this.animationType = animationType;

				this.animating = false;

				this.numAnimationSteps = NumAnimationFrames;
				this.animationFrameInterval = 10;

				this.animationTimer = new System.Windows.Forms.Timer();
				this.animationTimer.Tick += new EventHandler(this.animationTimer_Tick);
				this.animationTimer.Interval = this.animationFrameInterval;
			}

		
			public void Dispose()
			{
				if (this.animationTimer != null)
				{
					this.animationTimer.Stop();
					this.animationTimer.Dispose();
					this.animationTimer = null;
				}

				this.expando = null;
			}

			
			public void StartAnimation()
			{
				
				if (this.Animating)
				{
					return;
				}
			
				this.animationStepNum = 0;

			
				if (this.AnimationType == FadeAnimation)
				{
					this.expando.StartFadeAnimation();
				}
				else
				{
					this.expando.StartSlideAnimation();
				}

				
				this.animationTimer.Start();
			}


			
			protected void PerformAnimation()
			{
			
				if (this.animationStepNum < this.numAnimationSteps)
				{
					
					this.animationStepNum++;

					
					if (this.AnimationType == FadeAnimation)
					{
						this.expando.UpdateFadeAnimation(this.animationStepNum, this.numAnimationSteps);
					}
					else
					{
						this.expando.UpdateSlideAnimation(this.animationStepNum, this.numAnimationSteps);
					}
				}
				else
				{
					this.StopAnimation();
				}
			}


			
			public void StopAnimation()
			{
			
				this.animationTimer.Stop();
				this.animationTimer.Dispose();

				if (this.AnimationType == FadeAnimation)
				{
					this.expando.StopFadeAnimation();
				}
				else
				{
					this.expando.StopSlideAnimation();
				}
			}

		
			public Expando Expando
			{
				get
				{
					return this.expando;
				}
			}


		
			public int NumAnimationSteps
			{
				get
				{
					return this.numAnimationSteps;
				}

				set
				{
					if (value < 0)
					{
						throw new ArgumentOutOfRangeException("value", "NumAnimationSteps must be at least 0");
					}
				
					
					if (!this.animating)
					{
						this.numAnimationSteps = value;
					}
				}
			}


			
			public int AnimationFrameInterval
			{
				get
				{
					return this.animationFrameInterval;
				}

				set
				{
					this.animationFrameInterval = value;
				}
			}


			
			public bool Animating
			{
				get
				{
					return this.animating;
				}
			}


		
			public int AnimationType
			{
				get
				{
					return this.animationType;
				}
			}

			
			private void animationTimer_Tick(object sender, EventArgs e)
			{
			
				this.PerformAnimation();
			}

		
		}

		[Serializable()]
			public class ExpandoSurrogate : ISerializable
		{
			
			public string Name;

			public string Text;

			public Size Size;
			
			
			public Point Location;
			
			public string BackColor;
			
			public int ExpandedHeight;
			
			
			public ExpandoInfo.ExpandoInfoSurrogate CustomSettings;
		
			public HeaderInfo.HeaderInfoSurrogate CustomHeaderSettings;
			
			
			public bool Animate;
			
			
			public bool ShowFocusCues;
			
			
			public bool Collapsed;
			
			public bool CanCollapse;
			
			public bool SpecialGroup;

			[XmlElement("TitleImage", typeof(Byte[]), DataType="base64Binary")]
			public byte[] TitleImage;

			[XmlElement("Watermark", typeof(Byte[]), DataType="base64Binary")]
			public byte[] Watermark;
			public bool Enabled;
			
			public bool Visible;
			public bool AutoLayout;
			
			public AnchorStyles Anchor;
			
			public DockStyle Dock;
			
			public string FontName;

			public float FontSize;
			
			public FontStyle FontDecoration;

			[XmlElementAttribute("Tag", typeof(Byte[]), DataType="base64Binary")]
			public byte[] Tag;
			public int Version = 3300;

	
			public ExpandoSurrogate()
			{
				this.Name = null;
				this.Text = null;
				this.Size = Size.Empty;
				this.Location = Point.Empty;

				this.BackColor = ThemeManager.ConvertColorToString(SystemColors.Control);
				this.ExpandedHeight = -1;
				
				this.CustomSettings = null;
				this.CustomHeaderSettings = null;

				this.Animate = false;
				this.ShowFocusCues = false;
				this.Collapsed = false;
				this.CanCollapse = true;
				this.SpecialGroup = false;

				this.TitleImage = new byte[0];
				this.Watermark = new byte[0];

				this.Enabled = true;
				this.Visible = true;
				this.AutoLayout = false;

				this.Anchor = AnchorStyles.None;
				this.Dock = DockStyle.None;

				this.FontName = "Tahoma";
				this.FontSize = 8.25f;
				this.FontDecoration = FontStyle.Regular;


				this.Tag = new byte[0];
			}

		
			public void Load(Expando expando)
			{
				this.Name = expando.Name;
				this.Text = expando.Text;
				this.Size = expando.Size;
				this.Location = expando.Location;

				this.BackColor = ThemeManager.ConvertColorToString(expando.BackColor);
				this.ExpandedHeight = expando.ExpandedHeight;

				this.CustomSettings = new ExpandoInfo.ExpandoInfoSurrogate();
				this.CustomSettings.Load(expando.CustomSettings);
				this.CustomHeaderSettings = new HeaderInfo.HeaderInfoSurrogate();
				this.CustomHeaderSettings.Load(expando.CustomHeaderSettings);

				this.Animate = expando.Animate;
				this.ShowFocusCues = expando.ShowFocusCues;
				this.Collapsed = expando.Collapsed;
				this.CanCollapse = expando.CanCollapse;
				this.SpecialGroup = expando.SpecialGroup;

				this.TitleImage = ThemeManager.ConvertImageToByteArray(expando.TitleImage);
				this.Watermark = ThemeManager.ConvertImageToByteArray(expando.Watermark);

				this.Enabled = expando.Enabled;
				this.Visible = expando.Visible;
				this.AutoLayout = expando.AutoLayout;

				this.Anchor = expando.Anchor;
				this.Dock = expando.Dock;

				this.FontName = expando.Font.FontFamily.Name;
				this.FontSize = expando.Font.SizeInPoints;
				this.FontDecoration = expando.Font.Style;

				this.Tag = ThemeManager.ConvertObjectToByteArray(expando.Tag);
			}
			public Expando Save()
			{
				Expando expando = new Expando();
				((ISupportInitialize) expando).BeginInit();
				expando.SuspendLayout();

				expando.Name = this.Name;
				expando.Text = this.Text;
				expando.Size = this.Size;
				expando.Location = this.Location;

				expando.BackColor = ThemeManager.ConvertStringToColor(this.BackColor);
				expando.ExpandedHeight = this.ExpandedHeight;

				expando.customSettings = this.CustomSettings.Save();
				expando.customSettings.Expando = expando;
				expando.customHeaderSettings = this.CustomHeaderSettings.Save();
				expando.customHeaderSettings.Expando = expando;

				expando.TitleImage = ThemeManager.ConvertByteArrayToImage(this.TitleImage);
				expando.Watermark = ThemeManager.ConvertByteArrayToImage(this.Watermark);

				expando.Animate = this.Animate;
				expando.ShowFocusCues = this.ShowFocusCues;
				expando.Collapsed = this.Collapsed;
				expando.CanCollapse = this.CanCollapse;
				expando.SpecialGroup = this.SpecialGroup;

				expando.Enabled = this.Enabled;
				expando.Visible = this.Visible;
				expando.AutoLayout = this.AutoLayout;

				expando.Anchor = this.Anchor;
				expando.Dock = this.Dock;

				expando.Font = new Font(this.FontName, this.FontSize, this.FontDecoration);

				expando.Tag = ThemeManager.ConvertByteArrayToObject(this.Tag);


				((ISupportInitialize) expando).EndInit();
				expando.ResumeLayout(false);

				return expando;
			}
			[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter=true)]
			public void GetObjectData(SerializationInfo info, StreamingContext context)
			{
				info.AddValue("Version", this.Version);
				
				info.AddValue("Name", this.Name);
				info.AddValue("Text", this.Text);
				info.AddValue("Size", this.Size);
				info.AddValue("Location", this.Location);

				info.AddValue("BackColor", this.BackColor);
				info.AddValue("ExpandedHeight", this.ExpandedHeight);

				info.AddValue("CustomSettings", this.CustomSettings);
				info.AddValue("CustomHeaderSettings", this.CustomHeaderSettings);
				
				info.AddValue("Animate", this.Animate);
				info.AddValue("ShowFocusCues", this.ShowFocusCues);
				info.AddValue("Collapsed", this.Collapsed);
				info.AddValue("CanCollapse", this.CanCollapse);
				info.AddValue("SpecialGroup", this.SpecialGroup);

				info.AddValue("TitleImage", this.TitleImage);
				info.AddValue("Watermark", this.Watermark);
				
				info.AddValue("Enabled", this.Enabled);
				info.AddValue("Visible", this.Visible);
				info.AddValue("AutoLayout", this.AutoLayout);

				info.AddValue("Anchor", this.Anchor);
				info.AddValue("Dock", this.Dock);
				
				info.AddValue("FontName", this.FontName);
				info.AddValue("FontSize", this.FontSize);
				info.AddValue("FontDecoration", this.FontDecoration);
				
				info.AddValue("Tag", this.Tag);
				
			}
			[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter=true)]
			protected ExpandoSurrogate(SerializationInfo info, StreamingContext context) : base()
			{
				int version = info.GetInt32("Version");

				this.Name = info.GetString("Name");
				this.Text = info.GetString("Text");
				this.Size = (Size) info.GetValue("Size", typeof(Size));
				this.Location = (Point) info.GetValue("Location", typeof(Point));

				this.BackColor = info.GetString("BackColor");
				this.ExpandedHeight = info.GetInt32("ExpandedHeight");

				this.CustomSettings = (ExpandoInfo.ExpandoInfoSurrogate) info.GetValue("CustomSettings", typeof(ExpandoInfo.ExpandoInfoSurrogate));
				this.CustomHeaderSettings = (HeaderInfo.HeaderInfoSurrogate) info.GetValue("CustomHeaderSettings", typeof(HeaderInfo.HeaderInfoSurrogate));

				this.Animate = info.GetBoolean("Animate");
				this.ShowFocusCues = info.GetBoolean("ShowFocusCues");
				this.Collapsed = info.GetBoolean("Collapsed");
				this.CanCollapse = info.GetBoolean("CanCollapse");
				this.SpecialGroup = info.GetBoolean("SpecialGroup");

				this.TitleImage = (byte[]) info.GetValue("TitleImage", typeof(byte[]));
				this.Watermark = (byte[]) info.GetValue("Watermark", typeof(byte[]));

				this.Enabled = info.GetBoolean("Enabled");
				this.Visible = info.GetBoolean("Visible");
				this.AutoLayout = info.GetBoolean("AutoLayout");
				
				this.Anchor = (AnchorStyles) info.GetValue("Anchor", typeof(AnchorStyles));
				this.Dock = (DockStyle) info.GetValue("Dock", typeof(DockStyle));

				this.FontName = info.GetString("FontName");
				this.FontSize = info.GetSingle("FontSize");
				this.FontDecoration = (FontStyle) info.GetValue("FontDecoration", typeof(FontStyle));

				this.Tag = (byte[]) info.GetValue("Tag", typeof(byte[]));

			}

		
		}

	}


	public class ExpandoEventArgs : EventArgs
	{
		private Expando expando;




		public ExpandoEventArgs()
		{
			expando = null;
		}

		public ExpandoEventArgs(Expando expando)
		{
			this.expando = expando;
		}





		public Expando Expando
		{
			get
			{
				return this.expando;
			}
		}

		public bool Collapsed
		{
			get
			{
				return this.expando.Collapsed;
			}
		}


	}






	internal class ExpandoConverter : TypeConverter
	{
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == typeof(InstanceDescriptor))
			{
				return true;
			}

			return base.CanConvertTo(context, destinationType);
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(InstanceDescriptor) && value is Expando)
			{
				ConstructorInfo ci = typeof(Expando).GetConstructor(new Type[] {});

				if (ci != null)
				{
					return new InstanceDescriptor(ci, null, false);
				}
			}

			return base.ConvertTo(context, culture, value, destinationType);
		}
	}





	internal class ExpandoDesigner : ParentControlDesigner
	{
		public ExpandoDesigner() : base()
		{
			
		}


		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);

			properties.Remove("BackColor");
			properties.Remove("BackgroundImage");
			properties.Remove("BorderStyle");
			properties.Remove("Cursor");
			properties.Remove("BackgroundImage");
		}
	}






	public enum FocusStates
	{
		None = 0,	
		
		Mouse = 1
	}

}