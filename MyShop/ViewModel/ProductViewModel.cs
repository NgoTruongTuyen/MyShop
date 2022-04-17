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

        public bool Isloaded = false;

        private string server = @"LAPTOP-R4MFGNUI\SQL";
        private ObservableCollection<Product> _currentCategory { get; set; }
        public List<Product> _selectedCategory { get; set; }

        public String currentPagingTextBlock { get; set; }
        private int _totalItems { get; set; }
        private int _currentPage { get; set; }
        private int _totalPages { get; set; }
        private string searchKey;
        public string SearchKey
        {
            get { return searchKey; }
            set
            {
                if (!string.Equals(searchKey, value))
                {
                    searchKey = value;
                    //RaisePropertyChanged(); // Method to raise the PropertyChanged event in your BaseViewModel class...
                }
            }
        }
        int _rowsPerPage = 10;
        public RelayCommand nextPageCommand { get; }
        public RelayCommand previousPageCommand { get; }
        public RelayCommand firstPageCommand { get; }
        public RelayCommand lastPageCommand { get; }
        public RelayCommand allTabCommand { get; }
        public RelayCommand vivoTabCommand { get; }
        public RelayCommand appleTabCommand { get; }
        public RelayCommand samsungTabCommand { get; }
        public RelayCommand xiaomiTabCommand { get; }
        public RelayCommand realmeTabCommand { get; }
        public RelayCommand oppoTabCommand { get; }
        public RelayCommand searchCommand { get; }

        private ICommand doubleClickProductCommand;
        public ProductViewModel()
        {
            _categories = getDataFromDataBase(server);
            paging("All");
            nextPageCommand = new RelayCommand(clickNextPage, null);
            previousPageCommand = new RelayCommand(clickPreviousPage, null);
            firstPageCommand = new RelayCommand(clickFirstPage, null);
            lastPageCommand = new RelayCommand(clickLastPage, null);

            allTabCommand = new RelayCommand(clickAllTab, null);
            vivoTabCommand = new RelayCommand(clickVivoTab, null);
            appleTabCommand = new RelayCommand(clickAppleTab, null);
            samsungTabCommand = new RelayCommand(clickSamsungTab, null);
            xiaomiTabCommand = new RelayCommand(clickXiaomiTab, null);
            realmeTabCommand = new RelayCommand(clickRealmeTab, null);
            oppoTabCommand = new RelayCommand(clickOppoTab, null);
            searchCommand = new RelayCommand(search, null);
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
            var sql = "select * from Product";
            var command = new SqlCommand(sql, ConnectDatabase);

            var reader = command.ExecuteReader();

            ObservableCollection<Category> categories = new ObservableCollection<Category>();
            Category categoryAll = new Category()
            {
                Brand = "All",
                Products = new ObservableCollection<Product>()
            };

            Category categoryApple = new Category()
            {
                Brand = "Apple",
                Products = new ObservableCollection<Product>()
            };
            Category categoryVivo = new Category()
            {
                Brand = "Vivo",
                Products = new ObservableCollection<Product>()
            };
            Category categorySamsung = new Category()
            {
                Brand = "Samsung",
                Products = new ObservableCollection<Product>()
            };
            Category categoryLinovo = new Category()
            {
                Brand = "Xiaomi",
                Products = new ObservableCollection<Product>()
            };
            Category categoryOPPO = new Category()
            {
                Brand = "OPPO",
                Products = new ObservableCollection<Product>()
            };
            Category categoryRealme = new Category()
            {
                Brand = "Realme",
                Products = new ObservableCollection<Product>()
            };
            while (reader.Read())
            {
                var id = (int)reader["productID"];
                var name = (string)reader["productName"];
                var brand = (string)reader["brand"];
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
                Product tmp = new Product(id.ToString(), name, imageURL, costPrice, sellingPrice, screenSize, os, color, memory, storage, battery, releaseDate, stock, brand, viewCounts, buyCounts);
                if (brand == "Apple")
                {
                    categoryApple.Products.Add(tmp);
                }
                else if (brand == "Vivo")
                {
                    categoryVivo.Products.Add(tmp);
                }
                else if (brand == "Samsung")
                {
                    categorySamsung.Products.Add(tmp);
                }
                else if (brand == "Realme")
                {
                    categoryRealme.Products.Add(tmp);
                }
                else if (brand == "OPPO")
                {
                    categoryOPPO.Products.Add(tmp);
                }
                else if (brand == "Xiaomi")
                {
                    categoryLinovo.Products.Add(tmp);
                }
                categoryAll.Products.Add(tmp);
            }
            reader.Close();

            categories.Add(categoryAll);
            categories.Add(categoryApple);
            categories.Add(categoryVivo);
            categories.Add(categorySamsung);
            categories.Add(categoryRealme);
            categories.Add(categoryOPPO);
            categories.Add(categoryLinovo);
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

            // Sau khi kết nối thành công
            var sql = $"select * from Product where productName like '%{productName}%'";
            var command = new SqlCommand(sql, ConnectDatabase);

            var reader = command.ExecuteReader();

            ObservableCollection<Product> result = new ObservableCollection<Product>();

            while (reader.Read())
            {
                var id = (int)reader["productID"];
                var name = (string)reader["productName"];
                var brand = (string)reader["brand"];
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
                Product tmp = new Product(id.ToString(), name, imageURL, costPrice, sellingPrice, screenSize, os, color, memory, storage, battery, releaseDate, stock, brand, viewCounts, buyCounts);
               
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
        public void clickAllTab(object parameter)
        {
            paging("All");
        }
        public void clickVivoTab(object parameter)
        {
            paging("Vivo");
        }
        public void clickAppleTab(object parameter)
        {
            paging("Apple");
        }
        public void clickSamsungTab(object parameter)
        {
            paging("Samsung");
        }
        public void clickOppoTab(object parameter)
        {
            paging("OPPO");
        }
        public void clickXiaomiTab(object parameter)
        {
            paging("Xiaomi");
        }
        public void clickRealmeTab(object parameter)
        {
            paging("Realme");
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
            MessageBox.Show(item.ProductName + " element clicked");
            //var screen = new AddProductView();

            //screen.Show;
        }
    }

}
