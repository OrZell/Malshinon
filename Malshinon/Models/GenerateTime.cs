using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malshinon.Models
{
    public class GenerateTime
    {
        public static string GenerateTimeWindow(DateTime pastTime, DateTime currentTime)
        {
            return $"Between {pastTime} To {currentTime}";
        }

        public static DateTime SubtractDateTime(DateTime Time, int Minutes)
        {
            DateTime result = Time.Subtract(new TimeSpan(0, Minutes, 0));
            return result;
        }
    }
}
