using LiveCharts;
using LiveCharts.Wpf;
using MyShop.DAO;
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

namespace MyShop.ViewModel
{
    public class StatisticViewModel : BaseViewModel
    {
        public BindingList<Order> Orders { get; set; }
        public BindingList<Order> FiltedOrders { get; set; }

        //Thuộc tính tìm theo ngày
        private DateTime _startDate;
        public DateTime StartDate { get { return _startDate; } set { _startDate = value; OnPropertyChanged(nameof(StartDate)); Debug.WriteLine("wirting"); } }
        private DateTime _endDate;
        public DateTime EndDate { get { return _endDate; } set { _endDate = value; OnPropertyChanged(nameof(EndDate)); Debug.WriteLine("wirting"); } }
        public RelayCommand FilterDateCommand { get; set; }

        public OrderDAO orderDAO { get; set; }
        public OrderProductDAO orderProductDAO { get; set; }


        // Chart
        public SeriesCollection SeriesCollection { get; set; }
        public ObservableCollection<string> BarLabels { get; set; }
        public Func<double, string> Formatter { get; set; }
        public StatisticViewModel(NavigationStore navigationStore)
        {
            //DAO init
            orderDAO = new OrderDAO();
            orderProductDAO = new OrderProductDAO();


            //FilterDateCommand = new RelayCommand(filterWithDate, null);
            //Lấy dữ liệu đơn hàng từ database
            OrderService orderService = new OrderService();
            Orders = new BindingList<Order>();
            FiltedOrders = new BindingList<Order>();
            List<RevenueInfor> revenueList = orderService.GetRevenueList();

            HashSet<string> dateHashSet = new HashSet<string>();
            dateHashSet = getDateList(revenueList);

            List<String> dateList = new List<string>();

            int num = dateHashSet.Count;
            List<int> values = new List<int>();
            values = Enumerable.Repeat(0, num).ToList();

            // Khoi tai gia tri ban dau
            int index = 0;
            values[0] = revenueList[0].AmountOfMoney;
            HashSet<string> tempDateHashSet = new HashSet<string>();
            tempDateHashSet.Add(revenueList[0].CreateDate);
            dateList.Add(revenueList[0].CreateDate);
            for (int i = 1; i < revenueList.Count; i++)
            {
                if (!tempDateHashSet.Contains(revenueList[i].CreateDate))
                {
                    index++;
                    tempDateHashSet.Add(revenueList[i].CreateDate);
                    dateList.Add(revenueList[i].CreateDate);
                    values[index] += revenueList[i].AmountOfMoney;
                }
                else
                {
                    values[index] += revenueList[i].AmountOfMoney;
                }
            }


            StartDate = DateTime.ParseExact(dateList[0], "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            EndDate = DateTime.Now;


            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Revenue",
                    Values = new ChartValues<int>(values)
                }
            };

            //SeriesCollection.Add(new ColumnSeries
            //{
            //    Title = "Profile",
            //    Values = new ChartValues<double> { 10, 100, 15, 18 }
            //});
            BarLabels = new ObservableCollection<string>();

            foreach (string item in dateHashSet)
            {
                BarLabels.Add(item);
            }

            Formatter = value => value.ToString("N");
        }

        private HashSet<string> getDateList(List<RevenueInfor> revenueList)
        {
            HashSet<string> dateList = new HashSet<string>();

            foreach (RevenueInfor item in revenueList)
            {
                dateList.Add(item.CreateDate);
            }

            return dateList;
        }
    }
}
