using System;
using System.Collections.Generic;
using System.Text;

namespace PieceTracker.Model
{
    public class AuthenticationMasterRequest
    {
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
    }
}
