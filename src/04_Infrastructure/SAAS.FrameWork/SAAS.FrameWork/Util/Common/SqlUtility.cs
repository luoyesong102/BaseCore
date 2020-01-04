using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace SAAS.FrameWork.Util.Common
{
    public static class SqlUtility
    {
        /// <summary>
        /// 定位SQL语句关键字所在位置（主关键字，不定位子查询中的关键字）
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="keyword">关键字</param>
        /// <returns>关键字的索引位置</returns>
        public static int LocationSqlKeyWord(string sql, string keyword)
        {
            var keywordSplit = Regex.Split(sql, "\\s+" + keyword + "\\s+", RegexOptions.IgnoreCase);
            var keywords = Regex.Matches(sql, "\\s+" + keyword + "\\s+", RegexOptions.IgnoreCase);
            int partIndex = -1;
            string residueSql = "";
            for (int i = keywordSplit.Length - 1; i > 0; i--)
            {
                residueSql = CombineResidueString(keywordSplit, i, keywords);
                var leftBracketCount = residueSql.Count(ch => ch.Equals('('));
                var rightBracketCount = residueSql.Count(ch => ch.Equals(')'));
                if (leftBracketCount == rightBracketCount)
                {
                    partIndex = i;
                    break;
                }
            }
            if (partIndex == -1)
            {
                return -1;
            }
            return sql.Length - residueSql.Length - keywords[partIndex - 1].Groups[0].Value.TrimStart().Length;
        }

        /// <summary>
        /// 拼接关键字之后的SQL语句
        /// </summary>
        /// <param name="keywordSplit">根据关键字分割后的字符串集</param>
        /// <param name="partIndex">当前关键字在关键字集合中的索引</param>
        /// <param name="keywords">关键字集合</param>
        public static string CombineResidueString(string[] keywordSplit, int partIndex, MatchCollection keywords)
        {
            string sql = "";
            string @keyword;
            for (int i = keywordSplit.Length - 1; i >= partIndex; i--)
            {
                @keyword = i == keywords.Count ? "" : keywords[i].Groups[0].Value;
                sql = keywordSplit[i] + @keyword + sql;
            }
            return sql;
        }

    }
}
