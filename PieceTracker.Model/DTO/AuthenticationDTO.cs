using System;
using System.Collections.Generic;
using System.Text;

namespace PieceTracker.Model
{
    public class AuthenticationDTO : ResponseBaseModel
    {
        public string UserEmail { get; set; }
        public string UserMobile { get; set; }
        public bool IsOTPVerified { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleAlias { get; set; }
    }
}
