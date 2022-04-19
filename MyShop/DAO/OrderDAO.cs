using MyShop.Model;
using MyShop.DatabaseConnection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MyShop.DAO
{
    public class OrderDAO
    {
        DBConnection DBConn = DBConnection.GetInstance();
        public List<Order> GetAll()
        {
            List<Order> orders = new List<Order>();
            string sql = "select * from Orders";
            SqlDataReader reader = DBConn.query(sql);

            while (reader.Read())
            {
                var orderID = (int) reader["orderID"];
                var customerName = (string)reader["customerName"];
                var customerPhone = (string)reader["customerPhone"];
                var customerAddress = (string)reader["customerAddress"];
                var date = (DateTime)reader["createdDate"];
                var totalAmount = (int)reader["totalAmount"];
                var totalPrice = (int)reader["totalPrice"];

                orders.Add(new Order() { OrderId=orderID, CustomerName=customerName, CustomerPhone = customerPhone, CustomerAddress = customerAddress, 
                    Date =date, TotalPrice=totalPrice });
            }

            reader.Close();
            return orders;
        }

        public int insertOne(Order o)
        {
            string sql = "insert into Orders (createdDate, customerName, customerPhone, customerAddress, totalAmount, totalPrice) " +
                "values(CURRENT_TIMESTAMP, @customerName, @customerPhone,  @customerAddress, @totalAmount, @totalPrice)";

           DBConn.excute(sql, new SqlParameter("@customerName", o.CustomerName),
               new SqlParameter("@customerPhone", o.CustomerPhone),
               new SqlParameter("@customerAddress", o.CustomerAddress),
               new SqlParameter("@totalAmount", o.TotalAmount),
               new SqlParameter("@totalPrice", o.TotalPrice)
               );



            return getLatestID();

        }

        public void deleteOne(int id)
        {
            string sql = "delete from Orders where orderID = @id";

            DBConn.excute(sql, new SqlParameter("@id", id));
        }

        public int getLatestID()
        {
           string sql = "SELECT MAX(orderID) AS LastID FROM Orders";

            int id = DBConn.excuteScalar(sql);

            Debug.WriteLine("--------------------------------"+id+"----------------------------------------");

            return id;
        }

        internal void setTotalPrice(int orderId, int totalPrice)
        {
            string sql = "update Orders set totalPrice=@totalPrice where orderID=@orderID";

            DBConn.excute(sql, new SqlParameter("@totalPrice", totalPrice), new SqlParameter("@orderID", orderId));
        }

        internal void setTotalAmount(int orderId, int totalAmount)
        {
            string sql = "update Orders set totalAmount=@totalAmount where orderID=@orderID";

            DBConn.excute(sql, new SqlParameter("@totalAmount", totalAmount), new SqlParameter("@orderID", orderId));
        }

        internal void setCustomer(int orderId, string customerName, string customerPhone, string customerAddress)
        {
            string sql = "update Orders set customerName=@customerName, customerPhone=@customerPhone, customerAddress=@customerAddress where orderID=@orderID";

            DBConn.excute(sql, new SqlParameter("@customerName", customerName), 
                new SqlParameter("@customerPhone", customerPhone), 
                new SqlParameter("@customerAddress", customerAddress), 
                new SqlParameter("@orderID", orderId));
        }

        public decimal getAllPrice(int flag) // 0 day 1 month 2 year
        {
            string formatDate = "";
            List<Order> orderCurrent;
            if(flag == 0)
            {
                
                orderCurrent = GetAll().Where(p => p.Date.ToString("dd/MM/yyyy") == DateTime.Now.ToString("yy/MM/yyyy")).ToList();
            }
            else if(flag == 1)
            {
                
                orderCurrent = GetAll().Where(p => p.Date.ToString("MM/yyyy") == DateTime.Now.ToString("MM/yyyy")).ToList();
            }
            else
            {
               
                orderCurrent = GetAll().Where(p => p.Date.ToString("yyyy") == DateTime.Now.ToString("yyyy")).ToList();
            }
            return orderCurrent.Sum(p => p.TotalPrice);
        }

          public int getCount(int flag) // 0 day 1 month 2 year
        {
            string formatDate = "";
            List<Order> orderCurrent;
            if(flag == 0)
            {
                
                orderCurrent = GetAll().Where(p => p.Date.ToString("dd/MM/yyyy") == DateTime.Now.ToString("yy/MM/yyyy")).ToList();
            }
            else if(flag == 1)
            {
                
                orderCurrent = GetAll().Where(p => p.Date.ToString("MM/yyyy") == DateTime.Now.ToString("MM/yyyy")).ToList();
            }
            else
            {
               
                orderCurrent = GetAll().Where(p => p.Date.ToString("yyyy") == DateTime.Now.ToString("yyyy")).ToList();
            }
            return orderCurrent.Count();
        }

    }
}
