using MyShop.Commands;
using MyShop.Messenger;
using MyShop.Stores;
using MyShop.ViewModel;
using MyShop.ViewModel.Messenger;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MyShop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private MessengerEvent _message { get; set; }
        private LoginWindow _loginWindow { get; set; }
        private SettingMessenger setting { get; set; }
        protected override void OnStartup(StartupEventArgs e)
        {



            _message = new MessengerEvent();
            _message.Message += openMainWindow;

            _loginWindow = new LoginWindow()
            {
                DataContext = new LoginViewModel(_message)
            };
            _loginWindow.Show();

            setting = new SettingMessenger();
            setting.readData();



            base.OnStartup(e);
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

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(navigationStore)
            };
            MainWindow.Show();

            _loginWindow.Close();

        }
    }
}
