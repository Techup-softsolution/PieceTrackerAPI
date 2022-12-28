using System;
using System.Collections.Generic;
using System.Text;

namespace PieceTracker.Model
{
    public class GetAllProjectSummaryResponse : ResponseBaseModel
    {
        public string ProjectName { get; set; }
        public decimal ContractAmount { get; set; }
        public string CustomerName { get; set; }
        //public decimal EstimatedHours { get; set; }
        //public decimal EstimatedCost { get; set; }
        public string SiteAddress { get; set; }
        public string EstimatedCostAndTime { get; set; }

    }
}
