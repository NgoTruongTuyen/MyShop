using MyShop.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyShop.ViewModel
{
    public class BrandCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class CategoryViewModel : BaseViewModel
    {
        private string server = @"LAPTOP-R4MFGNUI\SQL";
        public ObservableCollection<BrandCategory> CategoryList { get; set; }
        public BrandCategory SelectedCategory { get; set; }
        public String NewBrand { get; set; }
        public String EditBrandName { get; set; }
        public RelayCommand addBrandCommand { get; }
        public RelayCommand editBrandCommand { get; }
        public RelayCommand deleteBrandCommand { get; }
        public RelayCommand clickBrandCommand { get; }
        public CategoryViewModel()
        {
            CategoryList = getDataFromDatabase(server);
            addBrandCommand = new RelayCommand(doAddCommand, null);
            clickBrandCommand = new RelayCommand(setEditName, null);
            editBrandCommand = new RelayCommand(updateBrand, null);
            deleteBrandCommand = new RelayCommand(deleteBrand, null);
        }
        private void setEditName(object parameter)
        {
            EditBrandName = SelectedCategory.Name;
        }
        private ObservableCollection<BrandCategory> getDataFromDatabase(String server)
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

            ObservableCollection<BrandCategory> categories = new ObservableCollection<BrandCategory>();
            while (reader.Read())
            {
                var id = (int)reader["brandId"];
                var name = (string)reader["brandName"];
                BrandCategory categoryTmp = new BrandCategory()
                {
                    Id = id,
                    Name = name
                };
                categories.Add(categoryTmp);
            }

            reader.Close();
            return categories;
        }
        private void doAddCommand(object parameter)
        {
            addBrand(server);
            CategoryList = getDataFromDatabase(server);
        }
        private void addBrand(String server)
        {
            if (NewBrand.Length == 0)
            {
                MessageBox.Show("Please fill your new brand's name", "", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
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
                String sql = "INSERT INTO dbo.Brands(brandName) values " +
                    "(@name)";
                SqlCommand command = new SqlCommand(sql, ConnectDatabase);
                command.Parameters.AddWithValue("@name", NewBrand);

                int result = command.ExecuteNonQuery();

                // Check Error
                if (result < 0)
                    MessageBox.Show("Add fail", "", MessageBoxButton.OK, MessageBoxImage.Error);
                MessageBox.Show("Add successfully", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void updateBrand(object parameter)
        {
            updateBrand(server);
            CategoryList = getDataFromDatabase(server);
        }
        private void updateBrand(String server)
        {
            if ( SelectedCategory == null || EditBrandName == null)
            {
                MessageBox.Show("Error", "Update Category", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if(EditBrandName.Length == 0)
                {
                    MessageBox.Show("Fill new brand's name", "Update Category", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if (MessageBox.Show("Are you sure to update this Category?", "Update Category", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
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
                        String sql = "Update dbo.Brands set brandName = @name where brandId = @id";
                        SqlCommand command = new SqlCommand(sql, ConnectDatabase);
                        command.Parameters.AddWithValue("@id", SelectedCategory.Id);
                        command.Parameters.AddWithValue("@name", EditBrandName);

                        int result = command.ExecuteNonQuery();

                        // Check Error
                        if (result < 0)
                            MessageBox.Show("Update fail", "", MessageBoxButton.OK, MessageBoxImage.Error);
                        else
                            MessageBox.Show("Update successfully", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                }
                
            }
           
        }
        private void deleteBrand(object parameter)
        {
            deleteBrand(server);
            CategoryList = getDataFromDatabase(server);
        }
        private void deleteBrand(String server)
        {
            
            if (MessageBox.Show("Are you sure to delete this Category?", "Delete Category", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
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
                String sql = "Delete from dbo.Brands where brandId = @id";
                SqlCommand command = new SqlCommand(sql, ConnectDatabase);
                command.Parameters.AddWithValue("@id", SelectedCategory.Id);

                int result = command.ExecuteNonQuery();

                // Check Error
                if (result < 0)
                    MessageBox.Show("Delete fail", "", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                    MessageBox.Show("Delete successfully", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
