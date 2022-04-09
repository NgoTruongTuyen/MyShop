using MyShop.Commands;
using MyShop.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyShop.ViewModel
{
    public class AddNewOrderViewModel : BaseViewModel
    {
        public ICommand NavigateCancelCommand { get; set; }
        public ICommand NavigateSubmitCommand { get; set; }

        public AddNewOrderViewModel(NavigationStore navigationStore)
        {
           NavigateCancelCommand = new NavigateCommand<OrderManagementViewModel>(navigationStore, () => new OrderManagementViewModel(navigationStore));
           NavigateSubmitCommand = new NavigateCommand<OrderManagementViewModel>(navigationStore, () => new OrderManagementViewModel(navigationStore));
        }
    }
}
