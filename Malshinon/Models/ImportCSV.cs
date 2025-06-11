using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malshinon.Models
{
    public class ImportCSV
    {
        public static List<string[]> ProcessTheCSVToLists(string path)
        {
            List<string[]> Data = new List<string[]>();
            string[] lines = File.ReadAllLines(path);

            foreach (string line in lines)
            {
                string[] values = line.Split(",");
                Data.Add(values);
            }
            return Data;
        }

        public static bool ValidateTheData(List<string[]> Data)
        {
            bool sign = true;
            foreach (string[] line in Data)
            {
                if (line[1] == "" || line[2] == "" || line[4] == "" || line[5] == "" || line[6] == "")
                {
                    sign = false;
                    break;
                }
                //Console.WriteLine("[{0}]", string.Join(", ", line));
            }
            return sign;
        }

        public static List<string[]> CorrectTheData(List<string[]> Data)
        {
            foreach (string[] line in Data)
            {
                if (line[0] == "")
                {
                    line[0] = GenerateCodes.GenerateSecretCode(line[1], line[2]);
                }

                if (line[3] == "")
                {
                    line[3] = GenerateCodes.GenerateSecretCode(line[4], line[5]);

                }
                //Console.WriteLine("[{0}]", string.Join(", ", line));
            }
            return Data;
        }
    }
}
