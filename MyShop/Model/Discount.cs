using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Model
{
    public class Discount:BaseModel
    {
        public int DiscountID { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int MinOrderValue { get; set; }
        
        public int DiscountPercentage { get; set; }

        public Discount(int discountID, string name, DateTime startDate, DateTime endDate, int minOrderValue, int discountPercentage)
        {
            DiscountID = discountID;
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            MinOrderValue = minOrderValue;
            DiscountPercentage = discountPercentage;
        }

        public Discount()
        {
        }
    }
}
