using System;
using System.Collections.Generic;
using System.Text;

namespace PieceTracker.Model
{
    public class DataConfig
    {
        public int Id { get; set; }
        public string DefaultConnection { get; set; }
        public string ReportAPIURL { get; set; }
        public string FilePath { get; set; }
    }
}
