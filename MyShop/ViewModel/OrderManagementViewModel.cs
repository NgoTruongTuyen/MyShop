using Castle.Components.DictionaryAdapter;
using MyShop.Commands;
using MyShop.DAO;
using MyShop.Messenger;
using MyShop.Model;
using MyShop.Service;
using MyShop.Stores;
using MyShop.ViewModel.Command;
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
        public BaseMessenger<Order> UpdatingMessenger { get; set; }
        public BaseMessenger<Order> AddingMessenger { get; set; }
        public Order SelectedItem { get; set; }
        public BindingList<Order> Orders { get; set; }
        public ICommand NavigateAddNewOrderCommand { get; set; }
        public ICommand NavigateEditNewOrderCommand { get; set; }

        public RelayCommand DeleteCommand { get; set; }

        public OrderDAO orderDAO { get; set; }
        public OrderProductDAO orderProductDAO { get; set; }

        public void deleteItem(object x)
        {
            int index = Orders.IndexOf(SelectedItem);
            if (index == -1)
            {
                return;
            }

            orderDAO.deleteOne(SelectedItem.OrderId);
            orderProductDAO.deleteOrder(SelectedItem.OrderId);

            Orders.Remove(SelectedItem);

           
        }

        public void updateItem(Order order)
        {
            int index = Orders.IndexOf(SelectedItem);
            if (index == -1)
            {
                return;
            }

            Orders.RemoveAt(index);
            Orders.Insert(index, order);

        }

        public void addItem(Order order)
        {
            Orders.Add(order);
        }

        public OrderManagementViewModel (NavigationStore navigationStore)
        {
            orderDAO = new OrderDAO();
            orderProductDAO = new OrderProductDAO();

            SelectedItem = new Order();
            UpdatingMessenger = new BaseMessenger<Order>();
            AddingMessenger = new BaseMessenger<Order>();

            UpdatingMessenger.action += updateItem;
            AddingMessenger.action += addItem;

            DeleteCommand = new RelayCommand(deleteItem, null);

            OrderService orderService = new OrderService();
            Orders = new BindingList<Order> ();

            List<Order> orders =orderService.GetAll();

            foreach (Order order in orders)
            {
                Orders.Add (order);
            }

            NavigateAddNewOrderCommand=new NavigateCommand<AddNewOrderViewModel>(navigationStore, () => new AddNewOrderViewModel(navigationStore));
            NavigateEditNewOrderCommand = new NavigateCommand<AddNewOrderViewModel>(navigationStore, () => new AddNewOrderViewModel(navigationStore, UpdatingMessenger, SelectedItem));
        }


        public void editCommand(object x)
        {
            int index = Orders.IndexOf(SelectedItem);
            if (index == -1)
                return;

            Orders.RemoveAt(index);
        }

       
    }
}
