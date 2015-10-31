using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;
using System.MyControl.MyMenu;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.IO;
using System.MyControl.MyToolTip;
namespace GDIDrawFlow
{
    /// <summary>
    /// DrawFlowControl 的摘要说明。
    /// </summary>

    public class DrawFlowControl : System.Windows.Forms.UserControl
    {
        #region 申明变量
        int tReady = 0;//控制提示时间
        private Bitmap[] imageArr = new Bitmap[5];
        private ContextMenu menu = new ContextMenu();
        public System.Windows.Forms.MenuItem menuItem1;
        public System.Windows.Forms.MenuItem menuItem2;
        public System.Windows.Forms.MenuItem menuItem3;
        public System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem menuItem5;
        public System.Windows.Forms.MenuItem menuItem6;
        private Menus menus;
        public bool isNodeLimit = true; //是否限制结点个数
        public bool isIntegrality = false;//是否检查程序完整性
        public Graphics graDrawPanel;
        public Graphics graDrawLine;
        public Bitmap bitmapMemeory, bitmapBackGroupMap;
        public Pen penDrawLine;
        private Color bckColor;
        public Pen penDrawNode;//绘制节点笔
        public Pen penDrawPoint;//绘制选择点笔
        public Pen penDrawString;//写字笔
        public Pen penDrawBeeLine;//绘制直线笔
        public Font fontDrawString;
        public DrawObject drawObject;
        public Bitmap bgImage;//窗体网格
        public System.Windows.Forms.TextBox drawStringContent;
        public Size size;
        public System.Windows.Forms.TextBox TBnodeContent;
        public System.Windows.Forms.Label testDrawLength;
        public System.Windows.Forms.Panel pnl_property;
        private System.MyControl.MyXPExplorerBar.TaskPane taskPane1;
        private System.Windows.Forms.Panel pnl_title;
        private System.Windows.Forms.PictureBox pb_splitter;
        public System.MyControl.MyXPExplorerBar.Expando epd_backGround;
        public System.MyControl.MyXPExplorerBar.Expando epd_nodeProperty;
        public System.MyControl.MyXPExplorerBar.Expando epd_lineProperty;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.Label label11;
        public System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        public System.MyControl.MyXPExplorerBar.Expando epd_stringProperty;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.PictureBox pb_closePropertyForm;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label24;
        public System.Windows.Forms.TextBox node_ID;
        public System.Windows.Forms.TextBox node_Type;
        public System.Windows.Forms.TextBox node_Height;
        public System.Windows.Forms.TextBox node_Width;
        public System.Windows.Forms.TextBox node_Name;
        public System.Windows.Forms.TextBox node_Size;
        public System.Windows.Forms.TextBox line_ID;
        public System.Windows.Forms.TextBox line_Type;
        public System.Windows.Forms.TextBox node_X;
        public System.Windows.Forms.TextBox node_Y;
        public System.Windows.Forms.Panel node_Font_Color;
        public System.Windows.Forms.Panel node_Border_Color;
        public System.Windows.Forms.Panel node_Fill_Color;
        public System.Windows.Forms.TextBox line_size;
        public System.Windows.Forms.TextBox line_FirNode_ID;
        public System.Windows.Forms.TextBox line_X0;
        public System.Windows.Forms.TextBox line_Y0;
        public System.Windows.Forms.TextBox line_SecNode_ID;
        public System.Windows.Forms.Panel line_color;
        public System.Windows.Forms.TextBox DS_ID;
        public System.Windows.Forms.TextBox DS_Content;
        public System.Windows.Forms.TextBox DS_X;
        public System.Windows.Forms.TextBox DS_Y;
        public System.Windows.Forms.TextBox DS_Height;
        public System.Windows.Forms.TextBox DS_Width;
        public System.Windows.Forms.TextBox DS_Size;
        public System.Windows.Forms.Panel DS_Color;
        public System.Windows.Forms.TextBox line_Y1;
        public System.Windows.Forms.TextBox line_X1;
        private System.Windows.Forms.ComboBox BG_Style;
        public System.Windows.Forms.MenuItem menuItem7;
        public System.Windows.Forms.MenuItem menuItem8;
        private System.Windows.Forms.MenuItem menuItem9;
        private System.Windows.Forms.MenuItem menuItem10;
        private System.Windows.Forms.MenuItem menuItem11;
        private System.Windows.Forms.MenuItem menuItem13;
        private System.Windows.Forms.MenuItem menuItem14;
        private System.Windows.Forms.MenuItem menuItem15;
        private System.Windows.Forms.MenuItem menuItem16;
        private System.Windows.Forms.MenuItem menuItem17;
        private System.Windows.Forms.MenuItem menuItem18;
        private System.Windows.Forms.MenuItem menuItem19;
        private System.Windows.Forms.MenuItem menuItem20;
        private System.Windows.Forms.MenuItem menuItem21;
        private System.Windows.Forms.MenuItem menuItem22;
        private System.Windows.Forms.MenuItem menuItem23;
        private System.Windows.Forms.MenuItem menuItem24;
        private System.Windows.Forms.MenuItem menuItem25;
        private System.Windows.Forms.MenuItem mi_newStartNode;
        private System.Windows.Forms.MenuItem mi_drawstring;
        private System.Windows.Forms.MenuItem mi_drawline;
        private System.Windows.Forms.MenuItem mi_line2;
        public System.Windows.Forms.TextBox TBLineContent;
        private System.Windows.Forms.ImageList toolBarImage;
        private System.MyControl.NSButton mti_startNode;
        private System.MyControl.NSButton mti_generalNode;
        private System.MyControl.NSButton mti_specificallyOperationNode;
        private System.MyControl.NSButton mti_gradationNode;
        private System.MyControl.NSButton mti_synchronizationNode;
        private System.MyControl.NSButton mti_asunderNode;
        private System.MyControl.NSButton mti_convergeNode;
        private System.MyControl.NSButton mti_gatherNode;
        private System.MyControl.NSButton mti_judgementNode;
        private System.MyControl.NSButton mti_DataNode;
        private System.MyControl.NSButton mti_endNode;
        private System.MyControl.NSButton nsButton1;
        private System.MyControl.NSButton mti_drawFoldLineTool;
        private System.MyControl.NSButton mti_drawLineTool;
        private System.MyControl.NSButton mti_drawString;
        private System.MyControl.NSButton mti_Ellipse;
        private System.MyControl.NSButton mti_Rect;
        public System.MyControl.MyToolTip.MyToolTip toolTip;
        public System.Windows.Forms.Panel tablePanel;
        public System.Windows.Forms.DataGrid dataGrid1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pb_down;
        private System.Windows.Forms.PictureBox pb_up;
        private System.Windows.Forms.Label lbl_tipInfo;
        private System.Windows.Forms.Panel pnl_tip;
        private System.Windows.Forms.Timer showTip;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox acceptFocus;
        public System.Windows.Forms.CheckBox cb_integrality;
        public System.Windows.Forms.CheckBox cb_limit;
        private System.Windows.Forms.Label lbl_checkIntegrality;
        public System.Windows.Forms.MenuItem menuItem12;
        private System.MyControl.NSButton mti_Cursor;
        private System.MyControl.NSButton nsButton2;
        private System.Windows.Forms.PictureBox pb_splitter2;
        private System.Windows.Forms.Panel pnl_tool;
        private System.Windows.Forms.Label label25;
        public System.Windows.Forms.TextBox tb_lineName;
        public System.Windows.Forms.CheckBox cb_isAlpha;
        private System.ComponentModel.IContainer components;
        #endregion

        #region 构造、初始化方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void init()
        {
            //
            //imageArr
            //
            imageArr[0] = new Bitmap(GetType(), "images.back1.gif");
            imageArr[1] = new Bitmap(GetType(), "images.back2.gif");
            imageArr[2] = new Bitmap(GetType(), "images.back3.gif");
            imageArr[3] = new Bitmap(GetType(), "images.back4.gif");
            imageArr[4] = new Bitmap(GetType(), "images.back5.gif");
            this.menus.Start(this);//实现菜单风格
            bgImage = new Bitmap(GetType(), "images.back1.gif");//窗体网格
            size = SystemInformation.PrimaryMonitorMaximizedWindowSize;
            this.bitmapMemeory = new Bitmap(size.Width, size.Height);
            this.graDrawPanel = Graphics.FromImage(this.bitmapMemeory);
            this.graDrawPanel.SmoothingMode = SmoothingMode.AntiAlias;
            this.bckColor = this.BackColor;
            this.graDrawLine = this.CreateGraphics();
            this.penDrawLine = new Pen(Color.Black, 1);
            this.penDrawNode = new Pen(Color.Blue, 2);
            this.penDrawPoint = new Pen(Color.Black, 6);
            this.penDrawBeeLine = new Pen(Color.Black, 1);
            this.penDrawBeeLine.StartCap = LineCap.RoundAnchor;
            AdjustableArrowCap myArrow = new AdjustableArrowCap(4, 6);
            CustomLineCap customArrow = myArrow;
            this.penDrawLine.CustomEndCap = myArrow;
            this.penDrawLine.StartCap = LineCap.RoundAnchor;
            this.penDrawString = new Pen(Color.Red, 5);
            this.fontDrawString = new Font("宋体", 9f);
            this.drawObject = new DrawObject(this);
            drawObject.GetTableList();
            FormRefrash();
            AddToolTip(true);
        }

