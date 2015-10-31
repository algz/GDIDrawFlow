using System;
using System.Drawing;
using System.Collections;

namespace GDIDrawFlow
{
	/// <summary>
	/// Node 的摘要说明。
	/// </summary>
	/// 

	public class Node
	{
		private int x,y;//节点坐标
		private int width,height;//节点宽，高
		private int iNodeListIndex;//节点在数组中的ID
		private DrawObjectType drawObjectType;//节点类型
		private string nodeText;//节点名称
		private int iTextSize=10;//节点名称文字大小
		private Font nodeTextFont;//节点名称文字字体
		private Color nodeTextColor;//节点名称文字颜色
		private Color cBorderColor;//矩形  和 椭圆的 边框颜色
		private Color cFillColor;//矩形  和 椭圆的 填充颜色
		private int iConnectInCount=0;//连入的线数
		private int iConnectOutCount=0;//连出的线数
		private DateTime dt_inFlowTime;//连入时间
		private DateTime dt_outFlowTime;//连出时间
		private string str_Function="未设置";//功能说明
		private string str_OperationRole="未设置";//业务角色
		private string str_FunctionInfo="未设置";//功能说明
		private string str_Info="未设置";//详细信息
		private ArrayList arr_InFlowNodeID;//流入节点的ID
		private ArrayList arr_OutFlowNodeID;//流出节点的ID
			
		/// <param name="index">索引</param>
		/// <param name="drawObjectType">节点类型</param>
		/// <param name="nodeText">节点文本</param>
		public Node(int index,DrawObjectType drawObjectType,string nodeText)
		{
			this.iNodeListIndex=index;
			this.drawObjectType=drawObjectType;
			this.nodeText=nodeText;
			nodeTextFont=new Font("宋体",iTextSize);
			nodeTextColor=Color.Black;
			cBorderColor=Color.Black;
			cFillColor=Color.Transparent;
			this.dt_inFlowTime=DateTime.Now;
			this.dt_outFlowTime=DateTime.Now;
			this.arr_InFlowNodeID=new ArrayList();
			this.arr_OutFlowNodeID=new ArrayList();
		}

		/// <summary>
		/// 节点类型
		/// </summary>
		public  enum DrawObjectType
		{
			/// <summary>
			/// 开始图元
			/// </summary>
			DrawNodeBegin,
			/// <summary>
			/// 节点图元
			/// </summary>
			DrawNodeGeneral,
			/// <summary>
			/// 特定操作图元
			/// </summary>
			DrawSpecificallyOperation,
			/// <summary>
			/// 顺序图元
			/// </summary>
			DrawGradation,
			/// <summary>
			/// 同步图元
			/// </summary>
			DrawSynchronization,
			/// <summary>
			/// 分支图元
			/// </summary>
			DrawAsunder,
			/// <summary>
			/// 汇聚图元
			/// </summary>
			DrawConverge,
			/// <summary>
			/// 汇总连接图元
			/// </summary>
			DrawGather,
			/// <summary>
			/// 判断图元
			/// </summary>
			DrawJudgement,
			/// <summary>
			/// 数据图元
			/// </summary>
			DrawDataNode,
			/// <summary>
			/// 结束图元
			/// </summary>
			DrawNodeEnd,
			/// <summary>
			/// 绘制矩形
			/// </summary>
			DrawRectangle,
			/// <summary>
			/// 绘制椭圆
			/// </summary>
			DrawEllipse
		}
		/// <summary>
		///　获取和设置节点名称
		/// </summary>
		public string NodeText
		{
			get
			{
				return this.nodeText;
			}
			set
			{
				this.nodeText=value;
			}
		}
		/// <summary>
		/// 获取和设置节点类型
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
		/// 获取和设置节点字体
		/// </summary>
		public Font NodeTextFont
		{
			get
			{
				nodeTextFont=new Font("宋体",iTextSize);
				return this.nodeTextFont;
			}
			set
			{
				this.nodeTextFont=value;
			}
		}
		/// <summary>
		/// 获取和设置节点字体颜色
		/// </summary>
		public Color NodeTextColor
		{
			get
			{
				return this.nodeTextColor;
			}
			set
			{
				this.nodeTextColor=value;
			}
		}

		/// <summary>
		/// 获取和设置节点边框颜色  （矩形  和 椭圆）
		/// </summary>
		public Color BorderColor
		{
			get
			{
				return this.cBorderColor;
			}
			set
			{
				this.cBorderColor=value;
			}
		}

		/// <summary>
		/// 获取和设置节点填充颜色  （矩形  和 椭圆）
		/// </summary>
		public Color FillColor
		{
			get
			{
				return this.cFillColor;
			}
			set
			{
				this.cFillColor=value;
			}
		}

		#region 节点位置  大小信息
		/// <summary>
		/// 节点的X坐标
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
		/// 节点的Y坐标 
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
		/// 节点的宽度
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
		/// 节点的高度
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
		/// <summary>
		/// 节点存储于数组中的ID
		/// </summary>
		public int NodeListIndex
		{
			get 
			{
				return this.iNodeListIndex;
			}
			set
			{
				this.iNodeListIndex=value;
			}
		}

		/// <summary>
		/// 连入节点的线个数
		/// </summary>
		public int ConnectInCount
		{
			get
			{
				return this.iConnectInCount;
			}
			set
			{
				this.iConnectInCount=value;
			}
		}

		/// <summary>
		/// 连出节点线的个数
		/// </summary>
		public int ConnectOutCount
		{
			get
			{
				return this.iConnectOutCount;
			}
			set
			{
				this.iConnectOutCount=value;
			}
		}

		/// <summary>
		/// 节点名称
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
		/// 节点连入时间
		/// </summary>
		public DateTime InFolwTime
		{
			get
			{
				return this.dt_inFlowTime;
			}
			set
			{
				this.dt_inFlowTime=value;
			}
		}

		/// <summary>
		/// 节点连出时间
		/// </summary>
		public DateTime OutFlowTime
		{
			get
			{
				return this.dt_outFlowTime;
			}
			set
			{
				this.dt_outFlowTime=value;
			}
		}

		/// <summary>
		/// 功能说明
		/// </summary>
		public string Function
		{
			get
			{
				return this.str_Function;
			}
			set
			{
				this.str_Function=value;
			}
		}

		/// <summary>
		/// 业务角色
		/// </summary>
		public string OperationRole
		{
			get
			{
				return this.str_OperationRole;
			}
			set
			{
				this.str_OperationRole=value;
			}
		}

		/// <summary>
		/// 功能说明
		/// </summary>
		public string FunctionInfo
		{
			get
			{
				return this.str_FunctionInfo;
			}
			set
			{
				this.str_FunctionInfo=value;
			}
		}
		
		/// <summary>
		/// 详细说明
		/// </summary>
		public string Info
		{
			get
			{
				return this.str_Info;
			}
			set
			{
				this.str_Info=value;
			}
		}
		/// <summary>
		///  获取或设定流入节点的ID
		/// </summary>
		public ArrayList InFlowNodeID
		{
			get
			{
				return this.arr_InFlowNodeID;
			}
			set
			{
				this.arr_InFlowNodeID=value;
			}
		}
		/// <summary>
		/// 获取或设定流出节点的ID
		/// </summary>
		public ArrayList OutFlowNodeID
		{
			get
			{
				return this.arr_OutFlowNodeID;
			}
			set
			{
				this.arr_OutFlowNodeID=value;
			}
		}
	}
}
