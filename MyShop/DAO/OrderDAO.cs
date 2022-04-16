﻿using MyShop.Model;
using MyShop.DatabaseConnection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
