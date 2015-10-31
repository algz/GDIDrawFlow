using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace GDIDrawFlow
{
	/// <summary>
	/// SelectPoint 的摘要说明。
	/// </summary>
	/// 
	[Serializable()] 
	public class SelectPoint
	{
		private Point[] pRectanglePoint=new Point[8];
		private Point[] pLinePoint=new Point[4];
		public SelectPoint()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public Point[] RectanglePoint
		{
			get
			{
				return this.pRectanglePoint;
			}
			set
			{
				this.pRectanglePoint=value;
			}
		}

		public Point[] LinePoint
		{
			get
			{
				return this.pLinePoint;
			}
			set
			{
				this.pLinePoint=value;
			}
		}

		public void SetRectanglePoint(int x,int y,int width,int height)
		{
			this.pRectanglePoint[0].X=x-3;
			this.pRectanglePoint[0].Y=y-5;
			this.pRectanglePoint[1].X=x+width/2;
			this.pRectanglePoint[1].Y=y-5;
			this.pRectanglePoint[2].X=x+width+3;
			this.pRectanglePoint[2].Y=y-5;
			this.pRectanglePoint[3].X=x-3;
			this.pRectanglePoint[3].Y=y+height/2-2;
			this.pRectanglePoint[4].X=x+width+3;
			this.pRectanglePoint[4].Y=y+height/2-2;
			this.pRectanglePoint[5].X=x-3;
			this.pRectanglePoint[5].Y=y+height;
			this.pRectanglePoint[6].X=x+width/2;
			this.pRectanglePoint[6].Y=y+height;
			this.pRectanglePoint[7].X=x+width+3;
			this.pRectanglePoint[7].Y=y+height;
		}

		public void AddDataRectanglePoint(int width,int height)
		{
			this.pRectanglePoint[0].X+=width;
			this.pRectanglePoint[0].Y+=height;
			this.pRectanglePoint[1].X+=width;
			this.pRectanglePoint[1].Y+=height;
			this.pRectanglePoint[2].X+=width;
			this.pRectanglePoint[2].Y+=height;
			this.pRectanglePoint[3].X+=width;
			this.pRectanglePoint[3].Y+=height;
			this.pRectanglePoint[4].X+=width;
			this.pRectanglePoint[4].Y+=height;
			this.pRectanglePoint[5].X+=width;
			this.pRectanglePoint[5].Y+=height;
			this.pRectanglePoint[6].X+=width;
			this.pRectanglePoint[6].Y+=height;
			this.pRectanglePoint[7].X+=width;
			this.pRectanglePoint[7].Y+=height;			
		}

		public void SetLinePoint(int x0,int y0,int x1,int y1)
		{
			this.pLinePoint[0].X=x0;
			this.pLinePoint[0].Y=y0;
			this.pLinePoint[1].X=x1;
			this.pLinePoint[1].Y=y1;
		}

		public void SetFlodLinePoint(Line line)
		{
			for(int i=0;i<4;i++)
			{
				this.pLinePoint[i].X=line.GetLineNodeInfo(i*2);
				this.pLinePoint[i].Y=line.GetLineNodeInfo(i*2+1);
			}
		}
		
		public void AddLinePoint(int width,int height)
		{
			this.pLinePoint[0].X+=width;
			this.pLinePoint[0].Y+=height;
			this.pLinePoint[1].X+=width;
			this.pLinePoint[1].Y+=height;
		}

		public void AddFlodLinePoint(int width,int height)
		{
			this.pLinePoint[0].X+=width;
			this.pLinePoint[0].Y+=height;
			this.pLinePoint[1].X+=width;
			this.pLinePoint[1].Y+=height;
			this.pLinePoint[2].X+=width;
			this.pLinePoint[2].Y+=height;
			this.pLinePoint[3].X+=width;
			this.pLinePoint[3].Y+=height;
		}
	}
}
