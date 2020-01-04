using System;
using System.Collections.Generic;
using System.Text;

namespace SAAS.FrameWork
{
    /// <summary>
    /// 分页参数
    /// </summary>
    public class PageParameter
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; } = 1;
        /// <summary>
        /// 每页显示的条数
        /// </summary>
        public int PageSize { get; set; } = 9999;
        /// <summary>
        /// 偏移量
        /// </summary>
        public int Offset { get { return PageSize * (PageIndex - 1); } }
    }
}
