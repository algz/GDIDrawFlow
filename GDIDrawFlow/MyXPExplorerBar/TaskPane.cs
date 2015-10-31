using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Xml.Serialization;


namespace System.MyControl.MyXPExplorerBar
{
	#region TaskPane
	[ToolboxItem(true),
	DesignerAttribute(typeof(TaskPaneDesigner))]
	public class TaskPane : ScrollableControl, ISupportInitialize
	{
		#region Event Handlers

		/// <summary>
		/// Occurs when an Expando is added to the TaskPane
		/// </summary>
		public event ExpandoEventHandler ExpandoAdded; 

		/// <summary>
		/// Occurs when an Expando is removed from the TaskPane
		/// </summary>
		public event ExpandoEventHandler ExpandoRemoved; 

		/// <summary>
		/// Occurs when a value in the CustomSettings proterty changes
		/// </summary>
		public event EventHandler CustomSettingsChanged;

		#endregion
			
		
		#region Class Data
		
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Internal list of Expandos contained in the TaskPane
		/// </summary>
		private TaskPane.ExpandoCollection expandoCollection;
		
		/// <summary>
		/// System defined settings for the TaskBar
		/// </summary>
		private ExplorerBarInfo systemSettings;

		/// <summary>
		/// Specifies whether the TaskPane is currently initialising
		/// </summary>
		private bool initialising;

		/// <summary>
		/// Specifies whether the TaskPane and its children should render 
		/// themselves using a theme similar to the Windows XP Classic theme
		/// </summary>
		private bool classicTheme;
		
		/// <summary>
		/// Specifies whether the TaskPane and its children should render 
		/// themselves using a non-official Windows XP theme
		/// </summary>
		private bool customTheme;

		/// <summary>
		/// A Rectangle that specifies the size and location of the watermark
		/// </summary>
		private Rectangle watermarkRect;

		/// <summary>
		/// Specifies whether the TaskPane is currently performing a 
		/// layout operation
		/// </summary>
		private bool layout;

		/// <summary>
		/// 
		/// </summary>
		private int beginUpdateCount;

		/// <summary>
		/// Specifies the custom settings for the TaskPane
		/// </summary>
		private TaskPaneInfo customSettings;

		/// <summary>
		/// 
		/// </summary>
		private bool allowExpandoDragging;

		/// <summary>
		/// 
		/// </summary>
		private Point dropPoint;

		/// <summary>
		/// 
		/// </summary>
		private Color dropIndicatorColor;

		#endregion


		#region Constructor

		/// <summary>
		/// Initializes a new instance of the TaskPane class with default settings
		/// </summary>
		public TaskPane()
		{
			// This call is required by the Windows.Forms Form Designer.
			components = new System.ComponentModel.Container();

			// set control styles
			this.SetStyle(ControlStyles.DoubleBuffer, true);
			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			this.SetStyle(ControlStyles.UserPaint, true);
			this.SetStyle(ControlStyles.ResizeRedraw, true);
			this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);

			this.expandoCollection = new TaskPane.ExpandoCollection(this);

			// get the system theme settings
			this.systemSettings = ThemeManager.GetSystemExplorerBarSettings();

			this.customSettings = new TaskPaneInfo();
			this.customSettings.TaskPane = this;
			this.customSettings.SetDefaultEmptyValues();

			this.BackColor = this.systemSettings.TaskPane.GradientStartColor;
			this.BackgroundImage = this.BackImage;

			this.classicTheme = false;
			this.customTheme = false;

			// size
			int width = (this.systemSettings.TaskPane.Padding.Left + 
				this.systemSettings.TaskPane.Padding.Right + 
				this.systemSettings.Header.BackImageWidth);
			int height = width;
			this.Size = new Size(width, height);

			// setup sutoscrolling
			this.AutoScroll = false;
			this.AutoScrollMargin = new Size(this.systemSettings.TaskPane.Padding.Right, 
				this.systemSettings.TaskPane.Padding.Bottom);

			// Listen for changes to the parent
			this.ParentChanged += new EventHandler(this.OnParentChanged);

			this.allowExpandoDragging = false;
			this.dropPoint = Point.Empty;
			this.dropIndicatorColor = Color.Red;

			this.beginUpdateCount = 0;

			this.initialising = false;
			this.layout = false;
		}

		#endregion


		#region Methods

		#region Appearance

		/// <summary>
		/// Forces the TaskPane and all it's Expandos to use a theme
		/// equivalent to Windows XPs classic theme 
		/// </summary>
		public void UseClassicTheme()
		{
			this.classicTheme = true;
			this.customTheme = false;
			
			ExplorerBarInfo settings = ThemeManager.GetSystemExplorerBarSettings(true);

			this.systemSettings.Dispose();
			this.systemSettings = null;

			this.SystemSettings = settings;
		}


		/// <summary>
		/// Forces the TaskPane and all it's Expandos to use the 
		/// specified theme
		/// </summary>
		/// <param name="stylePath">The path to the custom 
		/// shellstyle.dll to use</param>
		public void UseCustomTheme(string stylePath)
		{
			this.customTheme = true;
			this.classicTheme = false;
			
			ExplorerBarInfo settings = ThemeManager.GetSystemExplorerBarSettings(stylePath);

			this.systemSettings.Dispose();
			this.systemSettings = null;

			this.SystemSettings = settings;
		}


		/// <summary>
		/// Forces the TaskPane and all it's Expandos to use the 
		/// current system theme
		/// </summary>
		public void UseDefaultTheme()
		{
			this.customTheme = false;
			this.classicTheme = false;
			
			ExplorerBarInfo settings = ThemeManager.GetSystemExplorerBarSettings();

			this.systemSettings.Dispose();
			this.systemSettings = null;

			this.SystemSettings = settings;
		}

		#endregion

		#region Dispose

