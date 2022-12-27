using System;
using System.Collections.Generic;
using System.Text;

namespace PieceTracker.Model
{
    public class AddUpdateProjectRequest : RequestBaseModel
    {
        public int ItemMasterId { get; set; }
        public string ProjectName { get; set; }
        public decimal ContractAmount { get; set; }
        public string CustomerName { get; set; }
        public decimal EstimatedHours { get; set; }
        public decimal EstimatedCost { get; set; }
        public string SiteAddress { get; set; }
        public string PartNumber { get; set; }
        public string Description { get; set; }
        public int DeliveryId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Weight { get; set; }
        public string ProjectLocation { get; set; }
        public string BatchNumberProd { get; set; }
        public int ItemType { get; set; }
        public int SubItemType { get; set; }
        public decimal EstimateLabourHours { get; set; }
        public string ReferSheetNumber { get; set; }
        public int CoatingType { get; set; }
        public DateTime ScheduleFabricateDate { get; set; }
        public DateTime ScheduleCoatingDate { get; set; }
        public DateTime ActualFabricateDate { get; set; }
        public DateTime ActualCoatingDate { get; set; }
        public string FabricatorName { get; set; }
        public string CoatingVendorName { get; set; }
        public int ItemStatus { get; set; }
        public string ItemQRCode { get; set; }
    }
}
