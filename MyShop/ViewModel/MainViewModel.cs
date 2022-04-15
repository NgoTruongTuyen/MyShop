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
    public class MainViewModel: BaseViewModel
    {
        public event Action CurrentViewModelChanged;

        private readonly NavigationStore _navigationStore;
        public BaseViewModel CurrentViewModel => _navigationStore.CurrentViewModel;



        public ICommand NavigateOrderManagementCommand { get; }

        public MainViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;

            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            NavigateOrderManagementCommand = new NavigateCommand<OrderManagementViewModel>(navigationStore, () => new OrderManagementViewModel(navigationStore));
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
