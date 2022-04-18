using MyShop.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Model
{
    public class Order_Product: BaseModel
    {
        public int OrderId   { get; set; }
        public int ProductId { get; set; }

        public int Price { get; set; }
        private int _amount;
        public int Amount 
        { get
            {
                return _amount;
            }
            set 
            {
                _amount = value;
                OnPropertyChanged(nameof(Amount));

                OrderProductDAO orderProductDAO = new OrderProductDAO();

                orderProductDAO.update(this);
            } 
        }

        public Order_Product(int orderProductId, int orderId, int productId, int price, int amount)
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
