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
    public class ReportDAL
    {
        private MySQLServer? SQL;

        public ReportDAL(MySQLServer sql)
        {
            this.SQL = sql;
        }

        public void AddReport(Report report)
        {
            try
            {
                this.SQL.OpenConnection();
                MySqlConnection con = this.SQL.connection;
                MySqlCommand cmd = new MySqlCommand("INSERT INTO intelreports (reporter_id, target_id, text) " +
                                                    "VALUES (@reporterId, @targetId, @Text)", con);
                cmd.Parameters.AddWithValue(@"reporterId", report.ReporterId);
                cmd.Parameters.AddWithValue(@"targetId", report.MentionId);
                cmd.Parameters.AddWithValue(@"Text", report.Text);
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
