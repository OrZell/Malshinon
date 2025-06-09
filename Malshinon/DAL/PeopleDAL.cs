using System.Data.SqlClient;
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
            bool? sign;

            try
            {
                this.SQL.OpenConnection();
                MySqlConnection con = this.SQL.connection!;
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM people WHERE secret_code = @codeName", con);
                cmd.Parameters.AddWithValue(@"codeName", codeName);

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
