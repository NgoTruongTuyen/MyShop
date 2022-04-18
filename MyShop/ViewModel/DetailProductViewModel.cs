using MyShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.ViewModel
{
    public class DetailProductViewModel : BaseViewModel
    {
        public String Name { get; set; }
        public int Amount { get; set; }
        public int CostPrice { get; set; }
        public int SellingPrice { get; set; }
        public String ImageURL { get; set; }
        public String Brand { get; set; }
        public double ScreenSize { get; set; }
        public String OS { get; set; }
        public String Color { get; set; }
        public DateTime Date { get; set; }
        public int Memory { get; set; }
        public int Storage { get; set; }
        public int Battery { get; set; }
        public DetailProductViewModel(Product product)
        {
            Name = product.ProductName;
            CostPrice = product.CostPrice;
            SellingPrice = product.SellingPrice;
            ImageURL = product.ImageURL;
            Brand = product.Brand;
            ScreenSize = product.ScreenSize;
            OS = product.OS;
            Color = product.Color;
            Memory = product.Memory;
            Date = product.ReleaseDate;
            Storage = product.Storage;
            Battery = product.Battery;

        }


    }
}
