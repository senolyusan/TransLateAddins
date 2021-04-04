using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Word = Microsoft.Office.Interop.Word;
namespace TranslateAddins
{
    public partial class RegularFindAndReplace : UserControl
    {

        private DataGridViewTextBoxColumn ResultList = new DataGridViewTextBoxColumn
        {

        };
        private DataGridViewTextBoxColumn rng = new DataGridViewTextBoxColumn
        {
        };
        private DataGridViewTextBoxColumn ReplaceTo = new DataGridViewTextBoxColumn
        {
        };
        private DataGridViewButtonColumn scrollTo = new DataGridViewButtonColumn
        {
        };
        private DataGridViewButtonColumn replaceOnce = new DataGridViewButtonColumn
        {
        };

        private DataGridView dataGridView_ResultList = new DataGridView();




        public RegexOptions regexOptions = RegexOptions.None;
        List<int> chkdLstBoxHeight = new List<int>();
        List<string> tltipArr = new List<string>();
        List<Word.Range> ranges = new List<Word.Range>();
        MatchCollection matchCollection;
        /// <summary>
        /// dataGridView控件初始化
        /// </summary>
        public void dataGridView_ResultList_Init()
        {
            ResultList = new DataGridViewTextBoxColumn
            {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                Name = "ResultList",
                HeaderText = "结果列表",
                FillWeight = 50,
                MinimumWidth = 80,
                ReadOnly = true,
                SortMode = DataGridViewColumnSortMode.NotSortable

            };
            rng = new DataGridViewTextBoxColumn
            {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                Name = "rng",
                HeaderText = "",
                Visible = false,
                ReadOnly = true,
                Width = 0,
                SortMode = DataGridViewColumnSortMode.NotSortable
            };
            ReplaceTo = new DataGridViewTextBoxColumn
            {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                Name = "ReplaceTo",
                HeaderText = "替换为",
                FillWeight = 50,
                MinimumWidth = 80,
                Visible = false,
                SortMode = DataGridViewColumnSortMode.NotSortable
            };
            scrollTo = new DataGridViewButtonColumn
            {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                Name = "scrollTo",
                FillWeight = 1,
                UseColumnTextForButtonValue = true,
                MinimumWidth = 60,
                Width = 60,
                Text = "转到",
                ReadOnly = true,
                SortMode = DataGridViewColumnSortMode.NotSortable,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    NullValue = "转到"
                }
            };
            replaceOnce = new DataGridViewButtonColumn
            {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                Name = "ReplaceOnce",
                FillWeight = 1,
                UseColumnTextForButtonValue = false,
                MinimumWidth = 60,
                Width = 60,
                Visible = false,
                Text = "替换",
                ReadOnly = true,
                SortMode = DataGridViewColumnSortMode.NotSortable,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    NullValue = "替换"
                }
            };

            dataGridView_ResultList.Columns.Add(ResultList);
            dataGridView_ResultList.Columns.Add(rng);
            dataGridView_ResultList.Columns.Add(ReplaceTo);
            dataGridView_ResultList.Columns.Add(scrollTo);
            dataGridView_ResultList.Columns.Add(replaceOnce);

            dataGridView_ResultList.Dock = DockStyle.Fill;
            this.splitContainer1.Panel2.Controls.Add(dataGridView_ResultList);
            dataGridView_ResultList.RowHeadersVisible = false;
            dataGridView_ResultList.AllowUserToAddRows = false;
            dataGridView_ResultList.Visible = true;
            dataGridView_ResultList.CellContentClick += DataGridView_ResultList_CellContentClick;
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        public RegularFindAndReplace()
        {
            InitializeComponent();
            dataGridView_ResultList_Init();
            tabpage_Resize();
            chkdLstBoxHeight.Add(0);
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                chkdLstBoxHeight.Add(checkedListBox1.GetItemRectangle(i).Bottom);
            }

