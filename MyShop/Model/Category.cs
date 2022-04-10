using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Model
{
    public class Category
    {
        private string _brand;
        private ObservableCollection<Product> _products;

        public string Brand
        {
            get
            {
                return _brand;  
            }
            set
            {
                _brand = value;
            }
        }

        public ObservableCollection<Product> Products
        {
            get
            {
                return _products;
            }
            set
            {
                _products = value;
            }
        }

        public Category()
        {
            _products = new ObservableCollection<Product>();
        }
    }
}
