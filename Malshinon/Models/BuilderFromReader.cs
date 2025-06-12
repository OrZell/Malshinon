using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Malshinon.MySQL;
using MySql.Data.MySqlClient;

namespace Malshinon.Models
{
    public class BuilderFromReader
    {
        public static Person BuilderPerson(MySqlDataReader reader)
        {
            Dictionary<string, int> Data = SQLStaticData.Kinds;

            string firstName = reader.GetString("first_name");
            string lastName = reader.GetString("last_name");
            string secretCode = reader.GetString("secret_code");
            string type = reader.GetString("type");
            int numReports = reader.GetInt32("num_reports");
            int numMentions = reader.GetInt32("num_mentions");

            int intType = Data[type];

            Person person = new Person(firstName, lastName, secretCode, intType, numReports, numMentions);
            return person;
        }

        public static Alert BuilderAlert(MySqlDataReader reader)
        {
            int targetID = reader.GetInt32("target_id");
            string timeWindow = reader.GetString("time_window");
            string reason = reader.GetString("reason");
            Alert alert = new Alert(targetID, timeWindow, reason);
            return alert;
        }
    }
}
