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
	public class SaveFlowDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btn_save;
		private System.Windows.Forms.Button btn_cancel;
		private System.Windows.Forms.TextBox tb_flowName;
		private System.Windows.Forms.ListView lv_flowView;
		private System.Windows.Forms.ImageList imglist;
		private System.ComponentModel.IContainer components;
		
		private string isSave="Default";
		private string filename="";

		public SaveFlowDialog(string name)
		{
			InitializeComponent();
			splitName();
			this.tb_flowName.Text=name;
			this.BackgroundImage=new Bitmap(GetType(),"images.attribute.png");
		}
		private void splitName()
		{
			string [] stra=Directory.GetFiles(Application.StartupPath+"\\WorkFlowXml");
				foreach(string temp in stra)
				{
					string str=temp.Substring(temp.LastIndexOf("\\")+1);
					ListViewItem lvi=new ListViewItem();
					lvi.Text=str;
					lvi.ImageIndex=0;
					this.lv_flowView.Items.Add(lvi);
				}
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SaveFlowDialog));
			this.lv_flowView = new System.Windows.Forms.ListView();
			this.imglist = new System.Windows.Forms.ImageList(this.components);
			this.label1 = new System.Windows.Forms.Label();
			this.tb_flowName = new System.Windows.Forms.TextBox();
			this.btn_save = new System.Windows.Forms.Button();
			this.btn_cancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lv_flowView
			// 
			this.lv_flowView.LargeImageList = this.imglist;
			this.lv_flowView.Location = new System.Drawing.Point(12, 36);
			this.lv_flowView.MultiSelect = false;
			this.lv_flowView.Name = "lv_flowView";
			this.lv_flowView.Size = new System.Drawing.Size(526, 244);
			this.lv_flowView.SmallImageList = this.imglist;
			this.lv_flowView.StateImageList = this.imglist;
			this.lv_flowView.TabIndex = 0;
			this.lv_flowView.Click += new System.EventHandler(this.lv_flowView_Click);
			// 
			// imglist
			// 
			this.imglist.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imglist.ImageSize = new System.Drawing.Size(42, 42);
			this.imglist.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglist.ImageStream")));
			this.imglist.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Location = new System.Drawing.Point(10, 292);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 17);
			this.label1.TabIndex = 1;
			this.label1.Text = "文件名:";
			// 
			// tb_flowName
			// 
			this.tb_flowName.Location = new System.Drawing.Point(56, 288);
			this.tb_flowName.Name = "tb_flowName";
			this.tb_flowName.Size = new System.Drawing.Size(380, 21);
			this.tb_flowName.TabIndex = 2;
			this.tb_flowName.Text = "";
			this.tb_flowName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_flowName_KeyDown);
			// 
			// btn_save
			// 
			this.btn_save.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btn_save.Location = new System.Drawing.Point(464, 286);
			this.btn_save.Name = "btn_save";
			this.btn_save.TabIndex = 3;
			this.btn_save.Text = "保存";
			this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
			// 
			// btn_cancel
			// 
			this.btn_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btn_cancel.Location = new System.Drawing.Point(464, 316);
			this.btn_cancel.Name = "btn_cancel";
			this.btn_cancel.TabIndex = 4;
			this.btn_cancel.Text = "取消";
			this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
			// 
			// SaveFlowDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(556, 388);
			this.Controls.Add(this.btn_cancel);
			this.Controls.Add(this.btn_save);
			this.Controls.Add(this.tb_flowName);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lv_flowView);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.Name = "SaveFlowDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "保存流程图";
			this.ResumeLayout(false);

		}
		#endregion

		
		private void btn_save_Click(object sender, System.EventArgs e)
		{
			if(this.tb_flowName.Text.Length<0)
			{
				MessageForm.Show("文件名不能为空。");
				this.tb_flowName.Focus();
				return;
			}
			else if(this.tb_flowName.Text.Length>20)
			{
				MessageForm.Show("文件名过长。");
				this.tb_flowName.Focus();
				return;
			}
			int p=tb_flowName.Text.LastIndexOf(".");
			if(p<0 || tb_flowName.Text.Substring(p).ToUpper()!=".XML")
			{
				tb_flowName.Text+=".xml";
			}
			foreach(ListViewItem temp in lv_flowView.Items)
			{
				if(temp.Text==this.tb_flowName.Text)
				{
					DialogResult dr=MessageForm.Show("文件名己存在,是否覆盖己有文件",ButtonType.YesAndNo);
					if(dr==DialogResult.Yes)
					{
						this.FileName=this.tb_flowName.Text;
						this.SaveOperate="Save";
						this.DialogResult=DialogResult.OK;
					}
					else
					{
						return;
					}
					break;
				}
			}
			this.FileName=this.tb_flowName.Text;
			this.SaveOperate="Save";
			this.DialogResult=DialogResult.OK;
			
		}

		private void btn_cancel_Click(object sender, System.EventArgs e)
		{
			this.SaveOperate="Default";
			this.DialogResult=DialogResult.Cancel;
		}

		private void lv_flowView_Click(object sender, System.EventArgs e)
		{
			if(this.lv_flowView.SelectedItems.Count>0)
			{
				this.tb_flowName.Text=this.lv_flowView.SelectedItems[0].Text;
			}
		}

		private void tb_flowName_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Enter)
			{
				this.btn_save_Click(sender,e);
			}
		}

		/// <summary>
		/// 获取用户的操作
		/// </summary>
		public string SaveOperate
		{
			get{return isSave;}
			set{isSave=value;}
		}

		/// <summary>
		/// 获取或设置XML的文件名
		/// </summary>
		public string FileName
		{
			get{return filename;}
			set{filename=value;}
		}
	}
}
