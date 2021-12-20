using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
            foreach (BO.DroneToList s in bl.GetDrones())//create the source for the liseView
                listDrones.Add(s);
            DroneListView.DataContext = listDrones;
            var states = BO.DroneState.GetNames(typeof(BO.DroneState)).ToList();
            states.Insert(0, "All");
            State.DataContext = states;
            State.SelectedItem = "All";
            var weights = BO.WeightCategories.GetNames(typeof(BO.WeightCategories)).ToList();
            weights.Insert(0, "All");
            Weight.DataContext = weights;
            Weight.SelectedItem = "All";
        }

        Predicate<BO.DroneToList> stateCondition;
        Predicate<BO.DroneToList> weightCondition;
        

        private BLApi.IBL bl;
        private BO.DroneToList selected;
        private ObservableCollection<BO.DroneToList> listDrones = new ObservableCollection<BO.DroneToList>();
        private void updated(object sender, EventArgs e)//the event that will update the details of the listView
        {
            Weight.SelectedItem = "All";
            State.SelectedItem = "All";
            DroneListView.ItemsSource = bl.GetDrones();
            var t = Window.GetWindow(this);
            t.Opacity = 1;
        }

        private void addingDrone_Click(object sender, RoutedEventArgs e)
        {
            AddDrone drone = new AddDrone(bl);
            drone.updateList += updated;
            var t = Window.GetWindow(this);
            t.Opacity = 0.75;
            drone.ShowDialog();
        }

        private void ComboBox(object sender, SelectionChangedEventArgs e)
        {
            if(Weight.SelectedItem==null||State.SelectedItem==null)
            {
                return;
            }
            object item;
            Enum.TryParse(typeof(BO.WeightCategories), Weight.SelectedItem.ToString(), out item);
            object check;
            Enum.TryParse(typeof(BO.DroneState), State.SelectedItem.ToString(), out check);
            listDrones.Clear();
            DroneListView.ItemsSource = item switch
            {
                null => check switch
                {
                    null => bl.GetDrones(),
                    _ => bl.GetAllDronesBy(x => x.State == (BO.DroneState)check),
                },
                _ => check switch
                {
                    null => bl.GetAllDronesBy(x => x.MaxWeight == (BO.WeightCategories)item),
                    _ => bl.GetAllDronesBy(x => x.MaxWeight == (BO.WeightCategories)item && x.State == (BO.DroneState)check),
                },
            };
        }

        //private void ComboBox_State(object sender, SelectionChangedEventArgs e)
        //{
        //    if (Weight.SelectedItem == null || State.SelectedItem == null)
        //    {
        //        return;
        //    }
        //    object item;
        //    var b = Weight.SelectedItem;
        //    Enum.TryParse(typeof(BO.WeightCategories), Weight.SelectedItem.ToString(), out item);
        //    object check;
        //    Enum.TryParse(typeof(BO.DroneState), State.SelectedItem.ToString(), out check);
        //    listDrones.Clear();
        //    DroneListView.ItemsSource = item switch
        //    {
        //        null => check switch
        //        {
        //            null => bl.GetDrones(),
        //            _ => bl.GetAllDronesBy(x => x.State == (BO.DroneState)check),
        //        },
        //        _ => check switch
        //        {
        //            null => bl.GetAllDronesBy(x => x.MaxWeight == (BO.WeightCategories)item),
        //            _ => bl.GetAllDronesBy(x => x.MaxWeight == (BO.WeightCategories)item && x.State == (BO.DroneState)check),
        //        },
        //    };

        //}
        private void selectionChange(object sender, SelectionChangedEventArgs e)
        {
            selected = (BO.DroneToList)DroneListView.SelectedItem;
        }
        private void Action(object sender, MouseButtonEventArgs e)//event for double clicking on specific item 
        {
            ManagerViewDrone showDrone = new ManagerViewDrone(bl, selected.IdNumber);
            showDrone.updateList+=updated;
            showDrone.Show();
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
    }
}


