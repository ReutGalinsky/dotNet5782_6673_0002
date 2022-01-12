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
    public partial class CustomerAddDelivery : Page
    {
        public CustomerAddDelivery(BLApi.IBL b, string i)
        {
            InitializeComponent();
            bl = b;
            id = i;
            Weight.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));
            Priority.ItemsSource = Enum.GetValues(typeof(BO.Priorities));
            try
            {
                Geters.DataContext = bl.GetAllCustomersBy(x => x.IdNumber != i);
            }
            catch(Exception)
            {
                MessageBox.Show("Error in the customer's loading, please try again later");
            }
        }
        private BLApi.IBL bl;
        private string id;
        public event EventHandler updateList;
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.ParcelOfList parcel = new BO.ParcelOfList() { Priority = (BO.Priorities)Priority.SelectedItem, Weight = (BO.WeightCategories)Weight.SelectedItem, Sender = id };
                parcel.Geter = ((BO.CustomerToList)(Geters.SelectedItem)).IdNumber;
                bl.AddParcelToDelivery(parcel);
                updateList(sender, e);
                MessageBox.Show("Your parcel been added successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void focus(object sender, SelectionChangedEventArgs e)
        {
            if (Geters.SelectedItem == null || Priority.SelectedItem == null || Weight.SelectedItem == null)
                ADD.IsEnabled = false;
            else
                ADD.IsEnabled = true;
        }
    }
}

    
