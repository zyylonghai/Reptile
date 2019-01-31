using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostEmail
{
    public class PostEmailUtil
    {
        public PostEmailUtil()
        {

        }
        /// <summary>获取要发邮件的数据</summary>
        /// <returns></returns>
        public DataTable GetData()
        {
            string sql = "select a.ID,a.Url,a.TastNm,b.InfoStr From TastMast a,TastDetail b where a.ID=b.ID";
            DBHelp help = new DBHelp();
            DataTable dt = help.GetDataTable(sql);

            sql =string.Format("select * from ReptileInfo where ReptileDT between '{0}' and '{1}' order by ReptileDT", System.DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"));
            DataTable dt2 = help.GetDataTable(sql);

            #region
            DataTable resultdt = null;
            if (dt2 != null && dt2.Rows.Count > 0)
            {
                #region 创建表结构
                resultdt = new DataTable();
                DataColumn col = new DataColumn("emailContent");
                resultdt.Columns.Add(col);
                #endregion

                #region 填充数据
                foreach (DataRow row in dt.Rows)
                {
                    string infostr = dt.Rows[0]["InfoStr"].ToString();
                    List<ElementObject> elemlist = JsonConvert.DeserializeObject<List<ElementObject>>(row["InfoStr"].ToString());
                    foreach (ElementObject elem in elemlist)
                    {
                        if (elem.PostEmail)
                        {
                            DataRow[] rows = dt2.Select(string.Format("ID='{0}' AND FieldName='{1}'", row["ID"], elem.FieldNm));
                            if (rows.Length > 1)
                            {
                                if (string.Compare(rows[0]["FieldValue"].ToString (), rows[1]["FieldValue"].ToString ()) != 0)
                                {
                                    try
                                    {
                                        decimal price1 = Convert.ToDecimal(rows[0]["FieldValue"]);
                                        decimal price2 = Convert.ToDecimal(rows[1]["FieldValue"]);
                                        DataRow dr = resultdt.NewRow();
                                        //产品标题1+产品URL.It is now US$XX,higher/lower than US$XX yesterday.
                                        dr["emailContent"] = string.Format("{0}{1}. It is now US${2},{3} than US${4} yesterday.",
                                                                    row["TastNm"], row["Url"],price2,price2-price1 >0? "higher" : "lower", price1);
                                        resultdt.Rows.Add(dr);

                                    }
                                    catch (Exception ex)
                                    {

                                    }
                                }
                            }
                        }
                    }
                }

                #endregion
                resultdt.TableName = string.Format("{0} URLs  Price change alert", resultdt.Rows.Count);
            }

            #endregion

            return resultdt;
        }
    }

    public class ElementObject
    {
        /// <summary>
        /// 元素ID
        /// </summary>
        public string ElemID { get; set; }

        /// <summary>
        /// 元素name
        /// </summary>
        public string ElemNm { get; set; }
        /// <summary>
        /// 元素样式类
        /// </summary>
        public string ElemClass { get; set; }
        /// <summary>
        /// 元素Tag名称
        /// </summary>
        public string ElemTagNm { get; set; }

        /// <summary>
        /// Xpath
        /// </summary>
        public string Xpath { get; set; }
        /// <summary>
        /// 是否只获取数字
        /// </summary>
        public bool IsGetnum { get; set; }
        /// <summary>
        /// 自定义字段名称
        /// </summary>
        public string FieldNm { get; set; }
        /// <summary>
        /// 字段值变更是否发邮件
        /// </summary>
        public bool PostEmail { get; set; }
    }
}
