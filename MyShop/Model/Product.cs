using MyShop.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Model
{
    public class Product : BaseModel
    {
        public int ProductId { get; set; }
        public String ProductName { get; set; }
        public String ImageURL { get; set; }
        public int Stock { get; set; }
        public int CostPrice { get; set; }
        public int SellingPrice { get; set; }

        public String Brand { get; set; }
        
        public int BrandId { get; set; }
        public float ScreenSize { get; set; }

        public String OS { get; set; }
        public String Color { get; set; }
        public int Memory { get; set; }
        public int Storage { get; set; }
        public int Battery { get; set; }
        public int ViewCount { get; set; }
        public int BuyCount { get; set; }

        public int ViewCounts { get; set; }
        public int BuyCounts { get; set; }

        public int TotalPrice { 
            get
            {
                return CostPrice * OrderProducts[0].Amount;
            }
            set
            {
                TotalPrice = value;

                OnPropertyChanged(nameof(TotalPrice));            }
        }

        public BindingList<Order_Product> OrderProducts { get; set; }

        public DateTime ReleaseDate { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public Product()
        {

        }


        public Product(int productId, string productName, string imageURL, int stock, int costPrice, int sellingPrice, string brand, float screenSize, string oS, string color, int memory, int storage, int battery, int viewCount, int buyCount, BindingList<Order_Product> orderProducts, DateTime releaseDate)
        {
           ProductId = productId;
            ProductName = productName;
            ImageURL = imageURL;
            Stock = stock;
            CostPrice = costPrice;
            SellingPrice = sellingPrice;
            Brand = brand;
            ScreenSize = screenSize;
            OS = oS;
            Color = color;
            Memory = memory;
            Storage = storage;
            Battery = battery;
            ViewCounts = viewCount;
            BuyCounts = buyCount;
            OrderProducts = orderProducts;
            ReleaseDate = releaseDate;
        }

        public Product(int productId, string productName, string imageURL, int stock, int costPrice, int sellingPrice, int brand, float screenSize, string oS, string color, int memory, int storage, int battery, int viewCount, int buyCount, BindingList<Order_Product> orderProducts, DateTime releaseDate)

        {
            ProductId = productId;
            ProductName = productName;
            ImageURL = imageURL;
            Stock = stock;
            CostPrice = costPrice;
            SellingPrice = sellingPrice;
            BrandId = brand;
            ScreenSize = screenSize;
            OS = oS;
            Color = color;
            Memory = memory;
            Storage = storage;
            Battery = battery;
            ViewCounts = viewCount;
            BuyCounts = buyCount;
            OrderProducts = orderProducts;
            ReleaseDate = releaseDate;
        }

        public Product(int id,
                       string name,
                       string imgURL,
                       int cost,
                       int selling,
                       double screenSize,
                       string os,
                       string color,
                       int memory,
                       int storage,
                       int battery,
                       DateTime releaseDay,
                       int stock, 
                       string brand,
                       int view,
                       int buy)
        {
            ProductId = id;
            ProductName = name;
            ImageURL = imgURL;
            CostPrice = cost;
            SellingPrice = selling;
            ScreenSize = screenSize;
            OS = os;
            Color = color;
            Memory = memory;
            Storage = storage;
            Battery = battery;
            ReleaseDate = releaseDay;
            Stock = stock;
            Brand = brand;
            ViewCount = view;
            BuyCount = buy;
        }

        public Product(Product p)
        {
            ProductId = p.ProductId;
            ProductName = p.ProductName;
            ImageURL = p.ImageURL;
            Stock = p.Stock;
            CostPrice = p.CostPrice;
            SellingPrice = p.CostPrice;
            Brand = p.Brand;
            ScreenSize = p.ScreenSize;
            OS = p.OS;
            Color = p.Color;
            Memory = p.Memory;
            Storage = p.Storage;
            Battery = p.Battery;
            ViewCounts = p.ViewCounts;
            BuyCounts = p.BuyCounts;
            OrderProducts = new BindingList<Order_Product>();
            foreach (Order_Product op in p.OrderProducts)
                OrderProducts.Add(op);
            ReleaseDate = p.ReleaseDate;
        }
    }
}
