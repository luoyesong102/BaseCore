using System;
using System.Collections.Generic;

namespace DemoEF.Domain.Models
{
    public partial class TbFinanceBill
    {
        public long Id { get; set; }
        public string CustomerOrderNo { get; set; }
        public string OrderFlowId { get; set; }
        public string DecoratorFlowId { get; set; }
        public string AccountId { get; set; }
        public long? DecoratorUser { get; set; }
        public string DecoratorUsername { get; set; }
        public int? PayType { get; set; }
        public int? TransType { get; set; }
        public string TransRemark { get; set; }
        public string TradeNo { get; set; }
        public DateTime? TradeTime { get; set; }
        public DateTime? AddTime { get; set; }
        public long? FromEntityId { get; set; }
        public int? FromEntityType { get; set; }
        public decimal? FromTradeMoney { get; set; }
        public string FromTradeRemarks { get; set; }
        public decimal? FromBalance { get; set; }
        public long? ToEntityId { get; set; }
        public int? ToEntityType { get; set; }
        public decimal? ToTradeMoney { get; set; }
        public string ToTradeRemarks { get; set; }
        public decimal? ToBalance { get; set; }
        public string UserRemarks { get; set; }
        public string ExtData { get; set; }
        public string Fk1Str { get; set; }
        public string Fk2Str { get; set; }
        public string Fk3Str { get; set; }
        public string Fk4Str { get; set; }
    }
}
