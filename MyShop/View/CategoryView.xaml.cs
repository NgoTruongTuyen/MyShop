using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MyShop.View
{
    /// <summary>
    /// Interaction logic for CategoryView.xaml
    /// </summary>
    public partial class CategoryView : Window
    {
        public CategoryView()
        {
            InitializeComponent();
        }

        private void OnContextMenuOpen(object sender, RoutedEventArgs e)
        {
            ((ContextMenu)sender).DataContext = CategoryWindow.DataContext;
        }
    }
}
