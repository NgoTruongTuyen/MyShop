using MyShop.Commands;
using MyShop.DAO;
using MyShop.DatabaseConnection;
using MyShop.Messenger;
using MyShop.Model;
using MyShop.Stores;
using MyShop.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows.Input;

namespace MyShop.ViewModel
{
    public class AddNewOrderViewModel : BaseViewModel, INotifyPropertyChanged
    {

        OrderProductDAO orderProductDAO;

        SqlTransaction sqlTransaction;

        private RelayCommand _focusCommand;
        public RelayCommand FocusCommand { get { return _focusCommand; }  set { _focusCommand = value; OnPropertyChanged(nameof(FocusCommand)); } }

        private string _showSearchList;
        public string ShowSearchList {
            get 
            { 
                return _showSearchList; 
            } 
            set 
            { 
                _showSearchList = value; 
                OnPropertyChanged(nameof(ShowSearchList)); 
            } 
        }

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
                ShowSearchList = "Visible";
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
                if ((value as Product).OrderProducts != null)
                {
                    _selectSearchProduct = value; 
                    OnPropertyChanged(nameof(SelectSearchProduct));
               
                    ProductsInOrder.Add(SelectSearchProduct);

                    orderProductDAO.insertOne(SelectSearchProduct.OrderProducts[0]);

                    SubTotal = calcSubTotal();
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
           
            ProductDAO productDAO = new ProductDAO();
            OrderDAO orderDAO = new OrderDAO();

            order = new Order() { CustomerAddress = " ", CustomerName = " ", CustomerPhone = " ", TotalAmount = 0, TotalPrice = 0 };
            order.OrderId= orderDAO.insertOne(order);

            DBConnection.GetInstance().beginTrans();


            orderProductDAO = new OrderProductDAO();

            ShowSearchList = "Hidden";

            ProductsInOrder = new ObservableCollection<Product>();

            Products = new ObservableCollection<Product>();

            List<Product> _Product1 = productDAO.GetAll();
            foreach (Product product in _Product1)
            {
                Products.Add(product);
            }

            ProductSearchList = new ObservableCollection<Product>();

            SelectSearchProduct = new Product();

            Discount = 300000;

            SelectedProduct = new Product();

            DeleteCommand = new RelayCommand(deleteProduct, null);

            NavigateCancelCommand = new NavigateCommand<OrderManagementViewModel>(navigationStore, () =>
            {
                DBConnection.GetInstance().Transaction.Rollback();

                orderDAO.deleteOne(OrderId);

                orderProductDAO.deleteOrder(OrderId);

                return new OrderManagementViewModel(navigationStore);
            } 
            );
           NavigateSubmitCommand = new NavigateCommand<OrderManagementViewModel>(navigationStore, () => {
               OrderDAO orderDAO = new OrderDAO();

               int TotalAmount = 0;

               foreach (Product product in ProductsInOrder)
               {
                   TotalAmount += product.OrderProducts[0].Amount;
               }

               orderDAO.setTotalPrice(OrderId, Total);
               orderDAO.setTotalAmount(OrderId, TotalAmount);
               orderDAO.setCustomer(OrderId, CustomerName, CustomerPhone, CustomerAddress);

               DBConnection.GetInstance().Transaction.Commit();
               return new OrderManagementViewModel(navigationStore);
               });
        }

        public AddNewOrderViewModel(NavigationStore navigationStore, BaseMessenger<Order> baseMessenger, Order? sendedOrder)
        {
            DBConnection.GetInstance().beginTrans();

            orderProductDAO=new OrderProductDAO();

            ShowSearchList = "Hidden";

            ProductsInOrder = new ObservableCollection<Product>();

            Products = new ObservableCollection<Product>();

            ProductSearchList = new ObservableCollection<Product>();

            SelectSearchProduct = new Product();

            Discount = 300000;

            SelectedProduct = new Product();

            DeleteCommand = new RelayCommand(deleteProduct, null);

            order = sendedOrder;

            FocusCommand = new RelayCommand(handleFocus, null);


            ProductDAO productDAO = new ProductDAO();

            List<Product> _Products = productDAO.GetAll(OrderId);

            foreach (Product product in _Products)
            {
                ProductsInOrder.Add(product);
            }

            List<Product> _Product1 = productDAO.GetAll();
            foreach (Product product in _Product1)
            {
                Products.Add(product);
            }

            SubTotal = calcSubTotal();




            Messenger = baseMessenger;

            NavigateCancelCommand = new NavigateCommand<OrderManagementViewModel>(navigationStore, () =>
            {
                DBConnection.GetInstance().Transaction.Rollback();
                return new OrderManagementViewModel(navigationStore);
            });
            NavigateSubmitCommand = new NavigateCommand<OrderManagementViewModel>(navigationStore, () => {
                OrderDAO orderDAO = new OrderDAO();

                int TotalAmount = 0;

                foreach (Product product in ProductsInOrder)
                {
                    TotalAmount += product.OrderProducts[0].Amount;
                }

                orderDAO.setTotalPrice(OrderId, Total);
                orderDAO.setTotalAmount(OrderId, TotalAmount);
                orderDAO.setCustomer(OrderId, CustomerName, CustomerPhone, CustomerAddress);

                DBConnection.GetInstance().Transaction.Commit();
                Messenger.excute(order);
                return new OrderManagementViewModel(navigationStore);
            });
           
        }

        private void deleteProduct(object obj)
        {
            orderProductDAO.deleteOne(SelectedProduct.OrderProducts[0]);

            ProductsInOrder.Remove(SelectedProduct);

            SubTotal = calcSubTotal();
        }

        private void searchProduct()
        {
            ProductSearchList = new ObservableCollection<Product>();
            foreach (var product in Products)
            {
                if (product.ProductName.ToLower().Contains(_searchBox.ToLower()))
                {
                    Product _product = product as Product;
                    _product.OrderProducts = new BindingList<Order_Product>();
                    _product.OrderProducts.Add(new Order_Product() { Amount = 0, ProductId = _product.ProductId, OrderId = OrderId, Price = _product.SellingPrice });
                    ProductSearchList.Add(product);
                }
            }
        }

        public void handleFocus(object x)
        {
            if (ShowSearchList == "visible")
                ShowSearchList = "hidden";
            else if (ShowSearchList == "visible")
                ShowSearchList = "visible";
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
