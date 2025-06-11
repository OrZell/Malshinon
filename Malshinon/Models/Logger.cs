using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malshinon.Models
{
    public class Logger
    {
        public static string Path = @"..\..\..\Logs.txt";
        public static void CreateLog(string Log)
        {
            string LogEntry = $"[{DateTime.Now}] {Log}";
            File.AppendAllText(Path, LogEntry + Environment.NewLine);
        }
    }
}
