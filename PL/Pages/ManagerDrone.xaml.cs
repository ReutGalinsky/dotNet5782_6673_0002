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
            AvilibleList.DataContext = availibleLists;
            MaintanceList.DataContext = maintanceLists;
            ShippingList.DataContext = shipLists;
            //State.SelectedItem=State.Items[0];
            var weights = BO.WeightCategories.GetNames(typeof(BO.WeightCategories)).ToList();
            weights.Insert(0, "All");
            Weight.DataContext = weights;
            Weight.SelectedItem = "All";
            update += updated;
        }

        public EventHandler update;
        private BLApi.IBL bl;
        private BO.DroneToList selected;
        private ObservableCollection<BO.DroneToList> listDrones = new ObservableCollection<BO.DroneToList>();
        private ObservableCollection<BO.DroneToList> availibleLists = new ObservableCollection<BO.DroneToList>();
        private ObservableCollection<BO.DroneToList> maintanceLists = new ObservableCollection<BO.DroneToList>();
        private ObservableCollection<BO.DroneToList> shipLists = new ObservableCollection<BO.DroneToList>();
        private void updated(object sender, EventArgs e)//the event that will update the details of the listView
        {
            Weight.SelectedItem = "All";
            //State.SelectedItem=State.Items[0];
            reset();
        }
        private void addingDrone_Click(object sender, RoutedEventArgs e)
        {
            AddDrone drone = new AddDrone(bl);
            drone.updateList += updated;
            drone.ShowDialog();
        }
        private void ComboBox(object sender, SelectionChangedEventArgs e)
        {
            //if(Weight.SelectedItem==null||State.SelectedItem==null)
            //{
            //    return;
            //}
            //object item;
            //Enum.TryParse(typeof(BO.WeightCategories), Weight.SelectedItem.ToString(), out item);
            //listDrones.Clear();
            //switch(item)
            //{
            //    case null:
            //        update(sender, e);
            //        break;
            //    default:
            //        foreach (var obj in bl.GetAllDronesBy(x => x.MaxWeight == (BO.WeightCategories)item))
            //            listDrones.Add(obj);
            //        break;
            //}
           
        }
        private void selectionChange(object sender, SelectionChangedEventArgs e)
        {

            selected = (BO.DroneToList)(sender as ListView).SelectedItem;
                //selected = (BO.DroneToList)DroneListView.SelectedItem;
            
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
        private void groupState(object sender, SelectionChangedEventArgs e)
        {
            //if (Weight.SelectedItem == null || State.SelectedItem == null)
            //{
            //    return;
            //}
            //var comboBoxItem = (sender as ComboBox).Items[(sender as ComboBox).SelectedIndex] as ComboBoxItem;
            //if (comboBoxItem.Content.ToString()!="Show All")
            //{
            //    Weight.Visibility = Visibility.Collapsed;
            //    DroneListView.Visibility = Visibility.Collapsed;
            //    AvilibleList.Visibility = Visibility.Visible;
            //    MaintanceList.Visibility = Visibility.Visible;
            //    ShippingList.Visibility = Visibility.Visible;
            //    availibleLabel.Visibility = Visibility.Visible;
            //    maintanceLabel.Visibility = Visibility.Visible;
            //    shipLabel.Visibility = Visibility.Visible;

            //    var group = from item in bl.GetDrones()
            //                group item by item.State;
            //    foreach (var item in group)
            //    {
            //        switch (item.Key)
            //        {
            //            case BO.DroneState.Available:
            //                foreach (var g in item)
            //                    availibleLists.Add(g);
            //                break;
            //            case BO.DroneState.maintaince:
            //                foreach (var g in item)
            //                    maintanceLists.Add(g);
            //                break;
            //            case BO.DroneState.shipping:
            //                foreach (var g in item)
            //                    shipLists.Add(g);
            //                break;
            //        }
            //    }
            //}
            //else
            //{
            //    reset();
            //}
        }
        private void reset()
        {
            Weight.Visibility = Visibility.Visible;
            availibleLabel.Visibility = Visibility.Collapsed;
            maintanceLabel.Visibility = Visibility.Collapsed;
            shipLabel.Visibility = Visibility.Collapsed;
            DroneListView.Visibility = Visibility.Visible;
            AvilibleList.Visibility = Visibility.Collapsed;
            MaintanceList.Visibility = Visibility.Collapsed;
            ShippingList.Visibility = Visibility.Collapsed;
            listDrones.Clear();
            availibleLists.Clear();
            maintanceLists.Clear();
            shipLists.Clear();
            foreach (BO.DroneToList s in bl.GetDrones())//create the source for the liseView
                listDrones.Add(s);
        }
        private void parcel(object sender, RoutedEventArgs e)
        {
            BO.DroneToList parcelToView = ((sender as Button).DataContext) as BO.DroneToList;
                ManagerViewParcel showParcel = new ManagerViewParcel(bl, parcelToView.NumberOfParcel);
                showParcel.ShowDialog();
        }

        private void doubleClick(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void checkedState(object sender, RoutedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(DroneListView.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("State");
            view.GroupDescriptions.Add(groupDescription);

        }

        private void uncheckedState(object sender, RoutedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(DroneListView.ItemsSource);
            view.GroupDescriptions.Clear();

        }
    }
}


