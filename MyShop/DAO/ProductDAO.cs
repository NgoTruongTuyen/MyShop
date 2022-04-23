using MyShop.DatabaseConnection;
using MyShop.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DAO
{
   
    internal class ProductDAO
    {
        DBConnection DBConn = DBConnection.GetInstance();

        public List<Product> GetAll(int orderId)
        {
            List<Product> products = new List<Product>();
            string sql = "select * from Products p, Orders_Products op, Orders o where p.productId=op.productID and op.orderID=o.orderID and o.orderID="+ orderId;
            SqlDataReader reader = DBConn.query(sql);

            while (reader.Read())
            {
                var productId = reader["productId"];
                var productName = reader["productName"];
                var imageURL = reader["imageURL"];
                var stock = reader["stock"];
                var costPrice = reader["costPrice"];
                var sellingPrice = reader["sellingPrice"];
                var brand = reader["brand"];
                var screenSize = reader["screenSize"];
                var os = reader["os"];
                var memory = reader["memory"];
                var color = reader["color"];
                var storage = reader["storage"];
                var battery = reader["battery"];
                var releaseDate = reader["releaseDate"];
                var buyCounts = reader["buyCounts"];
                var viewCounts = reader["viewCounts"];

                var price = reader["price"];
                var amount = reader["amount"];

                BindingList<Order_Product> order_Products = new BindingList<Order_Product>();
                order_Products.Add(new Order_Product()
                {
                    Price = (int)price,
                    Amount = (int)amount,
                    ProductId = (int)productId,
                    OrderId = orderId
                });
                
                    Product _product=new Product()
                        {
                            ProductId = (int)productId,
                            ProductName = (string)productName,
                            ImageURL = (string)imageURL,
                            Stock = (int)stock,
                            CostPrice = (int)costPrice,
                            SellingPrice = (int)sellingPrice,
                            BrandId = (int)brand,
                            ScreenSize = (float)(double)screenSize,
                            OS = (string)os,
                            Color = (string)color,
                            Memory = (int)memory,
                            Storage = (int)storage,
                            Battery = (int)battery,
                            ReleaseDate = (DateTime)releaseDate,
                            ViewCounts = (int)viewCounts,
                            BuyCounts = (int)buyCounts,
                            OrderProducts = order_Products
                    };
               

                products.Add(_product);
            }

            reader.Close();
            return products;
        }

        public bool checkStock (int id, int expectNumber)
        {
            string sql = "select stock as stock from Products where productId = @id";

            SqlDataReader reader = DBConn.queryInNewConnection(sql, new SqlParameter("@id",id));

            int stock = 0;

            while (reader.Read())
            {
                stock = (int)reader["stock"];

                Debug.WriteLine("---------------------------" + stock + "--------------------------------------------");
            }

            reader.Close();

            return stock >= expectNumber;
        }

        public List<Product> GetAll()
        {
            List<Product> products = new List<Product>();
            string sql = "select * from Products";
            SqlDataReader reader = DBConn.query(sql);

            while (reader.Read())
            {
                var productId = reader["productId"];
                var productName = reader["productName"];
                var imageURL = reader["imageURL"];
                var stock = reader["stock"];
                var costPrice = reader["costPrice"];
                var sellingPrice = reader["sellingPrice"];
                var brand = reader["brand"];
                var screenSize = reader["screenSize"];
                var os = reader["os"];
                var memory = reader["memory"];
                var color = reader["color"];
                var storage = reader["storage"];
                var battery = reader["battery"];
                var releaseDate = reader["releaseDate"];
                var buyCounts = reader["buyCounts"];
                var viewCounts = reader["viewCounts"];

               
                Product _product = new Product()
                {
                    ProductId = (int)productId,
                    ProductName = (string)productName,
                    ImageURL = (string)imageURL,
                    Stock = (int)stock,
                    CostPrice = (int)costPrice,
                    SellingPrice = (int)sellingPrice,
                    BrandId = (int)brand,
                    ScreenSize = (float)(double)screenSize,
                    OS = (string)os,
                    Color = (string)color,
                    Memory = (int)memory,
                    Storage = (int)storage,
                    Battery = (int)battery,
                    ReleaseDate = (DateTime)releaseDate,
                    ViewCounts = (int)viewCounts,
                    BuyCounts = (int)buyCounts,
                   
                };


                products.Add(_product);
            }

            reader.Close();
            return products;
        }

        public void increaseStock(int id, int diff)
        {
            String sql = "update Products set stock=stock+5 where productID=@id";

            DBConn.excute(sql,
               new SqlParameter("@id", id)
              );
        }

        public void decreaseStock(int id, int diff)
        {
            String sql = "update Products set stock=stock-5 where productID=@id";

            DBConn.excute(sql, new SqlParameter("@diff", diff),
               new SqlParameter("@id", id)
              );
        }
    }

}
