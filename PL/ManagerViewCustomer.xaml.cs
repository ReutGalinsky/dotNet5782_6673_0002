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
            Id.DataContext = customer;
            Phone.DataContext = customer;
            Name.DataContext = customer;
            Longitude.Text = customer.Location.Longitude.ToString();
            Latitude.Text = customer.Location.Latitude.ToString();
        }
        private BLApi.IBL bl;
        private BO.Customer customer;
        private string id;
        public event EventHandler updateList;


        private void closing(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void move(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();

        }
    }
}
