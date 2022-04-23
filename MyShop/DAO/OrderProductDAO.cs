using MyShop.DatabaseConnection;
using MyShop.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyShop.DAO
{
   
    public class OrderProductDAO
    {
        DBConnection DBConn = DBConnection.GetInstance();
        ProductDAO productDAO = new ProductDAO();

        public void insertOne (Order_Product op)
        {  
                String sql = "insert into Orders_Products (productID, orderID, amount, price) values (@productID, @orderID, @amount, @price)";

                DBConn.excute(sql, new SqlParameter("@productID", op.ProductId),
                    new SqlParameter("@orderID", op.OrderId),
                    new SqlParameter("@amount", op.Amount),
                    new SqlParameter("@price", op.Price));
           
        }

        internal void update(Order_Product op)
        {
            String sql = "update Orders_Products set amount=@amount, price=@price where productID=@productID and orderID=@orderID";

            DBConn.excute(sql, new SqlParameter("@productID", op.ProductId),
               new SqlParameter("@orderID", op.OrderId),
               new SqlParameter("@amount", op.Amount),
               new SqlParameter("@price", op.Price));

        }
        public void deleteOne(Order_Product op)
        {
            String sql = "delete from Orders_Products where productID=@productID and orderID=@orderID";

            DBConn.excute(sql, new SqlParameter("@productID", op.ProductId),
                new SqlParameter("@orderID", op.OrderId));
        }

        internal void deleteOrder(int orderId)
        {
            String sql = "delete from Orders_Products where orderID=@id";

            DBConn.excute(sql, new SqlParameter("@id", orderId));
        }

        public List<Order_Product> getAll(int orderID)
        {



            List<Order_Product> list = new List<Order_Product>();

            string sql = "select * from Orders_Products where orderID = @orderID";

            SqlDataReader reader = DBConn.queryInNewConnection(sql, new SqlParameter("@orderID", orderID));


            while (reader.Read())
            {
                var productID = reader["productID"];
                var price = reader["price"];
                var amount = reader["amount"];

                list.Add(new Order_Product() { Amount = (int)amount, OrderId = orderID, Price = (int)price, ProductId = (int)productID });
            }

            reader.Close();

            return list;
        }
    }
}
