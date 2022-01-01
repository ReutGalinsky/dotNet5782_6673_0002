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
using System.Collections.ObjectModel;

namespace PL.Pages
{
    /// <summary>
    /// Interaction logic for CustomerDelivery.xaml
    /// </summary>
    public partial class CustomerDelivery : Page
    {
        public CustomerDelivery(BLApi.IBL b, string i)
        {
            InitializeComponent();
            bl = b;
            id = i;
            parcels = new ObservableCollection<BO.ParcelOfList>();
            foreach (BO.ParcelOfList s in bl.GetAllParcelsBy(x => x.Geter == id))//create the source for the listView
                parcels.Add(s);
            Delivery.DataContext = parcels;
            update += updated;
        }
        public event EventHandler updateList;

        public EventHandler update;
        private BLApi.IBL bl;
        string id;
        private BO.ParcelOfList selected;//selected item that will be send to the new window
        private ObservableCollection<BO.ParcelOfList> parcels;
        private void selectionChange(object sender, SelectionChangedEventArgs e)
        {
            selected = (BO.ParcelOfList)Delivery.SelectedItem;
        }
        private void Action(object sender, MouseButtonEventArgs e)//event for double clicking on specific item 
        {
            if (selected != null)
            {
                CustomerShowParcel showParcel = new CustomerShowParcel(bl, selected.IdNumber);
                showParcel.Show();
            }
        }
        private void updated(object sender, EventArgs e)//the event that will update the details of the listView
        {
            parcels.Clear();
            foreach (BO.ParcelOfList s in bl.GetAllParcelsBy(x => x.Geter == id))//create the source for the listView
                parcels.Add(s);
        }
        private void deleteParcel(object sender, RoutedEventArgs e)
        {
            var dialogResult = MessageBox.Show($"are you sure?", "Delete Parcel", MessageBoxButton.YesNo,MessageBoxImage.Question);
            BO.ParcelOfList ParcelToDelte = ((sender as Button).DataContext) as BO.ParcelOfList;
            if (dialogResult == MessageBoxResult.Yes && ParcelToDelte.ParcelState == BO.ParcelState.Define)//לחשוב אם רוצים לכלול גם אופציה שנשלח רחפן אך לא אסף עדיין את החבילה
            {
                try
                {
                    bl.RemoveParcel(ParcelToDelte.IdNumber);
                    parcels.Clear();
                    foreach (var item in bl.GetAllParcelsBy(x => x.Geter == id))
                        parcels.Add(item);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "error", MessageBoxButton.OK,MessageBoxImage.Error);

                }
            }
        }

        private void WeightCheck(object sender, RoutedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Delivery.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("Weight");
            view.GroupDescriptions.Add(groupDescription);

        }

        private void unCheck(object sender, RoutedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Delivery.ItemsSource);
            view.GroupDescriptions.Clear();

        }
        private void StateCheck(object sender, RoutedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Delivery.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("ParcelState");
            view.GroupDescriptions.Add(groupDescription);

        }
    }


}



