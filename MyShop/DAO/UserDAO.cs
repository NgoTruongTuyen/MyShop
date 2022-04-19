using MyShop.DatabaseConnection;
using MyShop.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DAO
{
    class UserDAO
    {
         DBConnection DBConn = DBConnection.GetInstance();
        public List<User> GetAll()
        {
            List<User> users = new List<User>();
            string sql = "select * from Users   ";
            SqlDataReader reader = DBConn.query(sql);

            while (reader.Read())
            {
                var username = (string) reader["username"];
                var password = (string)reader["password"];
                var entropy = (string)reader["entropy"];
                var remember = (Boolean)reader["remember"];

                users.Add(new User()
                {
                    UserName = username,
                    Password = password,
                    Entropy = entropy,
                    Remember = remember


                }) ;
            }

            reader.Close();
            return users;
        }

        public void insertUser()
        {

            string sql = "insert into Users (username, password, entropy) " +
             "values(@username, @password, @entropy,@remember)";

            string data = encode("123456");



            DBConn.excute(sql, new SqlParameter("@username", "vdphuc"),
                new SqlParameter("@password", data.Split(' ')[0]),
                new SqlParameter("@entropy", data.Split(' ')[1]),
                new SqlParameter("@remember", false)
                ) ;





        }

        public bool checkUser(string username, string password)
        {

            var data = GetAll();

            var user = data.Where(x => x.UserName == username).ToList();
            Debug.WriteLine(user);
            if(user.Count()==0)
            {
                return false;
            }

            var realPass = decode(user[0].Password, user[0].Entropy);

            return realPass == password;

        }




        private string encode(string password)
        {

            var passwordInBytes = Encoding.UTF8.GetBytes(password);

            var entropy = new byte[20];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(entropy);
            }
            var entropyBase64 = Convert.ToBase64String(entropy);

            var cypherText = ProtectedData.Protect(passwordInBytes, entropy,
                DataProtectionScope.CurrentUser);
            var cypherTextBase64 = Convert.ToBase64String(cypherText);

            Debug.WriteLine(cypherTextBase64);
            Debug.WriteLine(entropyBase64);
            Debug.WriteLine("=========================");

            return cypherTextBase64 + " " + entropyBase64;

        }

        private string decode(string cypherTextBase64, string entropyBase64)
        {
             var cypherTextInBytes = Convert.FromBase64String(cypherTextBase64);

            var entropyTextInBytes = Convert.FromBase64String(entropyBase64);

            var passwordInBytesR = ProtectedData.Unprotect(cypherTextInBytes,
                entropyTextInBytes, DataProtectionScope.CurrentUser);

            var result = Encoding.UTF8.GetString(passwordInBytesR);
            Debug.WriteLine(result);
            return result;

        }

         internal void setRemember(string username)
        {
            string sql = "update Users set remember =@remember where username=@username";

            DBConn.excute(sql, new SqlParameter("@remember", true), new SqlParameter("@username", username));
        }


        public bool checkUsername(string username)
        {
            var data = GetAll();
            return data.Where(x => x.UserName == username).Count() == 1;
        }
        public string  getPassword(string username)
        {
            var data = GetAll();
            var pass = data.Where(x => x.UserName == username).Select(y => y.Password).ToList();
            var entropy = data.Where(x => x.UserName == username).Select(y => y.Entropy).ToList();
            return decode(pass[0], entropy[0]);
        }


    }
}
