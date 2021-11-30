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
using BLAPI;
namespace UI
{
    /// <summary>
    /// Interaction logic for UpdateWindow.xaml
    /// </summary>
    public partial class UpdateWindow : Window
    {
        public UpdateWindow(IBL b,string i)
        {
            InitializeComponent();
            bl = b;
            customer = bl.GetCustomer(i);
            Id.DataContext = customer;
            Phone.DataContext = customer;
            Name.DataContext = customer;
            Latitude.DataContext = customer;
            Longitude.DataContext = customer;

        }
        private IBL bl;
        private BO.Customer customer;

        private void updateButton(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.UpdatingDetailsOfCustomer(customer.IdNumber,Name.Text,Phone.Text);
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show($"the phone number {Phone.Text} is illegal. please enter again", "Phone Error",MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
