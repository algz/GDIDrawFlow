using System;
using System.Windows.Forms;

namespace GDIDrawFlow
{
	/// <summary>
	/// GetTypeCursor 的摘要说明。
	/// </summary>
	public class GetTypeCursor
	{
		Cursor Begin_Cursor;
		Cursor General_Cursor;
		Cursor SpecificallyOperation_Cursor;
		Cursor Gradation_Cursor;
		Cursor Synchronization_Cursor;
		Cursor Asunder_Cursor;
		Cursor Converge_Cursor;
		Cursor Gather_Cursor;
		Cursor Judgement_Cursor;
		Cursor Data_Cursor;
		Cursor End_Cursor;

		Cursor Rectangle_Cursor;
		Cursor Ellipse_Cursor;
		Cursor BeeLine_Cursor;
		Cursor FoldLine_Cursor;
		Cursor String_Cursor;

		public GetTypeCursor()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
			this.Begin_Cursor=new Cursor(GetType(),"Cursors.Start.cur");
			this.General_Cursor=new Cursor(GetType(),"Cursors.General.cur");
			this.SpecificallyOperation_Cursor=new Cursor(GetType(),"Cursors.SpecificallyOperation.cur");
			this.Gradation_Cursor=new Cursor(GetType(),"Cursors.Gradation.cur");
			this.Synchronization_Cursor=new Cursor(GetType(),"Cursors.Synchronization.cur");
			this.Converge_Cursor=new Cursor(GetType(),"Cursors.Converge.cur");
			this.Asunder_Cursor=new Cursor(GetType(),"Cursors.Asunder.cur");
			this.Gather_Cursor=new Cursor(GetType(),"Cursors.Gather.cur");
			this.Judgement_Cursor=new Cursor(GetType(),"Cursors.Judgement.cur");
			this.Data_Cursor=new Cursor(GetType(),"Cursors.Data.cur");
			this.End_Cursor=new Cursor(GetType(),"Cursors.end.cur");

			this.Rectangle_Cursor=new Cursor(GetType(),"Cursors.Rectangle.cur");
			this.Ellipse_Cursor=new Cursor(GetType(),"Cursors.Ellipse.cur");
			this.BeeLine_Cursor=new Cursor(GetType(),"Cursors.Line.cur");
			this.FoldLine_Cursor=new Cursor(GetType(),"Cursors.FlodLine.cur");
			this.String_Cursor=new Cursor(GetType(),"Cursors.Pencil.cur");
		}

		/// <summary>
		/// 所画图元的类型
		/// </summary>
		public enum DrawType
		{
			/// <summary>
			/// 开始
			/// </summary>
			NodeBegin,
			/// <summary>
			/// 节点
			/// </summary>
			NodeGeneral,
			/// <summary>
			/// 特定操作
			/// </summary>
			SpecificallyOperation,
			/// <summary>
			/// 顺序
			/// </summary>
			Gradation,
			/// <summary>
			/// 同步图元
			/// </summary>
			Synchronization,
			/// <summary>
			/// 分支
			/// </summary>
			Asunder,
			/// <summary>
			/// 汇聚
			/// </summary>
			Converge,
			/// <summary>
			/// 汇总连接
			/// </summary>
			Gather,
			/// <summary>
			/// 判断
			/// </summary>
			Judgement,
			/// <summary>
			/// 数据
			/// </summary>
			DataNode,
			/// <summary>
			/// 结束
			/// </summary>
			NodeEnd,
			/// <summary>
			/// 矩形
			/// </summary>
			Rectangle,
			/// <summary>
			/// 椭圆
			/// </summary>
			Eillpse,
			/// <summary>
			/// 直线
			/// </summary>
			BeeLine,
			/// <summary>
			/// 折线
			/// </summary>
			FoldLine,
			/// <summary>
			/// 写字板
			/// </summary>
			String
		}

		public Cursor GetCursor(GetTypeCursor.DrawType type)
		{
			switch(type)
			{
				case GetTypeCursor.DrawType.NodeBegin :
					return this.Begin_Cursor;
				case GetTypeCursor.DrawType.NodeGeneral :
					return this.General_Cursor;
				case GetTypeCursor.DrawType.SpecificallyOperation :
					return this.SpecificallyOperation_Cursor;
				case GetTypeCursor.DrawType.Gradation :
					return this.Gradation_Cursor;
				case GetTypeCursor.DrawType.Synchronization :
					return this.Synchronization_Cursor;
				case GetTypeCursor.DrawType.Asunder :
					return this.Asunder_Cursor;
				case GetTypeCursor.DrawType.Converge :
					return this.Converge_Cursor;
				case GetTypeCursor.DrawType.Gather :
					return this.Gather_Cursor;
				case GetTypeCursor.DrawType.Judgement :
					return this.Judgement_Cursor;
				case GetTypeCursor.DrawType.DataNode :
					return this.Data_Cursor;
				case GetTypeCursor.DrawType.NodeEnd :
					return this.End_Cursor;
				case GetTypeCursor.DrawType.Rectangle :
					return this.Rectangle_Cursor;
				case GetTypeCursor.DrawType.Eillpse :
					return this.Ellipse_Cursor;
				case GetTypeCursor.DrawType.BeeLine :
					return this.BeeLine_Cursor;
				case GetTypeCursor.DrawType.FoldLine :
					return this.FoldLine_Cursor;
				case GetTypeCursor.DrawType.String :
					return this.String_Cursor;
			}
			return Cursors.Default;
		}
	}
}
