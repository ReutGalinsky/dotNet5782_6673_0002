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
using System.Collections.ObjectModel;


namespace PL
{
    /// <summary>
    /// Interaction logic for ListDrones.xaml
    /// </summary>
    public partial class ListDrones : Window
    {
        public ListDrones(BLApi.IBL i)//ctor
        {
            InitializeComponent();
            bl = i;
            foreach (BO.DroneToList s in bl.GetDrones())//create the source for the liseView
                listDrones.Add(s);
            DroneListView.DataContext = listDrones;
            BO.BaseStation b= i.GetBaseStation("7");
            State.ItemsSource = Enum.GetValues(typeof(BO.DroneState));
            weight.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));
        }
        private BLApi.IBL bl;
        private BO.DroneToList selected;//selected item that will be send to the new window

        private ObservableCollection<BO.DroneToList> listDrones = new ObservableCollection<BO.DroneToList>();
        private void updated(object sender, EventArgs e)//the event that will update the details of the listView
        {
            listDrones.Clear();
            foreach (var item in bl.GetDrones())
                listDrones.Add(item);
        }

        private void changedState(object sender, EventArgs e)//event for changing the drone state- in the state filter
        {
            var item = State.SelectedItem;
            var check = weight.SelectedItem;
            if (check == null)
            {
                DroneListView.ItemsSource = item switch
                {
                    BO.DroneState.Available => bl.PredicateDrone(x => x.State == BO.DroneState.Available),
                    BO.DroneState.maintaince => bl.PredicateDrone(x => x.State == BO.DroneState.maintaince),
                    BO.DroneState.shipping => bl.PredicateDrone(x => x.State == BO.DroneState.shipping),
                    _ => bl.GetDrones(),
                };
            }
            else
            {
                DroneListView.ItemsSource = item switch
                {
                    BO.DroneState.Available => bl.PredicateDrone(x => ((x.State == BO.DroneState.Available) && (x.MaxWeight == (BO.WeightCategories)check))),
                    BO.DroneState.maintaince => bl.PredicateDrone(x => ((x.State == BO.DroneState.maintaince) && (x.MaxWeight == (BO.WeightCategories)check))),
                    BO.DroneState.shipping => bl.PredicateDrone(x => ((x.State == BO.DroneState.shipping) && (x.MaxWeight == (BO.WeightCategories)check))),
                    _ => bl.GetDrones(),
                };
            }

        }
        private void selectionChange(object sender, SelectionChangedEventArgs e)
        {
            selected = (BO.DroneToList)DroneListView.SelectedItem;
        }

        private void Action(object sender, MouseButtonEventArgs e)//event for double clicking on specific item 
        {
            
        }

        private void closing_Click(object sender, RoutedEventArgs e)//event for the closing button
        {
            this.Close();
        }

        private void changeWeig(object sender, SelectionChangedEventArgs e)//event for changing the drone weight- in the weight filter
        {
            var item = weight.SelectedItem;
            var check = State.SelectedItem;
            if (check == null)
            {
                DroneListView.ItemsSource = item switch
                {
                    BO.WeightCategories.Heavy => bl.PredicateDrone(x => x.MaxWeight == BO.WeightCategories.Heavy),
                    BO.WeightCategories.Light => bl.PredicateDrone(x => x.MaxWeight == BO.WeightCategories.Light),
                    BO.WeightCategories.Middle => bl.PredicateDrone(x => x.MaxWeight == BO.WeightCategories.Middle),
                    _ => bl.GetDrones(),

                };
            }
            else
            {
                DroneListView.ItemsSource = item switch
                {
                    BO.WeightCategories.Heavy => bl.PredicateDrone(x => ((x.MaxWeight == BO.WeightCategories.Heavy) && (x.State == (BO.DroneState)check))),
                    BO.WeightCategories.Light => bl.PredicateDrone(x => ((x.MaxWeight == BO.WeightCategories.Light) && (x.State == (BO.DroneState)check))),
                    BO.WeightCategories.Middle => bl.PredicateDrone(x => ((x.MaxWeight == BO.WeightCategories.Middle) && (x.State == (BO.DroneState)check))),
                    _ => bl.GetDrones(),
                };
            }
        }

        private void addingDrone_Click(object sender, RoutedEventArgs e)//event for the adding button
        {
        }

        private void reset_Click(object sender, RoutedEventArgs e)//event for the reset button
        {
            listDrones.Clear();
            foreach (var item in bl.GetDrones())
                listDrones.Add(item);
            State.SelectedItem = null;
            weight.SelectedItem = null;
        }
    }
}
