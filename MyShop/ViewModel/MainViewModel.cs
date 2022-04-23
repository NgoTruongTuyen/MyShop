using MyShop.Commands;
using MyShop.Stores;
using MyShop.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
namespace MyShop.ViewModel
{
    public class MainViewModel: BaseViewModel
    {
        public event Action CurrentViewModelChanged;

        private readonly NavigationStore _navigationStore;
        public BaseViewModel CurrentViewModel => _navigationStore.CurrentViewModel;
        public ICommand NavigateOrderManagementCommand { get; }
        public ICommand NavigateDiscountManagementCommand { get; }
        public ICommand NavigateDashboardCommand { get; }

        public ICommand NavigateProductCommand { get; }
        public ICommand NavigateStatisticCommand { get; }
        public ICommand NavigateProfileCommand { get; }

        public MainViewModel(NavigationStore navigationStore)
        {
            
            _navigationStore = navigationStore;

            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            NavigateOrderManagementCommand = new NavigateCommand<OrderManagementViewModel>(navigationStore, () => new OrderManagementViewModel(navigationStore));


            NavigateDiscountManagementCommand = new NavigateCommand<DiscountManagementViewModel>(navigationStore, () => new DiscountManagementViewModel(navigationStore));


            NavigateDashboardCommand = new NavigateCommand<DashboardViewModel>(navigationStore, () => new DashboardViewModel(navigationStore));

            NavigateStatisticCommand = new NavigateCommand<StatisticViewModel>(navigationStore, () => new StatisticViewModel(navigationStore));

            NavigateProductCommand = new NavigateCommand<ProductViewModel>(navigationStore, () => new ProductViewModel(navigationStore));
        }

        private BaseViewModel GetCurrentViewModel()
        {
            return CurrentViewModel;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
