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
            State.ItemsSource = Enum.GetValues(typeof(BO.DroneState));
            Weight.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));
      
        }
        private BLApi.IBL bl;
private BO.DroneToList selected;
    private ObservableCollection<BO.DroneToList> listDrones = new ObservableCollection<BO.DroneToList>();
    private void updated(object sender, EventArgs e)//the event that will update the details of the listView
    {
        listDrones.Clear();
        foreach (var item in bl.GetDrones())
            listDrones.Add(item);
    }

    private void Action(object sender, MouseButtonEventArgs e)//event for double clicking on specific item 
        {
            //AddDrone action = new AddDrone(selected, bl);
            //action.updateList += updated;
            //action.Show();
        }

        private void addingDrone_Click(object sender, RoutedEventArgs e)
        {
            //AddDrone drone = new AddDrone(bl);
            //drone.updateList += updated;
            //drone.ShowDialog();
        }

        private void ComboBox_Weight(object sender, SelectionChangedEventArgs e)
        {
            var item = Weight.SelectedItem;
            var check = State.SelectedItem;
            if (check == null)
            {
                DroneListView.ItemsSource = item switch
                {
                    BO.WeightCategories.Heavy => bl.GetAllDronesBy(x => x.MaxWeight == BO.WeightCategories.Heavy),
                    BO.WeightCategories.Light => bl.GetAllDronesBy(x => x.MaxWeight == BO.WeightCategories.Light),
                    BO.WeightCategories.Middle => bl.GetAllDronesBy(x => x.MaxWeight == BO.WeightCategories.Middle),
                    _ => bl.GetDrones(),

                };
            }
            else
            {
                DroneListView.ItemsSource = item switch
                {
                    BO.WeightCategories.Heavy => bl.GetAllDronesBy(x => ((x.MaxWeight == BO.WeightCategories.Heavy) && (x.State == (BO.DroneState)check))),
                    BO.WeightCategories.Light => bl.GetAllDronesBy(x => ((x.MaxWeight == BO.WeightCategories.Light) && (x.State == (BO.DroneState)check))),
                    BO.WeightCategories.Middle => bl.GetAllDronesBy(x => ((x.MaxWeight == BO.WeightCategories.Middle) && (x.State == (BO.DroneState)check))),
                    _ => bl.GetDrones(),
                };
            }
        }

        private void ComboBox_State(object sender, SelectionChangedEventArgs e)
        {
            var item = State.SelectedItem;
            var check = Weight.SelectedItem;
            if (check == null)
            {
                DroneListView.ItemsSource = item switch
                {
                    BO.DroneState.Available => bl.GetAllDronesBy(x => x.State == BO.DroneState.Available),
                    BO.DroneState.maintaince => bl.GetAllDronesBy(x => x.State == BO.DroneState.maintaince),
                    BO.DroneState.shipping => bl.GetAllDronesBy(x => x.State == BO.DroneState.shipping),
                    _ => bl.GetDrones(),
                };
            }
            else
            {
                DroneListView.ItemsSource = item switch
                {
                    BO.DroneState.Available => bl.GetAllDronesBy(x => ((x.State == BO.DroneState.Available) && (x.MaxWeight == (BO.WeightCategories)check))),
                    BO.DroneState.maintaince => bl.GetAllDronesBy(x => ((x.State == BO.DroneState.maintaince) && (x.MaxWeight == (BO.WeightCategories)check))),
                    BO.DroneState.shipping => bl.GetAllDronesBy(x => ((x.State == BO.DroneState.shipping) && (x.MaxWeight == (BO.WeightCategories)check))),
                    _ => bl.GetDrones(),
                };
            }

        }

        private void reset_Click(object sender, RoutedEventArgs e)
        {

                listDrones.Clear();
                foreach (var item in bl.GetDrones())
                    listDrones.Add(item);
                State.SelectedItem = null;
                Weight.SelectedItem = null;

        }

        private void selectionChange(object sender, SelectionChangedEventArgs e)
        {
            selected = (BO.DroneToList)DroneListView.SelectedItem;
        }
    }
}


