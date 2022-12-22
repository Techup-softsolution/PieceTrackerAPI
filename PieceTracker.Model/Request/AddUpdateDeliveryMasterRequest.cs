using PieceTracker.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PieceTracker.Model.Request
{
    public class AddUpdateDeliveryMasterRequest : RequestBaseModel
    {
        public int ProjectId { get; set; }
        public int Vehicle { get; set; }
        public string Instructions { get; set; }
        public bool IsProjectAddress { get; set; }
        public string DeliveryAddress { get; set; }
        public int Status { get; set; }
        public decimal TotalWeight { get; set; }
    }
}