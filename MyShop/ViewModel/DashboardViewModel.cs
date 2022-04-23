using Aspose.Cells.Charts;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using MyShop.DAO;
using MyShop.Model;
using MyShop.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyShop.ViewModel
{
    public class DashboardViewModel : BaseViewModel
    {
        private ProductDAO _productDAO = new ProductDAO();
        private OrderDAO _orderDAO = new OrderDAO();

        private ObservableCollection<Product> ChartData  { get; set; }


        public LiveCharts.SeriesCollection Series { get; set; } = new LiveCharts.SeriesCollection();

        public string NumberProduct { get; set; } = "100#";
        public string NumberOrder { get; set; } = "1000#";
        public string Income { get; set; } = "10000#";

        public ObservableCollection<Product> Products { get; set; }

        //Command

        public RelayCommand DayCommand { get; set; }
        public RelayCommand MonthCommand { get; set; }
        public RelayCommand YearCommand { get; set; }

        private void InitChart()
        {
            foreach(var product in ChartData)
            {
                
                Series.Add(new PieSeries {
                    Title = product.Brand,
                    Values =  new ChartValues<ObservableValue>{new ObservableValue(product.BuyCounts) },
                    DataLabels =true,
                 }
            );
                 
            }
        }
        
        private void dayCommand(object x)
        {
             NumberOrder = _orderDAO.getCount(0).ToString();
             Income = _orderDAO.getAllPrice(0).ToString();

        }
           private void monthCommand(object x)
        {
             NumberOrder = _orderDAO.getCount(1).ToString();
             Income = _orderDAO.getAllPrice(1).ToString();

        }

           private void yearCommand(object x)
        {
             NumberOrder = _orderDAO.getCount(2).ToString();
             Income = _orderDAO.getAllPrice(2).ToString();

        }
        public DashboardViewModel()
        {
            Products = _productDAO.getTopFive();
            NumberProduct = _productDAO.GetAll().Count().ToString();
            NumberOrder = _orderDAO.getCount(0).ToString();
            Income = _orderDAO.getAllPrice(0).ToString();


            DayCommand = new RelayCommand(dayCommand,null);
            MonthCommand = new RelayCommand(monthCommand,null);
            YearCommand = new RelayCommand(yearCommand,null);

            ChartData= _productDAO.getDashboardChart();
            InitChart();

            

        }

        
    }
}
