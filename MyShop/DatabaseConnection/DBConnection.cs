using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyShop.DatabaseConnection
{
    public class DBConnection
    {
        private static DBConnection _instance;

        private SqlConnection _connection;

        public static DBConnection GetInstance()
        {
            if (_instance == null)
                _instance = new DBConnection();
            return _instance;
        }

        private DBConnection() 
        {
            var connectionString =
                string.Format(@"Server=(local);Database={0};Trusted_Connection=Yes;", "QLCH");

            // Kết nối
            _connection = new SqlConnection(connectionString);
            try
            {
                _connection.Open();
                MessageBox.Show("Connected");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        public SqlDataReader query(string sql, params object[] Params)
        {
            var command = new SqlCommand(sql, _connection);

            foreach (var p in Params)
            {
                command.Parameters.Add(p);
            }

           var reader = command.ExecuteReader();

           return reader;
        }

        public bool excute (string sql, params object[] Params)
        {
            var command = new SqlCommand(sql, _connection);

            foreach (var p in Params)
            {
                command.Parameters.Add(p);
            }

            try 
            {
                command.ExecuteNonQuery();
            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            

            return true;
        }


    }
}