            tltipArr.Add($"None\t0\r\n指定不设置任何选项。 有关正则表达式引擎的默认行为的详细信息，请参阅正则表达式选项文章中的“默认选项”部分。\n");
            tltipArr.Add($"IgnoreCase\t1\r\n指定不区分大小写的匹配。 有关详细信息，请参阅正则表达式选项文章中的“不区分大小写匹配”部分。\n");
            tltipArr.Add($"Multiline\t2\r\n多行模式。 更改 ^ 和 $ 的含义，使它们分别在任意一行的行首和行尾匹配，而不仅仅在整个字符串的开头和结尾匹配。 有关详细信息，请参阅正则表达式选项文章中的“多行模式”部分。\n");
            tltipArr.Add($"ExplicitCapture\t4\r\n指定唯一有效的捕获是显式命名或编号的(?< name > ...) 形式的组。这使未命名的圆括号可以充当非捕获组，并且不会使表达式的语法(?:...) 显得笨拙。有关详细信息，请参阅正则表达式选项文章中的“仅显式捕获”部分。\n");
            tltipArr.Add($"Compiled\t8\r\n指定将正则表达式编译为程序集。 这会产生更快的执行速度，但会增加启动时间。 在调用 Options 方法时，不应将此值分配给 CompileToAssembly(RegexCompilationInfo[], AssemblyName) 属性。 有关详细信息，请参阅正则表达式选项文章中的“已编译的正则表达式”部分。\n");
            tltipArr.Add($"Singleline\t16\r\n指定单行模式。 更改点(.) 的含义，以使它与每个字符（而不是除 \\n 之外的所有字符）匹配。 有关详细信息，请参阅正则表达式选项文章中的“单行模式”部分。\n");
            tltipArr.Add($"IgnorePatternWhitespace\t32\r\n消除模式中的非转义空白并启用由 # 标记的注释。 但是，此值不影响或消除标记单独的正则表达式语言元素的开头的字符类、数值量词或标记的空格。 有关详细信息，请参阅正则表达式选项一文中的“忽略空格”部分。\n");
            tltipArr.Add($"RightToLeft\t64\r\n指定搜索从右向左而不是从左向右进行。 有关详细信息，请参阅正则表达式选项文章中的“从右到左模式”部分。\n");
            tltipArr.Add($"ECMAScript\t256\r\n为表达式启用符合 ECMAScript 的行为。 该值只能与 IgnoreCase、Multiline 和 Compiled 值一起使用。 该值与其他任何值一起使用均将导致异常。有关 ECMAScript 选项的详细信息，请参阅正则表达式选项文章中的“ECMAScript 匹配行为”部分。\n");
            tltipArr.Add($"CultureInvariant\t512\r\n指定忽略语言中的区域性差异。 有关详细信息，请参阅正则表达式选项文章中的“使用固定区域性的比较”部分。");
            tltipArr.Add($"");

