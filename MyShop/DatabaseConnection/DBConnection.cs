using System;
using System.Collections.Generic;
using System.Data;
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
        public SqlConnection Connection { get { return _connection; } }

        public SqlTransaction Transaction { get; set; }    

        private SqlConnection _transConnection;
        public SqlConnection TransConnection { get { return _transConnection; } }

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
            _transConnection = new SqlConnection(connectionString);
            try
            {
                _connection.Open();
                _transConnection.Open();
                MessageBox.Show("Connected");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void beginTrans()
        {
            Transaction=_transConnection.BeginTransaction();
        }
        public SqlDataReader queryInNewConnection(string sql, params object[] Params)
        {
            var connectionString =
               string.Format(@"Server=(local);Database={0};Trusted_Connection=Yes;", "QLCH");

            var transConnection = new SqlConnection(connectionString);

            transConnection.Open();

            var command = new SqlCommand(sql, transConnection);

            foreach (var p in Params)
            {
                command.Parameters.Add(p);
            }

            var reader = command.ExecuteReader();

            return reader;
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

        internal int excuteScalar(string sql, params object[] Params)
        {
           
            var command = new SqlCommand(sql, _transConnection);
            foreach (var p in Params)
            {
                command.Parameters.Add(p);
            }

            try
            {
                //Sample 04: Execute the Query and Get the Count of Emplyees
                object count = command.ExecuteScalar();
                Int32 Total_Records = System.Convert.ToInt32(count);


                return Total_Records;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }


           
        }

        public bool excute (string sql, params object[] Params)
        {
            var command = new SqlCommand(sql, _transConnection);

            command.Transaction = Transaction;

            foreach (var p in Params)
            {
                command.Parameters.Add(p);
            }

            try 
            {
                command.ExecuteScalar();
            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            

            return true;
        }

        


    }
}
