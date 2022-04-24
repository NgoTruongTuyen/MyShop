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



        public ObservableCollection<Product> getTopFive()
        {
            var Products = GetAll();
            var topProduct =  Products.OrderByDescending(x => x.BuyCounts).Take(5).ToList();
            return new ObservableCollection<Product>(topProduct);

        }

        public ObservableCollection<Product> getDashboardChart()
        {

            var data = GetAll();
            BrandDAO brandDAO = new BrandDAO();
            var brand = brandDAO.getAll();

            var joinBrand = data.Join(brand, d => d.BrandId, b => b.BrandId, (d, b) => new { name = b.Name });

          
            var chart  = joinBrand.GroupBy(x => x.name)
                        .Select(y => new Product { ProductName =  y.Key, BuyCounts = y.Count() }).ToList();
            Debug.WriteLine("CHARTTTTTTTTTT");
            foreach(var sth  in chart)
            {
                Debug.WriteLine(sth.ProductName+" "+sth.BuyCounts);
            }
            return new ObservableCollection<Product>(chart);


        }


         public void insertOne(Product o, int brandId)
        {
            string sql = "insert into Products (productName, imageURL, stock, costPrice, sellingPrice, brand,screenSize, os,color, memory, storage, battery, releaseDate, buyCounts, viewCounts ) " +
                "values(@productName, @imageURL, @stock, @costPrice, @sellingPrice, @brand, @screenSize, @os,@color, @memory, @storage, @battery, @releaseDate, @buyCounts," +
                "@viewCounts );";

           DBConn.excute(sql, 
               new SqlParameter("@productName", o.ProductName),
               new SqlParameter("@imageURL", o.ImageURL),
               new SqlParameter("@stock", o.Stock),
               new SqlParameter("@costPrice", o.CostPrice),
               new SqlParameter("@sellingPrice", o.SellingPrice),
               new SqlParameter("@brand", brandId),
               new SqlParameter("@screenSize", o.ScreenSize),
               new SqlParameter("@os", o.OS),
               new SqlParameter("@color", o.Color),
               new SqlParameter("@memory", o.Memory),
               new SqlParameter("@storage", o.Storage),
               new SqlParameter("@battery", o.Battery),
               new SqlParameter("@releaseDate", o.ReleaseDate),
               new SqlParameter("@buyCounts", o.BuyCounts),
               new SqlParameter("@viewCounts", o.ViewCounts)
               );




        }



    }

}
