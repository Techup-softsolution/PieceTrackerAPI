using System;
using System.Collections.Generic;
using System.Text;

namespace PieceTracker.Model
{
    public class AddUpdateProjectItemStatusRequest : RequestBaseModel
    {
        public string StatusName { get; set; }

    }
}
