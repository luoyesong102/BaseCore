/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:    码友网(https://codedefault.com)--专注.NET/.NET Core
 * 版权所有，请勿删除
 ******************************************/

namespace Common.Service.ViewModels.Icon
{
    /// <summary>
    /// 
    /// </summary>
    public class IconImportViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string Icons { get; set; }
    }

    public class IconBatchViewModel
    {
        public string command { get; set; }
        public string ids { get; set; }
        
    }
}
