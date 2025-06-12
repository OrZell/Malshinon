using Malshinon.Models;
using Malshinon.MySQL;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malshinon.DAL
{
    public class AlertDAL
    {
        private MySQLServer SQL;
        public AlertDAL(MySQLServer sql)
        {
            this.SQL = sql;
        }

        public void AddAlert(string secretCode, string timeWindow, string reason)
        {
            try
            {
                this.SQL.OpenConnection();
                MySqlConnection con = this.SQL.connection;
                MySqlCommand cmd = new MySqlCommand("INSERT INTO alerts (target_id, time_window, reason)" +
                                                    "VALUES (@targetID, @timeWindow, @reason)", con);
                cmd.Parameters.AddWithValue(@"targetID", secretCode);
                cmd.Parameters.AddWithValue(@"timeWindow", timeWindow);
                cmd.Parameters.AddWithValue(@"reason", reason);
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

        public List<Alert> AllAlerts()
        {
            List<Alert> Alerts = new List<Alert>();
            MySqlDataReader reader = null;
            try
            {
                this.SQL.OpenConnection();
                MySqlConnection con = this.SQL.connection;
                MySqlCommand cmd = new MySqlCommand("SELECT * " +
                                                    "FROM alerts", con);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Alerts.Add(BuilderFromReader.BuilderAlert(reader));
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
            return Alerts;
    }
    }
}
