using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace PieceTracker.Model
{
    public class AddUpdateUserMasterRequest : RequestBaseModel
    {
        public string FullName { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public string Email { get; set; }
    }
}
