using System;
using System.Collections.Generic;
using System.Text;

namespace PieceTracker.Model
{
    public class ResetPasswordRequest
    {
        public string ResetToken { get; set; }
        public string Password { get; set; }
    }
    public class ResetPasswordLinkRequest
    {
        public int UserId { get; set; }
    }
}
