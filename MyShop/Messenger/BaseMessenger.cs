using MyShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Messenger
{
    public class BaseMessenger <T>
    {
       
            public event Action<T> action;
            public void excute (T ob)
            {
                action?.Invoke(ob);
            }
      
    }
}
