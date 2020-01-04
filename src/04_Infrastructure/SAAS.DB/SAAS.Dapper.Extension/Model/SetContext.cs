using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SAAS.Dapper.Extension.Model
{
    public class SetContext
    {
        public SetContext()
        {
            OrderbyExpressionList = new Dictionary<EOrderBy, LambdaExpression>();
        }

        public Type TableType { get;  set; }

        public LambdaExpression WhereExpression { get;  set; }

        public LambdaExpression IfNotExistsExpression { get;  set; }

        public Dictionary<EOrderBy, LambdaExpression> OrderbyExpressionList { get;  set; }

        public LambdaExpression SelectExpression { get;  set; }

        public int? TopNum { get;  set; }

        public bool NoLock { get;  set; }
    }
}
