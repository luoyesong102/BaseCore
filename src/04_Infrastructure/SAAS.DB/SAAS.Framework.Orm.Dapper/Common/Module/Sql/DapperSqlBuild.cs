using Dapper;

using SAAS.FrameWork.Module.Sql.Mysql;
using SAAS.FrameWork.Extensions;
using Newtonsoft.Json.Linq;

namespace SAAS.DB.Dapper
{
    public class DapperSqlBuild: SqlBuild
    {
        public readonly DynamicParameters sqlParam = new DynamicParameters();

        public DapperSqlBuild()
        {
            addSqlParam = (paramName, paramValue) =>
            {
                if (paramValue is JArray)
                {
                    paramValue = paramValue.ConvertBySerialize<object[]>();
                }
                sqlParam.Add(paramName, paramValue);
            };

        }


    }
}
