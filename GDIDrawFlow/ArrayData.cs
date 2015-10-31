using System;
using System.Collections;


namespace GDIDrawFlow
{
	/// <summary>
	/// ArrayData 的摘要说明。
	/// </summary>
	/// 
	[Serializable()]
	public class ArrayData
	{
		public  ArrayList arrLineList;//线的数组
		public  ArrayList arrNodeList;//节点的数组
		public  ArrayList arrDrawStringList;//写字板的数组
		
		public  ArrayList arrLineSelectList;//线的数组
		public  ArrayList arrNodeSelectList;//节点的数组
		public  ArrayList arrDrawStringSelectList;//写字板的数组	

		public  ArrayList arrLineNotSelectList;//没有被选择的线的数组
		public  ArrayList arrNodeNotSelectList;//没有被选择的节点的数组
		public  ArrayList arrDrawStringNotSelectList;//没有被选择的写字板的数组

		public  ArrayList arrLineConnectNode;//连接到活动节点的线	

		public ArrayData()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//

			this.arrLineList=new ArrayList();
			this.arrNodeList=new ArrayList();
			this.arrDrawStringList=new ArrayList();

			this.arrLineSelectList=new ArrayList();
			this.arrNodeSelectList=new ArrayList();
			this.arrDrawStringSelectList=new ArrayList();

			this.arrLineNotSelectList=new ArrayList();
			this.arrNodeNotSelectList=new ArrayList();
			this.arrDrawStringNotSelectList=new ArrayList();

			this.arrLineConnectNode=new ArrayList();
		}
	}
}
