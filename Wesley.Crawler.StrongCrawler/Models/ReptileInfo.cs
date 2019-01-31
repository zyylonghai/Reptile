using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wesley.Crawler.StrongCrawler.Models
{
   public class ReptileInfo
    {
        /// <summary>
        /// 任务ID
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 同一次抓取时的ID
        /// </summary>
        public string CommId { get; set; }
        /// <summary>
        /// 抓取的字段ID
        /// </summary>
        public string FieldID { get; set; }
        /// <summary>
        /// 抓取的字段值
        /// </summary>
        public string FieldValue { get; set; }
        /// <summary>
        /// 抓取的时间
        /// </summary>
        public DateTime ReptileDT { get; set; }
        /// <summary>
        /// 自定义抓取的字段名
        /// </summary>
        public string FieldName { get; set; }
    }
}