            try
            {
                checkedListBox1.MouseMove -= CheckedListBox1_MouseMove;
            }
            finally
            {
                checkedListBox1.MouseMove += CheckedListBox1_MouseMove;
            }
            try
            {
                checkedListBox1.SelectedIndexChanged -= CheckedListBox1_SelectedIndexChanged;
            }
            finally
            {
                checkedListBox1.SelectedIndexChanged += CheckedListBox1_SelectedIndexChanged;
            }
        }
        /// <summary>
        /// 正则选项事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            regexOptions = RegexOptions.None;
            Array s = Enum.GetValues(typeof(RegexOptions));

            CheckedListBox c = checkedListBox1;
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                RegexOptions options1 = RegexOptions.None;
                if (c.CheckedIndices.Contains(i))
                {
                    options1 = (RegexOptions)Enum.Parse(typeof(RegexOptions), s.GetValue(i+1).ToString(), true);
                }
                regexOptions |= options1;
            }
            if ((regexOptions & RegexOptions.ECMAScript) != RegexOptions.None)
            {
                regexOptions &= RegexOptions.ECMAScript | RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled;
            }
        }
        /// <summary>
        /// 正则选项列表说明
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckedListBox1_MouseMove(object sender, MouseEventArgs e)
        {
            int i, heightsum;
            for (i = 0; i < chkdLstBoxHeight.Count; i++)
            {
                if (e.Y < chkdLstBoxHeight[i])
                {
                    break;
                }
                else if (e.Y > chkdLstBoxHeight[chkdLstBoxHeight.Count - 1])
                {
                    i++;
                }
            }
            TextBox_OptionsTips.Text = tltipArr[i];

        }
        /// <summary>
        /// 选择Tab页
        /// </summary>
        /// <param name="index"></param>
        public void tabSelect(int index)
        {
            tabControl_RegularFindAndReplace.SelectedIndex = (index < tabControl_RegularFindAndReplace.TabCount || index >= 0) ? index : tabControl_RegularFindAndReplace.SelectedIndex;
            tabControl_RegularFindAndReplace_SelectedIndexChanged(null,null);
        }
        /// <summary>
        /// 文本框内容发生变化事件
        /// 文本框内容发生变化时，需要判断文本框内容，进行自适应高度调节，同时，同步【查找】和【替换】两个页面的内容。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_TextChanged(object sender, EventArgs e)
        {

            RichTextBox rtb = sender as RichTextBox;
            if (rtb == RTB_Find1)
            {
                try
                {
                    RTB_Find2.TextChanged -= textBox_TextChanged;
                }
                finally
                {
                    RTB_Find2.Text = RTB_Find1.Text;
                    RTB_Find2.TextChanged += textBox_TextChanged;
                }
            }
            else if (rtb == RTB_Find2)
            {
                try
                {
                    RTB_Find1.TextChanged -= textBox_TextChanged;
                }
                finally
                {
                    RTB_Find1.Text = RTB_Find2.Text;
                    RTB_Find1.TextChanged += textBox_TextChanged;
                }
            }
            tabpage_Resize();
        }
        /// <summary>
        /// 变更富文本框尺寸
        /// 根据文本框内容自适应调整尺寸
        /// </summary>
        /// <param name="rtb">富文本框</param>
        private void RTBSizeAutoChange(RichTextBox rtb)
        {
            if (rtb.Text.Length > 0)
            {
                int rowNumber = rtb.GetLineFromCharIndex(rtb.TextLength);
                if (rowNumber > 0)
                {
                    float rowHeight = (rtb.GetPositionFromCharIndex(rtb.TextLength).Y - rtb.GetPositionFromCharIndex(0).Y) / (rowNumber);
                    rtb.Height = (int)(rowNumber * rowHeight) + rtb.PreferredHeight;
                }
                else
                {
                    rtb.Height = rtb.PreferredHeight;
                }
            }
            else
            {
                rtb.Height = rtb.PreferredHeight;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabPage_Find_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 分隔符移动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            tabpage_Resize();
        }
        /// <summary>
        /// 分割视图尺寸变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void splitContainer1_SizeChanged(object sender, EventArgs e)
        {
            tabpage_Resize();
        }
        /// <summary>
        /// tab页自动调整
        /// </summary>
        public void tabpage_Resize()
        {
            if (tabControl_RegularFindAndReplace.SelectedTab == tabPage_Find)
            {
                RTBSizeAutoChange(RTB_Find1);
                if (RTB_Find1.Height + btn_Find.Height >= splitContainer1.Panel1.Height - 10)
                {
                    RTB_Find1.Height = splitContainer1.Panel1.Height - 10 - btn_Find.Height;
                }
                btn_Find.Top = RTB_Find1.Bottom + 3;

                btn_Find.Left = splitContainer1.Panel1.Width - 10 - btn_Find.Width;
                RTB_Find1.AutoScrollOffset = new Point(0, 0);

            }
            else if (tabControl_RegularFindAndReplace.SelectedTab == tabPage_Replace)
            {

                RTBSizeAutoChange(RTB_Find2);
                RTBSizeAutoChange(RTB_replace);
                if (RTB_Find2.Height + RTB_replace.Height + label_ReplaceTo.Height >= splitContainer1.Panel1.Height - btn_ReplaceAll.Height)
                {
                    int height_sum = RTB_Find2.Height + RTB_replace.Height;

                    RTB_Find2.Height = (RTB_Find2.Height * (splitContainer1.Panel1.ClientRectangle.Height - btn_ReplaceAll.Height - label_ReplaceTo.Height) / height_sum);
                    RTB_replace.Height = (RTB_replace.Height * (splitContainer1.Panel1.ClientRectangle.Height - btn_ReplaceAll.Height - label_ReplaceTo.Height) / height_sum);
                }
                else
                {
                }
                label_ReplaceTo.Top = RTB_Find2.Bottom + 3;
                RTB_replace.Top = label_ReplaceTo.Bottom + 3;

                
                btn_ReplaceAll.Left = splitContainer1.Panel2.Width - 10 - btn_ReplaceAll.Width;
                btn_Replace_ListOut.Left = btn_ReplaceAll.Left - 3 - btn_Replace_ListOut.Width;
                
                btn_ReplaceAll.Top = RTB_replace.Bottom + 3;
                btn_Replace_ListOut.Top = RTB_replace.Bottom + 3;

                RTB_Find2.AutoScrollOffset = new Point(0, 0);
                RTB_replace.AutoScrollOffset = new Point(0, 0);

            }
        }
        /// <summary>
        /// tab页切换事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl_RegularFindAndReplace_SelectedIndexChanged(object sender, EventArgs e)
        {
            tabpage_Resize();
            if (tabControl_RegularFindAndReplace.SelectedTab == tabPage_Options)
            {
                dataGridView_ResultList.Visible = false;
                TextBox_OptionsTips.Visible = true;
            }
            else
            {
                if (tabControl_RegularFindAndReplace.SelectedTab == tabPage_Find)
                {
                    dataGridView_ResultList.Columns[2].Visible = false;
                    dataGridView_ResultList.Columns[3].Visible = true;
                    dataGridView_ResultList.Columns[4].Visible = false;

                }
                else
                {
                    dataGridView_ResultList.Columns[0].DefaultCellStyle = new DataGridViewCellStyle
                    {
                        WrapMode = DataGridViewTriState.True,
                    };
                    dataGridView_ResultList.Columns[2].DefaultCellStyle = new DataGridViewCellStyle
                    {
                        WrapMode = DataGridViewTriState.True,
                    };
                    dataGridView_ResultList.Columns[2].Visible = true;
                    dataGridView_ResultList.Columns[3].Visible = true;
                    dataGridView_ResultList.Columns[4].Visible = true;
                }
                dataGridView_ResultList.Rows.Clear();
                TextBox_OptionsTips.Visible = false;
                dataGridView_ResultList.Visible = true;
            }
        }
        /// <summary>
        /// 正则查找匹配函数
        /// </summary>
        /// <param name="pattern">匹配字符串</param>
        /// <param name="instr">匹配源</param>
        /// <param name="regexOptions">正则选项，默认为RegexOptions.None</param>
        /// <see cref="https://docs.microsoft.com/zh-cn/dotnet/standard/base-types/regular-expression-language-quick-reference"/>    
        /// <returns>MatchCollection类型</returns>
        private MatchCollection RegularFind(string pattern, string instr, RegexOptions regexOptions = RegexOptions.None)
        {
            //MatchCollection matchCollection;
            Regex regex = new Regex(pattern, regexOptions);
            matchCollection = regex.Matches(instr);
            return matchCollection;
        }
        /// <summary>
        /// 正则替换匹配函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="replaceText">替换文本</param>
        private void RegularReplace(object sender, string replaceText)
        {
            Word.Document document = Globals.ThisAddIn.Application.ActiveDocument;
            Word.Range range = document.Range();
             matchCollection = RegularFind(RTB_Find2.Text, range.Text, regexOptions);
            dataGridView_ResultList.Rows.Clear();
            ranges.Clear();
            foreach (Match m in matchCollection)
            {
                Word.Range rng = document.Range(m.Index, m.Index + m.Length);
                Word.Range rngExpand = document.Range(m.Index, m.Index + m.Length);
                Word.WdUnits wd = Word.WdUnits.wdSentence;
                if(m.Length<10)                rngExpand.Expand(wd);
                if (rng.Start - rngExpand.Start > 10)
                {
                    rngExpand.Start = rng.Start - 10;
                }
                ranges.Add(rng);
                dataGridView_ResultList.Rows.Add(rngExpand.Text, rng, m.Result(replaceText));

            }
        }

        private void RegularFind(object sender)
        {

            Word.Document document = Globals.ThisAddIn.Application.ActiveDocument;
            Microsoft.Office.Interop.Word.Range range = document.Range();
             matchCollection = RegularFind(RTB_Find1.Text, range.Text, regexOptions);
            dataGridView_ResultList.Rows.Clear();
            ranges.Clear();
            foreach (Match m in matchCollection)
            {
                Word.Range rng = document.Range(m.Index, m.Index + m.Length);
                Word.Range rngExpand = document.Range(m.Index, m.Index + m.Length);
                Word.WdUnits wd = Word.WdUnits.wdSentence;
                rngExpand.Expand(wd);
                if (rng.Start - rngExpand.Start > 10)
                {
                    rngExpand.Start = rng.Start - 10;
                }
                ranges.Add(rng);
                dataGridView_ResultList.Rows.Add(rngExpand.Text, rng);
            }
        }
        /// <summary>
        /// 查找按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Find_Click(object sender, EventArgs e)
        {
            RegularFind(sender);
        }
        /// <summary>
        /// DataGridView内容点击事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridView_ResultList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                switch (e.ColumnIndex)
                {
                    case 0:
                    case 2:
                    case 3:
                        {
                            scrollto(ranges[e.RowIndex]);
                        }
                        break;
                    case 4:
                        {
                            long ParagraphsCount_Old, ParagraphsCount_New;
                            Word.Range range = ranges[e.RowIndex];
                            ParagraphsCount_Old = range.Paragraphs.Count;
                            replaceOne(range, dataGridView_ResultList.Rows[e.RowIndex].Cells["ReplaceTo"].Value as string);
                            ParagraphsCount_New = range.Paragraphs.Count;
                            if (ParagraphsCount_New>1 || ParagraphsCount_Old>1)
                            {
                                /**
                                 * 跨越多个段的替换操作会对暂存的结果造成影响，导致后续替换出错，因此这里对后续进行无效化处理。
                                 * 
                                 **/
                                for(int i = e.RowIndex; i < ranges.Count; i++)
                                {
                                    ranges.RemoveAt(i);
                                    dataGridView_ResultList.Rows.RemoveAt(i);
                                }
                            }
                            else
                            {
                                dataGridView_ResultList.Rows.RemoveAt(e.RowIndex);
                                ranges.RemoveAt(e.RowIndex);
                            }
                            dataGridView_ResultList.Refresh();                            
                        }
                        break;
                    default:
                        break;
                }
                if (e.ColumnIndex == 2)
                {
                    scrollto(dataGridView_ResultList.Rows[e.RowIndex].Cells[1].Value as Word.Range);
                }
            }
        }
        /// <summary>
        /// 单个替换处理
        /// </summary>
        /// <param name="range"></param>
        /// <param name="str"></param>
        private void replaceOne(Word.Range range, string str)
        {
            range.Text = str;
        }
        /// <summary>
        /// 转到处理
        /// </summary>
        /// <param name="range"></param>
        private void scrollto(Word.Range range)
        {
            range.Select();
        }
        /// <summary>
        /// 列出全部按钮处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Replace_ListOut_Click(object sender, EventArgs e)
        {
            RegularReplace(sender, RTB_replace.Text);
        }
        /// <summary>
        /// 替换全部按钮处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ReplaceAll_Click(object sender, EventArgs e)
        {
            RegularReplace(sender, RTB_replace.Text);
            for (int i= ranges.Count-1; i >= 0; --i)
            {
                ranges[i].Text = dataGridView_ResultList.Rows[i].Cells["ReplaceTo"].Value as string;
                ranges.RemoveAt(i);
                dataGridView_ResultList.Rows.RemoveAt(i);
            }
        }
    }
}
