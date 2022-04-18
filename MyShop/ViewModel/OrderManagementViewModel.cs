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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyShop.ViewModel
{
    
    public class OrderManagementViewModel : BaseViewModel
    {
        // Xử lý messenger
        public BaseMessenger<Order> UpdatingMessenger { get; set; }
        public BaseMessenger<Order> AddingMessenger { get; set; }
        public Order SelectedItem { get; set; }
        public BindingList<Order> Orders { get; set; }
        public BindingList<Order> FiltedOrders { get; set; }

        // Exit Add và Delete command
        public ICommand NavigateAddNewOrderCommand { get; set; }
        public ICommand NavigateEditNewOrderCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }

        //THuộc tính tìm theo ngày
        private DateTime _startDate;
        public DateTime StartDate { get { return _startDate; } set { _startDate = value; OnPropertyChanged(nameof(StartDate)); Debug.WriteLine("wirting"); } }
        private DateTime _endDate;
        public DateTime EndDate { get { return _endDate; } set { _endDate = value; OnPropertyChanged(nameof(EndDate)); Debug.WriteLine("wirting"); } }
        public RelayCommand SearchRegardingDateCommand { get; set; }
        public RelayCommand RefreshCommand { get; set; }    


        //Phân trang thông số
        public int totalItem { get; set; } = 0;
        public int itemPerPage { get; set; } = 3;
        public int totalPage { get; set; } = 0;
        public int CurrentPage { get; set; }
        public ObservableCollection<Order> CurrentOrders { get; set; }

        //Phân trang command
        public RelayCommand NextCommand { get; set; }
        public RelayCommand PreviousCommand { get; set; }
        public RelayCommand FirstCommand { get; set; }
        public RelayCommand LastCommand { get; set; }


        //DAO
        public OrderDAO orderDAO { get; set; }
        public OrderProductDAO orderProductDAO { get; set; }


        public void deleteItem(object x)
        {
            int index = FiltedOrders.IndexOf(SelectedItem);
            if (index == -1)
            {
                return;
            }

            orderDAO.deleteOne(SelectedItem.OrderId);
            orderProductDAO.deleteOrder(SelectedItem.OrderId);

            FiltedOrders.Remove(SelectedItem);
            Orders.Remove(SelectedItem);
            CurrentOrders = new ObservableCollection<Order>(FiltedOrders.Skip((CurrentPage - 1) * itemPerPage).Take(itemPerPage));
        }
        public void updateItem(Order order)
        {
            int index = FiltedOrders.IndexOf(SelectedItem);
            if (index == -1)
            {
                return;
            }

            FiltedOrders.RemoveAt(index);
            FiltedOrders.Insert(index, order);
        }

        public void addItem(Order order)
        {
            FiltedOrders.Add(order);
        }

        public void initPaging()
        {
            totalItem = FiltedOrders.Count();

            totalPage = totalItem / itemPerPage + (totalItem % itemPerPage == 0 ? 0 : 1);
            CurrentPage = 1;
            CurrentOrders = new ObservableCollection<Order>(FiltedOrders
            .Skip((CurrentPage - 1) * itemPerPage)
            .Take(itemPerPage));
        }

        public OrderManagementViewModel (NavigationStore navigationStore)
        {
            //Init selected order
            SelectedItem = new Order();
            //DAO init
            orderDAO = new OrderDAO();
            orderProductDAO = new OrderProductDAO();

            //init Messenger
            UpdatingMessenger = new BaseMessenger<Order>();
            AddingMessenger = new BaseMessenger<Order>();

            UpdatingMessenger.action += updateItem;
            AddingMessenger.action += addItem;

            //init Command
            DeleteCommand = new RelayCommand(deleteItem, null);
            NextCommand = new RelayCommand(goNext, null);
            PreviousCommand = new RelayCommand(goPrev, null);
            FirstCommand = new RelayCommand(goFirst, null);
            LastCommand = new RelayCommand(goLast, null);

            //Thuộc tính lọc theo ngày
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
            SearchRegardingDateCommand = new RelayCommand(seacrchRegardingDate, null);
            RefreshCommand = new RelayCommand(refresh, null);
           

            //Lấy dữ liệu đơn hàng từ database
            OrderService orderService = new OrderService();
            Orders = new BindingList<Order> ();
            FiltedOrders = new BindingList<Order>();
            List<Order> orders =orderService.GetAll();

            foreach (Order order in orders)
            {
                Orders.Add (order);
                FiltedOrders.Add(order);
            }

            //Khởi tạo pagination
            initPaging();

            NavigateAddNewOrderCommand =new NavigateCommand<AddNewOrderViewModel>(navigationStore, () => new AddNewOrderViewModel(navigationStore));
            NavigateEditNewOrderCommand = new NavigateCommand<AddNewOrderViewModel>(navigationStore, 
                () => new AddNewOrderViewModel(navigationStore, UpdatingMessenger, SelectedItem));
        }

        private void refresh(object obj)
        {
            FiltedOrders = new BindingList<Order>();
            foreach (Order order in Orders)
            {
                FiltedOrders.Add(order);
            }

            //Khởi tạo pagination
            initPaging();

        }

        private void seacrchRegardingDate(object obj)
        {
            string[] dateFormats = new[] { "yyyy/MM/dd", "MM/dd/yyyy","MM/dd/yyyyHH:mm:ss"};
            CultureInfo provider = new CultureInfo("en-US");
            Debug.WriteLine("-------------------------------Search----------------------------------------- start:"+StartDate+"------end:"+EndDate);

            FiltedOrders = new BindingList<Order>();
           
            foreach (Order order in Orders)
            {
               if (order.Date >= StartDate && order.Date <=EndDate)
                {
                    FiltedOrders.Add(order);
                }
            }

           initPaging ();
        }

        

        //Đi đế trang cuối
        private void goLast(object obj)
        {
            Debug.WriteLine("-------------------------------Last-----------------------------------------");
            CurrentPage = totalPage;
            CurrentOrders = new ObservableCollection<Order>(Orders.Skip((CurrentPage - 1) * itemPerPage).Take(itemPerPage));
        }
        //Đi đé trang đầu
        private void goFirst(object obj)
        {
            Debug.WriteLine("-------------------------------First-----------------------------------------");
            Debug.WriteLine(totalPage);
            Debug.WriteLine(totalItem);
            Debug.WriteLine(CurrentPage);

            CurrentPage = 1;
            CurrentOrders = new ObservableCollection<Order>(Orders.Skip((CurrentPage - 1) * itemPerPage).Take(itemPerPage));
        }
        //Đi đến trang trước
        private void goPrev(object obj)
        {
            Debug.WriteLine("-------------------------------Prev-----------------------------------------");

            int temp = CurrentPage;
            if (temp == 1 || totalPage == 0)
            {
                return;
            }
            CurrentPage = (temp - 1);
            CurrentOrders = new ObservableCollection<Order>(Orders.Skip((CurrentPage - 1) * itemPerPage).Take(itemPerPage));

            Debug.WriteLine("-------------------------------Prev-----------------------------------------");
        }
        //Đi đến trang sau
        private void goNext(object obj)
        {
            Debug.WriteLine("-------------------------------Next-----------------------------------------");

            int temp = CurrentPage;
            if (temp == totalPage || totalPage == 0)
            {
                return;
            }
            CurrentPage= (temp + 1);
            CurrentOrders = new ObservableCollection<Order>(Orders.Skip((CurrentPage - 1) * itemPerPage).Take(itemPerPage));

            Debug.WriteLine("-------------------------------Next-----------------------------------------");
        }
    }
}
