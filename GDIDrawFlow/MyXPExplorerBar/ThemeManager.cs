
using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Resources;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;


namespace System.MyControl.MyXPExplorerBar
{
	#region ThemeManager Class

	/// <summary>
	/// A class that extracts theme settings from Windows XP shellstyle dlls
	/// </summary>
	public class ThemeManager
	{
		/// <summary>
		/// pointer to a shellstyle dll
		/// </summary>
		private static IntPtr hModule = IntPtr.Zero;

		/// <summary>
		/// cached version of the current shellstyle in use
		/// </summary>
		private static ExplorerBarInfo currentShellStyle = null;



		/// <summary>
		/// Gets the System defined settings for the ExplorerBar according
		/// to the current System theme
		/// </summary>
		/// <returns>An ExplorerBarInfo object that contains the System defined 
		/// settings for the ExplorerBar according to the current System theme</returns>
		public static ExplorerBarInfo GetSystemExplorerBarSettings()
		{
			return GetSystemExplorerBarSettings(false);
		}


		/// <summary>
		/// Gets the System defined settings for the ExplorerBar according
		/// to the current System theme
		/// </summary>
		/// <param name="useClassicTheme">Specifies whether the current system theme 
		/// should be ignored and return unthemed settings</param>
		/// <returns>An ExplorerBarInfo object that contains the System defined 
		/// settings for the ExplorerBar according to the current System theme</returns>
		public static ExplorerBarInfo GetSystemExplorerBarSettings(bool useClassicTheme)
		{
			// check if we can return the cached theme
			// note: caching a classic theme seems to cause a few
			//       problems i haven't been able to resolve, so 
			//       for the moment always return a new 
			//       ExplorerBarInfo if useClassicTheme is true
			if (currentShellStyle != null && !useClassicTheme)
			{
				if (currentShellStyle.ShellStylePath != null && currentShellStyle.ShellStylePath.Equals(GetShellStylePath()))
				{
					return currentShellStyle;
				}
			}
            
			ExplorerBarInfo systemTheme;

			// check if we are using themes.  if so, load up the
			// appropriate shellstyle.dll
			if (!useClassicTheme && UxTheme.AppThemed && LoadShellStyleDll())
			{
				try
				{
					// get the uifile contained in the shellstyle.dll
					// and get ready to parse it
					Parser parser = new Parser(GetResourceUIFile());

					// let the parser do its stuff
					systemTheme = parser.Parse();
					systemTheme.SetOfficialTheme(true);
					systemTheme.ShellStylePath = GetShellStylePath();
				}
				catch
				{
					// something went wrong, so use default settings
					systemTheme = new ExplorerBarInfo();
					systemTheme.UseClassicTheme();
					systemTheme.SetOfficialTheme(true);

					// add non-themed arrows as the ExplorerBar will
					// look funny without them.
					systemTheme.SetUnthemedArrowImages();
				}
				finally
				{
					// unload the shellstyle.dll
					FreeShellStyleDll();
				}
			}
			else
			{
				// no themes available, so use default settings
				systemTheme = new ExplorerBarInfo();
				systemTheme.UseClassicTheme();
				systemTheme.SetOfficialTheme(true);

				// add non-themed arrows as the ExplorerBar will
				// look funny without them.
				systemTheme.SetUnthemedArrowImages();
			}

			// cache the theme
			currentShellStyle = systemTheme;

			return systemTheme;
		}


		/// <summary>
		/// Gets the System defined settings for the ExplorerBar specified
		/// by the shellstyle.dll at the specified path
		/// </summary>
		/// <param name="stylePath">The path to the shellstyle.dll</param>
		/// <returns>An ExplorerBarInfo object that contains the settings for 
		/// the ExplorerBar specified by the shellstyle.dll at the specified path</returns>
		public static ExplorerBarInfo GetSystemExplorerBarSettings(string stylePath)
		{
			// check if we can return the cached theme
			if (currentShellStyle != null)
			{
				if (!currentShellStyle.ClassicTheme && currentShellStyle.ShellStylePath != null && currentShellStyle.ShellStylePath.Equals(stylePath))
				{
					return currentShellStyle;
				}
			}
            
			ExplorerBarInfo systemTheme;

			// attampt to load the specified shellstyle.dll
			if (LoadShellStyleDll(stylePath))
			{
				try
				{
					// get the uifile contained in the shellstyle.dll
					// and get ready to parse it
					Parser parser = new Parser(GetResourceUIFile());

					// let the parser do its stuff
					systemTheme = parser.Parse();
					systemTheme.SetOfficialTheme(false);
					systemTheme.ShellStylePath = stylePath;
				}
				catch
				{
					// something went wrong, so try to use current system theme
					systemTheme = GetSystemExplorerBarSettings();
				}
				finally
				{
					// unload the shellstyle.dll
					FreeShellStyleDll();
				}
			}
			else
			{
				systemTheme = new ExplorerBarInfo();
				systemTheme.UseClassicTheme();
				systemTheme.SetOfficialTheme(true);

				systemTheme.SetUnthemedArrowImages();
			}

			currentShellStyle = systemTheme;

			return systemTheme;
		}


		#region ShellStyle Dll

		/// <summary>
		/// Loads the ShellStyle.dll into memory as determined by the current
		/// system theme
		/// </summary>
		/// <returns>If the function succeeds, the return value is true. If the 
		/// function fails, the return value is false</returns>
		private static bool LoadShellStyleDll()
		{
			// work out the path to the shellstyle.dll according
			// to the current theme
			string stylePath = GetShellStylePath();

			// if for some reason it doesn't exist, return false 
			// so we can use our classic theme instead
			if (!File.Exists(stylePath))
			{
				return false;
			}

			// make sure Windows won't throw up any error boxes if for
			// some reason it can't find the dll
			int lastErrorMode = NativeMethods.SetErrorMode(SetErrorModeFlags.SEM_FAILCRITICALERRORS | SetErrorModeFlags.SEM_NOOPENFILEERRORBOX);

			// attempt to load the shellstyle dll

			// fix: use LoadLibraryEx to load shellstyle.dll to improve
			//      compatibility with non-official themes
			//      use SetErrorMode to supress error messages
			//      scorteel (scorteel@ask.be)
			//      17/08/2004
			//      v1.21
			// fix: Win9x craps itself on the NativeMethods.LoadLibraryEx 
			//      and doesn't return a valid hModule pointer, so we'll 
			//      use LoadLibrary instead and hope if doesn't cause
			//      any problems
			//      18/01/2005
			//      v3.2
			if (Environment.OSVersion.Platform == PlatformID.Win32Windows)
			{
				hModule = NativeMethods.LoadLibrary(stylePath);
			}
			else
			{
				hModule = NativeMethods.LoadLibraryEx(stylePath, LoadLibraryExFlags.LOAD_LIBRARY_AS_DATAFILE);
			}

			// set the error mode back to its original value
			NativeMethods.SetErrorMode((SetErrorModeFlags)lastErrorMode);

			// return whether we succeeded
			return (hModule != IntPtr.Zero);
		}


