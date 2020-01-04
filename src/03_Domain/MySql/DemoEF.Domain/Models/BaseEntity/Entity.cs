using System;
using System.ComponentModel.DataAnnotations;

namespace YH.SAAS.Domain.Entities
{
  
    public class BaseLongEntity
    {

        public long Id { get; set; }
        public sbyte Status { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
       
    }
    public class BaseGuidEntity
    {

        public Guid Id { get; set; }
        public sbyte Status { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
       
    }
    public class BaseIntEntity
    {

        public int Id { get; set; }
        public sbyte Status { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
       
    }
}
