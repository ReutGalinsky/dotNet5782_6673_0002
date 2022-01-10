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
    public partial class CustomerParcel : Page
    {
        public CustomerParcel(BLApi.IBL b, string i)
        {
            InitializeComponent();
            bl = b;
            id = i;
            foreach (BO.ParcelOfList s in bl.GetAllParcelsBy(x => x.Sender == id))
                listParcels.Add(s);
            ParcelListView.DataContext = listParcels;
            update += updated;
        }

        public EventHandler update;
        public event EventHandler updateList;

        private BLApi.IBL bl;
        string id;
        private BO.ParcelOfList selected;
        private ObservableCollection<BO.ParcelOfList> listParcels = new ObservableCollection<BO.ParcelOfList>();
        PropertyGroupDescription groupState = new PropertyGroupDescription("ParcelState");
        PropertyGroupDescription groupWeight = new PropertyGroupDescription("Weight");

        private void selectionChange(object sender, SelectionChangedEventArgs e)
        {
            selected = (BO.ParcelOfList)ParcelListView.SelectedItem;
        }
        private void Action(object sender, MouseButtonEventArgs e) 
        {
            if (selected != null)
            {
                CustomerShowParcel showParcel = new CustomerShowParcel(bl, selected.IdNumber);
                showParcel.Show();
            }
        }
        private void updated(object sender, EventArgs e)
        {            listParcels.Clear();
            foreach (BO.ParcelOfList s in bl.GetAllParcelsBy(x => x.Sender == id))//create the source for the liseView
                listParcels.Add(s);
        }
        private void deleteParcel(object sender, RoutedEventArgs e)
        {
            var dialogResult = MessageBox.Show($"are you sure?", "Delete Parcel", MessageBoxButton.YesNo, MessageBoxImage.Question);
            BO.ParcelOfList ParcelToDelte = ((sender as Button).DataContext) as BO.ParcelOfList;
            if (dialogResult == MessageBoxResult.Yes && ParcelToDelte.ParcelState == BO.ParcelState.Define)//לחשוב אם רוצים לכלול גם אופציה שנשלח רחפן אך לא אסף עדיין את החבילה
            {
                try
                {
                    bl.RemoveParcel(ParcelToDelte.IdNumber);
                    listParcels.Clear();
                    foreach (var item in bl.GetAllParcelsBy(x => x.Sender == id))
                        listParcels.Add(item);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }
        private void WeightCheck(object sender, RoutedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ParcelListView.ItemsSource);
            view.GroupDescriptions.Add(groupWeight);
        }
        private void unCheckState(object sender, RoutedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ParcelListView.ItemsSource);
            view.GroupDescriptions.Remove(groupState);

        }
        private void StateCheck(object sender, RoutedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ParcelListView.ItemsSource);
            view.GroupDescriptions.Add(groupState);
        }
        private void unCheckWeight(object sender, RoutedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ParcelListView.ItemsSource);
            view.GroupDescriptions.Remove(groupWeight);
        }
    }
}      

 
 