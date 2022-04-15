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

        public string Customer { get; set; }

       public List<Order_Product> OrderProducts { get; set; }

        public Order(int orderID, DateTime date, int totalPrice, string customer)
        {
            Customer=customer;
            OrderId = orderID;
            Date = date;
            TotalPrice = totalPrice;
        }

        public Order()
        {
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
