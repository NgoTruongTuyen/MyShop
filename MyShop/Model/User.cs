using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Model
{
    public class User
    {
        private string _username;
        private string _password;
        private string _entropy;
        private bool _remember;
        public string UserName { get { return _username; } set { _username = value; } }
        public string Password { get { return _password; } set { _password = value; } }
        public string Entropy { get { return _entropy; }  set{_entropy = value;} } 
        public bool Remember { get { return _remember; } set{ _remember = value;} } 
    }
}
