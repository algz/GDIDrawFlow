
using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace System.MyControl.MyXPExplorerBar
{

	public class ExplorerBarInfo : IDisposable
	{
		private TaskPaneInfo taskPane;

		private TaskItemInfo taskItem;

	
		private ExpandoInfo expando;

		
		private HeaderInfo header;

		private bool officialTheme;

		
		private bool classicTheme;

		private string shellStylePath;

		

		public ExplorerBarInfo()
		{
			this.taskPane = new TaskPaneInfo();
			this.taskItem = new TaskItemInfo();
			this.expando = new ExpandoInfo();
			this.header = new HeaderInfo();

			this.officialTheme = false;
			this.classicTheme = false;
			this.shellStylePath = null;
		}


		public void SetUnthemedArrowImages()
		{
			this.Header.SetUnthemedArrowImages();
		}


		public void UseClassicTheme()
		{
			this.classicTheme = true;
			
			this.TaskPane.SetDefaultValues();
			this.Expando.SetDefaultValues();
			this.Header.SetDefaultValues();
			this.TaskItem.SetDefaultValues();

			this.SetUnthemedArrowImages();
		}


		public void Dispose()
		{
			this.taskPane.Dispose();
			this.header.Dispose();
			this.expando.Dispose();
		}

		public TaskPaneInfo TaskPane
		{
			get
			{
				return this.taskPane;
			}

			set
			{
				this.taskPane = value;
			}
		}


	
		public TaskItemInfo TaskItem
		{
			get
			{
				return this.taskItem;
			}

			set
			{
				this.taskItem = value;
			}
		}


		public ExpandoInfo Expando
		{
			get
			{
				return this.expando;
			}

			set
			{
				this.expando = value;
			}
		}


		
		public HeaderInfo Header
		{
			get
			{
				return this.header;
			}

			set
			{
				this.header = value;
			}
		}


		
		public bool OfficialTheme
		{
			get
			{
				return this.officialTheme;
			}

		}


		internal void SetOfficialTheme(bool officialTheme)
		{
			this.officialTheme = officialTheme;
		}


		public bool ClassicTheme
		{
			get
			{
				return this.classicTheme;
			}
		}


		public string ShellStylePath
		{
			get
			{
				return this.shellStylePath;
			}

			set
			{
				this.shellStylePath = value;
			}
		}

		[Serializable()]
			public class ExplorerBarInfoSurrogate : ISerializable
		{

			public TaskPaneInfo.TaskPaneInfoSurrogate TaskPaneInfoSurrogate;
	
			public TaskItemInfo.TaskItemInfoSurrogate TaskItemInfoSurrogate;

			public ExpandoInfo.ExpandoInfoSurrogate ExpandoInfoSurrogate;
			
		
			public HeaderInfo.HeaderInfoSurrogate HeaderInfoSurrogate;

			
			public int Version = 3300;

		
			public ExplorerBarInfoSurrogate()
			{
				this.TaskPaneInfoSurrogate = null;
				this.TaskItemInfoSurrogate = null;
				this.ExpandoInfoSurrogate = null;
				this.HeaderInfoSurrogate = null;
			}

		
			public void Load(ExplorerBarInfo explorerBarInfo)
			{
				this.TaskPaneInfoSurrogate = new TaskPaneInfo.TaskPaneInfoSurrogate();
				this.TaskPaneInfoSurrogate.Load(explorerBarInfo.TaskPane);

				this.TaskItemInfoSurrogate = new TaskItemInfo.TaskItemInfoSurrogate();
				this.TaskItemInfoSurrogate.Load(explorerBarInfo.TaskItem);

				this.ExpandoInfoSurrogate = new ExpandoInfo.ExpandoInfoSurrogate();
				this.ExpandoInfoSurrogate.Load(explorerBarInfo.Expando);

				this.HeaderInfoSurrogate = new HeaderInfo.HeaderInfoSurrogate();
				this.HeaderInfoSurrogate.Load(explorerBarInfo.Header);
			}


			
			public ExplorerBarInfo Save()
			{
				ExplorerBarInfo explorerBarInfo = new ExplorerBarInfo();

				explorerBarInfo.TaskPane = this.TaskPaneInfoSurrogate.Save();
				explorerBarInfo.TaskItem = this.TaskItemInfoSurrogate.Save();
				explorerBarInfo.Expando = this.ExpandoInfoSurrogate.Save();
				explorerBarInfo.Header = this.HeaderInfoSurrogate.Save();				
				
				return explorerBarInfo;
			}



			[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter=true)]
			public void GetObjectData(SerializationInfo info, StreamingContext context)
			{
				info.AddValue("Version", this.Version);
				
				info.AddValue("TaskPaneInfoSurrogate", this.TaskPaneInfoSurrogate);
				info.AddValue("TaskItemInfoSurrogate", this.TaskItemInfoSurrogate);
				info.AddValue("ExpandoInfoSurrogate", this.ExpandoInfoSurrogate);
				info.AddValue("HeaderInfoSurrogate", this.HeaderInfoSurrogate);
			}


			[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter=true)]
			protected ExplorerBarInfoSurrogate(SerializationInfo info, StreamingContext context) : base()
			{
				int version = info.GetInt32("Version");

				this.TaskPaneInfoSurrogate = (TaskPaneInfo.TaskPaneInfoSurrogate) info.GetValue("TaskPaneInfoSurrogate", typeof(TaskPaneInfo.TaskPaneInfoSurrogate));
				this.TaskItemInfoSurrogate = (TaskItemInfo.TaskItemInfoSurrogate) info.GetValue("TaskItemInfoSurrogate", typeof(TaskItemInfo.TaskItemInfoSurrogate));
				this.ExpandoInfoSurrogate = (ExpandoInfo.ExpandoInfoSurrogate) info.GetValue("ExpandoInfoSurrogate", typeof(ExpandoInfo.ExpandoInfoSurrogate));
				this.HeaderInfoSurrogate = (HeaderInfo.HeaderInfoSurrogate) info.GetValue("HeaderInfoSurrogate", typeof(HeaderInfo.HeaderInfoSurrogate));
			}

			
		}

	
	}

	


	public class TaskPaneInfo : IDisposable
	{
		
		private Color gradientStartColor;
		
		
		private Color gradientEndColor;

		
		private LinearGradientMode direction;

	
		private Padding padding;

		
		private Image backImage;

		private ImageStretchMode stretchMode;

	
		private Image watermark;

		
		private ContentAlignment watermarkAlignment;

	
		private TaskPane owner;




		public TaskPaneInfo()
		{
			
			this.gradientStartColor = Color.Transparent;
			this.gradientEndColor = Color.Transparent;
			this.direction = LinearGradientMode.Vertical;

			
			this.padding = new Padding(12, 12, 12, 12);

			this.backImage = null;
			this.stretchMode = ImageStretchMode.Tile;

			this.watermark = null;
			this.watermarkAlignment = ContentAlignment.BottomCenter;

			this.owner = null;
		}

	
		public void SetDefaultValues()
		{
			
			this.gradientStartColor = SystemColors.Window;
			this.gradientEndColor = SystemColors.Window;
			this.direction = LinearGradientMode.Vertical;

			
			this.padding.Left = 12;
			this.padding.Top = 12;
			this.padding.Right = 12;
			this.padding.Bottom = 12;

		
			this.backImage = null;
			this.stretchMode = ImageStretchMode.Tile;
			this.watermark = null;
			this.watermarkAlignment = ContentAlignment.BottomCenter;
		}


		
		public void SetDefaultEmptyValues()
		{
			
			this.gradientStartColor = Color.Empty;
			this.gradientEndColor = Color.Empty;
			this.direction = LinearGradientMode.Vertical;

			
			this.padding.Left = 0;
			this.padding.Top = 0;
			this.padding.Right = 0;
			this.padding.Bottom = 0;
			
			this.backImage = null;
			this.stretchMode = ImageStretchMode.Tile;
			this.watermark = null;
			this.watermarkAlignment = ContentAlignment.BottomCenter;
		}


		public void Dispose()
		{
			if (this.backImage != null)
			{
				this.backImage.Dispose();
				this.backImage = null;
			}

			if (this.watermark != null)
			{
				this.watermark.Dispose();
				this.watermark = null;
			}
		}

	
		[Description("The TaskPane's first gradient background color")]
		public Color GradientStartColor
		{
			get
			{
				return this.gradientStartColor;
			}

			set
			{
				if (this.gradientStartColor != value)
				{
					this.gradientStartColor = value;

					if (this.TaskPane != null)
					{
						this.TaskPane.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}



		private bool ShouldSerializeGradientStartColor()
		{
			return this.GradientStartColor != Color.Empty;
		}


		[Description("The TaskPane's second gradient background color")]
		public Color GradientEndColor
		{
			get
			{
				return this.gradientEndColor;
			}

			set
			{
				if (this.gradientEndColor != value)
				{
					this.gradientEndColor = value;

					if (this.TaskPane != null)
					{
						this.TaskPane.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		private bool ShouldSerializeGradientEndColor()
		{
			return this.GradientEndColor != Color.Empty;
		}


		[DefaultValue(LinearGradientMode.Vertical),
		Description("The direction of the TaskPane's background gradient")]
		public LinearGradientMode GradientDirection
		{
			get
			{
				return this.direction;
			}

			set
			{
				if (!Enum.IsDefined(typeof(LinearGradientMode), value)) 
				{
					throw new InvalidEnumArgumentException("value", (int) value, typeof(LinearGradientMode));
				}

				if (this.direction != value)
				{
					this.direction = value;

					if (this.TaskPane != null)
					{
						this.TaskPane.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		[DefaultValue(null),
		Description("The Image that is used as the TaskPane's background")]
		public Image BackImage
		{
			get
			{
				return this.backImage;
			}

			set
			{
				if (this.backImage != value)
				{
					this.backImage = value;

					if (this.TaskPane != null)
					{
						this.TaskPane.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		
		[Browsable(false),
		DefaultValue(ImageStretchMode.Tile),
		Description("Specifies how the TaskPane's background Image is drawn")]
		public ImageStretchMode StretchMode
		{
			get
			{
				return this.stretchMode;
			}

			set
			{
				if (!Enum.IsDefined(typeof(ImageStretchMode), value)) 
				{
					throw new InvalidEnumArgumentException("value", (int) value, typeof(ImageStretchMode));
				}

				if (this.stretchMode != value)
				{
					this.stretchMode = value;

					if (this.TaskPane != null)
					{
						this.TaskPane.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		[DefaultValue(null),
		Description("The Image that is used as the TaskPane's watermark")]
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

					if (this.TaskPane != null)
					{
						this.TaskPane.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		[DefaultValue(ContentAlignment.BottomCenter),
		Description("The alignment of the Image that is used as the TaskPane's watermark")]
		public ContentAlignment WatermarkAlignment
		{
			get
			{
				return this.watermarkAlignment;
			}

			set
			{
				if (!Enum.IsDefined(typeof(ContentAlignment), value)) 
				{
					throw new InvalidEnumArgumentException("value", (int) value, typeof(ContentAlignment));
				}

				if (this.watermarkAlignment != value)
				{
					this.watermarkAlignment = value;

					if (this.TaskPane != null)
					{
						this.TaskPane.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}

	
		[Description("The amount of space between the border and the Expando's along each side of the TaskPane")]
		public Padding Padding
		{
			get
			{
				return this.padding;
			}

			set
			{
				if (this.padding != value)
				{
					this.padding = value;

					if (this.TaskPane != null)
					{
						this.TaskPane.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}



		private bool ShouldSerializePadding()
		{
			return this.Padding != Padding.Empty;
		}


		protected internal TaskPane TaskPane
		{
			get
			{
				return this.owner;
			}
			
			set
			{
				this.owner = value;
			}
		}

	
		[Serializable()]
			public class TaskPaneInfoSurrogate : ISerializable
		{
			
			public string GradientStartColor;
			
			
			public string GradientEndColor;
		
			public LinearGradientMode GradientDirection;
			
			
			public Padding Padding;
			
			[XmlElementAttribute("BackImage", typeof(Byte[]), DataType="base64Binary")]
			public byte[] BackImage;
			
			public ImageStretchMode StretchMode;
		
			[XmlElementAttribute("Watermark", typeof(Byte[]), DataType="base64Binary")]
			public byte[] Watermark;
			
		
			public ContentAlignment WatermarkAlignment;

			
			public int Version = 3300;

			
			public TaskPaneInfoSurrogate()
			{
				this.GradientStartColor = ThemeManager.ConvertColorToString(Color.Empty);
				this.GradientEndColor = ThemeManager.ConvertColorToString(Color.Empty);
				this.GradientDirection = LinearGradientMode.Vertical;

				this.Padding = Padding.Empty;

				this.BackImage = new byte[0];
				this.StretchMode = ImageStretchMode.Normal;

				this.Watermark = new byte[0];
				this.WatermarkAlignment = ContentAlignment.BottomCenter;
			}

	
			public void Load(TaskPaneInfo taskPaneInfo)
			{
				this.GradientStartColor = ThemeManager.ConvertColorToString(taskPaneInfo.GradientStartColor);
				this.GradientEndColor = ThemeManager.ConvertColorToString(taskPaneInfo.GradientEndColor);
				this.GradientDirection = taskPaneInfo.GradientDirection;

				this.Padding = taskPaneInfo.Padding;

				this.BackImage = ThemeManager.ConvertImageToByteArray(taskPaneInfo.BackImage);
				this.StretchMode = taskPaneInfo.StretchMode;

				this.Watermark = ThemeManager.ConvertImageToByteArray(taskPaneInfo.Watermark);
				this.WatermarkAlignment = taskPaneInfo.WatermarkAlignment;
			}


		
			public TaskPaneInfo Save()
			{
				TaskPaneInfo taskPaneInfo = new TaskPaneInfo();

				taskPaneInfo.GradientStartColor = ThemeManager.ConvertStringToColor(this.GradientStartColor);
				taskPaneInfo.GradientEndColor = ThemeManager.ConvertStringToColor(this.GradientEndColor);
				taskPaneInfo.GradientDirection = this.GradientDirection;

				taskPaneInfo.Padding = this.Padding;

				taskPaneInfo.BackImage = ThemeManager.ConvertByteArrayToImage(this.BackImage);
				taskPaneInfo.StretchMode = this.StretchMode;

				taskPaneInfo.Watermark = ThemeManager.ConvertByteArrayToImage(this.Watermark);
				taskPaneInfo.WatermarkAlignment = this.WatermarkAlignment;
				
				return taskPaneInfo;
			}


		
			[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter=true)]
			public void GetObjectData(SerializationInfo info, StreamingContext context)
			{
				info.AddValue("Version", this.Version);
				
				info.AddValue("GradientStartColor", this.GradientStartColor);
				info.AddValue("GradientEndColor", this.GradientEndColor);
				info.AddValue("GradientDirection", this.GradientDirection);
				
				info.AddValue("Padding", this.Padding);
				
				info.AddValue("BackImage", this.BackImage);
				info.AddValue("StretchMode", this.StretchMode);
				
				info.AddValue("Watermark", this.Watermark);
				info.AddValue("WatermarkAlignment", this.WatermarkAlignment);
			}


			[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter=true)]
			protected TaskPaneInfoSurrogate(SerializationInfo info, StreamingContext context) : base()
			{
				int version = info.GetInt32("Version");

				this.GradientStartColor = info.GetString("GradientStartColor");
				this.GradientEndColor = info.GetString("GradientEndColor");
				this.GradientDirection = (LinearGradientMode) info.GetValue("GradientDirection", typeof(LinearGradientMode));
				
				this.Padding = (Padding) info.GetValue("Padding", typeof(Padding));

				this.BackImage = (byte[]) info.GetValue("BackImage", typeof(byte[]));
				this.StretchMode = (ImageStretchMode) info.GetValue("StretchMode", typeof(ImageStretchMode));

				this.Watermark = (byte[]) info.GetValue("Watermark", typeof(byte[]));
				this.WatermarkAlignment = (ContentAlignment) info.GetValue("WatermarkAlignment", typeof(ContentAlignment));
			}

		}

		
	}


	
	internal class TaskPaneInfoConverter : ExpandableObjectConverter
	{
		
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string) && value is TaskPaneInfo)
			{
				return "";
			}

			return base.ConvertTo(context, culture, value, destinationType);
		}
	}


	
	public class TaskItemInfo
	{
		

		private Padding padding; 

	
		private Margin margin;

		
		private Color linkNormal;

		
		private Color linkHot;

		private FontStyle fontDecoration;

		
		public TaskItemInfo()
		{
			
			this.padding = new Padding(6, 0, 4, 0);

			
			this.margin = new Margin(0, 4, 0, 0);

			
			this.linkNormal = SystemColors.ControlText;
			this.linkHot = SystemColors.ControlText;

			this.fontDecoration = FontStyle.Underline;

		}

	

	

		public void SetDefaultValues()
		{
			
			this.padding.Left = 6;
			this.padding.Top = 0;
			this.padding.Right = 4;
			this.padding.Bottom = 0;

		
			this.margin.Left = 0;
			this.margin.Top = 4;
			this.margin.Right = 0;
			this.margin.Bottom = 0;

			
			this.linkNormal = SystemColors.ControlText;
			this.linkHot = SystemColors.HotTrack;

			this.fontDecoration = FontStyle.Underline;
		}


		
		public void SetDefaultEmptyValues()
		{
			this.padding = Padding.Empty;
			this.margin = Margin.Empty;
			this.linkNormal = Color.Empty;
			this.linkHot = Color.Empty;
			this.fontDecoration = FontStyle.Underline;
		}

	



		[Description("The amount of space between individual TaskItems along each side of the TaskItem")]
		public Margin Margin
		{
			get
			{
				return this.margin;
			}

			set
			{
				if (this.margin != value)
				{
					this.margin = value;

				}
			}
		}

		private bool ShouldSerializeMargin()
		{
			return this.Margin != Margin.Empty;
		}

		[Description("The amount of space around the text along each side of the TaskItem")]
		public Padding Padding
		{
			get
			{
				return this.padding;
			}

			set
			{
				if (this.padding != value)
				{
					this.padding = value;

				}
			}
		}


		private bool ShouldSerializePadding()
		{
			return this.Padding != Padding.Empty;
		}

		[Description("The foreground color of a normal link")]
		public Color LinkColor
		{
			get
			{
				return this.linkNormal;
			}

			set
			{
				if (this.linkNormal != value)
				{
					this.linkNormal = value;
				}
			}
		}

		private bool ShouldSerializeLinkColor()
		{
			return this.LinkColor != Color.Empty;
		}


		[Description("The foreground color of a highlighted link")]
		public Color HotLinkColor
		{
			get
			{
				return this.linkHot;
			}

			set
			{
				if (this.linkHot != value)
				{
					this.linkHot = value;
				}
			}
		}


		
		private bool ShouldSerializeHotLinkColor()
		{
			return this.HotLinkColor != Color.Empty;
		}


		[DefaultValue(FontStyle.Underline),
		Description("")]
		public FontStyle FontDecoration
		{
			get
			{
				return this.fontDecoration;
			}

			set
			{
				if (!Enum.IsDefined(typeof(FontStyle), value)) 
				{
					throw new InvalidEnumArgumentException("value", (int) value, typeof(FontStyle));
				}

				if (this.fontDecoration != value)
				{
					this.fontDecoration = value;

				}
			}
		}

		[Serializable()]
			public class TaskItemInfoSurrogate : ISerializable
		{
			
			public Padding Padding; 
			
			
			public Margin Margin;
			
			public string LinkNormal;
			
			public string LinkHot;
			
			
			public FontStyle FontDecoration;

			public int Version = 3300;

			public TaskItemInfoSurrogate()
			{
				this.Padding = Padding.Empty;
				this.Margin = Margin.Empty;

				this.LinkNormal = ThemeManager.ConvertColorToString(Color.Empty);
				this.LinkHot = ThemeManager.ConvertColorToString(Color.Empty);

				this.FontDecoration = FontStyle.Regular;
			}

		
			public void Load(TaskItemInfo taskItemInfo)
			{
				this.Padding = taskItemInfo.Padding;
				this.Margin = taskItemInfo.Margin;

				this.LinkNormal = ThemeManager.ConvertColorToString(taskItemInfo.LinkColor);
				this.LinkHot = ThemeManager.ConvertColorToString(taskItemInfo.HotLinkColor);

				this.FontDecoration = taskItemInfo.FontDecoration;
			}


			public TaskItemInfo Save()
			{
				TaskItemInfo taskItemInfo = new TaskItemInfo();

				taskItemInfo.Padding = this.Padding;
				taskItemInfo.Margin = this.Margin;

				taskItemInfo.LinkColor = ThemeManager.ConvertStringToColor(this.LinkNormal);
				taskItemInfo.HotLinkColor = ThemeManager.ConvertStringToColor(this.LinkHot);

				taskItemInfo.FontDecoration = this.FontDecoration;
				
				return taskItemInfo;
			}


		
			[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter=true)]
			public void GetObjectData(SerializationInfo info, StreamingContext context)
			{
				info.AddValue("Version", this.Version);
				
				info.AddValue("Padding", this.Padding);
				info.AddValue("Margin", this.Margin);

				info.AddValue("LinkNormal", this.LinkNormal);
				info.AddValue("LinkHot", this.LinkHot);

				info.AddValue("FontDecoration", this.FontDecoration);
			}


			
			[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter=true)]
			protected TaskItemInfoSurrogate(SerializationInfo info, StreamingContext context) : base()
			{
				int version = info.GetInt32("Version");
				
				this.Padding = (Padding) info.GetValue("Padding", typeof(Padding));
				this.Margin = (Margin) info.GetValue("Margin", typeof(Margin));
				
				this.LinkNormal = info.GetString("LinkNormal");
				this.LinkHot = info.GetString("LinkHot");

				this.FontDecoration = (FontStyle) info.GetValue("FontDecoration", typeof(FontStyle));
			}

			
		}

		
	}


	
	internal class TaskItemInfoConverter : ExpandableObjectConverter
	{
		
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string) && value is TaskItemInfo)
			{
				return "";
			}

			return base.ConvertTo(context, culture, value, destinationType);
		}
	}

	
	public class ExpandoInfo : IDisposable
	{
		
		private Color specialBackColor;
		
		
		private Color normalBackColor;

		private Border specialBorder;
		
	
		private Border normalBorder;
		
		
		private Color specialBorderColor;
	
		private Color normalBorderColor;

		
		private Padding specialPadding;
		
		private Padding normalPadding;

		private ContentAlignment watermarkAlignment;

		private Image specialBackImage;

		private Image normalBackImage;

		
		private Expando owner;

		public ExpandoInfo()
		{
			
			this.specialBackColor = Color.Transparent;
			this.normalBackColor = Color.Transparent;

		
			this.specialBorder = new Border(1, 0, 1, 1);
			this.specialBorderColor = Color.Transparent;

			this.normalBorder = new Border(1, 0, 1, 1);
			this.normalBorderColor = Color.Transparent;

			
			this.specialPadding = new Padding(12, 10, 12, 10);
			this.normalPadding = new Padding(12, 10, 12, 10);

			this.specialBackImage = null;
			this.normalBackImage = null;

			this.watermarkAlignment = ContentAlignment.BottomRight;

			this.owner = null;
		}

		
		public void SetDefaultValues()
		{
			
			this.specialBackColor = SystemColors.Window;
			this.normalBackColor = SystemColors.Window;

			
			this.specialBorder.Left = 1;
			this.specialBorder.Top = 0;
			this.specialBorder.Right = 1;
			this.specialBorder.Bottom = 1;

			this.specialBorderColor = SystemColors.Highlight;

			this.normalBorder.Left = 1;
			this.normalBorder.Top = 0;
			this.normalBorder.Right = 1;
			this.normalBorder.Bottom = 1;

			this.normalBorderColor = SystemColors.Control;

			
			this.specialPadding.Left = 12;
			this.specialPadding.Top = 10;
			this.specialPadding.Right = 12;
			this.specialPadding.Bottom = 10;
			
			this.normalPadding.Left = 12;
			this.normalPadding.Top = 10;
			this.normalPadding.Right = 12;
			this.normalPadding.Bottom = 10;

			this.specialBackImage = null;
			this.normalBackImage = null;

			this.watermarkAlignment = ContentAlignment.BottomRight;
		}


		
		public void SetDefaultEmptyValues()
		{
		
			this.specialBackColor = Color.Empty;
			this.normalBackColor = Color.Empty;

			
			this.specialBorder = Border.Empty;
			this.specialBorderColor = Color.Empty;

			this.normalBorder = Border.Empty;
			this.normalBorderColor = Color.Empty;

			
			this.specialPadding = Padding.Empty;
			this.normalPadding = Padding.Empty;

			this.specialBackImage = null;
			this.normalBackImage = null;

			this.watermarkAlignment = ContentAlignment.BottomRight;
		}


		public void Dispose()
		{
			if (this.specialBackImage != null)
			{
				this.specialBackImage.Dispose();
				this.specialBackImage = null;
			}

			if (this.normalBackImage != null)
			{
				this.normalBackImage.Dispose();
				this.normalBackImage = null;
			}
		}

		
		[Description("The background color of a special Expando")]
		public Color SpecialBackColor
		{
			get
			{
				return this.specialBackColor;
			}

			set
			{
				if (this.specialBackColor != value)
				{
					this.specialBackColor = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		
		private bool ShouldSerializeSpecialBackColor()
		{
			return this.SpecialBackColor != Color.Empty;
		}


		
		[Description("The background color of a normal Expando")]
		public Color NormalBackColor
		{
			get
			{
				return this.normalBackColor;
			}

			set
			{
				if (this.normalBackColor != value)
				{
					this.normalBackColor = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		
		private bool ShouldSerializeNormalBackColor()
		{
			return this.NormalBackColor != Color.Empty;
		}

		
		[DefaultValue(ContentAlignment.BottomRight), 
		Description("The alignment for the expando's background image")]
		public ContentAlignment WatermarkAlignment
		{
			get
			{
				return this.watermarkAlignment;
			}

			set
			{
				if (!Enum.IsDefined(typeof(ContentAlignment), value)) 
				{
					throw new InvalidEnumArgumentException("value", (int) value, typeof(ContentAlignment));
				}

				if (this.watermarkAlignment != value)
				{
					this.watermarkAlignment = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


	
		[DefaultValue(null), 
		Description("")]
		public Image SpecialBackImage
		{
			get
			{
				return this.specialBackImage;
			}

			set
			{
				if (this.specialBackImage != value)
				{
					this.specialBackImage = value;
				}
			}
		}


		
		[DefaultValue(null), 
		Description("")]
		public Image NormalBackImage
		{
			get
			{
				return this.normalBackImage;
			}

			set
			{
				if (this.normalBackImage != value)
				{
					this.normalBackImage = value;
				}
			}
		}

		[Description("The width of the Border along each side of a special Expando")]
		public Border SpecialBorder
		{
			get
			{
				return this.specialBorder;
			}

			set
			{
				if (this.specialBorder != value)
				{
					this.specialBorder = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		
		private bool ShouldSerializeSpecialBorder()
		{
			return this.SpecialBorder != Border.Empty;
		}


		[Description("The width of the Border along each side of a normal Expando")]
		public Border NormalBorder
		{
			get
			{
				return this.normalBorder;
			}

			set
			{
				if (this.normalBorder != value)
				{
					this.normalBorder = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		
		private bool ShouldSerializeNormalBorder()
		{
			return this.NormalBorder != Border.Empty;
		}


		[Description("The border color for a special Expando")]
		public Color SpecialBorderColor
		{
			get
			{
				return this.specialBorderColor;
			}

			set
			{
				if (this.specialBorderColor != value)
				{
					this.specialBorderColor = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		private bool ShouldSerializeSpecialBorderColor()
		{
			return this.SpecialBorderColor != Color.Empty;
		}


		[Description("The border color for a normal Expando")]
		public Color NormalBorderColor
		{
			get
			{
				return this.normalBorderColor;
			}

			set
			{
				if (this.normalBorderColor != value)
				{
					this.normalBorderColor = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		
		private bool ShouldSerializeNormalBorderColor()
		{
			return this.NormalBorderColor != Color.Empty;
		}

	
		[Description("The amount of space between the border and items along each side of a special Expando")]
		public Padding SpecialPadding
		{
			get
			{
				return this.specialPadding;
			}

			set
			{
				if (this.specialPadding != value)
				{
					this.specialPadding = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		private bool ShouldSerializeSpecialPadding()
		{
			return this.SpecialPadding != Padding.Empty;
		}
		

		
		[Description("The amount of space between the border and items along each side of a normal Expando")]
		public Padding NormalPadding
		{
			get
			{
				return this.normalPadding;
			}

			set
			{
				if (this.normalPadding != value)
				{
					this.normalPadding = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		private bool ShouldSerializeNormalPadding()
		{
			return this.NormalPadding != Padding.Empty;
		}

		
		protected internal Expando Expando
		{
			get
			{
				return this.owner;
			}

			set
			{
				this.owner = value;
			}
		}

		
		[Serializable()]
			public class ExpandoInfoSurrogate : ISerializable
		{
			
			public string SpecialBackColor;
			
			public string NormalBackColor;
			
			
			public Border SpecialBorder;
			
			public Border NormalBorder;
			
			
			public string SpecialBorderColor;
			
			
			public string NormalBorderColor;
			
			
			public Padding SpecialPadding;
		
			public Padding NormalPadding;
			
			
			[XmlElementAttribute("SpecialBackImage", typeof(Byte[]), DataType="base64Binary")]
			public byte[] SpecialBackImage;
			
			[XmlElementAttribute("NormalBackImage", typeof(Byte[]), DataType="base64Binary")]
			public byte[] NormalBackImage;
			
			public ContentAlignment WatermarkAlignment;

		
			public int Version = 3300;

			
			public ExpandoInfoSurrogate()
			{
				this.SpecialBackColor = ThemeManager.ConvertColorToString(Color.Empty);
				this.NormalBackColor = ThemeManager.ConvertColorToString(Color.Empty);

				this.SpecialBorder = Border.Empty;
				this.NormalBorder = Border.Empty;

				this.SpecialBorderColor = ThemeManager.ConvertColorToString(Color.Empty);
				this.NormalBorderColor = ThemeManager.ConvertColorToString(Color.Empty);
				
				this.SpecialPadding = Padding.Empty;
				this.NormalPadding = Padding.Empty;

				this.SpecialBackImage = new byte[0];
				this.NormalBackImage = new byte[0];

				this.WatermarkAlignment = ContentAlignment.BottomRight;
			}

			
			public void Load(ExpandoInfo expandoInfo)
			{
				this.SpecialBackColor = ThemeManager.ConvertColorToString(expandoInfo.SpecialBackColor);
				this.NormalBackColor =ThemeManager.ConvertColorToString( expandoInfo.NormalBackColor);

				this.SpecialBorder = expandoInfo.SpecialBorder;
				this.NormalBorder = expandoInfo.NormalBorder;

				this.SpecialBorderColor = ThemeManager.ConvertColorToString(expandoInfo.SpecialBorderColor);
				this.NormalBorderColor = ThemeManager.ConvertColorToString(expandoInfo.NormalBorderColor);

				this.SpecialPadding = expandoInfo.SpecialPadding;
				this.NormalPadding = expandoInfo.NormalPadding;

				this.SpecialBackImage = ThemeManager.ConvertImageToByteArray(expandoInfo.SpecialBackImage);
				this.NormalBackImage = ThemeManager.ConvertImageToByteArray(expandoInfo.NormalBackImage);

				this.WatermarkAlignment = expandoInfo.WatermarkAlignment;
			}



			public ExpandoInfo Save()
			{
				ExpandoInfo expandoInfo = new ExpandoInfo();

				expandoInfo.SpecialBackColor = ThemeManager.ConvertStringToColor(this.SpecialBackColor);
				expandoInfo.NormalBackColor = ThemeManager.ConvertStringToColor(this.NormalBackColor);

				expandoInfo.SpecialBorder = this.SpecialBorder;
				expandoInfo.NormalBorder = this.NormalBorder;

				expandoInfo.SpecialBorderColor = ThemeManager.ConvertStringToColor(this.SpecialBorderColor);
				expandoInfo.NormalBorderColor = ThemeManager.ConvertStringToColor(this.NormalBorderColor);
				
				expandoInfo.SpecialPadding = this.SpecialPadding;
				expandoInfo.NormalPadding = this.NormalPadding;

				expandoInfo.SpecialBackImage = ThemeManager.ConvertByteArrayToImage(this.SpecialBackImage);
				expandoInfo.NormalBackImage = ThemeManager.ConvertByteArrayToImage(this.NormalBackImage);
				
				expandoInfo.WatermarkAlignment = this.WatermarkAlignment;
				
				return expandoInfo;
			}


			[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter=true)]
			public void GetObjectData(SerializationInfo info, StreamingContext context)
			{
				info.AddValue("Version", this.Version);
				
				info.AddValue("SpecialBackColor", this.SpecialBackColor);
				info.AddValue("NormalBackColor", this.NormalBackColor);
				
				info.AddValue("SpecialBorder", this.SpecialBorder);
				info.AddValue("NormalBorder", this.NormalBorder);
				
				info.AddValue("SpecialBorderColor", this.SpecialBorderColor);
				info.AddValue("NormalBorderColor", this.NormalBorderColor);
				
				info.AddValue("SpecialPadding", this.SpecialPadding);
				info.AddValue("NormalPadding", this.NormalPadding);
				
				info.AddValue("SpecialBackImage", this.SpecialBackImage);
				info.AddValue("NormalBackImage", this.NormalBackImage);
				
				info.AddValue("WatermarkAlignment", this.WatermarkAlignment);
			}



			[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter=true)]
			protected ExpandoInfoSurrogate(SerializationInfo info, StreamingContext context) : base()
			{
				int version = info.GetInt32("Version");

				this.SpecialBackColor = info.GetString("SpecialBackColor");
				this.NormalBackColor = info.GetString("NormalBackColor");

				this.SpecialBorder = (Border) info.GetValue("SpecialBorder", typeof(Border));
				this.NormalBorder = (Border) info.GetValue("NormalBorder", typeof(Border));

				this.SpecialBorderColor = info.GetString("SpecialBorderColor");
				this.NormalBorderColor = info.GetString("NormalBorderColor");

				this.SpecialPadding = (Padding) info.GetValue("SpecialPadding", typeof(Padding));
				this.NormalPadding = (Padding) info.GetValue("NormalPadding", typeof(Padding));

				this.SpecialBackImage = (byte[]) info.GetValue("SpecialBackImage", typeof(byte[]));
				this.NormalBackImage = (byte[]) info.GetValue("NormalBackImage", typeof(byte[]));

				this.WatermarkAlignment = (ContentAlignment) info.GetValue("WatermarkAlignment", typeof(ContentAlignment));
			}

		
		}

	}


	internal class ExpandoInfoConverter : ExpandableObjectConverter
	{
		
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string) && value is ExpandoInfo)
			{
				return "";
			}

			return base.ConvertTo(context, culture, value, destinationType);
		}


	
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			
			PropertyDescriptorCollection collection = TypeDescriptor.GetProperties(typeof(ExpandoInfo), attributes);

			string[] s = new string[9];
			s[0] = "NormalBackColor";
			s[1] = "SpecialBackColor";
			s[2] = "NormalBorder";
			s[3] = "SpecialBorder";
			s[4] = "NormalBorderColor";
			s[5] = "SpecialBorderColor";
			s[6] = "NormalPadding";
			s[7] = "SpecialPadding";
			s[8] = "WatermarkAlignment";

			return collection.Sort(s);
		}
	}

	
	public class HeaderInfo : IDisposable
	{
		private Font titleFont;

		private int margin;

		
		private Image specialBackImage;

		
		private Image normalBackImage;

		
		private int backImageWidth;

		
		private int backImageHeight;

		
		private Color specialTitle;
		
		
		private Color normalTitle;

		private Color specialTitleHot;

		private Color normalTitleHot;

		
		private ContentAlignment specialAlignment;

		
		private ContentAlignment normalAlignment;
		
		private Padding specialPadding;

		private Padding normalPadding;

		
		private Border specialBorder;

		
		private Border normalBorder;

		private Color specialBorderColor;

		
		private Color normalBorderColor;

		private Color specialBackColor;

		private Color normalBackColor;

		
		private Image specialArrowUp;
		
		private Image specialArrowUpHot;
		
		
		private Image specialArrowDown;
		
		private Image specialArrowDownHot;
		
		
		private Image normalArrowUp;
		
		
		private Image normalArrowUpHot;
		
		
		private Image normalArrowDown;
		

		private Image normalArrowDownHot;

		private bool useTitleGradient;

	
	
		private Color specialGradientStartColor;

		private Color specialGradientEndColor;


		private Color normalGradientStartColor;


		private Color normalGradientEndColor;


		private float gradientOffset;


		private int titleRadius;


		private Expando owner;

	
		private bool rightToLeft;



		
		public HeaderInfo()
		{
			
			if (Environment.OSVersion.Version.Major >= 5)
			{
				
				this.titleFont = new Font("Tahoma", 8.25f, FontStyle.Bold);
			}
			else
			{
				
				this.titleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
			}

			this.margin = 15;

			
			this.specialTitle = Color.Transparent;
			this.specialTitleHot = Color.Transparent;
			
			this.normalTitle = Color.Transparent;
			this.normalTitleHot = Color.Transparent;
			
			this.specialAlignment = ContentAlignment.MiddleLeft;
			this.normalAlignment = ContentAlignment.MiddleLeft;

			this.specialPadding = new Padding(10, 0, 1, 0);
			this.normalPadding = new Padding(10, 0, 1, 0);

		
			this.specialBorder = new Border(2, 2, 2, 0);
			this.specialBorderColor = Color.Transparent;

			this.normalBorder = new Border(2, 2, 2, 0);
			this.normalBorderColor = Color.Transparent;
			
			this.specialBackColor = Color.Transparent;
			this.normalBackColor = Color.Transparent;

			this.specialBackImage = null;
			this.normalBackImage = null;

			this.backImageWidth = -1;
			this.backImageHeight = -1;

		
			this.specialArrowUp = null;
			this.specialArrowUpHot = null;
			this.specialArrowDown = null;
			this.specialArrowDownHot = null;

			this.normalArrowUp = null;
			this.normalArrowUpHot = null;
			this.normalArrowDown = null;
			this.normalArrowDownHot = null;

			this.useTitleGradient = false;
			this.specialGradientStartColor = Color.White;
			this.specialGradientEndColor = SystemColors.Highlight;
			this.normalGradientStartColor = Color.White;
			this.normalGradientEndColor = SystemColors.Highlight;
			this.gradientOffset = 0.5f;
			this.titleRadius = 5;

			this.owner = null;
			this.rightToLeft = false;
		}

	
		public void SetDefaultValues()
		{
			if (Environment.OSVersion.Version.Major >= 5)
			{
				
				this.titleFont = new Font("Tahoma", 8.25f, FontStyle.Bold);
			}
			else
			{
				
				this.titleFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
			}

			this.margin = 15;

			this.specialTitle = SystemColors.HighlightText;
			this.specialTitleHot = SystemColors.HighlightText;
			
			this.normalTitle = SystemColors.ControlText;
			this.normalTitleHot = SystemColors.ControlText;
			
			this.specialAlignment = ContentAlignment.MiddleLeft;
			this.normalAlignment = ContentAlignment.MiddleLeft;

		
			this.specialPadding.Left = 10;
			this.specialPadding.Top = 0;
			this.specialPadding.Right = 1;
			this.specialPadding.Bottom = 0;

			this.normalPadding.Left = 10;
			this.normalPadding.Top = 0;
			this.normalPadding.Right = 1;
			this.normalPadding.Bottom = 0;

		
			this.specialBorder.Left = 2;
			this.specialBorder.Top = 2;
			this.specialBorder.Right = 2;
			this.specialBorder.Bottom = 0;

			this.specialBorderColor = SystemColors.Highlight;
			this.specialBackColor = SystemColors.Highlight;

			this.normalBorder.Left = 2;
			this.normalBorder.Top = 2;
			this.normalBorder.Right = 2;
			this.normalBorder.Bottom = 0;

			this.normalBorderColor = SystemColors.Control;
			this.normalBackColor = SystemColors.Control;

			
			this.specialBackImage = null;
			this.normalBackImage = null;

			this.backImageWidth = 186;
			this.backImageHeight = 25;

		
			this.specialArrowUp = null;
			this.specialArrowUpHot = null;
			this.specialArrowDown = null;
			this.specialArrowDownHot = null;

			this.normalArrowUp = null;
			this.normalArrowUpHot = null;
			this.normalArrowDown = null;
			this.normalArrowDownHot = null;

			this.useTitleGradient = false;
			this.specialGradientStartColor = Color.White;
			this.specialGradientEndColor = SystemColors.Highlight;
			this.normalGradientStartColor = Color.White;
			this.normalGradientEndColor = SystemColors.Highlight;
			this.gradientOffset = 0.5f;
			this.titleRadius = 2;

			this.rightToLeft = false;
		}
		

		public void SetDefaultEmptyValues()
		{
			
			this.titleFont = null;

			this.margin = 15;

			
			this.specialTitle = Color.Empty;
			this.specialTitleHot = Color.Empty;
			
			this.normalTitle = Color.Empty;
			this.normalTitleHot = Color.Empty;
			
			this.specialAlignment = ContentAlignment.MiddleLeft;
			this.normalAlignment = ContentAlignment.MiddleLeft;

			this.specialPadding = Padding.Empty;
			this.normalPadding = Padding.Empty;

	
			this.specialBorder = Border.Empty;
			this.specialBorderColor = Color.Empty;
			this.specialBackColor = Color.Empty;

			this.normalBorder = Border.Empty;
			this.normalBorderColor = Color.Empty;
			this.normalBackColor = Color.Empty;

			this.specialBackImage = null;
			this.normalBackImage = null;

			this.backImageWidth = 186;
			this.backImageHeight = 25;

	
			this.specialArrowUp = null;
			this.specialArrowUpHot = null;
			this.specialArrowDown = null;
			this.specialArrowDownHot = null;

			this.normalArrowUp = null;
			this.normalArrowUpHot = null;
			this.normalArrowDown = null;
			this.normalArrowDownHot = null;

			this.useTitleGradient = false;
			this.specialGradientStartColor = Color.Empty;
			this.specialGradientEndColor = Color.Empty;
			this.normalGradientStartColor = Color.Empty;
			this.normalGradientEndColor = Color.Empty;
			this.gradientOffset = 0.5f;
			this.titleRadius = 2;

			this.rightToLeft = false;
		}

		public void Dispose()
		{
			if (this.specialBackImage != null)
			{
				this.specialBackImage.Dispose();
				this.specialBackImage = null;
			}

			if (this.normalBackImage != null)
			{
				this.normalBackImage.Dispose();
				this.normalBackImage = null;
			}


			if (this.specialArrowUp != null)
			{
				this.specialArrowUp.Dispose();
				this.specialArrowUp = null;
			}

			if (this.specialArrowUpHot != null)
			{
				this.specialArrowUpHot.Dispose();
				this.specialArrowUpHot = null;
			}

			if (this.specialArrowDown != null)
			{
				this.specialArrowDown.Dispose();
				this.specialArrowDown = null;
			}

			if (this.specialArrowDownHot != null)
			{
				this.specialArrowDownHot.Dispose();
				this.specialArrowDownHot = null;
			}
			
			if (this.normalArrowUp != null)
			{
				this.normalArrowUp.Dispose();
				this.normalArrowUp = null;
			}

			if (this.normalArrowUpHot != null)
			{
				this.normalArrowUpHot.Dispose();
				this.normalArrowUpHot = null;
			}

			if (this.normalArrowDown != null)
			{
				this.normalArrowDown.Dispose();
				this.normalArrowDown = null;
			}

			if (this.normalArrowDownHot != null)
			{
				this.normalArrowDownHot.Dispose();
				this.normalArrowDownHot = null;
			}
		}

	

		[Description("The width of the border along each side of a special Expando's Title Bar")]
		public Border SpecialBorder
		{
			get
			{
				return this.specialBorder;
			}

			set
			{
				if (this.specialBorder != value)
				{
					this.specialBorder = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		
		private bool ShouldSerializeSpecialBorder()
		{
			return this.SpecialBorder != Border.Empty;
		}


		[Description("The border color for a special Expandos titlebar")]
		public Color SpecialBorderColor
		{
			get
			{
				return this.specialBorderColor;
			}

			set
			{
				if (this.specialBorderColor != value)
				{
					this.specialBorderColor = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		private bool ShouldSerializeSpecialBorderColor()
		{
			return this.SpecialBorderColor != Color.Empty;
		}


		[Description("The background Color for a special Expandos titlebar")]
		public Color SpecialBackColor
		{
			get
			{
				return this.specialBackColor;
			}

			set
			{
				if (this.specialBackColor != value)
				{
					this.specialBackColor = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		private bool ShouldSerializeSpecialBackColor()
		{
			return this.SpecialBackColor != Color.Empty;
		}


		
		[Description("The width of the border along each side of a normal Expando's Title Bar")]
		public Border NormalBorder
		{
			get
			{
				return this.normalBorder;
			}

			set
			{
				if (this.normalBorder != value)
				{
					this.normalBorder = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


	
		private bool ShouldSerializeNormalBorder()
		{
			return this.NormalBorder != Border.Empty;
		}


	
		[Description("The border color for a normal Expandos titlebar")]
		public Color NormalBorderColor
		{
			get
			{
				return this.normalBorderColor;
			}

			set
			{
				if (this.normalBorderColor != value)
				{
					this.normalBorderColor = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		private bool ShouldSerializeNormalBorderColor()
		{
			return this.NormalBorderColor != Color.Empty;
		}

		[Description("The background Color for a normal Expandos titlebar")]
		public Color NormalBackColor
		{
			get
			{
				return this.normalBackColor;
			}

			set
			{
				if (this.normalBackColor != value)
				{
					this.normalBackColor = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}



		private bool ShouldSerializeNormalBackColor()
		{
			return this.NormalBackColor != Color.Empty;
		}

		[DefaultValue(null), 
		Description("The Font used to render the titlebar's text")]
		public Font TitleFont
		{
			get
			{
				return this.titleFont;
			}

			set
			{
				if (this.titleFont != value)
				{
					this.titleFont = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}

		protected internal string FontName
		{
			get
			{
				return this.TitleFont.Name;
			}

			set
			{
				this.TitleFont = new Font(value, this.TitleFont.SizeInPoints, this.TitleFont.Style);
			}
		}


		protected internal float FontSize
		{
			get
			{
				return this.TitleFont.SizeInPoints;
			}

			set
			{
				this.TitleFont = new Font(this.TitleFont.Name, value, this.TitleFont.Style);
			}
		}


		protected internal FontStyle FontWeight
		{
			get
			{
				return this.TitleFont.Style;
			}

			set
			{
				value |= this.TitleFont.Style;
				
				this.TitleFont = new Font(this.TitleFont.Name, this.TitleFont.SizeInPoints, value);
			}
		}
		

		protected internal FontStyle FontStyle
		{
			get
			{
				return this.TitleFont.Style;
			}

			set
			{
				value |= this.TitleFont.Style;
				
				this.TitleFont = new Font(this.TitleFont.Name, this.TitleFont.SizeInPoints, value);
			}
		}

	
		[DefaultValue(null), 
		Description("The background image for a special titlebar")]
		public Image SpecialBackImage
		{
			get
			{
				return this.specialBackImage;
			}

			set
			{
				if (this.specialBackImage != value)
				{
					this.specialBackImage = value;

					if (value!= null)
					{
						this.backImageWidth = value.Width;
						this.backImageHeight = value.Height;
					}

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		[DefaultValue(null), 
		Description("The background image for a normal titlebar")]
		public Image NormalBackImage
		{
			get
			{
				return this.normalBackImage;
			}

			set
			{
				if (this.normalBackImage != value)
				{
					this.normalBackImage = value;

					if (value!= null)
					{
						this.backImageWidth = value.Width;
						this.backImageHeight = value.Height;
					}

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		protected internal int BackImageWidth
		{
			get
			{
				if (this.backImageWidth == -1)
				{
					return 186;
				}
				
				return this.backImageWidth;
			}

			set
			{
				this.backImageWidth = value;
			}
		}


		protected internal int BackImageHeight
		{
			get
			{
				if (this.backImageHeight < 23)
				{
					return 23;
				}
				
				return this.backImageHeight;
			}

			set
			{
				this.backImageHeight = value;
			}
		}
		
	
		[DefaultValue(null), 
		Description("A special Expando's collapse arrow image in it's normal state")]
		public Image SpecialArrowUp
		{
			get
			{
				return this.specialArrowUp;
			}

			set
			{
				if (this.specialArrowUp != value)
				{
					this.specialArrowUp = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		[DefaultValue(null), 
		Description("A special Expando's collapse arrow image in it's highlighted state")]
		public Image SpecialArrowUpHot
		{
			get
			{
				return this.specialArrowUpHot;
			}

			set
			{
				if (this.specialArrowUpHot != value)
				{
					this.specialArrowUpHot = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		[DefaultValue(null), 
		Description("A special Expando's expand arrow image in it's normal state")]
		public Image SpecialArrowDown
		{
			get
			{
				return this.specialArrowDown;
			}

			set
			{
				if (this.specialArrowDown != value)
				{
					this.specialArrowDown = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


	
		[DefaultValue(null), 
		Description("A special Expando's expand arrow image in it's highlighted state")]
		public Image SpecialArrowDownHot
		{
			get
			{
				return this.specialArrowDownHot;
			}

			set
			{
				if (this.specialArrowDownHot != value)
				{
					this.specialArrowDownHot = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}
		
		
		
		[DefaultValue(null), 
		Description("A normal Expando's collapse arrow image in it's normal state")]
		public Image NormalArrowUp
		{
			get
			{
				return this.normalArrowUp;
			}

			set
			{
				if (this.normalArrowUp != value)
				{
					this.normalArrowUp = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


	
		[DefaultValue(null), 
		Description("A normal Expando's collapse arrow image in it's highlighted state")]
		public Image NormalArrowUpHot
		{
			get
			{
				return this.normalArrowUpHot;
			}

			set
			{
				if (this.normalArrowUpHot != value)
				{
					this.normalArrowUpHot = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


	
		[DefaultValue(null), 
		Description("A normal Expando's expand arrow image in it's normal state")]
		public Image NormalArrowDown
		{
			get
			{
				return this.normalArrowDown;
			}

			set
			{
				if (this.normalArrowDown != value)
				{
					this.normalArrowDown = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		
		[DefaultValue(null), 
		Description("A normal Expando's expand arrow image in it's highlighted state")]
		public Image NormalArrowDownHot
		{
			get
			{
				return this.normalArrowDownHot;
			}

			set
			{
				if (this.normalArrowDownHot != value)
				{
					this.normalArrowDownHot = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}

		internal void SetUnthemedArrowImages()
		{
			
			System.Reflection.Assembly myAssembly;
			myAssembly = this.GetType().Assembly;
			ResourceManager myManager = new ResourceManager("System.MyControl.MyXPExplorerBar.ExpandoArrows", myAssembly);
				
		
			try
			{
				this.specialArrowDown = new Bitmap((Image) myManager.GetObject("SPECIALGROUPEXPAND"));
				this.specialArrowDownHot = new Bitmap((Image) myManager.GetObject("SPECIALGROUPEXPANDHOT"));
				this.specialArrowUp = new Bitmap((Image) myManager.GetObject("SPECIALGROUPCOLLAPSE"));
				this.specialArrowUpHot = new Bitmap((Image) myManager.GetObject("SPECIALGROUPCOLLAPSEHOT"));
				
				this.normalArrowDown = new Bitmap((Image) myManager.GetObject("NORMALGROUPEXPAND"));
				this.normalArrowDownHot = new Bitmap((Image) myManager.GetObject("NORMALGROUPEXPANDHOT"));
				this.normalArrowUp = new Bitmap((Image) myManager.GetObject("NORMALGROUPCOLLAPSE"));
				this.normalArrowUpHot = new Bitmap((Image) myManager.GetObject("NORMALGROUPCOLLAPSEHOT"));
			}
			catch
			{

			}
		}

	
		[DefaultValue(15), 
		Description("The margin around the titlebar")]
		public int Margin
		{
			get
			{
				return this.margin;
			}

			set
			{
				if (this.margin != value)
				{
					this.margin = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}

	
		[Description("The amount of space between the border and items along each side of a special Expandos Title Bar")]
		public Padding SpecialPadding
		{
			get
			{
				return this.specialPadding;
			}

			set
			{
				if (this.specialPadding != value)
				{
					this.specialPadding = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		
		private bool ShouldSerializeSpecialPadding()
		{
			return this.SpecialPadding != Padding.Empty;
		}


		
		[Description("The amount of space between the border and items along each side of a normal Expandos Title Bar")]
		public Padding NormalPadding
		{
			get
			{
				return this.normalPadding;
			}

			set
			{
				if (this.normalPadding != value)
				{
					this.normalPadding = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


	
		private bool ShouldSerializeNormalPadding()
		{
			return this.NormalPadding != Padding.Empty;
		}

	
		[Description("The color of the text displayed in a special Expandos titlebar in it's normal state")]
		public Color SpecialTitleColor
		{
			get
			{
				return this.specialTitle;
			}

			set
			{
				if (this.specialTitle != value)
				{
					this.specialTitle = value;

					
					if (this.SpecialTitleHotColor == Color.Transparent)
					{
						this.SpecialTitleHotColor = value;
					}

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


	
		private bool ShouldSerializeSpecialTitleColor()
		{
			return this.SpecialTitleColor != Color.Empty;
		}


		[Description("The color of the text displayed in a special Expandos titlebar in it's highlighted state")]
		public Color SpecialTitleHotColor
		{
			get
			{
				return this.specialTitleHot;
			}

			set
			{
				if (this.specialTitleHot != value)
				{
					this.specialTitleHot = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		
		private bool ShouldSerializeSpecialTitleHotColor()
		{
			return this.SpecialTitleHotColor != Color.Empty;
		}


	
		[Description("The color of the text displayed in a normal Expandos titlebar in it's normal state")]
		public Color NormalTitleColor
		{
			get
			{
				return this.normalTitle;
			}

			set
			{
				if (this.normalTitle != value)
				{
					this.normalTitle = value;

					if (this.NormalTitleHotColor == Color.Transparent)
					{
						this.NormalTitleHotColor = value;
					}

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		private bool ShouldSerializeNormalTitleColor()
		{
			return this.NormalTitleColor != Color.Empty;
		}


		[Description("The color of the text displayed in a normal Expandos titlebar in it's highlighted state")]
		public Color NormalTitleHotColor
		{
			get
			{
				return this.normalTitleHot;
			}

			set
			{
				if (this.normalTitleHot != value)
				{
					this.normalTitleHot = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		private bool ShouldSerializeNormalTitleHotColor()
		{
			return this.NormalTitleHotColor != Color.Empty;
		}


		
		[DefaultValue(ContentAlignment.MiddleLeft), 
		Description("The alignment of the text displayed in a special Expandos titlebar")]
		public ContentAlignment SpecialAlignment
		{
			get
			{
				return this.specialAlignment;
			}

			set
			{
				if (!Enum.IsDefined(typeof(ContentAlignment), value)) 
				{
					throw new InvalidEnumArgumentException("value", (int) value, typeof(ContentAlignment));
				}

				if (this.specialAlignment != value)
				{
					this.specialAlignment = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		[DefaultValue(ContentAlignment.MiddleLeft), 
		Description("The alignment of the text displayed in a normal Expandos titlebar")]
		public ContentAlignment NormalAlignment
		{
			get
			{
				return this.normalAlignment;
			}

			set
			{
				if (!Enum.IsDefined(typeof(ContentAlignment), value)) 
				{
					throw new InvalidEnumArgumentException("value", (int) value, typeof(ContentAlignment));
				}

				if (this.normalAlignment != value)
				{
					this.normalAlignment = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}

		[DefaultValue(false),
		Description("")]
		public bool TitleGradient
		{
			get
			{
				return this.useTitleGradient;
			}

			set
			{
				if (this.useTitleGradient != value)
				{
					this.useTitleGradient = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		
		[Description("")]
		public Color SpecialGradientStartColor
		{
			get
			{
				return this.specialGradientStartColor;
			}

			set
			{
				if (this.specialGradientStartColor != value)
				{
					this.specialGradientStartColor = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		
		private bool ShouldSerializeSpecialGradientStartColor()
		{
			return this.SpecialGradientStartColor != Color.Empty;
		}


		[Description("")]
		public Color SpecialGradientEndColor
		{
			get
			{
				return this.specialGradientEndColor;
			}

			set
			{
				if (this.specialGradientEndColor != value)
				{
					this.specialGradientEndColor = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		private bool ShouldSerializeSpecialGradientEndColor()
		{
			return this.SpecialGradientEndColor != Color.Empty;
		}


	
		[Description("")]
		public Color NormalGradientStartColor
		{
			get
			{
				return this.normalGradientStartColor;
			}

			set
			{
				if (this.normalGradientStartColor != value)
				{
					this.normalGradientStartColor = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		private bool ShouldSerializeNormalGradientStartColor()
		{
			return this.NormalGradientStartColor != Color.Empty;
		}


		[Description("")]
		public Color NormalGradientEndColor
		{
			get
			{
				return this.normalGradientEndColor;
			}

			set
			{
				if (this.normalGradientEndColor != value)
				{
					this.normalGradientEndColor = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


		private bool ShouldSerializeNormalGradientEndColor()
		{
			return this.NormalGradientEndColor != Color.Empty;
		}


		[DefaultValue(0.5f),
		Description("")]
		public float GradientOffset
		{
			get
			{
				return this.gradientOffset;
			}

			set
			{
				if (value < 0)
				{
					value = 0f;
				}
				else if (value > 1)
				{
					value = 1f;
				}
				
				if (this.gradientOffset != value)
				{
					this.gradientOffset = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}


	
		[DefaultValue(2),
		Description("")]
		public int TitleRadius
		{
			get
			{
				return this.titleRadius;
			}

			set
			{
				if (value < 0)
				{
					value = 0;
				}
				else if (value > this.BackImageHeight)
				{
					value = this.BackImageHeight;
				}
				
				if (this.titleRadius != value)
				{
					this.titleRadius = value;

					if (this.Expando != null)
					{
						this.Expando.FireCustomSettingsChanged(EventArgs.Empty);
					}
				}
			}
		}

	
		protected internal Expando Expando
		{
			get
			{
				return this.owner;
			}

			set
			{
				this.owner = value;
			}
		}


	
		internal bool RightToLeft
		{
			get
			{
				return this.rightToLeft;
			}

			set
			{
				this.rightToLeft = value;
			}
		}

		[Serializable()]
			public class HeaderInfoSurrogate : ISerializable
		{
			
			public string FontName;
		
			public float FontSize;
			
		
			public FontStyle FontStyle;
			
			public int Margin;
			
			[XmlElementAttribute("SpecialBackImage", typeof(Byte[]), DataType="base64Binary")]
			public byte[] SpecialBackImage;
			
			
			[XmlElementAttribute("NormalBackImage", typeof(Byte[]), DataType="base64Binary")]
			public byte[] NormalBackImage;
		
			public string SpecialTitle;
			
	
			public string NormalTitle;
			
		
			public string SpecialTitleHot;
			
			public string NormalTitleHot;
			
		
			public ContentAlignment SpecialAlignment;
			
			
			public ContentAlignment NormalAlignment;
			
			
			public Padding SpecialPadding;
			
		
			public Padding NormalPadding;
	
			public Border SpecialBorder;
			
			
			public Border NormalBorder;
			
		
			public string SpecialBorderColor;
			
			public string NormalBorderColor;
		
			public string SpecialBackColor;
			
			
			public string NormalBackColor;
			
			[XmlElementAttribute("SpecialArrowUp", typeof(Byte[]), DataType="base64Binary")]
			public byte[] SpecialArrowUp;
			
			
			[XmlElementAttribute("SpecialArrowUpHot", typeof(Byte[]), DataType="base64Binary")]
			public byte[] SpecialArrowUpHot;
			
			
			[XmlElementAttribute("SpecialArrowDown", typeof(Byte[]), DataType="base64Binary")]
			public byte[] SpecialArrowDown;
			
			
			[XmlElementAttribute("SpecialArrowDownHot", typeof(Byte[]), DataType="base64Binary")]
			public byte[] SpecialArrowDownHot;
			
			[XmlElementAttribute("NormalArrowUp", typeof(Byte[]), DataType="base64Binary")]
			public byte[] NormalArrowUp;
			
			
			[XmlElementAttribute("NormalArrowUpHot", typeof(Byte[]), DataType="base64Binary")]
			public byte[] NormalArrowUpHot;
			
			[XmlElementAttribute("NormalArrowDown", typeof(Byte[]), DataType="base64Binary")]
			public byte[] NormalArrowDown;
			
			
			[XmlElementAttribute("NormalArrowDownHot", typeof(Byte[]), DataType="base64Binary")]
			public byte[] NormalArrowDownHot;
			
		
			public bool TitleGradient;
			
			public string SpecialGradientStartColor;
			
			public string SpecialGradientEndColor;
		
			public string NormalGradientStartColor;
			
			
			public string NormalGradientEndColor;
			
			
			public float GradientOffset;
			
			public int TitleRadius;

		
			public int Version = 3300;

		


			public HeaderInfoSurrogate()
			{
				this.FontName = null;
				this.FontSize = 8.25f;
				this.FontStyle = FontStyle.Regular;
				this.Margin = 15;

				this.SpecialBackImage = new byte[0];
				this.NormalBackImage = new byte[0];

				this.SpecialTitle = ThemeManager.ConvertColorToString(Color.Empty);
				this.NormalTitle = ThemeManager.ConvertColorToString(Color.Empty);
				this.SpecialTitleHot = ThemeManager.ConvertColorToString(Color.Empty);
				this.NormalTitleHot = ThemeManager.ConvertColorToString(Color.Empty);

				this.SpecialAlignment = ContentAlignment.MiddleLeft;
				this.NormalAlignment = ContentAlignment.MiddleLeft;

				this.SpecialPadding = Padding.Empty;
				this.NormalPadding = Padding.Empty;

				this.SpecialBorder = Border.Empty;
				this.NormalBorder = Border.Empty;
				this.SpecialBorderColor = ThemeManager.ConvertColorToString(Color.Empty);
				this.NormalBorderColor = ThemeManager.ConvertColorToString(Color.Empty);
				
				this.SpecialBackColor = ThemeManager.ConvertColorToString(Color.Empty);
				this.NormalBackColor = ThemeManager.ConvertColorToString(Color.Empty);

				this.SpecialArrowUp = new byte[0];
				this.SpecialArrowUpHot = new byte[0];
				this.SpecialArrowDown = new byte[0];
				this.SpecialArrowDownHot = new byte[0];
				this.NormalArrowUp = new byte[0];
				this.NormalArrowUpHot = new byte[0];
				this.NormalArrowDown = new byte[0];
				this.NormalArrowDownHot = new byte[0];

				this.TitleGradient = false;
				this.SpecialGradientStartColor = ThemeManager.ConvertColorToString(Color.Empty);
				this.SpecialGradientEndColor = ThemeManager.ConvertColorToString(Color.Empty);
				this.NormalGradientStartColor = ThemeManager.ConvertColorToString(Color.Empty);
				this.NormalGradientEndColor = ThemeManager.ConvertColorToString(Color.Empty);
				this.GradientOffset = 0.5f;
			}

		
			public void Load(HeaderInfo headerInfo)
			{
				if (headerInfo.TitleFont != null)
				{
					this.FontName = headerInfo.TitleFont.Name;
					this.FontSize = headerInfo.TitleFont.SizeInPoints;
					this.FontStyle = headerInfo.TitleFont.Style;
				}

				this.Margin = headerInfo.Margin;

				this.SpecialBackImage = ThemeManager.ConvertImageToByteArray(headerInfo.SpecialBackImage);
				this.NormalBackImage = ThemeManager.ConvertImageToByteArray(headerInfo.NormalBackImage);

				this.SpecialTitle = ThemeManager.ConvertColorToString(headerInfo.SpecialTitleColor);
				this.NormalTitle = ThemeManager.ConvertColorToString(headerInfo.NormalTitleColor);
				this.SpecialTitleHot = ThemeManager.ConvertColorToString(headerInfo.SpecialTitleHotColor);
				this.NormalTitleHot = ThemeManager.ConvertColorToString(headerInfo.NormalTitleHotColor);

				this.SpecialAlignment = headerInfo.SpecialAlignment;
				this.NormalAlignment = headerInfo.NormalAlignment;

				this.SpecialPadding = headerInfo.SpecialPadding;
				this.NormalPadding = headerInfo.NormalPadding;

				this.SpecialBorder = headerInfo.SpecialBorder;
				this.NormalBorder = headerInfo.NormalBorder;
				this.SpecialBorderColor = ThemeManager.ConvertColorToString(headerInfo.SpecialBorderColor);
				this.NormalBorderColor = ThemeManager.ConvertColorToString(headerInfo.NormalBorderColor);
				
				this.SpecialBackColor = ThemeManager.ConvertColorToString(headerInfo.SpecialBackColor);
				this.NormalBackColor = ThemeManager.ConvertColorToString(headerInfo.NormalBackColor);

				this.SpecialArrowUp = ThemeManager.ConvertImageToByteArray(headerInfo.SpecialArrowUp);
				this.SpecialArrowUpHot = ThemeManager.ConvertImageToByteArray(headerInfo.SpecialArrowUpHot);
				this.SpecialArrowDown = ThemeManager.ConvertImageToByteArray(headerInfo.SpecialArrowDown);
				this.SpecialArrowDownHot = ThemeManager.ConvertImageToByteArray(headerInfo.SpecialArrowDownHot);
				this.NormalArrowUp = ThemeManager.ConvertImageToByteArray(headerInfo.NormalArrowUp);
				this.NormalArrowUpHot = ThemeManager.ConvertImageToByteArray(headerInfo.NormalArrowUpHot);
				this.NormalArrowDown = ThemeManager.ConvertImageToByteArray(headerInfo.NormalArrowDown);
				this.NormalArrowDownHot = ThemeManager.ConvertImageToByteArray(headerInfo.NormalArrowDownHot);

				this.TitleGradient = headerInfo.TitleGradient;
				this.SpecialGradientStartColor = ThemeManager.ConvertColorToString(headerInfo.SpecialGradientStartColor);
				this.SpecialGradientEndColor = ThemeManager.ConvertColorToString(headerInfo.SpecialGradientEndColor);
				this.NormalGradientStartColor = ThemeManager.ConvertColorToString(headerInfo.NormalGradientStartColor);
				this.NormalGradientEndColor = ThemeManager.ConvertColorToString(headerInfo.NormalGradientEndColor);
				this.GradientOffset = headerInfo.GradientOffset;
			}


	
			public HeaderInfo Save()
			{
				HeaderInfo headerInfo = new HeaderInfo();

				if (this.FontName != null)
				{
					headerInfo.TitleFont = new Font(this.FontName, this.FontSize, this.FontStyle);
				}

				headerInfo.Margin = this.Margin;

				headerInfo.SpecialBackImage = ThemeManager.ConvertByteArrayToImage(this.SpecialBackImage);
				headerInfo.NormalBackImage = ThemeManager.ConvertByteArrayToImage(this.NormalBackImage);

				headerInfo.SpecialTitleColor = ThemeManager.ConvertStringToColor(this.SpecialTitle);
				headerInfo.NormalTitleColor = ThemeManager.ConvertStringToColor(this.NormalTitle);
				headerInfo.SpecialTitleHotColor = ThemeManager.ConvertStringToColor(this.SpecialTitleHot);
				headerInfo.NormalTitleHotColor = ThemeManager.ConvertStringToColor(this.NormalTitleHot);

				headerInfo.SpecialAlignment = this.SpecialAlignment;
				headerInfo.NormalAlignment = this.NormalAlignment;
				
				headerInfo.SpecialPadding = this.SpecialPadding;
				headerInfo.NormalPadding = this.NormalPadding;

				headerInfo.SpecialBorder = this.SpecialBorder;
				headerInfo.NormalBorder = this.NormalBorder;
				headerInfo.SpecialBorderColor = ThemeManager.ConvertStringToColor(this.SpecialBorderColor);
				headerInfo.NormalBorderColor = ThemeManager.ConvertStringToColor(this.NormalBorderColor);

				headerInfo.SpecialBackColor = ThemeManager.ConvertStringToColor(this.SpecialBackColor);
				headerInfo.NormalBackColor = ThemeManager.ConvertStringToColor(this.NormalBackColor);

				headerInfo.SpecialArrowUp = ThemeManager.ConvertByteArrayToImage(this.SpecialArrowUp);
				headerInfo.SpecialArrowUpHot = ThemeManager.ConvertByteArrayToImage(this.SpecialArrowUpHot);
				headerInfo.SpecialArrowDown = ThemeManager.ConvertByteArrayToImage(this.SpecialArrowDown);
				headerInfo.SpecialArrowDownHot = ThemeManager.ConvertByteArrayToImage(this.SpecialArrowDownHot);
				headerInfo.NormalArrowUp = ThemeManager.ConvertByteArrayToImage(this.NormalArrowUp);
				headerInfo.NormalArrowUpHot = ThemeManager.ConvertByteArrayToImage(this.NormalArrowUpHot);
				headerInfo.NormalArrowDown = ThemeManager.ConvertByteArrayToImage(this.NormalArrowDown);
				headerInfo.NormalArrowDownHot = ThemeManager.ConvertByteArrayToImage(this.NormalArrowDownHot);

				headerInfo.TitleGradient = this.TitleGradient;
				headerInfo.SpecialGradientStartColor = ThemeManager.ConvertStringToColor(this.SpecialGradientStartColor);
				headerInfo.SpecialGradientEndColor = ThemeManager.ConvertStringToColor(this.SpecialGradientEndColor);
				headerInfo.NormalGradientStartColor = ThemeManager.ConvertStringToColor(this.NormalGradientStartColor);
				headerInfo.NormalGradientEndColor = ThemeManager.ConvertStringToColor(this.NormalGradientEndColor);
				headerInfo.GradientOffset = this.GradientOffset;
				
				return headerInfo;
			}


			[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter=true)]
			public void GetObjectData(SerializationInfo info, StreamingContext context)
			{
				info.AddValue("Version", this.Version);

				info.AddValue("FontName", this.FontName);
				info.AddValue("FontSize", this.FontSize);
				info.AddValue("FontStyle", this.FontStyle);

				info.AddValue("Margin", this.Margin);

				info.AddValue("SpecialBackImage", this.SpecialBackImage);
				info.AddValue("NormalBackImage", this.NormalBackImage);

				info.AddValue("SpecialTitle", this.SpecialTitle);
				info.AddValue("NormalTitle", this.NormalTitle);
				info.AddValue("SpecialTitleHot", this.SpecialTitleHot);
				info.AddValue("NormalTitleHot", this.NormalTitleHot);

				info.AddValue("SpecialAlignment", this.SpecialAlignment);
				info.AddValue("NormalAlignment", this.NormalAlignment);

				info.AddValue("SpecialPadding", this.SpecialPadding);
				info.AddValue("NormalPadding", this.NormalPadding);

				info.AddValue("SpecialBorder", this.SpecialBorder);
				info.AddValue("NormalBorder", this.NormalBorder);
				info.AddValue("SpecialBorderColor", this.SpecialBorderColor);
				info.AddValue("NormalBorderColor", this.NormalBorderColor);

				info.AddValue("SpecialBackColor", this.SpecialBackColor);
				info.AddValue("NormalBackColor", this.NormalBackColor);

				info.AddValue("SpecialArrowUp", this.SpecialArrowUp);
				info.AddValue("SpecialArrowUpHot", this.SpecialArrowUpHot);
				info.AddValue("SpecialArrowDown", this.SpecialArrowDown);
				info.AddValue("SpecialArrowDownHot", this.SpecialArrowDownHot);
				info.AddValue("NormalArrowUp", this.NormalArrowUp);
				info.AddValue("NormalArrowUpHot", this.NormalArrowUpHot);
				info.AddValue("NormalArrowDown", this.NormalArrowDown);
				info.AddValue("NormalArrowDownHot", this.NormalArrowDownHot);

				info.AddValue("TitleGradient", this.TitleGradient);
				info.AddValue("SpecialGradientStartColor", this.SpecialGradientStartColor);
				info.AddValue("SpecialGradientEndColor", this.SpecialGradientEndColor);
				info.AddValue("NormalGradientStartColor", this.NormalGradientStartColor);
				info.AddValue("NormalGradientEndColor", this.NormalGradientEndColor);
				info.AddValue("GradientOffset", this.GradientOffset);
			}


		
			[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter=true)]
			protected HeaderInfoSurrogate(SerializationInfo info, StreamingContext context) : base()
			{
				int version = info.GetInt32("Version");
				
				this.FontName = info.GetString("FontName");
				this.FontSize = info.GetSingle("FontSize");
				this.FontStyle = (FontStyle) info.GetValue("FontStyle", typeof(FontStyle));

				this.Margin = info.GetInt32("Margin");
				
				this.SpecialBackImage = (byte[]) info.GetValue("SpecialBackImage", typeof(byte[]));
				this.NormalBackImage = (byte[]) info.GetValue("NormalBackImage", typeof(byte[]));
				
				this.SpecialTitle = info.GetString("SpecialTitle");
				this.NormalTitle = info.GetString("NormalTitle");
				this.SpecialTitleHot = info.GetString("SpecialTitleHot");
				this.NormalTitleHot = info.GetString("NormalTitleHot");
				
				this.SpecialAlignment = (ContentAlignment) info.GetValue("SpecialAlignment", typeof(ContentAlignment));
				this.NormalAlignment = (ContentAlignment) info.GetValue("NormalAlignment", typeof(ContentAlignment));

				this.SpecialPadding = (Padding) info.GetValue("SpecialPadding", typeof(Padding));
				this.NormalPadding = (Padding) info.GetValue("NormalPadding", typeof(Padding));
				
				this.SpecialBorder = (Border) info.GetValue("SpecialBorder", typeof(Border));
				this.NormalBorder = (Border) info.GetValue("NormalBorder", typeof(Border));
				this.SpecialBorderColor = info.GetString("SpecialBorderColor");
				this.NormalBorderColor = info.GetString("NormalBorderColor");
				
				this.SpecialBackColor = info.GetString("SpecialBackColor");
				this.NormalBackColor = info.GetString("NormalBackColor");
				
				this.SpecialArrowUp = (byte[]) info.GetValue("SpecialArrowUp", typeof(byte[]));
				this.SpecialArrowUpHot = (byte[]) info.GetValue("SpecialArrowUpHot", typeof(byte[]));
				this.SpecialArrowDown = (byte[]) info.GetValue("SpecialArrowDown", typeof(byte[]));
				this.SpecialArrowDownHot = (byte[]) info.GetValue("SpecialArrowDownHot", typeof(byte[]));
				this.NormalArrowUp = (byte[]) info.GetValue("NormalArrowUp", typeof(byte[]));
				this.NormalArrowUpHot = (byte[]) info.GetValue("NormalArrowUpHot", typeof(byte[]));
				this.NormalArrowDown = (byte[]) info.GetValue("NormalArrowDown", typeof(byte[]));
				this.NormalArrowDownHot = (byte[]) info.GetValue("NormalArrowDownHot", typeof(byte[]));
				
				this.TitleGradient = info.GetBoolean("TitleGradient");
				this.SpecialGradientStartColor = info.GetString("SpecialGradientStartColor");
				this.SpecialGradientEndColor = info.GetString("SpecialGradientEndColor");
				this.NormalGradientStartColor = info.GetString("NormalGradientStartColor");
				this.NormalGradientEndColor = info.GetString("NormalGradientEndColor");
				this.GradientOffset = info.GetSingle("GradientOffset");
			}

			
		}

	
	}




	internal class HeaderInfoConverter : ExpandableObjectConverter
	{
		
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string) && value is HeaderInfo)
			{
				return "";
			}

			return base.ConvertTo(context, culture, value, destinationType);
		}


		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
		
			
			PropertyDescriptorCollection collection = TypeDescriptor.GetProperties(typeof(HeaderInfo), attributes);

			string[] s = new string[33];
			s[0] = "TitleFont";
			s[1] = "TitleGradient";
			s[2] = "NormalGradientStartColor";
			s[3] = "NormalGradientEndColor";
			s[4] = "SpecialGradientStartColor";
			s[5] = "SpecialGradientEndColor";
			s[6] = "GradientOffset";
			s[7] = "TitleRadius";
			s[8] = "NormalBackImage";
			s[9] = "SpecialBackImage";
			s[10] = "NormalArrowUp";
			s[11] = "NormalArrowUpHot";
			s[12] = "NormalArrowDown";
			s[13] = "NormalArrowDownHot";
			s[14] = "SpecialArrowUp";
			s[15] = "SpecialArrowUpHot";
			s[16] = "SpecialArrowDown";
			s[17] = "SpecialArrowDownHot";
			s[18] = "NormalAlignment";
			s[19] = "SpecialAlignment";
			s[20] = "NormalBackColor";
			s[21] = "SpecialBackColor";
			s[22] = "NormalBorder";
			s[23] = "SpecialBorder";
			s[24] = "NormalBorderColor";
			s[25] = "SpecialBorderColor";
			s[26] = "NormalPadding";
			s[27] = "SpecialPadding";
			s[28] = "NormalTitleColor";
			s[29] = "NormalTitleHotColor";
			s[30] = "SpecialTitleColor";
			s[31] = "SpecialTitleHotColor";
			s[32] = "Margin";

			return collection.Sort(s);
		}
	}



	[Serializable(),  
	TypeConverter(typeof(BorderConverter))]
	public class Border
	{
	
		[NonSerialized()]
		public static readonly Border Empty = new Border(0, 0, 0, 0);
		
		private int left;
		
		
		private int right;
		

		private int top;
	
		private int bottom;

		public Border() : this(0, 0, 0, 0)
		{

		}


		public Border(int left, int top, int right, int bottom)
		{
			this.left = left;
			this.right = right;
			this.top = top;
			this.bottom = bottom;
		}

	
		public override bool Equals(object obj)
		{
			if (!(obj is Border))
			{
				return false;
			}

			Border border = (Border) obj;

			if (((border.Left == this.Left) && (border.Top == this.Top)) && (border.Right == this.Right))
			{
				return (border.Bottom == this.Bottom);
			}

			return false;
		}


		public override int GetHashCode()
		{
			return (((this.Left ^ ((this.Top << 13) | (this.Top >> 0x13))) ^ ((this.Right << 0x1a) | (this.Right >> 6))) ^ ((this.Bottom << 7) | (this.Bottom >> 0x19)));
		}

		public int Left
		{
			get
			{
				return this.left;
			}

			set
			{
				if (value < 0)
				{
					value = 0;
				}

				this.left = value;
			}
		}


		public int Right
		{
			get
			{
				return this.right;
			}

			set
			{
				if (value < 0)
				{
					value = 0;
				}

				this.right = value;
			}
		}


		public int Top
		{
			get
			{
				return this.top;
			}

			set
			{
				if (value < 0)
				{
					value = 0;
				}

				this.top = value;
			}
		}

		public int Bottom
		{
			get
			{
				return this.bottom;
			}

			set
			{
				if (value < 0)
				{
					value = 0;
				}

				this.bottom = value;
			}
		}


	
		[Browsable(false)]
		public bool IsEmpty
		{
			get
			{
				if (((this.Left == 0) && (this.Top == 0)) && (this.Right == 0))
				{
					return (this.Bottom == 0);
				}

				return false;
			}
		}

	
		public static bool operator ==(Border left, Border right)
		{
			if (((left.Left == right.Left) && (left.Top == right.Top)) && (left.Right == right.Right))
			{
				return (left.Bottom == right.Bottom);
			}

			return false;
		}


		public static bool operator !=(Border left, Border right)
		{
			return !(left == right);
		}

		
	}



	internal class BorderConverter : TypeConverter
	{
		
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if (sourceType == typeof(string))
			{
				return true;
			}

			return base.CanConvertFrom(context, sourceType);
		}


		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == typeof(InstanceDescriptor))
			{
				return true;
			}
			
			return base.CanConvertTo(context, destinationType);
		}


		
		public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
		{
			if (value is string)
			{
				string text = ((string) value).Trim();

				if (text.Length == 0)
				{
					return null;
				}

				if (culture == null)
				{
					culture = CultureInfo.CurrentCulture;
				}

				char[] listSeparators = culture.TextInfo.ListSeparator.ToCharArray();

				string[] s = text.Split(listSeparators);

				if (s.Length < 4)
				{
					return null;
				}

				return new Border(int.Parse(s[0]), int.Parse(s[1]), int.Parse(s[2]), int.Parse(s[3]));
			}	
			
			return base.ConvertFrom(context, culture, value);
		}



		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}

			if ((destinationType == typeof(string)) && (value is Border))
			{
				Border b = (Border) value;

				if (culture == null)
				{
					culture = CultureInfo.CurrentCulture;
				}

				string separator = culture.TextInfo.ListSeparator + " ";

				TypeConverter converter = TypeDescriptor.GetConverter(typeof(int));

				string[] s = new string[4];

				s[0] = converter.ConvertToString(context, culture, b.Left);
				s[1] = converter.ConvertToString(context, culture, b.Top);
				s[2] = converter.ConvertToString(context, culture, b.Right);
				s[3] = converter.ConvertToString(context, culture, b.Bottom);

				return string.Join(separator, s);
			}

			if ((destinationType == typeof(InstanceDescriptor)) && (value is Border))
			{
				Border b = (Border) value;

				Type[] t = new Type[4];
				t[0] = t[1] = t[2] = t[3] = typeof(int);

				ConstructorInfo info = typeof(Border).GetConstructor(t);

				if (info != null)
				{
					object[] o = new object[4];

					o[0] = b.Left;
					o[1] = b.Top;
					o[2] = b.Right;
					o[3] = b.Bottom;

					return new InstanceDescriptor(info, o);
				}
			}
			
			return base.ConvertTo(context, culture, value, destinationType);
		}


		public override object CreateInstance(ITypeDescriptorContext context, System.Collections.IDictionary propertyValues)
		{
			return new Border((int) propertyValues["Left"], 
				(int) propertyValues["Top"], 
				(int) propertyValues["Right"], 
				(int) propertyValues["Bottom"]);
		}


	
		public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
		{
			return true;
		}


	
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			PropertyDescriptorCollection collection = TypeDescriptor.GetProperties(typeof(Border), attributes);

			string[] s = new string[4];
			s[0] = "Left";
			s[1] = "Top";
			s[2] = "Right";
			s[3] = "Bottom";

			return collection.Sort(s);
		}


		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}

	
	[Serializable, 
	TypeConverter(typeof(PaddingConverter))]
	public class Padding
	{
		
		
		
		[NonSerialized()]
		public static readonly Padding Empty = new Padding(0, 0, 0, 0);
		
		private int left;
		
		
		private int right;
		
	
		private int top;
	
		private int bottom;

		public Padding() : this(0, 0, 0, 0)
		{

		}


		public Padding(int left, int top, int right, int bottom)
		{
			this.left = left;
			this.right = right;
			this.top = top;
			this.bottom = bottom;
		}

		
		public override bool Equals(object obj)
		{
			if (!(obj is Padding))
			{
				return false;
			}

			Padding padding = (Padding) obj;

			if (((padding.Left == this.Left) && (padding.Top == this.Top)) && (padding.Right == this.Right))
			{
				return (padding.Bottom == this.Bottom);
			}

			return false;
		}


	
		public override int GetHashCode()
		{
			return (((this.Left ^ ((this.Top << 13) | (this.Top >> 0x13))) ^ ((this.Right << 0x1a) | (this.Right >> 6))) ^ ((this.Bottom << 7) | (this.Bottom >> 0x19)));
		}

		public int Left
		{
			get
			{
				return this.left;
			}

			set
			{
				if (value < 0)
				{
					value = 0;
				}

				this.left = value;
			}
		}


		public int Right
		{
			get
			{
				return this.right;
			}

			set
			{
				if (value < 0)
				{
					value = 0;
				}

				this.right = value;
			}
		}


		public int Top
		{
			get
			{
				return this.top;
			}

			set
			{
				if (value < 0)
				{
					value = 0;
				}

				this.top = value;
			}
		}

		public int Bottom
		{
			get
			{
				return this.bottom;
			}

			set
			{
				if (value < 0)
				{
					value = 0;
				}

				this.bottom = value;
			}
		}


		[Browsable(false)]
		public bool IsEmpty
		{
			get
			{
				if (((this.Left == 0) && (this.Top == 0)) && (this.Right == 0))
				{
					return (this.Bottom == 0);
				}

				return false;
			}
		}


		public static bool operator ==(Padding left, Padding right)
		{
			if (((left.Left == right.Left) && (left.Top == right.Top)) && (left.Right == right.Right))
			{
				return (left.Bottom == right.Bottom);
			}

			return false;
		}


		public static bool operator !=(Padding left, Padding right)
		{
			return !(left == right);
		}

		
	}



	internal class PaddingConverter : TypeConverter
	{
		
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if (sourceType == typeof(string))
			{
				return true;
			}

			return base.CanConvertFrom(context, sourceType);
		}


		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == typeof(InstanceDescriptor))
			{
				return true;
			}
			
			return base.CanConvertTo(context, destinationType);
		}


		
		public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
		{
			if (value is string)
			{
				string text = ((string) value).Trim();

				if (text.Length == 0)
				{
					return null;
				}

				if (culture == null)
				{
					culture = CultureInfo.CurrentCulture;
				}

				char[] listSeparators = culture.TextInfo.ListSeparator.ToCharArray();

				string[] s = text.Split(listSeparators);

				if (s.Length < 4)
				{
					return null;
				}

				return new Padding(int.Parse(s[0]), int.Parse(s[1]), int.Parse(s[2]), int.Parse(s[3]));
			}	
			
			return base.ConvertFrom(context, culture, value);
		}


		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}

			if ((destinationType == typeof(string)) && (value is Padding))
			{
				Padding p = (Padding) value;

				if (culture == null)
				{
					culture = CultureInfo.CurrentCulture;
				}

				string separator = culture.TextInfo.ListSeparator + " ";

				TypeConverter converter = TypeDescriptor.GetConverter(typeof(int));

				string[] s = new string[4];

				s[0] = converter.ConvertToString(context, culture, p.Left);
				s[1] = converter.ConvertToString(context, culture, p.Top);
				s[2] = converter.ConvertToString(context, culture, p.Right);
				s[3] = converter.ConvertToString(context, culture, p.Bottom);

				return string.Join(separator, s);
			}

			if ((destinationType == typeof(InstanceDescriptor)) && (value is Padding))
			{
				Padding p = (Padding) value;

				Type[] t = new Type[4];
				t[0] = t[1] = t[2] = t[3] = typeof(int);

				ConstructorInfo info = typeof(Padding).GetConstructor(t);

				if (info != null)
				{
					object[] o = new object[4];

					o[0] = p.Left;
					o[1] = p.Top;
					o[2] = p.Right;
					o[3] = p.Bottom;

					return new InstanceDescriptor(info, o);
				}
			}
			
			return base.ConvertTo(context, culture, value, destinationType);
		}


		public override object CreateInstance(ITypeDescriptorContext context, System.Collections.IDictionary propertyValues)
		{
			return new Padding((int) propertyValues["Left"], 
				(int) propertyValues["Top"], 
				(int) propertyValues["Right"], 
				(int) propertyValues["Bottom"]);
		}


		
		public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
		{
			return true;
		}


		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			PropertyDescriptorCollection collection = TypeDescriptor.GetProperties(typeof(Padding), attributes);

			string[] s = new string[4];
			s[0] = "Left";
			s[1] = "Top";
			s[2] = "Right";
			s[3] = "Bottom";

			return collection.Sort(s);
		}


		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}

	
	[Serializable,  
	TypeConverter(typeof(MarginConverter))]
	public class Margin
	{
		[NonSerialized()]
		public static readonly Margin Empty = new Margin(0, 0, 0, 0);
		
		private int left;
		
		
		private int right;
		
		
		private int top;
		
	
		private int bottom;

		public Margin() : this(0, 0, 0, 0)
		{

		}


		
		public Margin(int left, int top, int right, int bottom)
		{
			this.left = left;
			this.right = right;
			this.top = top;
			this.bottom = bottom;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is Margin))
			{
				return false;
			}

			Margin margin = (Margin) obj;

			if (((margin.Left == this.Left) && (margin.Top == this.Top)) && (margin.Right == this.Right))
			{
				return (margin.Bottom == this.Bottom);
			}

			return false;
		}


		public override int GetHashCode()
		{
			return (((this.Left ^ ((this.Top << 13) | (this.Top >> 0x13))) ^ ((this.Right << 0x1a) | (this.Right >> 6))) ^ ((this.Bottom << 7) | (this.Bottom >> 0x19)));
		}

		public int Left
		{
			get
			{
				return this.left;
			}

			set
			{
				if (value < 0)
				{
					value = 0;
				}

				this.left = value;
			}
		}


		public int Right
		{
			get
			{
				return this.right;
			}

			set
			{
				if (value < 0)
				{
					value = 0;
				}

				this.right = value;
			}
		}


		
		public int Top
		{
			get
			{
				return this.top;
			}

			set
			{
				if (value < 0)
				{
					value = 0;
				}

				this.top = value;
			}
		}


		public int Bottom
		{
			get
			{
				return this.bottom;
			}

			set
			{
				if (value < 0)
				{
					value = 0;
				}

				this.bottom = value;
			}
		}


		[Browsable(false)]
		public bool IsEmpty
		{
			get
			{
				if (((this.Left == 0) && (this.Top == 0)) && (this.Right == 0))
				{
					return (this.Bottom == 0);
				}

				return false;
			}
		}

	
		public static bool operator ==(Margin left, Margin right)
		{
			if (((left.Left == right.Left) && (left.Top == right.Top)) && (left.Right == right.Right))
			{
				return (left.Bottom == right.Bottom);
			}

			return false;
		}

		public static bool operator !=(Margin left, Margin right)
		{
			return !(left == right);
		}

	}


	internal class MarginConverter : TypeConverter
	{
		
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if (sourceType == typeof(string))
			{
				return true;
			}

			return base.CanConvertFrom(context, sourceType);
		}

		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == typeof(InstanceDescriptor))
			{
				return true;
			}
			
			return base.CanConvertTo(context, destinationType);
		}


		public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
		{
			if (value is string)
			{
				string text = ((string) value).Trim();

				if (text.Length == 0)
				{
					return null;
				}

				if (culture == null)
				{
					culture = CultureInfo.CurrentCulture;
				}

				char[] listSeparators = culture.TextInfo.ListSeparator.ToCharArray();

				string[] s = text.Split(listSeparators);

				if (s.Length < 4)
				{
					return null;
				}

				return new Margin(int.Parse(s[0]), int.Parse(s[1]), int.Parse(s[2]), int.Parse(s[3]));
			}	
			
			return base.ConvertFrom(context, culture, value);
		}


		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}

			if ((destinationType == typeof(string)) && (value is Margin))
			{
				Margin m = (Margin) value;

				if (culture == null)
				{
					culture = CultureInfo.CurrentCulture;
				}

				string separator = culture.TextInfo.ListSeparator + " ";

				TypeConverter converter = TypeDescriptor.GetConverter(typeof(int));

				string[] s = new string[4];

				s[0] = converter.ConvertToString(context, culture, m.Left);
				s[1] = converter.ConvertToString(context, culture, m.Top);
				s[2] = converter.ConvertToString(context, culture, m.Right);
				s[3] = converter.ConvertToString(context, culture, m.Bottom);

				return string.Join(separator, s);
			}

			if ((destinationType == typeof(InstanceDescriptor)) && (value is Margin))
			{
				Margin m = (Margin) value;

				Type[] t = new Type[4];
				t[0] = t[1] = t[2] = t[3] = typeof(int);

				ConstructorInfo info = typeof(Margin).GetConstructor(t);

				if (info != null)
				{
					object[] o = new object[4];

					o[0] = m.Left;
					o[1] = m.Top;
					o[2] = m.Right;
					o[3] = m.Bottom;

					return new InstanceDescriptor(info, o);
				}
			}
			
			return base.ConvertTo(context, culture, value, destinationType);
		}


		public override object CreateInstance(ITypeDescriptorContext context, System.Collections.IDictionary propertyValues)
		{
			return new Margin((int) propertyValues["Left"], 
				(int) propertyValues["Top"], 
				(int) propertyValues["Right"], 
				(int) propertyValues["Bottom"]);
		}


		public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
		{
			return true;
		}


		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			PropertyDescriptorCollection collection = TypeDescriptor.GetProperties(typeof(Margin), attributes);

			string[] s = new string[4];
			s[0] = "Left";
			s[1] = "Top";
			s[2] = "Right";
			s[3] = "Bottom";

			return collection.Sort(s);
		}


		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}

	public enum ImageStretchMode
	{
	
		Normal = 0,

		Transparent = 2,
		
	
		Tile = 3,
	
		Horizontal = 5,
		
	
		Stretch = 6,
		
		
		ARGBImage = 7
	}

	
}
