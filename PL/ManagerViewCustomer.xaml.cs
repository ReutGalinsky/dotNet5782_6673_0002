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
    /// Interaction logic for ManagerViewCustomer.xaml
    /// </summary>
    public partial class ManagerViewCustomer : Window
    {
        public ManagerViewCustomer(BLApi.IBL b, string i)
        {
        
            InitializeComponent();
            bl = b;
            id = i;
            customer = bl.GetCustomer(id);
            Id.Text = customer.IdNumber;
            Name.Text = customer.Name;
            Phone.Text = customer.Phone;
            Latitude.DataContext = customer;
            Longitude.DataContext = customer;
        }
        private BLApi.IBL bl;
        private string id;
        private BO.Customer customer;

        private void closing(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

       
    }
}
