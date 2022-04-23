using Microsoft.Win32;
using MyShop.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspose.Cells;
using MyShop.Model;
using System.Collections.ObjectModel;
using System.IO;
using MyShop.DAO;

namespace MyShop.ViewModel
{
    public class ImportViewModel: BaseViewModel
    {
        private string _folderName;
        public string FileName { get; set; }
        public RelayCommand importCommand { get; }
        public RelayCommand submitCommand { get; }
        public RelayCommand CancelCommand { get; }

        public bool IsSubmit { get; set; }
        public bool IsCancel { get; set; }

        private ObservableCollection<Category> _listProduct;

        ProductDAO _productDAO = new ProductDAO();
        BrandDAO _brandDAO = new BrandDAO();

        public ImportViewModel()
        {

            FileName = "+ Import File";
            importCommand = new RelayCommand(addFile, null);
            submitCommand = new RelayCommand(submitFile, null);
            CancelCommand = new RelayCommand(cancelCommand, null);

            _listProduct = new ObservableCollection<Category>();
            IsSubmit = false;
            

        }


        private bool isExist(string name)
        {

            ObservableCollection<Brand> brand = _brandDAO.getAll();

            foreach(var b in brand)
            {
                if(b.Name == name)
                {
                    return true;
                }
            }

            return false;
        }


        private void submitFile(object parameter)
        {

            FileName = "+ Import File";
             IsSubmit = false;
            IsCancel = false;

            // update database
            // . . .
            // insert brand

            foreach(var data in _listProduct)
            {
                if (!isExist(data.Brand))
                {
                    _brandDAO.insertOne(data.Brand);
                }
                var id = _brandDAO.getId(data.Brand);
                Debug.WriteLine(id);
                
                foreach(var entry in data.Products)
                {
                    _productDAO.insertOne(entry, id);
                }
                
                
            }



          
            // moving image to store folder
            movingImg();
        }

        private void cancelCommand(object x)
        {
            
            FileName = "+ Import File";
            IsSubmit = false;
            IsCancel = false;
        }

        private void addFile(object parameter)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Multiselect = true;
            openFile.Filter = "Excel files (*.xlsx)|*.xlsx|All files(*.*)|*.*";
            openFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if(openFile.ShowDialog() == true)
            {
                FileName = Path.GetFileName(openFile.FileName);
                Debug.WriteLine(openFile.FileName);
                _folderName = Path.GetDirectoryName(openFile.FileName);
                Debug.WriteLine(Path.GetDirectoryName(openFile.FileName));
                Debug.WriteLine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName);

                /*
                 * MyShop\MyShop
                 */
                getProductData(openFile.FileName);
                IsSubmit = true;
                IsCancel = true;
            }
          
        }

        private void movingImg()
        {
            string rootFolder = @$"{_folderName}\img";
            string destinationFolder = @$"{Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName}\Image\store";
            string[] fileList = Directory.GetFiles(rootFolder);
            foreach (string file in fileList)
            {
                Debug.WriteLine(file);
                string fileName = Path.GetFileName(file);
                string fileToMove = rootFolder +@"\"+ fileName;
                string moveTo = destinationFolder+ @"\" + fileName;

                Debug.WriteLine(fileToMove);
                Debug.WriteLine(moveTo);

                File.Move(fileToMove, moveTo);
            }
        }

        private void getProductData(string fileName)
        {
            var workbook = new Workbook(fileName);
            var tabs = workbook.Worksheets;

            foreach(var tab in tabs)
            {
                Debug.WriteLine(tab.Name);
                Category worksheet = new Category();
                worksheet.Brand = tab.Name;

                ObservableCollection<Product> products = new ObservableCollection<Product>();

                var column = 'B';
                var row = '6';
                var cell = tab.Cells[$"{column}{row}"];
                
                while(cell.Value != null)
                {

                    Debug.WriteLine(cell.StringValue);
                    Debug.WriteLine(tab.Cells[$"C{row}"].StringValue); 
                    products.Add(new Product(
                    )
                    {
                        ProductName = cell.StringValue,
                        ImageURL = tab.Cells[$"C{row}"].StringValue,
                        CostPrice = tab.Cells[$"D{row}"].IntValue,
                        ScreenSize = float.Parse(tab.Cells[$"E{row}"].StringValue),
                        OS = tab.Cells[$"F{row}"].StringValue,

                        Color = tab.Cells[$"G{row}"].StringValue,

                        Memory = tab.Cells[$"H{row}"].IntValue,
                        Storage = tab.Cells[$"I{row}"].IntValue,
                        Battery = tab.Cells[$"J{row}"].IntValue,
                        ReleaseDate = tab.Cells[$"K{row}"].DateTimeValue,
                        Stock = tab.Cells[$"L{row}"].IntValue,
                        // Brand = worksheet.Brand,

                    }

                    ) ;
                    Debug.WriteLine("HERE");
                    row++;
                    cell = tab.Cells[$"{column}{row}"];
                    
                }


                _listProduct.Add(new Category()
                {
                    Brand = tab.Name,
                    Products = products,
                });

                foreach(var x in _listProduct)
                {
                    Debug.WriteLine(x.Brand);
                    foreach(var y in x.Products)
                    {
                        Debug.WriteLine(y.ProductName);
                            

                    }
                }

            }
        }

        
    }
}
