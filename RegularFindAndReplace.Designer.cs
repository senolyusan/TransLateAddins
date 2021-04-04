
using System;
using System.Windows.Forms;

namespace TranslateAddins
{
    partial class RegularFindAndReplace
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl_RegularFindAndReplace = new System.Windows.Forms.TabControl();
            this.tabPage_Find = new System.Windows.Forms.TabPage();
            this.btn_Find = new System.Windows.Forms.Button();
            this.RTB_Find1 = new System.Windows.Forms.RichTextBox();
            this.tabPage_Replace = new System.Windows.Forms.TabPage();
            this.btn_ReplaceAll = new System.Windows.Forms.Button();
            this.btn_Replace_ListOut = new System.Windows.Forms.Button();
            this.RTB_replace = new System.Windows.Forms.RichTextBox();
            this.RTB_Find2 = new System.Windows.Forms.RichTextBox();
            this.label_ReplaceTo = new System.Windows.Forms.Label();
            this.tabPage_Options = new System.Windows.Forms.TabPage();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.TextBox_OptionsTips = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.ResultList = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rng = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl_RegularFindAndReplace.SuspendLayout();
            this.tabPage_Find.SuspendLayout();
            this.tabPage_Replace.SuspendLayout();
            this.tabPage_Options.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl_RegularFindAndReplace);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.TextBox_OptionsTips);
            this.splitContainer1.Size = new System.Drawing.Size(239, 497);
            this.splitContainer1.SplitterDistance = 182;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
            this.splitContainer1.SizeChanged += new System.EventHandler(this.splitContainer1_SizeChanged);
            // 
            // tabControl_RegularFindAndReplace
            // 
            this.tabControl_RegularFindAndReplace.Controls.Add(this.tabPage_Find);
            this.tabControl_RegularFindAndReplace.Controls.Add(this.tabPage_Replace);
            this.tabControl_RegularFindAndReplace.Controls.Add(this.tabPage_Options);
            this.tabControl_RegularFindAndReplace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl_RegularFindAndReplace.Location = new System.Drawing.Point(0, 0);
            this.tabControl_RegularFindAndReplace.Name = "tabControl_RegularFindAndReplace";
            this.tabControl_RegularFindAndReplace.SelectedIndex = 0;
            this.tabControl_RegularFindAndReplace.Size = new System.Drawing.Size(239, 182);
            this.tabControl_RegularFindAndReplace.TabIndex = 0;
            this.tabControl_RegularFindAndReplace.SelectedIndexChanged += new System.EventHandler(this.tabControl_RegularFindAndReplace_SelectedIndexChanged);
            // 
            // tabPage_Find
            // 
            this.tabPage_Find.Controls.Add(this.btn_Find);
            this.tabPage_Find.Controls.Add(this.RTB_Find1);
            this.tabPage_Find.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Find.Name = "tabPage_Find";
            this.tabPage_Find.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Find.Size = new System.Drawing.Size(231, 156);
            this.tabPage_Find.TabIndex = 0;
            this.tabPage_Find.Text = "正则查找";
            this.tabPage_Find.UseVisualStyleBackColor = true;
            this.tabPage_Find.Click += new System.EventHandler(this.tabPage_Find_Click);
            // 
            // btn_Find
            // 
            this.btn_Find.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btn_Find.Location = new System.Drawing.Point(149, 71);
            this.btn_Find.Name = "btn_Find";
            this.btn_Find.Size = new System.Drawing.Size(75, 23);
            this.btn_Find.TabIndex = 6;
            this.btn_Find.Text = "查找";
            this.btn_Find.UseVisualStyleBackColor = true;
            this.btn_Find.Click += new System.EventHandler(this.btn_Find_Click);
            // 
            // RTB_Find1
            // 
            this.RTB_Find1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RTB_Find1.Location = new System.Drawing.Point(6, 6);
            this.RTB_Find1.Name = "RTB_Find1";
            this.RTB_Find1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.RTB_Find1.Size = new System.Drawing.Size(218, 59);
            this.RTB_Find1.TabIndex = 4;
            this.RTB_Find1.Text = "";
            this.RTB_Find1.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // tabPage_Replace
            // 
            this.tabPage_Replace.Controls.Add(this.btn_ReplaceAll);
            this.tabPage_Replace.Controls.Add(this.btn_Replace_ListOut);
            this.tabPage_Replace.Controls.Add(this.RTB_replace);
            this.tabPage_Replace.Controls.Add(this.RTB_Find2);
            this.tabPage_Replace.Controls.Add(this.label_ReplaceTo);
            this.tabPage_Replace.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Replace.Name = "tabPage_Replace";
            this.tabPage_Replace.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Replace.Size = new System.Drawing.Size(231, 156);
            this.tabPage_Replace.TabIndex = 1;
            this.tabPage_Replace.Text = "正则替换";
            this.tabPage_Replace.UseVisualStyleBackColor = true;
            // 
            // btn_ReplaceAll
            // 
            this.btn_ReplaceAll.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btn_ReplaceAll.Location = new System.Drawing.Point(150, 28);
            this.btn_ReplaceAll.Name = "btn_ReplaceAll";
            this.btn_ReplaceAll.Size = new System.Drawing.Size(75, 23);
            this.btn_ReplaceAll.TabIndex = 5;
            this.btn_ReplaceAll.Text = "替换全部";
            this.btn_ReplaceAll.UseVisualStyleBackColor = true;
            this.btn_ReplaceAll.Click += new System.EventHandler(this.btn_ReplaceAll_Click);
            // 
            // btn_Replace_ListOut
            // 
            this.btn_Replace_ListOut.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btn_Replace_ListOut.Location = new System.Drawing.Point(69, 28);
            this.btn_Replace_ListOut.Name = "btn_Replace_ListOut";
            this.btn_Replace_ListOut.Size = new System.Drawing.Size(75, 23);
            this.btn_Replace_ListOut.TabIndex = 5;
            this.btn_Replace_ListOut.Text = "列出全部";
            this.btn_Replace_ListOut.UseVisualStyleBackColor = true;
            this.btn_Replace_ListOut.Click += new System.EventHandler(this.btn_Replace_ListOut_Click);
            // 
            // RTB_replace
            // 
            this.RTB_replace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.RTB_replace.Location = new System.Drawing.Point(7, 50);
            this.RTB_replace.Name = "RTB_replace";
            this.RTB_replace.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.RTB_replace.Size = new System.Drawing.Size(218, 21);
            this.RTB_replace.TabIndex = 4;
            this.RTB_replace.Text = "";
            this.RTB_replace.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // RTB_Find2
            // 
            this.RTB_Find2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RTB_Find2.Location = new System.Drawing.Point(7, 7);
            this.RTB_Find2.Name = "RTB_Find2";
            this.RTB_Find2.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.RTB_Find2.Size = new System.Drawing.Size(218, 21);
            this.RTB_Find2.TabIndex = 3;
            this.RTB_Find2.Text = "";
            this.RTB_Find2.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // label_ReplaceTo
            // 
            this.label_ReplaceTo.AutoSize = true;
            this.label_ReplaceTo.Location = new System.Drawing.Point(7, 35);
            this.label_ReplaceTo.Name = "label_ReplaceTo";
            this.label_ReplaceTo.Size = new System.Drawing.Size(41, 12);
            this.label_ReplaceTo.TabIndex = 1;
            this.label_ReplaceTo.Text = "替换为";
            // 
            // tabPage_Options
            // 
            this.tabPage_Options.Controls.Add(this.checkedListBox1);
            this.tabPage_Options.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Options.Name = "tabPage_Options";
            this.tabPage_Options.Size = new System.Drawing.Size(231, 156);
            this.tabPage_Options.TabIndex = 2;
            this.tabPage_Options.Text = "正则选项";
            this.tabPage_Options.UseVisualStyleBackColor = true;
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.CheckOnClick = true;
            this.checkedListBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Items.AddRange(new object[] {
            "IgnoreCase",
            "Multiline",
            "ExplicitCapture",
            "Compiled",
            "Singleline",
            "IgnorePatternWhitespace",
            "RightToLeft",
            "ECMAScript",
            "CultureInvariant"});
            this.checkedListBox1.Location = new System.Drawing.Point(0, 0);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(231, 156);
            this.checkedListBox1.TabIndex = 0;
            // 
            // TextBox_OptionsTips
            // 
            this.TextBox_OptionsTips.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextBox_OptionsTips.Enabled = false;
            this.TextBox_OptionsTips.Location = new System.Drawing.Point(0, 0);
            this.TextBox_OptionsTips.Multiline = true;
            this.TextBox_OptionsTips.Name = "TextBox_OptionsTips";
            this.TextBox_OptionsTips.Size = new System.Drawing.Size(239, 311);
            this.TextBox_OptionsTips.TabIndex = 1;
            // 
            // ResultList
            // 
            this.ResultList.Name = "ResultList";
            // 
            // rng
            // 
            this.rng.Name = "rng";
            // 
            // RegularFindAndReplace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "RegularFindAndReplace";
            this.Size = new System.Drawing.Size(239, 497);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl_RegularFindAndReplace.ResumeLayout(false);
            this.tabPage_Find.ResumeLayout(false);
            this.tabPage_Replace.ResumeLayout(false);
            this.tabPage_Replace.PerformLayout();
            this.tabPage_Options.ResumeLayout(false);
            this.ResumeLayout(false);

        }


        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControl_RegularFindAndReplace;
        private System.Windows.Forms.TabPage tabPage_Find;
        private System.Windows.Forms.TabPage tabPage_Replace;
        private System.Windows.Forms.Label label_ReplaceTo;
        private System.Windows.Forms.RichTextBox RTB_Find1;
        private System.Windows.Forms.RichTextBox RTB_replace;
        private System.Windows.Forms.RichTextBox RTB_Find2;
        private TabPage tabPage_Options;
        private CheckedListBox checkedListBox1;
        private TextBox TextBox_OptionsTips;
        private Button btn_Find;
        private Button btn_ReplaceAll;
        private Button btn_Replace_ListOut;
        private ToolTip toolTip1;
    }
}
