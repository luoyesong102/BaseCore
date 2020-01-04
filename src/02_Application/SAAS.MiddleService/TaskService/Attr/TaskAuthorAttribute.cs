using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskService.Attr
{
    public class TaskAuthorAttribute : Attribute
    {
        public string Name { get; set; }
        public string Role { get; set; }
    }
}
