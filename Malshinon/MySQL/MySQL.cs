using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Malshinon.MySQL
{
    public class MySQLServer
    {
        private string SqlString = "Server=localhost;Database=malshinon;User=root;Password=''";
        public MySqlConnection? connection = null;

        public MySQLServer()
        {

        }

        public MySqlConnection OpenConnection()
        {
            if (connection == null)
            {
                connection = new MySqlConnection(SqlString);
            }

            if (connection.State != System.Data.ConnectionState.Open)
            {
                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return connection;
        }

        public void CloseConnection()
        {
            if (connection != null && connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
                connection = null;
            }
        }
    }
}
