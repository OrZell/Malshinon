using System.Data.SqlClient;
using System.Data.SqlTypes;
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

        public int ChaeckAndReturnTheType(string secretCode)
        {
            string? typeIn = null;
            int typeOut;
            MySqlDataReader reader = null;
            try
            {
                this.SQL.OpenConnection();
                MySqlConnection con = this.SQL.connection;
                MySqlCommand cmd = new MySqlCommand("SELECT type " +
                                                    "FROM people " +
                                                    "WHERE secret_code = @secretCode", con);
                cmd.Parameters.AddWithValue(@"secretCode", secretCode);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    typeIn = reader.GetString("type");
                }

            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
                this.SQL.CloseConnection();
            }
            typeOut = MySQL.SQLStaticData.Kinds[typeIn];
            return typeOut;
        }

        public void ConvertToBoth(string secretCode)
        {
            try
            {
                this.SQL.OpenConnection();
                MySqlConnection con = this.SQL.connection;
                MySqlCommand cmd = new MySqlCommand("UPDATE people " +
                                                    "SET type = @num " +
                                                    "WHERE secret_code = @secretCode", con);
                cmd.Parameters.AddWithValue(@"num", 3);
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

        public void ConvertToPotentialAgent(string secretCode)
        {
            try
            {
                this.SQL.OpenConnection();
                MySqlConnection con = this.SQL.connection;
                MySqlCommand cmd = new MySqlCommand("UPDATE people " +
                                                    "SET type = @num " +
                                                    "WHERE secret_code = @secretCode", con);
                cmd.Parameters.AddWithValue(@"num", 4);
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
                MySqlConnection con = this.SQL.connection;
                MySqlCommand cmd = new MySqlCommand("UPDATE people " +
                                                    "SET num_mentions = num_mentions + 1 " +
                                                    "WHERE secret_code = @secretCode", con);
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

        public void IncreaseReportsBySecretCode(string secretCode)
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
                                                    "SET num_reports = num_reports + 1 " +
                                                    "WHERE secret_code = @secretCode", con);
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

        public int FindIdBySecretCode(string secretCode)
        {
            MySqlDataReader reader = null!;
            int? id = null;
            try
            {
                this.SQL.OpenConnection();
                MySqlConnection con = this.SQL.connection;
                MySqlCommand cmd = new MySqlCommand("SELECT id " +
                                                    "FROM people " +
                                                    "WHERE secret_code = @secretCode", con);
                cmd.Parameters.AddWithValue(@"secretCode", secretCode);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    id = reader.GetInt16("id");
                }

            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
                this.SQL.CloseConnection();
            }
            if (id == null)
            {
                throw new Exception("No Id Detected");
            }
            return (int)id;
        }

        public int GetAllChartersBySecretCode(string secretCode)
        {
            MySqlDataReader reader = null!;
            int total = 0;
            try
            {
                this.SQL.OpenConnection();
                MySqlConnection con = this.SQL.connection;
                MySqlCommand cmd = new MySqlCommand("SELECT text " +
                                                    "FROM intelreports " +
                                                    "INNER JOIN people " +
                                                    "ON intelreports.reporter_id = people.id " +
                                                    "WHERE people.secret_code = @secretCode", con);
                cmd.Parameters.AddWithValue(@"secretCode", secretCode);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    total += reader.GetString("text").Replace(" ","").Length;
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
                this.SQL.CloseConnection();
            }
            return total;
        }

        public int GetNumOfReportsBySecretCode(string secretCode)
        {
            int? num = null;
            MySqlDataReader reader = null;
            try
            {
                this.SQL.OpenConnection();
                MySqlConnection con = this.SQL.connection;
                MySqlCommand cmd = new MySqlCommand("SELECT num_reports " +
                                                    "FROM people " +
                                                    "WHERE secret_code = @secretCode", con);
                cmd.Parameters.AddWithValue(@"secretCode", secretCode);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    num = reader.GetInt32("num_reports");
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
                this.SQL.CloseConnection();
            }
            return (int)num;
        }

        public int GetNumOfMentionsBySecretCode(string secretCode)
        {
            int? num = null;
            MySqlDataReader reader = null;
            try
            {
                this.SQL.OpenConnection();
                MySqlConnection con = this.SQL.connection;
                MySqlCommand cmd = new MySqlCommand("SELECT num_mentions " +
                                                    "FROM people " +
                                                    "WHERE secret_code = @secretCode", con);
                cmd.Parameters.AddWithValue(@"secretCode", secretCode);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    num = reader.GetInt32("num_mentions");
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
                this.SQL.CloseConnection();
            }
            return (int)num;
        }
    }
}