		/// <summary>
		/// Loads the specified ShellStyle.dll into memory
		/// </summary>
		/// <returns>If the function succeeds, the return value is true. If the 
		/// function fails, the return value is false</returns>
		private static bool LoadShellStyleDll(string stylePath)
		{
			// if the file doesn't exist, return the current style
			if (!File.Exists(stylePath))
			{
				return LoadShellStyleDll();
			}

			// make sure Windows won't throw up any error boxes if for
			// some reason it can't find the dll
			int lastErrorMode = NativeMethods.SetErrorMode(SetErrorModeFlags.SEM_FAILCRITICALERRORS | SetErrorModeFlags.SEM_NOOPENFILEERRORBOX);

			// attempt to load the shellstyle dll

			// fix: use LoadLibraryEx to load shellstyle.dll to improve
			//      compatibility with non-official themes
			//      use SetErrorMode to supress error messages
			//      scorteel (scorteel@ask.be)
			//      17/08/2004
			//      v1.21
			// fix: Win9x craps itself on the NativeMethods.LoadLibraryEx 
			//      and doesn't return a valid hModule pointer, so we'll 
			//      use LoadLibrary instead and hope if doesn't cause
			//      any problems
			//      18/01/2005
			//      v3.2
			if (Environment.OSVersion.Platform == PlatformID.Win32Windows)
			{
				hModule = NativeMethods.LoadLibrary(stylePath);
			}
			else
			{
				hModule = NativeMethods.LoadLibraryEx(stylePath, LoadLibraryExFlags.LOAD_LIBRARY_AS_DATAFILE);
			}

			// set the error mode back to its original value
			NativeMethods.SetErrorMode((SetErrorModeFlags)lastErrorMode);

			// return whether we succeeded
			return (hModule != IntPtr.Zero);
		}


		/// <summary>
		/// Removes the ShellStyle.dll from memory.  Assumes that
		/// LoadShellStyleDll() was successful
		/// </summary>
		private static void FreeShellStyleDll()
		{
			// unload the dll
			NativeMethods.FreeLibrary(hModule);

			// reset the hModule pointer
			hModule = IntPtr.Zero;
		}


		/// <summary>
		/// Returns a string that specifies the path to the shellstyle.dll 
		/// accordingto the current theme
		/// </summary>
		/// <returns>a string that specifies the path to the shellstyle.dll 
		/// accordingto the current theme</returns>
		private static string GetShellStylePath()
		{
			// work out the path to the shellstyle.dll according
			// to the current theme
			// fix: considered a issue with handling custom themes 
			//      located in sub-directories or using a non-flat 
			//      directory structure
			//      torsten_rendelmann (torsten.rendelmann@gmx.net)
			//      13/09/2005
			//      v3.3
			string themeName = UxTheme.ThemeName;

			if (themeName.IndexOf('\\') >= 0)
			{
				themeName = themeName.Substring(0, themeName.LastIndexOf('\\'));
			}

			string styleName = themeName + "\\Shell\\" + UxTheme.ColorName;
			string stylePath = styleName + "\\shellstyle.dll";

			return stylePath;
		}

		#endregion


		#region Resources

		/// <summary>
		/// Extracts the UIFILE from the currently loaded ShellStyle.dll
		/// </summary>
		/// <returns>A string that contains the UIFILE</returns>
		internal static string GetResourceUIFile()
		{
			// locate the "UIFILE" resource
			IntPtr hResource = NativeMethods.FindResource(hModule, "#1", "UIFILE");

			// get its size
			int resourceSize = NativeMethods.SizeofResource(hModule, hResource);

			// load the resource
			IntPtr resourceData = NativeMethods.LoadResource(hModule, hResource);

			// copy the resource data into a byte array so we
			// still have a copy once the resource is freed
			// fix: use GCHandle.Alloc to pin uiBytes
			//      Paul Haley (phaley@mail.com)
			//      03/06/2004
			//      v1.1
			byte[] uiBytes = new byte[resourceSize];
			GCHandle gcHandle = GCHandle.Alloc(uiBytes, GCHandleType.Pinned);
			IntPtr firstCopyElement = Marshal.UnsafeAddrOfPinnedArrayElement(uiBytes, 0);
			NativeMethods.CopyMemory(firstCopyElement, resourceData, resourceSize);

			// free the resource
			gcHandle.Free();
			NativeMethods.FreeResource(resourceData);

			// convert the char array to an ansi string
			string s = Marshal.PtrToStringAnsi(firstCopyElement, resourceSize);

			return s;
		}


		/// <summary>
		/// Returns a Bitmap from the currently loaded ShellStyle.dll
		/// </summary>
		/// <param name="resourceName">The name of the Bitmap to load</param>
		/// <returns>The Bitmap specified by the resourceName</returns>
		internal static Bitmap GetResourceBMP(string resourceName)
		{
			// find the resource
			IntPtr hBitmap = NativeMethods.LoadBitmap(hModule, Int32.Parse(resourceName));

			// load the bitmap
			Bitmap bitmap = Bitmap.FromHbitmap(hBitmap);

			return bitmap;
		}


