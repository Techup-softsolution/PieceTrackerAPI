using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieceTracker.Common
{
    public class Log
    {
        public static void WriteLog(string LogFile, string Message)
        {
            if (!File.Exists(LogFile))
            {
                File.Create(LogFile).Dispose();
            }

            using (StreamWriter sw = new StreamWriter(LogFile, true))
            {
                Message = Message + " - " + DateTime.Now.ToLocalTime().ToString();
                sw.WriteLine(Message);
                sw.WriteLine("");
            };
        }
    }
}
