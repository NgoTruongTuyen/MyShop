using MyShop.Commands;
using MyShop.DAO;
using MyShop.DatabaseConnection;
using MyShop.Model;
using MyShop.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyShop.ViewModel
{
    public class DetailOrderViewModel : BaseViewModel
    {
        OrderProductDAO orderProductDAO;

        //Command
        public ICommand NavigateBackCommand { get; set; }

        private int _subTotal;
        public int SubTotal
        {
            get
            {
                return _subTotal;
            }
            set
            {
                _subTotal = value;

                OnPropertyChanged(nameof(SubTotal));
            }
        }
        private int _total;
        public int Total
        {
            get
            {
                return TotalPrice;
            }
            set
            {
                _total = value;

                OnPropertyChanged(nameof(Total));
            }
        }
        private int _discount;
        public int Discount
        {
            get { return SubTotal-Total; }
            set
            {
                _discount = value;
                OnPropertyChanged(nameof(Discount));
            }
        }

        private ObservableCollection<Product> _products;
        public ObservableCollection<Product> Products
        {
            get
            {
                return _products;
            }
            set { _products = value; OnPropertyChanged(nameof(Products)); }
        }

        public ObservableCollection<Product> _productsInOrder;
        public ObservableCollection<Product> ProductsInOrder
        {
            get
            {
                return _productsInOrder;
            }
            set
            {
                _productsInOrder = value;
                OnPropertyChanged(nameof(ProductsInOrder));

            }
        }

        public Order? order { get; set; }

        public int OrderId { get { return order.OrderId; } set { order.OrderId = value; } }
        public DateTime Date { get { return order.Date; } set { order.Date = value; } }
        public int TotalPrice { get { return order.TotalPrice; } set { order.TotalPrice = value; } }
        public string CustomerName { get { return order.CustomerName; } set { order.CustomerName = value; } }
        public string CustomerPhone { get { return order.CustomerPhone; } set { order.CustomerPhone = value; } }
        public string CustomerAddress { get { return order.CustomerAddress; } set { order.CustomerAddress = value; } }

        public NavigationStore NavigationStore { get; }

        public DetailOrderViewModel(NavigationStore navigationStore, Order? sendedOrder)
        {
           

            orderProductDAO = new OrderProductDAO();

            Discount = 300000;

            order = sendedOrder;

            ProductDAO productDAO = new ProductDAO();

            List<Product> _Products = productDAO.GetAll(OrderId);

            ProductsInOrder = new ObservableCollection<Product>(_Products);

            List<Product> _Product1 = productDAO.GetAll();
            Products = new ObservableCollection<Product>(_Product1);

            SubTotal = calcSubTotal();

            NavigateBackCommand = new NavigateCommand<OrderManagementViewModel>(navigationStore, () =>
            {
                return new OrderManagementViewModel(navigationStore);
            });
        }

        public int calcSubTotal()
        {
            int subtotal = 0;
            foreach (Product product in ProductsInOrder)
            {
                subtotal += product.SellingPrice * product.OrderProducts[0].Amount;
            }
            return subtotal;
        }
    }
}
