using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using MyShop.DAO;
using MyShop.Model;
using MyShop.ViewModel.Command;
using MyShop.ViewModel.Messenger;



namespace MyShop.ViewModel
{
    public class LoginViewModel : BaseViewModel
    { 
        private UserDAO _userDAO = new UserDAO();

        public RelayCommand LoginCommand { get; set; }

        public LoginViewModel(MessengerEvent messenger)
        {


            _messenger = messenger;
            LoginCommand = new RelayCommand(isValidUser, null);
            getSuggestion();
            MouseDownCommand = new RelayCommand(mouseDown, null);
            Visibility = "Hidden";
               

        }

        private void  getSuggestion()
        {
            var data = _userDAO.GetAll();
            var dataFilter = data.Where(x => x.Remember == true);

            //Debug.WriteLine("+++++++++++++");
            //Debug.WriteLine(data[0].Remember);
            //Debug.WriteLine(data[0].Remember.GetType());
                

            Suggestion = new List<string>(dataFilter.Select(x => x.UserName)); 

        }

        void mouseDown(object x)
        {
            VisibilityList = "Hidden";
        }



        private MessengerEvent _messenger { get; set; }

        public string _username;
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
                VisibilityList = "Visible";
            }
        }
        public string Password { get; set; }
        public string Visibility { get; set; }
        public List<string> Suggestion { get; set; }

        public RelayCommand MouseDownCommand { get; set; }

        public string _itemSelected;
        public bool CheckBox { get; set; }
        public string ItemSelected
        {
            get
            {
                return _itemSelected;
            }
            set
            {
                _itemSelected = value;
                OnPropertyChanged(nameof(ItemSelected));
                Username = ItemSelected;

                VisibilityList = "Hidden";

                if (_userDAO.checkUsername(_username))
                {
                    Password =_userDAO.getPassword(_username);
                }
            }
        }

        public string VisibilityList { get; set; } = "Hidden";

        public void setVisibleList()
        {
            if (VisibilityList == "Visible")
            {
                VisibilityList = "Hidden";
                return;
            }
            VisibilityList = "Visible";


        }



        private void isValidUser(object x)
        {
            PasswordBox pass = x as PasswordBox;
            if (_userDAO.checkUser(Username, pass.Password))
            {
                Debug.WriteLine("OKOK");
                if (CheckBox)
                {

                    _userDAO.setRemember(Username);
                }
                _messenger.executeAction(x);
                return;
            }

            Debug.WriteLine("NONO");
            Visibility = "Visible";

        }


    }
}
