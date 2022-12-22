using System;
using System.Collections.Generic;
using System.Text;

namespace PieceTracker.Model
{
    public class GeneralModel
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public long RecordId { get; set; }
        public string Message { get; set; }
        public string Link { get; set; }
    }
}
