using System;
using System.Drawing;

namespace GDIDrawFlow
{
	/// <summary>
	/// Line 的摘要说明。
	/// </summary>
	public class Line
	{
		private int[] iLineNode;//坐标存储
		private int iLineListIndex;//线在数组中的ID
		private int iLastCount=0;//线坐标存储数组中的最后一点
		private Node firstNode,secondNode;//线开始 和结束  端点 连接的节点
		private int iFirNodeInterfaceIndex,iSecNodeInterfaceIndex;//连接到节点的连接口
		private Color lineColor;//线和文字说明的颜色
		private int iLineSize=1;//线的大小
		private DrawObjectType drawObjectType;//线的类型
		private string strContent="连接";//文字说明
		private Font lineTextFont;//文字说明的字体
		private int iTextSize=10;//文字说明的大小
		private bool b_FoldModality;//折线转变的样式。。-|_   false   |-|  true

		public Line(DrawObjectType drawObjectType,int index)
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
			this.iLineListIndex=index;
			this.iLineNode=new int[10];
			lineColor=Color.Black;
			this.drawObjectType=drawObjectType;
			lineTextFont=new Font("宋体",iTextSize);
		}

		/// <summary>
		/// 线的类型
		/// </summary>
		public  enum DrawObjectType
		{
			/// <summary>
			/// 直线
			/// </summary>
			DrawBeeLine,
			/// <summary>
			///  折线
			/// </summary>
			DrawFoldLine
		}
		/// <summary>
		/// 获取和设置线的类型
		/// </summary>
		public DrawObjectType ObjectType
		{
			get
			{
				return drawObjectType;
			}
			set
			{
				this.drawObjectType=value;
			}
		}

		/// <summary>
		/// 获取线的坐标信息
		/// </summary>
		/// <param name="index">端点 标识</param>
		/// <returns></returns>
		public int GetLineNodeInfo(int index)
		{
			return this.iLineNode[index];
		}
		
		/// <summary>
		/// 线在数组中的存储ID
		/// </summary>
		public int LineListIndex
		{
			get
			{
				return this.iLineListIndex;
			}
			set
			{
				this.iLineListIndex=value;
			}
		}

		/// <summary>
		/// 添加线 坐标
		/// </summary>
		/// <param name="x">X 坐标</param>
		/// <param name="y">Y 坐标</param>
		public void addPoint(int x,int y)
		{
			this.iLineNode[this.iLastCount]=x;
			this.iLineNode[this.iLastCount+1]=y;
		}

		/// <summary>
		/// 修改线的坐标 
		/// </summary>
		/// <param name="pointIndex"></param>
		/// <param name="x">X 坐标</param>
		/// <param name="y">Y 坐标</param>
		public void setPoint(int pointIndex,int x,int y)
		{
			this.iLineNode[pointIndex*2]=x;
			this.iLineNode[pointIndex*2+1]=y;
		}

		/// <summary>
		/// 直线或折线的表示   4 直线  8 折线
		/// </summary>
		public int LineNodeCount
		{
			get
			{
				return this.iLastCount;
			}
			set
			{
				this.iLastCount=value;
			}
		}
	
		/// <summary>
		/// 开始端点的连接节点
		/// </summary>
		public Node FirstNode
		{
			get
			{
				return this.firstNode;
			}
			set
			{
				this.firstNode=value;
			}
		}

		/// <summary>
		/// 结束端点的连接节点
		/// </summary>
		public Node SecondNode
		{
			get
			{
				return this.secondNode;
			}
			set
			{
				this.secondNode=value;
			}
		}

		/// <summary>
		/// 开始端点  连接的节点的连接口
		/// </summary>
		public int FirNodeInterfaceIndex
		{
			get
			{
				return this.iFirNodeInterfaceIndex;
			}
			set
			{
				this.iFirNodeInterfaceIndex=value;
			}
		}

		/// <summary>
		/// 结束端点  连接的节点的连接口
		/// </summary>
		public int SecNodeInterfaceIndex
		{
			get
			{
				return this.iSecNodeInterfaceIndex;
			}
			set
			{
				this.iSecNodeInterfaceIndex=value;
			}
		}

		#region 线的坐标
		public int X0
		{
			get
			{
				return this.iLineNode[0];
			}
			set
			{
				this.iLineNode[0]=value;
			}
		}

		public int Y0
		{
			get
			{
				return this.iLineNode[1];
			}
			set
			{
				this.iLineNode[1]=value;
			}
		}

		public int X1
		{
			get
			{
				return this.iLineNode[2];
			}
			set
			{
				this.iLineNode[2]=value;
			}
		}

		public int Y1
		{
			get
			{
				return this.iLineNode[3];
			}
			set
			{
				this.iLineNode[3]=value;
			}
		}

		public int X2
		{
			get
			{
				return this.iLineNode[4];
			}
			set
			{
				this.iLineNode[4]=value;
			}
		}

		public int Y2
		{
			get
			{
				return this.iLineNode[5];
			}
			set
			{
				this.iLineNode[5]=value;
			}
		}

		public int X3
		{
			get
			{
				return this.iLineNode[6];
			}
			set
			{
				this.iLineNode[6]=value;
			}
		}

		public int Y3
		{
			get
			{
				return this.iLineNode[7];
			}
			set
			{
				this.iLineNode[7]=value;
			}
		}
		#endregion
		/// <summary>
		/// 线及线的的文字说明的颜色
		/// </summary>
		public Color LineColor
		{
			get
			{
				return this.lineColor;
			}
			set
			{
				this.lineColor=value;
			}
		}

		/// <summary>
		/// 线的大小
		/// </summary>
		public int LineSize
		{
			get
			{
				return this.iLineSize;
			}
			set
			{
				this.iLineSize=value;
			}
		}
		/// <summary>
		///  线的文字说明的内容
		/// </summary>
		public  string Content
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
		///  线的文字说明的字体
		/// </summary>
		public Font LineTextFont
		{
			get
			{
				this.lineTextFont=new Font("宋体",iTextSize);
				return this.lineTextFont;
			}
			set
			{
				this.lineTextFont=value;
			}
		}

		/// <summary>
		///  线的文字说明的大小
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
		/// 获取和设置折线的样式 
		/// </summary>
		public bool Modality
		{
			get
			{
				return this.b_FoldModality;
			}
			set
			{
				this.b_FoldModality=value;
			}
		}
	}
}
