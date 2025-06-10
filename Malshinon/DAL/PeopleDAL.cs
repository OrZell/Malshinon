using System.Data.SqlClient;
using Malshinon.Models;
using Malshinon.MySQL;
using MySql.Data.MySqlClient;

namespace Malshinon.DAL
{
    public class PeopleDAL
    {
        private MySQLServer? SQL;

        public PeopleDAL(MySQLServer sql)
        {
            this.SQL = sql;
        }

        public bool SearchByCodeName(string codeName)
        {
            bool sign = false;
            MySqlDataReader reader = null!;

            try
            {
                this.SQL.OpenConnection();
                MySqlConnection con = this.SQL.connection!;
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM people WHERE secret_code = @codeName", con);
                cmd.Parameters.AddWithValue(@"codeName", codeName);

                reader =  cmd.ExecuteReader();
                if (reader.Read())
                {
                    sign = true;
                }

            }
            catch (MySqlException ex)
            {
                throw new Exception (ex.Message);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
                this.SQL.CloseConnection();
            }
            return sign;
        }

        public void AddPerson(Person person)
        {
            try
            {
                this.SQL.OpenConnection();
                MySqlConnection con = this.SQL.connection!;
                MySqlCommand cmd = new MySqlCommand("INSERT INTO people (" +
                    "first_name, " +
                    "last_name, " +
                    "secret_code, " +
                    "type, " +
                    "num_reports, " +
                    "num_mentions) " +
                    "VALUES (" +
                    "@FirstName, " +
                    "@LastName, " +
                    "@SecretCode, " +
                    "@Type, " +
                    "@NumReports, " +
                    "@NumMentions)",con);
                cmd.Parameters.AddWithValue(@"FirstName", person.FirstName);
                cmd.Parameters.AddWithValue(@"LastName", person.LastName);
                cmd.Parameters.AddWithValue(@"SecretCode", person.SecretCode);
                cmd.Parameters.AddWithValue(@"Type", person.Type);
                cmd.Parameters.AddWithValue(@"NumReports", person.NumReports);
                cmd.Parameters.AddWithValue(@"NumMentions", person.NumMentions);
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                this.SQL.CloseConnection();
            }
        }

        public void IncreaseMentionsBySecretCode(string secretCode)
        {
            if (!SearchByCodeName(secretCode))
            {
                Console.WriteLine();
            }

            try
            {
                this.SQL.OpenConnection();
                MySqlConnection con = this.SQL.connection!;
                MySqlCommand cmd = new MySqlCommand("UPDATE people " +
                                                    "SET num_mentions = num_mentions + 1 " +
                                                    "WHERE secret_code = @secretCode");
                cmd.Parameters.AddWithValue(@"secretCode", secretCode);
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                this.SQL.CloseConnection();
            }
        }
    }
}
