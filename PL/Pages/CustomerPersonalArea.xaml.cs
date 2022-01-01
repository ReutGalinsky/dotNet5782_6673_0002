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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL.Pages
{
    /// <summary>
    /// Interaction logic for CustomerPersonalArea.xaml
    /// </summary>
    public partial class CustomerPersonalArea : Page
    {
        public CustomerPersonalArea(BLApi.IBL b, string i)
        {
            InitializeComponent();
            bl = b;
            customer = bl.GetCustomer(i);
            Id.DataContext = customer;
            Phone.DataContext = customer;
            Name.DataContext = customer;
            Longitude.Text = customer.Location.Longitude.ToString();
            Latitude.Text = customer.Location.Latitude.ToString();
        }
        private BLApi.IBL bl;
        private BO.Customer customer;
        public EventHandler update;
        public event EventHandler updateList;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.UpdatingDetailsOfCustomer(customer.IdNumber, Name.Text, Phone.Text);
                Name.IsEnabled = false;
                Phone.IsEnabled = false;
                edit.Visibility = Visibility.Visible;
                (sender as Button).Visibility = Visibility.Collapsed;
                MessageBox.Show($"the customer number {customer.IdNumber} updated successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"the phone number {Phone.Text} is illegal. please enter again", "Phone Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Latitude_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            Name.IsEnabled = true;
            Phone.IsEnabled = true;
            updatingButton.Visibility = Visibility.Visible;
            (sender as Button).Visibility = Visibility.Collapsed;
        }
    }
}

