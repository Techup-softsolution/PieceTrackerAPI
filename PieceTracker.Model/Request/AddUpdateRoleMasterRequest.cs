using System;
using System.Collections.Generic;
using System.Text;

namespace PieceTracker.Model
{
    public class AddUpdateRoleMasterRequest : RequestBaseModel
    {
        public string RoleName { get; set; }
        public string RoleAlias { get; set; }
    }
}
