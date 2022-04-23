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
    class BrandDAO
    {

         DBConnection DBConn = DBConnection.GetInstance();

        public ObservableCollection<Brand> getAll()
        {
            ObservableCollection < Brand > list = new ObservableCollection<Brand> ();

            string sql = "select * from Brands";

            SqlDataReader reader = DBConn.query (sql);

            while (reader.Read()) 
            {
                int id = (int)reader["brandId"];
                string name = (string)reader["brandName"];
               
                list.Add(new Brand() { 
                    Name =name,
                    BrandId = id
                });
            }

            return list;
        }

    }
}
