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
    /// Interaction logic for ManagerAddCustomer.xaml
    /// </summary>
    public partial class ManagerAddCustomer : Page
    {
        public ManagerAddCustomer(BLApi.IBL b)
        {
            InitializeComponent();
            bl = b;
            Id.DataContext = customer;
            Name.DataContext = customer;
            Phone.DataContext = customer;
        }
        BLApi.IBL bl;
        private BO.Customer customer = new BO.Customer();
        public event EventHandler updateList;



        private void Onlynumbers(object sender, KeyEventArgs e)
        {
            Tools.TextBox_OnlyNumbers_PreviewKeyDown(sender, e);
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double temp;
                if (double.TryParse(Longitude.Text, out temp) == false)
                {
                    MessageBox.Show("Longitude suppose to be double");
                }
                customer.Location = new BO.Location();
                customer.Location.Longitude = temp;
                if (double.TryParse(Latitude.Text, out temp) == false)
                {
                    MessageBox.Show("Latitude suppose to be double");
                }
                customer.Location.Latitude = temp;
                bl.AddCustomer(customer);
                Random rand = new Random();
                int password = rand.Next();
                bl.AddUser(new BO.User() { isManager = false, UserName = customer.IdNumber, UserPassword = password.ToString() });
                MessageBox.Show($"the customer password is: {password.ToString()}");
                updateList(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Focus(object sender, TextChangedEventArgs e)
        {
            if (customer.IdNumber == "" || Longitude.Text == "" || Latitude.Text == "" || customer.Name == "" || customer.Phone == "")
            {
                ADD.IsEnabled = false;
            }
            else
                ADD.IsEnabled = true;
        }
    }
}


