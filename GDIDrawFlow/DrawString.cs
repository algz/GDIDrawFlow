using System;
using System.Drawing;

namespace GDIDrawFlow
{
	/// <summary>
	/// DrawString 的摘要说明。
	/// </summary>
	public class DrawString
	{
		private string strContent="请双击输入内容。";//内容
		private int x,y;// 坐标
		private int width,height;//宽，高
		private int iDrawStringListIndex;//写字板在数组的存储ID
		private int iTextSize=10;//字体大小
		private Font dSTextFont;//字体信息
		private Color dSTextColor;//字体颜色

		public DrawString(int index)
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
			this.iDrawStringListIndex=index;
			dSTextFont=new Font("宋体",iTextSize);
			this.dSTextColor=Color.Black;
		}

		/// <summary>
		/// 内容
		/// </summary>
		public string Content
		{
			get
			{
				return this.strContent;
			}
			set
			{
				this.strContent=value;
			}
		}

		/// <summary>
		///写字板在数组中的ID
		///</summary>
		public int DrawStrListIndex
		{
			get
			{
				return this.iDrawStringListIndex;
			}
			set
			{
				this.iDrawStringListIndex=value;
			}
		}


		#region 写字板的大小 位置
		/// <summary>
		/// 写字板的X坐标
		/// </summary>
		public int X
		{
			get
			{
				return this.x;
			}
			set
			{
				this.x=value;
			}
		}

		/// <summary>
		/// 写字板的Y坐标
		/// </summary>
		public int Y
		{
			get
			{
				return this.y;
			}
			set
			{
				this.y=value;
			}
		}

		/// <summary>
		/// 写字板的宽度
		/// </summary>
		public int Width
		{
			get
			{
				return this.width;
			}
			set
			{
				this.width=value;
			}
		}
	
		/// <summary>
		/// 写字板的高度
		/// </summary>
		public int Height
		{
			get
			{
				return this.height;
			}
			set
			{
				this.height=value;
			}
		}
		
		#endregion
		#region 字体信息
		/// <summary>
		/// 字体大小
		/// </summary>
		public int TextSize
		{
			get
			{
				return this.iTextSize;
			}
			set
			{
				this.iTextSize=value;
			}
		}

		/// <summary>
		/// 字体信息
		/// </summary>
		public Font DSTextFont
		{
			get
			{
				this.DSTextFont=new Font("宋体",iTextSize);
				return this.dSTextFont;
			}
			set
			{
				this.dSTextFont=value;
			}
		}

		/// <summary>
		/// 字体颜色
		/// </summary>
		public Color DSTextColor
		{
			get
			{
				return this.dSTextColor;
			}
			set
			{
				this.dSTextColor=value;
			}
		}
		#endregion 
	}
}
