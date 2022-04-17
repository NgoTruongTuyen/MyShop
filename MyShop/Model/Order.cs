using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Model
{
    public class Order
    {
        public string OrderId { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalPrice { get; set; }

        public string Customer { get; set; }

       public List<Order_Product> OrderProducts { get; set; }

        public Order(string orderID, DateTime date, decimal totalPrice, string customer)
        {
            Customer=customer;
            OrderId = orderID;
            Date = date;
            TotalPrice = totalPrice;
        }
    }
}
