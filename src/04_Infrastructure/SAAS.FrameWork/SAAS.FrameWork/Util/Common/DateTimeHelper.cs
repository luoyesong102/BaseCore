using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SAAS.FrameWork.Util.Common
{
    public static class DateTimeHelper
    {
        public static string TimeDiff(DateTime? DateTime1, DateTime? DateTime2)
        {
            if (DateTime1 == null || DateTime2 == null) return "";
            TimeSpan ts = DateTime2.Value - DateTime1.Value;
            return //ts.Days + "天"+
                       ts.Hours + "小时"
                    + ts.Minutes + "分钟"
                    + ts.Seconds + "秒";
        }


        ///// <summary>
        ///// 已重载.计算两个日期的时间间隔,返回的是时间间隔的日期差的绝对值.
        ///// </summary>
        ///// <param name="DateTime1">第一个日期和时间</param>
        ///// <param name="DateTime2">第二个日期和时间</param>
        ///// <returns></returns>
        //public static string DateDiff(DateTime? DateTime1, DateTime? DateTime2)
        //{
        //    string dateDiff = null;
        //    try
        //    {
        //        TimeSpan ts1 = new TimeSpan(DateTime1.Value.Ticks);
        //        TimeSpan ts2 = new TimeSpan(DateTime2.Value.Ticks);
        //        TimeSpan ts = ts1.Subtract(ts2).Duration();
        //        dateDiff = ts.Days.ToString() + "天"
        //                + ts.Hours.ToString() + "小时"
        //                + ts.Minutes.ToString() + "分钟"
        //                + ts.Seconds.ToString() + "秒";
        //    }
        //    catch
        //    {

        //    }
        //    return dateDiff;
        //}
        ///// <summary>
        ///// 已重载.计算两个日期的时间间隔,返回的是时间间隔的日期差的绝对值.
        ///// </summary>
        ///// <param name="DateTime1">第一个日期和时间</param>
        ///// <param name="DateTime2">第二个日期和时间</param>
        ///// <returns></returns>
        //public static string DateDiffHour(DateTime? DateTime1, DateTime? DateTime2)
        //{
        //    string dateDiff = null;
        //    try
        //    {
        //        TimeSpan ts1 = new TimeSpan(DateTime1.Value.Ticks);
        //        TimeSpan ts2 = new TimeSpan(DateTime1.Value.Ticks);
        //        TimeSpan ts = ts1.Subtract(ts2).Duration();
        //        dateDiff = (ts.Hours + ts.Days * 24).ToString() + ":"
        //                + ts.Minutes.ToString() + ":"
        //                + ts.Seconds.ToString() + "";
        //    }
        //    catch
        //    {

        //    }
        //    return dateDiff;
        //}

        /// <summary>
        /// 返回两个时间的分钟差
        /// </summary>
        /// <param name="DateTime1">第一个日期和时间</param>
        /// <param name="DateTime2">第二个日期和时间</param>
        /// <returns></returns>
        //public static double DateDiffTotalMin(DateTime DateTime1, DateTime DateTime2)
        //{
        //    double dateDiff = 0;
        //    try
        //    {
        //        TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
        //        TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
        //        TimeSpan ts = ts1.Subtract(ts2).Duration();
        //        dateDiff = ts.TotalMinutes;
        //    }
        //    catch
        //    {

        //    }
        //    return dateDiff;
        //}
    }
}