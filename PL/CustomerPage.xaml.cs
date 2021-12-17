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

namespace PL
{
    /// <summary>
    /// Interaction logic for CustomerPage.xaml
    /// </summary>
    public partial class CustomerPage : Window
    {
        public CustomerPage(BLApi.IBL b,string i)
        {
            InitializeComponent();
            bl = b;
            id = i;
        }
        BLApi.IBL bl;
        string id;
        private void ButtonParcel(object sender, RoutedEventArgs e)
        {
            Customer.Content = new Pages.CustomerParcel(bl,id);
        }
private void ButtonPersonalArea(object sender, RoutedEventArgs e)
        {
            Customer.Content = new Pages.CustomerPersonalArea(bl,id);
        }
        private void AddLineButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonDelivery(object sender, RoutedEventArgs e)
        {
            Customer.Content = new Pages.CustomerDelivery(bl,id);
        }     
    }
}
