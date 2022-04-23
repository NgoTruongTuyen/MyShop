using MyShop.Commands;
using MyShop.DAO;
using MyShop.Model;
using MyShop.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyShop.ViewModel
{
    internal class AddNewDiscountView: BaseViewModel
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int MinOrderValue { get; set; }
        public int DiscountPercentage { get; set; }
        public ICommand NavigateCancelCommnd { get; set; }
        public ICommand NavigateSubmitCommnd { get; set; }
        public AddNewDiscountView(NavigationStore navigationStore)
        {
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;

            NavigateCancelCommnd = new NavigateCommand<DiscountManagementViewModel>(navigationStore, () => new DiscountManagementViewModel(navigationStore));
            NavigateSubmitCommnd = new NavigateCommand<DiscountManagementViewModel>(navigationStore, () =>
            {
                DiscountDAO discountDAO = new DiscountDAO();

                Discount discount = new Discount() { DiscountPercentage = this.DiscountPercentage, StartDate = this.StartDate, EndDate = this.EndDate, MinOrderValue = this.MinOrderValue, Name = this.Name };

                discountDAO.insertOne(discount);

                return new DiscountManagementViewModel(navigationStore);
                });
        }
    }
}
