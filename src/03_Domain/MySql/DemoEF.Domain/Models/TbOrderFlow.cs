using System;
using System.Collections.Generic;

namespace DemoEF.Domain.Models
{
    public partial class TbOrderFlow
    {
        public long Id { get; set; }
        public string CustomerOrderNo { get; set; }
        public string CustomerName { get; set; }
        public string CustomerTel { get; set; }
        public string CustomerAddrArea { get; set; }
        public string CustomerAddrCommunity { get; set; }
        public string CustomerAddrHourse { get; set; }
        public DateTime? CustomerMeasurementTime { get; set; }
        public string CustomerSituation { get; set; }
        public string CustomerRegistTime { get; set; }
        public string CustomerChudanyuan { get; set; }
        public string CustomerLevel { get; set; }
        public string CustomerRoomType { get; set; }
        public string CustomerRoomSize { get; set; }
        public string CustomerBudget { get; set; }
        public string CustomerOrderStatus { get; set; }
        public string CustomerPrice { get; set; }
        public string CustomerChudanbeizhu { get; set; }
        public int AskState { get; set; }
        public DateTime? TrackAssignTime { get; set; }
        public long? TrackUser { get; set; }
        public string TrackUsername { get; set; }
        public long? TypeinUser { get; set; }
        public string TypeinUsername { get; set; }
        public DateTime? TypeinTime { get; set; }
        public DateTime? LastUpdateTime { get; set; }
        public string ReceiveUsername { get; set; }
        public int? ReceiveCount { get; set; }
        public int? SignState { get; set; }
        public long? SignCompany { get; set; }
        public string SignPrice { get; set; }
        public DateTime? SignTime { get; set; }
        public DateTime? SignStartTime { get; set; }
        public string SignComment { get; set; }
        public DateTime? AskStarttime { get; set; }
        public string CustomerOrderTags { get; set; }
        public string TypeinComment { get; set; }
        public int? AssignCount { get; set; }
        public int? TypeinState { get; set; }
        public string Building { get; set; }
        public string BuildType { get; set; }
        public string DecorationType { get; set; }
        public string DecorationWay { get; set; }
        public int? DispatchCount { get; set; }
        public string ProjectCost { get; set; }
        public string DecorationStyle { get; set; }
        public int? Isurgent { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public int? CurOrderstate { get; set; }
        public int? AssignType { get; set; }
        public int? Isnew { get; set; }
        public int? ServiceMeasurementState { get; set; }
        public int? ServiceSignState { get; set; }
        public long? ServiceSignId { get; set; }
        public long? ServiceMeasurementId { get; set; }
        public int? ProjectOrderstate { get; set; }
        public DateTime? AssignTime { get; set; }
        public string CustomerOldmeasurementTime { get; set; }
        public long? TrackId { get; set; }
    }
}
