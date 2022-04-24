using MyShop.Commands;
using MyShop.Messenger;
using MyShop.Stores;
using MyShop.View;
using MyShop.ViewModel.Command;
using MyShop.ViewModel.Messenger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        public ICommand NavigateSettingCommand { get; }
        public RelayCommand LogoutCommand { get; }
        private LoginWindow _login { get; set; }
        private MessengerEvent _message { get; set; }

        SettingMessenger setting { get; set; }

        public MainViewModel(NavigationStore navigationStore)
        {
            
            _navigationStore = navigationStore;

            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            NavigateOrderManagementCommand = new NavigateCommand<OrderManagementViewModel>(navigationStore, () => new OrderManagementViewModel(navigationStore));


            NavigateDiscountManagementCommand = new NavigateCommand<DiscountManagementViewModel>(navigationStore, () => new DiscountManagementViewModel(navigationStore));


            NavigateDashboardCommand = new NavigateCommand<DashboardViewModel>(navigationStore, () => new DashboardViewModel(navigationStore));

            NavigateStatisticCommand = new NavigateCommand<StatisticViewModel>(navigationStore, () => new StatisticViewModel(navigationStore));

            NavigateProductCommand = new NavigateCommand<ProductViewModel>(navigationStore, () => new ProductViewModel(navigationStore));

            NavigateSettingCommand = new NavigateCommand<SettingViewModel>(navigationStore, () => new SettingViewModel(navigationStore));
            LogoutCommand = new RelayCommand(logoutCommand);
            _message = new MessengerEvent();
            _message.Message += openMainWindow;
            setting = new SettingMessenger();
        }

        private BaseViewModel GetCurrentViewModel()
        {
            return CurrentViewModel;
        }

        private void logoutCommand(object x)
        {
            setting.readData();

            if (setting.Page != "off")
            {
                if (_navigationStore.CurrentViewModel is DashboardViewModel)
                {

                    setting.writeData(setting.ItemPerPage, "dashboard");
                }
                else if (_navigationStore.CurrentViewModel is OrderManagementViewModel)
                {

                    setting.writeData(setting.ItemPerPage, "order");
                }

                else if (_navigationStore.CurrentViewModel is ProductViewModel)
                {

                    setting.writeData(setting.ItemPerPage, "product");
                }
                else if (_navigationStore.CurrentViewModel is SettingViewModel)
                {


                    setting.writeData(setting.ItemPerPage, "setting");
                }
                else if (_navigationStore.CurrentViewModel is DiscountManagementViewModel)
                {

                    setting.writeData(setting.ItemPerPage, "discount");
                }
                else
                {
                    setting.writeData(setting.ItemPerPage, "statistic");
                }

            }


            _login = new LoginWindow()
            {
                DataContext = new LoginViewModel(_message)
            };
            
            _login.Show();
            (x as MainWindow).Close();

        }

        private void openMainWindow(object x)
        {
            setting.readData();
            NavigationStore navigationStore = new NavigationStore();
             if(setting.Page == "setting")
            {


                navigationStore.CurrentViewModel = new SettingViewModel();
            }
            else if (setting.Page == "import")
            {

                navigationStore.CurrentViewModel = new ImportViewModel();
            }
            else if(setting.Page == "order")
            {

                navigationStore.CurrentViewModel = new OrderManagementViewModel(navigationStore);
            }
            else if(setting.Page == "product")
            {

                navigationStore.CurrentViewModel = new ProductViewModel(navigationStore);
            }
            else if(setting.Page == "discount")
            {

                navigationStore.CurrentViewModel = new DiscountManagementViewModel(navigationStore);
            }
            else if (setting.Page =="statistic")
            {
                navigationStore.CurrentViewModel = new StatisticViewModel(navigationStore);
            }
            else 
            {

                navigationStore.CurrentViewModel = new DashboardViewModel(navigationStore);

            }
            MainWindow main = new MainWindow()
            {
                DataContext = new MainViewModel(navigationStore)
            };
            main.Show();

            _login.Close();

        }


        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
