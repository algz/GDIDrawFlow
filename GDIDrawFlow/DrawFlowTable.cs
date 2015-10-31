using System;
using System.Data;
using System.Collections;


namespace GDIDrawFlow
{
	/// <summary>
	/// DrawFlowTable 的摘要说明。
	/// </summary>
	public class DrawFlowTable
	{
		DrawObject drawObject;
		public DrawFlowTable(DrawObject indrawObject)
		{
			drawObject=indrawObject;
			flowTable.Columns.Add (colName);
			colName.MaxLength=2000;
			flowTable.Columns.Add(colInNode);
			flowTable.Columns.Add (colOutNode);
			flowTable.Columns.Add(colFunction);
			flowTable.Columns.Add (colRole);
			flowTable.Columns.Add(colInfo);
			nodeList=drawObject.NodeList;
		}
		DataTable flowTable=new DataTable ("FlowTable");
		DataColumn colName=new DataColumn ("节点名称");
		DataColumn colInNode=new DataColumn ("前驱结点");
		DataColumn colOutNode=new DataColumn ("后继结点");
		DataColumn colFunction=new DataColumn ("实现功能");
		DataColumn colRole=new DataColumn ("用户角色");
		DataColumn colInfo=new DataColumn ("功能说明");
		ArrayList nodeInList=new ArrayList ();
		ArrayList nodeOutList=new ArrayList ();
		ArrayList nodeList;
		public DataTable CreateFlowTable()
		{
			this.flowTable.Rows.Clear();
			nodeList=drawObject.NodeList;
			string outNode="";	  
			for(int i=0;i<nodeList.Count;i++)
			{
				Node node=(Node)nodeList[i];
				nodeInList=node.InFlowNodeID; //前驱ID数组
				nodeOutList=node.OutFlowNodeID;//后继ID数组
				int nodeInID;
				int nodeOutID;
				if(nodeInList.Count>0)
				{
					for(int j=0;j<nodeInList.Count;j++)
					{
						outNode="";
						DataRow rowIn=flowTable.NewRow();
						nodeInID=(int)nodeInList[j];
						Node nodeInTemp=(Node)nodeList[nodeInID];
						rowIn["节点名称"]=node.NodeText;
						rowIn["前驱结点"]=nodeInTemp.NodeText;
						rowIn["实现功能"]=node.Function;
						rowIn["用户角色"]=node.OperationRole;
						rowIn["功能说明"]=node.FunctionInfo;
						for(int k=0;k<nodeOutList.Count;k++)
						{
							nodeOutID=(int)nodeOutList[k];
							Node nodeOutTemp=(Node)nodeList[nodeOutID];
							outNode+=nodeOutTemp.NodeText+",";
						}
						if(outNode.Length>0 && outNode.Substring(outNode.Length-1)==",")
						{
							outNode=outNode.Remove(outNode.Length-1,1);
						}
						rowIn["后继结点"]=outNode;
					
						flowTable.Rows.Add (rowIn);
					}
				}
				else
				{
					for(int j=0;j<nodeOutList.Count;j++)
					{
						outNode="";
						DataRow rowOut=flowTable.NewRow();
						nodeInID=(int)nodeOutList[j];
						Node nodeOutTemp=(Node)nodeList[nodeInID];
						rowOut["节点名称"]=node.NodeText;
						rowOut["后继结点"]=nodeOutTemp.NodeText;
						rowOut["实现功能"]=node.Function;
						rowOut["用户角色"]=node.OperationRole;
						rowOut["功能说明"]=node.FunctionInfo;
						for(int k=0;k<nodeInList.Count;k++)
						{
							nodeOutID=(int)nodeInList[k];
							Node nodeInTemp=(Node)nodeList[nodeOutID];
							outNode+=nodeInTemp.NodeText+",";
						}
						//outNode=outNode.Remove(outNode.Length-1,1);
						rowOut["前驱结点"]=outNode;
					
						flowTable.Rows.Add (rowOut);
					}
				}
			}
			return flowTable;

		}
	}
}
