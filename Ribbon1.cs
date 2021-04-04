using Microsoft.Office.Tools.Ribbon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Word;
using System.Text.RegularExpressions;
using Office = Microsoft.Office.Core;
namespace TranslateAddins
{
    public partial class Ribbon1
    {
        public AddinUserControl addinTaskPanelControl;
        public Microsoft.Office.Tools.CustomTaskPane AddincustomTaskPane;        
        public bool UpdatedFlag;
        public RegularFindAndReplace regularFindAndReplaceControl;
        public Microsoft.Office.Tools.CustomTaskPane RegularFindAndReplaceTaskPane;

        private void Ribbon1_Load(object sender, RibbonUIEventArgs e)
        {            
            Application app = Globals.ThisAddIn.Application;
            Office.CommandBar cmdBar = app.CommandBars["Text"];
            try
            {
                app.CommandBars["Text"].Reset();
                app.CommandBars["Text"]
                   .Controls["Addin_Translate"]
                   .Delete();
            }catch(Exception)
            {

            }
            finally 
            {

                Office.CommandBarControl translateButton = cmdBar.Controls.Add(Office.MsoControlType.msoControlButton);
                Office.CommandBarButton cb = translateButton as Office.CommandBarButton;
                cb.Caption = "Addin_Translate";
                cb.Visible = false;
                cb.Click += CommandBarsButton_Btn_Click1;
            }
        }



        private void AddincustomTaskPane_VisibleChanged(object sender, EventArgs e)
        {
            //System.Windows.Forms.MessageBox.Show($"{AddincustomTaskPane.Visible}");
            if (AddincustomTaskPane.Visible)
            {
                Globals.ThisAddIn.Application.CommandBars["Text"].Controls["Addin_Translate"].Visible = true;
            }
            else
            {
                Globals.ThisAddIn.Application.CommandBars["Text"].Controls["Addin_Translate"].Visible = false;
            }
        }

        /// <summary>
        /// 任务窗格位置变更处理函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddinTaskPanelControl_LocationChanged(object sender, EventArgs e)
        {
            if (sender is Microsoft.Office.Tools.CustomTaskPane taskPane)
            {
                AddinUserControl userControl = taskPane.Control as AddinUserControl;
                userControl.Width = taskPane.Width;
                userControl.Height = taskPane.Height;
                userControl.AddinUserControl_Init();
            }
        }
        /// <summary>
        /// 选项卡按钮处理函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, RibbonControlEventArgs e)
        {

            ///添加任务窗格
            if (addinTaskPanelControl==null)
            {
                addinTaskPanelControl = new AddinUserControl();
            }
            if (AddincustomTaskPane == null) 
            { 
                AddincustomTaskPane = Globals.ThisAddIn.CustomTaskPanes.Add(addinTaskPanelControl, "任务窗格");
                AddincustomTaskPane.VisibleChanged += AddincustomTaskPane_VisibleChanged;
            }
            AddincustomTaskPane.Visible = true;
            addinTaskPanelControl.Visible = true;
            try
            {
                Globals.ThisAddIn.Application.WindowSelectionChange -= Application_WindowSelectionChange;
            }
            finally 
            { 
                Globals.ThisAddIn.Application.WindowSelectionChange += Application_WindowSelectionChange;
            }
            try
            {
                addinTaskPanelControl.GiveFeedback -= AddinTaskPanelControl_GiveFeedback;
            }
            finally 
            {
                addinTaskPanelControl.GiveFeedback += AddinTaskPanelControl_GiveFeedback; 
            }
            try
            {
                addinTaskPanelControl.SizeChanged -= AddinTaskPanelControl_SizeChanged;
            }
            finally
            {
                addinTaskPanelControl.SizeChanged += AddinTaskPanelControl_SizeChanged;
            }
            AddincustomTaskPane.Width = 300;
        }
        /// <summary>
        /// 右键按钮处理函数
        /// </summary>
        /// <param name="Ctrl"></param>
        /// <param name="CancelDefault"></param>
        private void CommandBarsButton_Btn_Click1(Office.CommandBarButton Ctrl, ref bool CancelDefault)
        {

            addinTaskPanelControl.tabControl1.SelectedTab = addinTaskPanelControl.tabPage2;
            string wd = System.Web.HttpUtility.UrlEncode($"{Globals.ThisAddIn.Application.Selection.Range.Text}",System.Text.Encoding.UTF8);
            addinTaskPanelControl.webBrowser1.Navigate($"https://translate.google.cn/?sl=auto&tl=en&text={wd}&op=translate");
        }

