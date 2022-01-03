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
    /// Interaction logic for CustomerAddDelivery.xaml
    /// </summary>
    public partial class CustomerAddDelivery : Page
    {
        public CustomerAddDelivery(BLApi.IBL b, string i)
        {
            InitializeComponent();
            bl = b;
            id = i;
            Weight.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));
            Priority.ItemsSource = Enum.GetValues(typeof(BO.Priorities));
            Geters.DataContext = bl.GetAllCustomersBy(x=>x.IdNumber!=i);
        }
        private BLApi.IBL bl;
        private string id;
        public event EventHandler updateList;


        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.ParcelOfList parcel = new BO.ParcelOfList() { Priority = (BO.Priorities)Priority.SelectedItem, Weight = (BO.WeightCategories)Weight.SelectedItem, Sender = id };
                parcel.Geter = ((BO.CustomerToList)(Geters.SelectedItem)).IdNumber;//?האם ההמרה זו הייתה הבעיה
                bl.AddParcelToDelivery(parcel);
                updateList(sender, e);
            }
            catch (Exception ex)//חריגה לא מדוייקת
            {
                MessageBox.Show("the geter customer is not existing in the system, please enter again correct details", "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void focus_Click(object sender, RoutedEventArgs e)
        {
            if (Longitude.Text == "min 34.2 max 35.8")
                Longitude.Text = "";
           
        }
        private void focus_Click1(object sender, RoutedEventArgs e)
        {
            if (Latitude.Text == "min 32.2 max 34.8")
                Latitude.Text = "";

        }
    }
}

    
