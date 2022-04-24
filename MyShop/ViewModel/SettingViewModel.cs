using MyShop.Messenger;
using MyShop.Stores;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.ViewModel
{
    public class SettingViewModel: BaseViewModel
    {


        SettingMessenger setting = new SettingMessenger();
        

        //public propery SETTING 
        private string _quanlity;
        private string _reopen;
        public string Quanlity
        {
            get => _quanlity;
            set
            {
                _quanlity = value;
                OnPropertyChanged(nameof(Quanlity));
                Debug.WriteLine(_quanlity);
                setting.readData();
                setting.writeData(Int32.Parse( _quanlity), setting.Page);
            }
        }
        public string Reopen
        {
            get => _reopen;
            set
            {
                _reopen = value;
                OnPropertyChanged(nameof(Reopen));

                Debug.WriteLine(_reopen);

                 setting.readData();
                if(_reopen == "True")
                {

                    setting.writeData(setting.ItemPerPage, "setting");
                }
                else
                {


                    setting.writeData(setting.ItemPerPage, "off");
                }

            }
            
        }

       
        public SettingViewModel()
        {
 setting.readData();
            Quanlity = setting.ItemPerPage.ToString();
            if(setting.Page == "off")
            {
                Reopen = "False";
            }
            else
            {
                Reopen = "True";
            }

        }
         public SettingViewModel(NavigationStore navigationStore)
        {

            setting.readData();
            Quanlity = setting.ItemPerPage.ToString();
            if(setting.Page == "off")
            {
                Reopen = "False";
            }
            else
            {
                Reopen = "True";
            }
            
         
        }

    }
}
