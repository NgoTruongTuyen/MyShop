using Castle.Components.DictionaryAdapter;
using MyShop.Commands;
using MyShop.Model;
using MyShop.Service;
using MyShop.Stores;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyShop.ViewModel
{
    
    public class OrderManagementViewModel : BaseViewModel
    {
        public BindingList<Order> Orders { get; set; }

        public ICommand NavigateAddNewOrderCommand { get; set; }
       public OrderManagementViewModel (NavigationStore navigationStore)
        {
            OrderService orderService = new OrderService();
            Orders = new BindingList<Order> ();

            List<Order> orders =orderService.GetAll();

            foreach (Order order in orders)
            {
                Orders.Add (order);
            }

            NavigateAddNewOrderCommand=new NavigateCommand<AddNewOrderViewModel>(navigationStore, () => new AddNewOrderViewModel(navigationStore));
        }
    }
}
