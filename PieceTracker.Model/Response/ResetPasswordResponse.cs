using System;
using System.Collections.Generic;
using System.Text;

namespace PieceTracker.Model
{
    public class ResetPasswordResponse
    {
        public int UserId { get; set; }
        public string ResetPasswordLink { get; set; }
    }
    public class ResetPasswordLinkResponse
    {
        public int UserId { get; set; }
        public string ResetLink { get; set; }
    }
}
