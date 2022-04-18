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
    }
}