		/// <summary> 
		/// Releases the unmanaged resources used by the TaskPane and 
		/// optionally releases the managed resources
		/// </summary>
		/// <param name="disposing">True to release both managed and unmanaged 
		/// resources; false to release only unmanaged resources</param>
		protected override void Dispose( bool disposing )
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
				}
			}

			base.Dispose(disposing);
		}

		#endregion

		#region Expandos

		/// <summary>
		/// Collaspes all the Expandos contained in the TaskPane
		/// </summary>
		// suggested by: PaleyX (jmpalethorpe@tiscali.co.uk)
		//               03/06/2004
		//               v1.1
		public void CollapseAll()
		{
			foreach (Expando expando in this.Expandos)
			{
				expando.Collapsed = true;
			}
		}


		/// <summary>
		/// Expands all the Expandos contained in the TaskPane
		/// </summary>
		// suggested by: PaleyX (jmpalethorpe@tiscali.co.uk)
		//               03/06/2004
		//               v1.1
		public void ExpandAll()
		{
			foreach (Expando expando in this.Expandos)
			{
				expando.Collapsed = false;
			}
		}


		/// <summary>
		/// Collaspes all the Expandos contained in the TaskPane, 
		/// except for the specified Expando which is expanded
		/// </summary>
		/// <param name="expando">The Expando that is to be expanded</param>
		// suggested by: PaleyX (jmpalethorpe@tiscali.co.uk)
		//               03/06/2004
		//               v1.1
		public void CollapseAllButOne(Expando expando)
		{
			foreach (Expando e in this.Expandos)
			{
				if (e != expando)
				{
					e.Collapsed = true;
				}
				else
				{
					expando.Collapsed = false;
				}
			}
		}


		/// <summary>
		/// Calculates the Point that the currently dragged Expando will 
		/// dropped at based on the specified mouse position
		/// </summary>
		/// <param name="point">The current position of the mouse in screen 
		/// co-ordinates</param>
		internal void UpdateDropPoint(Point point)
		{
			Point p = this.PointToClient(point);
			
			if (this.ClientRectangle.Contains(p))
			{
				if (p.Y <= this.Expandos[0].Top)
				{
					this.dropPoint.Y = this.Padding.Top / 2;
				}
				else if (p.Y >= this.Expandos[this.Expandos.Count - 1].Bottom)
				{
					this.dropPoint.Y = this.Expandos[this.Expandos.Count - 1].Bottom + (this.Padding.Top / 2);
				}
				else
				{
					for (int i=0; i<this.Expandos.Count; i++)
					{
						if (p.Y >= this.Expandos[i].Top && p.Y <= this.Expandos[i].Bottom)
						{
							if (p.Y <= this.Expandos[i].Top + (this.Expandos[i].Height / 2))
							{
								if (i == 0)
								{
									this.dropPoint.Y = this.Padding.Top / 2;
								}
								else
								{
									this.dropPoint.Y = this.Expandos[i].Top - ((this.Expandos[i].Top - this.Expandos[i-1].Bottom) / 2);
								}
							}
							else
							{	
								if (i == this.Expandos.Count - 1)
								{
									this.dropPoint.Y = this.Expandos[this.Expandos.Count - 1].Bottom + (this.Padding.Top / 2);
								}
								else
								{
									this.dropPoint.Y = this.Expandos[i].Bottom + ((this.Expandos[i+1].Top - this.Expandos[i].Bottom) / 2);
								}
							}

							break;
						}
					}
				}
			}
			else
			{
				this.dropPoint = Point.Empty;
			}

			this.Invalidate(false);
		}


		/// <summary>
		/// "Drops" the specified Expando and moves it to the current drop point
		/// </summary>
		/// <param name="expando">The Expando to be "dropped"</param>
		internal void DropExpando(Expando expando)
		{
			if (this.dropPoint == Point.Empty)
			{
				return;
			}
			
			if (expando != null && expando.TaskPane == this)
			{
				int i = 0;
				int expandoIndex = this.Expandos.IndexOf(expando);
				
				for (; i<this.Expandos.Count; i++)
				{
					if (this.dropPoint.Y <= this.Expandos[i].Top)
					{
						if (i > expandoIndex)
						{
							this.Expandos.Move(expando, i-1);
						}
						else if (i < expandoIndex)
						{
							this.Expandos.Move(expando, i);
						}

						break;
					}
				}

				if (i == this.Expandos.Count)
				{
					this.Expandos.Move(expando, i);
				}
			}

			this.dropPoint = Point.Empty;

			this.Invalidate(false);
		}

		#endregion

		#region ISupportInitialize Members

		/// <summary>
		/// Signals the TaskPane that initialization is starting
		/// </summary>
		public void BeginInit()
		{
			this.initialising = true;
		}


		/// <summary>
		/// Signals the TaskPane that initialization is complete
		/// </summary>
		public void EndInit()
		{
			this.initialising = false;

			this.DoLayout();
		}


		/// <summary>
		/// Gets whether the TaskPane is currently initialising
		/// </summary>
		[Browsable(false)]
		public bool Initialising
		{
			get
			{
				return this.initialising;
			}
		}

		#endregion

		#region Layout

		// fix: Added BeginUpdate() and EndUpdate() so that DoLayout() 
		//      isn't called everytime something happens with Expandos
		//      Brian Nottingham (nottinbe@slu.edu)
		//      22/12/2004
		//      v3.0
		
		/// <summary>
		/// Prevents the TaskPane from drawing until the EndUpdate method is called
		/// </summary>
		public void BeginUpdate()
		{
			this.beginUpdateCount++;
		}


		/// <summary>
		/// Resumes drawing of the TaskPane after drawing is suspended by the 
		/// BeginUpdate method
		/// </summary>
		public void EndUpdate()
		{
			this.beginUpdateCount = Math.Max(this.beginUpdateCount--, 0);

			if (beginUpdateCount == 0)
			{
				this.DoLayout(true);
			}
		}


		/// <summary>
		/// Forces the TaskPane to apply layout logic to child Expandos, 
		/// and adjusts the Size and Location of the Expandos if necessary
		/// </summary>
		public void DoLayout()
		{
			this.DoLayout(false);
		}


		// fix: Added DoLayout(bool performRealLayout) to improve 
		//      TaskPane scroll behavior
		//      Jewlin (jewlin88@hotmail.com)
		//      22/10/2004
		//      v3.0

		/// <summary>
		/// Forces the TaskPane to apply layout logic to child Expandos, 
		/// and adjusts the Size and Location of the Expandos if necessary
		/// </summary>
		/// <param name="performRealLayout">true to execute pending layout 
		/// requests; otherwise, false</param>
		public void DoLayout(bool performRealLayout)
		{
			// fix: take into account beginUpdateCount
			//      Brian Nottingham (nottinbe@slu.edu)
			//      22/12/2004
			//      v3.0
			//if (this.layout)
			if (this.layout || this.beginUpdateCount > 0)
			{
				return;
			}

			this.layout = true;
			
			// stop the layout engine
			this.SuspendLayout();
			
			Expando e;
			Point p;
			
			// work out how wide to make the controls, and where
			// the top of the first control should be
			int y = this.DisplayRectangle.Y + this.Padding.Top;
			int width = this.ClientSize.Width - this.Padding.Left - this.Padding.Right;

			// for each control in our list...
			for (int i=0; i<this.Expandos.Count; i++)
			{
				e = this.Expandos[i];

				// go to the next expando if this one is invisible and 
				// it's parent is visible
				if (!e.Visible && e.Parent != null && e.Parent.Visible)
				{
					continue;
				}

				p = new Point(this.Padding.Left, y);

				// set the width and location of the control
				e.Location = p;
				e.Width = width;

				// update the next starting point
				y += e.Height + this.Padding.Bottom;
			}

			// restart the layout engine
			this.ResumeLayout(performRealLayout);

			this.layout = false;
		}


		/// <summary>
		/// Calculates where the specified Expando should be located
		/// </summary>
		/// <returns>A Point that specifies where the Expando should 
		/// be located</returns>
		protected internal Point CalcExpandoLocation(Expando target)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}

			int targetIndex = this.Expandos.IndexOf(target);

			Expando e;
			Point p;
			
			int y = this.DisplayRectangle.Y + this.Padding.Top;
			int width = this.ClientSize.Width - this.Padding.Left - this.Padding.Right;

			for (int i=0; i<targetIndex; i++)
			{
				e = this.Expandos[i];

				if (!e.Visible)
				{
					continue;
				}

				p = new Point(this.Padding.Left, y);
				y += e.Height + this.Padding.Bottom;
			}
			
			return new Point(this.Padding.Left, y);
		}


		/// <summary>
		/// Updates the layout of the Expandos while in design mode, and 
		/// adds/removes Expandos from the ControlCollection as necessary
		/// </summary>
		internal void UpdateExpandos()
		{
			if (this.Expandos.Count == this.Controls.Count)
			{
				// make sure the the expandos index in the ControlCollection 
				// are the same as in the ExpandoCollection (indexes in the 
				// ExpandoCollection may have changed due to the user moving 
				// them around in the editor)
				this.MatchControlCollToExpandoColl();				
				
				return;
			}

			// were any expandos added
			if (this.Expandos.Count > this.Controls.Count)
			{
				// add any extra expandos in the ExpandoCollection to the 
				// ControlCollection
				for (int i=0; i<this.Expandos.Count; i++)
				{
					if (!this.Controls.Contains(this.Expandos[i]))
					{
						this.OnExpandoAdded(new ExpandoEventArgs(this.Expandos[i]));
					}
				}
			}
			else
			{
				// expandos were removed
				int i = 0;
				Expando expando;

				// remove any extra expandos from the ControlCollection
				while (i < this.Controls.Count)
				{
					expando = (Expando) this.Controls[i];
					
					if (!this.Expandos.Contains(expando))
					{
						this.OnExpandoRemoved(new ExpandoEventArgs(expando));
					}
					else
					{
						i++;
					}
				}
			}
		}


		/// <summary>
		/// Make sure the the expandos index in the ControlCollection 
		/// are the same as in the ExpandoCollection (indexes in the 
		/// ExpandoCollection may have changed due to the user moving 
		/// them around in the editor or calling ExpandoCollection.Move())
		/// </summary>
		internal void MatchControlCollToExpandoColl()
		{
			this.SuspendLayout();
				
			for (int i=0; i<this.Expandos.Count; i++)
			{
				this.Controls.SetChildIndex(this.Expandos[i], i);
			}

			this.ResumeLayout(false);
				
			this.DoLayout(true);

			this.Invalidate(true);
		}

		#endregion

		#endregion


		#region Properties

		#region Colors

		/// <summary>
		/// Gets the first color of the TaskPane's background gradient fill.
		/// </summary>
		[Browsable(false)]
		public Color GradientStartColor
		{
			get
			{
				if (this.CustomSettings.GradientStartColor != Color.Empty)
				{
					return this.CustomSettings.GradientStartColor;
				}

				return this.systemSettings.TaskPane.GradientStartColor;
			}
		}


		/// <summary>
		/// Gets the second color of the TaskPane's background gradient fill.
		/// </summary>
		[Browsable(false)]
		public Color GradientEndColor
		{
			get
			{
				if (this.CustomSettings.GradientEndColor != Color.Empty)
				{
					return this.CustomSettings.GradientEndColor;
				}

				return this.systemSettings.TaskPane.GradientEndColor;
			}
		}


		/// <summary>
		/// Gets the direction of the TaskPane's background gradient fill.
		/// </summary>
		[Browsable(false)]
		public LinearGradientMode GradientDirection
		{
			get
			{
				if (this.CustomSettings.GradientStartColor != Color.Empty && 
					this.CustomSettings.GradientEndColor != Color.Empty)
				{
					return this.CustomSettings.GradientDirection;
				}

				return this.systemSettings.TaskPane.GradientDirection;
			}
		}

		#endregion

		#region Expandos

		/// <summary>
		/// A TaskPane.ExpandoCollection representing the collection of 
		/// Expandos contained within the TaskPane
		/// </summary>
		[Category("Behavior"),
		DefaultValue(null), 
		Description("The Expandos contained in the TaskPane"), 
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content), 
		Editor(typeof(ExpandoCollectionEditor), typeof(UITypeEditor))]
		public TaskPane.ExpandoCollection Expandos
		{
			get
			{
				return this.expandoCollection;
			}
		}


		/// <summary>
		/// A Control.ControlCollection representing the collection of 
		/// controls contained within the control
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Control.ControlCollection Controls
		{
			get
			{
				return base.Controls;
			}
		}


		/// <summary>
		/// Gets or sets whether Expandos can be dragged around the TaskPane
		/// </summary>
		[Category("Behavior"),
		DefaultValue(false), 
		Description("Indicates whether Expandos can be dragged around the TaskPane")]
		public bool AllowExpandoDragging
		{
			get
			{
				return this.allowExpandoDragging;
			}

			set
			{
				this.allowExpandoDragging = value;
			}
		}


		/// <summary>
		/// Gets or sets the Color that the Expando drop point indicator is drawn in
		/// </summary>
		[Browsable(false),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Color ExpandoDropIndicatorColor
		{
			get
			{
				return this.dropIndicatorColor;
			}

			set
			{
				this.dropIndicatorColor = value;
			}
		}

		#endregion

		#region Images

		/// <summary>
		/// Gets the Image used as the TaskPane's background
		/// </summary>
		[Browsable(false)]
		public Image BackImage
		{
			get
			{
				if (this.CustomSettings.BackImage != null)
				{
					return this.CustomSettings.BackImage;
				}

				return this.systemSettings.TaskPane.BackImage;
			}
		}


		/// <summary>
		/// Gets how the TaskPane's background Image is to be drawn
		/// </summary>
		[Browsable(false)]
		public ImageStretchMode StretchMode
		{
			get
			{
				if (this.CustomSettings.BackImage != null)
				{
					return this.CustomSettings.StretchMode;
				}

				return this.systemSettings.TaskPane.StretchMode;
			}
		}


		/// <summary>
		/// Gets the Image that is used as a watermark in the TaskPane's 
		/// client area
		/// </summary>
		[Browsable(false)]
		public Image Watermark
		{
			get
			{
				if (this.CustomSettings.Watermark != null)
				{
					return this.CustomSettings.Watermark;
				}

				return this.systemSettings.TaskPane.Watermark;
			}
		}


		/// <summary>
		/// Gets the alignment of the TaskPane's watermark
		/// </summary>
		[Browsable(false)]
		public ContentAlignment WatermarkAlignment
		{
			get
			{
				if (this.CustomSettings.Watermark != null)
				{
					return this.CustomSettings.WatermarkAlignment;
				}

				return this.systemSettings.TaskPane.WatermarkAlignment;
			}
		}

		#endregion

		#region Padding

		/// <summary>
		/// Gets the amount of space between the border and the 
		/// Expando's along each side of the TaskPane.
		/// </summary>
		[Browsable(false)]
		public Padding Padding
		{
			get
			{
				if (this.CustomSettings.Padding != Padding.Empty)
				{
					return this.CustomSettings.Padding;
				}

				return this.systemSettings.TaskPane.Padding;
			}
		}

		#endregion

		#region SystemSettings

		/// <summary>
		/// Gets or sets the system defined settings for the TaskPane
		/// </summary>
		protected internal ExplorerBarInfo SystemSettings
		{
			get
			{
				return this.systemSettings;
			}

			set
			{
				// ignore null values
				if (value == null)
				{
					return;
				}
				
				if (this.systemSettings != value)
				{
					this.SuspendLayout();
					
					if (this.systemSettings != null)
					{
						this.systemSettings.Dispose();
						this.systemSettings = null;
					}

					this.watermarkRect = Rectangle.Empty;

					this.systemSettings = value;
					this.BackColor = this.GradientStartColor;
					this.BackgroundImage = this.BackImage;

					foreach (Expando expando in this.Expandos)
					{
						expando.SystemSettings = this.systemSettings;
						expando.DoLayout();
					}

					this.DoLayout();

					this.ResumeLayout(true);

					this.Invalidate(true);
				}
			}
		}


		/// <summary>
		/// Gets the custom settings for the TaskPane
		/// </summary>
		[Category("Appearance"),
		Description(""),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		TypeConverter(typeof(TaskPaneInfoConverter))]
		public TaskPaneInfo CustomSettings
		{
			get
			{
				return this.customSettings;
			}

			set
			{
				this.customSettings = value;
			}
		}


		/// <summary>
		/// Resets the custom settings to their default values
		/// </summary>
		public void ResetCustomSettings()
		{
			this.CustomSettings.SetDefaultEmptyValues();

			this.FireCustomSettingsChanged(EventArgs.Empty);
		}

		#endregion

		#endregion


		#region Events

		#region Controls

		/// <summary>
		/// Raises the ControlAdded event
		/// </summary>
		/// <param name="e">A ControlEventArgs that contains the event data</param>
		protected override void OnControlAdded(ControlEventArgs e)
		{
			// make sure the control is an Expando
			if ((e.Control as Expando) == null)
			{
				// remove the control
				this.Controls.Remove(e.Control);

				// throw a hissy fit
				throw new InvalidCastException("Only Expando's can be added to the TaskPane");
			}
			
			base.OnControlAdded(e);

			// add the expando to the ExpandoCollection if necessary
			if (!this.Expandos.Contains((Expando) e.Control))
			{
				this.Expandos.Add((Expando) e.Control);
			}
		}


		/// <summary>
		/// Raises the ControlRemoved event
		/// </summary>
		/// <param name="e">A ControlEventArgs that contains the event data</param>
		protected override void OnControlRemoved(ControlEventArgs e)
		{
			base.OnControlRemoved (e);

			// remove the control from the itemList
			if (this.Expandos.Contains(e.Control))
			{
				this.Expandos.Remove((Expando) e.Control);
			}

			// update the layout of the controls
			this.DoLayout();
		}

		#endregion

		#region Custom Settings

		/// <summary>
		/// Raises the CustomSettingsChanged event
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data</param>
		internal void FireCustomSettingsChanged(EventArgs e)
		{
			this.BackColor = this.GradientStartColor;
			this.BackgroundImage = this.BackImage;
				
			this.DoLayout();

			this.Invalidate(true);

			this.OnCustomSettingsChanged(e);
		}


		/// <summary>
		/// Raises the CustomSettingsChanged event
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data</param>
		protected virtual void OnCustomSettingsChanged(EventArgs e)
		{
			if (CustomSettingsChanged != null)
			{
				CustomSettingsChanged(this, e);
			}
		}

		#endregion

		#region Expandos

		/// <summary> 
		/// Event handler for the Expando StateChanged event
		/// </summary>
		/// <param name="sender">The object that fired the event</param>
		/// <param name="e">An ExpandoEventArgs that contains the event data</param>
		private void expando_StateChanged(object sender, ExpandoEventArgs e)
		{
			this.OnExpandoStateChanged(e);
		}


		/// <summary>
		/// Occurs when the value of an Expandos Collapsed property changes
		/// </summary>
		/// <param name="e">An ExpandoEventArgs that contains the event data</param>
		protected virtual void OnExpandoStateChanged(ExpandoEventArgs e)
		{
			this.DoLayout(true);
		}


		/// <summary>
		/// Raises the ExpandoAdded event
		/// </summary>
		/// <param name="e">An ExpandoEventArgs that contains the event data</param>
		protected virtual void OnExpandoAdded(ExpandoEventArgs e)
		{
			// add the expando to the ControlCollection if it hasn't already
			if (!this.Controls.Contains(e.Expando))
			{
				this.Controls.Add(e.Expando);
			}

			// set anchor styles
			e.Expando.Anchor = (AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right);		
			
			// tell the Expando who's its daddy...
			e.Expando.TaskPane = this;
			e.Expando.SystemSettings = this.systemSettings;

			// listen for collapse/expand events
			e.Expando.StateChanged += new ExpandoEventHandler(this.expando_StateChanged);

			// update the layout of the controls
			this.DoLayout();

			//
			if (ExpandoAdded != null)
			{
				ExpandoAdded(this, e);
			}
		}


		/// <summary>
		/// Raises the ExpandoRemoved event
		/// </summary>
		/// <param name="e">An ExpandoEventArgs that contains the event data</param>
		protected virtual void OnExpandoRemoved(ExpandoEventArgs e)
		{
			// remove the control from the ControlCollection if it hasn't already
			if (this.Controls.Contains(e.Expando))
			{
				this.Controls.Remove(e.Expando);
			}

			// remove the StateChanged listener
			e.Expando.StateChanged -= new ExpandoEventHandler(this.expando_StateChanged);

			// update the layout of the controls
			this.DoLayout();

			//
			if (ExpandoRemoved != null)
			{
				ExpandoRemoved(this, e);
			}
		}

		#endregion

		#region Paint

		/// <summary> 
		/// Raises the PaintBackground event
		/// </summary>
		/// <param name="e">A PaintEventArgs that contains the event data</param>
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			// paint background
			if (this.BackImage != null)
			{
				//base.OnPaintBackground(e);

				WrapMode wrap = WrapMode.Clamp;
				
				if ((this.StretchMode == ImageStretchMode.Tile) || (this.StretchMode == ImageStretchMode.Horizontal))
				{
					wrap = WrapMode.Tile;
				}

				using (TextureBrush brush = new TextureBrush(this.BackImage, wrap))
				{
					e.Graphics.FillRectangle(brush, this.DisplayRectangle);
				}
			}
			else
			{
				if (this.GradientStartColor != this.GradientEndColor)
				{
					using (LinearGradientBrush brush = new LinearGradientBrush(this.DisplayRectangle, 
							   this.GradientStartColor, 
							   this.GradientEndColor, 
							   this.GradientDirection))
					{
						e.Graphics.FillRectangle(brush, this.DisplayRectangle);
					}
				}
				else
				{
					using (SolidBrush brush = new SolidBrush(this.GradientStartColor))
					{
						e.Graphics.FillRectangle(brush, this.ClientRectangle);
					}
				}
			}

			// draw the watermark if we have one
			if (this.Watermark != null)
			{
				Rectangle rect = new Rectangle(0, 0, this.Watermark.Width, this.Watermark.Height);

				// work out a rough location of where the watermark should go

				switch (this.WatermarkAlignment)
				{
					case ContentAlignment.BottomCenter:
					case ContentAlignment.BottomLeft:
					case ContentAlignment.BottomRight:
					{
						rect.Y = this.DisplayRectangle.Bottom - this.Watermark.Height;
						
						break;
					}

					case ContentAlignment.MiddleCenter:
					case ContentAlignment.MiddleLeft:
					case ContentAlignment.MiddleRight:
					{
						rect.Y = this.DisplayRectangle.Top + ((this.DisplayRectangle.Height - this.Watermark.Height) / 2);
						
						break;
					}
				}

				switch (this.WatermarkAlignment)
				{
					case ContentAlignment.BottomRight:
					case ContentAlignment.MiddleRight:
					case ContentAlignment.TopRight:
					{
						rect.X = this.ClientRectangle.Right - this.Watermark.Width;
						
						break;
					}

					case ContentAlignment.BottomCenter:
					case ContentAlignment.MiddleCenter:
					case ContentAlignment.TopCenter:
					{
						rect.X = this.ClientRectangle.Left + ((this.ClientRectangle.Width - this.Watermark.Width) / 2);
						
						break;
					}
				}

				// shrink the destination rect if necesary so that we
				// can see all of the image
				
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

				// draw the watermark
				e.Graphics.DrawImage(this.Watermark, rect);
			}
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			if (this.dropPoint != Point.Empty)
			{
				int width = this.ClientSize.Width - this.Padding.Left - this.Padding.Right;

				using (Brush brush = new SolidBrush(this.ExpandoDropIndicatorColor))
				{
					e.Graphics.FillRectangle(brush, this.Padding.Left, this.dropPoint.Y, width, 1);

					e.Graphics.FillPolygon(brush, new Point[] { new Point(this.Padding.Left, this.dropPoint.Y - 4), 
																  new Point(this.Padding.Left + 4, this.dropPoint.Y), 
																  new Point(this.Padding.Left, this.dropPoint.Y + 4)});

					e.Graphics.FillPolygon(brush, new Point[] { new Point(this.Width - this.Padding.Right, this.dropPoint.Y - 4), 
																  new Point(this.Width - this.Padding.Right - 4, this.dropPoint.Y), 
																  new Point(this.Width - this.Padding.Right, this.dropPoint.Y + 4)});
				}
			}
		}

		#endregion

		#region Parents

		// fix: TaskPane will now perform a layout when its 
		//      parent becomes visible
		//      Brian Nottingham (nottinbe@slu.edu)
		//      22/12/2004
		//      v3.0
		
		/// <summary>
		/// Event handler for the ParentChanged event
		/// </summary>
		/// <param name="sender">The object that fired the event</param>
		/// <param name="e">An EventArgs that contains the event data</param>
		private void OnParentChanged(object sender, EventArgs e)
		{
			if (this.Parent != null)
			{
				this.Parent.VisibleChanged += new EventHandler(this.OnParentVisibleChanged);
			}
		}


		/// <summary>
		/// Event handler for the ParentVisibleChanged event
		/// </summary>
		/// <param name="sender">The object that fired the event</param>
		/// <param name="e">An EventArgs that contains the event data</param>
		private void OnParentVisibleChanged(object sender, EventArgs e)
		{
			if (sender != this.Parent)
			{
				((Control) sender).VisibleChanged -= new EventHandler(this.OnParentVisibleChanged);
				
				return;
			}

			if (this.Parent.Visible)
			{
				this.DoLayout();
			}
		}

		#endregion

		#region System Colors

		/// <summary> 
		/// Raises the SystemColorsChanged event
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data</param>
		protected override void OnSystemColorsChanged(EventArgs e)
		{
			base.OnSystemColorsChanged(e);

			// don't go any further if we are explicitly using
			// the classic or a custom theme
			if (this.classicTheme || this.customTheme)
			{
				return;
			}

			this.SuspendLayout();

			// get rid of the current system theme info
			this.systemSettings.Dispose();
			this.systemSettings = null;

			// get a new system theme info for the new theme
			this.systemSettings = ThemeManager.GetSystemExplorerBarSettings();
			
			this.BackgroundImage = this.BackImage;


			// update the system settings for each expando
			foreach (Control control in this.Controls)
			{
				if (control is Expando)
				{
					Expando expando = (Expando) control;
					
					expando.SystemSettings = this.systemSettings;
				}
			}

			// update the layout of the controls
			this.DoLayout();

			this.ResumeLayout(true);
		}

		#endregion

		#region Size

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);

			this.DoLayout();
		}

		#endregion

		#endregion


		#region ExpandoCollection

		/// <summary>
		/// Represents a collection of Expando objects
		/// </summary>
		public class ExpandoCollection : CollectionBase
		{
			#region Class Data

			/// <summary>
			/// The TaskPane that owns this ExpandoCollection
			/// </summary>
			private TaskPane owner;

			#endregion


			#region Constructor

			/// <summary>
			/// Initializes a new instance of the TaskPane.ExpandoCollection class
			/// </summary>
			/// <param name="owner">A TaskPane representing the taskpane that owns 
			/// the Expando collection</param>
			public ExpandoCollection(TaskPane owner) : base()
			{
				if (owner == null)
				{
					throw new ArgumentNullException("owner");
				}
				
				this.owner = owner;
			}

			#endregion


			#region Methods

			/// <summary>
			/// Adds the specified expando to the expando collection
			/// </summary>
			/// <param name="value">The Expando to add to the expando collection</param>
			public void Add(Expando value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}

				this.List.Add(value);
				this.owner.Controls.Add(value);

				this.owner.OnExpandoAdded(new ExpandoEventArgs(value));
			}


			/// <summary>
			/// Adds an array of expando objects to the collection
			/// </summary>
			/// <param name="expandos">An array of Expando objects to add 
			/// to the collection</param>
			public void AddRange(Expando[] expandos)
			{
				if (expandos == null)
				{
					throw new ArgumentNullException("expandos");
				}

				for (int i=0; i<expandos.Length; i++)
				{
					this.Add(expandos[i]);
				}
			}
			
			
			/// <summary>
			/// Removes all expandos from the collection
			/// </summary>
			public new void Clear()
			{
				while (this.Count > 0)
				{
					this.RemoveAt(0);
				}
			}


			/// <summary>
			/// Determines whether the specified expando is a member of the 
			/// collection
			/// </summary>
			/// <param name="expando">The Expando to locate in the collection</param>
			/// <returns>true if the Expando is a member of the collection; 
			/// otherwise, false</returns>
			public bool Contains(Expando expando)
			{
				if (expando == null)
				{
					throw new ArgumentNullException("expando");
				}

				return (this.IndexOf(expando) != -1);
			}


			/// <summary>
			/// Determines whether the specified control is a member of the 
			/// collection
			/// </summary>
			/// <param name="control">The Control to locate in the collection</param>
			/// <returns>true if the Control is a member of the collection; 
			/// otherwise, false</returns>
			public bool Contains(Control control)
			{
				if (!(control is Expando))
				{
					return false;
				}

				return this.Contains((Expando) control);
			}


			/// <summary>
			/// Retrieves the index of the specified expando in the expando 
			/// collection
			/// </summary>
			/// <param name="expando">The Expando to locate in the collection</param>
			/// <returns>A zero-based index value that represents the position 
			/// of the specified Expando in the TaskPane.ExpandoCollection</returns>
			public int IndexOf(Expando expando)
			{
				if (expando == null)
				{
					throw new ArgumentNullException("expando");
				}
				
				for (int i=0; i<this.Count; i++)
				{
					if (this[i] == expando)
					{
						return i;
					}
				}

				return -1;
			}


			/// <summary>
			/// Removes the specified expando from the expando collection
			/// </summary>
			/// <param name="value">The Expando to remove from the 
			/// TaskPane.ExpandoCollection</param>
			public void Remove(Expando value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}

				this.List.Remove(value);

				this.owner.Controls.Remove(value);

				this.owner.OnExpandoRemoved(new ExpandoEventArgs(value));
			}

			
			/// <summary>
			/// Removes an expando from the expando collection at the 
			/// specified indexed location
			/// </summary>
			/// <param name="index">The index value of the Expando to 
			/// remove</param>
			public new void RemoveAt(int index)
			{
				this.Remove(this[index]);
			}


			/// <summary>
			/// Moves the specified expando to the specified indexed location 
			/// in the expando collection
			/// </summary>
			/// <param name="value">The expando to be moved</param>
			/// <param name="index">The indexed location in the expando collection 
			/// that the specified expando will be moved to</param>
			public void Move(Expando value, int index)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}

				// make sure the index is within range
				if (index < 0)
				{
					index = 0;
				}
				else if (index > this.Count)
				{
					index = this.Count;
				}

				// don't go any further if the expando is already 
				// in the desired position or we don't contain it
				if (!this.Contains(value) || this.IndexOf(value) == index)
				{
					return;
				}

				this.List.Remove(value);

				// if the index we're supposed to move the expando to
				// is now greater to the number of expandos contained, 
				// add it to the end of the list, otherwise insert it at 
				// the specified index
				if (index > this.Count)
				{
					this.List.Add(value);
				}
				else
				{
					this.List.Insert(index, value);
				}

				// re-layout the controls
				this.owner.MatchControlCollToExpandoColl();
			}


			/// <summary>
			/// Moves the specified expando to the top of the expando collection
			/// </summary>
			/// <param name="value">The expando to be moved</param>
			public void MoveToTop(Expando value)
			{
				this.Move(value, 0);
			}


			/// <summary>
			/// Moves the specified expando to the bottom of the expando collection
			/// </summary>
			/// <param name="value">The expando to be moved</param>
			public void MoveToBottom(Expando value)
			{
				this.Move(value, this.Count);
			}

			#endregion


			#region Properties

			/// <summary>
			/// The Expando located at the specified index location within 
			/// the expando collection
			/// </summary>
			/// <param name="index">The index of the expando to retrieve 
			/// from the expando collection</param>
			public virtual Expando this[int index]
			{
				get
				{
					return this.List[index] as Expando;
				}
			}

			#endregion
		}

		#endregion
	
	
		#region ExpandoCollectionEditor

		/// <summary>
		/// A custom CollectionEditor for editing ExpandoCollections
		/// </summary>
		internal class ExpandoCollectionEditor : CollectionEditor
		{
			/// <summary>
			/// Initializes a new instance of the CollectionEditor class 
			/// using the specified collection type
			/// </summary>
			/// <param name="type"></param>
			public ExpandoCollectionEditor(Type type) : base(type)
			{
			
			}


			/// <summary>
			/// Edits the value of the specified object using the specified 
			/// service provider and context
			/// </summary>
			/// <param name="context">An ITypeDescriptorContext that can be 
			/// used to gain additional context information</param>
			/// <param name="isp">A service provider object through which 
			/// editing services can be obtained</param>
			/// <param name="value">The object to edit the value of</param>
			/// <returns>The new value of the object. If the value of the 
			/// object has not changed, this should return the same object 
			/// it was passed</returns>
			public override object EditValue(ITypeDescriptorContext context, IServiceProvider isp, object value)
			{
				TaskPane originalControl = (TaskPane) context.Instance;

				object returnObject = base.EditValue(context, isp, value);

				originalControl.UpdateExpandos();

				return returnObject;
			}


			/// <summary>
			/// Creates a new instance of the specified collection item type
			/// </summary>
			/// <param name="itemType">The type of item to create</param>
			/// <returns>A new instance of the specified object</returns>
			protected override object CreateInstance(Type itemType)
			{
				object expando = base.CreateInstance(itemType);
			
				((Expando) expando).Name = "expando";
			
				return expando;
			}
		}

		#endregion


		#region TaskPaneSurrogate

		/// <summary>
		/// A class that is serialized instead of a TaskPane (as 
		/// TaskPanes contain objects that cause serialization problems)
		/// </summary>
		[Serializable(),
			XmlRoot("TaskPaneSurrogate", Namespace="", IsNullable=false)]
			public class TaskPaneSurrogate : ISerializable
		{
			#region Class Data

			/// <summary>
			/// See TaskPane.Name.  This member is not intended to be used 
			/// directly from your code.
			/// </summary>
			public string Name;

			/// <summary>
			/// See TaskPane.Size.  This member is not intended to be used 
			/// directly from your code.
			/// </summary>
			public Size Size;
			
			/// <summary>
			/// See TaskPane.Location.  This member is not intended to be used 
			/// directly from your code.
			/// </summary>
			public Point Location;
			
			/// <summary>
			/// See TaskPane.BackColor.  This member is not intended to be used 
			/// directly from your code.
			/// </summary>
			public string BackColor;
			
			/// <summary>
			/// See TaskPane.CustomSettings.  This member is not intended to be used 
			/// directly from your code.
			/// </summary>
			public TaskPaneInfo.TaskPaneInfoSurrogate CustomSettings;
			
			/// <summary>
			/// See TaskPane.AutoScroll.  This member is not intended to be used 
			/// directly from your code.
			/// </summary>
			public bool AutoScroll;
			
			/// <summary>
			/// See TaskPane.AutoScrollMargin.  This member is not intended to be used 
			/// directly from your code.
			/// </summary>
			public Size AutoScrollMargin;
			
			/// <summary>
			/// See TaskPane.Enabled.  This member is not intended to be used 
			/// directly from your code.
			/// </summary>
			public bool Enabled;
			
			/// <summary>
			/// See TaskPane.Visible.  This member is not intended to be used 
			/// directly from your code.
			/// </summary>
			public bool Visible;
			
			/// <summary>
			/// See TaskPane.Anchor.  This member is not intended to be used 
			/// directly from your code.
			/// </summary>
			public AnchorStyles Anchor;
			
			/// <summary>
			/// See TaskPane.Dock.  This member is not intended to be used 
			/// directly from your code.
			/// </summary>
			public DockStyle Dock;
			
			/// <summary>
			/// See Font.Name.  This member is not intended to be used 
			/// directly from your code.
			/// </summary>
			public string FontName;
			
			/// <summary>
			/// See Font.Size.  This member is not intended to be used 
			/// directly from your code.
			/// </summary>
			public float FontSize;
			
			/// <summary>
			/// See Font.Style.  This member is not intended to be used 
			/// directly from your code.
			/// </summary>
			public FontStyle FontDecoration;

			/// <summary>
			/// See TaskPane.Expandos.  This member is not intended to be used 
			/// directly from your code.
			/// </summary>
			[XmlArray("Expandos"), XmlArrayItem("ExpandoSurrogate", typeof(Expando.ExpandoSurrogate))]
			public ArrayList Expandos;

			/// <summary>
			/// See Control.Tag.  This member is not intended to be used 
			/// directly from your code.
			/// </summary>
			[XmlElementAttribute("Tag", typeof(Byte[]), DataType="base64Binary")]
			public byte[] Tag;

			/// <summary>
			/// See TaskPane.AllowExpandoDragging.  This member is not intended to be used 
			/// directly from your code.
			/// </summary>
			public bool AllowExpandoDragging;

			/// <summary>
			/// See TaskPane.ExpandoDropIndicatorColor.  This member is not intended to be used 
			/// directly from your code.
			/// </summary>
			public string ExpandoDropIndicatorColor;

			/// <summary>
			/// Version number of the surrogate.  This member is not intended 
			/// to be used directly from your code.
			/// </summary>
			public int Version = 3300;

			#endregion


			#region Constructor

			/// <summary>
			/// Initializes a new instance of the TaskPaneSurrogate class with default settings
			/// </summary>
			public TaskPaneSurrogate()
			{
				this.Name = null;

				this.Size = Size.Empty;
				this.Location = Point.Empty;

				this.BackColor = ThemeManager.ConvertColorToString(SystemColors.Control);

				this.CustomSettings = null;

				this.AutoScroll = false;
				this.AutoScrollMargin = Size.Empty;

				this.Enabled = true;
				this.Visible = true;

				this.Anchor = AnchorStyles.None;
				this.Dock = DockStyle.None;

				this.FontName = "Tahoma";
				this.FontSize = 8.25f;
				this.FontDecoration = FontStyle.Regular;

				this.Tag = new byte[0];

				this.AllowExpandoDragging = false;
				this.ExpandoDropIndicatorColor = ThemeManager.ConvertColorToString(Color.Red);

				this.Expandos = new ArrayList();
			}

			#endregion


			#region Methods

			/// <summary>
			/// Populates the TaskPaneSurrogate with data that is to be 
			/// serialized from the specified TaskPane
			/// </summary>
			/// <param name="taskPane">The TaskPane that contains the data 
			/// to be serialized</param>
			public void Load(TaskPane taskPane)
			{
				this.Name = taskPane.Name;
				this.Size = taskPane.Size;
				this.Location = taskPane.Location;

				this.BackColor = ThemeManager.ConvertColorToString(taskPane.BackColor);

				this.CustomSettings = new TaskPaneInfo.TaskPaneInfoSurrogate();
				this.CustomSettings.Load(taskPane.CustomSettings);

				this.AutoScroll = taskPane.AutoScroll;
				this.AutoScrollMargin = taskPane.AutoScrollMargin;

				this.Enabled = taskPane.Enabled;
				this.Visible = taskPane.Visible;

				this.Anchor = taskPane.Anchor;
				this.Dock = taskPane.Dock;

				this.FontName = taskPane.Font.FontFamily.Name;
				this.FontSize = taskPane.Font.SizeInPoints;
				this.FontDecoration = taskPane.Font.Style;

				this.AllowExpandoDragging = taskPane.AllowExpandoDragging;
				this.ExpandoDropIndicatorColor =  ThemeManager.ConvertColorToString(taskPane.ExpandoDropIndicatorColor);

				this.Tag = ThemeManager.ConvertObjectToByteArray(taskPane.Tag);

				foreach (Expando expando in taskPane.Expandos)
				{
					Expando.ExpandoSurrogate es = new Expando.ExpandoSurrogate();

					es.Load(expando);

					this.Expandos.Add(es);
				}
			}


			/// <summary>
			/// Returns a TaskPane that contains the deserialized TaskPaneSurrogate data
			/// </summary>
			/// <returns>A TaskPane that contains the deserialized TaskPaneSurrogate data</returns>
			public TaskPane Save()
			{
				TaskPane taskPane = new TaskPane();
				((ISupportInitialize) taskPane).BeginInit();
				taskPane.SuspendLayout();

				taskPane.Name = this.Name;
				taskPane.Size = this.Size;
				taskPane.Location = this.Location;

				taskPane.BackColor = ThemeManager.ConvertStringToColor(this.BackColor);

				taskPane.customSettings = this.CustomSettings.Save();
				taskPane.customSettings.TaskPane = taskPane;

				taskPane.AutoScroll = this.AutoScroll;
				taskPane.AutoScrollMargin = this.AutoScrollMargin;

				taskPane.Enabled = this.Enabled;
				taskPane.Visible = this.Visible;

				taskPane.Anchor = this.Anchor;
				taskPane.Dock = this.Dock;

				taskPane.Font = new Font(this.FontName, this.FontSize, this.FontDecoration);

				taskPane.Tag = ThemeManager.ConvertByteArrayToObject(this.Tag);

				taskPane.AllowExpandoDragging = this.AllowExpandoDragging;
				taskPane.ExpandoDropIndicatorColor = ThemeManager.ConvertStringToColor(this.ExpandoDropIndicatorColor);

				foreach (Object o in this.Expandos)
				{
					Expando e = ((Expando.ExpandoSurrogate) o).Save();
					
					taskPane.Expandos.Add(e);
				}

				((ISupportInitialize) taskPane).EndInit();
				taskPane.ResumeLayout(false);

				return taskPane;
			}

			
			/// <summary>
			/// Populates a SerializationInfo with the data needed to serialize the TaskPaneSurrogate
			/// </summary>
			/// <param name="info">The SerializationInfo to populate with data</param>
			/// <param name="context">The destination for this serialization</param>
			[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter=true)]
			public void GetObjectData(SerializationInfo info, StreamingContext context)
			{
				info.AddValue("Version", this.Version);
				
				info.AddValue("Name", this.Name);
				info.AddValue("Size", this.Size);
				info.AddValue("Location", this.Location);

				info.AddValue("BackColor", this.BackColor);

				info.AddValue("CustomSettings", this.CustomSettings);

				info.AddValue("AutoScroll", this.AutoScroll);
				info.AddValue("AutoScrollMargin", this.AutoScrollMargin);

				info.AddValue("Enabled", this.Enabled);
				info.AddValue("Visible", this.Visible);

				info.AddValue("Anchor", this.Anchor);
				info.AddValue("Dock", this.Dock);
				
				info.AddValue("FontName", this.FontName);
				info.AddValue("FontSize", this.FontSize);
				info.AddValue("FontDecoration", this.FontDecoration);

				info.AddValue("AllowExpandoDragging", this.AllowExpandoDragging);
				info.AddValue("ExpandoDropIndicatorColor", this.ExpandoDropIndicatorColor);
				
				info.AddValue("Tag", this.Tag);
				
				info.AddValue("Expandos", this.Expandos);
			}


			/// <summary>
			/// Initializes a new instance of the TaskPaneSurrogate class using the information 
			/// in the SerializationInfo
			/// </summary>
			/// <param name="info">The information to populate the TaskPaneSurrogate</param>
			/// <param name="context">The source from which the TaskPaneSurrogate is deserialized</param>
			[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter=true)]
			protected TaskPaneSurrogate(SerializationInfo info, StreamingContext context) : base()
			{
				int version = info.GetInt32("Version");

				this.Name = info.GetString("Name");
				this.Size = (Size) info.GetValue("Size", typeof(Size));
				this.Location = (Point) info.GetValue("Location", typeof(Point));

				this.BackColor = info.GetString("BackColor");

				this.CustomSettings = (TaskPaneInfo.TaskPaneInfoSurrogate) info.GetValue("CustomSettings", typeof(TaskPaneInfo.TaskPaneInfoSurrogate));

				this.AutoScroll = info.GetBoolean("AutoScroll");
				this.AutoScrollMargin = (Size) info.GetValue("AutoScrollMargin", typeof(Size));

				this.Enabled = info.GetBoolean("Enabled");
				this.Visible = info.GetBoolean("Visible");
				
				this.Anchor = (AnchorStyles) info.GetValue("Anchor", typeof(AnchorStyles));
				this.Dock = (DockStyle) info.GetValue("Dock", typeof(DockStyle));

				this.FontName = info.GetString("FontName");
				this.FontSize = info.GetSingle("FontSize");
				this.FontDecoration = (FontStyle) info.GetValue("FontDecoration", typeof(FontStyle));

				if (version >= 3300)
				{
					this.AllowExpandoDragging = info.GetBoolean("AllowExpandoDragging");
					this.ExpandoDropIndicatorColor = info.GetString("ExpandoDropIndicatorColor");
				}

				this.Tag = (byte[]) info.GetValue("Tag", typeof(byte[]));

				this.Expandos = (ArrayList) info.GetValue("Expandos", typeof(ArrayList));
			}

			#endregion
		}

		#endregion
	}

	#endregion
	#region TaskPaneDesigner

	internal class TaskPaneDesigner : ScrollableControlDesigner
	{
		public TaskPaneDesigner() : base()
		{
			
		}
		/// <summary>
		/// 移除该控件无用的属性
		/// </summary>
		/// <param name="properties"></param>
		protected override void PreFilterProperties(System.Collections.IDictionary properties)
		{
			base.PreFilterProperties(properties);

			properties.Remove("BackColor");
			properties.Remove("BackgroundImage");
			properties.Remove("Cursor");
			properties.Remove("ForeColor");
		}
	}

	#endregion
}
