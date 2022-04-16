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
        public float ScreenSize { get; set; }
        public String OS { get; set; }
        public String Color { get; set; }
        public int Memory { get; set; }
        public int Storage { get; set; }
        public int Battery { get; set; }

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


    }
}
