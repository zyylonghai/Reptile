using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wesley.Crawler.StrongCrawler;
using Wesley.Crawler.StrongCrawler.Events;
using OpenQA.Selenium;
using Newtonsoft.Json;
using Wesley.Crawler.StrongCrawler.Models;
using System.Text.RegularExpressions;
using System.Configuration;

namespace AppStart
{
    public partial class Form1 : Form
    {
        private delegate void SetTextCallback(object val);

        private Dictionary<string, StrongCrawler> contains = new Dictionary<string, StrongCrawler>();
        private int index = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            AddTast tast = new AddTast();
            tast.Show();
        }

        /// <summary>
        /// 载入数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            DBHelp dbhelp = new DBHelp();

            dataSet1.TastMast.FillData(dbhelp.GetDataTable("select *From TastMast a,TastDetail b where a.ID=b.ID and a.IsUse=1"));
            //this.dataGridView1.DataSource = dt;


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == this.dataGridView1.Columns["detail"].Index)
            {
                AddTast tast = new AddTast(this.dataGridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString());
                tast.Show();
                //MessageBox.Show(this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
            }
        }
        /// <summary>
        /// 启动爬虫
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            ////this.timer1.Interval =  1000 * 10;
            ////this.timer1.Start();
            //TastMast mast;
            //List<ElementObject> elemlist;
            //if (index < this.dataGridView1.Rows.Count)
            //{
            //    DataGridViewRow row = this.dataGridView1.Rows[index];
            //    //foreach (DataGridViewRow row in this.dataGridView1.Rows)
            //    //{
            //    mast = new TastMast();
            //    mast.ID = row.Cells["ID"].Value.ToString();
            //    mast.Url = row.Cells["url"].Value.ToString();
            //    mast.TastNm = row.Cells["TastNm"].Value.ToString();
            //    mast.LoginUrl = row.Cells["LoginUrl"].Value.ToString();
            //    mast.UserID = row.Cells["UserID"].Value.ToString();
            //    mast.UserInputId = row.Cells["UserInputId"].Value.ToString();
            //    mast.Pwd = row.Cells["Pwd"].Value.ToString();
            //    mast.PwdInputId = row.Cells["PwdInputId"].Value.ToString();
            //    mast.LoginBtnId = row.Cells["LoginBtnId"].Value.ToString();
            //    elemlist = JsonConvert.DeserializeObject<List<ElementObject>>(row.Cells["infoStr"].Value.ToString());
            //    StarReptile(mast, elemlist);
            //}

            //}
            this.timer1.Start();
            this.toolStripButton3.Enabled = false;
            this.toolStripButton5.Enabled = true;
        }

        private void StarReptile(TastMast mast, List<ElementObject> elemlist)
        {
            var hotelUrl = mast.Url;
            StrongCrawler hotelCrawler = null;
            if (!contains.TryGetValue(mast.ID, out hotelCrawler))
            {
                hotelCrawler = new StrongCrawler();
                contains.Add(mast.ID, hotelCrawler);
            }
            hotelCrawler.Data = new List<ReptileInfo>();
            //List<ReptileInfo> data = new List<ReptileInfo>();
            ReptileInfo info;
            hotelCrawler.OnStart += HotelCrawler_OnStart;
            //hotelCrawler.OnStart += (s, starargvs) =>
            //{
            //    //Console.WriteLine("爬虫开始抓取地址：" + starargvs.Uri.ToString());
            //    SetCtrlValues("爬虫开始抓取地址：" + starargvs.Uri.ToString());
            //    //this.listBox1.Items.Add("爬虫开始抓取地址：" + starargvs.Uri.ToString());

            //};
            hotelCrawler.OnError += (s, starargvs) =>
            {
                //Console.WriteLine("爬虫抓取出现错误：" + starargvs.Uri.ToString() + "，异常消息：" + starargvs.Exception.ToString());
                SetCtrlValues("爬虫抓取出现错误：" + starargvs.Uri.ToString() + "，异常消息：" + starargvs.Exception.Message +starargvs .Exception .StackTrace);
                //if (!mast.isAMZPRO)
                //{
                    hotelCrawler.ChDriver = null;
                //}
                //hotelCrawler.OnStart -= (s2, starargvs2) =>
                // {

                // };
                //this.listBox1.Items.Add("爬虫抓取出现错误：" + starargvs.Uri.ToString() + "，异常消息：" + starargvs.Exception.ToString());
            };
            hotelCrawler.OnCompleted += HotelCrawler_OnCompleted;
            //hotelCrawler.OnCompleted += (s, starargvs) =>
            //{
            //    hotelCrawler.OnStart -= HotelCrawler_OnStart;
            //    DBHelp help = new DBHelp();
            //    foreach (ReptileInfo item in data)
            //    {
            //        help.InsertData(item);
            //    }

            //};
            hotelCrawler.Onfinally += (s, finallyobj) =>
            {
                hotelCrawler.OnStart -= HotelCrawler_OnStart;
                hotelCrawler.OnCompleted -= HotelCrawler_OnCompleted;
                index++;
                System.Threading.Thread.Sleep(5000);
                DoNextTastStarReptile();

            };
            var operation = new Operation
            {
                Action = (x) =>
                {
                    if (mast.isAMZPRO) //从AMZPRO插件取数
                    {
                        string amzdata = ConfigurationManager.AppSettings["amzpro"].ToString();
                        var webelements = x.FindElements(By.XPath(amzdata));
                        string commid = string.Empty;
                        string asin =null;
                        for (int i = 1; i <= webelements.Count; i++)
                        {
                            commid = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                            foreach (ElementObject elem in elemlist)
                            {
                                try
                                {
                                    info = new ReptileInfo();
                                    info.ID = mast.ID;
                                    info.FieldID = elem.ElemID;
                                    info.FieldName = elem.FieldNm;
                                    info.ReptileDT = System.DateTime.Now;
                                    info.CommId = commid;
                                    IWebElement webelement = null;
                                    if (!string.IsNullOrEmpty(elem.Xpath))
                                    {
                                        webelement = x.FindElement(By.XPath(string.Format(elem.Xpath, i)));
                                    }
                                    if (webelement != null)
                                    {
                                        info.FieldValue = elem.IsGetnum ? GetNums(webelement.Text) : webelement.Text;
                                        if (elem.ElemID == "Product Name")
                                        {
                                            string[] array = webelement.GetAttribute("href").Split ('/');
                                            if (array!=null &&array .Length >0)
                                            {
                                                asin = array[array.Length - 1];
                                            }
                                            
                                        }
                                    }
                                    hotelCrawler.Data.Add(info);
                                }
                                catch (Exception ex)
                                {
                                    SetCtrlValues(string.Format("{0}{1}", mast.Url, ex.Message));
                                }
                            }

                            #region 新增Asin字段数据
                            info = new ReptileInfo();
                            info.ID = mast.ID;
                            info.CommId = commid;
                            info.FieldID = "ASIN";
                            info.FieldName = ConfigurationManager.AppSettings["asinname"].ToString();
                            info.FieldValue = asin;
                            info.ReptileDT = System.DateTime.Now;
                            hotelCrawler.Data.Add(info);
                            #endregion
                            //var field = item.FindElement(By.XPath("//a[@ng-if='p.fbaFees']"));
                            //var field1 = x.FindElement(By.XPath(string.Format("/html/body/amzscout-pro/div/draggable/section/main/content/div/div[2]/div[{0}]/div[9]", i)));

                        }

                    }
                    else
                    {
                        foreach (ElementObject elem in elemlist)
                        {
                            try
                            {
                                info = new ReptileInfo();
                                info.ID = mast.ID;
                                info.FieldName = elem.FieldNm;
                                info.ReptileDT = System.DateTime.Now;
                                IWebElement webelement = null;

                                if (!string.IsNullOrEmpty(elem.ElemID))
                                {
                                    info.FieldID = elem.ElemID;
                                    webelement = x.FindElement(By.Id(elem.ElemID));
                                }
                                if (!string.IsNullOrEmpty(elem.ElemNm))
                                {

                                    if (webelement != null)
                                    {
                                        webelement = webelement.FindElement(By.Name(elem.ElemNm));
                                    }
                                    else
                                        webelement = x.FindElement(By.Name(elem.ElemNm));
                                }
                                if (!string.IsNullOrEmpty(elem.ElemClass))
                                {
                                    try
                                    {
                                        if (webelement != null)
                                        {
                                            webelement = webelement.FindElement(By.ClassName(elem.ElemClass));
                                        }
                                        else
                                        {
                                            webelement = x.FindElement(By.ClassName(elem.ElemClass));
                                        }
                                    }
                                    catch (Exception exc2)
                                    {
                                        webelement = x.FindElement(By.XPath(elem.ElemClass));
                                        //webelement = lstelem[0];
                                        //webelement = x.FindElement(By.XPath(elem.ElemClass));
                                    }
                                }

                                if (!string.IsNullOrEmpty(elem.Xpath))
                                {
                                    if (webelement != null)
                                    {
                                        webelement = webelement.FindElement(By.XPath(elem.Xpath));

                                    }
                                    else
                                    {
                                        webelement = x.FindElement(By.XPath(elem.Xpath));
                                    }
                                }
                                if (webelement != null)
                                {
                                    info.FieldValue = elem.IsGetnum ? GetNums(webelement.Text) : webelement.Text;
                                }
                                hotelCrawler.Data.Add(info);
                            }
                            catch (Exception ex)
                            {
                                SetCtrlValues(string.Format("{0}{1}", mast.Url, ex.Message));
                            }
                        }
                    }
                },
                Condition = (x) =>
                {
                    //判断Ajax评论内容是否已经加载成功
                    //return x.FindElement(By.XPath("//*[@id='commentList']")).Displayed && x.FindElement(By.XPath("//*[@id='hotel_info_comment']/div[@id='commentList']")).Displayed && !x.FindElement(By.XPath("//*[@id='hotel_info_comment']/div[@id='commentList']")).Text.Contains("点评载入中");
                    return true;
                },
                Timeout = 500
            };

            hotelCrawler.Start(mast, null, operation);//不操作JS先将参数设置为NULL
        }

        private void HotelCrawler_OnCompleted(object sender, OnCompletedEventArgs e)
        {
            DBHelp help = new DBHelp();
            StrongCrawler crawler = sender as StrongCrawler;
            string commid = DateTime.Now.ToString("yyyyMMddHHmmss");
            foreach (ReptileInfo item in crawler.Data)
            {
                item.CommId = string.IsNullOrEmpty(item.CommId) ? commid : item.CommId;
                help.InsertData(item);
            }

        }

        private void HotelCrawler_OnStart(object sender, OnStartEventArgs e)
        {
            SetCtrlValues("爬虫开始抓取地址：" + e.Uri.ToString()+"   时间:"+System.DateTime.Now.ToString ());
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TastMast mast;
            List<ElementObject> elemlist;
            if (Constvariable.Timing != -1)//定时抓取数据
            {
                if (System.DateTime.Now.Hour != Constvariable.Timing || DateTime.Now.Day == Constvariable.day)
                {
                    return;
                }
            }
            index = 0;
            #region 重新载入数据
            DBHelp dbhelp = new DBHelp();
            dataSet1.TastMast.FillData(dbhelp.GetDataTable("select *From TastMast a,TastDetail b where a.ID=b.ID and a.IsUse=1"));
            contains.Clear();
            #endregion
            //if (index < this.dataGridView1.Rows.Count)
            //{
            DataGridViewRow row = this.dataGridView1.Rows[index];
            //foreach (DataGridViewRow row in this.dataGridView1.Rows)
            //{
            mast = new TastMast();
            mast.ID = row.Cells["ID"].Value.ToString();
            mast.Url = row.Cells["url"].Value.ToString();
            mast.TastNm = row.Cells["TastNm"].Value.ToString();
            mast.LoginUrl = row.Cells["LoginUrl"].Value.ToString();
            mast.UserID = row.Cells["UserID"].Value.ToString();
            mast.UserInputId = row.Cells["UserInputId"].Value.ToString();
            mast.Pwd = row.Cells["Pwd"].Value.ToString();
            mast.PwdInputId = row.Cells["PwdInputId"].Value.ToString();
            mast.LoginBtnId = row.Cells["LoginBtnId"].Value.ToString();
            mast.isAMZPRO = ConvertoBool(row.Cells["isAMZPRO"].Value.ToString ());
            mast.SearchKey = row.Cells["SearchKey"].Value.ToString();
            mast.SearchInputID = row.Cells["SearchInputID"].Value.ToString();
            mast.SearchBtnID = row.Cells["SearchBtnID"].Value.ToString();
            mast.AMZxpath = row.Cells["AMZxpath"].Value.ToString();

            elemlist = JsonConvert.DeserializeObject<List<ElementObject>>(row.Cells["infoStr"].Value.ToString());
            StarReptile(mast, elemlist);
            Constvariable.day = DateTime.Now.Day;
            //}
            //}
            //else
            //{
            //    index = 0;
            //}
        }

        //执行下一条任务
        private void DoNextTastStarReptile()
        {
            TastMast mast;
            List<ElementObject> elemlist;
            if (index < this.dataGridView1.Rows.Count)
            {
                DataGridViewRow row = this.dataGridView1.Rows[index];
                //foreach (DataGridViewRow row in this.dataGridView1.Rows)
                //{
                mast = new TastMast();
                mast.ID = row.Cells["ID"].Value.ToString();
                mast.Url = row.Cells["url"].Value.ToString();
                mast.TastNm = row.Cells["TastNm"].Value.ToString();
                mast.LoginUrl = row.Cells["LoginUrl"].Value.ToString();
                mast.UserID = row.Cells["UserID"].Value.ToString();
                mast.UserInputId = row.Cells["UserInputId"].Value.ToString();
                mast.Pwd = row.Cells["Pwd"].Value.ToString();
                mast.PwdInputId = row.Cells["PwdInputId"].Value.ToString();
                mast.LoginBtnId = row.Cells["LoginBtnId"].Value.ToString();
                mast.isAMZPRO = ConvertoBool(row.Cells["isAMZPRO"].Value.ToString());
                mast.SearchKey = row.Cells["SearchKey"].Value.ToString();
                mast.SearchInputID = row.Cells["SearchInputID"].Value.ToString();
                mast.SearchBtnID = row.Cells["SearchBtnID"].Value.ToString();
                mast.AMZxpath = row.Cells["AMZxpath"].Value.ToString();

                elemlist = JsonConvert.DeserializeObject<List<ElementObject>>(row.Cells["infoStr"].Value.ToString());
                StarReptile(mast, elemlist);
                //}
            }
        }

        private void SetCtrlValues(object obj)
        {
            // InvokeRequired需要比较调用线程ID和创建线程ID
            // 如果它们不相同则返回true
            if (this.richTextBox1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetCtrlValues);
                this.Invoke(d, new object[] { obj });
            }
            else
            {
                //this.richTextBox1.AppendText(obj.ToString ());
                this.richTextBox1.AppendText(string.Format("\n\n{0}", obj.ToString()));
                /*this.listBox1.Items.Add(obj)*/;
            }
        }

        private void GetReptileInfo(string id)
        {
            DBHelp dbhelp = new DBHelp();
            DataTable dt = dbhelp.GetDataTable("select CommId, FieldID,FieldName ,FieldValue ,ReptileDT  From ReptileInfo  " +
                " where ID='" + id + "' order by CommId ");
            #region 自定义表结构
            DataTable data = new DataTable();
            DataColumn col;
            DataTable dt2 = dbhelp.GetDataTable("select InfoStr From TastDetail where ID='" + id + "'");

            foreach (ElementObject item in JsonConvert.DeserializeObject<List<ElementObject>>(dt2.Rows[0]["InfoStr"].ToString()))
            {
                col = new DataColumn(item.FieldNm);
                data.Columns.Add(col);
            }
            col = new DataColumn("抓取时间");
            data.Columns.Add(col);
            #endregion

            #region 填充数据
            string oldcommid = string.Empty;
            DataRow dtrow = null;
            foreach (DataRow row in dt.Rows)
            {
                if (string.Compare(row["CommId"].ToString(), oldcommid) == 0)
                {
                    if (dtrow != null)
                        try
                        {
                            dtrow[row["FieldName"].ToString()] = row["FieldValue"];
                            dtrow["抓取时间"] = row["ReptileDT"];
                        }
                        catch (Exception ex3) { }
                }
                else
                {
                    dtrow = data.NewRow();
                    try
                    {
                        dtrow[row["FieldName"].ToString()] = row["FieldValue"];
                        dtrow["抓取时间"] = row["ReptileDT"];
                        data.Rows.Add(dtrow);
                        oldcommid = row["CommId"].ToString();
                    }
                    catch (Exception ex4) { }
                }
            }
            #endregion

            this.dataGridView2.DataSource = data;
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //if (e != null && this.dataGridView1.Rows[e.RowIndex] != null)
            //    GetReptileInfo(this.dataGridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString());
        }
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            Config cf = new Config(this.timer1);
            DialogResult dialog = cf.ShowDialog(this);

        }

        public static string GetNums(string str)
        {
            var reg = new Regex(@"\d*\.\d*|0\.\d*[1-9]\d*$");
            //var reg = new Regex(@"\d*\.\d*|0\.\d*[1-9]\d*$");
            var ms = reg.Matches(str);
            string[] nums = new string[ms.Count];
            for (int i = 0, len = nums.Length; i < len; i++)
            {
                nums[i] = ms[i].Value;
            }
            if (nums.Length == 0)
            {
                //var reg3 = new Regex(@"\d*\,\d*|0\,\d*[1-9]\d*$");
                //var ms3 = reg3.Matches(str);
                //nums = new string[ms3.Count];
                //for (int i = 0, len = nums.Length; i < len; i++)
                //{
                //    nums[i] = ms3[i].Value;
                //}
                //if (nums.Length == 0)
                //{
                str = str.Replace(",", "");
                var reg2 = new Regex(@"[0-9]+");
                var ms2 = reg2.Matches(str);
                nums = new string[ms2.Count];
                for (int i = 0, len = nums.Length; i < len; i++)
                {
                    nums[i] = ms2[i].Value;
                }
                //}
            }
            return nums.Length == 0 ? string.Empty : nums[0];
        }
        /// <summary>
        /// 停止爬虫
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            this.timer1.Stop();
            index = 0;
            this.toolStripButton5.Enabled = false;
            this.toolStripButton3.Enabled = true;
        }

        /// <summary>
        /// 发邮件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            PostEmail.PostEmailUtil postemail = new PostEmail.PostEmailUtil();
            DataTable dt = postemail.GetData();
        }

        private bool ConvertoBool(string val)
        {
            return string.Compare("True", val) == 0 ? true : false;
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            FilesUtiles.OpenCSV(@"D:\work\爬虫\new\爬虫服务器最新\爬虫\222.csv");
        }
        //private void dataGridView2_RowEnter(object sender, DataGridViewCellEventArgs e)
        //{
        //   GetReptileInfo(this.dataGridView2.Rows[e.RowIndex].Cells["ID"].Value.ToString ());
        //}
    }
}
