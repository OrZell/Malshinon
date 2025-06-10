using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malshinon.MySQL
{
    public class SQLStaticData
    {
        public static Dictionary<string, int> Kinds = new Dictionary<string, int> { 
            { "reporter", 1 }, 
            { "target", 2}, 
            { "both", 3},
            { "potential_agent", 4} 
        };
    }
}
