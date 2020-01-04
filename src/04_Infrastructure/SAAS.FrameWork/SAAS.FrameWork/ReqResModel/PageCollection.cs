using System;
using System.Collections.Generic;
using System.Text;

namespace SAAS.FrameWork
{
    public class PageCollection<T>
    {
        /// <summary>
        /// 当前第几页
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 总记录条数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 每页条数
        /// </summary>
        public int PageSize { get; set; } = 20;
        /// <summary>
        /// 页数
        /// </summary>
        public int PageCount
        {
            get
            {
                if (TotalCount % PageSize == 0)
                {
                    return TotalCount / PageSize;
                }
                else
                {
                    return TotalCount / PageSize + 1;
                }
            }
        }
        /// <summary>
        /// 分页查询的数据
        /// </summary>
        public IList<T> Collection { get; set; }
    }
}
