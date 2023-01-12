using System;
using System.Collections.Generic;
using System.Text;

namespace PieceTracker.Model.Request {
    public class GetProjectDeliveriesByDateAndStatusRequest {
        public int? ProjectId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Status { get; set; }

        public GetProjectDeliveriesByDateAndStatusRequest() { }
    }
}
