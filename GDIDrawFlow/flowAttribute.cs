using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace GDIDrawFlow
{
	/// <summary>
	/// flowAttribute 的摘要说明。
	/// </summary>
	public class flowAttribute : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox tb_nodeName;
		private System.Windows.Forms.DateTimePicker dtp_inflowTime;
		private System.Windows.Forms.ComboBox cb_inflowNode;
		private System.Windows.Forms.TextBox tb_function;
		private System.Windows.Forms.DateTimePicker dtp_outflowTime;
		private System.Windows.Forms.ComboBox cb_outflowNode;
		private System.Windows.Forms.TextBox tb_operationRole;
		private System.Windows.Forms.TextBox tb_info;
		private System.Windows.Forms.TextBox tb_functionInfo;
		private System.Windows.Forms.Button btn_Ok;
		private System.Windows.Forms.Button btn_cancel;
		private Node node;//传入的编辑的节点
		private DrawFlowControl control;
		private System.Windows.Forms.ComboBox cb_nodeType;//主父框架
		private DateTime dt_TempDateTime;//记录就要被修改的值
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TextBox tb_InConnectCondition;//流入节点的关系名称
		private System.Windows.Forms.TextBox tb_OutConnectCondition;//流出节点的关系名称 
		private ArrayList arr_InFlowLineList;//连入节点的名称
		private ArrayList arr_OutFlowLineList;//连出节点的名称
		private string str_NodeTypeSeleBefor;//之前选择定的节点类型

		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public flowAttribute(Node node,DrawFlowControl control)
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();
	
			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
			this.node=node;
			this.control=control;
			this.arr_InFlowLineList=new ArrayList();
			this.arr_OutFlowLineList=new ArrayList();
			this.FormInterfaceInit();
			this.FormContentInit();
			this.BackgroundImage=new Bitmap(GetType(),"images.attribute.png");
		}

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.label5 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.tb_functionInfo = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.tb_function = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.tb_info = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.tb_nodeName = new System.Windows.Forms.TextBox();
			this.tb_operationRole = new System.Windows.Forms.TextBox();
			this.btn_Ok = new System.Windows.Forms.Button();
			this.btn_cancel = new System.Windows.Forms.Button();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.dtp_inflowTime = new System.Windows.Forms.DateTimePicker();
			this.dtp_outflowTime = new System.Windows.Forms.DateTimePicker();
			this.cb_inflowNode = new System.Windows.Forms.ComboBox();
			this.cb_outflowNode = new System.Windows.Forms.ComboBox();
			this.cb_nodeType = new System.Windows.Forms.ComboBox();
			this.label11 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.tb_InConnectCondition = new System.Windows.Forms.TextBox();
			this.tb_OutConnectCondition = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.BackColor = System.Drawing.Color.Transparent;
			this.label5.Location = new System.Drawing.Point(46, 154);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(60, 17);
			this.label5.TabIndex = 9;
			this.label5.Text = "后继结点:";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.BackColor = System.Drawing.Color.Transparent;
			this.label8.Location = new System.Drawing.Point(46, 226);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(60, 17);
			this.label8.TabIndex = 13;
			this.label8.Text = "功能说明:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.BackColor = System.Drawing.Color.Transparent;
			this.label4.Location = new System.Drawing.Point(46, 118);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(60, 17);
			this.label4.TabIndex = 7;
			this.label4.Text = "前驱结点:";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.BackColor = System.Drawing.Color.Transparent;
			this.label7.Location = new System.Drawing.Point(46, 190);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(60, 17);
			this.label7.TabIndex = 12;
			this.label7.Text = "实现功能:";
			// 
			// tb_functionInfo
			// 
			this.tb_functionInfo.Location = new System.Drawing.Point(114, 226);
			this.tb_functionInfo.Multiline = true;
			this.tb_functionInfo.Name = "tb_functionInfo";
			this.tb_functionInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.tb_functionInfo.Size = new System.Drawing.Size(304, 38);
			this.tb_functionInfo.TabIndex = 10;
			this.tb_functionInfo.Text = "";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.BackColor = System.Drawing.Color.Transparent;
			this.label6.Location = new System.Drawing.Point(46, 284);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(60, 17);
			this.label6.TabIndex = 10;
			this.label6.Text = "详细说明:";
			// 
			// tb_function
			// 
			this.tb_function.Location = new System.Drawing.Point(114, 186);
			this.tb_function.Name = "tb_function";
			this.tb_function.Size = new System.Drawing.Size(110, 21);
			this.tb_function.TabIndex = 8;
			this.tb_function.Text = "";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.BackColor = System.Drawing.Color.Transparent;
			this.label3.Location = new System.Drawing.Point(242, 190);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(60, 17);
			this.label3.TabIndex = 4;
			this.label3.Text = "业务角色:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.Location = new System.Drawing.Point(242, 44);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(35, 17);
			this.label2.TabIndex = 2;
			this.label2.Text = "类型:";
			// 
			// tb_info
			// 
			this.tb_info.Location = new System.Drawing.Point(48, 304);
			this.tb_info.Multiline = true;
			this.tb_info.Name = "tb_info";
			this.tb_info.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.tb_info.Size = new System.Drawing.Size(370, 34);
			this.tb_info.TabIndex = 11;
			this.tb_info.Text = "";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Location = new System.Drawing.Point(46, 44);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 17);
			this.label1.TabIndex = 0;
			this.label1.Text = "名称:";
			// 
			// tb_nodeName
			// 
			this.tb_nodeName.Location = new System.Drawing.Point(114, 40);
			this.tb_nodeName.Name = "tb_nodeName";
			this.tb_nodeName.Size = new System.Drawing.Size(110, 21);
			this.tb_nodeName.TabIndex = 0;
			this.tb_nodeName.Text = "";
			// 
			// tb_operationRole
			// 
			this.tb_operationRole.Location = new System.Drawing.Point(306, 186);
			this.tb_operationRole.Name = "tb_operationRole";
			this.tb_operationRole.Size = new System.Drawing.Size(112, 21);
			this.tb_operationRole.TabIndex = 9;
			this.tb_operationRole.Text = "";
			// 
			// btn_Ok
			// 
			this.btn_Ok.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btn_Ok.Location = new System.Drawing.Point(444, 38);
			this.btn_Ok.Name = "btn_Ok";
			this.btn_Ok.TabIndex = 12;
			this.btn_Ok.Text = "确定";
			this.btn_Ok.Click += new System.EventHandler(this.btn_Ok_Click);
			// 
			// btn_cancel
			// 
			this.btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btn_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btn_cancel.Location = new System.Drawing.Point(444, 76);
			this.btn_cancel.Name = "btn_cancel";
			this.btn_cancel.TabIndex = 13;
			this.btn_cancel.Text = "取消";
			this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.BackColor = System.Drawing.Color.Transparent;
			this.label9.Location = new System.Drawing.Point(242, 78);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(60, 17);
			this.label9.TabIndex = 19;
			this.label9.Text = "结束时间:";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.BackColor = System.Drawing.Color.Transparent;
			this.label10.Location = new System.Drawing.Point(46, 80);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(60, 17);
			this.label10.TabIndex = 18;
			this.label10.Text = "开始时间:";
			// 
			// dtp_inflowTime
			// 
			this.dtp_inflowTime.Location = new System.Drawing.Point(114, 76);
			this.dtp_inflowTime.Name = "dtp_inflowTime";
			this.dtp_inflowTime.Size = new System.Drawing.Size(110, 21);
			this.dtp_inflowTime.TabIndex = 2;
			this.dtp_inflowTime.DropDown += new System.EventHandler(this.Time_DropDown);
			this.dtp_inflowTime.CloseUp += new System.EventHandler(this.Time_ValueChanged);
			// 
			// dtp_outflowTime
			// 
			this.dtp_outflowTime.Location = new System.Drawing.Point(306, 76);
			this.dtp_outflowTime.Name = "dtp_outflowTime";
			this.dtp_outflowTime.Size = new System.Drawing.Size(112, 21);
			this.dtp_outflowTime.TabIndex = 3;
			this.dtp_outflowTime.DropDown += new System.EventHandler(this.Time_DropDown);
			this.dtp_outflowTime.CloseUp += new System.EventHandler(this.Time_ValueChanged);
			// 
			// cb_inflowNode
			// 
			this.cb_inflowNode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cb_inflowNode.Location = new System.Drawing.Point(114, 114);
			this.cb_inflowNode.Name = "cb_inflowNode";
			this.cb_inflowNode.Size = new System.Drawing.Size(110, 20);
			this.cb_inflowNode.TabIndex = 4;
			this.cb_inflowNode.TextChanged += new System.EventHandler(this.cb_inflowNode_TextChanged);
			this.cb_inflowNode.SelectedIndexChanged += new System.EventHandler(this.cb_inflowNode_SelectedIndexChanged);
			// 
			// cb_outflowNode
			// 
			this.cb_outflowNode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cb_outflowNode.Location = new System.Drawing.Point(114, 150);
			this.cb_outflowNode.Name = "cb_outflowNode";
			this.cb_outflowNode.Size = new System.Drawing.Size(110, 20);
			this.cb_outflowNode.TabIndex = 6;
			this.cb_outflowNode.SelectedIndexChanged += new System.EventHandler(this.cb_outflowNode_SelectedIndexChanged);
			// 
			// cb_nodeType
			// 
			this.cb_nodeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cb_nodeType.Items.AddRange(new object[] {
															 "开始",
															 "任务",
															 "特定操作",
															 "顺序",
															 "同步",
															 "分支",
															 "汇聚",
															 "汇总连接",
															 "判断",
															 "应用数据",
															 "结束",
															 "矩形",
															 "椭圆形"});
			this.cb_nodeType.Location = new System.Drawing.Point(306, 40);
			this.cb_nodeType.Name = "cb_nodeType";
			this.cb_nodeType.Size = new System.Drawing.Size(112, 20);
			this.cb_nodeType.TabIndex = 1;
			this.cb_nodeType.DropDown += new System.EventHandler(this.cb_nodeType_DropDown);
			this.cb_nodeType.SelectedIndexChanged += new System.EventHandler(this.cb_nodeType_SelectedIndexChanged);
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.BackColor = System.Drawing.Color.Transparent;
			this.label11.Location = new System.Drawing.Point(242, 118);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(60, 17);
			this.label11.TabIndex = 25;
			this.label11.Text = "连接条件:";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.BackColor = System.Drawing.Color.Transparent;
			this.label12.Location = new System.Drawing.Point(242, 154);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(60, 17);
			this.label12.TabIndex = 26;
			this.label12.Text = "连接条件:";
			// 
			// tb_InConnectCondition
			// 
			this.tb_InConnectCondition.Location = new System.Drawing.Point(306, 114);
			this.tb_InConnectCondition.Name = "tb_InConnectCondition";
			this.tb_InConnectCondition.Size = new System.Drawing.Size(112, 21);
			this.tb_InConnectCondition.TabIndex = 5;
			this.tb_InConnectCondition.Text = "";
			// 
			// tb_OutConnectCondition
			// 
			this.tb_OutConnectCondition.Location = new System.Drawing.Point(306, 150);
			this.tb_OutConnectCondition.Name = "tb_OutConnectCondition";
			this.tb_OutConnectCondition.Size = new System.Drawing.Size(112, 21);
			this.tb_OutConnectCondition.TabIndex = 7;
			this.tb_OutConnectCondition.Text = "";
			// 
			// flowAttribute
			// 
			this.AcceptButton = this.btn_Ok;
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.BackColor = System.Drawing.SystemColors.Control;
			this.CancelButton = this.btn_cancel;
			this.ClientSize = new System.Drawing.Size(554, 386);
			this.Controls.Add(this.tb_OutConnectCondition);
			this.Controls.Add(this.tb_InConnectCondition);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.tb_functionInfo);
			this.Controls.Add(this.tb_function);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.tb_info);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.tb_operationRole);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.tb_nodeName);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cb_nodeType);
			this.Controls.Add(this.cb_outflowNode);
			this.Controls.Add(this.cb_inflowNode);
			this.Controls.Add(this.dtp_outflowTime);
			this.Controls.Add(this.dtp_inflowTime);
			this.Controls.Add(this.btn_cancel);
			this.Controls.Add(this.btn_Ok);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.Name = "flowAttribute";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "属性";
			this.Load += new System.EventHandler(this.flowAttribute_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void flowAttribute_Load(object sender, System.EventArgs e)
		{
			
		}
		/// <summary>
		/// 详细说明 窗口是否可编辑
		/// </summary>
		public void FormInterfaceInit()
		{
			if(node.ObjectType !=Node.DrawObjectType.DrawNodeBegin)
			{
				this.tb_info.Enabled=false;
			}
		}
		/// <summary>
		/// 填充节点信息到窗口中 
		/// </summary>
		public void FormContentInit()
		{
			this.tb_nodeName.Text=node.NodeText;
			this.cb_nodeType.Text=DrawObject.getDefaultText(node.ObjectType);
			this.dtp_inflowTime.Text=""+node.InFolwTime;
			this.dtp_outflowTime.Text=""+node.OutFlowTime;
			this.FillInFlowNode();
			this.FillOutFlowNode();
			if(this.cb_inflowNode.Items.Count>0)
			{
				this.cb_inflowNode.SelectedItem=this.cb_inflowNode.Items[0];
			}
			else
			{
				this.tb_InConnectCondition.Enabled=false;	
			}
			if(this.cb_outflowNode.Items.Count>0)
			{
				this.cb_outflowNode.SelectedItem=this.cb_outflowNode.Items[0];
			}
			else
			{
				this.tb_OutConnectCondition.Enabled=false;
			}
			//			this.tb_InConnectCondition
			this.tb_function.Text=node.Function;
			this.tb_functionInfo.Text=node.FunctionInfo;
			this.tb_operationRole.Text=node.OperationRole;
			if(this.tb_info.Enabled!=false)
			{
				this.tb_info.Text=node.Info;
			}
		}
		/// <summary>
		/// 添加流入该节点的  其他节点
		/// </summary>
		public void FillInFlowNode()
		{
			Line line;
			for(int i=0;i<this.control.drawObject.arrLineList.Count;i++)
			{
				line=(Line)this.control.drawObject.arrLineList[i];
				if(line.SecondNode==node && line.FirstNode!=null)
				{
					this.cb_inflowNode.Items.Add(line.FirstNode.NodeText+"    ID:"+line.FirstNode.NodeListIndex);
					this.arr_InFlowLineList.Add(line);
				}
			}
		}
		/// <summary>
		/// 添加流出该节点的  其他节点
		/// </summary>
		public void FillOutFlowNode()
		{
			Line line;
			for(int i=0;i<this.control.drawObject.arrLineList.Count;i++)
			{
				line =(Line)this.control.drawObject.arrLineList[i];
				if(line.FirstNode==node && line.SecondNode!=null)
				{
					this.cb_outflowNode.Items.Add(line.SecondNode.NodeText+"    ID:"+line.SecondNode.NodeListIndex);
					this.arr_OutFlowLineList.Add(line);
				}
			}
		}
		/// <summary>
		/// 点击确定
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btn_Ok_Click(object sender, System.EventArgs e)
		{
			node.NodeText=this.tb_nodeName.Text;
			node.InFolwTime=this.dtp_inflowTime.Value;
			node.OutFlowTime=this.dtp_outflowTime.Value;
			node.Function=this.tb_function.Text;
			node.FunctionInfo=this.tb_functionInfo.Text;
			node.OperationRole=this.tb_operationRole.Text;
			node.Info=this.tb_info.Text;
			if(this.ChangeNodeType())
			{
				this.cb_nodeType.Text=this.str_NodeTypeSeleBefor;
				return;
			}
			this.ChangeLineContent();
			this.Close();
			this.Dispose();
			this.control.FormRefrash();
			this.control.drawObject.ReflashNodeIn_OutNodeID();
			this.control.drawObject.GetTableList();
		}
		/// <summary>
		/// 改变节点的类型
		/// </summary>
		public bool ChangeNodeType()		{			if(this.cb_nodeType.Text!=DrawObject.getDefaultText(node.ObjectType))			{				if(this.cb_nodeType.Text==DrawObject.getDefaultText(Node.DrawObjectType.DrawNodeGeneral))				{					if(this.control.drawObject.i_GeneralCount>=20)					{						MessageForm.Show("对不起，您目前选择的节点限制为20个.","DrawFlow");						return true;					}				}				else if(this.cb_nodeType.Text==DrawObject.getDefaultText(Node.DrawObjectType.DrawNodeBegin))				{					if(this.control.drawObject.bStartFlag)					{						MessageForm.Show("对不起，开始节点创建受限制。","DrawFlow");						return true;					}					else if(this.node.ConnectInCount>0)					{						MessageForm.Show("您要更改的节点的不满足转为开始节点的要求，开始节点没有前驱,有多个后继 .","DrawFlow");						return true;											}				}				else if(this.cb_nodeType.Text==DrawObject.getDefaultText(Node.DrawObjectType.DrawNodeEnd))				{					if(this.control.drawObject.bStartFlag)					{						MessageForm.Show("对不起，结束节点创建受限制。","DrawFlow");						return true;					}					else if(this.node.ConnectOutCount>0)					{						MessageForm.Show("您要更改的节点的不满足转为结束节点的要求，结束节点有多个前驱,没有后继。","DrawFlow");						return true;											}				}				else if(this.cb_nodeType.Text==DrawObject.getDefaultText(Node.DrawObjectType.DrawAsunder))				{					if(this.node.ConnectInCount>1)					{						MessageForm.Show("您要更改的节点的不满足转为分支节点的要求，分支节点只有一个前驱,有多个后继。","DrawFlow");						return true;											}							}				else if(this.cb_nodeType.Text==DrawObject.getDefaultText(Node.DrawObjectType.DrawConverge))				{					if(this.node.ConnectOutCount>1)					{						MessageForm.Show("您要更改的节点的不满足转为汇聚节点的要求，汇聚节点有多个前驱,只有一个后继。","DrawFlow");						return true;											}									}				else if(this.cb_nodeType.Text==DrawObject.getDefaultText(Node.DrawObjectType.DrawDataNode))				{					if(this.node.ConnectInCount>0)					{						MessageForm.Show("您要更改的节点的不满足转为数据节点的要求，数据节点没有前驱,有多个后继。","DrawFlow");						return true;											}									}				else if(this.cb_nodeType.Text==DrawObject.getDefaultText(Node.DrawObjectType.DrawGather))				{					if(this.node.ConnectOutCount>1)					{						MessageForm.Show("您要更改的节点的不满足转为汇总节点的要求，汇总节点有多个前驱,只有一个后继。","DrawFlow");						return true;											}									}				else if(this.cb_nodeType.Text==DrawObject.getDefaultText(Node.DrawObjectType.DrawGradation))				{					if(this.node.ConnectInCount>1 || this.node.ConnectOutCount>1)					{						MessageForm.Show("您要更改的节点的不满足转为顺序节点的要求，顺序节点只有一个前驱,只有一个后继。","DrawFlow");						return true;											}									}				else if(this.cb_nodeType.Text==DrawObject.getDefaultText(Node.DrawObjectType.DrawSpecificallyOperation))				{					if(this.node.ConnectInCount>1 || this.node.ConnectOutCount>0)					{						MessageForm.Show("您要更改的节点的不满足转为特定操作节点的要求，特定操作节点只有一个前驱,没有后继。","DrawFlow");						return true;											}									}				else if(this.cb_nodeType.Text==DrawObject.getDefaultText(Node.DrawObjectType.DrawSynchronization))				{					if(this.node.ConnectInCount>2 || this.node.ConnectOutCount>1)					{						MessageForm.Show("您要更改的节点的不满足转为同步节点的要求，同步节点只有两个前驱,有一个后继。","DrawFlow");						return true;											}									}				switch(this.cb_nodeType.Text)				{					case "开始" : node.ObjectType=Node.DrawObjectType.DrawNodeBegin;this.control.drawObject.bStartFlag=true; break;					case "任务" : node.ObjectType=Node.DrawObjectType.DrawNodeGeneral; this.control.drawObject.i_GeneralCount++;break;					case "特定操作" : node.ObjectType=Node.DrawObjectType.DrawSpecificallyOperation; break;					case "顺序" : node.ObjectType=Node.DrawObjectType.DrawGradation; break;					case "同步" : node.ObjectType=Node.DrawObjectType.DrawSynchronization; break;					case "分支" : node.ObjectType=Node.DrawObjectType.DrawAsunder; break;					case "汇聚" : node.ObjectType=Node.DrawObjectType.DrawConverge; break;					case "汇总连接" : node.ObjectType=Node.DrawObjectType.DrawGather; break;					case "判断" : node.ObjectType=Node.DrawObjectType.DrawJudgement; break;					case "应用数据" : node.ObjectType=Node.DrawObjectType.DrawDataNode; break;					case "结束" : node.ObjectType=Node.DrawObjectType.DrawNodeEnd;this.control.drawObject.bEndFlag=true; break;					case "矩形" : node.ObjectType=Node.DrawObjectType.DrawRectangle; break;					case "椭圆形" : node.ObjectType=Node.DrawObjectType.DrawEllipse; break;				}			}			return false;		}
		/// <summary>
		/// 点击取消  关闭窗口
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btn_cancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
			this.Dispose();
		}
		/// <summary>
		/// 流入时间  和流出时间的检测
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Time_ValueChanged(object sender, System.EventArgs e)
		{
			if(this.dtp_inflowTime.Value>this.dtp_outflowTime.Value)
			{
				((DateTimePicker)sender).Value=this.dt_TempDateTime.Date;
				MessageForm.Show("您选择的流入时间大于流出时间。");
			}	
		}
		/// <summary>
		/// 记录要被修改的时间  用于复原
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Time_DropDown(object sender, System.EventArgs e)
		{
			this.dt_TempDateTime=((DateTimePicker)sender).Value;
		}
		/// <summary>
		/// 根据前驱 或  后继节点的改变来改变连接条件 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cb_inflowNode_TextChanged(object sender, System.EventArgs e)
		{
			//			String ShowText=this.cb_outflowNode.Text;
			//			int i=ShowText.LastIndexOf("ID:");
			//			int Node_ID=int.Parse(ShowText.Substring(i,1));
			//			MessageBox.Show(""+Node_ID);
		}
		/// <summary>
		/// 前驱节点选择改变处理时间
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cb_inflowNode_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string ShowText=this.cb_inflowNode.Text;
			int i=ShowText.LastIndexOf("ID:");
			int Node_ID=int.Parse(ShowText.Substring(i+3,1));
			for(int j=0;j<this.arr_InFlowLineList.Count;j++)
			{
				Line line=(Line)this.arr_InFlowLineList[j];
				if(line.SecondNode==this.node && line.FirstNode.NodeListIndex==Node_ID)
				{
					this.tb_InConnectCondition.Text=line.Content;
					return;
				}
			}
		}
		/// <summary>
		/// 后继节点选择改变处理时间
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cb_outflowNode_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string ShowText=this.cb_outflowNode.Text;
			int i=ShowText.LastIndexOf("ID:");
			int Node_ID=int.Parse(ShowText.Substring(i+3,1));
			for(int j=0;j<this.arr_OutFlowLineList.Count;j++)
			{
				Line line=(Line)this.arr_OutFlowLineList[j];
				if(line.FirstNode==this.node && line.SecondNode.NodeListIndex==Node_ID)
				{
					this.tb_OutConnectCondition.Text=line.Content;
					return;
				}
			}		
		}
		/// <summary>
		/// 修改拥护修改的连接线的 名称
		/// </summary>
		public void ChangeLineContent()
		{
			string ShowText;
			int i;
			int Node_ID;
			if(this.tb_InConnectCondition.Enabled==true)
			{
				ShowText=this.cb_inflowNode.Text;
				i=ShowText.LastIndexOf("ID:");
				Node_ID=int.Parse(ShowText.Substring(i+3,1));
				for(int j=0;j<this.arr_InFlowLineList.Count;j++)
				{
					Line line=(Line)this.arr_InFlowLineList[j];
					if(line.SecondNode==this.node && line.FirstNode.NodeListIndex==Node_ID)
					{
						line.Content=this.tb_InConnectCondition.Text;
					}
				}
			}
			if(this.tb_OutConnectCondition.Enabled==true)
			{
				ShowText=this.cb_outflowNode.Text;
				i=ShowText.LastIndexOf("ID:");
				Node_ID=int.Parse(ShowText.Substring(i+3,1));
				for(int j=0;j<this.arr_OutFlowLineList.Count;j++)
				{
					Line line=(Line)this.arr_OutFlowLineList[j];
					if(line.FirstNode==this.node && line.SecondNode.NodeListIndex==Node_ID)
					{
						line.Content=this.tb_OutConnectCondition.Text;
					}
				}
			}
		}
		/// <summary>
		/// 记录原先选定的节点类型
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cb_nodeType_DropDown(object sender, System.EventArgs e)
		{
			this.str_NodeTypeSeleBefor=this.cb_nodeType.Text;
		}
		/// <summary>
		/// 判断选择的节点类型是否可以操作
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cb_nodeType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.cb_nodeType.Text==DrawObject.getDefaultText(Node.DrawObjectType.DrawNodeGeneral))
			{
				if(this.control.drawObject.i_GeneralCount>=20 && node.ObjectType!=Node.DrawObjectType.DrawNodeGeneral)
				{
					MessageForm.Show("对不起，您目前选择的节点限制为20个.","DrawFlow");
					this.cb_nodeType.Text=this.str_NodeTypeSeleBefor;
					return;
				}
			}
			else if(this.cb_nodeType.Text==DrawObject.getDefaultText(Node.DrawObjectType.DrawNodeBegin) && node.ObjectType!=Node.DrawObjectType.DrawNodeBegin)
			{
				if(this.control.drawObject.bStartFlag)
				{
					MessageForm.Show("对不起，开始节点创建受限制。","DrawFlow");
					this.cb_nodeType.Text=this.str_NodeTypeSeleBefor;
					return;
				}
			}
			else if(this.cb_nodeType.Text==DrawObject.getDefaultText(Node.DrawObjectType.DrawNodeEnd) && node.ObjectType!=Node.DrawObjectType.DrawNodeEnd)
			{
				if(this.control.drawObject.bStartFlag)
				{
					MessageForm.Show("对不起，结束节点创建受限制。","DrawFlow");
					this.cb_nodeType.Text=this.str_NodeTypeSeleBefor;
					return;
				}
			}		
		}
	}
}
