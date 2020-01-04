using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CacheSqlXmlService.SqlManager
{
    public class ConditionBulider
    {
        public static ICondition Acquire()
        {
            return new Condition();
        }
    }
}
