using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Model
{
    public class RevenueInfor : BaseModel
    {
        public string CreateDate { get; set; }
        public int AmountOfMoney { get; set; }

        public RevenueInfor()
        {

        }

        public RevenueInfor(string createDate, int money)
        {
            CreateDate = createDate;
            AmountOfMoney = money;
        }
    }
}
