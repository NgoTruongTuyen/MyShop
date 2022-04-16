using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Model
{
    public class Order_Product: INotifyPropertyChanged
    {
        public int OrderId   { get; set; }
        public int ProductId { get; set; }

        public decimal Price { get; set; }
        public int Amount { get; set; }

        public Order_Product(int orderProductId, int orderId, int productId, decimal price, int amount)
        {
            OrderId = orderId;
            ProductId = productId;
            Price = price;
            Amount = amount;
        }

        public Order_Product()
        {
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
