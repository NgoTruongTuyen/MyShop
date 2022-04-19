using MyShop.DatabaseConnection;
using MyShop.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                            Brand = (string)brand,
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
                    Brand = (string)brand,
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



        public ObservableCollection<Product> getTopFive()
        {
            var Products = GetAll();
            var topProduct =  Products.OrderByDescending(x => x.BuyCounts).Take(5).ToList();
            return new ObservableCollection<Product>(topProduct);

        }

        public ObservableCollection<Product> getDashboardChart()
        {

            var data = GetAll();
          
            var chart  = data.GroupBy(x => x.Brand)
                        .Select(y => new Product { Brand = y.Key, BuyCounts = y.Count() }).ToList();
            return new ObservableCollection<Product>(chart);


        }

    }
}
