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
    /// Interaction logic for addParcel.xaml
    /// </summary>
    public partial class addParcel : Window
    {
        public addParcel(BLApi.IBL b,string i)
        {
            InitializeComponent();
            bl = b;
            id = i;
            Weight.ItemsSource = Enum.GetValues( typeof(BO.WeightCategories));
            Prioprity.ItemsSource = Enum.GetValues(typeof(BO.Priorities));
        }
        private BLApi.IBL bl;
        private string id;
        public event EventHandler updateList;

        private void adButton(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.ParcelOfList parcel = new BO.ParcelOfList() { Priority=(BO.Priorities)Prioprity.SelectedItem, Weight= (BO.WeightCategories)Weight.SelectedItem , Geter=IdGeter.Text, Sender=id};//?האם ההמרה זו הייתה הבעיה
                bl.AddParcelToDelivery(parcel);
                this.Close();
            }
            catch(Exception ex)//חריגה לא מדוייקת
            {
                MessageBox.Show("the geter customer is not existing in the system, please enter again correct details","error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.ParcelOfList parcel=new BO.ParcelOfList() { Geter=id=IdGeter.Text, Priority=(BO.Priorities)Prioprity.SelectedItem, Weight= (BO.WeightCategories)Weight.SelectedItem, Sender=id };
                bl.AddParcelToDelivery(parcel);
                updateList(sender, e);
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("error");
            }
        }
    }
}
