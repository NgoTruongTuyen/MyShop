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
        
        //private MessengerEvent _message { get;set; }
        //private LoginWindow _loginWindow { get;set; }
        //protected override void OnStartup(StartupEventArgs e)
        //{
            
        //    _message = new MessengerEvent();
        //    _message.Message += openMainWindow;

        //    _loginWindow = new LoginWindow()
        //    {
        //        DataContext = new LoginViewModel(_message)
        //    };
        //    _loginWindow.Show();


        //    base.OnStartup(e);
        //}

        //private void openMainWindow(object x)
        //{
        //    NavigationStore navigationStore = new NavigationStore();
        //    navigationStore.CurrentViewModel = new DashboardViewModel();
        //    MainWindow = new MainWindow() {
        //        DataContext = new MainViewModel(navigationStore)
        //    };
        //    MainWindow.Show();

        //    _loginWindow.Close();

        //}
    }
}
