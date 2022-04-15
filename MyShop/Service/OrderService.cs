using MyShop.DAO;
using MyShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Service
{
    public class OrderService
    {
        public List<Order> GetAll()
        {
            List<Order> list = new List<Order>();

            OrderDAO orderDAO = new OrderDAO();

            list=orderDAO.GetAll();

            return list;
        }
    }
}
