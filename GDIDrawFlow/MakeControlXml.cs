using System;
using System.Xml;
using System.Collections;
namespace GDIDrawFlow
{
	/// <summary>
	/// MakeControlXml处理控件的XML信息
	/// </summary>
	public class MakeControlXml
	{
		DrawObject drawObject;
		public MakeControlXml(DrawObject indrawObject)
		{
			drawObject=indrawObject;
		}

		public string GetControlInfo()
		{
			ArrayList arrNodeList=drawObject.NodeList;
			ArrayList arrLineList=drawObject.LineList;
			ArrayList arrStringList=drawObject.StringList;
			string controlXml="<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n";
			controlXml+="<WorkFlow>\r\n";
			controlXml+="  <arrNodeList>\r\n";
			for(int i=0;i<arrNodeList.Count;i++)
			{
				Node node=(Node)arrNodeList[i];
				
				controlXml+="    <node nodeID='"+node.NodeListIndex+"' nodeType='"+(int)node.ObjectType+"'>\r\n"+
					"      <nodeText><![CDATA["+node.NodeText.Replace("<","<")+"]]></nodeText>\r\n"+
					"      <nodeFontName>"+node.NodeTextFont.Name+"</nodeFontName>\r\n"+
					"      <nodeFontSize>"+node.TextSize+"</nodeFontSize>\r\n"+
					"      <nodeTextColor>"+node.NodeTextColor.ToArgb()+"</nodeTextColor>\r\n"+
					"      <nodeBorderColor>"+node.BorderColor.ToArgb()+"</nodeBorderColor>\r\n"+
					"      <nodeFillColor>"+node.FillColor.ToArgb()+"</nodeFillColor>\r\n"+
					"      <nodeHeight>"+node.Height+"</nodeHeight>\r\n"+
					"      <nodeWidth>"+node.Width+"</nodeWidth>\r\n"+
					"      <nodeX>"+node.X+"</nodeX>\r\n"+
					"      <nodeY>"+node.Y+"</nodeY>\r\n"+
					"      <nodeInCount>"+node.ConnectInCount+"</nodeInCount>\r\n"+
					"      <nodeOutCount>"+node.ConnectOutCount+"</nodeOutCount>\r\n"+
					"      <nodeInFlowTime>"+node.InFolwTime.ToString()+"</nodeInFlowTime>\r\n"+
					"      <nodeOutFlowTime>"+node.OutFlowTime.ToString()+"</nodeOutFlowTime>\r\n"+
					"      <nodeFunction><![CDATA["+node.Function.Replace("<","<")+"]]></nodeFunction>\r\n"+
					"      <nodeOperationRole><![CDATA["+node.OperationRole.Replace("<","<")+"]]></nodeOperationRole>\r\n"+
					"      <nodeFunctionInfo><![CDATA["+node.FunctionInfo.Replace("<","<")+"]]></nodeFunctionInfo>\r\n"+
					"      <nodeInfo><![CDATA["+node.Info.Replace("<","<")+"]]></nodeInfo>\r\n"+
					"    </node>\r\n";
				
			}
			controlXml+="  </arrNodeList>\r\n  <arrLineList>";
			
			string firstIndex="";
			string secondIndex;
			for(int i=0;i<arrLineList.Count;i++)
			{
				Line line=(Line)arrLineList[i];
				if(line.FirstNode!=null ){firstIndex=Convert.ToString(line.FirstNode.NodeListIndex);}
				else{firstIndex="null";}
				if(line.SecondNode!=null){secondIndex=Convert.ToString(line.SecondNode.NodeListIndex);}
				else{secondIndex="null";}
				
				if(line.LineNodeCount==4)
				{
					controlXml+="    <line lineID='"+line.LineListIndex+"' lineType='"+(int)line.ObjectType+"'>\r\n"+
						"      <X0>"+line.X0+"</X0>\r\n"+
						"      <Y0>"+line.Y0+"</Y0>\r\n"+
						"      <X1>"+line.X1+"</X1>\r\n"+
						"      <Y1>"+line.Y1+"</Y1>\r\n";
				}
				else if(line.LineNodeCount==8)
				{
					controlXml+="    <line lineID='"+line.LineListIndex+"' lineType='"+(int)line.ObjectType+"'>\r\n"+
						"      <X0>"+line.X0+"</X0>\r\n"+
						"      <Y0>"+line.Y0+"</Y0>\r\n"+
						"      <X1>"+line.X1+"</X1>\r\n"+
						"      <Y1>"+line.Y1+"</Y1>\r\n"+
						"      <X2>"+line.X2+"</X2>\r\n"+
						"      <Y2>"+line.Y2+"</Y2>\r\n"+
						"      <X3>"+line.X3+"</X3>\r\n"+
						"      <Y3>"+line.Y3+"</Y3>\r\n";
				}
				controlXml+="      <lineNodeCount>"+line.LineNodeCount+"</lineNodeCount>\r\n"+
					"      <lineFirstNode>"+firstIndex+"</lineFirstNode>\r\n"+
					"      <lineSecondNode>"+secondIndex+"</lineSecondNode>\r\n"+
					"      <lineFirstNodeIndex>"+line.FirNodeInterfaceIndex+"</lineFirstNodeIndex>\r\n"+
					"      <lineSecondNodeIndex>"+line.SecNodeInterfaceIndex+"</lineSecondNodeIndex>\r\n"+
					"      <lineSize>"+line.LineSize+"</lineSize>\r\n"+
					"      <lineColor>"+line.LineColor.ToArgb()+"</lineColor>\r\n"+
					"      <lineText><![CDATA["+line.Content+"]]></lineText>\r\n"+
					"    </line>\r\n";
			}
			controlXml+="  </arrLineList>\r\n  <arrStringList>";

			string contentTemp="";
			for(int i=0;i<arrStringList.Count;i++)
			{
				DrawString ds=(DrawString)arrStringList[i];
				contentTemp="<![CDATA["+ds.Content.Replace("<","<")+"]]>\r\n";
				controlXml+="    <textString textID='"+ds.DrawStrListIndex+"'>\r\n"+
					"      <textContent>"+contentTemp+"</textContent>\r\n"+
					"      <textX>"+ds.X+"</textX>\r\n"+
					"      <textY>"+ds.Y+"</textY>\r\n"+
					"      <textHeight>"+ds.Height+"</textHeight>\r\n"+
					"      <textWidth>"+ds.Width+"</textWidth>\r\n"+
					"      <textColor>"+ds.DSTextColor.ToArgb()+"</textColor>\r\n"+
					"      <textFontName>"+ds.DSTextFont.Name+"</textFontName>\r\n"+
					"      <textFontSize>"+ds.DSTextFont.Size+"</textFontSize>\r\n"+
					"    </textString>\r\n";
			}
			controlXml+="  </arrStringList>\r\n";
			controlXml+="</WorkFlow>";
			return controlXml;			
		}

	}
}
