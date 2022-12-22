using System;
using System.Collections.Generic;
using System.Text;

namespace PieceTracker.Model
{
    public class GetAllRoleMasterResponse : ResponseBaseModel
    {
        public string RoleName { get; set; }
        public string RoleAlias { get; set; }
    }
}
