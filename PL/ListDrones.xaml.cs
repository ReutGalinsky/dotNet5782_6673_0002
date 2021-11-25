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
    /// Interaction logic for ListDrones.xaml
    /// </summary>
    public partial class ListDrones : Window
    {
        public ListDrones(IBL.IBL i)
        {
            InitializeComponent();
            bl = i;
            DroneListView.ItemsSource = bl.GetDrones();
            State.ItemsSource = Enum.GetValues(typeof(IBL.BO.DroneState));
            weight.ItemsSource = Enum.GetValues(typeof(IBL.BO.WeightCategories));
        }
        private IBL.IBL bl;
        private void viewDrone(object sender, MouseButtonEventArgs e)
        {
            Drone droneWindow = new Drone(bl);
            droneWindow.Show();
        }

        private void changedState(object sender, SelectionChangedEventArgs e)
        {
            var item = State.SelectedItem;
            DroneListView.ItemsSource = item switch
            {
                IBL.BO.DroneState.Available => bl.PredicateDrone(x => x.State == IBL.BO.DroneState.Available),
                IBL.BO.DroneState.maintaince => bl.PredicateDrone(x => x.State == IBL.BO.DroneState.maintaince),
                IBL.BO.DroneState.shipping => bl.PredicateDrone(x => x.State == IBL.BO.DroneState.shipping),
                _=> bl.GetDrones(),
            };

        }

        private void changedWeight(object sender, SizeChangedEventArgs e)
        {
            var item = weight.SelectedItem;

            DroneListView.ItemsSource = item switch
            {
                IBL.BO.WeightCategories.Heavy => bl.PredicateDrone(x => x.MaxWeight == IBL.BO.WeightCategories.Heavy),
                IBL.BO.WeightCategories.Light => bl.PredicateDrone(x => x.MaxWeight == IBL.BO.WeightCategories.Light),
                IBL.BO.WeightCategories.Middle => bl.PredicateDrone(x => x.MaxWeight == IBL.BO.WeightCategories.Middle),
                _ => bl.GetDrones(),

            };
        }

        private void showDouble(object sender, RequestBringIntoViewEventArgs e)
        {
                        Drone dWindow = new Drone();
            dWindow.Show();
        }
    }
}
