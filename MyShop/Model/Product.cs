using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Model
{
    public class Product : INotifyPropertyChanged
    {
        public String ProductId { get; set; }
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

        public List<Order_Product> OrderProducts { get; set; }

        public DateTime ReleaseDate { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public Product()
        {

        }

        public Product( string name, 
                        string imgURL, 
                        int cost, 
                        float screenSize, 
                        string os, 
                        string color, 
                        int memory, 
                        int storage, 
                        int battery, 
                        DateTime releaseDay, 
                        int number, string brand)
        {
            ProductId = "";
            ProductName = name;
            ImageURL = imgURL;
            CostPrice = cost;
            ScreenSize = screenSize;
            OS = os;
            Color = color;
            Memory = memory;
            Storage = storage;
            Battery = battery;
            ReleaseDate = releaseDay;
            Stock = number;
            Brand = brand;

        }
    }
}
