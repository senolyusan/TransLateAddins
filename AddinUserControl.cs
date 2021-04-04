using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Tools.Ribbon;
using Microsoft.Office.Interop.Word;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Word = Microsoft.Office.Interop.Word;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Word;

namespace TranslateAddins
{
    /// <summary>
    /// 状态枚举
    /// </summary>
    public enum Status
    {
        unChecked = 0,
        OK = 1,
        NG = -1
    }
    /// <summary>
    /// 结构体定义
    /// 
    /// 定义结构体，存储正则匹配出的数据，便于后续DataGridView使用
    /// </summary>
    public struct ItemStruct
    {
        /// <summary>
        /// 类别
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 中文词
        /// </summary>
        public string ChineseWords { get; set; }
        /// <summary>
        /// 英文词
        /// </summary>
        public string EnglishWords { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public Status statustype { get; set; }

        /// <summary>
        /// 索引号
        /// 
        /// 索引号负责将ItemStruct与Dictionary的Key关联起来，双方需要保持一致
        /// </summary>
        public long paragragh { get; set; }
        /// <summary>
        /// 状态文本起始位置
        /// </summary>
        public long statusStart
        {
            get
            {
                return Type.Length + ChineseWords.Length + EnglishWords.Length + 3;
            }
        }
        /// <summary>
        /// 状态文本长度
        /// </summary>
        public long statusLength
        {
            get
            {
                return (statustype != 0) ? 2 : 3;
            }
        }/// <summary>
         /// 重载ToString函数
         /// </summary>
         /// <returns>string类型</returns>
        public override string ToString()
        {
            return $"{Type}\t{ChineseWords}\t{EnglishWords}\t{((statustype == Status.unChecked) ? ("未确认") : ((statustype == Status.OK) ? "正确" : "错误"))}";
        }
    }
    public partial class AddinUserControl : UserControl
    {
        public bool debug1 = true;
        /// <summary>
        /// 文本区域字典
        /// 文本区域字典是为了存放遍历出的文本区域，便于下一步修改操作的
        /// </summary>
        private Dictionary<int, Range> rangeDict = new Dictionary<int, Range>();
        
        /// <summary>
        /// 结构体列表，便于DataGridView使用
        /// </summary>
        public List<ItemStruct> itemStructs = new List<ItemStruct>();
        /// <summary>
        /// 文本Hash，用来监测文档变化的
        /// </summary>
        public int TextHash;
        /// <summary>
        /// 构造函数，实现初始化
        /// </summary>
        public AddinUserControl()
        {
            InitializeComponent();
            AddinUserControl_Init();
            
        }
        /// <summary>
        /// 初始化函数
        /// </summary>
        public void AddinUserControl_Init()
        {
            comboBox1.Dock = DockStyle.Right                ;
            
        }

