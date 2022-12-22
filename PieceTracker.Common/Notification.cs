using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieceTracker.Common
{
    public class NotificationMessage
    {
        public string Message { get; set; }
        public Enums.NotifyType Type { get; set; }

        public NotificationMessage(string msg, Enums.NotifyType errorType)
        {
            Message = msg;
            Type = errorType;
        }
    }
}
