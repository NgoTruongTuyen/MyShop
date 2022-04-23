using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyShop.View
{
    /// <summary>
    /// Interaction logic for AddNewProductView.xaml
    /// </summary>
    public partial class AddNewProductView : UserControl
    {
        public AddNewProductView()
        {
            InitializeComponent();
        }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SearchList.Visibility=Visibility.Hidden;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchList.Visibility = Visibility.Visible;
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

            Debug.WriteLine("helloooooooooooooooooo");
            if ( CalcSubTotalButton.Command.CanExecute(null))
                CalcSubTotalButton.Command.Execute(null);
        }
    }
}
