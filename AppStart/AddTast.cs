using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wesley.Crawler.StrongCrawler.Models;

namespace AppStart
{
    public partial class AddTast : Form
    {
        private WindowStatus  _status = WindowStatus.Add ;//窗体任务状态，1：新增 2：编辑
        private string _id = string.Empty;
        public AddTast()
        {
            InitializeComponent();
        }

        public AddTast(string id)
            : this()
        {
            this._status = WindowStatus.Edit;
            this._id = id;
            GetData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TastMast mast = new TastMast();
            mast.ID = string.IsNullOrEmpty(this.txtID.Text) ? System.DateTime.Now.ToString("yyyymmddHHmmss") : this.txtID.Text;
            mast.Url = this.txturl.Text;
            mast.TastNm = this.txtTastNm.Text;
            mast.LoginUrl = this.txtloginurl.Text.Trim();
            mast.UserID = this.txtuserId.Text.Trim();
            mast.UserInputId = this.txtuseridinput.Text.Trim();
            mast.Pwd = this.txtpwd.Text.Trim();
            mast.PwdInputId = this.txtpwdinput.Text.Trim();
            mast.LoginBtnId = this.txtloginbtn.Text.Trim();
            mast.isAMZPRO = this.checkBox1.Checked;
            mast.SearchKey = this.txbsearchkey.Text.Trim();
            mast.SearchInputID = this.txtInputID.Text.Trim();
            mast.SearchBtnID = this.txtSearchBtnID.Text.Trim();
            mast.AMZxpath = this.txtamzxpath.Text.Trim();


            TastDetail detail = new TastDetail();
            detail.RowNo = 1;
            detail.ID = mast.ID;
            List<ElementObject> elemlist = new List<ElementObject>();
            for (int i = 0; i < this.dataGridView1.Rows.Count - 1; i++)
            {
                DataGridViewRow row = this.dataGridView1.Rows[i];
                ElementObject elem = new ElementObject();
                elem.ElemID = row.Cells["ElemID"].Value == null ? string.Empty : row.Cells["ElemID"].Value.ToString();
                elem.ElemNm = row.Cells["ElemNm"].Value == null ? string.Empty : row.Cells["ElemNm"].Value.ToString();
                elem.ElemClass = row.Cells["ElemClass"].Value == null ? string.Empty : row.Cells["ElemClass"].Value.ToString();
                elem.ElemTagNm = row.Cells["ElemTagNm"].Value == null ? string.Empty : row.Cells["ElemTagNm"].Value.ToString();
                elem .Xpath = row.Cells["XPath"].Value ==null ?string.Empty : row.Cells["XPath"].Value.ToString();
                elem.FieldNm = row.Cells["FieldNm"].Value == null ? string.Empty : row.Cells["FieldNm"].Value.ToString();
                elem.IsGetnum = (bool)(row.Cells["IsGetnum"].Value==null ?false : row.Cells["IsGetnum"].Value);
                elem.PostEmail = (bool)(row.Cells["PostEmail"].Value ==null ?false : row.Cells["PostEmail"].Value);
                elemlist.Add(elem);
            }
            detail.InfoStr = JsonConvert.SerializeObject(elemlist);
            if (_status == WindowStatus.Add)
            {
                DBHelp dbhelp = new DBHelp();
                dbhelp.BeginTrans();
                dbhelp.InsertData(mast);
                dbhelp.InsertData(detail);
                dbhelp.Commit();
            }
            if (_status == WindowStatus.Edit)
            {
                DBHelp dbhelp = new DBHelp();
                dbhelp.BeginTrans();
                dbhelp.Update(mast,new Dictionary<string, object> { { "ID",mast.ID} });
                dbhelp.Update(detail, new Dictionary<string, object> { { "ID", mast.ID } });
                dbhelp.Commit();
            }
            this.Close();
        }

        private void GetData()
        {
            string sql = "select *From TastMast a,TastDetail b where a.ID=b.ID and a.ID='" + _id + "'";
            DBHelp help = new DBHelp();
            DataTable dt = help.GetDataTable(sql);
            this.txtID.Text = _id;
            this.txturl.Text = dt.Rows[0]["Url"].ToString();
            this.txtTastNm.Text = dt.Rows[0]["TastNm"].ToString();
            this.txtloginurl.Text = dt.Rows[0]["LoginUrl"].ToString();
            this.txtuserId.Text = dt.Rows[0]["UserID"].ToString();
            this.txtuseridinput.Text = dt.Rows[0]["UserInputId"].ToString();
            this.txtpwd.Text = dt.Rows[0]["Pwd"].ToString();
            this.txtpwdinput.Text = dt.Rows[0]["PwdInputId"].ToString();
            this.txtloginbtn.Text = dt.Rows[0]["LoginBtnId"].ToString();
            this.txbsearchkey.Text = dt.Rows[0]["SearchKey"].ToString();
            this.checkBox1.Checked = (bool)(dt.Rows[0]["isAMZPRO"]);
            this.txtInputID.Text = dt.Rows[0]["SearchInputID"].ToString();
            this.txtSearchBtnID.Text = dt.Rows[0]["SearchBtnID"].ToString();
            this.txtamzxpath.Text = dt.Rows[0]["AMZxpath"].ToString();


            string infostr = dt.Rows[0]["InfoStr"].ToString();
            List<ElementObject> elemlist = JsonConvert.DeserializeObject<List<ElementObject >>(infostr);
            foreach (ElementObject item in elemlist)
            {
                int newrowindex = this.dataGridView1 .Rows .Add ();
                this.dataGridView1.Rows[newrowindex].Cells["ElemID"].Value = item.ElemID;
                this.dataGridView1.Rows[newrowindex].Cells["ElemNm"].Value = item.ElemNm;
                this.dataGridView1.Rows[newrowindex].Cells["ElemClass"].Value = item.ElemClass;
                this.dataGridView1.Rows[newrowindex].Cells["ElemTagNm"].Value = item.ElemTagNm;
                this.dataGridView1.Rows[newrowindex].Cells["XPath"].Value = item.Xpath;
                this.dataGridView1.Rows[newrowindex].Cells["FieldNm"].Value = item.FieldNm;
                this.dataGridView1.Rows[newrowindex].Cells["IsGetnum"].Value = item.IsGetnum;
                this.dataGridView1.Rows[newrowindex].Cells["PostEmail"].Value = item.PostEmail;

            }
        }
    }
}
