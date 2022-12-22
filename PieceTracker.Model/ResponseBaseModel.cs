using System;
using System.Collections.Generic;
using System.Text;

namespace PieceTracker.Model
{
    public class ResponseBaseModel
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
