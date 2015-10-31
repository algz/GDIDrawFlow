using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace GDIDrawFlow
{
	/// <summary>
	/// LoadingForm 的摘要说明。
	/// </summary>
	public class LoadingForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Timer timeReady;
		private System.Windows.Forms.Timer showForm;
		private System.ComponentModel.IContainer components;

		public LoadingForm()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();
			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
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
		public string LoadingText
		{
			set
			{
				this.label1.Text=value;
			}
		}
		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(LoadingForm));
			this.label1 = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.timeReady = new System.Windows.Forms.Timer(this.components);
			this.showForm = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label1.Location = new System.Drawing.Point(4, 14);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(232, 26);
			this.label1.TabIndex = 0;
			this.label1.Text = "文件正在读取...";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(8, 48);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(216, 24);
			this.pictureBox1.TabIndex = 1;
			this.pictureBox1.TabStop = false;
			// 
			// timeReady
			// 
			this.timeReady.Interval = 1000;
			this.timeReady.Tick += new System.EventHandler(this.timeReady_Tick);
			// 
			// showForm
			// 
			this.showForm.Interval = 1;
			this.showForm.Tick += new System.EventHandler(this.showForm_Tick);
			// 
			// LoadingForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(117)), ((System.Byte)(183)), ((System.Byte)(57)));
			this.ClientSize = new System.Drawing.Size(238, 72);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "LoadingForm";
			this.Opacity = 0;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.TopMost = true;
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LoadingForm_KeyDown);
			this.ResumeLayout(false);

		}
		#endregion

		private void LoadingForm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.Alt==true && e.KeyCode==Keys.F4)
			{
				e.Handled=true;
			}
		}
		new public void Show()
		{
			base.Show();
			timeReady.Start();
			this.showForm.Start();

		}
		int i=0;
		private void timeReady_Tick(object sender, System.EventArgs e)
		{
			i++;
			if(i==10)
			{
				this.Hide();
				i=0;
				this.timeReady.Stop();
			}
		}

		private void showForm_Tick(object sender, System.EventArgs e)
		{
			if(this.Opacity<=0.6)
			{
				this.Opacity+=0.1;
			}
			else
			{
				this.showForm.Stop();
			}
		}
		new public void Hide()
		{
			base.Hide();
			this.Opacity=0;
		}

	}
}
