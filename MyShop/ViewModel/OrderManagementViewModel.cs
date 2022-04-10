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
    
    public class OrderManagementViewModel : BaseViewModel
    {
        public ICommand NavigateAddNewOrderCommand { get; set; }
       public OrderManagementViewModel (NavigationStore navigationStore)
        {
            NavigateAddNewOrderCommand=new NavigateCommand<AddNewOrderViewModel>(navigationStore, () => new AddNewOrderViewModel(navigationStore));
        }
    }
}
