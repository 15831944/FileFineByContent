using ContentFileFind.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommForAdolph;

namespace ContentFileFind
{
    public partial class FindFileForm : Form
    {
        //搜索状态标识是否继续
        //public static bool CheckStatus = true;
        //选取文件夹下所有文件列表
        List<FileInfo> listFiles = new List<FileInfo>();
        //搜索状态
        public CountdownEvent handler;

        public FindFileForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 文件项单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBox_DoubleClick(object sender, EventArgs e)
        {
            //Clipboard.SetDataObject(listBox1.SelectedItem);
            ListBox listbox = (ListBox)sender;
            string fileSelectItem = listbox.SelectedItem.ToString();
            string path = fileSelectItem.Substring(0, fileSelectItem.LastIndexOf('\\'));
            //打开所在文件夹
            IOHelperExt.ExplorePath(fileSelectItem, true);
        }
        //选择搜索路径
        private void selectFile_Click(object sender, EventArgs e)
        {
            //定义委托
            //Action<string> actionDelegate = (x) => { listBox1.Items.Add(x); };

            DialogResult formResult = folderBrowserDialog1.ShowDialog();
            if (formResult == System.Windows.Forms.DialogResult.OK)
            {
                this.txtFilepath.Text = folderBrowserDialog1.SelectedPath;
                //清空文件列表
                //this.listBox1.Items.Clear();

                ThreadPool.QueueUserWorkItem(delegate
                {
                    //选取文件路径
                    DirectoryInfo dirInfo = new DirectoryInfo(this.txtFilepath.Text);
                    //获取文件夹下所有文件
                    IOHelperExt.GetAllFiles(dirInfo, ref listFiles);

                    //按文件大小排序
                    listFiles = listFiles.OrderBy(p => p.Length).ToList();

                    //foreach (var item in listFiles)
                    //{
                    //    listBox1.BeginInvoke(actionDelegate, item.FullName);
                    //}
                });
            }
        }



        /// <summary>
        /// 清空文件夹搜索数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Reset_Click(object sender, EventArgs e)
        {
            this.txtFilepath.Text = "";
        }

        /// <summary>
        /// 搜索内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_SearchKeyValue_Click(object sender, EventArgs e)
        {
            //重置为0表示停止
            if (btn_SearchKeyValue.Text == "停止")
            {
                handler.Reset(0);
                btn_SearchKeyValue.Text = btn_SearchKeyValue.Text == "停止" ? "搜索" : "停止";
                return;
            }

            btn_SearchKeyValue.Text = btn_SearchKeyValue.Text == "停止" ? "搜索" : "停止";

            //查找到的文件列表添加
            Action<string> listbox2Delegate = (fileItem) =>
            {
                if (!listBox2.Items.Contains(fileItem))
                {
                    listBox2.Items.Add(fileItem);
                }
            };

            //查找到的文件列表添加
            Action<string> disedDelegate = (fileCount) =>
            {
                label1.Text = fileCount;
            };

            IList<string> fileList = new List<string>();
            string searchValue = txt_KeyValue.Text;
            if (string.IsNullOrEmpty(searchValue))
            {
                MessageBox.Show("请输入查找内容");
                return;
            }
            btn_ClearResult_Click(sender, e);


            //分段
            var lengthInterval = 1;

            List<List<FileInfo>> lists = new List<List<FileInfo>>();

            var itemsCount = listFiles.Count / lengthInterval;
            var overFlowCount = listFiles.Count % lengthInterval;

            //得总线程数
            var threadCount = itemsCount == 0 ? 1 : lengthInterval + 1;

            int startIndex = 0, takeCount = 0;

            for (int i = 0; i < threadCount; i++)
            {
                //商为0时取余数，否则取商
                takeCount = itemsCount == 0 ? overFlowCount : itemsCount;
                var itemlist = listFiles.Skip(startIndex).Take(takeCount).ToList();
                lists.Add(itemlist);
                startIndex += takeCount;
            }

            //计数 余数>0是则不能整除   商为0时表示刚好整除 则总数为lengthInterval  否则+1
            handler = new CountdownEvent(listFiles.Count);
            this.label3.Text = listFiles.Count.ToString();
            foreach (var item in lists)
            {
                ThreadPool.QueueUserWorkItem(delegate
                    {
                        ContentCheck(searchValue, item, handler, listbox2Delegate, disedDelegate);
                        //handler.Signal();
                    });
            }

            Thread checkThreadPool = new Thread(new ThreadStart(delegate
            {
                while (true)
                {
                    if (handler.CurrentCount == 0)
                    {
                        MessageBox.Show(string.Format("共找到{0}个文件", listBox2.Items.Count), "搜索完成");
                        break;
                    }
                    Thread.Sleep(200);
                }
            }));
            checkThreadPool.Start();
        }

        /// <summary>
        /// 内容查找
        /// </summary>
        /// <param name="searchValue"></param>
        private void ContentCheck(string searchValue, IList<FileInfo> fileList, CountdownEvent handler, Action<string> listbox2Delegate, Action<string> disedDelegate)
        {
            CommForAdolph.OfficeHelper officeHelper = new CommForAdolph.OfficeHelper();
            foreach (var item in fileList)//listBox1.Items)
            {
                if (handler.CurrentCount == 0)
                {
                    return;
                }

                //文件全路径
                var itemStr = item.FullName.ToString();
                int fileNameStartIndex = itemStr.LastIndexOf('\\');

                if (itemStr.Substring(fileNameStartIndex, itemStr.Length - fileNameStartIndex).Contains(searchValue))
                {
                    if (!listBox2.Items.Contains(itemStr))
                        this.listBox2.BeginInvoke(listbox2Delegate, itemStr);
                    //listBox2.Items.Add(item);
                }

                if (itemStr.Contains("~$"))
                    continue;

                if (item.Extension == ".doc" || item.Extension == ".docx")
                {

                    if (officeHelper.CheckWordContent(itemStr, searchValue))
                    {
                        this.listBox2.BeginInvoke(listbox2Delegate, itemStr);
                    }
                }
                else if (item.Extension == ".xls" || item.Extension == ".xlsx")
                {
                    if (officeHelper.CheckExcelContent(itemStr, searchValue))
                    {
                        this.listBox2.BeginInvoke(listbox2Delegate, itemStr);
                    }
                }
                //检查搜索内容
                else if (IOHelperExt.CheckContent(searchValue, itemStr) != -1)
                {
                    this.listBox2.BeginInvoke(listbox2Delegate, itemStr);
                }

                if (handler.CurrentCount > 0)
                {
                    handler.Signal();
                    this.label1.BeginInvoke(disedDelegate, handler.CurrentCount.ToString());
                }
            }
        }

        /// <summary>
        /// 清空搜索内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ClearResult_Click(object sender, EventArgs e)
        {
            //this.txt_KeyValue.Text = "";
            this.listBox2.Items.Clear();
        }
    }
}
