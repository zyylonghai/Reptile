using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wesley.Crawler.StrongCrawler.Models
{
    public class TastMast
    {
        /// <summary>ID</summary>
        public string ID { get; set; }
        /// <summary>URL</summary>
        public string Url { get; set; }
        /// <summary>任务名称 </summary>
        public string TastNm { get; set; }

        /// <summary>
        /// 登录网址
        /// </summary>
        public string LoginUrl { get; set; }

        /// <summary>
        /// 帐号
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// 帐号元素ID
        /// </summary>
        public string UserInputId { get; set; }

        /// <summary>帐号密码</summary>
        public string Pwd { get; set; }

        /// <summary>帐号密码元素ID</summary>
        public string PwdInputId { get; set; }

        /// <summary>
        /// 登录按钮元素ID
        /// </summary>
        public string LoginBtnId { get; set; }
        /// <summary>
        /// 是否从插件获取
        /// </summary>
        public bool isAMZPRO { get; set; }

        public string SearchKey { get; set; }

        /// <summary>
        /// 搜索输入框ID
        /// </summary>
        public string SearchInputID { get; set; }

        /// <summary>
        /// 搜索按钮ID
        /// </summary>
        public string SearchBtnID { get; set; }

        /// <summary>
        /// 打开插件的元素的xpath
        /// </summary>
        public string AMZxpath { get; set; }
    }
}
