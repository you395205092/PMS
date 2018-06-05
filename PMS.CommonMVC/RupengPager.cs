using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.CommonMVC
{
    public class RupengPager
    {
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总数据条数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 显示出来页码的最大数
        /// </summary>
        public int MaxPagerCount { get; set; }

        /// <summary>
        /// 当前页的页码（1开始算其实页）
        /// </summary>
        public int PageIndex { get; set; }


        /// <summary>
        /// 链接的格式，约定其中页面用{pn}趱位符
        /// </summary>
        public string UrlPattern { get; set; }

        public string CurrentPageClassName { get; set; }

        public string GetPagerHtml()
        {
            StringBuilder html = new StringBuilder();
            html.Append("<ul>");
            int pageCount = (int)Math.Ceiling(TotalCount * 1.0f / PageSize);

            int startPageIndex = Math.Max(1, PageIndex - MaxPagerCount / 2);
            int endPageIndex = Math.Min(pageCount, startPageIndex + MaxPagerCount - 1);
            for (int i = startPageIndex; i <= endPageIndex; i++)
            {
                if (i==PageIndex)
                {
                    html.AppendLine("<li class='").Append(CurrentPageClassName).Append("'>").Append(i).Append("</li>");
                }
                else
                {
                    string href = UrlPattern.Replace("{pn}", i.ToString());
                    html.AppendLine("<li><a href='href'>").Append(i).Append("</a></li>");
                }
            
            }
            html.Append("</ul>");
            return html.ToString();
        }


    }

}
