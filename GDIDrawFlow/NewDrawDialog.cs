using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text.RegularExpressions;
namespace GDIDrawFlow
{
	/// <summary>
	/// MessageForm 的摘要说明。
	/// </summary>
	public class NewDrawDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label lbl_title;
		private System.Windows.Forms.PictureBox pb_close;
		private System.Windows.Forms.Panel pnl_MoveBar;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Label lbl_Cancel;
		private System.Windows.Forms.Label lbl_Confirm;
		private System.Windows.Forms.Timer showForm;
		private int cx,cy;
		private System.Windows.Forms.Label label1;
		private bool isDown;
		private System.Windows.Forms.CheckBox cb_integrality;
		private System.Windows.Forms.CheckBox cb_LoadDefaultFlow;
		private System.Windows.Forms.CheckBox cb_limit;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TextBox tb_flowName;
		private string flowName="";
		private bool integrality=false;//是否检查完整性
		private bool loadDefaultFlow=false;//是否加载默认流程图
		private bool nodeLimit=false;//是否限制结点
		public NewDrawDialog()
		{
			InitializeComponent();
			this.BackgroundImage=new Bitmap(GetType(),"images.newFlow.jpg");
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(NewDrawDialog));
			this.lbl_Cancel = new System.Windows.Forms.Label();
			this.lbl_Confirm = new System.Windows.Forms.Label();
			this.lbl_title = new System.Windows.Forms.Label();
			this.pb_close = new System.Windows.Forms.PictureBox();
			this.pnl_MoveBar = new System.Windows.Forms.Panel();
			this.showForm = new System.Windows.Forms.Timer(this.components);
			this.label1 = new System.Windows.Forms.Label();
			this.cb_integrality = new System.Windows.Forms.CheckBox();
			this.cb_limit = new System.Windows.Forms.CheckBox();
			this.cb_LoadDefaultFlow = new System.Windows.Forms.CheckBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.tb_flowName = new System.Windows.Forms.TextBox();
			this.pnl_MoveBar.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// lbl_Cancel
			// 
			this.lbl_Cancel.BackColor = System.Drawing.Color.Transparent;
			this.lbl_Cancel.Cursor = System.Windows.Forms.Cursors.Hand;
			this.lbl_Cancel.Image = ((System.Drawing.Image)(resources.GetObject("lbl_Cancel.Image")));
			this.lbl_Cancel.Location = new System.Drawing.Point(318, 102);
			this.lbl_Cancel.Name = "lbl_Cancel";
			this.lbl_Cancel.Size = new System.Drawing.Size(64, 22);
			this.lbl_Cancel.TabIndex = 3;
			this.lbl_Cancel.Text = "取消";
			this.lbl_Cancel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lbl_Cancel.Click += new System.EventHandler(this.lbl_Cancel_Click);
			// 
			// lbl_Confirm
			// 
			this.lbl_Confirm.BackColor = System.Drawing.Color.Transparent;
			this.lbl_Confirm.Cursor = System.Windows.Forms.Cursors.Hand;
			this.lbl_Confirm.Image = ((System.Drawing.Image)(resources.GetObject("lbl_Confirm.Image")));
			this.lbl_Confirm.Location = new System.Drawing.Point(318, 64);
			this.lbl_Confirm.Name = "lbl_Confirm";
			this.lbl_Confirm.Size = new System.Drawing.Size(64, 22);
			this.lbl_Confirm.TabIndex = 2;
			this.lbl_Confirm.Text = "确定";
			this.lbl_Confirm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lbl_Confirm.Click += new System.EventHandler(this.lbl_confirm_Click);
			// 
			// lbl_title
			// 
			this.lbl_title.BackColor = System.Drawing.Color.Transparent;
			this.lbl_title.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.lbl_title.ForeColor = System.Drawing.Color.White;
			this.lbl_title.Location = new System.Drawing.Point(6, 6);
			this.lbl_title.Name = "lbl_title";
			this.lbl_title.Size = new System.Drawing.Size(360, 16);
			this.lbl_title.TabIndex = 5;
			this.lbl_title.Text = "新建流程图";
			this.lbl_title.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnl_MoveBar_MouseUp);
			this.lbl_title.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnl_MoveBar_MouseMove);
			this.lbl_title.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnl_MoveBar_MouseDown);
			// 
			// pb_close
			// 
			this.pb_close.BackColor = System.Drawing.Color.Transparent;
			this.pb_close.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pb_close.BackgroundImage")));
			this.pb_close.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pb_close.Location = new System.Drawing.Point(372, 6);
			this.pb_close.Name = "pb_close";
			this.pb_close.Size = new System.Drawing.Size(16, 16);
			this.pb_close.TabIndex = 6;
			this.pb_close.TabStop = false;
			this.pb_close.Click += new System.EventHandler(this.pb_close_Click);
			// 
			// pnl_MoveBar
			// 
			this.pnl_MoveBar.BackColor = System.Drawing.Color.Transparent;
			this.pnl_MoveBar.Controls.Add(this.lbl_title);
			this.pnl_MoveBar.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnl_MoveBar.Location = new System.Drawing.Point(0, 0);
			this.pnl_MoveBar.Name = "pnl_MoveBar";
			this.pnl_MoveBar.Size = new System.Drawing.Size(398, 28);
			this.pnl_MoveBar.TabIndex = 7;
			this.pnl_MoveBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnl_MoveBar_MouseUp);
			this.pnl_MoveBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnl_MoveBar_MouseMove);
			this.pnl_MoveBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnl_MoveBar_MouseDown);
			// 
			// showForm
			// 
			this.showForm.Enabled = true;
			this.showForm.Interval = 10;
			this.showForm.Tick += new System.EventHandler(this.showForm_Tick);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Location = new System.Drawing.Point(104, 68);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 17);
			this.label1.TabIndex = 10;
			this.label1.Text = "名称:";
			// 
			// cb_integrality
			// 
			this.cb_integrality.BackColor = System.Drawing.Color.Transparent;
			this.cb_integrality.Location = new System.Drawing.Point(112, 92);
			this.cb_integrality.Name = "cb_integrality";
			this.cb_integrality.Size = new System.Drawing.Size(160, 24);
			this.cb_integrality.TabIndex = 11;
			this.cb_integrality.Text = "检测流程图完整性";
			// 
			// cb_limit
			// 
			this.cb_limit.BackColor = System.Drawing.Color.Transparent;
			this.cb_limit.Checked = true;
			this.cb_limit.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cb_limit.Location = new System.Drawing.Point(112, 116);
			this.cb_limit.Name = "cb_limit";
			this.cb_limit.Size = new System.Drawing.Size(164, 24);
			this.cb_limit.TabIndex = 12;
			this.cb_limit.Text = "图元个数限制";
			// 
			// cb_LoadDefaultFlow
			// 
			this.cb_LoadDefaultFlow.BackColor = System.Drawing.Color.Transparent;
			this.cb_LoadDefaultFlow.Location = new System.Drawing.Point(112, 140);
			this.cb_LoadDefaultFlow.Name = "cb_LoadDefaultFlow";
			this.cb_LoadDefaultFlow.Size = new System.Drawing.Size(130, 24);
			this.cb_LoadDefaultFlow.TabIndex = 13;
			this.cb_LoadDefaultFlow.Text = "加载默认流程图";
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(69)), ((System.Byte)(158)), ((System.Byte)(238)));
			this.panel1.Controls.Add(this.tb_flowName);
			this.panel1.Location = new System.Drawing.Point(144, 66);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(118, 16);
			this.panel1.TabIndex = 9;
			// 
			// tb_flowName
			// 
			this.tb_flowName.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.tb_flowName.Location = new System.Drawing.Point(1, 1);
			this.tb_flowName.Name = "tb_flowName";
			this.tb_flowName.Size = new System.Drawing.Size(116, 14);
			this.tb_flowName.TabIndex = 8;
			this.tb_flowName.Text = "";
			this.tb_flowName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_flowName_KeyDown);
			// 
			// NewDrawDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(398, 188);
			this.Controls.Add(this.cb_LoadDefaultFlow);
			this.Controls.Add(this.cb_limit);
			this.Controls.Add(this.cb_integrality);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.pb_close);
			this.Controls.Add(this.lbl_Cancel);
			this.Controls.Add(this.lbl_Confirm);
			this.Controls.Add(this.pnl_MoveBar);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "NewDrawDialog";
			this.Opacity = 0;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "系统提示";
			this.Load += new System.EventHandler(this.NewDrawDialog_Load);
			this.pnl_MoveBar.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region 窗体移动事件
		private void pnl_MoveBar_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			isDown=true;
			cx=e.X;
			cy=e.Y;
		}

		private void pnl_MoveBar_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(isDown)
			{
				this.Left+=e.X-cx;
				this.Top +=e.Y-cy;
			}
		}

		private void pnl_MoveBar_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			isDown=false;
		}
		#endregion

		private void pb_close_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
		string temp=@"^\w+$";
		private void lbl_confirm_Click(object sender, System.EventArgs e)
		{
			if(this.tb_flowName.Text.Length>30)
			{
				MessageForm.Show("文件名过长。");
				this.tb_flowName.Focus();
				return;
			}
			else if(this.tb_flowName.Text.Length>0)
			{
				Regex r=new Regex(temp);
				Match m = r.Match(this.tb_flowName.Text);
				if(!m.Success)
				{
					MessageForm.Show("输入的文件名不合法.");
					return;
				}
				this.loadDefaultFlow=this.cb_LoadDefaultFlow.Checked;
				this.nodeLimit=this.cb_limit.Checked;
				this.integrality=this.cb_integrality.Checked;
				this.flowName=this.tb_flowName.Text;
				
				this.DialogResult=DialogResult.OK;
			}
			else
			{
				MessageForm.Show("流程图名称不能为空。");
			}
		}

		private void showForm_Tick(object sender, System.EventArgs e)
		{
			if(this.Opacity<0.9)
			{
				this.Opacity+=0.05;
			}
			else
			{
				this.showForm .Stop();
			}
		}

		private void lbl_Cancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult=DialogResult.Cancel;
		}

		private void tb_flowName_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Enter)lbl_confirm_Click(sender,e);
		}

		private void NewDrawDialog_Load(object sender, System.EventArgs e)
		{
			this.tb_flowName.Text=this.flowName;
			if(this.flowName.Length>0)
			{
				this.cb_integrality.Enabled=false;
				this.cb_limit.Enabled=false;
				this.cb_LoadDefaultFlow.Enabled=false;
			}
		}

		/// <summary>
		/// 得到流程图名称
		/// </summary>
		public string FlowName
		{
			get
			{
				   return this.flowName;
		    }
			set
			{
				this.flowName=value;
			}
		}
		/// <summary>
		/// 设置新建重命名流程图对话框标题
		/// </summary>
		public string DialogCaption
		{
			set
			{
				this.lbl_title.Text=value;
			}
		}
		
		public bool isIntegrality
		{
			get{return this.integrality;}
		}
		public bool isLoadDefaultFlow
		{
			get{return this.loadDefaultFlow;}
		}
		public bool isNodeLimit
		{
			get{return this.nodeLimit;}
		}
	}
}
