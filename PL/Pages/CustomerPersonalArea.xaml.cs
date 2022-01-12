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
    public partial class CustomerPersonalArea : Page
    {
        public CustomerPersonalArea(BLApi.IBL b, string i)
        {
            InitializeComponent();
            bl = b;
            try
            {
                customer = bl.GetCustomer(i);
            }
            catch (Exception)
            { MessageBox.Show("Error in loading the user, please try again later");return; }
            Id.DataContext = customer;
            Phone.DataContext = customer;
            Name.DataContext = customer;
            Longitude.Text = customer.Location.Longitude.ToString();
            Latitude.Text = customer.Location.Latitude.ToString();
        }
        private BLApi.IBL bl;
        private BO.Customer customer;
        public EventHandler update;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
                {
                if (Name.Text == customer.Name && Phone.Text == customer.Phone)
                { MessageBox.Show("oops... it looks like you entered the same details");
                    edit.Visibility = Visibility.Visible;
                    Name.IsEnabled = false;
                    Phone.IsEnabled = false;
                    (sender as Button).Visibility = Visibility.Collapsed;
                    return;
                }
                bl.UpdatingDetailsOfCustomer(customer.IdNumber, Name.Text, Phone.Text);
                Name.IsEnabled = false;
                Phone.IsEnabled = false;
                customer.Name = Name.Text;
                customer.Phone = Phone.Text;
                edit.Visibility = Visibility.Visible;
                (sender as Button).Visibility = Visibility.Collapsed;
                MessageBox.Show($"the customer number {customer.IdNumber} updated successfully!");
            }
            catch (Exception)
            {
                MessageBox.Show($"the phone number {Phone.Text} is illegal. please enter again", "Phone Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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

