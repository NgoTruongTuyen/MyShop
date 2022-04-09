using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Model
{
    public class Order_Product
    {
        public int OrderProductId { get; set; }
        public string OrderId   { get; set; }
        public string ProductId { get; set; }

        public decimal Price { get; set; }
        public int Amount { get; set; }

        public Order_Product(int orderProductId, string orderId, string productId, decimal price, int amount)
        {
            OrderProductId = orderProductId;
            OrderId = orderId;
            ProductId = productId;
            Price = price;
            Amount = amount;
        }
    }
}
