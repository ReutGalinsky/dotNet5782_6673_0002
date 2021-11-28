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
        public ListDrones(IBL.IBL i)
        {
            InitializeComponent();
            bl = i;
            foreach (IBL.BO.DroneToList s in bl.GetDrones())
                listDrones.Add(s);
            DroneListView.DataContext = listDrones;
            
            State.ItemsSource = Enum.GetValues(typeof(IBL.BO.DroneState));
            weight.ItemsSource = Enum.GetValues(typeof(IBL.BO.WeightCategories));
        }
        private IBL.IBL bl;
        private IBL.BO.DroneToList selected;
        private ObservableCollection<IBL.BO.DroneToList> listDrones = new ObservableCollection<IBL.BO.DroneToList>();
        private void viewDrone(object sender, MouseButtonEventArgs e)
        {
            Drone droneWindow = new Drone(bl);
            droneWindow.Show();
        }
        private void updated(object sender, EventArgs e)
        {
            listDrones.Clear();
            foreach (var item in bl.GetDrones())
                listDrones.Add(item);
        }

        private void changedState(object sender, EventArgs e)
        {
            var item = State.SelectedItem;
            var check = weight.SelectedItem;
            if (check == null)
            {
                DroneListView.ItemsSource = item switch
                {
                    IBL.BO.DroneState.Available => bl.PredicateDrone(x => x.State == IBL.BO.DroneState.Available),
                    IBL.BO.DroneState.maintaince => bl.PredicateDrone(x => x.State == IBL.BO.DroneState.maintaince),
                    IBL.BO.DroneState.shipping => bl.PredicateDrone(x => x.State == IBL.BO.DroneState.shipping),
                    _ => bl.GetDrones(),
                };
            }
            else
            {
                DroneListView.ItemsSource = item switch
                {
                    IBL.BO.DroneState.Available => bl.PredicateDrone(x => ((x.State == IBL.BO.DroneState.Available)&&(x.MaxWeight== (IBL.BO.WeightCategories)check))),
                    IBL.BO.DroneState.maintaince => bl.PredicateDrone(x => ((x.State == IBL.BO.DroneState.maintaince) && (x.MaxWeight == (IBL.BO.WeightCategories)check))),
                    IBL.BO.DroneState.shipping => bl.PredicateDrone(x => ((x.State == IBL.BO.DroneState.shipping) && (x.MaxWeight == (IBL.BO.WeightCategories)check))),
                    _ => bl.GetDrones(),
                };
            }

        }
        private void selectionChange(object sender, SelectionChangedEventArgs e)
        {
            selected = (IBL.BO.DroneToList)DroneListView.SelectedItem;
        }

        private void Action(object sender, MouseButtonEventArgs e)
        {
            if (selected != null)
            {
                Drone dWindow = new Drone(selected, bl);
                dWindow.updateList += updated;
                dWindow.Show();
                selected = null;
            }
        }

        private void closing_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void changeWeig(object sender, SelectionChangedEventArgs e)
        {
            var item = weight.SelectedItem;
            var check = State.SelectedItem;
            if (check == null)
            {
                DroneListView.ItemsSource = item switch
                {
                    IBL.BO.WeightCategories.Heavy => bl.PredicateDrone(x => x.MaxWeight == IBL.BO.WeightCategories.Heavy),
                    IBL.BO.WeightCategories.Light => bl.PredicateDrone(x => x.MaxWeight == IBL.BO.WeightCategories.Light),
                    IBL.BO.WeightCategories.Middle => bl.PredicateDrone(x => x.MaxWeight == IBL.BO.WeightCategories.Middle),
                    _ => bl.GetDrones(),

                };
            }
            else
            {
                DroneListView.ItemsSource = item switch
                {
                    IBL.BO.WeightCategories.Heavy => bl.PredicateDrone(x => ((x.MaxWeight == IBL.BO.WeightCategories.Heavy) && (x.State == (IBL.BO.DroneState)check))),
                    IBL.BO.WeightCategories.Light => bl.PredicateDrone(x => ((x.MaxWeight == IBL.BO.WeightCategories.Light) && (x.State == (IBL.BO.DroneState)check))),
                    IBL.BO.WeightCategories.Middle => bl.PredicateDrone(x => ((x.MaxWeight == IBL.BO.WeightCategories.Middle) && (x.State == (IBL.BO.DroneState)check))),
                    _ => bl.GetDrones(),
                };
            }
        }

        private void addingDrone_Click(object sender, RoutedEventArgs e)
        {
            Drone dWindow = new Drone(bl);
            dWindow.updateList += updated;
            dWindow.Show();
        }
    }
}
