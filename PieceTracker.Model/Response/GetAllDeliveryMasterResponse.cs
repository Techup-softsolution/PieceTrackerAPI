using System;
using System.Collections.Generic;
using System.Text;

namespace PieceTracker.Model.Response
{
    public class GetAllDeliveryMasterResponse : ResponseBaseModel
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int Vehicle { get; set; }
        public string Instructions { get; set; }
        public bool IsProjectAddress { get; set; }
        public string DeliveryAddress { get; set; }
        public int Status { get; set; }
        public decimal TotalWeight { get; set; }
        public string DeliveryName { get; set; }
        public DateTime DeliveryDate { get; set; }
    }
}
