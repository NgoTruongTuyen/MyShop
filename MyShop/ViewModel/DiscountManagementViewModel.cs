using MyShop.Commands;
using MyShop.DAO;
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
    public class DiscountManagementViewModel: BaseViewModel
    {
        public ICommand NavigateAddDiscountCommand { get; }
        public ObservableCollection<Discount> DiscountList { get; set; }
        public DiscountManagementViewModel(NavigationStore navigationStore)
        {
            DiscountDAO discountDAO = new DiscountDAO();

            DiscountList = discountDAO.getAll();

            NavigateAddDiscountCommand = new NavigateCommand<AddNewDiscountView>(navigationStore, () => new AddNewDiscountView(navigationStore));
        }
    }
}
