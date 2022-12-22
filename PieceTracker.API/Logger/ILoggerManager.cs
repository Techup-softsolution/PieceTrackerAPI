using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PieceTracker.API.Logger
{
    public interface ILoggerManager
    {
        void Information(string message);
        void Warning(string message);
        void Debug(string message);
        void Error(string message);
    }
}