		/// <summary>
		/// Returns a Png Bitmap from the currently loaded ShellStyle.dll
		/// </summary>
		/// <param name="resourceName">The name of the Png to load</param>
		/// <returns>The Bitmap specified by the resourceName</returns>
		internal static Bitmap GetResourcePNG(string resourceName)
		{
			// the resource size includes some header information (for PNG's in shellstyle.dll this
			// appears to be the standard 40 bytes of BITMAPHEADERINFO).
			const int FILE_HEADER_BYTES = 40;

			// load the bitmap resource normally to get dimensions etc.
			Bitmap tmpNoAlpha = Bitmap.FromResource(hModule, "#" + resourceName);
			IntPtr hResource = NativeMethods.FindResource(hModule, "#" + resourceName, 2 /*RT_BITMAP*/ );
			int resourceSize = NativeMethods.SizeofResource(hModule, hResource);

			// initialise 32bit alpha bitmap (target)
			Bitmap bitmap = new Bitmap(tmpNoAlpha.Width, tmpNoAlpha.Height, PixelFormat.Format32bppArgb);

			// load the resource via kernel32.dll (preserves alpha)
			IntPtr hLoadedResource = NativeMethods.LoadResource(hModule, hResource);

			// copy bitmap data into byte array directly
			// still have a copy once the resource is freed
			// fix: use GCHandle.Alloc to pin uiBytes
			//      Paul Haley (phaley@mail.com)
			//      03/06/2004
			//      v1.1
			byte[] bitmapBytes = new byte[resourceSize];
			GCHandle gcHandle = GCHandle.Alloc(bitmapBytes, GCHandleType.Pinned);
			IntPtr firstCopyElement = Marshal.UnsafeAddrOfPinnedArrayElement(bitmapBytes, 0);
			// nb. we only copy the actual PNG data (no header)
			NativeMethods.CopyMemory(firstCopyElement, hLoadedResource, resourceSize);
			NativeMethods.FreeResource(hLoadedResource);

			// copy the byte array contents back to a handle to the alpha bitmap (use lockbits)
			Rectangle copyArea = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
			BitmapData alphaBits = bitmap.LockBits(copyArea, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

			// copymemory to bitmap data (Scan0)
			firstCopyElement = Marshal.UnsafeAddrOfPinnedArrayElement(bitmapBytes, FILE_HEADER_BYTES);
			NativeMethods.CopyMemory(alphaBits.Scan0, firstCopyElement, resourceSize - FILE_HEADER_BYTES);
			gcHandle.Free();

			// complete operation
			bitmap.UnlockBits(alphaBits);
			NativeMethods.GdiFlush();

			// flip bits (not sure why this is needed at the moment..)
			bitmap.RotateFlip(RotateFlipType.Rotate180FlipX);

			return bitmap;
		}


		/// <summary>
		/// Returns a string from the currently loaded ShellStyle.dll
		/// </summary>
		/// <param name="id">The integer identifier of the string to be loaded</param>
		internal static string GetResourceString(int id)
		{
			// return null if shellstyle.dll isn't loaded
			if (hModule == IntPtr.Zero)
			{
				return null;
			}

			// get the string
			StringBuilder buffer = new StringBuilder(1024);
			NativeMethods.LoadString(hModule, id, buffer, 1024);

			return buffer.ToString();
		}


		/// <summary>
		/// Converts an Icon to a Bitmap
		/// </summary>
		/// <param name="icon">The Icon to be converted</param>
		/// <returns>A Bitmap that contains the converted Icon</returns>
		public static Bitmap IconToBitmap(Icon icon)
		{
			// try to convert to a 32bpp bitmap
			Bitmap bitmap = ThemeManager.ConvertToBitmap(icon);

			// if the bitmap is null, either there isn't a 32bpp 
			// icon we can convert or the display is running in 
			// less than 32bpp mode, so we can safely get windows 
			// to convert the icon for us (it won't ignore alpha)
			if (bitmap == null)
			{
				bitmap = icon.ToBitmap();
			}

			return bitmap;
		}


		/// <summary>
		/// Converts an Icon to a Bitmap
		/// </summary>
		/// <param name="icon">The Icon to be converted</param>
		/// <returns>A Bitmap that contains the converted Icon</returns>
		internal static unsafe Bitmap ConvertToBitmap(Icon icon)
		{
			Bitmap bitmap = null;

			// get the screen bpp
			int bitDepth = 0;

			IntPtr dc = NativeMethods.GetDC(IntPtr.Zero);
			bitDepth = NativeMethods.GetDeviceCaps(dc, 12 /*BITSPIXEL*/);
			bitDepth *= NativeMethods.GetDeviceCaps(dc, 14 /*PLANES*/);
			NativeMethods.ReleaseDC(IntPtr.Zero, dc);

			// if the screen bpp is not 32bpp return the 
			// null bitmap so that IconToBitmap can get 
			// windows to convert the icon (as it only 
			// ignores the alpha channel if the display 
			// is in 32bpp mode - why??)
			if (bitDepth != 32)
			{
				return bitmap;
			}

			// get the default icon sizes
			int defaultWidth = NativeMethods.GetSystemMetrics(11 /*SM_CXICON*/);
			int defaultHeight = NativeMethods.GetSystemMetrics(12 /*SM_CYICON*/);

			// convert the icon into a byte array

			MemoryStream ms = new MemoryStream();
			icon.Save(ms);

			byte[] iconData = ms.ToArray();

			ms.Close();
			ms = null;

			// prevent the garbage collector from relocating the iconData
			fixed (byte* data = iconData)
			{
				// "read" the data
				ICONFILE* iconfile = (ICONFILE*)data;

				// make sure we have valid data
				if (iconfile->reserved != 0 || iconfile->resourceType != 1 || iconfile->iconCount == 0)
				{
					throw new ArgumentException("The argument picture must be a picture that can be used as a Icon");
				}

				// set the current entry to the start of the entry section
				ICONENTRY* currentEntry = &iconfile->entries;

				// the entry that contains the icon whose properties closest 
				// match the default icon size and bitdepth
				ICONENTRY* targetEntry = null;
				int bpp = 0;

				// record the size of an ICONENTRY
				int iconEntrySize = Marshal.SizeOf(typeof(ICONENTRY));

				// make sure we have enough data to read each entry
				if ((iconEntrySize * iconfile->iconCount) >= iconData.Length)
				{
					throw new ArgumentException("The argument picture must be a picture that can be used as a Icon");
				}

				// go through each entry
				for (int i = 0; i < iconfile->iconCount; i++)
				{
					// get the icons bpp
					int iconBitDepth = currentEntry->numPlanes * currentEntry->bitsPerPixel;

					// make sure it is at least 16bpp
					iconBitDepth = Math.Max(iconBitDepth, 16);

					// set the target entry if we haven't already
					if (targetEntry == null)
					{
						targetEntry = currentEntry;
						bpp = iconBitDepth;
					}
					else
					{
						// work out the difference between default sizes
						int targetTotalDiff = Math.Abs(targetEntry->width - defaultWidth) + Math.Abs(targetEntry->height - defaultHeight);
						int currentTotalDiff = Math.Abs(currentEntry->width - defaultWidth) + Math.Abs(currentEntry->height - defaultHeight);

						// check if the current match is closer than the previous match
						if (currentTotalDiff < targetTotalDiff)
						{
							targetEntry = currentEntry;
							bpp = iconBitDepth;
						}
							// if the size differences are the same, compare bit depths
						else if ((currentTotalDiff == targetTotalDiff) && ((iconBitDepth <= bitDepth) && (iconBitDepth > bpp)) || (bpp > bitDepth) && (iconBitDepth < bpp))
						{
							targetEntry = currentEntry;
							bpp = iconBitDepth;
						}
					}

					// move to the next entry
					currentEntry++;
				}

				// make sure the target entry is valid
				if ((targetEntry->dataOffset < 0) || ((targetEntry->dataOffset + targetEntry->dataSize) > iconData.Length))
				{
					throw new ArgumentException("The argument picture must be a picture that can be used as a Icon");
				}

				// make sure the target is 32bpp
				if (targetEntry->bitsPerPixel == 32)
				{
					int offset = targetEntry->dataOffset;
					int dataSize = targetEntry->dataSize;

					bitmap = new Bitmap(targetEntry->width, targetEntry->height, PixelFormat.Format32bppArgb);

					int FILE_HEADER_BYTES = 40;
					int PALETTE_SIZE = targetEntry->bitsPerPixel * 4;
					int AND_MAP_SIZE = (targetEntry->width / 8) * (targetEntry->height / 8);
					int XOR_MAP_SIZE = dataSize - FILE_HEADER_BYTES - PALETTE_SIZE;
					int ROW_SIZE = targetEntry->width * (targetEntry->bitsPerPixel / 8);

					byte[] bitmapBytes = new byte[dataSize];

					GCHandle gcHandle = GCHandle.Alloc(bitmapBytes, GCHandleType.Pinned);
					// nb. we only copy the actual PNG data (no header)
					Array.Copy(iconData, offset, bitmapBytes, 0, dataSize);

					// copy the byte array contents back to a handle to the alpha bitmap (use lockbits)
					Rectangle copyArea = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
					BitmapData alphaBits = bitmap.LockBits(copyArea, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

					int dataStart = FILE_HEADER_BYTES + PALETTE_SIZE;

					// copymemory to bitmap data (Scan0)
					IntPtr firstCopyElement = Marshal.UnsafeAddrOfPinnedArrayElement(bitmapBytes, dataStart);
					NativeMethods.CopyMemory(alphaBits.Scan0, firstCopyElement, XOR_MAP_SIZE - ROW_SIZE);
					gcHandle.Free();

					// complete operation
					bitmap.UnlockBits(alphaBits);
					NativeMethods.GdiFlush();

					// flip bits (not sure why this is needed at the moment..)
					bitmap.RotateFlip(RotateFlipType.Rotate180FlipX);
				}
			}

			return bitmap;
		}


		/// <summary>
		/// Converts an Image to a byte array
		/// </summary>
		/// <param name="image">The image to be converted</param>
		/// <returns>A byte array that contains the converted image</returns>
		internal static byte[] ConvertImageToByteArray(Image image)
		{
			if (image == null)
			{
				return new byte[0];
			}

			MemoryStream ms = new MemoryStream();

			image.Save(ms, ImageFormat.Png);

			return ms.ToArray();
		}


		/// <summary>
		/// Converts a byte array to an Image
		/// </summary>
		/// <param name="bytes">The array of bytes to be converted</param>
		/// <returns>An Image that represents the byte array</returns>
		internal static Image ConvertByteArrayToImage(byte[] bytes)
		{
			if (bytes.Length == 0)
			{
				return null;
			}

			MemoryStream ms = new MemoryStream(bytes);

			return Image.FromStream(ms);
		}


		/// <summary>
		/// Converts a Color to a string representation
		/// </summary>
		/// <param name="color">The Color to be converted</param>
		/// <returns>A string that represents the specified color</returns>
		internal static string ConvertColorToString(Color color)
		{
			if (color == Color.Empty)
			{
				return null;
			}

			return "" + color.A + ":" + color.R + ":" + color.G + ":" + color.B;
		}


		/// <summary>
		/// Converts a string to a color
		/// </summary>
		/// <param name="col">The string to be converted</param>
		/// <returns>The converted Color</returns>
		internal static Color ConvertStringToColor(string col)
		{
			if (col == null)
			{
				return Color.Empty;
			}

			string[] s = col.Split(new char[] { ':' });

			if (s.Length != 4)
			{
				return Color.Empty;
			}

			return Color.FromArgb(Int32.Parse(s[0]), Int32.Parse(s[1]), Int32.Parse(s[2]), Int32.Parse(s[3]));
		}


		/// <summary>
		/// Converts an object to a byte array
		/// </summary>
		/// <param name="obj">The object to be converted</param>
		/// <returns>A byte array that contains the converted object</returns>
		internal static byte[] ConvertObjectToByteArray(object obj)
		{
			if (obj == null)
			{
				return new byte[0];
			}

			MemoryStream stream = new MemoryStream();
			IFormatter formatter = new BinaryFormatter();

			formatter.Serialize(stream, obj);

			byte[] bytes = stream.ToArray();

			stream.Flush();
			stream.Close();
			stream = null;

			return bytes;
		}


		/// <summary>
		/// Converts a byte array to an object
		/// </summary>
		/// <param name="bytes">The array of bytes to be converted</param>
		/// <returns>An object that represents the byte array</returns>
		internal static object ConvertByteArrayToObject(byte[] bytes)
		{
			if (bytes.Length == 0)
			{
				return null;
			}

			MemoryStream stream = new MemoryStream(bytes);
			stream.Position = 0;

			IFormatter formatter = new BinaryFormatter();

			object obj = formatter.Deserialize(stream);

			stream.Close();
			stream = null;

			return obj;
		}

		#endregion
	}

	#endregion



	#region UxTheme

	public class UxTheme
	{
		/// <summary>
		/// Private constructor
		/// </summary>
		private UxTheme()
		{

		}


		/// <summary>
		/// Reports whether the current application's user interface 
		/// displays using visual styles
		/// </summary>
		public static bool AppThemed
		{
			get
			{
				bool themed = false;

				OperatingSystem os = System.Environment.OSVersion;

				// check if the OS id XP or higher
				// fix:	Win2k3 now recognised
				//      Russkie (codeprj@webcontrol.net.au)
				if (os.Platform == PlatformID.Win32NT && ((os.Version.Major == 5 && os.Version.Minor >= 1) || os.Version.Major > 5))
				{
					themed = IsAppThemed();
				}

				return themed;
			}
		}


		/// <summary>
		/// Retrieves the name of the current visual style
		/// </summary>
		public static String ThemeName
		{
			get
			{
				StringBuilder themeName = new StringBuilder(256);

				GetCurrentThemeName(themeName, 256, null, 0, null, 0);

				return themeName.ToString();
			}
		}


		/// <summary>
		/// Retrieves the color scheme name of the current visual style
		/// </summary>
		public static String ColorName
		{
			get
			{
				StringBuilder themeName = new StringBuilder(256);
				StringBuilder colorName = new StringBuilder(256);

				GetCurrentThemeName(themeName, 256, colorName, 256, null, 0);

				return colorName.ToString();
			}
		}


		#region Win32 Methods

		/// <summary>
		/// Opens the theme data for a window and its associated class
		/// </summary>
		/// <param name="hwnd">Handle of the window for which theme data 
		/// is required</param>
		/// <param name="pszClassList">Pointer to a string that contains 
		/// a semicolon-separated list of classes</param>
		/// <returns>OpenThemeData tries to match each class, one at a 
		/// time, to a class data section in the active theme. If a match 
		/// is found, an associated HTHEME handle is returned. If no match 
		/// is found NULL is returned</returns>
		[DllImport("UxTheme.dll")]
		public static extern IntPtr OpenThemeData(IntPtr hwnd, [MarshalAs(UnmanagedType.LPTStr)] string pszClassList);


		/// <summary>
		/// Closes the theme data handle
		/// </summary>
		/// <param name="hTheme">Handle to a window's specified theme data. 
		/// Use OpenThemeData to create an HTHEME</param>
		/// <returns>Returns S_OK if successful, or an error value otherwise</returns>
		[DllImport("UxTheme.dll")]
		public static extern int CloseThemeData(IntPtr hTheme);


		/// <summary>
		/// Draws the background image defined by the visual style for the 
		/// specified control part
		/// </summary>
		/// <param name="hTheme">Handle to a window's specified theme data. 
		/// Use OpenThemeData to create an HTHEME</param>
		/// <param name="hdc">Handle to a device context (HDC) used for 
		/// drawing the theme-defined background image</param>
		/// <param name="iPartId">Value of type int that specifies the part 
		/// to draw</param>
		/// <param name="iStateId">Value of type int that specifies the state 
		/// of the part to draw</param>
		/// <param name="pRect">Pointer to a RECT structure that contains the 
		/// rectangle, in logical coordinates, in which the background image 
		/// is drawn</param>
		/// <param name="pClipRect">Pointer to a RECT structure that contains 
		/// a clipping rectangle. This parameter may be set to NULL</param>
		/// <returns>Returns S_OK if successful, or an error value otherwise</returns>
		[DllImport("UxTheme.dll")]
		public static extern int DrawThemeBackground(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, ref RECT pRect, ref RECT pClipRect);


		/// <summary>
		/// Tests if a visual style for the current application is active
		/// </summary>
		/// <returns>TRUE if a visual style is enabled, and windows with 
		/// visual styles applied should call OpenThemeData to start using 
		/// theme drawing services, FALSE otherwise</returns>
		[DllImport("UxTheme.dll")]
		public static extern bool IsThemeActive();


		/// <summary>
		/// Reports whether the current application's user interface 
		/// displays using visual styles
		/// </summary>
		/// <returns>TRUE if the application has a visual style applied,
		/// FALSE otherwise</returns>
		[DllImport("UxTheme.dll")]
		public static extern bool IsAppThemed();


		/// <summary>
		/// Retrieves the name of the current visual style, and optionally retrieves the 
		/// color scheme name and size name
		/// </summary>
		/// <param name="pszThemeFileName">Pointer to a string that receives the theme 
		/// path and file name</param>
		/// <param name="dwMaxNameChars">Value of type int that contains the maximum 
		/// number of characters allowed in the theme file name</param>
		/// <param name="pszColorBuff">Pointer to a string that receives the color scheme 
		/// name. This parameter may be set to NULL</param>
		/// <param name="cchMaxColorChars">Value of type int that contains the maximum 
		/// number of characters allowed in the color scheme name</param>
		/// <param name="pszSizeBuff">Pointer to a string that receives the size name. 
		/// This parameter may be set to NULL</param>
		/// <param name="cchMaxSizeChars">Value of type int that contains the maximum 
		/// number of characters allowed in the size name</param>
		/// <returns>Returns S_OK if successful, otherwise an error code</returns>
		[DllImport("UxTheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
		protected static extern int GetCurrentThemeName(StringBuilder pszThemeFileName, int dwMaxNameChars, StringBuilder pszColorBuff, int cchMaxColorChars, StringBuilder pszSizeBuff, int cchMaxSizeChars);


		/// <summary>
		/// Draws the part of a parent control that is covered by a 
		/// partially-transparent or alpha-blended child control
		/// </summary>
		/// <param name="hwnd">Handle of the child control</param>
		/// <param name="hdc">Handle to the child control's device context </param>
		/// <param name="prc">Pointer to a RECT structure that defines the 
		/// area to be drawn. The rectangle is in the child window's coordinates. 
		/// This parameter may be set to NULL</param>
		/// <returns>Returns S_OK if successful, or an error value otherwise</returns>
		[DllImport("UxTheme.dll")]
		public static extern int DrawThemeParentBackground(IntPtr hwnd, IntPtr hdc, ref RECT prc);

		#endregion



		#region WindowClasses

		/// <summary>
		/// Window class IDs used by UxTheme.dll to draw controls
		/// </summary>
		public class WindowClasses
		{
			/// <summary>
			/// TextBox class
			/// </summary>
			public static readonly string Edit = "EDIT";

			/// <summary>
			/// ListView class
			/// </summary>
			public static readonly string ListView = "LISTVIEW";

			/// <summary>
			/// TreeView class
			/// </summary>
			public static readonly string TreeView = "TREEVIEW";
		}

		#endregion



		#region Parts

		/// <summary>
		/// Window parts IDs used by UxTheme.dll to draw controls
		/// </summary>
		public class Parts
		{
			#region Edit

			/// <summary>
			/// TextBox parts
			/// </summary>
			public enum Edit
			{
				/// <summary>
				/// TextBox
				/// </summary>
				EditText = 1
			}

			#endregion


			#region ListView

			/// <summary>
			/// ListView parts
			/// </summary>
			public enum ListView
			{
				/// <summary>
				/// ListView
				/// </summary>
				ListItem = 1
			}

			#endregion


			#region TreeView

			/// <summary>
			/// TreeView parts
			/// </summary>
			public enum TreeView
			{
				/// <summary>
				/// TreeView
				/// </summary>
				TreeItem = 1
			}

			#endregion
		}

		#endregion



		#region PartStates

		/// <summary>
		/// Window part state IDs used by UxTheme.dll to draw controls
		/// </summary>
		public class PartStates
		{
			#region EditParts

			/// <summary>
			/// TextBox part states
			/// </summary>
			public enum EditText
			{
				/// <summary>
				/// The TextBox is in its normal state
				/// </summary>
				Normal = 1,

				/// <summary>
				/// The mouse is over the TextBox
				/// </summary>
				Hot = 2,

				/// <summary>
				/// The TextBox is selected
				/// </summary>
				Selected = 3,

				/// <summary>
				/// The TextBox is disabled
				/// </summary>
				Disabled = 4,

				/// <summary>
				/// The TextBox currently has focus
				/// </summary>
				Focused = 5,

				/// <summary>
				/// The TextBox is readonly
				/// </summary>
				Readonly = 6
			}

			#endregion


			#region ListViewParts

			/// <summary>
			/// ListView part states
			/// </summary>
			public enum ListItem
			{
				/// <summary>
				/// The ListView is in its normal state
				/// </summary>
				Normal = 1,

				/// <summary>
				/// The mouse is over the ListView
				/// </summary>
				Hot = 2,

				/// <summary>
				/// The ListView is selected
				/// </summary>
				Selected = 3,

				/// <summary>
				/// The ListView is disabled
				/// </summary>
				Disabled = 4,

				/// <summary>
				/// The ListView is selected but currently does not have focus
				/// </summary>
				SelectedNotFocused = 5
			}

			#endregion


			#region TreeViewParts

			/// <summary>
			/// TreeView part states
			/// </summary>
			public enum TreeItem
			{
				/// <summary>
				/// The TreeView is in its normal state
				/// </summary>
				Normal = 1,

				/// <summary>
				/// The mouse is over the TreeView
				/// </summary>
				Hot = 2,

				/// <summary>
				/// The TreeView is selected
				/// </summary>
				Selected = 3,

				/// <summary>
				/// The TreeView is disabled
				/// </summary>
				Disabled = 4,

				/// <summary>
				/// The TreeView is selected but currently does not have focus
				/// </summary>
				SelectedNotFocused = 5
			}

			#endregion
		}

		#endregion
	}

	#endregion



	#region Parser Class
	internal class Parser
	{
		private const int UNKNOWN = 0;
		private const int MAINSECTIONSS = 1;
		private const int MAINSECTIONTASKSS = 2;
		private const int SECTIONSS = 3;
		private const int SECTIONTASKSS = 4;
		private const int TASKPANE = 5;
		private int style;

		private const int BUTTON = 1;
		private const int DESTINATIONTASK = 2;
		private const int ACTIONTASK = 4;
		private const int TITLE = 8;
		private const int ARROW = 16;
		private const int WATERMARK = 32;
		private const int TASKLIST = 64;
		private const int SECTIONLIST = 128;
		private const int SELECTED = 256;
		private const int MOUSEFOCUSED = 512;
		private const int KEYFOCUSED = 1024;
		private const int EXPANDO = 2048;
		private const int BACKDROP = 4096;
		private const int HEADER = 8192;
		private int section;

		private const int CONTENT = 1;
		private const int CONTENTALIGN = 2;
		private const int FONTFACE = 3;
		private const int FONTSIZE = 4;
		private const int FONTWEIGHT = 5;
		private const int FONTSTYLE = 6;
		private const int BACKGROUND = 7;
		private const int FOREGROUND = 8;
		private const int BORDERTHICKNESS = 9;
		private const int BORDERDOLOR = 10;
		private const int PADDING = 11;
		private const int MARGIN = 12;
		private int property;

		private ExplorerBarInfo info;

		private StringTokenizer tokenizer;


		public Parser(string uifile)
		{
			uifile = uifile.Replace("rp", " ");
			uifile = uifile.Replace("rcstr", " ");
			uifile = uifile.Replace("rcint", " ");
			uifile = uifile.Replace("pt", " ");
			uifile = uifile.Replace("rect", " ");

			this.tokenizer = new StringTokenizer(uifile, " \t\n\r\f<>=()[]{}:;,\\");

			this.style = UNKNOWN;
			this.section = UNKNOWN;
			this.property = UNKNOWN;
		}
		public ExplorerBarInfo Parse()
		{
			this.info = new ExplorerBarInfo();

			string token = null;

			while (this.tokenizer.HasMoreTokens())
			{
				token = this.tokenizer.NextToken();

				if (token.Equals("style"))
				{
					style = GetStyle(token);
				}
				else if (token.Equals("/style"))
				{
					style = UNKNOWN;
				}
				else if (style != UNKNOWN && IsSection(token))
				{
					section = GetSection(token);
				}
				else if (style != UNKNOWN && section != UNKNOWN && IsProperty(token))
				{
					property = GetPropertyType(token);
					ExtractProperty();
				}
			}

			return info;
		}


		private int GetStyle(string s)
		{
			if (!this.tokenizer.PeekToken().Equals("resid"))
			{
				return UNKNOWN;
			}

			this.tokenizer.SkipToken();
			string t = this.tokenizer.NextToken();

			switch (t)
			{
				case "mainsectionss": return MAINSECTIONSS;

				case "mainsectiontaskss": return MAINSECTIONTASKSS;

				case "sectionss": return SECTIONSS;

				case "sectiontaskss": return SECTIONTASKSS;

				case "taskpane": return TASKPANE;
			}

			return UNKNOWN;
		}


		private bool IsSection(string s)
		{
			return (s.Equals("button") || s.Equals("destinationtask") ||
				s.Equals("actiontask") || s.Equals("title") ||
				s.Equals("arrow") || s.Equals("watermark") ||
				s.Equals("tasklist") || s.Equals("sectionlist") ||
				s.Equals("backdrop") || s.Equals("expando") || s.Equals("header"));
		}
		private int GetSection(string s)
		{
			switch (s)
			{
				case "button": if (this.tokenizer.PeekToken().Equals("keyfocused"))
							   {
								   this.tokenizer.SkipToken();

								   return BUTTON + KEYFOCUSED;
							   }
					return BUTTON;

				case "destinationtask": return DESTINATIONTASK;

				case "actiontask": return ACTIONTASK;

				case "title": if (this.tokenizer.PeekToken().Equals("mousefocused"))
							  {
								  this.tokenizer.SkipToken();

								  return TITLE + MOUSEFOCUSED;
							  }
					return TITLE;

				case "arrow": if (this.tokenizer.PeekToken().Equals("selected"))
							  {
								  this.tokenizer.SkipToken();

								  if (this.tokenizer.PeekToken().Equals("mousefocused"))
								  {
									  this.tokenizer.SkipToken();

									  return ARROW + SELECTED + MOUSEFOCUSED;
								  }

								  return ARROW + SELECTED;
							  }
							  else if (this.tokenizer.PeekToken().Equals("mousefocused"))
							  {
								  this.tokenizer.SkipToken();

								  return ARROW + MOUSEFOCUSED;
							  }
					return ARROW;

				case "watermark": return WATERMARK;

				case "tasklist": return TASKLIST;

				case "sectionlist": return SECTIONLIST;

				case "expando": return EXPANDO;

				case "backdrop": return BACKDROP;

				case "header": return HEADER;
			}

			return UNKNOWN;
		}

		private bool IsProperty(string s)
		{
			return (s.Equals("content") || s.Equals("contentalign") ||
				s.Equals("fontface") || s.Equals("fontsize") ||
				s.Equals("fontweight") || s.Equals("fontstyle") ||
				s.Equals("background") || s.Equals("foreground") ||
				s.Equals("borderthickness") || s.Equals("bordercolor") ||
				s.Equals("padding") || s.Equals("margin") || s.Equals("cursor"));
		}

		private int GetPropertyType(string s)
		{
			switch (s)
			{
				case "content": return CONTENT;

				case "contentalign": return CONTENTALIGN;

				case "fontface": return FONTFACE;

				case "fontsize": return FONTSIZE;

				case "fontweight": return FONTWEIGHT;

				case "fontstyle": return FONTSTYLE;

				case "background": return BACKGROUND;

				case "foreground": return FOREGROUND;

				case "borderthickness": return BORDERTHICKNESS;

				case "bordercolor": return BORDERDOLOR;

				case "padding": return PADDING;

				case "margin": return MARGIN;
			}

			return UNKNOWN;
		}


		private void ExtractProperty()
		{
			switch (property)
			{
				case CONTENT: ExtractContent();
					break;

				case CONTENTALIGN: ExtractContentAlignment();
					break;

				case FONTFACE: ExtractFontFace();
					break;

				case FONTSIZE: ExtractFontSize();
					break;

				case FONTWEIGHT: ExtractFontWeight();
					break;

				case FONTSTYLE: ExtractFontStyle();
					break;

				case BACKGROUND: ExtractBackground();
					break;

				case FOREGROUND: ExtractForeground();
					break;

				case BORDERTHICKNESS: ExtractBorder();
					break;

				case BORDERDOLOR: ExtractBorderColor();
					break;

				case PADDING: ExtractPadding();
					break;

				case MARGIN: ExtractMargin();
					break;
			}
		}
		private void ExtractContent()
		{
			string token = this.tokenizer.PeekToken();
			if (token.Equals("rcbmp"))
			{
				this.tokenizer.SkipToken();

				ExtractBitmap();
			}
		}

		private void ExtractContentAlignment()
		{
			string token = this.tokenizer.NextToken();

			ContentAlignment c = GetContentAlignment(token);

			bool wrap = (token.IndexOf("wrap") != -1);

			if (style == MAINSECTIONSS)
			{
				if (section == TITLE)
				{
					info.Header.SpecialAlignment = c;
				}
				else if (section == WATERMARK)
				{
					info.Expando.WatermarkAlignment = c;
				}
			}
			else if (style == SECTIONSS)
			{
				if (section == TITLE)
				{
					info.Header.NormalAlignment = c;
				}
				else if (section == WATERMARK)
				{
					info.Expando.WatermarkAlignment = c;
				}
			}
			else if (style == TASKPANE)
			{
				if (section == BACKDROP)
				{
					info.TaskPane.WatermarkAlignment = c;
				}
			}
		}

		private ContentAlignment GetContentAlignment(string s)
		{
			ContentAlignment c;

			if (s.IndexOf("top") != -1)
			{
				if (s.IndexOf("left") != -1)
				{
					c = ContentAlignment.TopLeft;
				}
				else if (s.IndexOf("center") != -1)
				{
					c = ContentAlignment.TopCenter;
				}
				else if (s.IndexOf("right") != -1)
				{
					c = ContentAlignment.TopRight;
				}
				else
				{
					c = ContentAlignment.TopLeft;
				}
			}
			else if (s.IndexOf("middle") != -1)
			{
				if (s.IndexOf("left") != -1)
				{
					c = ContentAlignment.MiddleLeft;
				}
				else if (s.IndexOf("center") != -1)
				{
					c = ContentAlignment.MiddleCenter;
				}
				else if (s.IndexOf("right") != -1)
				{
					c = ContentAlignment.MiddleRight;
				}
				else
				{
					c = ContentAlignment.MiddleLeft;
				}
			}
			else if (s.IndexOf("bottom") != -1)
			{
				if (s.IndexOf("left") != -1)
				{
					c = ContentAlignment.BottomLeft;
				}
				else if (s.IndexOf("center") != -1)
				{
					c = ContentAlignment.BottomCenter;
				}
				else if (s.IndexOf("right") != -1)
				{
					c = ContentAlignment.BottomRight;
				}
				else
				{
					c = ContentAlignment.BottomLeft;
				}
			}
			else
			{
				if (s.Equals("wrapleft"))
				{
					c = ContentAlignment.MiddleLeft;
				}
				if (s.Equals("wrapcenter"))
				{
					c = ContentAlignment.MiddleRight;
				}
				if (s.Equals("wrapright"))
				{
					c = ContentAlignment.MiddleRight;
				}
				else
				{
					c = ContentAlignment.MiddleLeft;
				}
			}

			return c;
		}

		private void ExtractFontFace()
		{
			int id = Int32.Parse(this.tokenizer.NextToken());
			string fontName = ThemeManager.GetResourceString(id);

			if (style == MAINSECTIONSS || style == SECTIONSS)
			{
				if (section == EXPANDO)
				{
					if (fontName != null && fontName.Length > 0)
					{
						info.Header.FontName = fontName;
					}
				}
			}
		}

		private void ExtractFontSize()
		{
			int id = Int32.Parse(this.tokenizer.NextToken());
			string fontSize = ThemeManager.GetResourceString(id);

			if (style == MAINSECTIONSS || style == SECTIONSS)
			{
				if (section == EXPANDO)
				{
					if (fontSize != null && fontSize.Length > 0)
					{
						info.Header.FontSize = Single.Parse(fontSize);
					}
				}
			}
		}

		private void ExtractFontWeight()
		{
			int weight = 400;
			int id = Int32.Parse(this.tokenizer.NextToken());
			string fontWeight = ThemeManager.GetResourceString(id);

			if (fontWeight != null && fontWeight.Length > 0)
			{
				weight = Int32.Parse(fontWeight);
			}

			FontStyle fontStyle;

			if (weight == 700)
			{
				fontStyle = FontStyle.Bold;
			}
			else
			{
				fontStyle = FontStyle.Regular;
			}

			if (style == MAINSECTIONSS)
			{
				if (section == BUTTON || section == HEADER)
				{
					info.Header.FontWeight = fontStyle;
				}
			}
			else if (style == SECTIONSS)
			{
				if (section == BUTTON || section == HEADER)
				{
					info.Header.FontWeight = fontStyle;
				}
			}
		}
		private void ExtractFontStyle()
		{
			string token = this.tokenizer.NextToken();

			FontStyle fontStyle;
			if (token.Equals("underline"))
			{
				fontStyle = FontStyle.Underline;
			}
			else if (token.Equals("italic"))
			{
				fontStyle = FontStyle.Italic;
			}
			else if (token.Equals("strikeout"))
			{
				fontStyle = FontStyle.Strikeout;
			}
			else
			{
				fontStyle = FontStyle.Regular;
			}
			if (style == MAINSECTIONSS || style == SECTIONSS)
			{
				if (section == TITLE)
				{
					info.Header.FontStyle = fontStyle;
				}
			}
			else if (style == MAINSECTIONTASKSS)
			{
				if (section - MOUSEFOCUSED == BUTTON)
				{
					info.TaskItem.FontDecoration = fontStyle;
				}
			}
			else if (style == SECTIONTASKSS)
			{
				if (section - MOUSEFOCUSED == BUTTON)
				{
					info.TaskItem.FontDecoration = fontStyle;
				}
			}
		}
		private void ExtractBackground()
		{
			string token = this.tokenizer.PeekToken();

			if (token.Equals("rcbmp"))
			{
				this.tokenizer.SkipToken();

				ExtractBitmap();
			}
			else if (token.Equals("gradient"))
			{
				this.tokenizer.SkipToken();

				info.TaskPane.GradientStartColor = ExtractColor();
				info.TaskPane.GradientEndColor = ExtractColor();
				info.TaskPane.GradientDirection = (LinearGradientMode)Int32.Parse(tokenizer.NextToken());
			}
			else
			{
				Color c = ExtractColor();
				if (c.A == 0 && c.R == 0 && c.G == 0 && c.B == 0)
				{
					return;
				}
				if (style == MAINSECTIONSS)
				{
					if (section == WATERMARK || section == TASKLIST)
					{
						info.Expando.SpecialBackColor = c;
					}
					else if (section == EXPANDO)
					{
						info.Expando.SpecialBackColor = c;
						info.Header.SpecialBackColor = c;
					}
					else if (section == HEADER)
					{
						info.Header.SpecialBackColor = c;
					}
				}
				else if (style == SECTIONSS)
				{
					if (section == TASKLIST)
					{
						info.Expando.NormalBackColor = c;
					}
					else if (section == EXPANDO)
					{
						info.Expando.NormalBackColor = c;
						info.Header.NormalBackColor = c;
					}
					else if (section == HEADER)
					{
						info.Header.NormalBackColor = c;
					}
				}
				else if (style == TASKPANE)
				{
					if (section == BACKDROP || section == SECTIONLIST)
					{
						info.TaskPane.GradientStartColor = c;
						info.TaskPane.GradientEndColor = c;
						info.TaskPane.GradientDirection = LinearGradientMode.Vertical;
					}
				}
			}
		}
		private void ExtractBitmap()
		{
			string id = this.tokenizer.NextToken();

			ImageStretchMode stretch = (ImageStretchMode)Int32.Parse(this.tokenizer.NextToken());

			string transparent = this.tokenizer.NextToken();

			Bitmap image = null;

			if (stretch == ImageStretchMode.Transparent || stretch == ImageStretchMode.ARGBImage)
			{
				image = ThemeManager.GetResourcePNG(id);
			}
			else
			{
				image = ThemeManager.GetResourceBMP(id);

				if (transparent.StartsWith("#"))
				{
					byte[] bytes = GetBytes(transparent);
					image.MakeTransparent(Color.FromArgb((int)bytes[0], (int)bytes[1], (int)bytes[2]));
				}
			}

			if (style == MAINSECTIONSS)
			{
				if (section == BUTTON || section == HEADER)
				{
					info.Header.SpecialBackImage = image;
				}
				else if (section == ARROW)
				{
					info.Header.SpecialArrowDown = image;
				}
				else if (section - SELECTED - MOUSEFOCUSED == ARROW)
				{
					info.Header.SpecialArrowUpHot = image;
				}
				else if (section - SELECTED == ARROW)
				{
					info.Header.SpecialArrowUp = image;
				}
				else if (section - MOUSEFOCUSED == ARROW)
				{
					info.Header.SpecialArrowDownHot = image;
				}
				else if (section == TASKLIST)
				{
					info.Expando.SpecialBackImage = image;
				}
			}
			else if (style == SECTIONSS)
			{
				if (section == BUTTON || section == HEADER)
				{
					info.Header.NormalBackImage = image;
				}
				else if (section == ARROW)
				{
					info.Header.NormalArrowDown = image;
				}
				else if (section - SELECTED - MOUSEFOCUSED == ARROW)
				{
					info.Header.NormalArrowUpHot = image;
				}
				else if (section - SELECTED == ARROW)
				{
					info.Header.NormalArrowUp = image;
				}
				else if (section - MOUSEFOCUSED == ARROW)
				{
					info.Header.NormalArrowDownHot = image;
				}
				else if (section == TASKLIST)
				{
					info.Expando.NormalBackImage = image;
				}
			}
			else if (style == TASKPANE)
			{
				if (section == SECTIONLIST)
				{
					info.TaskPane.BackImage = image;
					info.TaskPane.StretchMode = stretch;
				}
				else if (section == BACKDROP)
				{
					info.TaskPane.Watermark = image;
				}
			}
		}


		private void ExtractForeground()
		{
			Color c = ExtractColor();

			if (style == MAINSECTIONSS)
			{
				if (section == BUTTON || section == HEADER)
				{
					info.Header.SpecialTitleColor = c;
				}
				else if (section == TITLE)
				{
					info.Header.SpecialTitleColor = c;
				}
				else if (section - MOUSEFOCUSED == TITLE)
				{
					info.Header.SpecialTitleHotColor = c;
				}
				else if (section - KEYFOCUSED == TITLE)
				{
					info.Header.SpecialTitleHotColor = c;
				}
			}
			else if (style == MAINSECTIONTASKSS)
			{
				if (section == BUTTON)
				{
					info.TaskItem.LinkColor = c;
				}
				else if (section == TITLE)
				{
					info.TaskItem.LinkColor = c;
				}
				else if (section - MOUSEFOCUSED == TITLE)
				{
					info.TaskItem.HotLinkColor = c;
				}
			}
			else if (style == SECTIONSS)
			{
				if (section == BUTTON || section == HEADER)
				{
					info.Header.NormalTitleColor = c;
				}
				else if (section == TITLE)
				{
					info.Header.NormalTitleColor = c;
				}
				else if (section - MOUSEFOCUSED == TITLE)
				{
					info.Header.NormalTitleHotColor = c;
				}
				else if (section - KEYFOCUSED == TITLE)
				{
					info.Header.NormalTitleHotColor = c;
				}
			}
			else if (style == SECTIONTASKSS)
			{
				if (section == BUTTON)
				{
					info.TaskItem.LinkColor = c;
				}
				else if (section == TITLE)
				{
					info.TaskItem.LinkColor = c;
				}
				else if (section - MOUSEFOCUSED == TITLE)
				{
					info.TaskItem.HotLinkColor = c;
				}
			}
		}
		private void ExtractPadding()
		{
			Padding p = new Padding();
			p.Left = Int32.Parse(this.tokenizer.NextToken());
			p.Top = Int32.Parse(this.tokenizer.NextToken());
			p.Right = Int32.Parse(this.tokenizer.NextToken());
			p.Bottom = Int32.Parse(this.tokenizer.NextToken());

	
			if (style == MAINSECTIONSS)
			{
				if (section == BUTTON || section == HEADER)
				{
					info.Header.SpecialPadding = p;
				}
				else if (section == TASKLIST)
				{
					info.Expando.SpecialPadding = p;
				}
			}
			else if (style == MAINSECTIONTASKSS)
			{
				if (section == TITLE)
				{
					info.TaskItem.Padding = p;
				}
			}
			else if (style == SECTIONSS)
			{
				if (section == BUTTON || section == HEADER)
				{
					info.Header.NormalPadding = p;
				}
				else if (section == TASKLIST)
				{
					info.Expando.NormalPadding = p;
				}
			}
			else if (style == SECTIONTASKSS)
			{
				if (section == TITLE)
				{
					info.TaskItem.Padding = p;
				}
			}
			else if (style == TASKPANE)
			{
				if (section == SECTIONLIST)
				{
					info.TaskPane.Padding = p;
				}
			}
		}

		private void ExtractMargin()
		{
			Margin m = new Margin();
			m.Left = Int32.Parse(this.tokenizer.NextToken());
			m.Top = Int32.Parse(this.tokenizer.NextToken());
			m.Bottom = Int32.Parse(this.tokenizer.NextToken());
			m.Right = Int32.Parse(this.tokenizer.NextToken());
			if (style == MAINSECTIONTASKSS)
			{
				if (section == DESTINATIONTASK)
				{
					info.TaskItem.Margin = m;
				}
				else if (section == ACTIONTASK)
				{
					info.TaskItem.Margin = m;
				}
			}
			else if (style == SECTIONTASKSS)
			{
				if (section == DESTINATIONTASK)
				{
					info.TaskItem.Margin = m;
				}
				else if (section == ACTIONTASK)
				{
					info.TaskItem.Margin = m;
				}
			}
		}

		private void ExtractBorder()
		{
			Border b = new Border();
			b.Left = Int32.Parse(this.tokenizer.NextToken());
			b.Top = Int32.Parse(this.tokenizer.NextToken());
			b.Right = Int32.Parse(this.tokenizer.NextToken());
			b.Bottom = Int32.Parse(this.tokenizer.NextToken());

			if (style == MAINSECTIONSS)
			{
				if (section == BUTTON || section == HEADER)
				{
					info.Header.SpecialBorder = b;
				}
				else if (section == TASKLIST)
				{
					info.Expando.SpecialBorder = b;
				}
			}
			else if (style == SECTIONSS)
			{
				if (section == BUTTON || section == HEADER)
				{
					info.Header.NormalBorder = b;
				}
				else if (section == TASKLIST)
				{
					info.Expando.NormalBorder = b;
				}
			}
		}
		private void ExtractBorderColor()
		{
			Color c = ExtractColor();
			if (style == MAINSECTIONSS)
			{
				if (section == BUTTON || section == HEADER)
				{
					info.Header.SpecialBorderColor = c;
				}
				else if (section == TASKLIST)
				{
					info.Expando.SpecialBorderColor = c;
				}
			}
			else if (style == SECTIONSS)
			{
				if (section == BUTTON || section == HEADER)
				{
					info.Header.NormalBorderColor = c;
				}
				else if (section == TASKLIST)
				{
					info.Expando.NormalBorderColor = c;
				}
			}
		}
		private Color ExtractColor()
		{
			string token = this.tokenizer.PeekToken();

			Color c = Color.Transparent;

			if (token.Equals("rgb"))
			{
				c = ExtractRGBColor();
			}
			else if (token.Equals("argb"))
			{
				c = ExtractARGBColor();
			}
			else if (token.StartsWith("#"))
			{
				c = ExtractHexColor(token);
			}
			else
			{
				c = Color.FromName(token);
			}

			return c;
		}

		private Color ExtractRGBColor()
		{
			if (this.tokenizer.PeekToken().Equals("rgb"))
			{
				tokenizer.SkipToken();
			}

			return Color.FromArgb(Int32.Parse(this.tokenizer.NextToken()),			// Red
				Int32.Parse(this.tokenizer.NextToken()),		// Green
				Int32.Parse(this.tokenizer.NextToken()));		// Blue
		}


		private Color ExtractARGBColor()
		{
			if (this.tokenizer.PeekToken().Equals("argb"))
			{
				tokenizer.SkipToken();
			}

				Color c = Color.FromArgb(Int32.Parse(this.tokenizer.NextToken()),	// Alpha
				Int32.Parse(this.tokenizer.NextToken()),		// Red
				Int32.Parse(this.tokenizer.NextToken()),		// Green
				Int32.Parse(this.tokenizer.NextToken()));		// Blue

			if (c.A == 0 && c.R == 0 && c.G == 0 && c.B == 0)
			{
				return c;
			}

			c = Color.FromArgb(255 - c.A, c.R, c.G, c.B);

			return c;
		}
		private Color ExtractHexColor(string s)
		{
			byte[] bytes = GetBytes(s.Substring(1));

			return Color.FromArgb((int)bytes[0], (int)bytes[1], (int)bytes[2]);
		}
		public byte[] GetBytes(string hexString)
		{
			StringBuilder sb = new StringBuilder();
			char c;
			for (int i = 0; i < hexString.Length; i++)
			{
				c = hexString[i];

				if (IsHexDigit(c))
				{
					sb.Append(c);
				}
			}

			if (sb.Length % 2 != 0)
			{
				sb.Remove(sb.Length - 1, 1);
			}

			int byteLength = sb.Length / 2;
			byte[] bytes = new byte[byteLength];
			string hex;
			int j = 0;
			for (int i = 0; i < bytes.Length; i++)
			{
				hex = new String(new Char[] { sb[j], sb[j + 1] });
				bytes[i] = HexToByte(hex);
				j = j + 2;
			}

			return bytes;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="c"></param>
		/// <returns></returns>
		private bool IsHexDigit(Char c)
		{
			int numChar;
			int numA = Convert.ToInt32('A');
			int num1 = Convert.ToInt32('0');
			c = Char.ToUpper(c);
			numChar = Convert.ToInt32(c);

			if (numChar >= numA && numChar < (numA + 6))
			{
				return true;
			}

			if (numChar >= num1 && numChar < (num1 + 10))
			{
				return true;
			}

			return false;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="hex"></param>
		/// <returns></returns>
		private byte HexToByte(string hex)
		{
			if (hex.Length > 2 || hex.Length <= 0)
			{
				throw new ArgumentException("hex must be 1 or 2 characters in length");
			}

			byte newByte = byte.Parse(hex, System.Globalization.NumberStyles.HexNumber);

			return newByte;
		}
	}

	#endregion



	#region StringTokenizer Class

	internal class StringTokenizer
	{

		private int currentIndex;


		private int numberOfTokens;

		private ArrayList tokens;

		private string source;

		private string delimiter;


		public StringTokenizer(string source, string delimiter)
		{
			this.tokens = new ArrayList(10);
			this.source = source;
			this.delimiter = delimiter;

			if (delimiter.Length == 0)
			{
				this.delimiter = " ";
			}

			this.Tokenize();
		}

		public StringTokenizer(string source, char[] delimiter)
			: this(source, new string(delimiter))
		{

		}

		public StringTokenizer(string source)
			: this(source, "")
		{

		}

		private void Tokenize()
		{
			string s = this.source;
			StringBuilder sb = new StringBuilder();
			this.numberOfTokens = 0;
			this.tokens.Clear();
			this.currentIndex = 0;

			int i = 0;

			while (i < this.source.Length)
			{
				if (this.delimiter.IndexOf(this.source[i]) != -1)
				{
					if (sb.Length > 0)
					{
						this.tokens.Add(sb.ToString());

						sb.Remove(0, sb.Length);
					}
				}
				else
				{
					sb.Append(this.source[i]);
				}

				i++;
			}

			this.numberOfTokens = this.tokens.Count;
		}

		public int CountTokens()
		{
			return this.tokens.Count;
		}

		public bool HasMoreTokens()
		{
			if (this.currentIndex <= (this.tokens.Count - 1))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public string NextToken()
		{
			string s = "";

			if (this.currentIndex <= (this.tokens.Count - 1))
			{
				s = (string)tokens[this.currentIndex];

				this.currentIndex++;

				return s;
			}
			else
			{
				return null;
			}
		}

		public void SkipToken()
		{
			if (this.currentIndex <= (this.tokens.Count - 1))
			{
				this.currentIndex++;
			}
		}

		public string PeekToken()
		{
			string s = "";

			if (this.currentIndex <= (this.tokens.Count - 1))
			{
				s = (string)tokens[this.currentIndex];

				return s;
			}
			else
			{
				return null;
			}
		}

		public string Source
		{
			get
			{
				return this.source;
			}
		}

		public string Delimiter
		{
			get
			{
				return this.delimiter;
			}
		}
	}

	#endregion
}