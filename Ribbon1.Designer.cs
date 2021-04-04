
namespace TranslateAddins
{
    partial class Ribbon1 : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public Ribbon1()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ribbon1));
            this.TranslateAddinsTab1 = this.Factory.CreateRibbonTab();
            this.group1 = this.Factory.CreateRibbonGroup();
            this.button1 = this.Factory.CreateRibbonButton();
            this.group2 = this.Factory.CreateRibbonGroup();
            this.btn_RegularFind = this.Factory.CreateRibbonButton();
            this.btn_RegularReplace = this.Factory.CreateRibbonButton();
            this.btn_RegularOptions = this.Factory.CreateRibbonButton();
            this.TranslateAddinsTab1.SuspendLayout();
            this.group1.SuspendLayout();
            this.group2.SuspendLayout();
            this.SuspendLayout();
            // 
            // TranslateAddinsTab1
            // 
            this.TranslateAddinsTab1.Groups.Add(this.group1);
            this.TranslateAddinsTab1.Groups.Add(this.group2);
            this.TranslateAddinsTab1.Label = "工作单";
            this.TranslateAddinsTab1.Name = "TranslateAddinsTab1";
            // 
            // group1
            // 
            this.group1.Items.Add(this.button1);
            this.group1.Label = "翻译辅助";
            this.group1.Name = "group1";
            // 
            // button1
            // 
            this.button1.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Label = "翻译辅助窗格";
            this.button1.Name = "button1";
            this.button1.ShowImage = true;
            this.button1.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button1_Click);
            // 
            // group2
            // 
            this.group2.Items.Add(this.btn_RegularFind);
            this.group2.Items.Add(this.btn_RegularReplace);
            this.group2.Items.Add(this.btn_RegularOptions);
            this.group2.Label = "正则查找替换";
            this.group2.Name = "group2";
            // 
            // btn_RegularFind
            // 
            this.btn_RegularFind.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btn_RegularFind.Image = ((System.Drawing.Image)(resources.GetObject("btn_RegularFind.Image")));
            this.btn_RegularFind.Label = "正则查找";
            this.btn_RegularFind.Name = "btn_RegularFind";
            this.btn_RegularFind.ShowImage = true;
            this.btn_RegularFind.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.RegularFindAndReplace_btn_Click);
            // 
            // btn_RegularReplace
            // 
            this.btn_RegularReplace.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btn_RegularReplace.Image = ((System.Drawing.Image)(resources.GetObject("btn_RegularReplace.Image")));
            this.btn_RegularReplace.Label = "正则替换";
            this.btn_RegularReplace.Name = "btn_RegularReplace";
            this.btn_RegularReplace.ShowImage = true;
            this.btn_RegularReplace.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.RegularFindAndReplace_btn_Click);
            // 
            // btn_RegularOptions
            // 
            this.btn_RegularOptions.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btn_RegularOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_RegularOptions.Image")));
            this.btn_RegularOptions.Label = "正则选项";
            this.btn_RegularOptions.Name = "btn_RegularOptions";
            this.btn_RegularOptions.ShowImage = true;
            this.btn_RegularOptions.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.RegularFindAndReplace_btn_Click);
            // 
            // Ribbon1
            // 
            this.Name = "Ribbon1";
            this.RibbonType = "Microsoft.Word.Document";
            this.Tabs.Add(this.TranslateAddinsTab1);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.Ribbon1_Load);
            this.TranslateAddinsTab1.ResumeLayout(false);
            this.TranslateAddinsTab1.PerformLayout();
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();
            this.group2.ResumeLayout(false);
            this.group2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab TranslateAddinsTab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group2;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btn_RegularFind;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btn_RegularReplace;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btn_RegularOptions;
    }

    partial class ThisRibbonCollection
    {
        internal Ribbon1 Ribbon1
        {
            get { return this.GetRibbon<Ribbon1>(); }
        }
    }
}
