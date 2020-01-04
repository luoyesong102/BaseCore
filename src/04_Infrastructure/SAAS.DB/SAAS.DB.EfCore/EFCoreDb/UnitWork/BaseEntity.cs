using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SAAS.Framework.Orm.EfCore.UnitWork
{
    public  class BaseEntity<TKey>
    {
        [Key]
        public  TKey Id { get; set; }
        public int IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid CreatedByUserGuid { get; set; }
        public string CreatedByUserName { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public Guid? ModifiedByUserGuid { get; set; }
        public string ModifiedByUserName { get; set; }

        [Timestamp]
        [ConcurrencyCheck]
        public byte[] RowVersion { get; set; }
    }
    public class BaseEntity
    {
       
        public int IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid CreatedByUserGuid { get; set; }
        public string CreatedByUserName { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public Guid? ModifiedByUserGuid { get; set; }
        public string ModifiedByUserName { get; set; }

        [Timestamp]
        [ConcurrencyCheck]
        public byte[] RowVersion { get; set; }
    }
}
