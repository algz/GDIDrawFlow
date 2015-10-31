using System;
using System.Collections;
using System.Xml;
using System.Windows.Forms;
using System.Drawing;

namespace GDIDrawFlow
{
	/// <summary>
	/// 处理服务器的XML文件,将其画出
	/// </summary>
	public class MakeServerXml
	{

		private string serverXml="";		
		ArrayList arrNodeList;
		ArrayList arrLineList;
        ArrayList arrStringList;
		public MakeServerXml(string xmlInfo)
		{
			serverXml=xmlInfo;
		}

		/// <summary>
		/// 从服务器的XML文件中，获取流程图数组
		/// </summary>
		public void CreateArrDraw()
		{
			arrNodeList=new ArrayList ();
			arrLineList=new ArrayList ();
			arrStringList=new ArrayList();
			
			XmlDocument doc=new XmlDocument ();
			doc.LoadXml(serverXml);
			
			//获取结点列表
			XmlNodeList nodeChilds=doc.SelectNodes("//arrNodeList//node");
			
			foreach (XmlNode node in nodeChilds)
			{
				XmlNodeList xnl=node.ChildNodes;
				int index=Convert.ToInt32(node.Attributes.Item(0).InnerText);
				int nodetype=Convert.ToInt32(node.Attributes.Item(1).InnerText);
				string nodetext=xnl.Item(0).InnerText;
				Node xmlnode=new Node (index,(Node.DrawObjectType)nodetype,nodetext);
				string fontname=xnl.Item(1).InnerText;
				int fontSize=Convert.ToInt32(xnl.Item(2).InnerText);
				xmlnode.NodeTextFont=new System.Drawing.Font (fontname,fontSize);
				xmlnode.TextSize=fontSize;
				xmlnode.NodeTextColor=System.Drawing.Color.FromArgb(int.Parse(xnl.Item(3).InnerText));
				xmlnode.BorderColor=Color.FromArgb(int.Parse(xnl.Item(4).InnerText));
				xmlnode.FillColor=Color.FromArgb(int.Parse(xnl.Item(5).InnerText));
				xmlnode.Height=Convert.ToInt32(xnl.Item(6).InnerText);
				xmlnode.Width=Convert.ToInt32(xnl.Item(7).InnerText);
				xmlnode.X=Convert.ToInt32(xnl.Item(8).InnerText);
				xmlnode.Y=Convert.ToInt32(xnl.Item(9).InnerText);
				xmlnode.ConnectInCount=Convert.ToInt32(xnl.Item(10).InnerText);
				xmlnode.ConnectOutCount=Convert.ToInt32(xnl.Item(11).InnerText);
				xmlnode.InFolwTime=Convert.ToDateTime(xnl.Item(12).InnerText);
				xmlnode.OutFlowTime=Convert.ToDateTime(xnl.Item(13).InnerText);
				xmlnode.Function=xnl.Item(14).InnerText;
				xmlnode.OperationRole=xnl.Item(15).InnerText;
				xmlnode.FunctionInfo=xnl.Item(16).InnerText;
				xmlnode.Info=xnl.Item(17).InnerText;
				arrNodeList.Add(xmlnode);
			}

			//获取线列表
			XmlNodeList lineChilds=doc.SelectNodes("//arrLineList//line");
			foreach (XmlNode node in lineChilds)
			{
				XmlNodeList xnl=node.ChildNodes;

				int index=Convert.ToInt32(node.Attributes.Item(0).InnerText);
				int lineType=Convert.ToInt32(node.Attributes.Item(1).InnerText);
				
				Line line=new Line ((Line.DrawObjectType)lineType,index);
				line.X0=Convert.ToInt32(xnl.Item(0).InnerText);
				line.Y0=Convert.ToInt32(xnl.Item(1).InnerText);
				line.X1=Convert.ToInt32(xnl.Item(2).InnerText);
				line.Y1=Convert.ToInt32(xnl.Item(3).InnerText);	
				
				if(lineType==1)
				{
					line.X2=Convert.ToInt32(xnl.Item(4).InnerText);
					line.Y2=Convert.ToInt32(xnl.Item(5).InnerText);
					line.X3=Convert.ToInt32(xnl.Item(6).InnerText);
					line.Y3=Convert.ToInt32(xnl.Item(7).InnerText);
					line.LineNodeCount=Convert.ToInt32(xnl.Item(8).InnerText);	
					if(xnl.Item(9).InnerText=="null")
					{
						line.FirstNode=null;
					}
					else
					{
						int firstIndex=Convert.ToInt32(xnl.Item(9).InnerText);
						line.FirstNode=(Node)arrNodeList[firstIndex];
					}
			
					if(xnl.Item(10).InnerText=="null")
					{
						line.SecondNode=null;
					}
					else
					{
						int secondIndex=Convert.ToInt32(xnl.Item(10).InnerText);
						line.SecondNode=(Node)arrNodeList[secondIndex];
					}
					line.FirNodeInterfaceIndex=Convert.ToInt32(xnl.Item(11).InnerText);
					line.SecNodeInterfaceIndex=Convert.ToInt32(xnl.Item(12).InnerText);
					line.LineSize=Convert.ToInt32(xnl.Item(13).InnerText);
					line.LineColor=System.Drawing.Color.FromArgb(int.Parse(xnl.Item(14).InnerText));
					line.Content=xnl.Item(15).InnerText;
					arrLineList.Add(line);
				}
				else if(lineType==0)
				{
					line.LineNodeCount=Convert.ToInt32(xnl.Item(4).InnerText);
					if(xnl.Item(5).InnerText=="null")
					{
						line.FirstNode=null;
					}
					else
					{
						line.FirstNode=(Node)arrNodeList[Convert.ToInt32(xnl.Item(5).InnerText)];
					}
					if(xnl.Item(6).InnerText=="null")
					{
						line.SecondNode=null;
					}
					else
					{
						line.SecondNode=(Node)arrNodeList[Convert.ToInt32(xnl.Item(6).InnerText)];
					}
					line.FirNodeInterfaceIndex=Convert.ToInt32(xnl.Item(7).InnerText);
					line.SecNodeInterfaceIndex=Convert.ToInt32(xnl.Item(8).InnerText);
					line.LineSize=Convert.ToInt32(xnl.Item(9).InnerText);
					line.LineColor=System.Drawing.Color.FromArgb(int.Parse(xnl.Item(10).InnerText));
					line.Content=xnl.Item(11).InnerText;
					arrLineList.Add(line);
                    
				}

		
			}

			//获取说明文字
			XmlNodeList textChilds=doc.SelectNodes("//arrStringList//textString");
			foreach (XmlNode node in textChilds)
			{
				XmlNodeList xnl=node.ChildNodes;

				int index=Convert.ToInt32(node.Attributes.Item(0).InnerText);
				DrawString ds=new DrawString (index);
				ds.Content=xnl.Item(0).InnerText;
				ds.X=Convert.ToInt32(xnl.Item(1).InnerText);
				ds.Y=Convert.ToInt32(xnl.Item(2).InnerText);
				ds.Height=Convert.ToInt32(xnl.Item(3).InnerText );
				ds.Width=Convert.ToInt32(xnl.Item(4).InnerText);
				ds.DSTextColor=System.Drawing.Color.FromArgb(int.Parse(xnl.Item(5).InnerText));
				string fontName=xnl.Item(6).InnerText;
				int fontSize=Convert.ToInt32(xnl.Item(7).InnerText);
				ds.DSTextFont=new System.Drawing.Font (fontName,fontSize);
				ds.TextSize=fontSize;
				arrStringList.Add(ds);
		
			}

		}


		public ArrayList NodeList
		{
			get{return arrNodeList;}
		}
		public ArrayList LineList
		{
			get{return arrLineList;}
		}
		public ArrayList TextList
		{
			get{return arrStringList;}
		}
	}
}
