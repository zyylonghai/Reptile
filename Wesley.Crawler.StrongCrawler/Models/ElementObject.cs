using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wesley.Crawler.StrongCrawler.Models
{
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
