using MyShop.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyShop.ViewModel
{
    public class DashboardViewModel : BaseViewModel
    {

        public string NumberProduct { get; set; } = "100#";
        public string NumberOrder { get; set; } = "1000#";
        public string Income { get; set; } = "10000#";

        public ObservableCollection<Product> Products { get; set; }

        public DashboardViewModel()
        {
            Products = new ObservableCollection<Product>()
            {
                new Product()
                {
                    ImageURL="../Image/product/Iphone11.png",
                    ProductName = "Iphone 11",
                    SellingPrice = 17000000,
                    Stock = 10000
                },

                 new Product()
                {

                    ImageURL="../Image/product/Iphone11.png",
                    ProductName = "Iphone 11",
                    SellingPrice = 17000000,
                    Stock = 10000
                },
                  new Product()
                {

                    ImageURL="../Image/product/Iphone11.png",
                    ProductName = "Iphone 11",
                    SellingPrice = 17000000,
                    Stock = 10000
                },
                    new Product()
                {

                    ImageURL="../Image/product/Iphone11.png",
                    ProductName = "Iphone 11",
                    SellingPrice = 17000000,
                    Stock = 10000
                }



            };
        }
    }
}
