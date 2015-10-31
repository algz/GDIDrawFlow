using System;
using System.Drawing;


namespace GDIDrawFlow
{
	/// <summary>
	/// GetTypeImage 的摘要说明。
	/// </summary>
	/// 
	[Serializable()]
	public class GetTypeImage
	{
		private Bitmap startNode;
		private Bitmap generalNode;
		private Bitmap specificallyOperationNode;
		private Bitmap gradationNode;
		private Bitmap synchronizationNode;
		private Bitmap asunderNode;
		private Bitmap convergeNode;
		private Bitmap gatherNode;
		private Bitmap judgementNode;
		private Bitmap DataNode;
		private Bitmap endNode;
		public GetTypeImage()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
			this.startNode=new Bitmap(GetType(),"images.startNode.png");
			this.generalNode=new Bitmap(GetType(),"images.generalNode.png");
			this.specificallyOperationNode=new Bitmap(GetType(),"images.specificallyOperationNode.png");
			this.gradationNode =new Bitmap(GetType(),"images.gradationNode.png");
			this.synchronizationNode =new Bitmap(GetType(),"images.synchronizationNode.png");
			this.asunderNode=new Bitmap(GetType(),"images.asunderNode.png");	
			this.convergeNode=new Bitmap(GetType(),"images.convergeNode.png");	
			this.gatherNode =new Bitmap(GetType(),"images.gatherNode.png");
			this.judgementNode=new Bitmap(GetType(),"images.judgementNode.png");
			this.DataNode=new Bitmap(GetType(),"images.DataNode.png");
			this.endNode=new Bitmap(GetType(),"images.endNode.png");
		}
		public Bitmap GetImage(Node.DrawObjectType type)
		{
			switch(type)
			{
				case Node.DrawObjectType.DrawNodeBegin:
					return	this.startNode;
				case Node.DrawObjectType.DrawNodeGeneral:
					return this.generalNode;
				case Node.DrawObjectType.DrawSpecificallyOperation:
					return this.specificallyOperationNode;
				case Node.DrawObjectType.DrawGradation:
					return this.gradationNode;
				case Node.DrawObjectType.DrawSynchronization:
					return this.synchronizationNode;
				case Node.DrawObjectType.DrawAsunder:
					return  this.asunderNode;
				case Node.DrawObjectType.DrawConverge:
					return this.convergeNode;
				case Node.DrawObjectType.DrawGather:
					return this.gatherNode;
				case Node.DrawObjectType.DrawJudgement:
					return this.judgementNode;
				case Node.DrawObjectType.DrawDataNode:
					return this.DataNode;
				case Node.DrawObjectType.DrawNodeEnd:
					return this.endNode;
				case Node.DrawObjectType.DrawRectangle:
					return null;
				case Node.DrawObjectType.DrawEllipse:
					return null;
			}
			return new Bitmap(GetType(),"images.Rectangle.png");
		}
	}
}