        private void AddinTaskPanel_Load(object sender, EventArgs e)
        {
            
        }
        /// <summary>
        /// DataGridView列表更新函数
        /// </summary>
        /// <param name="itemStructs">存储数据的列表</param>
        /// <param name="type">筛选值</param>
        public void DataGridViewUpdate(List<ItemStruct> itemStructs,string type)
        {
            dataGridView_Translate.Visible = false;
            dataGridView_Translate.Rows.Clear();
            foreach(ItemStruct item in itemStructs)
            {
                if (string.IsNullOrEmpty(type) || item.Type == type)
                {
                    int rownum = dataGridView_Translate.Rows.Add();
                    dataGridView_Translate.Rows[rownum].Cells[0].Value = false;
                    dataGridView_Translate.Rows[rownum].Cells[1].Value = item.ChineseWords;
                    dataGridView_Translate.Rows[rownum].Cells[2].Value = item.EnglishWords;
                    dataGridView_Translate.Rows[rownum].Cells[3].Value = true;
                    dataGridView_Translate.Rows[rownum].Cells[4].Value = true;
                    dataGridView_Translate.Rows[rownum].Cells[5].Value = item.paragragh;
                    dataGridView_Translate.Rows[rownum].Cells[6].Value = item.statusStart;
                }
            }
            dataGridView_Translate.Visible = true;
        }
        /// <summary>
        /// 文档更新函数
        /// </summary>
        /// <param name="rows">由DataGridView导出的行列表，指示需要更新的文档位置</param>
        /// <param name="status">更新标志，指【正确/错误】</param>
        void updatePara(List<DataGridViewRow> rows,bool status)
        {

            if (debug1)
            {
                rows.ForEach(row =>
                {
                    updatePara(row, status);
                });
            }
        }
        /// <summary>
        /// 文档更新函数
        /// </summary>
        /// <param name="row">由DataGridView导出的行，指示需要更新的文档位置</param>
        /// <param name="status">更新标志，指【正确/错误】</param>
        void updatePara(DataGridViewRow row, bool status)
        {
            if(debug1) {
                //这里使用Try Catch结构，是为了避免在已经更新了文档之后，再次点击按钮，导致文档出错的
                //副作用是会导致未更新时也不会报错。
                try
                {
                    Microsoft.Office.Interop.Word.Range range = rangeDict[int.Parse(row.Cells[5].Value.ToString())];
                    range.Start += int.Parse(row.Cells[6].Value.ToString());
                    range.End -= 1;
                    range.Text = status ? "正确" : "错误";
                    int red = (0 << 8 | 0) << 8 | 255;
                    int v = !status ? ((192 << 8 | 112) << 8) | 0 : red;
                    range.Font.TextColor.RGB = v;
                }
                catch (Exception)
                {
                    
                }
            }
        }
        /// <summary>
        /// DataGridView单元格内容点击响应函数
        /// 
        /// 响应单元格的点击，实现复选框选择/中文自动跳转查词/正确按钮点击/错误按钮点击
        /// </summary>
        /// <param name="sender">被点击对象，这里不处理</param>
        /// <param name="e">点击事件参数，包括被点击单元格相关信息</param>
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 0:
                    ///点击第一列，也就是复选框的处理
                    dataGridView_Translate.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = !(bool)dataGridView_Translate.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    break;
                case 1:
                    ///点击第二列，也就是中文词时候的处理
                    if (e.RowIndex >= 0) 
                    { 
                        tabControl1.SelectedTab = tabPage2;
                        string wd = System.Web.HttpUtility.UrlEncode($"{dataGridView_Translate.Rows[e.RowIndex].Cells[e.ColumnIndex].Value}", System.Text.Encoding.UTF8);
                        webBrowser1.Navigate($"https://translate.google.cn/?sl=auto&tl=en&text={wd}&op=translate");                        
                    }
                    break;
                case 3:
                    ///点击第四列，也就是正确按钮时候的处理
                    { 
                        
                        DataGridViewRow row = dataGridView_Translate.Rows[e.RowIndex];
                        if (row.Cells[5].Value!=null) 
                        {
                            //buttonCell.Style.ForeColor = Color.FromArgb(255, 0, 0);                            
                            updatePara(row, true);
                            row.Cells[5].Value = null;
                            disableBtn(row,true);
                            
                        }
                        //updateItems();
                    }
                    break;
                case 4:
                    ///点击第五列，也就是错误按钮时候的处理
                    {
                        DataGridViewRow row = dataGridView_Translate.Rows[e.RowIndex];
                        if (row.Cells[5].Value!=null)
                        {
                            //buttonCell.Style.ForeColor = Color.FromArgb(0, 112, 192);
                            updatePara(row, false);
                            row.Cells[5].Value = null;
                            disableBtn(row,false);
                        }
                        //updateItems();
                    }
                    break;
            }
            ///文本不一致时候，启用刷新按钮
            if (TextHash != Globals.ThisAddIn.Application.ActiveDocument.Range().Text.GetHashCode())
            {
                btn_Refresh.Enabled = true;
            }
        }
        /// <summary>
        /// 无效化【正确/错误】按钮
        /// 
        /// 但是实际上这里只起到了修改颜色的作用
        /// </summary>
        /// <param name="row">需要无效化的行</param>
        /// <param name="flag">无效化标志，也就是点击了哪个按钮【正确/错误】</param>
        private void disableBtn(DataGridViewRow row,bool flag)
        {
            row.Cells[3].Value = false;
            row.Cells[4].Value = false;
            
            ///可以在下面代码中可以更改Color的值
            row.Cells[1].Style.BackColor = flag? Color.PaleVioletRed : Color.LightSlateGray;
            row.Cells[2].Style.BackColor = flag ? Color.LightSlateGray : Color.LightBlue;
        }
        /// <summary>
        /// 更新列表
        /// 这个函数对文档进行遍历，用正则表达式匹配需要处理的行，并存入itemStructs中，同时更新rangeDIct字典
        /// </summary>
        /// <param name="type">筛选类型</param>
        public void updateItems(string type)
        {
            dataGridView_Translate.Visible = false;            
            Microsoft.Office.Interop.Word.Range range;
            Regex regex;
            System.Text.RegularExpressions.MatchCollection matches;

            itemStructs.Clear();
            rangeDict.Clear();
            range = Globals.ThisAddIn.Application.ActiveDocument.Content;
            String pattern = "([\\S]+)\t([\\S]+)\t([\\S]+)\t未确认";
            dataGridView_Translate.Enabled = false;
            foreach (Section sec in range.Sections)
            {
                foreach (Paragraph prgrf in sec.Range.Paragraphs)
                {
                    regex = new Regex(pattern);
                    matches = regex.Matches(prgrf.Range.Text);
                    foreach (Match match in matches)
                    {
                        GroupCollection gp = match.Groups;
                        ItemStruct item = new ItemStruct
                        {
                            Type = gp[1].Value,
                            ChineseWords = gp[2].Value,
                            EnglishWords = gp[3].Value,
                            statustype = Status.unChecked,

                            ///这里是为了给itemStructs和rangeDict一个共同的唯一值索引
                            paragragh = prgrf.GetHashCode()
                        };
                        itemStructs.Add(item);
                        rangeDict.Add(prgrf.GetHashCode(), prgrf.Range);
                    }
                }
            }
            dataGridView_Translate.Enabled = true;
            dataGridView_Translate.Visible = true;            
            DataGridViewUpdate(itemStructs,type);
            selectNone();
            TextHash = range.Text.GetHashCode();            
        }
        /// <summary>
        /// 更新列表
        /// 这个函数对文档进行遍历，用正则表达式匹配需要处理的行，并存入itemStructs中，同时更新rangeDIct字典
        /// 根据组合框Combobox1选择项的不同，使用不同的参数调用处理函数
        /// </summary>
        public void updateItems()
        {
            if(comboBox1.SelectedIndex <= 0) { 
                updateItems(null);
            }
            else
            {
                updateItems(comboBox1.Text);
            }
        }
        /// <summary>
        /// 【√】【×】按钮处理函数
        /// </summary>
        /// <param name="sender">调用对象，也就是哪个按钮触发的事件</param>
        /// <param name="e">事件参数，这里不做处理</param>
        private void btn_Click(object sender, EventArgs e)
        {
            Button b =sender as Button;
            List<DataGridViewRow> rows = new List<DataGridViewRow>();            
            foreach(DataGridViewRow row in dataGridView_Translate.Rows)
            {
                if (bool.Parse(row.Cells[0].Value.ToString()))
                {
                    rows.Add(row);
                }
            }
            if (b.Name=="btn_Right")
            {
                updatePara(rows,true);
            }
            else
            {
                updatePara(rows, false);
            }
            updateItems();
            updateCombobox();
            selectNone();
        }
        /// <summary>
        /// 全选按钮处理函数
        /// 可以实现全选，全不选
        /// 逻辑还有可以再优化的地方
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_SelectAll_Click(object sender, EventArgs e)
        {
            if (btn_SelectAll.Text == "全选")
            {
                selectAll();
            }
            else
            {
                selectNone();
            }
        }
        /// <summary>
        /// 全选处理函数
        /// </summary>
        private void selectAll()
        {
            dataGridView_Translate.Visible = false;
            foreach (DataGridViewRow row in dataGridView_Translate.Rows)
            {
                row.Cells[0].Value = true;
            }
            dataGridView_Translate.Visible = true;
            btn_SelectAll.Text = "全不选";
        }
        /// <summary>
        /// 全不选处理函数
        /// </summary>
        private void selectNone()
        {

            dataGridView_Translate.Visible = false;
            foreach (DataGridViewRow row in dataGridView_Translate.Rows)
            {
                row.Cells[0].Value = false;
            }
            dataGridView_Translate.Visible = true;
            btn_SelectAll.Text = "全选";
        }
        /// <summary>
        /// 反选处理函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Inverse_Click(object sender, EventArgs e)
        {
            dataGridView_Translate.Visible = false;
            foreach (DataGridViewRow row in dataGridView_Translate.Rows)
            {
                row.Cells[0].Value = !bool.Parse(row.Cells[0].Value.ToString());
            }
            dataGridView_Translate.Visible = true;
        }
        /// <summary>
        /// 刷新按钮处理函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            dataGridView_Translate.Visible = false;
            updateItems();
            updateCombobox();
            btn_Refresh.Enabled = false;
            dataGridView_Translate.Visible = true;
        }
        /// <summary>
        /// 组合框更新函数
        /// 对组合框列表进行更新
        /// </summary>
        private void updateCombobox()
        {
            Dictionary<string, string> keys = new Dictionary<string, string>();
            comboBox1.Items.Clear();
            keys.Add("", "");
            itemStructs.ForEach(item => {
                if (!keys.ContainsKey(item.Type))
                {
                    keys.Add(item.Type, item.Type);
                }
            });
            comboBox1.Items.AddRange(keys.Keys.ToArray());
        }
        /// <summary>
        /// 组合框选择项更改处理函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateItems(comboBox1.Text);
        }
    }
}