        public DrawFlowControl()
        {
            // 该调用是 Windows.Forms 窗体设计器所必需的。
            InitializeComponent();
            init();
            this.BackgroundImage = new Bitmap(GetType(), "images.back1.gif");
            // TODO: 在 InitComponent 调用后添加任何初始化
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);

        }

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                    components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码
        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器 
        /// 修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DrawFlowControl));
            this.menus = new System.MyControl.MyMenu.Menus(this.components);
            this.menu = new System.Windows.Forms.ContextMenu();
            this.menuItem10 = new System.Windows.Forms.MenuItem();
            this.mi_newStartNode = new System.Windows.Forms.MenuItem();
            this.menuItem13 = new System.Windows.Forms.MenuItem();
            this.menuItem14 = new System.Windows.Forms.MenuItem();
            this.menuItem15 = new System.Windows.Forms.MenuItem();
            this.menuItem16 = new System.Windows.Forms.MenuItem();
            this.menuItem17 = new System.Windows.Forms.MenuItem();
            this.menuItem18 = new System.Windows.Forms.MenuItem();
            this.menuItem19 = new System.Windows.Forms.MenuItem();
            this.menuItem20 = new System.Windows.Forms.MenuItem();
            this.menuItem21 = new System.Windows.Forms.MenuItem();
            this.menuItem22 = new System.Windows.Forms.MenuItem();
            this.menuItem24 = new System.Windows.Forms.MenuItem();
            this.menuItem23 = new System.Windows.Forms.MenuItem();
            this.menuItem25 = new System.Windows.Forms.MenuItem();
            this.mi_drawstring = new System.Windows.Forms.MenuItem();
            this.mi_drawline = new System.Windows.Forms.MenuItem();
            this.mi_line2 = new System.Windows.Forms.MenuItem();
            this.menuItem11 = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.menuItem12 = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.drawStringContent = new System.Windows.Forms.TextBox();
            this.TBnodeContent = new System.Windows.Forms.TextBox();
            this.testDrawLength = new System.Windows.Forms.Label();
            this.toolTip = new System.MyControl.MyToolTip.MyToolTip(this.components);
            this.mti_drawFoldLineTool = new System.MyControl.NSButton();
            this.mti_drawLineTool = new System.MyControl.NSButton();
            this.mti_drawString = new System.MyControl.NSButton();
            this.mti_Ellipse = new System.MyControl.NSButton();
            this.mti_Rect = new System.MyControl.NSButton();
            this.mti_endNode = new System.MyControl.NSButton();
            this.mti_DataNode = new System.MyControl.NSButton();
            this.mti_judgementNode = new System.MyControl.NSButton();
            this.mti_gatherNode = new System.MyControl.NSButton();
            this.mti_convergeNode = new System.MyControl.NSButton();
            this.mti_asunderNode = new System.MyControl.NSButton();
            this.mti_synchronizationNode = new System.MyControl.NSButton();
            this.mti_gradationNode = new System.MyControl.NSButton();
            this.mti_specificallyOperationNode = new System.MyControl.NSButton();
            this.mti_generalNode = new System.MyControl.NSButton();
            this.mti_startNode = new System.MyControl.NSButton();
            this.pb_splitter = new System.Windows.Forms.PictureBox();
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.mti_Cursor = new System.MyControl.NSButton();
            this.pb_splitter2 = new System.Windows.Forms.PictureBox();
            this.pnl_property = new System.Windows.Forms.Panel();
            this.taskPane1 = new System.MyControl.MyXPExplorerBar.TaskPane();
            this.epd_backGround = new System.MyControl.MyXPExplorerBar.Expando();
            this.BG_Style = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cb_integrality = new System.Windows.Forms.CheckBox();
            this.cb_limit = new System.Windows.Forms.CheckBox();
            this.lbl_checkIntegrality = new System.Windows.Forms.Label();
            this.cb_isAlpha = new System.Windows.Forms.CheckBox();
            this.epd_nodeProperty = new System.MyControl.MyXPExplorerBar.Expando();
            this.label2 = new System.Windows.Forms.Label();
            this.node_ID = new System.Windows.Forms.TextBox();
            this.node_Type = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.node_X = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.node_Y = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.node_Height = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.node_Width = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.node_Name = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.node_Size = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.node_Font_Color = new System.Windows.Forms.Panel();
            this.node_Border_Color = new System.Windows.Forms.Panel();
            this.node_Fill_Color = new System.Windows.Forms.Panel();
            this.epd_lineProperty = new System.MyControl.MyXPExplorerBar.Expando();
            this.label13 = new System.Windows.Forms.Label();
            this.line_size = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.line_FirNode_ID = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.line_ID = new System.Windows.Forms.TextBox();
            this.line_Type = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.line_X0 = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.line_Y0 = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.line_Y1 = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.line_X1 = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.line_SecNode_ID = new System.Windows.Forms.TextBox();
            this.line_color = new System.Windows.Forms.Panel();
            this.label25 = new System.Windows.Forms.Label();
            this.tb_lineName = new System.Windows.Forms.TextBox();
            this.epd_stringProperty = new System.MyControl.MyXPExplorerBar.Expando();
            this.label27 = new System.Windows.Forms.Label();
            this.DS_ID = new System.Windows.Forms.TextBox();
            this.DS_Content = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.DS_X = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.DS_Y = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.DS_Height = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.DS_Width = new System.Windows.Forms.TextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.DS_Size = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.DS_Color = new System.Windows.Forms.Panel();
            this.pnl_title = new System.Windows.Forms.Panel();
            this.pb_closePropertyForm = new System.Windows.Forms.PictureBox();
            this.toolBarImage = new System.Windows.Forms.ImageList(this.components);
            this.TBLineContent = new System.Windows.Forms.TextBox();
            this.pnl_tool = new System.Windows.Forms.Panel();
            this.nsButton2 = new System.MyControl.NSButton();
            this.nsButton1 = new System.MyControl.NSButton();
            this.tablePanel = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pb_down = new System.Windows.Forms.PictureBox();
            this.pb_up = new System.Windows.Forms.PictureBox();
            this.pnl_tip = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lbl_tipInfo = new System.Windows.Forms.Label();
            this.showTip = new System.Windows.Forms.Timer(this.components);
            this.acceptFocus = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pb_splitter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_splitter2)).BeginInit();
            this.pnl_property.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.taskPane1)).BeginInit();
            this.taskPane1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.epd_backGround)).BeginInit();
            this.epd_backGround.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.epd_nodeProperty)).BeginInit();
            this.epd_nodeProperty.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.epd_lineProperty)).BeginInit();
            this.epd_lineProperty.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.epd_stringProperty)).BeginInit();
            this.epd_stringProperty.SuspendLayout();
            this.pnl_title.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_closePropertyForm)).BeginInit();
            this.pnl_tool.SuspendLayout();
            this.tablePanel.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_down)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_up)).BeginInit();
            this.pnl_tip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menus
            // 
            this.menus.ImageList = null;
            // 
            // menu
            // 
            this.menu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem10,
            this.menuItem11,
            this.menuItem1,
            this.menuItem4,
            this.menuItem2,
            this.menuItem3,
            this.menuItem5,
            this.menuItem7,
            this.menuItem8,
            this.menuItem9,
            this.menuItem12,
            this.menuItem6});
            // 
            // menuItem10
            // 
            this.menuItem10.Index = 0;
            this.menuItem10.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mi_newStartNode,
            this.menuItem13,
            this.menuItem14,
            this.menuItem15,
            this.menuItem16,
            this.menuItem17,
            this.menuItem18,
            this.menuItem19,
            this.menuItem20,
            this.menuItem21,
            this.menuItem22,
            this.menuItem24,
            this.menuItem23,
            this.menuItem25,
            this.mi_drawstring,
            this.mi_drawline,
            this.mi_line2});
            this.menuItem10.Text = "图元";
            // 
            // mi_newStartNode
            // 
            this.mi_newStartNode.Index = 0;
            this.mi_newStartNode.Text = "开始";
            this.mi_newStartNode.Click += new System.EventHandler(this.mi_newNode_Click);
            // 
            // menuItem13
            // 
            this.menuItem13.Index = 1;
            this.menuItem13.Text = "节点";
            this.menuItem13.Click += new System.EventHandler(this.mi_newNode_Click);
            // 
            // menuItem14
            // 
            this.menuItem14.Index = 2;
            this.menuItem14.Text = "特定操作";
            this.menuItem14.Click += new System.EventHandler(this.mi_newNode_Click);
            // 
            // menuItem15
            // 
            this.menuItem15.Index = 3;
            this.menuItem15.Text = "顺序";
            this.menuItem15.Click += new System.EventHandler(this.mi_newNode_Click);
            // 
            // menuItem16
            // 
            this.menuItem16.Index = 4;
            this.menuItem16.Text = "同步";
            this.menuItem16.Click += new System.EventHandler(this.mi_newNode_Click);
            // 
            // menuItem17
            // 
            this.menuItem17.Index = 5;
            this.menuItem17.Text = "分支";
            this.menuItem17.Click += new System.EventHandler(this.mi_newNode_Click);
            // 
            // menuItem18
            // 
            this.menuItem18.Index = 6;
            this.menuItem18.Text = "汇聚";
            this.menuItem18.Click += new System.EventHandler(this.mi_newNode_Click);
            // 
            // menuItem19
            // 
            this.menuItem19.Index = 7;
            this.menuItem19.Text = "汇总连接";
            this.menuItem19.Click += new System.EventHandler(this.mi_newNode_Click);
            // 
            // menuItem20
            // 
            this.menuItem20.Index = 8;
            this.menuItem20.Text = "判断";
            this.menuItem20.Click += new System.EventHandler(this.mi_newNode_Click);
            // 
            // menuItem21
            // 
            this.menuItem21.Index = 9;
            this.menuItem21.Text = "应用数据";
            this.menuItem21.Click += new System.EventHandler(this.mi_newNode_Click);
            // 
            // menuItem22
            // 
            this.menuItem22.Index = 10;
            this.menuItem22.Text = "结束";
            this.menuItem22.Click += new System.EventHandler(this.mi_newNode_Click);
            // 
            // menuItem24
            // 
            this.menuItem24.Index = 11;
            this.menuItem24.Text = "-";
            // 
            // menuItem23
            // 
            this.menuItem23.Index = 12;
            this.menuItem23.Text = "矩形工具";
            this.menuItem23.Click += new System.EventHandler(this.mi_newNode_Click);
            // 
            // menuItem25
            // 
            this.menuItem25.Index = 13;
            this.menuItem25.Text = "椭圆工具";
            this.menuItem25.Click += new System.EventHandler(this.mi_newNode_Click);
            // 
            // mi_drawstring
            // 
            this.mi_drawstring.Index = 14;
            this.mi_drawstring.Text = "写字工具";
            this.mi_drawstring.Click += new System.EventHandler(this.mi_drawstring_Click);
            // 
            // mi_drawline
            // 
            this.mi_drawline.Index = 15;
            this.mi_drawline.Text = "直线工具";
            this.mi_drawline.Click += new System.EventHandler(this.mi_drawline_Click);
            // 
            // mi_line2
            // 
            this.mi_line2.Index = 16;
            this.mi_line2.Text = "三折线工具";
            this.mi_line2.Click += new System.EventHandler(this.mi_line2_Click);
            // 
            // menuItem11
            // 
            this.menuItem11.Index = 1;
            this.menuItem11.Text = "-";
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 2;
            this.menuItem1.Shortcut = System.Windows.Forms.Shortcut.CtrlA;
            this.menuItem1.Text = "全选";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 3;
            this.menuItem4.Text = "-";
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 4;
            this.menuItem2.Shortcut = System.Windows.Forms.Shortcut.Del;
            this.menuItem2.Text = "删除";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 5;
            this.menuItem3.Shortcut = System.Windows.Forms.Shortcut.CtrlD;
            this.menuItem3.Text = "删除所有";
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 6;
            this.menuItem5.Text = "-";
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 7;
            this.menuItem7.Text = "置顶";
            this.menuItem7.Click += new System.EventHandler(this.menuItem7_Click);
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 8;
            this.menuItem8.Text = "置底";
            this.menuItem8.Click += new System.EventHandler(this.menuItem8_Click);
            // 
            // menuItem9
            // 
            this.menuItem9.Index = 9;
            this.menuItem9.Text = "-";
            // 
            // menuItem12
            // 
            this.menuItem12.Index = 10;
            this.menuItem12.Shortcut = System.Windows.Forms.Shortcut.CtrlB;
            this.menuItem12.Text = "图元属性";
            this.menuItem12.Click += new System.EventHandler(this.menuItem12_Click);
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 11;
            this.menuItem6.Shortcut = System.Windows.Forms.Shortcut.CtrlM;
            this.menuItem6.Text = "流程属性";
            this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
            // 
            // drawStringContent
            // 
            this.drawStringContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.drawStringContent.HideSelection = false;
            this.drawStringContent.Location = new System.Drawing.Point(-34, 0);
            this.drawStringContent.Multiline = true;
            this.drawStringContent.Name = "drawStringContent";
            this.drawStringContent.Size = new System.Drawing.Size(112, 24);
            this.drawStringContent.TabIndex = 4;
            this.drawStringContent.Text = "请输入内容。";
            this.drawStringContent.Visible = false;
            this.drawStringContent.KeyDown += new System.Windows.Forms.KeyEventHandler(this.inputContent_KeyDown);
            // 
            // TBnodeContent
            // 
            this.TBnodeContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TBnodeContent.Location = new System.Drawing.Point(-22, 24);
            this.TBnodeContent.Name = "TBnodeContent";
            this.TBnodeContent.Size = new System.Drawing.Size(100, 21);
            this.TBnodeContent.TabIndex = 5;
            this.TBnodeContent.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TBnodeContent.Visible = false;
            this.TBnodeContent.TextChanged += new System.EventHandler(this.TBnodeContent_TextChanged);
            this.TBnodeContent.KeyDown += new System.Windows.Forms.KeyEventHandler(this.inputContent_KeyDown);
            // 
            // testDrawLength
            // 
            this.testDrawLength.AutoSize = true;
            this.testDrawLength.Font = new System.Drawing.Font("宋体", 10F);
            this.testDrawLength.Location = new System.Drawing.Point(140, 8);
            this.testDrawLength.Name = "testDrawLength";
            this.testDrawLength.Size = new System.Drawing.Size(0, 14);
            this.testDrawLength.TabIndex = 7;
            this.testDrawLength.Visible = false;
            // 
            // mti_drawFoldLineTool
            // 
            this.mti_drawFoldLineTool.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(228)))), ((int)(((byte)(213)))));
            this.mti_drawFoldLineTool.ButtonForm = System.MyControl.eButtonForm.Rectangle;
            this.mti_drawFoldLineTool.HighlightColor = System.Drawing.Color.CornflowerBlue;
            this.mti_drawFoldLineTool.HottrackImage = null;
            this.toolTip.SetIconType(this.mti_drawFoldLineTool, System.MyControl.MyToolTip.ToolTipIconType.Information);
            this.mti_drawFoldLineTool.Location = new System.Drawing.Point(50, 362);
            this.mti_drawFoldLineTool.Name = "mti_drawFoldLineTool";
            this.mti_drawFoldLineTool.NormalImage = ((System.Drawing.Image)(resources.GetObject("mti_drawFoldLineTool.NormalImage")));
            this.mti_drawFoldLineTool.OnlyShowBitmap = false;
            this.mti_drawFoldLineTool.PressedImage = null;
            this.mti_drawFoldLineTool.Size = new System.Drawing.Size(40, 56);
            this.mti_drawFoldLineTool.TabIndex = 21;
            this.mti_drawFoldLineTool.TabStop = false;
            this.mti_drawFoldLineTool.Text = "三折线";
            this.mti_drawFoldLineTool.TextAlign = System.MyControl.eTextAlign.Bottom;
            this.mti_drawFoldLineTool.ToolTip = null;
            this.mti_drawFoldLineTool.DoubleClick += new System.EventHandler(this.mti_drawLineTool_DoubleClick);
            this.mti_drawFoldLineTool.Click += new System.EventHandler(this.line_Click);
            // 
            // mti_drawLineTool
            // 
            this.mti_drawLineTool.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(228)))), ((int)(((byte)(213)))));
            this.mti_drawLineTool.ButtonForm = System.MyControl.eButtonForm.Rectangle;
            this.mti_drawLineTool.HighlightColor = System.Drawing.Color.CornflowerBlue;
            this.mti_drawLineTool.HottrackImage = null;
            this.toolTip.SetIconType(this.mti_drawLineTool, System.MyControl.MyToolTip.ToolTipIconType.Information);
            this.mti_drawLineTool.Location = new System.Drawing.Point(4, 362);
            this.mti_drawLineTool.Name = "mti_drawLineTool";
            this.mti_drawLineTool.NormalImage = ((System.Drawing.Image)(resources.GetObject("mti_drawLineTool.NormalImage")));
            this.mti_drawLineTool.OnlyShowBitmap = false;
            this.mti_drawLineTool.PressedImage = null;
            this.mti_drawLineTool.Size = new System.Drawing.Size(40, 56);
            this.mti_drawLineTool.TabIndex = 20;
            this.mti_drawLineTool.TabStop = false;
            this.mti_drawLineTool.Text = "直线";
            this.mti_drawLineTool.TextAlign = System.MyControl.eTextAlign.Bottom;
            this.mti_drawLineTool.ToolTip = null;
            this.mti_drawLineTool.DoubleClick += new System.EventHandler(this.mti_drawLineTool_DoubleClick);
            this.mti_drawLineTool.Click += new System.EventHandler(this.line_Click);
            // 
            // mti_drawString
            // 
            this.mti_drawString.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(228)))), ((int)(((byte)(213)))));
            this.mti_drawString.ButtonForm = System.MyControl.eButtonForm.Rectangle;
            this.mti_drawString.HighlightColor = System.Drawing.Color.CornflowerBlue;
            this.mti_drawString.HottrackImage = null;
            this.toolTip.SetIconType(this.mti_drawString, System.MyControl.MyToolTip.ToolTipIconType.Information);
            this.mti_drawString.Location = new System.Drawing.Point(96, 362);
            this.mti_drawString.Name = "mti_drawString";
            this.mti_drawString.NormalImage = ((System.Drawing.Image)(resources.GetObject("mti_drawString.NormalImage")));
            this.mti_drawString.OnlyShowBitmap = false;
            this.mti_drawString.PressedImage = null;
            this.mti_drawString.Size = new System.Drawing.Size(40, 56);
            this.mti_drawString.TabIndex = 19;
            this.mti_drawString.TabStop = false;
            this.mti_drawString.Text = "写字";
            this.mti_drawString.TextAlign = System.MyControl.eTextAlign.Bottom;
            this.mti_drawString.ToolTip = null;
            this.mti_drawString.DoubleClick += new System.EventHandler(this.mti_drawString_DoubleClick);
            this.mti_drawString.Click += new System.EventHandler(this.button8_Click);
            // 
            // mti_Ellipse
            // 
            this.mti_Ellipse.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(228)))), ((int)(((byte)(213)))));
            this.mti_Ellipse.ButtonForm = System.MyControl.eButtonForm.Rectangle;
            this.mti_Ellipse.HighlightColor = System.Drawing.Color.CornflowerBlue;
            this.mti_Ellipse.HottrackImage = null;
            this.toolTip.SetIconType(this.mti_Ellipse, System.MyControl.MyToolTip.ToolTipIconType.Information);
            this.mti_Ellipse.Location = new System.Drawing.Point(96, 296);
            this.mti_Ellipse.Name = "mti_Ellipse";
            this.mti_Ellipse.NormalImage = ((System.Drawing.Image)(resources.GetObject("mti_Ellipse.NormalImage")));
            this.mti_Ellipse.OnlyShowBitmap = false;
            this.mti_Ellipse.PressedImage = null;
            this.mti_Ellipse.Size = new System.Drawing.Size(40, 56);
            this.mti_Ellipse.TabIndex = 18;
            this.mti_Ellipse.TabStop = false;
            this.mti_Ellipse.Text = "椭圆";
            this.mti_Ellipse.TextAlign = System.MyControl.eTextAlign.Bottom;
            this.mti_Ellipse.ToolTip = null;
            this.mti_Ellipse.DoubleClick += new System.EventHandler(this.drawNode_DoubleClick);
            this.mti_Ellipse.Click += new System.EventHandler(this.drawNode_Click);
            // 
            // mti_Rect
            // 
            this.mti_Rect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(228)))), ((int)(((byte)(213)))));
            this.mti_Rect.ButtonForm = System.MyControl.eButtonForm.Rectangle;
            this.mti_Rect.HighlightColor = System.Drawing.Color.CornflowerBlue;
            this.mti_Rect.HottrackImage = null;
            this.toolTip.SetIconType(this.mti_Rect, System.MyControl.MyToolTip.ToolTipIconType.Information);
            this.mti_Rect.Location = new System.Drawing.Point(50, 296);
            this.mti_Rect.Name = "mti_Rect";
            this.mti_Rect.NormalImage = ((System.Drawing.Image)(resources.GetObject("mti_Rect.NormalImage")));
            this.mti_Rect.OnlyShowBitmap = false;
            this.mti_Rect.PressedImage = null;
            this.mti_Rect.Size = new System.Drawing.Size(40, 56);
            this.mti_Rect.TabIndex = 17;
            this.mti_Rect.TabStop = false;
            this.mti_Rect.Text = "矩形";
            this.mti_Rect.TextAlign = System.MyControl.eTextAlign.Bottom;
            this.mti_Rect.ToolTip = null;
            this.mti_Rect.DoubleClick += new System.EventHandler(this.drawNode_DoubleClick);
            this.mti_Rect.Click += new System.EventHandler(this.drawNode_Click);
            // 
            // mti_endNode
            // 
            this.mti_endNode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(228)))), ((int)(((byte)(213)))));
            this.mti_endNode.ButtonForm = System.MyControl.eButtonForm.Rectangle;
            this.mti_endNode.HighlightColor = System.Drawing.Color.CornflowerBlue;
            this.mti_endNode.HottrackImage = null;
            this.toolTip.SetIconType(this.mti_endNode, System.MyControl.MyToolTip.ToolTipIconType.Information);
            this.mti_endNode.Location = new System.Drawing.Point(50, 208);
            this.mti_endNode.Name = "mti_endNode";
            this.mti_endNode.NormalImage = ((System.Drawing.Image)(resources.GetObject("mti_endNode.NormalImage")));
            this.mti_endNode.OnlyShowBitmap = false;
            this.mti_endNode.PressedImage = null;
            this.mti_endNode.Size = new System.Drawing.Size(40, 56);
            this.mti_endNode.TabIndex = 10;
            this.mti_endNode.TabStop = false;
            this.mti_endNode.Text = "结束";
            this.mti_endNode.TextAlign = System.MyControl.eTextAlign.Bottom;
            this.mti_endNode.ToolTip = null;
            this.mti_endNode.DoubleClick += new System.EventHandler(this.drawNode_DoubleClick);
            this.mti_endNode.Click += new System.EventHandler(this.drawNode_Click);
            // 
            // mti_DataNode
            // 
            this.mti_DataNode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(228)))), ((int)(((byte)(213)))));
            this.mti_DataNode.ButtonForm = System.MyControl.eButtonForm.Rectangle;
            this.mti_DataNode.HighlightColor = System.Drawing.Color.CornflowerBlue;
            this.mti_DataNode.HottrackImage = null;
            this.toolTip.SetIconType(this.mti_DataNode, System.MyControl.MyToolTip.ToolTipIconType.Information);
            this.mti_DataNode.Location = new System.Drawing.Point(4, 208);
            this.mti_DataNode.Name = "mti_DataNode";
            this.mti_DataNode.NormalImage = ((System.Drawing.Image)(resources.GetObject("mti_DataNode.NormalImage")));
            this.mti_DataNode.OnlyShowBitmap = false;
            this.mti_DataNode.PressedImage = null;
            this.mti_DataNode.Size = new System.Drawing.Size(40, 56);
            this.mti_DataNode.TabIndex = 9;
            this.mti_DataNode.TabStop = false;
            this.mti_DataNode.Text = "数据";
            this.mti_DataNode.TextAlign = System.MyControl.eTextAlign.Bottom;
            this.mti_DataNode.ToolTip = null;
            this.mti_DataNode.DoubleClick += new System.EventHandler(this.drawNode_DoubleClick);
            this.mti_DataNode.Click += new System.EventHandler(this.drawNode_Click);
            // 
            // mti_judgementNode
            // 
            this.mti_judgementNode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(228)))), ((int)(((byte)(213)))));
            this.mti_judgementNode.ButtonForm = System.MyControl.eButtonForm.Rectangle;
            this.mti_judgementNode.HighlightColor = System.Drawing.Color.CornflowerBlue;
            this.mti_judgementNode.HottrackImage = null;
            this.toolTip.SetIconType(this.mti_judgementNode, System.MyControl.MyToolTip.ToolTipIconType.Information);
            this.mti_judgementNode.Location = new System.Drawing.Point(96, 138);
            this.mti_judgementNode.Name = "mti_judgementNode";
            this.mti_judgementNode.NormalImage = ((System.Drawing.Image)(resources.GetObject("mti_judgementNode.NormalImage")));
            this.mti_judgementNode.OnlyShowBitmap = false;
            this.mti_judgementNode.PressedImage = null;
            this.mti_judgementNode.Size = new System.Drawing.Size(40, 56);
            this.mti_judgementNode.TabIndex = 8;
            this.mti_judgementNode.TabStop = false;
            this.mti_judgementNode.Text = "判断";
            this.mti_judgementNode.TextAlign = System.MyControl.eTextAlign.Bottom;
            this.mti_judgementNode.ToolTip = null;
            this.mti_judgementNode.DoubleClick += new System.EventHandler(this.drawNode_DoubleClick);
            this.mti_judgementNode.Click += new System.EventHandler(this.drawNode_Click);
            // 
            // mti_gatherNode
            // 
            this.mti_gatherNode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(228)))), ((int)(((byte)(213)))));
            this.mti_gatherNode.ButtonForm = System.MyControl.eButtonForm.Rectangle;
            this.mti_gatherNode.HighlightColor = System.Drawing.Color.CornflowerBlue;
            this.mti_gatherNode.HottrackImage = null;
            this.toolTip.SetIconType(this.mti_gatherNode, System.MyControl.MyToolTip.ToolTipIconType.Information);
            this.mti_gatherNode.Location = new System.Drawing.Point(50, 138);
            this.mti_gatherNode.Name = "mti_gatherNode";
            this.mti_gatherNode.NormalImage = ((System.Drawing.Image)(resources.GetObject("mti_gatherNode.NormalImage")));
            this.mti_gatherNode.OnlyShowBitmap = false;
            this.mti_gatherNode.PressedImage = null;
            this.mti_gatherNode.Size = new System.Drawing.Size(40, 56);
            this.mti_gatherNode.TabIndex = 7;
            this.mti_gatherNode.TabStop = false;
            this.mti_gatherNode.Text = "汇总";
            this.mti_gatherNode.TextAlign = System.MyControl.eTextAlign.Bottom;
            this.mti_gatherNode.ToolTip = null;
            this.mti_gatherNode.DoubleClick += new System.EventHandler(this.drawNode_DoubleClick);
            this.mti_gatherNode.Click += new System.EventHandler(this.drawNode_Click);
            // 
            // mti_convergeNode
            // 
            this.mti_convergeNode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(228)))), ((int)(((byte)(213)))));
            this.mti_convergeNode.ButtonForm = System.MyControl.eButtonForm.Rectangle;
            this.mti_convergeNode.HighlightColor = System.Drawing.Color.CornflowerBlue;
            this.mti_convergeNode.HottrackImage = null;
            this.toolTip.SetIconType(this.mti_convergeNode, System.MyControl.MyToolTip.ToolTipIconType.Information);
            this.mti_convergeNode.Location = new System.Drawing.Point(4, 138);
            this.mti_convergeNode.Name = "mti_convergeNode";
            this.mti_convergeNode.NormalImage = ((System.Drawing.Image)(resources.GetObject("mti_convergeNode.NormalImage")));
            this.mti_convergeNode.OnlyShowBitmap = false;
            this.mti_convergeNode.PressedImage = null;
            this.mti_convergeNode.Size = new System.Drawing.Size(40, 56);
            this.mti_convergeNode.TabIndex = 6;
            this.mti_convergeNode.TabStop = false;
            this.mti_convergeNode.Text = "汇聚";
            this.mti_convergeNode.TextAlign = System.MyControl.eTextAlign.Bottom;
            this.mti_convergeNode.ToolTip = null;
            this.mti_convergeNode.DoubleClick += new System.EventHandler(this.drawNode_DoubleClick);
            this.mti_convergeNode.Click += new System.EventHandler(this.drawNode_Click);
            // 
            // mti_asunderNode
            // 
            this.mti_asunderNode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(228)))), ((int)(((byte)(213)))));
            this.mti_asunderNode.ButtonForm = System.MyControl.eButtonForm.Rectangle;
            this.mti_asunderNode.HighlightColor = System.Drawing.Color.CornflowerBlue;
            this.mti_asunderNode.HottrackImage = null;
            this.toolTip.SetIconType(this.mti_asunderNode, System.MyControl.MyToolTip.ToolTipIconType.Information);
            this.mti_asunderNode.Location = new System.Drawing.Point(96, 80);
            this.mti_asunderNode.Name = "mti_asunderNode";
            this.mti_asunderNode.NormalImage = ((System.Drawing.Image)(resources.GetObject("mti_asunderNode.NormalImage")));
            this.mti_asunderNode.OnlyShowBitmap = false;
            this.mti_asunderNode.PressedImage = null;
            this.mti_asunderNode.Size = new System.Drawing.Size(40, 56);
            this.mti_asunderNode.TabIndex = 5;
            this.mti_asunderNode.TabStop = false;
            this.mti_asunderNode.Text = "分支";
            this.mti_asunderNode.TextAlign = System.MyControl.eTextAlign.Bottom;
            this.mti_asunderNode.ToolTip = null;
            this.mti_asunderNode.DoubleClick += new System.EventHandler(this.drawNode_DoubleClick);
            this.mti_asunderNode.Click += new System.EventHandler(this.drawNode_Click);
            // 
            // mti_synchronizationNode
            // 
            this.mti_synchronizationNode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(228)))), ((int)(((byte)(213)))));
            this.mti_synchronizationNode.ButtonForm = System.MyControl.eButtonForm.Rectangle;
            this.mti_synchronizationNode.HighlightColor = System.Drawing.Color.CornflowerBlue;
            this.mti_synchronizationNode.HottrackImage = null;
            this.toolTip.SetIconType(this.mti_synchronizationNode, System.MyControl.MyToolTip.ToolTipIconType.Information);
            this.mti_synchronizationNode.Location = new System.Drawing.Point(50, 80);
            this.mti_synchronizationNode.Name = "mti_synchronizationNode";
            this.mti_synchronizationNode.NormalImage = ((System.Drawing.Image)(resources.GetObject("mti_synchronizationNode.NormalImage")));
            this.mti_synchronizationNode.OnlyShowBitmap = false;
            this.mti_synchronizationNode.PressedImage = null;
            this.mti_synchronizationNode.Size = new System.Drawing.Size(40, 56);
            this.mti_synchronizationNode.TabIndex = 4;
            this.mti_synchronizationNode.TabStop = false;
            this.mti_synchronizationNode.Text = "同步";
            this.mti_synchronizationNode.TextAlign = System.MyControl.eTextAlign.Bottom;
            this.mti_synchronizationNode.ToolTip = null;
            this.mti_synchronizationNode.DoubleClick += new System.EventHandler(this.drawNode_DoubleClick);
            this.mti_synchronizationNode.Click += new System.EventHandler(this.drawNode_Click);
            // 
            // mti_gradationNode
            // 
            this.mti_gradationNode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(228)))), ((int)(((byte)(213)))));
            this.mti_gradationNode.ButtonForm = System.MyControl.eButtonForm.Rectangle;
            this.mti_gradationNode.HighlightColor = System.Drawing.Color.CornflowerBlue;
            this.mti_gradationNode.HottrackImage = null;
            this.toolTip.SetIconType(this.mti_gradationNode, System.MyControl.MyToolTip.ToolTipIconType.Information);
            this.mti_gradationNode.Location = new System.Drawing.Point(4, 80);
            this.mti_gradationNode.Name = "mti_gradationNode";
            this.mti_gradationNode.NormalImage = ((System.Drawing.Image)(resources.GetObject("mti_gradationNode.NormalImage")));
            this.mti_gradationNode.OnlyShowBitmap = false;
            this.mti_gradationNode.PressedImage = null;
            this.mti_gradationNode.Size = new System.Drawing.Size(40, 56);
            this.mti_gradationNode.TabIndex = 3;
            this.mti_gradationNode.TabStop = false;
            this.mti_gradationNode.Text = "顺序";
            this.mti_gradationNode.TextAlign = System.MyControl.eTextAlign.Bottom;
            this.toolTip.SetTipTitle(this.mti_gradationNode, "ssssssss");
            this.toolTip.SetToolTip(this.mti_gradationNode, "ss");
            this.mti_gradationNode.ToolTip = null;
            this.mti_gradationNode.DoubleClick += new System.EventHandler(this.drawNode_DoubleClick);
            this.mti_gradationNode.Click += new System.EventHandler(this.drawNode_Click);
            // 
            // mti_specificallyOperationNode
            // 
            this.mti_specificallyOperationNode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(228)))), ((int)(((byte)(213)))));
            this.mti_specificallyOperationNode.ButtonForm = System.MyControl.eButtonForm.Rectangle;
            this.mti_specificallyOperationNode.HighlightColor = System.Drawing.Color.CornflowerBlue;
            this.mti_specificallyOperationNode.HottrackImage = null;
            this.toolTip.SetIconType(this.mti_specificallyOperationNode, System.MyControl.MyToolTip.ToolTipIconType.Information);
            this.mti_specificallyOperationNode.Location = new System.Drawing.Point(96, 12);
            this.mti_specificallyOperationNode.Name = "mti_specificallyOperationNode";
            this.mti_specificallyOperationNode.NormalImage = ((System.Drawing.Image)(resources.GetObject("mti_specificallyOperationNode.NormalImage")));
            this.mti_specificallyOperationNode.OnlyShowBitmap = false;
            this.mti_specificallyOperationNode.PressedImage = null;
            this.mti_specificallyOperationNode.Size = new System.Drawing.Size(40, 56);
            this.mti_specificallyOperationNode.TabIndex = 2;
            this.mti_specificallyOperationNode.TabStop = false;
            this.mti_specificallyOperationNode.Text = "特定";
            this.mti_specificallyOperationNode.TextAlign = System.MyControl.eTextAlign.Bottom;
            this.mti_specificallyOperationNode.ToolTip = null;
            this.mti_specificallyOperationNode.DoubleClick += new System.EventHandler(this.drawNode_DoubleClick);
            this.mti_specificallyOperationNode.Click += new System.EventHandler(this.drawNode_Click);
            // 
            // mti_generalNode
            // 
            this.mti_generalNode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(228)))), ((int)(((byte)(213)))));
            this.mti_generalNode.ButtonForm = System.MyControl.eButtonForm.Rectangle;
            this.mti_generalNode.HighlightColor = System.Drawing.Color.CornflowerBlue;
            this.mti_generalNode.HottrackImage = null;
            this.toolTip.SetIconType(this.mti_generalNode, System.MyControl.MyToolTip.ToolTipIconType.Information);
            this.mti_generalNode.Location = new System.Drawing.Point(50, 12);
            this.mti_generalNode.Name = "mti_generalNode";
            this.mti_generalNode.NormalImage = ((System.Drawing.Image)(resources.GetObject("mti_generalNode.NormalImage")));
            this.mti_generalNode.OnlyShowBitmap = false;
            this.mti_generalNode.PressedImage = null;
            this.mti_generalNode.Size = new System.Drawing.Size(40, 56);
            this.mti_generalNode.TabIndex = 1;
            this.mti_generalNode.TabStop = false;
            this.mti_generalNode.Text = "任务";
            this.mti_generalNode.TextAlign = System.MyControl.eTextAlign.Bottom;
            this.mti_generalNode.ToolTip = null;
            this.mti_generalNode.DoubleClick += new System.EventHandler(this.drawNode_DoubleClick);
            this.mti_generalNode.Click += new System.EventHandler(this.drawNode_Click);
            // 
            // mti_startNode
            // 
            this.mti_startNode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(228)))), ((int)(((byte)(213)))));
            this.mti_startNode.ButtonForm = System.MyControl.eButtonForm.Rectangle;
            this.mti_startNode.HighlightColor = System.Drawing.Color.CornflowerBlue;
            this.mti_startNode.HottrackImage = null;
            this.toolTip.SetIconType(this.mti_startNode, System.MyControl.MyToolTip.ToolTipIconType.Information);
            this.mti_startNode.Location = new System.Drawing.Point(4, 12);
            this.mti_startNode.Name = "mti_startNode";
            this.mti_startNode.NormalImage = ((System.Drawing.Image)(resources.GetObject("mti_startNode.NormalImage")));
            this.mti_startNode.OnlyShowBitmap = false;
            this.mti_startNode.PressedImage = null;
            this.mti_startNode.Size = new System.Drawing.Size(40, 56);
            this.mti_startNode.TabIndex = 0;
            this.mti_startNode.TabStop = false;
            this.mti_startNode.Text = "开始";
            this.mti_startNode.TextAlign = System.MyControl.eTextAlign.Bottom;
            this.mti_startNode.ToolTip = null;
            this.mti_startNode.DoubleClick += new System.EventHandler(this.drawNode_DoubleClick);
            this.mti_startNode.Click += new System.EventHandler(this.drawNode_Click);
            // 
            // pb_splitter
            // 
            this.pb_splitter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(183)))), ((int)(((byte)(57)))));
            this.pb_splitter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pb_splitter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pb_splitter.Dock = System.Windows.Forms.DockStyle.Left;
            this.pb_splitter.Image = ((System.Drawing.Image)(resources.GetObject("pb_splitter.Image")));
            this.pb_splitter.Location = new System.Drawing.Point(0, 0);
            this.pb_splitter.Name = "pb_splitter";
            this.pb_splitter.Size = new System.Drawing.Size(6, 480);
            this.pb_splitter.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pb_splitter.TabIndex = 35;
            this.pb_splitter.TabStop = false;
            this.toolTip.SetTipTitle(this.pb_splitter, "分隔线");
            this.toolTip.SetToolTip(this.pb_splitter, "可以控制隐藏或显示结点工具属性信息，调整画布区域的大小.");
            this.pb_splitter.Click += new System.EventHandler(this.pb_splitter_Click);
            // 
            // dataGrid1
            // 
            this.dataGrid1.AllowSorting = false;
            this.dataGrid1.AlternatingBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(228)))), ((int)(((byte)(213)))));
            this.dataGrid1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(220)))), ((int)(((byte)(167)))));
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(228)))), ((int)(((byte)(213)))));
            this.dataGrid1.CaptionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(186)))), ((int)(((byte)(95)))));
            this.dataGrid1.CaptionForeColor = System.Drawing.Color.MidnightBlue;
            this.dataGrid1.DataMember = "";
            this.dataGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid1.Font = new System.Drawing.Font("Tahoma", 8F);
            this.dataGrid1.ForeColor = System.Drawing.Color.Black;
            this.dataGrid1.GridLineColor = System.Drawing.Color.Black;
            this.dataGrid1.HeaderBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dataGrid1.HeaderFont = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.dataGrid1.HeaderForeColor = System.Drawing.Color.WhiteSmoke;
            this.toolTip.SetIconType(this.dataGrid1, System.MyControl.MyToolTip.ToolTipIconType.Information);
            this.dataGrid1.LinkColor = System.Drawing.Color.Teal;
            this.dataGrid1.Location = new System.Drawing.Point(0, 0);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.ParentRowsBackColor = System.Drawing.Color.Gainsboro;
            this.dataGrid1.ParentRowsForeColor = System.Drawing.Color.MidnightBlue;
            this.dataGrid1.ReadOnly = true;
            this.dataGrid1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(186)))), ((int)(((byte)(95)))));
            this.dataGrid1.SelectionForeColor = System.Drawing.Color.WhiteSmoke;
            this.dataGrid1.Size = new System.Drawing.Size(508, 226);
            this.dataGrid1.TabIndex = 48;
            this.toolTip.SetTipTitle(this.dataGrid1, "即时表格");
            this.toolTip.SetToolTip(this.dataGrid1, "流程图所连对象可以在这即时表现。");
            this.dataGrid1.CurrentCellChanged += new System.EventHandler(this.dataGrid1_CurrentCellChanged);
            // 
            // mti_Cursor
            // 
            this.mti_Cursor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(228)))), ((int)(((byte)(213)))));
            this.mti_Cursor.ButtonForm = System.MyControl.eButtonForm.Rectangle;
            this.mti_Cursor.HighlightColor = System.Drawing.Color.CornflowerBlue;
            this.mti_Cursor.HottrackImage = null;
            this.toolTip.SetIconType(this.mti_Cursor, System.MyControl.MyToolTip.ToolTipIconType.Information);
            this.mti_Cursor.Location = new System.Drawing.Point(4, 296);
            this.mti_Cursor.Name = "mti_Cursor";
            this.mti_Cursor.NormalImage = ((System.Drawing.Image)(resources.GetObject("mti_Cursor.NormalImage")));
            this.mti_Cursor.OnlyShowBitmap = false;
            this.mti_Cursor.PressedImage = null;
            this.mti_Cursor.Size = new System.Drawing.Size(40, 56);
            this.mti_Cursor.TabIndex = 22;
            this.mti_Cursor.TabStop = false;
            this.mti_Cursor.Text = "指针";
            this.mti_Cursor.TextAlign = System.MyControl.eTextAlign.Bottom;
            this.mti_Cursor.ToolTip = null;
            this.mti_Cursor.Click += new System.EventHandler(this.mti_Cursor_Click);
            // 
            // pb_splitter2
            // 
            this.pb_splitter2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(183)))), ((int)(((byte)(57)))));
            this.pb_splitter2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pb_splitter2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pb_splitter2.Dock = System.Windows.Forms.DockStyle.Right;
            this.pb_splitter2.Image = ((System.Drawing.Image)(resources.GetObject("pb_splitter2.Image")));
            this.pb_splitter2.Location = new System.Drawing.Point(140, 1);
            this.pb_splitter2.Name = "pb_splitter2";
            this.pb_splitter2.Size = new System.Drawing.Size(6, 479);
            this.pb_splitter2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pb_splitter2.TabIndex = 24;
            this.pb_splitter2.TabStop = false;
            this.toolTip.SetTipTitle(this.pb_splitter2, "分隔条");
            this.toolTip.SetToolTip(this.pb_splitter2, "可以控制隐藏或显示图元工具栏，调整画布区域的大小.");
            this.pb_splitter2.Click += new System.EventHandler(this.pb_splitter2_Click);
            // 
            // pnl_property
            // 
            this.pnl_property.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(229)))), ((int)(((byte)(252)))));
            this.pnl_property.Controls.Add(this.taskPane1);
            this.pnl_property.Controls.Add(this.pnl_title);
            this.pnl_property.Controls.Add(this.pb_splitter);
            this.pnl_property.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnl_property.Location = new System.Drawing.Point(988, 0);
            this.pnl_property.Name = "pnl_property";
            this.pnl_property.Size = new System.Drawing.Size(6, 480);
            this.pnl_property.TabIndex = 18;
            // 
            // taskPane1
            // 
            this.taskPane1.AutoScrollMargin = new System.Drawing.Size(12, 12);
            this.taskPane1.CustomSettings.GradientDirection = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.taskPane1.CustomSettings.GradientEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(228)))), ((int)(((byte)(213)))));
            this.taskPane1.CustomSettings.GradientStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(228)))), ((int)(((byte)(213)))));
            this.taskPane1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.taskPane1.Expandos.AddRange(new System.MyControl.MyXPExplorerBar.Expando[] {
            this.epd_backGround,
            this.epd_nodeProperty,
            this.epd_lineProperty,
            this.epd_stringProperty});
            this.taskPane1.Location = new System.Drawing.Point(6, 20);
            this.taskPane1.Name = "taskPane1";
            this.taskPane1.Size = new System.Drawing.Size(0, 460);
            this.taskPane1.TabIndex = 37;
            this.taskPane1.Text = "taskPane1";
            // 
            // epd_backGround
            // 
            this.epd_backGround.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.epd_backGround.CustomHeaderSettings.NormalBorderColor = System.Drawing.Color.Green;
            this.epd_backGround.CustomHeaderSettings.NormalGradientEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(185)))), ((int)(((byte)(147)))));
            this.epd_backGround.CustomHeaderSettings.NormalGradientStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(185)))), ((int)(((byte)(147)))));
            this.epd_backGround.CustomHeaderSettings.SpecialBorderColor = System.Drawing.Color.Green;
            this.epd_backGround.CustomHeaderSettings.SpecialGradientEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(185)))), ((int)(((byte)(147)))));
            this.epd_backGround.CustomHeaderSettings.SpecialGradientStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(185)))), ((int)(((byte)(147)))));
            this.epd_backGround.CustomHeaderSettings.TitleGradient = true;
            this.epd_backGround.CustomSettings.NormalBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(228)))), ((int)(((byte)(213)))));
            this.epd_backGround.CustomSettings.NormalBorderColor = System.Drawing.Color.Green;
            this.epd_backGround.CustomSettings.SpecialBackColor = System.Drawing.Color.White;
            this.epd_backGround.CustomSettings.SpecialBorderColor = System.Drawing.Color.White;
            this.epd_backGround.ExpandedHeight = 212;
            this.epd_backGround.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.epd_backGround.Items.AddRange(new System.Windows.Forms.Control[] {
            this.BG_Style,
            this.label1,
            this.cb_integrality,
            this.cb_limit,
            this.lbl_checkIntegrality,
            this.cb_isAlpha});
            this.epd_backGround.Location = new System.Drawing.Point(12, 12);
            this.epd_backGround.Name = "epd_backGround";
            this.epd_backGround.Size = new System.Drawing.Size(0, 212);
            this.epd_backGround.TabIndex = 0;
            this.epd_backGround.Text = "背景";
            // 
            // BG_Style
            // 
            this.BG_Style.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.BG_Style.Items.AddRange(new object[] {
            "背景1",
            "背景2",
            "背景3",
            "背景4",
            "背景5"});
            this.BG_Style.Location = new System.Drawing.Point(8, 56);
            this.BG_Style.Name = "BG_Style";
            this.BG_Style.Size = new System.Drawing.Size(144, 21);
            this.BG_Style.TabIndex = 0;
            this.BG_Style.SelectedIndexChanged += new System.EventHandler(this.BG_Style_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "背景网格:";
            // 
            // cb_integrality
            // 
            this.cb_integrality.BackColor = System.Drawing.Color.Transparent;
            this.cb_integrality.Location = new System.Drawing.Point(8, 112);
            this.cb_integrality.Name = "cb_integrality";
            this.cb_integrality.Size = new System.Drawing.Size(134, 24);
            this.cb_integrality.TabIndex = 2;
            this.cb_integrality.Text = "检测流程图完整性";
            this.cb_integrality.UseVisualStyleBackColor = false;
            this.cb_integrality.CheckedChanged += new System.EventHandler(this.cb_integrality_CheckedChanged);
            // 
            // cb_limit
            // 
            this.cb_limit.BackColor = System.Drawing.Color.Transparent;
            this.cb_limit.Location = new System.Drawing.Point(8, 84);
            this.cb_limit.Name = "cb_limit";
            this.cb_limit.Size = new System.Drawing.Size(134, 24);
            this.cb_limit.TabIndex = 3;
            this.cb_limit.Text = "限制图元个数";
            this.cb_limit.UseVisualStyleBackColor = false;
            this.cb_limit.CheckedChanged += new System.EventHandler(this.cb_limit_CheckedChanged);
            // 
            // lbl_checkIntegrality
            // 
            this.lbl_checkIntegrality.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl_checkIntegrality.Image = ((System.Drawing.Image)(resources.GetObject("lbl_checkIntegrality.Image")));
            this.lbl_checkIntegrality.Location = new System.Drawing.Point(8, 176);
            this.lbl_checkIntegrality.Name = "lbl_checkIntegrality";
            this.lbl_checkIntegrality.Size = new System.Drawing.Size(78, 23);
            this.lbl_checkIntegrality.TabIndex = 4;
            this.lbl_checkIntegrality.Text = "检测完整性";
            this.lbl_checkIntegrality.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_checkIntegrality.Click += new System.EventHandler(this.lbl_checkIntegrality_Click);
            // 
            // cb_isAlpha
            // 
            this.cb_isAlpha.BackColor = System.Drawing.Color.Transparent;
            this.cb_isAlpha.Checked = true;
            this.cb_isAlpha.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_isAlpha.Location = new System.Drawing.Point(8, 140);
            this.cb_isAlpha.Name = "cb_isAlpha";
            this.cb_isAlpha.Size = new System.Drawing.Size(134, 24);
            this.cb_isAlpha.TabIndex = 5;
            this.cb_isAlpha.Text = "图元透明";
            this.cb_isAlpha.UseVisualStyleBackColor = false;
            this.cb_isAlpha.CheckedChanged += new System.EventHandler(this.cb_isAlpha_CheckedChanged);
            // 
            // epd_nodeProperty
            // 
            this.epd_nodeProperty.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.epd_nodeProperty.CustomHeaderSettings.NormalGradientEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(185)))), ((int)(((byte)(147)))));
            this.epd_nodeProperty.CustomHeaderSettings.NormalGradientStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(185)))), ((int)(((byte)(147)))));
            this.epd_nodeProperty.CustomHeaderSettings.SpecialGradientEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(185)))), ((int)(((byte)(147)))));
            this.epd_nodeProperty.CustomHeaderSettings.SpecialGradientStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(185)))), ((int)(((byte)(147)))));
            this.epd_nodeProperty.CustomHeaderSettings.TitleGradient = true;
            this.epd_nodeProperty.CustomSettings.NormalBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(228)))), ((int)(((byte)(213)))));
            this.epd_nodeProperty.CustomSettings.NormalBorderColor = System.Drawing.Color.Green;
            this.epd_nodeProperty.CustomSettings.SpecialBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(228)))), ((int)(((byte)(213)))));
            this.epd_nodeProperty.ExpandedHeight = 280;
            this.epd_nodeProperty.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.epd_nodeProperty.Items.AddRange(new System.Windows.Forms.Control[] {
            this.label2,
            this.node_ID,
            this.node_Type,
            this.label3,
            this.node_X,
            this.label4,
            this.node_Y,
            this.label5,
            this.node_Height,
            this.label6,
            this.node_Width,
            this.label7,
            this.label8,
            this.node_Name,
            this.label10,
            this.node_Size,
            this.label9,
            this.label11,
            this.label12,
            this.node_Font_Color,
            this.node_Border_Color,
            this.node_Fill_Color});
            this.epd_nodeProperty.Location = new System.Drawing.Point(12, 236);
            this.epd_nodeProperty.Name = "epd_nodeProperty";
            this.epd_nodeProperty.Size = new System.Drawing.Size(0, 280);
            this.epd_nodeProperty.TabIndex = 1;
            this.epd_nodeProperty.Text = "结点属性";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "结点ID:";
            // 
            // node_ID
            // 
            this.node_ID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.node_ID.Location = new System.Drawing.Point(66, 30);
            this.node_ID.Name = "node_ID";
            this.node_ID.ReadOnly = true;
            this.node_ID.Size = new System.Drawing.Size(90, 21);
            this.node_ID.TabIndex = 1;
            // 
            // node_Type
            // 
            this.node_Type.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.node_Type.Enabled = false;
            this.node_Type.Location = new System.Drawing.Point(66, 58);
            this.node_Type.Name = "node_Type";
            this.node_Type.ReadOnly = true;
            this.node_Type.Size = new System.Drawing.Size(90, 21);
            this.node_Type.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "结点类型:";
            // 
            // node_X
            // 
            this.node_X.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.node_X.Location = new System.Drawing.Point(44, 86);
            this.node_X.Name = "node_X";
            this.node_X.Size = new System.Drawing.Size(34, 21);
            this.node_X.TabIndex = 5;
            this.node_X.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Location_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Left:";
            // 
            // node_Y
            // 
            this.node_Y.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.node_Y.Location = new System.Drawing.Point(118, 86);
            this.node_Y.Name = "node_Y";
            this.node_Y.Size = new System.Drawing.Size(34, 21);
            this.node_Y.TabIndex = 7;
            this.node_Y.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Location_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(88, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Top:";
            // 
            // node_Height
            // 
            this.node_Height.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.node_Height.Location = new System.Drawing.Point(118, 116);
            this.node_Height.Name = "node_Height";
            this.node_Height.Size = new System.Drawing.Size(34, 21);
            this.node_Height.TabIndex = 11;
            this.node_Height.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Location_KeyDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(78, 118);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Height:";
            // 
            // node_Width
            // 
            this.node_Width.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.node_Width.Location = new System.Drawing.Point(44, 116);
            this.node_Width.Name = "node_Width";
            this.node_Width.Size = new System.Drawing.Size(34, 21);
            this.node_Width.TabIndex = 9;
            this.node_Width.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Location_KeyDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 118);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Width:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 146);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "结点名称:";
            // 
            // node_Name
            // 
            this.node_Name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.node_Name.Location = new System.Drawing.Point(66, 144);
            this.node_Name.Name = "node_Name";
            this.node_Name.Size = new System.Drawing.Size(90, 21);
            this.node_Name.TabIndex = 13;
            this.node_Name.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Location_KeyDown);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 174);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 13);
            this.label10.TabIndex = 16;
            this.label10.Text = "字体大小:";
            // 
            // node_Size
            // 
            this.node_Size.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.node_Size.Location = new System.Drawing.Point(66, 172);
            this.node_Size.Name = "node_Size";
            this.node_Size.Size = new System.Drawing.Size(90, 21);
            this.node_Size.TabIndex = 17;
            this.node_Size.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Location_KeyDown);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 202);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "字体颜色:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 230);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 13);
            this.label11.TabIndex = 20;
            this.label11.Text = "边框颜色:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 258);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(59, 13);
            this.label12.TabIndex = 22;
            this.label12.Text = "填充颜色:";
            // 
            // node_Font_Color
            // 
            this.node_Font_Color.BackColor = System.Drawing.Color.Black;
            this.node_Font_Color.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.node_Font_Color.Location = new System.Drawing.Point(66, 200);
            this.node_Font_Color.Name = "node_Font_Color";
            this.node_Font_Color.Size = new System.Drawing.Size(90, 18);
            this.node_Font_Color.TabIndex = 40;
            this.node_Font_Color.BackColorChanged += new System.EventHandler(this.Plant_BackColorChanged);
            this.node_Font_Color.Click += new System.EventHandler(this.ColorChange);
            // 
            // node_Border_Color
            // 
            this.node_Border_Color.BackColor = System.Drawing.Color.Black;
            this.node_Border_Color.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.node_Border_Color.Location = new System.Drawing.Point(66, 228);
            this.node_Border_Color.Name = "node_Border_Color";
            this.node_Border_Color.Size = new System.Drawing.Size(90, 18);
            this.node_Border_Color.TabIndex = 41;
            this.node_Border_Color.BackColorChanged += new System.EventHandler(this.Plant_BackColorChanged);
            this.node_Border_Color.Click += new System.EventHandler(this.ColorChange);
            // 
            // node_Fill_Color
            // 
            this.node_Fill_Color.BackColor = System.Drawing.Color.Black;
            this.node_Fill_Color.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.node_Fill_Color.Location = new System.Drawing.Point(66, 254);
            this.node_Fill_Color.Name = "node_Fill_Color";
            this.node_Fill_Color.Size = new System.Drawing.Size(90, 18);
            this.node_Fill_Color.TabIndex = 42;
            this.node_Fill_Color.BackColorChanged += new System.EventHandler(this.Plant_BackColorChanged);
            this.node_Fill_Color.Click += new System.EventHandler(this.ColorChange);
            // 
            // epd_lineProperty
            // 
            this.epd_lineProperty.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.epd_lineProperty.CustomHeaderSettings.NormalGradientEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(185)))), ((int)(((byte)(147)))));
            this.epd_lineProperty.CustomHeaderSettings.NormalGradientStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(185)))), ((int)(((byte)(147)))));
            this.epd_lineProperty.CustomHeaderSettings.SpecialGradientEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(185)))), ((int)(((byte)(147)))));
            this.epd_lineProperty.CustomHeaderSettings.SpecialGradientStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(185)))), ((int)(((byte)(147)))));
            this.epd_lineProperty.CustomHeaderSettings.TitleGradient = true;
            this.epd_lineProperty.CustomSettings.NormalBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(228)))), ((int)(((byte)(213)))));
            this.epd_lineProperty.CustomSettings.NormalBorderColor = System.Drawing.Color.Green;
            this.epd_lineProperty.CustomSettings.SpecialBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(228)))), ((int)(((byte)(213)))));
            this.epd_lineProperty.ExpandedHeight = 278;
            this.epd_lineProperty.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.epd_lineProperty.Items.AddRange(new System.Windows.Forms.Control[] {
            this.label13,
            this.line_size,
            this.label14,
            this.label16,
            this.label17,
            this.line_FirNode_ID,
            this.label18,
            this.line_ID,
            this.line_Type,
            this.label19,
            this.line_X0,
            this.label20,
            this.line_Y0,
            this.label21,
            this.line_Y1,
            this.label22,
            this.line_X1,
            this.label23,
            this.line_SecNode_ID,
            this.line_color,
            this.label25,
            this.tb_lineName});
            this.epd_lineProperty.Location = new System.Drawing.Point(12, 528);
            this.epd_lineProperty.Name = "epd_lineProperty";
            this.epd_lineProperty.Size = new System.Drawing.Size(0, 278);
            this.epd_lineProperty.TabIndex = 2;
            this.epd_lineProperty.Text = "线属性";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(32, 228);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(35, 13);
            this.label13.TabIndex = 44;
            this.label13.Text = "粗细:";
            // 
            // line_size
            // 
            this.line_size.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.line_size.Location = new System.Drawing.Point(67, 224);
            this.line_size.Name = "line_size";
            this.line_size.Size = new System.Drawing.Size(90, 21);
            this.line_size.TabIndex = 45;
            this.line_size.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Location_KeyDown);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 202);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(59, 13);
            this.label14.TabIndex = 42;
            this.label14.Text = "线条颜色:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(20, 176);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(46, 13);
            this.label16.TabIndex = 38;
            this.label16.Text = "线尾ID:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(20, 146);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(46, 13);
            this.label17.TabIndex = 36;
            this.label17.Text = "线头ID:";
            // 
            // line_FirNode_ID
            // 
            this.line_FirNode_ID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.line_FirNode_ID.Enabled = false;
            this.line_FirNode_ID.Location = new System.Drawing.Point(67, 144);
            this.line_FirNode_ID.Name = "line_FirNode_ID";
            this.line_FirNode_ID.ReadOnly = true;
            this.line_FirNode_ID.Size = new System.Drawing.Size(90, 21);
            this.line_FirNode_ID.TabIndex = 37;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(32, 36);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(34, 13);
            this.label18.TabIndex = 24;
            this.label18.Text = "线ID:";
            // 
            // line_ID
            // 
            this.line_ID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.line_ID.Enabled = false;
            this.line_ID.Location = new System.Drawing.Point(66, 32);
            this.line_ID.Name = "line_ID";
            this.line_ID.ReadOnly = true;
            this.line_ID.Size = new System.Drawing.Size(90, 21);
            this.line_ID.TabIndex = 25;
            // 
            // line_Type
            // 
            this.line_Type.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.line_Type.Enabled = false;
            this.line_Type.Location = new System.Drawing.Point(66, 60);
            this.line_Type.Name = "line_Type";
            this.line_Type.ReadOnly = true;
            this.line_Type.Size = new System.Drawing.Size(90, 21);
            this.line_Type.TabIndex = 27;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(18, 64);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(47, 13);
            this.label19.TabIndex = 26;
            this.label19.Text = "线类型:";
            // 
            // line_X0
            // 
            this.line_X0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.line_X0.Location = new System.Drawing.Point(45, 88);
            this.line_X0.Name = "line_X0";
            this.line_X0.Size = new System.Drawing.Size(34, 21);
            this.line_X0.TabIndex = 29;
            this.line_X0.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Location_KeyDown);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(15, 88);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(23, 13);
            this.label20.TabIndex = 28;
            this.label20.Text = "X1:";
            // 
            // line_Y0
            // 
            this.line_Y0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.line_Y0.Location = new System.Drawing.Point(119, 88);
            this.line_Y0.Name = "line_Y0";
            this.line_Y0.Size = new System.Drawing.Size(34, 21);
            this.line_Y0.TabIndex = 31;
            this.line_Y0.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Location_KeyDown);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(89, 88);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(23, 13);
            this.label21.TabIndex = 30;
            this.label21.Text = "Y1:";
            // 
            // line_Y1
            // 
            this.line_Y1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.line_Y1.Location = new System.Drawing.Point(119, 116);
            this.line_Y1.Name = "line_Y1";
            this.line_Y1.Size = new System.Drawing.Size(34, 21);
            this.line_Y1.TabIndex = 35;
            this.line_Y1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Location_KeyDown);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(90, 116);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(23, 13);
            this.label22.TabIndex = 34;
            this.label22.Text = "Y2:";
            // 
            // line_X1
            // 
            this.line_X1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.line_X1.Location = new System.Drawing.Point(45, 116);
            this.line_X1.Name = "line_X1";
            this.line_X1.Size = new System.Drawing.Size(34, 21);
            this.line_X1.TabIndex = 33;
            this.line_X1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Location_KeyDown);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(16, 116);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(23, 13);
            this.label23.TabIndex = 32;
            this.label23.Text = "X2:";
            // 
            // line_SecNode_ID
            // 
            this.line_SecNode_ID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.line_SecNode_ID.Enabled = false;
            this.line_SecNode_ID.Location = new System.Drawing.Point(67, 172);
            this.line_SecNode_ID.Name = "line_SecNode_ID";
            this.line_SecNode_ID.ReadOnly = true;
            this.line_SecNode_ID.Size = new System.Drawing.Size(90, 21);
            this.line_SecNode_ID.TabIndex = 46;
            // 
            // line_color
            // 
            this.line_color.BackColor = System.Drawing.Color.Black;
            this.line_color.Location = new System.Drawing.Point(68, 200);
            this.line_color.Name = "line_color";
            this.line_color.Size = new System.Drawing.Size(90, 18);
            this.line_color.TabIndex = 47;
            this.line_color.BackColorChanged += new System.EventHandler(this.Plant_BackColorChanged);
            this.line_color.Click += new System.EventHandler(this.ColorChange);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(20, 256);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(47, 13);
            this.label25.TabIndex = 48;
            this.label25.Text = "线名称:";
            // 
            // tb_lineName
            // 
            this.tb_lineName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_lineName.Location = new System.Drawing.Point(68, 252);
            this.tb_lineName.Name = "tb_lineName";
            this.tb_lineName.Size = new System.Drawing.Size(90, 21);
            this.tb_lineName.TabIndex = 49;
            this.tb_lineName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Location_KeyDown);
            // 
            // epd_stringProperty
            // 
            this.epd_stringProperty.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.epd_stringProperty.CustomHeaderSettings.NormalGradientEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(185)))), ((int)(((byte)(147)))));
            this.epd_stringProperty.CustomHeaderSettings.NormalGradientStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(185)))), ((int)(((byte)(147)))));
            this.epd_stringProperty.CustomHeaderSettings.SpecialGradientEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(185)))), ((int)(((byte)(147)))));
            this.epd_stringProperty.CustomHeaderSettings.SpecialGradientStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(185)))), ((int)(((byte)(147)))));
            this.epd_stringProperty.CustomHeaderSettings.TitleGradient = true;
            this.epd_stringProperty.CustomSettings.NormalBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(228)))), ((int)(((byte)(213)))));
            this.epd_stringProperty.CustomSettings.NormalBorderColor = System.Drawing.Color.Green;
            this.epd_stringProperty.CustomSettings.SpecialBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(228)))), ((int)(((byte)(213)))));
            this.epd_stringProperty.ExpandedHeight = 250;
            this.epd_stringProperty.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.epd_stringProperty.Items.AddRange(new System.Windows.Forms.Control[] {
            this.label27,
            this.DS_ID,
            this.DS_Content,
            this.label28,
            this.DS_X,
            this.label29,
            this.DS_Y,
            this.label30,
            this.DS_Height,
            this.label31,
            this.DS_Width,
            this.label32,
            this.label15,
            this.DS_Size,
            this.label24,
            this.DS_Color});
            this.epd_stringProperty.Location = new System.Drawing.Point(12, 818);
            this.epd_stringProperty.Name = "epd_stringProperty";
            this.epd_stringProperty.Size = new System.Drawing.Size(0, 250);
            this.epd_stringProperty.TabIndex = 3;
            this.epd_stringProperty.Text = "文字属性";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(20, 36);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(46, 13);
            this.label27.TabIndex = 24;
            this.label27.Text = "文字ID:";
            // 
            // DS_ID
            // 
            this.DS_ID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DS_ID.Location = new System.Drawing.Point(66, 32);
            this.DS_ID.Name = "DS_ID";
            this.DS_ID.ReadOnly = true;
            this.DS_ID.Size = new System.Drawing.Size(90, 21);
            this.DS_ID.TabIndex = 25;
            // 
            // DS_Content
            // 
            this.DS_Content.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DS_Content.Location = new System.Drawing.Point(66, 60);
            this.DS_Content.Name = "DS_Content";
            this.DS_Content.Size = new System.Drawing.Size(90, 21);
            this.DS_Content.TabIndex = 27;
            this.DS_Content.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Location_KeyDown);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(6, 64);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(59, 13);
            this.label28.TabIndex = 26;
            this.label28.Text = "文字内容:";
            // 
            // DS_X
            // 
            this.DS_X.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DS_X.Location = new System.Drawing.Point(44, 88);
            this.DS_X.Name = "DS_X";
            this.DS_X.Size = new System.Drawing.Size(34, 21);
            this.DS_X.TabIndex = 29;
            this.DS_X.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Location_KeyDown);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(15, 88);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(23, 13);
            this.label29.TabIndex = 28;
            this.label29.Text = "X1:";
            // 
            // DS_Y
            // 
            this.DS_Y.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DS_Y.Location = new System.Drawing.Point(119, 88);
            this.DS_Y.Name = "DS_Y";
            this.DS_Y.Size = new System.Drawing.Size(34, 21);
            this.DS_Y.TabIndex = 31;
            this.DS_Y.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Location_KeyDown);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(89, 88);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(23, 13);
            this.label30.TabIndex = 30;
            this.label30.Text = "Y1:";
            // 
            // DS_Height
            // 
            this.DS_Height.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DS_Height.Location = new System.Drawing.Point(119, 116);
            this.DS_Height.Name = "DS_Height";
            this.DS_Height.Size = new System.Drawing.Size(34, 21);
            this.DS_Height.TabIndex = 35;
            this.DS_Height.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Location_KeyDown);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(78, 116);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(42, 13);
            this.label31.TabIndex = 34;
            this.label31.Text = "Height:";
            // 
            // DS_Width
            // 
            this.DS_Width.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DS_Width.Location = new System.Drawing.Point(44, 116);
            this.DS_Width.Name = "DS_Width";
            this.DS_Width.Size = new System.Drawing.Size(34, 21);
            this.DS_Width.TabIndex = 33;
            this.DS_Width.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Location_KeyDown);
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(8, 116);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(39, 13);
            this.label32.TabIndex = 32;
            this.label32.Text = "Width:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(4, 146);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(59, 13);
            this.label15.TabIndex = 36;
            this.label15.Text = "字体大小:";
            // 
            // DS_Size
            // 
            this.DS_Size.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DS_Size.Location = new System.Drawing.Point(64, 144);
            this.DS_Size.Name = "DS_Size";
            this.DS_Size.Size = new System.Drawing.Size(90, 21);
            this.DS_Size.TabIndex = 37;
            this.DS_Size.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Location_KeyDown);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(4, 176);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(59, 13);
            this.label24.TabIndex = 38;
            this.label24.Text = "字体颜色:";
            // 
            // DS_Color
            // 
            this.DS_Color.BackColor = System.Drawing.Color.Black;
            this.DS_Color.Location = new System.Drawing.Point(64, 174);
            this.DS_Color.Name = "DS_Color";
            this.DS_Color.Size = new System.Drawing.Size(90, 18);
            this.DS_Color.TabIndex = 39;
            this.DS_Color.BackColorChanged += new System.EventHandler(this.Plant_BackColorChanged);
            this.DS_Color.Click += new System.EventHandler(this.ColorChange);
            // 
            // pnl_title
            // 
            this.pnl_title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(167)))), ((int)(((byte)(233)))));
            this.pnl_title.Controls.Add(this.pb_closePropertyForm);
            this.pnl_title.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_title.Location = new System.Drawing.Point(6, 0);
            this.pnl_title.Name = "pnl_title";
            this.pnl_title.Size = new System.Drawing.Size(0, 20);
            this.pnl_title.TabIndex = 36;
            // 
            // pb_closePropertyForm
            // 
            this.pb_closePropertyForm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pb_closePropertyForm.Image = ((System.Drawing.Image)(resources.GetObject("pb_closePropertyForm.Image")));
            this.pb_closePropertyForm.Location = new System.Drawing.Point(160, 2);
            this.pb_closePropertyForm.Name = "pb_closePropertyForm";
            this.pb_closePropertyForm.Size = new System.Drawing.Size(16, 16);
            this.pb_closePropertyForm.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pb_closePropertyForm.TabIndex = 0;
            this.pb_closePropertyForm.TabStop = false;
            this.pb_closePropertyForm.Click += new System.EventHandler(this.pb_closePropertyForm_Click);
            // 
            // toolBarImage
            // 
            this.toolBarImage.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("toolBarImage.ImageStream")));
            this.toolBarImage.TransparentColor = System.Drawing.Color.Black;
            this.toolBarImage.Images.SetKeyName(0, "");
            this.toolBarImage.Images.SetKeyName(1, "");
            this.toolBarImage.Images.SetKeyName(2, "");
            this.toolBarImage.Images.SetKeyName(3, "");
            this.toolBarImage.Images.SetKeyName(4, "");
            this.toolBarImage.Images.SetKeyName(5, "");
            this.toolBarImage.Images.SetKeyName(6, "");
            this.toolBarImage.Images.SetKeyName(7, "");
            this.toolBarImage.Images.SetKeyName(8, "");
            this.toolBarImage.Images.SetKeyName(9, "");
            this.toolBarImage.Images.SetKeyName(10, "");
            this.toolBarImage.Images.SetKeyName(11, "");
            this.toolBarImage.Images.SetKeyName(12, "");
            this.toolBarImage.Images.SetKeyName(13, "");
            this.toolBarImage.Images.SetKeyName(14, "");
            this.toolBarImage.Images.SetKeyName(15, "");
            // 
            // TBLineContent
            // 
            this.TBLineContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TBLineContent.Location = new System.Drawing.Point(-22, 46);
            this.TBLineContent.Name = "TBLineContent";
            this.TBLineContent.Size = new System.Drawing.Size(100, 21);
            this.TBLineContent.TabIndex = 41;
            this.TBLineContent.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TBLineContent.Visible = false;
            this.TBLineContent.KeyDown += new System.Windows.Forms.KeyEventHandler(this.inputContent_KeyDown);
            // 
            // pnl_tool
            // 
            this.pnl_tool.AutoScroll = true;
            this.pnl_tool.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(228)))), ((int)(((byte)(213)))));
            this.pnl_tool.Controls.Add(this.pb_splitter2);
            this.pnl_tool.Controls.Add(this.nsButton2);
            this.pnl_tool.Controls.Add(this.mti_drawFoldLineTool);
            this.pnl_tool.Controls.Add(this.mti_drawLineTool);
            this.pnl_tool.Controls.Add(this.mti_drawString);
            this.pnl_tool.Controls.Add(this.mti_Ellipse);
            this.pnl_tool.Controls.Add(this.mti_Rect);
            this.pnl_tool.Controls.Add(this.mti_Cursor);
            this.pnl_tool.Controls.Add(this.nsButton1);
            this.pnl_tool.Controls.Add(this.mti_endNode);
            this.pnl_tool.Controls.Add(this.mti_DataNode);
            this.pnl_tool.Controls.Add(this.mti_judgementNode);
            this.pnl_tool.Controls.Add(this.mti_gatherNode);
            this.pnl_tool.Controls.Add(this.mti_convergeNode);
            this.pnl_tool.Controls.Add(this.mti_asunderNode);
            this.pnl_tool.Controls.Add(this.mti_synchronizationNode);
            this.pnl_tool.Controls.Add(this.mti_gradationNode);
            this.pnl_tool.Controls.Add(this.mti_specificallyOperationNode);
            this.pnl_tool.Controls.Add(this.mti_generalNode);
            this.pnl_tool.Controls.Add(this.mti_startNode);
            this.pnl_tool.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnl_tool.Location = new System.Drawing.Point(0, 0);
            this.pnl_tool.Name = "pnl_tool";
            this.pnl_tool.Size = new System.Drawing.Size(146, 480);
            this.pnl_tool.TabIndex = 45;
            // 
            // nsButton2
            // 
            this.nsButton2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(183)))), ((int)(((byte)(57)))));
            this.nsButton2.ButtonForm = System.MyControl.eButtonForm.Rectangle;
            this.nsButton2.HighlightColor = System.Drawing.SystemColors.HotTrack;
            this.nsButton2.HottrackImage = null;
            this.nsButton2.Location = new System.Drawing.Point(0, 276);
            this.nsButton2.Name = "nsButton2";
            this.nsButton2.NormalImage = null;
            this.nsButton2.OnlyShowBitmap = false;
            this.nsButton2.PressedImage = null;
            this.nsButton2.Size = new System.Drawing.Size(138, 1);
            this.nsButton2.TabIndex = 23;
            this.nsButton2.TabStop = false;
            this.nsButton2.TextAlign = System.MyControl.eTextAlign.Bottom;
            this.nsButton2.ToolTip = null;
            // 
            // nsButton1
            // 
            this.nsButton1.BackColor = System.Drawing.Color.Silver;
            this.nsButton1.ButtonForm = System.MyControl.eButtonForm.Rectangle;
            this.nsButton1.Dock = System.Windows.Forms.DockStyle.Top;
            this.nsButton1.Enabled = false;
            this.nsButton1.HighlightColor = System.Drawing.SystemColors.HotTrack;
            this.nsButton1.HottrackImage = null;
            this.nsButton1.Location = new System.Drawing.Point(0, 0);
            this.nsButton1.Name = "nsButton1";
            this.nsButton1.NormalImage = null;
            this.nsButton1.OnlyShowBitmap = false;
            this.nsButton1.PressedImage = null;
            this.nsButton1.Size = new System.Drawing.Size(146, 1);
            this.nsButton1.TabIndex = 16;
            this.nsButton1.TabStop = false;
            this.nsButton1.TextAlign = System.MyControl.eTextAlign.Bottom;
            this.nsButton1.ToolTip = null;
            // 
            // tablePanel
            // 
            this.tablePanel.Controls.Add(this.dataGrid1);
            this.tablePanel.Controls.Add(this.panel2);
            this.tablePanel.Location = new System.Drawing.Point(466, 0);
            this.tablePanel.Name = "tablePanel";
            this.tablePanel.Size = new System.Drawing.Size(522, 226);
            this.tablePanel.TabIndex = 50;
            this.tablePanel.Visible = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(228)))), ((int)(((byte)(213)))));
            this.panel2.Controls.Add(this.pb_down);
            this.panel2.Controls.Add(this.pb_up);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(508, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(14, 226);
            this.panel2.TabIndex = 49;
            // 
            // pb_down
            // 
            this.pb_down.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pb_down.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pb_down.Image = ((System.Drawing.Image)(resources.GetObject("pb_down.Image")));
            this.pb_down.Location = new System.Drawing.Point(0, 214);
            this.pb_down.Name = "pb_down";
            this.pb_down.Size = new System.Drawing.Size(14, 12);
            this.pb_down.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pb_down.TabIndex = 1;
            this.pb_down.TabStop = false;
            this.pb_down.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pb_down_MouseDown);
            // 
            // pb_up
            // 
            this.pb_up.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pb_up.Dock = System.Windows.Forms.DockStyle.Top;
            this.pb_up.Image = ((System.Drawing.Image)(resources.GetObject("pb_up.Image")));
            this.pb_up.Location = new System.Drawing.Point(0, 0);
            this.pb_up.Name = "pb_up";
            this.pb_up.Size = new System.Drawing.Size(14, 14);
            this.pb_up.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pb_up.TabIndex = 0;
            this.pb_up.TabStop = false;
            this.pb_up.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pb_up_MouseDown);
            // 
            // pnl_tip
            // 
            this.pnl_tip.BackColor = System.Drawing.Color.Green;
            this.pnl_tip.Controls.Add(this.pictureBox1);
            this.pnl_tip.Controls.Add(this.lbl_tipInfo);
            this.pnl_tip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_tip.Location = new System.Drawing.Point(146, 0);
            this.pnl_tip.Name = "pnl_tip";
            this.pnl_tip.Size = new System.Drawing.Size(842, 0);
            this.pnl_tip.TabIndex = 51;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(228)))), ((int)(((byte)(213)))));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(24, 24);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // lbl_tipInfo
            // 
            this.lbl_tipInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(228)))), ((int)(((byte)(213)))));
            this.lbl_tipInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_tipInfo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_tipInfo.ForeColor = System.Drawing.Color.ForestGreen;
            this.lbl_tipInfo.Location = new System.Drawing.Point(0, 0);
            this.lbl_tipInfo.Name = "lbl_tipInfo";
            this.lbl_tipInfo.Size = new System.Drawing.Size(842, 0);
            this.lbl_tipInfo.TabIndex = 0;
            this.lbl_tipInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // showTip
            // 
            this.showTip.Interval = 10;
            this.showTip.Tick += new System.EventHandler(this.showTip_Tick);
            // 
            // acceptFocus
            // 
            this.acceptFocus.Location = new System.Drawing.Point(200, 42);
            this.acceptFocus.Name = "acceptFocus";
            this.acceptFocus.Size = new System.Drawing.Size(0, 21);
            this.acceptFocus.TabIndex = 52;
            this.acceptFocus.KeyDown += new System.Windows.Forms.KeyEventHandler(this.acceptFocus_KeyDown);
            this.acceptFocus.KeyUp += new System.Windows.Forms.KeyEventHandler(this.acceptFocus_KeyUp);
            // 
            // DrawFlowControl
            // 
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ContextMenu = this.menu;
            this.Controls.Add(this.acceptFocus);
            this.Controls.Add(this.pnl_tip);
            this.Controls.Add(this.pnl_tool);
            this.Controls.Add(this.TBLineContent);
            this.Controls.Add(this.pnl_property);
            this.Controls.Add(this.testDrawLength);
            this.Controls.Add(this.TBnodeContent);
            this.Controls.Add(this.drawStringContent);
            this.Controls.Add(this.tablePanel);
            this.Name = "DrawFlowControl";
            this.Size = new System.Drawing.Size(959, 480);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.UserControl1_Paint);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.UserControl1_MouseMove);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.UserControl1_MouseDown);
            this.Resize += new System.EventHandler(this.DrawFlowControl_Resize);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.UserControl1_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.pb_splitter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_splitter2)).EndInit();
            this.pnl_property.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.taskPane1)).EndInit();
            this.taskPane1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.epd_backGround)).EndInit();
            this.epd_backGround.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.epd_nodeProperty)).EndInit();
            this.epd_nodeProperty.ResumeLayout(false);
            this.epd_nodeProperty.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.epd_lineProperty)).EndInit();
            this.epd_lineProperty.ResumeLayout(false);
            this.epd_lineProperty.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.epd_stringProperty)).EndInit();
            this.epd_stringProperty.ResumeLayout(false);
            this.epd_stringProperty.PerformLayout();
            this.pnl_title.ResumeLayout(false);
            this.pnl_title.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_closePropertyForm)).EndInit();
            this.pnl_tool.ResumeLayout(false);
            this.tablePanel.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pb_down)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_up)).EndInit();
            this.pnl_tip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        #endregion

        #region 绘制图形
        /// <summary>
        /// 产生新的线
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void line_Click(object sender, System.EventArgs e)//产生新的线
        {
            System.MyControl.NSButton pb = (System.MyControl.NSButton)sender;
            switch (pb.Name)
            {
                case "mti_drawLineTool":
                    this.drawObject.NewLine(Line.DrawObjectType.DrawBeeLine);
                    break;

                case "mti_drawFoldLineTool":
                    this.drawObject.NewLine(Line.DrawObjectType.DrawFoldLine);
                    break;
            }
        }

        private void mti_drawLineTool_DoubleClick(object sender, System.EventArgs e)
        {
            System.MyControl.NSButton pb = (System.MyControl.NSButton)sender;
            switch (pb.Name)
            {
                case "mti_drawLineTool":
                    this.drawObject.NewDefaultLine(Line.DrawObjectType.DrawBeeLine, new Point(200, 100));
                    break;

                case "mti_drawFoldLineTool":
                    this.drawObject.NewDefaultLine(Line.DrawObjectType.DrawFoldLine, new Point(200, 100));
                    break;
            }
        }

        private void drawNode_Click(object sender, System.EventArgs e)//产生新的节点
        {
            System.MyControl.NSButton pb = (System.MyControl.NSButton)sender;
            switch (pb.Name)
            {
                case "mti_startNode":
                    this.drawObject.NewNode(GDIDrawFlow.Node.DrawObjectType.DrawNodeBegin);
                    break;
                case "mti_generalNode":
                    this.drawObject.NewNode(GDIDrawFlow.Node.DrawObjectType.DrawNodeGeneral);
                    break;
                case "mti_specificallyOperationNode":
                    this.drawObject.NewNode(GDIDrawFlow.Node.DrawObjectType.DrawSpecificallyOperation);
                    break;
                case "mti_gradationNode":
                    this.drawObject.NewNode(GDIDrawFlow.Node.DrawObjectType.DrawGradation);
                    break;
                case "mti_synchronizationNode":
                    this.drawObject.NewNode(GDIDrawFlow.Node.DrawObjectType.DrawSynchronization);
                    break;
                case "mti_asunderNode":
                    this.drawObject.NewNode(GDIDrawFlow.Node.DrawObjectType.DrawAsunder);
                    break;
                case "mti_convergeNode":
                    this.drawObject.NewNode(GDIDrawFlow.Node.DrawObjectType.DrawConverge);
                    break;
                case "mti_gatherNode":
                    this.drawObject.NewNode(GDIDrawFlow.Node.DrawObjectType.DrawGather);
                    break;
                case "mti_judgementNode":
                    this.drawObject.NewNode(GDIDrawFlow.Node.DrawObjectType.DrawJudgement);
                    break;
                case "mti_DataNode":
                    this.drawObject.NewNode(GDIDrawFlow.Node.DrawObjectType.DrawDataNode);
                    break;
                case "mti_endNode":
                    this.drawObject.NewNode(GDIDrawFlow.Node.DrawObjectType.DrawNodeEnd);
                    break;
                case "mti_Rect":
                    this.drawObject.NewNode(GDIDrawFlow.Node.DrawObjectType.DrawRectangle);
                    break;
                case "mti_Ellipse":
                    this.drawObject.NewNode(GDIDrawFlow.Node.DrawObjectType.DrawEllipse);
                    break;
            }
        }
        private void drawNode_DoubleClick(object sender, System.EventArgs e)
        {
            System.MyControl.NSButton pb = (System.MyControl.NSButton)sender;
            switch (pb.Name)
            {
                case "mti_startNode":
                    this.drawObject.NewDefaultNode(GDIDrawFlow.Node.DrawObjectType.DrawNodeBegin, new Point(200, 100));
                    break;
                case "mti_generalNode":
                    this.drawObject.NewDefaultNode(GDIDrawFlow.Node.DrawObjectType.DrawNodeGeneral, new Point(200, 100));
                    break;
                case "mti_specificallyOperationNode":
                    this.drawObject.NewDefaultNode(GDIDrawFlow.Node.DrawObjectType.DrawSpecificallyOperation, new Point(200, 100));
                    break;
                case "mti_gradationNode":
                    this.drawObject.NewDefaultNode(GDIDrawFlow.Node.DrawObjectType.DrawGradation, new Point(200, 100));
                    break;
                case "mti_synchronizationNode":
                    this.drawObject.NewDefaultNode(GDIDrawFlow.Node.DrawObjectType.DrawSynchronization, new Point(200, 100));
                    break;
                case "mti_asunderNode":
                    this.drawObject.NewDefaultNode(GDIDrawFlow.Node.DrawObjectType.DrawAsunder, new Point(200, 100));
                    break;
                case "mti_convergeNode":
                    this.drawObject.NewDefaultNode(GDIDrawFlow.Node.DrawObjectType.DrawConverge, new Point(200, 100));
                    break;
                case "mti_gatherNode":
                    this.drawObject.NewDefaultNode(GDIDrawFlow.Node.DrawObjectType.DrawGather, new Point(200, 100));
                    break;
                case "mti_judgementNode":
                    this.drawObject.NewDefaultNode(GDIDrawFlow.Node.DrawObjectType.DrawJudgement, new Point(200, 100));
                    break;
                case "mti_DataNode":
                    this.drawObject.NewDefaultNode(GDIDrawFlow.Node.DrawObjectType.DrawDataNode, new Point(200, 100));
                    break;
                case "mti_endNode":
                    this.drawObject.NewDefaultNode(GDIDrawFlow.Node.DrawObjectType.DrawNodeEnd, new Point(200, 100));
                    break;
                case "mti_Rect":
                    this.drawObject.NewDefaultNode(GDIDrawFlow.Node.DrawObjectType.DrawRectangle, new Point(200, 100));
                    break;
                case "mti_Ellipse":
                    this.drawObject.NewDefaultNode(GDIDrawFlow.Node.DrawObjectType.DrawEllipse, new Point(200, 100));
                    break;
            }
        }
        /// <summary>
        /// 生成写字板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, System.EventArgs e)
        {
            this.drawObject.NewDrawString();
        }
        /// <summary>
        /// 默认绘制写字板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mti_drawString_DoubleClick(object sender, System.EventArgs e)
        {
            this.drawObject.NewDefaultDrawString(new Point(200, 100));
        }
        #endregion

        #region 菜单项
        private void menuItem3_Click(object sender, System.EventArgs e)//删除全部
        {
            this.drawObject.DeleteAll();
        }

        private void menuItem2_Click(object sender, System.EventArgs e)//删除选定的
        {
            this.drawObject.DeleteSelectElement();
        }

        private void menuItem1_Click(object sender, System.EventArgs e)//全部选择
        {
            this.drawObject.SelectAll();
        }

        private void menuItem7_Click(object sender, System.EventArgs e)//置顶
        {
            this.drawObject.SetTop();
        }

        private void menuItem8_Click(object sender, System.EventArgs e)//置低
        {
            this.drawObject.SetDown();
        }
        #endregion

        #region 其它
        private void TBnodeContent_TextChanged(object sender, System.EventArgs e)
        {
            this.testDrawLength.Text = TBnodeContent.Text;
        }
        /// <summary>
        /// 线，节点，写字板上的内容输入结束的处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void inputContent_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.drawObject.InputContentEnter();
            }
        }
        /// <summary>
        /// 改变工作区大小
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DrawFlowControl_Resize(object sender, System.EventArgs e)
        {
            this.FormRefrash();
            GC.Collect();
        }
        /// <summary>
        /// 刷新背景
        /// </summary>
        public void FormRefrash()
        {
            this.drawObject.reDrawBitmap(this.graDrawPanel, 10, 10);
            drawObject.RefreshBackground();
        }

        #endregion

        #region 操作图形
        private void UserControl1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.acceptFocus.Focus();
            if (e.Button == MouseButtons.Left)
            {
                this.drawObject.MouseDown(e);
            }
            else
            {
                this.drawObject.MouseDown_Right(e);
            }
        }

        private void UserControl1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.drawObject.MouseMove(e);
        }

        private void UserControl1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.drawObject.MouseUp(e);
        }

        private void UserControl1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            //base.OnPaint (e);
            this.drawObject.AutoRepaintForm(e.Graphics);
        }

        #endregion

        #region 竖行工具栏收缩处理事件
        /// <summary>
        /// 右边工具栏收缩处理事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pb_splitter_Click(object sender, System.EventArgs e)
        {
            if (pnl_property.Width > this.pb_splitter.Width)
            {
                this.pnl_property.Width = this.pb_splitter.Width;
                this.pb_splitter.Image = (Image)new Bitmap(GetType(), "images.left.gif");
            }
            else
            {
                this.pnl_property.Width = 195;
                this.pb_splitter.Image = (Image)new Bitmap(GetType(), "images.right.gif");
            }
            this.tablePanel.Left = this.pnl_property.Left - this.tablePanel.Width;
        }

        private void pb_splitter2_Click(object sender, System.EventArgs e)
        {
            if (pnl_tool.Width > 50)
            {
                this.pnl_tool.Width = this.pb_splitter2.Width;
                this.pb_splitter2.Image = (Image)new Bitmap(GetType(), "images.right.gif");
                this.pb_splitter2.Dock = DockStyle.Left;
            }
            else
            {
                this.pnl_tool.Width = 146;
                this.pb_splitter2.Image = (Image)new Bitmap(GetType(), "images.left.gif");
                this.pb_splitter2.Dock = DockStyle.Right;
            }
        }
        #endregion

        #region 属性栏操作
        /// <summary>
        /// 隐藏属性栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pb_closePropertyForm_Click(object sender, System.EventArgs e)
        {
            this.pnl_property.Hide();
            this.pnl_property.Width = this.pb_splitter.Width;
        }

        /// <summary>
        /// 改变颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ColorChange(object sender, System.EventArgs e)
        {
            Panel pnl = (Panel)sender;
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                pnl.BackColor = cd.Color;
            }
        }
        /// <summary>
        /// 属性栏显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem6_Click(object sender, System.EventArgs e)
        {
            this.drawObject.Attribute();
        }

        private void menuItem12_Click(object sender, System.EventArgs e)
        {
            this.pnl_property.Show();
            if (pnl_property.Width > this.pb_splitter.Width)
            {
                this.pnl_property.Width = this.pb_splitter.Width;
                this.pb_splitter.Image = (Image)new Bitmap(GetType(), "images.left.gif");
            }
            else
            {
                this.pnl_property.Width = 198;
                this.pb_splitter.Image = (Image)new Bitmap(GetType(), "images.right.gif");
            }
            this.tablePanel.Left = this.pnl_property.Left - this.tablePanel.Width;
        }
        /// <summary>
        /// 在属性栏上修改的处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Location_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.drawObject.AttriTBChange(sender, e);
            }
        }

        /// <summary>
        /// 属性栏上关于颜色的改变　
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Plant_BackColorChanged(object sender, System.EventArgs e)
        {
            this.drawObject.ColorChange(sender, e);
        }
        #endregion

        #region 背景图更换
        /// <summary>
        /// 更换背景
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BG_Style_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            drawObject.bgImage = GetBackImage(BG_Style.SelectedIndex);
            this.drawObject.reDrawBitmap(this.graDrawPanel, 10, 10);
            drawObject.RefreshBackground();
        }
        /// <summary>
        /// 得到选定索引的图片
        /// </summary>
        /// <param name="imageIndex">索引值</param>
        /// <returns></returns>
        private Bitmap GetBackImage(int imageIndex)
        {

            switch (imageIndex)
            {
                case 0:
                    return imageArr[0];
                case 1:
                    return imageArr[1];
                case 2:
                    return imageArr[2];
                case 3:
                    return imageArr[3];
                case 4:
                    return imageArr[4];
            }
            return imageArr[0];
        }

        #endregion

        #region 缩略图
        /// <summary>
        /// 保存缩略图
        /// </summary>
        public void SaveBitmap()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "*.JPG|*.JPG|*.BMP|*.BMP|*.*|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                ImageFormat imageF = ImageFormat.Jpeg;
                if (sfd.FilterIndex == 0)
                {
                    imageF = ImageFormat.Jpeg;
                }
                else if (sfd.FilterIndex == 1)
                {
                    imageF = ImageFormat.Bmp;
                }
                this.BackgroundImage.Save(sfd.FileName, imageF);
            }
        }
        /// <summary>
        /// 获取缩略图
        /// </summary>
        /// <returns></returns>
        public string GetBitmap()
        {
            Image mbitmap = MakeMiniature.MakeThumbnail(this.BackgroundImage, 300, 180, "HW");
            MemoryStream ms = new MemoryStream();
            mbitmap.Save(ms, ImageFormat.Jpeg);
            byte[] byt = ms.GetBuffer();
            ms.Flush();
            ms.Close();
            String str = System.Convert.ToBase64String(byt);
            return str;
        }
        #endregion

        # region 右键菜单绘制元素
        /// <summary>
        /// 右键绘制 节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mi_newNode_Click(object sender, System.EventArgs e)
        {
            MenuItem mi = (MenuItem)sender;
            GDIDrawFlow.Node.DrawObjectType NodeType = Node.DrawObjectType.DrawNodeBegin;
            switch (mi.Text)
            {
                case "开始":
                    NodeType = Node.DrawObjectType.DrawNodeBegin;
                    break;
                case "节点":
                    NodeType = Node.DrawObjectType.DrawNodeGeneral;
                    break;
                case "特定操作":
                    NodeType = Node.DrawObjectType.DrawSpecificallyOperation;
                    break;
                case "顺序":
                    NodeType = Node.DrawObjectType.DrawGradation;
                    break;
                case "同步":
                    NodeType = Node.DrawObjectType.DrawSynchronization;
                    break;
                case "分支":
                    NodeType = Node.DrawObjectType.DrawAsunder;
                    break;
                case "汇聚":
                    NodeType = Node.DrawObjectType.DrawConverge;
                    break;
                case "汇总连接":
                    NodeType = Node.DrawObjectType.DrawGather;
                    break;
                case "判断":
                    NodeType = Node.DrawObjectType.DrawJudgement;
                    break;
                case "应用数据":
                    NodeType = Node.DrawObjectType.DrawDataNode;
                    break;
                case "结束":
                    NodeType = Node.DrawObjectType.DrawNodeEnd;
                    break;
                case "矩形工具":
                    NodeType = Node.DrawObjectType.DrawRectangle;
                    break;
                case "椭圆工具":
                    NodeType = Node.DrawObjectType.DrawEllipse;
                    break;
            }
            Point p = drawObject.location;
            this.drawObject.NewDefaultNode(NodeType, p);
        }
        /// <summary>
        ///  右键绘制 写字板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mi_drawstring_Click(object sender, System.EventArgs e)
        {
            this.drawObject.NewDefaultDrawString(drawObject.location);
        }
        /// <summary>
        ///  右键绘制 直线
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mi_drawline_Click(object sender, System.EventArgs e)
        {
            this.drawObject.NewDefaultLine(Line.DrawObjectType.DrawBeeLine, drawObject.location);
        }
        /// <summary>
        /// 右键绘制 折线
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mi_line2_Click(object sender, System.EventArgs e)
        {
            this.drawObject.NewDefaultLine(Line.DrawObjectType.DrawFoldLine, drawObject.location);
        }
        #endregion

        /// <summary>
        /// 获取控件上的画笔
        /// </summary>
        public DrawObject GetDarw
        {
            get { return drawObject; }
        }
        /// <summary>
        /// 获取控件的Xml信息
        /// </summary>
        /// <returns></returns>
        public string GetControlXml()
        {
            MakeControlXml cmx = new MakeControlXml(this.drawObject);
            return cmx.GetControlInfo();
        }

        /// <summary>
        /// 获取服务器上的XML文件信息，转换成流程图
        /// </summary>
        /// <param name="inServerXml">XML信息</param>
        public void DarwServerXml(string inServerXml, string FlowName)
        {
            MakeServerXml msx = new MakeServerXml(inServerXml);
            msx.CreateArrDraw();
            this.drawObject.NodeList = msx.NodeList;
            this.drawObject.LineList = msx.LineList;
            this.drawObject.StringList = msx.TextList;
            for (int i = 0; i < msx.NodeList.Count; i++)
            {
                if (((Node)msx.NodeList[i]).ObjectType == Node.DrawObjectType.DrawNodeBegin)
                {
                    this.drawObject.bStartFlag = true;
                }
                else if (((Node)msx.NodeList[i]).ObjectType == Node.DrawObjectType.DrawNodeEnd)
                {
                    this.drawObject.bEndFlag = true;
                }
                else if (((Node)msx.NodeList[i]).ObjectType == Node.DrawObjectType.DrawNodeGeneral)
                {
                    this.drawObject.i_GeneralCount++;
                }
            }
            this.drawObject.lastEdit = null;
            this.drawObject.reDrawBitmap(this.graDrawPanel, 10, 10);
            this.drawObject.RefreshBackground();
            this.drawObject.ReflashNodeIn_OutNodeID();
            this.drawObject.GetTableList();
            DrawFlowGroup.loadingForm.Hide();
        }
        private void SetToolTip(Control control, ToolTipIconType ttIconType, string title, string text)
        {
            toolTip.SetIconType(control, ttIconType);
            toolTip.SetTipTitle(control, title);
            toolTip.SetToolTip(control, text);
        }
        public void SetToolTip(Control control, string title, string text)
        {
            SetToolTip(control, ToolTipIconType.Information, title, text);
        }

        public void AddToolTip(bool isLimitNode)
        {
            if (isLimitNode)
            {
                SetToolTip(mti_startNode, "开始结点", "标志着流程的起始；定义了可以正常激活工作流的角色和用户，\r\n开始图元只允许有后继结点，无前驱结点.");

                SetToolTip(mti_generalNode, "任务结点", "转向另一个过程(或活动)或从另一个过程(活动)转入端点.\r\n可以有多个前驱结点,可以有多个后继结点.");

                SetToolTip(mti_specificallyOperationNode, "特定操作结点", "有独立含义和详细说明的一个工作步骤或一组工作步骤.\r\n有一个前驱结点,没有后继结点.");

                SetToolTip(mti_gradationNode, "顺序结点", "根据上一个步骤的结果，控制传入下一步骤的标记.\r\n有一个前驱结点,有一个后继结点.");

                SetToolTip(mti_synchronizationNode, "同步结点", "控制两个工作步骤同时进行的标记节点.\r\n有两个前驱结点,一个后继结点.");

                SetToolTip(mti_asunderNode, "分支结点", "控制选择下一个工作步骤的标记节点.\r\n有一个前驱结点,有多个后继结点.");

                SetToolTip(mti_convergeNode, "汇聚结点", "将上一个工作步骤或上一个工作组步骤汇集一起转入下一个工作(组)步骤的标记.\r\n有多个前驱结点,有一个后继结点.");

                SetToolTip(mti_gatherNode, "汇总连接结点", "汇集工作组步骤的标记.\r\n多个前驱结点,一个后继结点.");

                SetToolTip(mti_judgementNode, "判断结点", "对事务进行分析决策，并根据结果选择下一工作步骤.\r\n无限制前驱结点、后继结点.");

                SetToolTip(mti_DataNode, "应用数据", "有独立含义和详细说明的一个工作步骤或一组工作步骤数据节点.\r\n没有前驱结点,可以有多个后继结点.");

                SetToolTip(mti_endNode, "结束结点", "标志着流程的结束，\r\n结束图元只允许有前驱结点,无后继结点.");

                SetToolTip(mti_Rect, "矩形工具", "绘制矩形，用于表示其它结点.无限制前驱结点后继结点.");

                SetToolTip(mti_Ellipse, "椭圆工具", "绘制椭圆，用于表示其它结点.无限制前驱结点后继结点.");
            }
            else
            {
                SetToolTip(mti_startNode, "开始结点", "标志着流程的起始；定义了可以正常激活工作流的角色和用户。");

                SetToolTip(mti_generalNode, "任务结点", "转向另一个过程(或活动)或从另一个过程(活动)转入端点.");

                SetToolTip(mti_specificallyOperationNode, "特定操作结点", "有独立含义和详细说明的一个工作步骤或一组工作步骤.");

                SetToolTip(mti_gradationNode, "顺序结点", "根据上一个步骤的结果，控制传入下一步骤的标记.");

                SetToolTip(mti_synchronizationNode, "同步结点", "控制两个工作步骤同时进行的标记节点.");

                SetToolTip(mti_asunderNode, "分支结点", "控制选择下一个工作步骤的标记节点.");

                SetToolTip(mti_convergeNode, "汇聚结点", "将上一个工作步骤或上一个工作组步骤汇集一起转入下一个工作(组)步骤的标记.");

                SetToolTip(mti_gatherNode, "汇总连接结点", "汇集工作组步骤的标记.");

                SetToolTip(mti_judgementNode, "判断结点", "对事务进行分析决策，并根据结果选择下一工作步骤.");

                SetToolTip(mti_DataNode, "应用数据", "有独立含义和详细说明的一个工作步骤或一组工作步骤数据节点.");

                SetToolTip(mti_endNode, "结束结点", "标志着流程的开始的结束.");

                SetToolTip(mti_Rect, "矩形工具", "绘制矩形，用于表示其它结点.");

                SetToolTip(mti_Ellipse, "椭圆工具", "绘制椭圆，用于表示其它结点.");
            }
            SetToolTip(mti_drawString, "写字工具", "用于描述信息,结点说明、特定注释等.");
            SetToolTip(mti_drawLineTool, "直线工具", "用直线来连接结点、描述结点间的条件关系.");
            SetToolTip(mti_drawFoldLineTool, "三折线工具", "用三个折点来连接结点、描述结点间的条件关系.");
        }


        private void pb_up_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.tablePanel.Height -= 17;
        }

        private void pb_down_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.tablePanel.Height += 17;
        }

        private void dataGrid1_CurrentCellChanged(object sender, System.EventArgs e)
        {
            this.dataGrid1.Select(this.dataGrid1.CurrentCell.RowNumber);
        }


        private void showTip_Tick(object sender, System.EventArgs e)
        {
            if (tReady < 300)
            {
                tReady++;
                if (pnl_tip.Height < 24)
                {
                    this.pnl_tip.Height += 2;
                }
            }
            else
            {
                if (pnl_tip.Height > 0)
                {
                    this.pnl_tip.Height -= 2;
                }
                else
                {
                    tReady = 0;
                    this.showTip.Stop();
                }
            }
        }


        public void ShowTip(string info)
        {
            this.pnl_tip.BackColor = Color.FromArgb(30, 100, 100, 100);
            this.lbl_tipInfo.Text = info;
            this.showTip.Start();
        }

        private void acceptFocus_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.Control)
            {
                this.drawObject.MemThisObject();
            }
            else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Right || e.KeyCode == Keys.Left)
            {
                this.drawObject.MinMoveElement(e.KeyCode);
            }
            else if (e.KeyCode == Keys.Delete)
            {
                this.drawObject.DeleteSelectElement();
            }
            else if (e.Control && e.KeyCode == Keys.A)
            {
                this.drawObject.SelectAll();
            }
            else if (e.Control && e.KeyCode == Keys.D)
            {
                this.drawObject.DeleteAll();
            }
            else if (e.Control && e.KeyCode == Keys.M)
            {
                this.drawObject.Attribute();
            }
            else if (e.Control && e.KeyCode == Keys.B)
            {

                if (pnl_property.Width > this.pb_splitter.Width)
                {
                    this.pnl_property.Width = this.pb_splitter.Width;
                    this.pb_splitter.Image = (Image)new Bitmap(GetType(), "images.left.gif");
                }
                else
                {
                    this.pnl_property.Width = 198;
                    this.pb_splitter.Image = (Image)new Bitmap(GetType(), "images.right.gif");
                }
                this.tablePanel.Left = this.pnl_property.Left - this.tablePanel.Width;
                this.pnl_property.Show();
            }
        }

        private void acceptFocus_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
            {
                this.drawObject.ClearThisObject();
            }
        }
        private void cb_integrality_CheckedChanged(object sender, System.EventArgs e)
        {
            this.isIntegrality = this.cb_integrality.Checked;
        }

        /// <summary>
        /// 结点个数限制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_limit_CheckedChanged(object sender, System.EventArgs e)
        {
            this.isNodeLimit = this.cb_limit.Checked;
            AddToolTip(this.cb_limit.Checked);
        }

        /// <summary>
        /// 检测流程图完整性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbl_checkIntegrality_Click(object sender, System.EventArgs e)
        {
            if (this.drawObject.CheckIntegrity())
            {
                MessageForm.Show("检测成功");
            }
        }
        /// <summary>
        /// 指针工具
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mti_Cursor_Click(object sender, System.EventArgs e)
        {
            this.drawObject.CursorsDefault();
        }

        private void cb_isAlpha_CheckedChanged(object sender, System.EventArgs e)
        {
            this.drawObject.isAlpha = this.cb_isAlpha.Checked;
            this.drawObject.reDrawBitmap(this.graDrawPanel, 10, 10);
            this.drawObject.RefreshBackground();
        }
    }
}