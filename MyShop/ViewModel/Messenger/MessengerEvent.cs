using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.ViewModel.Messenger
{
    public class MessengerEvent
    {
         public event Action<object> Message;
        public void executeAction(object message)
        {
            Message?.Invoke(message);
        }

    }
}
