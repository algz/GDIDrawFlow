using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
namespace GDIDrawFlow
{
	/// <summary>
	/// saveDialog 的摘要说明。
	/// </summary>
	public class OpenFlowDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btn_cancel;
		private System.Windows.Forms.ListView lv_flowView;
		private System.Windows.Forms.ImageList imglist;
		private System.Windows.Forms.Button btn_open;
		private System.Windows.Forms.Button btn_flow;
		private System.Windows.Forms.Button btn_mode;
		private System.ComponentModel.IContainer components;
		string [] flowFileNames;
		string [] ModeFileNames;
		private string openfileName="";
		private string openType="OpenXml";
		public OpenFlowDialog()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();
			splitName();
			this.BackgroundImage=new Bitmap(GetType(),"images.attribute.png");
			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
		}
		private void splitName()
		{
			flowFileNames=Directory.GetFiles(Application.StartupPath+"\\WorkFlowXml\\");
			ModeFileNames=Directory.GetFiles(Application.StartupPath+"\\WorkFlowMode\\");
			btn_flow_Click(null,null);
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(OpenFlowDialog));
			this.lv_flowView = new System.Windows.Forms.ListView();
			this.imglist = new System.Windows.Forms.ImageList(this.components);
			this.btn_open = new System.Windows.Forms.Button();
			this.btn_cancel = new System.Windows.Forms.Button();
			this.btn_flow = new System.Windows.Forms.Button();
			this.btn_mode = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lv_flowView
			// 
			this.lv_flowView.BackColor = System.Drawing.Color.White;
			this.lv_flowView.LargeImageList = this.imglist;
			this.lv_flowView.Location = new System.Drawing.Point(8, 42);
			this.lv_flowView.MultiSelect = false;
			this.lv_flowView.Name = "lv_flowView";
			this.lv_flowView.Size = new System.Drawing.Size(446, 270);
			this.lv_flowView.SmallImageList = this.imglist;
			this.lv_flowView.StateImageList = this.imglist;
			this.lv_flowView.TabIndex = 0;
			this.lv_flowView.DoubleClick += new System.EventHandler(this.lv_flowView_DoubleClick);
			// 
			// imglist
			// 
			this.imglist.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imglist.ImageSize = new System.Drawing.Size(42, 42);
			this.imglist.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglist.ImageStream")));
			this.imglist.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// btn_open
			// 
			this.btn_open.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btn_open.Location = new System.Drawing.Point(464, 42);
			this.btn_open.Name = "btn_open";
			this.btn_open.TabIndex = 3;
			this.btn_open.Text = "打开";
			this.btn_open.Click += new System.EventHandler(this.btn_open_Click);
			// 
			// btn_cancel
			// 
			this.btn_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btn_cancel.Location = new System.Drawing.Point(464, 78);
			this.btn_cancel.Name = "btn_cancel";
			this.btn_cancel.TabIndex = 4;
			this.btn_cancel.Text = "取消";
			this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
			// 
			// btn_flow
			// 
			this.btn_flow.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btn_flow.Location = new System.Drawing.Point(8, 318);
			this.btn_flow.Name = "btn_flow";
			this.btn_flow.TabIndex = 5;
			this.btn_flow.Text = "流程图";
			this.btn_flow.Click += new System.EventHandler(this.btn_flow_Click);
			// 
			// btn_mode
			// 
			this.btn_mode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btn_mode.Location = new System.Drawing.Point(102, 318);
			this.btn_mode.Name = "btn_mode";
			this.btn_mode.TabIndex = 6;
			this.btn_mode.Text = "模版";
			this.btn_mode.Click += new System.EventHandler(this.btn_mode_Click);
			// 
			// OpenFlowDialog
			// 
			this.AcceptButton = this.btn_open;
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(556, 388);
			this.Controls.Add(this.btn_mode);
			this.Controls.Add(this.btn_flow);
			this.Controls.Add(this.btn_cancel);
			this.Controls.Add(this.btn_open);
			this.Controls.Add(this.lv_flowView);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.Name = "OpenFlowDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "打开流程图";
			this.ResumeLayout(false);

		}
		#endregion

		private void btn_open_Click(object sender, System.EventArgs e)
		{
			if(this.lv_flowView.SelectedItems.Count==0)
			{
				this.lv_flowView.Focus();
				return;
			}
			this.OpenFileName=this.lv_flowView.SelectedItems[0].Text;
			DialogResult=DialogResult.OK;
		}

		private void btn_cancel_Click(object sender, System.EventArgs e)
		{
			DialogResult=DialogResult.Cancel;

		}

		private void btn_flow_Click(object sender, System.EventArgs e)
		{
			this.lv_flowView.Items.Clear();
			if(ModeFileNames!=null)
			{
				foreach(string temp in flowFileNames)
				{
					string str=temp.Substring(temp.LastIndexOf("\\")+1);
					ListViewItem lvi=new ListViewItem();
					lvi.Text=str;
					lvi.ImageIndex=0;
					this.lv_flowView.Items.Add(lvi);
				}
			}
			openType="OpenXml";
		}

		private void btn_mode_Click(object sender, System.EventArgs e)
		{
			this.lv_flowView.Items.Clear();
			if(ModeFileNames!=null)
			{
				foreach(string temp in ModeFileNames)
				{
					string str=temp.Substring(temp.LastIndexOf("\\")+1);
					ListViewItem lvi=new ListViewItem();
					lvi.Text=str;
					lvi.ImageIndex=0;
					this.lv_flowView.Items.Add(lvi);
				}
			}
			openType="OpenMode";
		}

		private void lv_flowView_DoubleClick(object sender, System.EventArgs e)
		{
			if(lv_flowView.SelectedItems!=null)
			{
				btn_open_Click(sender,e);
			}
		}

		/// <summary>
		/// 获取或设置用户选择的Xml文件或模板
		/// </summary>
		public string OpenFileName
		{
			get{return openfileName;}
			set{openfileName=value;}
		}
		/// <summary>
		/// 获取用户选择的类型
		/// </summary>
		public string OpenType
		{
			get{return openType;}
		}
	}
}
