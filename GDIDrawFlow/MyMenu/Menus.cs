using System;
using System.ComponentModel;
using System.Collections;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;


namespace  System.MyControl.MyMenu
{

	[ToolboxBitmap (typeof(Menus), "OfficeMenus.bmp")]
	public class Menus : System.ComponentModel.Component
	{
		private System.ComponentModel.Container components = null;

		static ImageList _imageList;

		static NameValueCollection picDetails = new NameValueCollection();

		public Menus(System.ComponentModel.IContainer container)
		{
			container.Add(this);
			InitializeComponent();
		}

		public Menus()
		{
			InitializeComponent();
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
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}


		/// <summary>
		/// 开始线程
		/// </summary>
		/// <param name="form"></param>
		public void Start(Control control)
		{
			try 
			{
				ContextMenu cmenu = control.ContextMenu;
				if ( cmenu != null ) {
					InitMenuItem(cmenu);
				}

				foreach ( Control c in control.Controls ) 
				{
					if ( c.ContextMenu != null )
						InitMenuItem(c.ContextMenu);
				}

			}
			catch {}
		}

		/// <summary>
		/// 结束线程
		/// </summary>
		/// <param name="form"></param>
		public void End(Form form)
		{
			try 
			{
				System.Windows.Forms.MainMenu menu = form.Menu;

				foreach ( MenuItem mi in menu.MenuItems )
				{
					mi.MeasureItem -= new System.Windows.Forms.MeasureItemEventHandler(mainMenuItem_MeasureItem);
					mi.DrawItem -= new System.Windows.Forms.DrawItemEventHandler(mainMenuItem_DrawItem);
					mi.OwnerDraw = false;
					
					UninitMenuItem(mi);
				}

				ContextMenu cmenu = form.ContextMenu;
				if ( cmenu != null ) 
				{
					UninitMenuItem(cmenu);
				}

				foreach ( Control c in form.Controls ) 
				{
					if ( c.ContextMenu != null )
						UninitMenuItem(c.ContextMenu);
				}

			}
			catch {}
		}

		private void InitMenuItem(Menu mi)
		{
			foreach ( MenuItem m in mi.MenuItems )
			{
				m.MeasureItem += 
					new System.Windows.Forms.MeasureItemEventHandler(this.menuItem_MeasureItem);
				m.DrawItem += 
					new System.Windows.Forms.DrawItemEventHandler(this.menuItem_DrawItem);
				m.OwnerDraw = true;

				InitMenuItem(m);
			}
		}


		private void UninitMenuItem(Menu mi)
		{
			foreach ( MenuItem m in mi.MenuItems )
			{
				m.MeasureItem -= 
					new System.Windows.Forms.MeasureItemEventHandler(this.menuItem_MeasureItem);
				m.DrawItem -= 
					new System.Windows.Forms.DrawItemEventHandler(this.menuItem_DrawItem);
				m.OwnerDraw = false;

				UninitMenuItem(m);
			}
		}


		private void menuItem_MeasureItem(object sender, System.Windows.Forms.MeasureItemEventArgs e)
		{
			MenuItem mi = (MenuItem) sender;
			if ( mi.Text == "-" ) {
				e.ItemHeight = 7;
			} else {
				SizeF miSize = e.Graphics.MeasureString(mi.Text, Globals.menuFont);
				int scWidth = 0;
				if ( mi.Shortcut != Shortcut.None ) {
					SizeF scSize = e.Graphics.MeasureString(mi.Shortcut.ToString(), Globals.menuFont);
					scWidth = Convert.ToInt32(scSize.Width);
				}
				int miHeight = Convert.ToInt32(miSize.Height) + 7;
				if (miHeight < 25) miHeight = Globals.MIN_MENU_HEIGHT;
				e.ItemHeight = miHeight;
				e.ItemWidth = Convert.ToInt32(miSize.Width) + scWidth + (Globals.PIC_AREA_SIZE * 2);
			}
		}

		private void menuItem_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
		{
			MenuItemDrawing.DrawMenuItem(e, (MenuItem) sender);
		}

		private void mainMenuItem_MeasureItem(object sender, System.Windows.Forms.MeasureItemEventArgs e)
		{
			MenuItem mi = (MenuItem) sender;
			SizeF miSize = e.Graphics.MeasureString(mi.Text, Globals.menuFont);
			e.ItemWidth = Convert.ToInt32(miSize.Width);
		}

		private void mainMenuItem_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
		{
			MainMenuItemDrawing.DrawMenuItem(e, (MenuItem) sender);
		}

		public void AddPicture(MenuItem mi, int index)
		{
			picDetails.Add(mi.Handle.ToString(), index.ToString());
		}

		public static Bitmap GetItemPicture(MenuItem mi)
		{
			if ( _imageList == null )
				return null;

			string [] picIndex = picDetails.GetValues(mi.Handle.ToString());
			
			if ( picIndex == null )
				return null;
			else
				return (Bitmap)_imageList.Images[Convert.ToInt32(picIndex[0])];
		}

		public ImageList ImageList
		{
			get { return _imageList; }
			set { _imageList = value; }
		}
	}
}
