using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskService.Models
{
    public class TaskLog
    {
        public string BeginDate { get; set; }
        public string EndDate { get; set; }
        public string Msg { get; set; }
    }
}
