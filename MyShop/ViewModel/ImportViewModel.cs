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

namespace MyShop.ViewModel
{
    public class ImportViewModel: BaseViewModel
    {
        private string _folderName;
        public string FileName { get; set; }
        public RelayCommand importCommand { get; }
        public RelayCommand submitCommand { get; }

        private ObservableCollection<Category> _listProduct;

        public ImportViewModel()
        {

            FileName = "+ Import File";
            importCommand = new RelayCommand(addFile, null);
            submitCommand = new RelayCommand(submitFile, null);
            _listProduct = new ObservableCollection<Category>();
            

        }

        private void submitFile(object parameter)
        {
            // update database
                // . . .
            // moving image to store folder
            movingImg();
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
                    products.Add(new Product(
                    )
                    {
                        ProductName = cell.StringValue,
                        ImageURL = tab.Cells[$"C{row}"].StringValue,
                        CostPrice = tab.Cells[$"D{row}"].IntValue,
                        ScreenSize = float.Parse( tab.Cells[$"E{row}"].StringValue),
                        OS = tab.Cells[$"F{row}"].StringValue,

                        Color = tab.Cells[$"G{row}"].StringValue,

                        Memory = tab.Cells[$"H{row}"].IntValue,
                        Storage = tab.Cells[$"I{row}"].IntValue,
                        Battery = tab.Cells[$"J{row}"].IntValue,
                        ReleaseDate = tab.Cells[$"K{row}"].DateTimeValue,
                        Stock =tab.Cells[$"L{row}"].IntValue,
                        Brand = worksheet.Brand,

                    }
                        
                    );
                    row++;
                    cell = tab.Cells[$"{column}{row}"];
                    
                }


                _listProduct.Add(new Category()
                {
                    Brand = tab.Name,
                    Products = products,
                });

            }
        }

        
    }
}
