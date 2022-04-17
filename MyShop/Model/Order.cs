using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Model
{
    public class Order: INotifyPropertyChanged
    {
        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        public int TotalPrice { get; set; }

        public int TotalAmount { get; set; }

        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }

        public List<Order_Product> OrderProducts { get; set; }

        public Order(int orderId, DateTime date, int totalPrice, int totalAmount, string customerName, string customerPhone, string customerAddress, List<Order_Product> orderProducts)
        {
            OrderId = orderId;
            Date = date;
            TotalPrice = totalPrice;
            TotalAmount = totalAmount;
            CustomerName = customerName;
            CustomerPhone = customerPhone;
            CustomerAddress = customerAddress;
            OrderProducts = orderProducts;
        }

        public Order()
        {
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
