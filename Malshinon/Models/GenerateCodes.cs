using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malshinon.Models
{
    public class GenerateCodes
    {
        public static string GenerateSecretCode(string firstName, string lastName)
        {
            Random Rand = new Random();
            return $"{firstName.ToUpper()}{DateTime.Now.ToString().ToUpper()}{lastName.ToUpper()}{Rand.Next(101).ToString()}".Replace(" ","").Replace(":","").Replace("/","");
        }
    }
}
