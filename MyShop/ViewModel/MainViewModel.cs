using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.ViewModel
{
    public class MainViewModel: BaseViewModel
    {
        public BaseViewModel CurrentViewModel { get; set; }
        public BaseViewModel OrderManagementViewModel { get; set; }
        public MainViewModel()
        {
            CurrentViewModel = new DashboardViewModel();
            OrderManagementViewModel = new OrderManagementViewModel();
        }
    }
}
