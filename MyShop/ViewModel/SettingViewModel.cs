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
            }
            
        }

       
        public SettingViewModel()
        {

        }
    }
}
