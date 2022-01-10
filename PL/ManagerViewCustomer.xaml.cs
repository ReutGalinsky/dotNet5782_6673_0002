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
            Longitude.DataContext = customer.Location;
            Latitude.DataContext = customer.Location;
            listParcels.DataContext = bl.GetAllParcelsBy(x => x.Sender == i || x.Geter == i);
            ParcelGrid.DataContext = parcel;
        }
        private BLApi.IBL bl;
        private PO.ParcelPO parcel=new PO.ParcelPO();
        private BO.Customer customer;
        private string id;
        public event EventHandler updateList;
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void CovertParcelTOPO(PO.ParcelPO parcelPO, BO.ParcelOfList p)
        {
            parcelPO.IdNumber = p.IdNumber;
            parcelPO.Geter = p.Geter;
            parcelPO.Sender = p.Sender;
            parcelPO.ParcelState = p.ParcelState;
            parcelPO.Priority = p.Priority;
            parcelPO.Weight = p.Weight;
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.UpdatingDetailsOfCustomer(customer.IdNumber, customer.Name, customer.Phone);
                MessageBox.Show($"the base station number {customer.IdNumber} updated successfully!");
                updateList(sender, e);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void OnlyNumbers(object sender, KeyEventArgs e)
        {
            Tools.TextBox_OnlyNumbers_PreviewKeyDown(sender, e);
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void showTheParcel(object sender, RoutedEventArgs e)
        {

            ManagerViewParcel parcel = new ManagerViewParcel(bl,((BO.ParcelOfList)(listParcels.SelectedItem)).IdNumber);
            parcel.ShowDialog();
        }

        private void enableShow(object sender, SelectionChangedEventArgs e)
        {
            ParcelGrid.Visibility = Visibility.Visible;
            pickLabel.Visibility = Visibility.Collapsed;
            CovertParcelTOPO(parcel,  listParcels.SelectedItem as BO.ParcelOfList);
        }
    }
}
