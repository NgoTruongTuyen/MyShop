using Microsoft.Win32;
using MyShop.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MyShop.ViewModel
{
    public class AddProductViewModel : BaseViewModel
    {
        private string server = @"LAPTOP-R4MFGNUI\SQL";
        public RelayCommand importCommand { get; }
        public RelayCommand submitCommand { get; }
        public ImageSource Image { get; set; }
        public string ImageUrl { get; set; }
        public String Name { get; set; }
        public int Amount { get; set; }
        public int CostPrice { get; set; }
        public int SellingPrice { get; set; }
        public String Brand { get; set; }
        public double ScreenSize { get; set; }
        public String OS { get; set; }
        public String Color { get; set; }

        private DateTime _startDate = DateTime.Now;
        public DateTime DateRealease
        {
            get { return _startDate; }
            set { _startDate = value; OnPropertyChanged("StartDate"); }
        }

        public int Memory { get; set; }
        public int Storage { get; set; }
        public int Battery { get; set; }
        public List<String> CategoryList { get; set; }
        public List<List<String>> CategoryIDList { get; set; }
        public AddProductViewModel()
        {
            importCommand = new RelayCommand(importFile, null);
            submitCommand = new RelayCommand(movingImg, null);
            getBrand(server);
        }
        private void importFile(object parameter)
        {
            OpenFileDialog oft = new OpenFileDialog();
            oft.Filter = "Choose Image (*.jpg;*.png;*.gif)|*.jpg;*png;*gif";
            if (oft.ShowDialog() == true)
            {
                ImageUrl = oft.FileName;
                BitmapImage bitmap = new BitmapImage(new Uri(ImageUrl));
                Image = bitmap;

            }
        }
        private void getBrand(String server)
        {

            SqlConnection ConnectDatabase =
                new SqlConnection(string.Format($@"Server={server};Database=QLCH;Trusted_Connection=Yes;"));
            try
            {
                ConnectDatabase.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            // Sau khi kết nối thành công
            var sql = "select * from Brands";
            var command = new SqlCommand(sql, ConnectDatabase);
            var reader = command.ExecuteReader();
            List<String> CategoryNameList = new List<String>();
            List<List<String>> CategoryNameList2 = new List<List<String>>();

            while (reader.Read())
            {
                List<String> temp = new List<String>();
                var id = (int)reader["brandId"];
                var name = (string)reader["brandName"];
                temp.Add(id.ToString());
                temp.Add(name);
                CategoryNameList2.Add(temp);
                CategoryNameList.Add(name);
                
            }
            
            reader.Close();
            CategoryIDList = CategoryNameList2;
            CategoryList = CategoryNameList;
        }
        private bool addProduct(String server)
        {

            SqlConnection ConnectDatabase =
                new SqlConnection(string.Format($@"Server={server};Database=QLCH;Trusted_Connection=Yes;"));
            try
            {
                ConnectDatabase.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            String sql = "INSERT INTO dbo.Products(productName,imageURL,stock,costPrice,sellingPrice,brand,screenSize,os,color,memory,storage,battery,releaseDate,buyCounts,viewCounts) values " +
                "(@name,@image,@stock,@costPrice,@sellingPrice,@brand,@screenSize,@os,@color,@memory,@storage,@battery,@date,@buy,@view)";
            SqlCommand command = new SqlCommand(sql, ConnectDatabase);
            command.Parameters.AddWithValue("@name", Name);
            String tmp = Path.GetFileName(ImageUrl);
            tmp = @"/Image/product/" + tmp;
            command.Parameters.AddWithValue("@image",tmp);
            command.Parameters.AddWithValue("@stock", Amount);
            command.Parameters.AddWithValue("@costPrice", CostPrice);
            command.Parameters.AddWithValue("@sellingPrice", SellingPrice);
            for(int i = 0; i < CategoryIDList.Count; i++)
            {
                if(Brand == CategoryIDList[i][1])
                {
                    Brand = CategoryIDList[i][0];
                }
            }
            command.Parameters.AddWithValue("@brand", Int32.Parse(Brand));
            command.Parameters.AddWithValue("@screenSize", ScreenSize);
            command.Parameters.AddWithValue("@os", OS);
            command.Parameters.AddWithValue("@color", Color);
            command.Parameters.AddWithValue("@memory", Memory);
            command.Parameters.AddWithValue("@storage", Storage);
            command.Parameters.AddWithValue("@battery",Battery);
            command.Parameters.AddWithValue("@date", DateRealease);
            command.Parameters.AddWithValue("@buy", 0);
            command.Parameters.AddWithValue("@view", 0);
            int result = command.ExecuteNonQuery();
            
            // Check Error
            if (result < 0)
                return false;
            return true;
        }
        private void movingImg(object parameter)
        {
            string destinationFolder = @$"{Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName}\Image\product\";

            destinationFolder += Path.GetFileName(ImageUrl);
            Debug.WriteLine(destinationFolder);
            File.Copy(ImageUrl, destinationFolder);
            if (addProduct(server))
            {
                MessageBox.Show("Add successfully", "",
                   MessageBoxButton.OK, MessageBoxImage.Information);
            }
            
        }
    }
}
