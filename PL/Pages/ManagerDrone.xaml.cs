using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Globalization;
using System.Windows.Data;


namespace PL.Pages
{
    /// <summary>
    /// Interaction logic for ManagerDrone.xaml
    /// </summary>
    public partial class ManagerDrone : Page
    {
        public ManagerDrone(BLApi.IBL b)
        {
            InitializeComponent();
            bl = b;
            DroneListView.DataContext = listDrones;
            update += updated;
        }
        public EventHandler update;
        private BLApi.IBL bl;
        private BO.DroneToList selected;
        private ObservableCollection<BO.DroneToList> listDrones = new ObservableCollection<BO.DroneToList>();
        PropertyGroupDescription groupState = new PropertyGroupDescription("State");
        PropertyGroupDescription groupWeight = new PropertyGroupDescription("MaxWeight");

        private void updated(object sender, EventArgs e)//the event that will update the details of the listView
        {
            reset();
        }
        private void selectionChange(object sender, SelectionChangedEventArgs e)
        {

            selected = (BO.DroneToList)(sender as ListView).SelectedItem;
            
        }
        private void Action(object sender, MouseButtonEventArgs e)//event for double clicking on specific item 
        {
            if (selected != null)
            {
                ManagerViewDrone showDrone = new ManagerViewDrone(bl, selected.IdNumber);
                showDrone.updateList += updated;
                showDrone.Show();
            }
        }
        private void deleteDrone(object sender, RoutedEventArgs e)
        {
            var dialogResult = MessageBox.Show($"are you sure?", "Delede Drone", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                try
                {
                    BO.DroneToList DroneToDelte = ((sender as Button).DataContext) as BO.DroneToList;
                    bl.RemoveDrone(DroneToDelte.IdNumber);
                    updated(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,"error",MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void reset()
        {
            DroneListView.Visibility = Visibility.Visible;
            listDrones.Clear();
            foreach (BO.DroneToList s in bl.GetDrones())//create the source for the liseView
                listDrones.Add(s);
        }
        private void parcel(object sender, RoutedEventArgs e)
        {
            BO.DroneToList parcelToView = ((sender as Button).DataContext) as BO.DroneToList;
                ManagerViewParcel showParcel = new ManagerViewParcel(bl, parcelToView.NumberOfParcel);
                showParcel.ShowDialog();
        }
        private void checkedState(object sender, RoutedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(DroneListView.ItemsSource);
            view.GroupDescriptions.Add(groupState);
        }

        private void uncheckedState(object sender, RoutedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(DroneListView.ItemsSource);
            view.GroupDescriptions.Remove(groupState);
        }

        private void WeightCheck(object sender, RoutedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(DroneListView.ItemsSource);
            view.GroupDescriptions.Add(groupWeight);
        }

        private void uncheckedWeight(object sender, RoutedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(DroneListView.ItemsSource);
            view.GroupDescriptions.Remove(groupWeight);
        }
    }
}


