using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using MyShop.Model;
using MyShop.ViewModel.Command;
using MyShop.ViewModel.Messenger;



namespace MyShop.ViewModel
{
    public  class LoginViewModel: BaseViewModel
    {


        public RelayCommand LoginCommand { get; set; }

        public LoginViewModel(MessengerEvent messenger)
        {
            _messenger = messenger;
            LoginCommand = new RelayCommand(isValidUser, null);
        }



        private MessengerEvent _messenger { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Visibility { get; set; } = "Hidden";
        private void isValidUser(object x)
        {
            PasswordBox pass = x as PasswordBox;
            if(Username == "Hello")
            {
                if(pass.Password == "World")
                {
                    // 
                    Debug.WriteLine("OKOK");
                    
                    _messenger.executeAction(x);
                    return;
                }
                
            }

            Debug.WriteLine("NONO");
            Visibility = "Visible";

        }
    }
}
