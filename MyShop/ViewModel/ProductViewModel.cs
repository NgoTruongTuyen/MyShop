using MyShop.Model;
using MyShop.View;
using MyShop.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MyShop.ViewModel
{
    public class ProductViewModel : BaseViewModel
    {
        private ObservableCollection<Category> _categories;

        private string server = @"LAPTOP-R4MFGNUI\SQL";
        private ObservableCollection<Product> _currentCategory { get; set; }
        public List<Product> _selectedCategory { get; set; }
        public Product _selectedProduct { get; set; }
        public String currentPagingTextBlock { get; set; }
        private int _totalItems { get; set; }
        private int _currentPage { get; set; }
        private int _totalPages { get; set; }
        private String searchKey;
        private String filterKey;
        private String categoryName { get; set; }
        public List<String> CategoryList { get; set; }
        public String CategoryName
        {
            get { return categoryName; }
            set
            {
                if (!string.Equals(categoryName, value))
                {
                    categoryName = value;
                    paging(value);
                }
            }
        }
        public String SearchKey
        {
            get { return searchKey; }
            set
            {
                if (!string.Equals(searchKey, value))
                {
                    searchKey = value;
                }
            }
        }
        public string FilterKey
        {
            get { return filterKey; }
            set
            {
                if (!string.Equals(filterKey, value))
                {
                    filterKey = value;
                    filter(value);
                }
            }
        }
        int _rowsPerPage = 9;
        public RelayCommand nextPageCommand { get; }
        public RelayCommand previousPageCommand { get; }
        public RelayCommand firstPageCommand { get; }
        public RelayCommand lastPageCommand { get; }
        public RelayCommand searchCommand { get; }
        public RelayCommand addProductCommand { get; }

        private ICommand doubleClickProductCommand;
        public RelayCommand editProductCommand { get; }
        public RelayCommand deleteProductCommand { get; }
        public ProductViewModel()
        {

            _categories = getDataFromDataBase(server);
            paging("All");
            categoryName = "All";
            nextPageCommand = new RelayCommand(clickNextPage, null);
            previousPageCommand = new RelayCommand(clickPreviousPage, null);
            firstPageCommand = new RelayCommand(clickFirstPage, null);
            lastPageCommand = new RelayCommand(clickLastPage, null);

            searchCommand = new RelayCommand(search, null);
            addProductCommand = new RelayCommand(addProduct, null);
            editProductCommand = new RelayCommand(editProduct, null);
            deleteProductCommand = new RelayCommand(deleteProduct, null);

        }
        public ObservableCollection<Category> Categories
        {
            get { return _categories; }
            set { _categories = value; }
        }
        public ObservableCollection<Category> getDataFromDataBase(String server)
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
            CategoryNameList.Add("All");

            ObservableCollection<Category> categories = new ObservableCollection<Category>();
            while (reader.Read())
            {
                List<String> temp = new List<String>();
                var id = (int)reader["brandId"];
                var name = (string)reader["brandName"];
                temp.Add(id.ToString());
                temp.Add(name);
                CategoryNameList2.Add(temp);
                CategoryNameList.Add(name);
                Category categoryTmp = new Category()
                {
                    Brand = name,
                    Products = new ObservableCollection<Product>()
                };
                categories.Add(categoryTmp);
            }
            Category categoryAll = new Category()
            {
                Brand = "All",
                Products = new ObservableCollection<Product>()
            };
            reader.Close();

            sql = "select * from Products";
            command = new SqlCommand(sql, ConnectDatabase);

            reader = command.ExecuteReader();

            while (reader.Read())
            {
                var id = (int)reader["productID"];
                var name = (string)reader["productName"];
                var brandId = (int)reader["brand"];

                String brand = "";

                for (int i = 0; i < CategoryNameList2.Count; i++)
                {
                    if (brandId.ToString() == CategoryNameList2[i][0])
                    {
                        brand = CategoryNameList2[i][1];
                        break;
                    }
                }

                var imageURL = (string)reader["imageURL"];
                var sellingPrice = (int)reader["sellingPrice"];
                var stock = (int)reader["stock"];
                var costPrice = (int)reader["costPrice"];
                var screenSize = (double)reader["screenSize"];
                var os = (string)reader["os"];
                var color = (string)reader["color"];
                var memory = (int)reader["memory"];
                var storage = (int)reader["storage"];
                var battery = (int)reader["battery"];
                var releaseDate = (DateTime)reader["releaseDate"];
                var buyCounts = (int)reader["buyCounts"];
                var viewCounts = (int)reader["viewCounts"];
                Product tmp = new Product(id, name, imageURL, costPrice, sellingPrice, screenSize, os, color, memory, storage, battery, releaseDate, stock, brand, viewCounts, buyCounts);
                for (int i = 0; i < categories.Count; i++)
                {
                    if (categories[i].Brand == brand)
                    {
                        categories[i].Products.Add(tmp);
                        break;
                    }
                }
                categoryAll.Products.Add(tmp);
            }
            reader.Close();

            categories.Add(categoryAll);
            CategoryList = CategoryNameList;
            return categories;
        }

        public ObservableCollection<Product> findProductByName(String server, string productName)
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

            var sql = "select * from Brands";
            var command = new SqlCommand(sql, ConnectDatabase);
            var reader = command.ExecuteReader();
            List<List<String>> CategoryNameList = new List<List<String>>();

            while (reader.Read())
            {
                List<String> temp = new List<String>();
                var id = (int)reader["brandId"];
                var name = (string)reader["brandName"];
                temp.Add(id.ToString());
                temp.Add(name);
                CategoryNameList.Add(temp);

            }
            reader.Close();


            sql = $"select * from Products where productName like '%{productName}%'";
            command = new SqlCommand(sql, ConnectDatabase);
            reader = command.ExecuteReader();

            ObservableCollection<Product> result = new ObservableCollection<Product>();

            while (reader.Read())
            {
                var id = (int)reader["productID"];
                var name = (string)reader["productName"];
                var brandId = (int)reader["brand"];

                String brand = "";

                for (int i = 0; i < CategoryNameList.Count; i++)
                {
                    if (brandId.ToString() == CategoryNameList[i][0])
                    {
                        brand = CategoryNameList[i][1];
                        break;
                    }
                }
                var imageURL = (string)reader["imageURL"];
                var sellingPrice = (int)reader["sellingPrice"];
                var stock = (int)reader["stock"];
                var costPrice = (int)reader["costPrice"];
                var screenSize = (double)reader["screenSize"];
                var os = (string)reader["os"];
                var color = (string)reader["color"];
                var memory = (int)reader["memory"];
                var storage = (int)reader["storage"];
                var battery = (int)reader["battery"];
                var releaseDate = (DateTime)reader["releaseDate"];
                var buyCounts = (int)reader["buyCounts"];
                var viewCounts = (int)reader["viewCounts"];
                Product tmp = new Product(id, name, imageURL, costPrice, sellingPrice, screenSize, os, color, memory, storage, battery, releaseDate, stock, brand, viewCounts, buyCounts);

                result.Add(tmp);
            }
            reader.Close();


            return result;
        }
        public ObservableCollection<Product> filterProductByPrice(String server, String price)
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
            String content = "";
            price = price.Split("System.Windows.Controls.ComboBoxItem: ")[1];
            if (price.ToString() == "0 - 5,000,000")
            {
                content = "<= 5000000";
            }
            else if (price.ToString() == "5,000,000 - 10,000,000")
            {
                content = "> 5000000 and sellingPrice < 10000000";
            }
            else if (price.ToString() == "10,000,000+")
            {
                content = ">=10000000";
            }
            var sql = "select * from Brands";
            var command = new SqlCommand(sql, ConnectDatabase);
            var reader = command.ExecuteReader();
            List<List<String>> CategoryNameList = new List<List<String>>();

            while (reader.Read())
            {
                List<String> temp = new List<String>();
                var id = (int)reader["brandId"];
                var name = (string)reader["brandName"];
                temp.Add(id.ToString());
                temp.Add(name);
                CategoryNameList.Add(temp);

            }
            reader.Close();

            sql = $"select * from Products where sellingPrice {content}";
            command = new SqlCommand(sql, ConnectDatabase);

            reader = command.ExecuteReader();

            ObservableCollection<Product> result = new ObservableCollection<Product>();

            while (reader.Read())
            {
                var id = (int)reader["productID"];
                var name = (string)reader["productName"];
                var brandId = (int)reader["brand"];

                String brand = "";

                for (int i = 0; i < CategoryNameList.Count; i++)
                {
                    if (brandId.ToString() == CategoryNameList[i][0])
                    {
                        brand = CategoryNameList[i][1];
                        break;
                    }
                }

                var imageURL = (string)reader["imageURL"];
                var sellingPrice = (int)reader["sellingPrice"];
                var stock = (int)reader["stock"];
                var costPrice = (int)reader["costPrice"];
                var screenSize = (double)reader["screenSize"];
                var os = (string)reader["os"];
                var color = (string)reader["color"];
                var memory = (int)reader["memory"];
                var storage = (int)reader["storage"];
                var battery = (int)reader["battery"];
                var releaseDate = (DateTime)reader["releaseDate"];
                var buyCounts = (int)reader["buyCounts"];
                var viewCounts = (int)reader["viewCounts"];
                Product tmp = new Product(id, name, imageURL, costPrice, sellingPrice, screenSize, os, color, memory, storage, battery, releaseDate, stock, brand, viewCounts, buyCounts);

                result.Add(tmp);
            }
            reader.Close();


            return result;
        }
        public ObservableCollection<Product> getProductOfCategory(string name)
        {
            ObservableCollection<Product> category = null;
            _categories = getDataFromDataBase(server);
            for (int i = 0; i < _categories.Count; i++)
            {
                if (_categories[i].Brand == name)
                {
                    category = _categories[i].Products;
                    break;
                }
            }
            return category;
        }
        public void paging(String name)
        {

            _currentCategory = getProductOfCategory(name);

            _currentPage = 1;
            _selectedCategory = _currentCategory
                .Skip((_currentPage - 1) * _rowsPerPage)
                .Take(_rowsPerPage)
                .ToList();

            _totalItems = _currentCategory.Count;
            _totalPages = _currentCategory.Count / _rowsPerPage +
                (_currentCategory.Count % _rowsPerPage == 0 ? 0 : 1);

            currentPagingTextBlock = $"{_currentPage}/{_totalPages}";
        }

        public void clickNextPage(object parameter)
        {
            if (_currentPage < _totalPages)
            {
                _currentPage++;
                _selectedCategory = _currentCategory
                .Skip((_currentPage - 1) * _rowsPerPage)
                .Take(_rowsPerPage)
                .ToList();

                currentPagingTextBlock = $"{_currentPage}/{_totalPages}";
            }
        }

        public void clickPreviousPage(object parameter)
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                _selectedCategory = _currentCategory
                .Skip((_currentPage - 1) * _rowsPerPage)
                .Take(_rowsPerPage)
                .ToList();
                currentPagingTextBlock = $"{_currentPage}/{_totalPages}";
            }
        }
        public void clickFirstPage(object parameter)
        {
            _currentPage = 1;
            _selectedCategory = _currentCategory
            .Skip((_currentPage - 1) * _rowsPerPage)
            .Take(_rowsPerPage)
            .ToList();
            currentPagingTextBlock = $"{_currentPage}/{_totalPages}";
        }
        public void clickLastPage(object parameter)
        {
            _currentPage = _totalPages;
            _selectedCategory = _currentCategory
            .Skip((_currentPage - 1) * _rowsPerPage)
            .Take(_rowsPerPage)
            .ToList();
            currentPagingTextBlock = $"{_currentPage}/{_totalPages}";
        }

        public void search(object parameter)
        {

            _currentCategory = findProductByName(server, SearchKey);

            _currentPage = 1;
            _selectedCategory = _currentCategory
                .Skip((_currentPage - 1) * _rowsPerPage)
                .Take(_rowsPerPage)
                .ToList();

            _totalItems = _currentCategory.Count;
            _totalPages = _currentCategory.Count / _rowsPerPage +
                (_currentCategory.Count % _rowsPerPage == 0 ? 0 : 1);

            currentPagingTextBlock = $"{_currentPage}/{_totalPages}";
        }
        public void filter(String Key)
        {
            _currentCategory = filterProductByPrice(server, Key);

            _currentPage = 1;
            _selectedCategory = _currentCategory
                .Skip((_currentPage - 1) * _rowsPerPage)
                .Take(_rowsPerPage)
                .ToList();

            _totalItems = _currentCategory.Count;
            _totalPages = _currentCategory.Count / _rowsPerPage +
                (_currentCategory.Count % _rowsPerPage == 0 ? 0 : 1);

            currentPagingTextBlock = $"{_currentPage}/{_totalPages}";
        }
        public ICommand DoubleClickProductCommand
        {
            get
            {
                return doubleClickProductCommand ?? (doubleClickProductCommand = new RelayCommand(x =>
                {
                    DoStuff(x as Product);
                }));
            }
        }
        private void DoStuff(Product item)
        {
            var screen = new DetailProductView();
            var tmp = new DetailProductViewModel(item);
            screen.DataContext = tmp;
            screen.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            screen.ShowDialog();
        }
        private void addProduct(object parameter)
        {
            var screen = new AddProductView();
            screen.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            screen.ShowDialog();

            _currentCategory = getProductOfCategory(CategoryName);
            _selectedCategory = _currentCategory
                .Skip((_currentPage - 1) * _rowsPerPage)
                .Take(_rowsPerPage)
                .ToList();

            _totalItems = _currentCategory.Count;
            _totalPages = _currentCategory.Count / _rowsPerPage +
                (_currentCategory.Count % _rowsPerPage == 0 ? 0 : 1);

            currentPagingTextBlock = $"{_currentPage}/{_totalPages}";
        }
        private void editProduct(object parameter)
        {
            var screen = new AddProductView();
            screen.DataContext = new AddProductViewModel(_selectedProduct);
            screen.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            screen.ShowDialog();
            _currentCategory = getProductOfCategory(CategoryName);
            _selectedCategory = _currentCategory
                .Skip((_currentPage - 1) * _rowsPerPage)
                .Take(_rowsPerPage)
                .ToList();

            _totalItems = _currentCategory.Count;
            _totalPages = _currentCategory.Count / _rowsPerPage +
                (_currentCategory.Count % _rowsPerPage == 0 ? 0 : 1);

            currentPagingTextBlock = $"{_currentPage}/{_totalPages}";

        }
        private void deleteProduct(object parameter)
        {
            if(MessageBox.Show("Are you sure to delete this Product?", "delete",
                   MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes){
                deleteProduct(server);
                _currentCategory = getProductOfCategory(CategoryName);
                _selectedCategory = _currentCategory
                    .Skip((_currentPage - 1) * _rowsPerPage)
                    .Take(_rowsPerPage)
                    .ToList();

                _totalItems = _currentCategory.Count;
                _totalPages = _currentCategory.Count / _rowsPerPage +
                    (_currentCategory.Count % _rowsPerPage == 0 ? 0 : 1);

                currentPagingTextBlock = $"{_currentPage}/{_totalPages}";
            }

           

        }
        private void deleteProduct(string server)
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
            String sql = $"delete from dbo.Products where productID = {_selectedProduct.ProductId}";
            SqlCommand command = new SqlCommand(sql, ConnectDatabase);
           
            
            int result = command.ExecuteNonQuery();

            // Check Error
            if (result < 0)
                MessageBox.Show("Delete fail", "", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                MessageBox.Show("Delete successfully", "", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
   
}