        /// <summary>
        /// 任务窗格尺寸调整处理函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddinTaskPanelControl_SizeChanged(object sender, EventArgs e)
        {
            AddinUserControl userControl = sender as AddinUserControl;
            userControl.comboBox1.Width = userControl.Width - userControl.btn_Refresh.Width - 6;
            userControl.comboBox1.Top = userControl.btn_Refresh.Top;
            userControl.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            userControl.Dock = System.Windows.Forms.DockStyle.Right;
            userControl.tabControl1.Width = userControl.Width;
            userControl.dataGridView_Translate.Width = userControl.tabPage1.Width;

        }
        private void AddinTaskPanelControl_GiveFeedback(object sender, System.Windows.Forms.GiveFeedbackEventArgs e)
        {            
        }
        /// <summary>
        /// Word选择内容更改函数
        /// 因为word并没有像Excel一样的内容更改函数，所以只能用这个函数加文本hash校验进行代替
        /// </summary>
        /// <param name="Sel"></param>
        private void Application_WindowSelectionChange(Selection Sel)
        {
            //if (Sel.Type != WdSelectionType.wdSelectionShape) updatedFlag = false;
            int p = Globals.ThisAddIn.Application.ActiveDocument.Range().Text.GetHashCode();
            if (p != addinTaskPanelControl.TextHash)
            {
                //updateItems();
                
                addinTaskPanelControl.btn_Refresh.Enabled = true;
                addinTaskPanelControl.Refresh();
            }
        }

        private void RegularFindAndReplace_btn_Click(object sender, RibbonControlEventArgs e)
        {

            ///添加正则查找窗格
            if (regularFindAndReplaceControl == null)
            {
                regularFindAndReplaceControl = new RegularFindAndReplace
                {
                    MinimumSize = new System.Drawing.Size(240, 0)
                };
            }
            if (RegularFindAndReplaceTaskPane == null) 
            {
                try
                {
                    RegularFindAndReplaceTaskPane = Globals.ThisAddIn.CustomTaskPanes.Add(regularFindAndReplaceControl, "正则查找替换");
                    
                }
                catch (Exception err)
                {
                    System.Windows.Forms.MessageBox.Show($"{err.Message}");
                    throw;
                }
                
            }
            RibbonButton btn = sender as RibbonButton;
            regularFindAndReplaceControl.Visible = true;
            try
            {

                RegularFindAndReplaceTaskPane.Width = 300;
                RegularFindAndReplaceTaskPane.Visible = true;

                try
                {
                    RegularFindAndReplaceTaskPane.VisibleChanged -= RegularFindAndReplaceTaskPane_VisibleChanged;
                }
                finally 
                { 
                    RegularFindAndReplaceTaskPane.VisibleChanged += RegularFindAndReplaceTaskPane_VisibleChanged;
                }
            }
            catch ( Exception)
            {
                try
                {
                    RegularFindAndReplaceTaskPane = Globals.ThisAddIn.CustomTaskPanes.First(a=>a.Title=="正则查找替换");
                }
                catch (Exception)
                {
                    RegularFindAndReplaceTaskPane = Globals.ThisAddIn.CustomTaskPanes.Add(regularFindAndReplaceControl, "正则查找替换");
                    RegularFindAndReplaceTaskPane.Width = 300;
                }

            }
            if (btn.Id == "btn_RegularFind")
            {
                regularFindAndReplaceControl.tabSelect(0);
            }
            else if(btn.Id =="btn_RegularReplace")
            {
                regularFindAndReplaceControl.tabSelect(1);
            }
            else if(btn.Id == "btn_RegularOptions")
            {
                regularFindAndReplaceControl.tabSelect(2);
            }
        }

        private void RegularFindAndReplaceTaskPane_VisibleChanged(object sender, EventArgs e)
        {
            if (RegularFindAndReplaceTaskPane.Visible)
            {
                regularFindAndReplaceControl.tabpage_Resize();
            }

        }
    }
}
