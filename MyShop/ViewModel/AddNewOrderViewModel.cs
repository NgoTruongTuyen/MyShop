using MyShop.Commands;
using MyShop.DAO;
using MyShop.Messenger;
using MyShop.Model;
using MyShop.Stores;
using MyShop.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace MyShop.ViewModel
{
    public class AddNewOrderViewModel : BaseViewModel, INotifyPropertyChanged
    {

        private int _subTotal;
        public int SubTotal
        {
            get
            {
                int subtotal = 0;
                foreach (Product product in ProductsInOrder)
                {
                    subtotal += product.SellingPrice * product.OrderProducts[0].Amount;
                }

                return subtotal;
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
                return SubTotal-Discount;
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
            get { return _discount; }
            set
            {
                _discount = value;
                OnPropertyChanged(nameof(Discount));
            }
        }

        private string _searchBox;
        public string SearchBox {
            get { return _searchBox; } 
            set { 
                _searchBox = value; 
                OnPropertyChanged(nameof(SearchBox));

                searchProduct();
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

        private Product _selectSearchProduct;
        public Product SelectSearchProduct
        {
            get { return _selectSearchProduct; }
            set
            {
                if (_selectSearchProduct != value)
                {
                    _selectSearchProduct = value; OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<Product> _productSearchList;
        public ObservableCollection<Product> ProductSearchList
        {
            get { return _productSearchList; }
            set { _productSearchList = value; OnPropertyChanged(nameof(ProductSearchList)); }
        }


        public ObservableCollection<Product> ProductsInOrder { get; set; }

        public Product SelectedProduct { get; set; }

        public RelayCommand DeleteCommand { get; set; }
 
        public ICommand NavigateCancelCommand { get; set; }
        public ICommand NavigateSubmitCommand { get; set; }
        public Order? order { get; set; }

        public int OrderId { get { return order.OrderId; } set { order.OrderId = value; } }
        public DateTime Date { get { return order.Date; } set { order.Date = value; } }
        public int TotalPrice { get { return order.TotalPrice; } set { order.TotalPrice = value; } }
        public string CustomerName { get { return order.CustomerName; } set { order.CustomerName = value; } }
        public string CustomerPhone { get { return order.CustomerPhone; } set { order.CustomerPhone = value; } }
        public string CustomerAddress { get { return order.CustomerAddress; } set { order.CustomerAddress = value; } }


        public BaseMessenger<Order> Messenger;

        public AddNewOrderViewModel(NavigationStore navigationStore)
        {
            order=new Order();
           NavigateCancelCommand = new NavigateCommand<OrderManagementViewModel>(navigationStore, () => new OrderManagementViewModel(navigationStore));
           NavigateSubmitCommand = new NavigateCommand<OrderManagementViewModel>(navigationStore, () => new OrderManagementViewModel(navigationStore));
        }

        public AddNewOrderViewModel(NavigationStore navigationStore, BaseMessenger<Order> baseMessenger)
        {
            ProductDAO productDAO = new ProductDAO();

            ProductsInOrder = new ObservableCollection<Product>();

            List<Product> _Products = productDAO.GetAll(OrderId);

            foreach (Product product in _Products)
            {
                ProductsInOrder.Add(product);
            }

            Messenger = baseMessenger;
            order = new Order();

            NavigateCancelCommand = new NavigateCommand<OrderManagementViewModel>(navigationStore, () => new OrderManagementViewModel(navigationStore));
            NavigateSubmitCommand = new NavigateCommand<OrderManagementViewModel>(navigationStore, () => {
                Messenger.excute(order);
                return new OrderManagementViewModel(navigationStore);
                });
           
        }

        public AddNewOrderViewModel(NavigationStore navigationStore, BaseMessenger<Order> baseMessenger, Order? sendedOrder)
        {
            Products = new ObservableCollection<Product>();
            Discount = 300000;

            SelectedProduct = new Product();

            DeleteCommand = new RelayCommand(deleteProduct, null);

            order = sendedOrder;

            ProductsInOrder = new ObservableCollection<Product>();

            ProductDAO productDAO = new ProductDAO();

            List<Product> _Products = productDAO.GetAll(OrderId);

            foreach (Product product in _Products)
            {
                ProductsInOrder.Add(product);
            }

            List<Product> _Product1=productDAO.GetAll();
            foreach (Product product in _Product1)
            {
                Products.Add(product);
            }

            ProductSearchList = new ObservableCollection<Product>();

            SelectSearchProduct = new Product();


            Messenger = baseMessenger;

            NavigateCancelCommand = new NavigateCommand<OrderManagementViewModel>(navigationStore, () => new OrderManagementViewModel(navigationStore));
            NavigateSubmitCommand = new NavigateCommand<OrderManagementViewModel>(navigationStore, () => {
                Messenger.excute(order);
                return new OrderManagementViewModel(navigationStore);
            });
           
        }

        private void deleteProduct(object obj)
        {
            ProductsInOrder.Remove(SelectedProduct);
        }

        private void searchProduct()
        {
            ProductSearchList = new ObservableCollection<Product>();
            foreach (var product in Products)
            {
                if (product.ProductName.ToLower().Contains(_searchBox.ToLower()))
                {

                    ProductSearchList.Add(product);
                }

            }


        }
    }
}
