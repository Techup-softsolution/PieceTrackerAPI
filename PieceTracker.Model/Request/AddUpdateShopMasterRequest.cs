using System;
using System.Collections.Generic;
using System.Text;

namespace PieceTracker.Model.Request
{
    public class AddUpdateShopMasterRequest : RequestBaseModel
    {
        public string ShopName { get; set; }
        public string PartNumber { get; set; }
        public int ProjectId { get; set; }
        public string ProjectLocation { get; set; }
        public decimal Quantity { get; set; }
        public decimal Weight { get; set; }
        public string SubItem { get; set; }
        public int StatusId { get; set; }
    }
}
