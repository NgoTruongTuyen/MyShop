using MyShop.DatabaseConnection;
using MyShop.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DAO
{
    public class DiscountDAO
    {
        DBConnection DBConn = DBConnection.GetInstance();

        public ObservableCollection<Discount> getAll()
        {
            ObservableCollection < Discount > list = new ObservableCollection<Discount> ();

            string sql = "select * from Discounts";

            SqlDataReader reader = DBConn.query (sql);

            while (reader.Read()) 
            {
                int id = (int)reader["discountID"];
                int min = (int)reader["minOrderValue"];
                DateTime startDate = (DateTime)reader["startDate"];
                DateTime endDate = (DateTime)reader["endDate"];
                string name = (string)reader["name"];
                int percentage = (int)reader["discountPercentage"];

                list.Add(new Discount(id, name, startDate, endDate, min, percentage));
            }

            return list;
        }

        public void insertOne(Discount discount)
        {
            string sql = "insert into Discounts(minOrderValue, startDate, endDate, name, discountPercentage) values (@min, @start, @end, @name, @percent)";

            DBConn.excute(sql, 
                new SqlParameter("@min", discount.MinOrderValue),
                new SqlParameter("@start", discount.StartDate),
                new SqlParameter("@end", discount.EndDate),
                new SqlParameter("@name", discount.Name),
                new SqlParameter("@percent", discount.DiscountPercentage)
                );
        }

        public Discount getBestDiscount (int orderValue)
        {
            List<Discount> list = new List<Discount>();

            string sql = "select * from Discounts where CURRENT_TIMESTAMP >= startDate and CURRENT_TIMESTAMP <= endDate";

            SqlDataReader reader = DBConn.query(sql);

            while (reader.Read())
            {
                int id = (int)reader["discountID"];
                int min = (int)reader["minOrderValue"];
                DateTime startDate = (DateTime)reader["startDate"];
                DateTime endDate = (DateTime)reader["endDate"];
                string name = (string)reader["name"];
                int percentage = (int)reader["discountPercentage"];

                list.Add(new Discount(id, name, startDate, endDate, min, percentage));
            }

            Discount _discount = new Discount() { DiscountPercentage = 0, Name = "None discount" };

            foreach (Discount discount in list)
            {
               if( _discount.DiscountPercentage<discount.DiscountPercentage && orderValue>discount.MinOrderValue)
                    _discount=discount;
            }

            reader.Close();

            return _discount;
        }
    }
}
