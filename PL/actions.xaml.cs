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

namespace PL
{
    /// <summary>
    /// Interaction logic for actions.xaml
    /// </summary>
    public partial class actions : Page//page for actions on selected drone
    {
        public actions(BO.DroneToList a, BLApi.IBL bl)
        //ctor- set the current drone and the binding- sourct objects
        {
            InitializeComponent();
            b = bl;
            drone = new DronePO();
            drone.Battery = a.Battery;
            drone.IdNumber = a.IdNumber;
            drone.State = a.State;
            drone.MaxWeight = a.MaxWeight;
            drone.Model = a.Model;
            drone.Longitude = LocationFormat.sexagesimalFormat(a.Location.Longitude, true);
            drone.Latitude = LocationFormat.sexagesimalFormat(a.Location.Latitude, false);
            drone.NumberOfParcel = a.NumberOfParcel;
            state.DataContext = drone;
            id.DataContext = drone;
            parcel.DataContext = drone;
            Model.DataContext = drone;
            battery.DataContext = drone;
            weight.DataContext = drone;
            latitude.DataContext = drone;
            lonigude.DataContext = drone;
            notEnabled();
            Enabled();
            weight.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));//use the enum for the weight
        }
        private void Enabled()
        //function that each time define which button will be enabled
        {
            if (drone.State == BO.DroneState.Available)
            {
                charging.IsEnabled = true;
                sendShip.IsEnabled = true;
            }
            else if (drone.State == BO.DroneState.maintaince)
            {
                release.IsEnabled = true;
            }
            else
            {
                parcel.Visibility = Visibility.Visible;
                parcell.Visibility = Visibility.Visible;
                if (b.GetParcel(drone.NumberOfParcel).CollectingDroneTime == null)
                    Collecting.IsEnabled = true;
                else
                    supplying.IsEnabled = true;
            }
        }
        private void notEnabled()
        //function that define all the buttons to be disenabled
        {
            time.Visibility = Visibility.Hidden;
            timeLabel.Visibility = Visibility.Hidden;
            parcel.Visibility = Visibility.Hidden;
            parcell.Visibility = Visibility.Hidden;
            charging.IsEnabled = false;
            sendShip.IsEnabled = false;
            release.IsEnabled = false;
            Collecting.IsEnabled = false;
            supplying.IsEnabled = false;
        }
        public void convertToPo(DronePO dronePo, BO.Drone d)
        //function that get the drone and update it to the current values as given from the bl
        {
            dronePo.Battery = d.Battery;
            dronePo.IdNumber = d.IdNumber;
            dronePo.State = d.State;
            dronePo.MaxWeight = d.MaxWeight;
            dronePo.Model = d.Model;
            dronePo.NumberOfParcel = d.PassedParcel?.IdNumber;
            dronePo.Latitude = LocationFormat.sexagesimalFormat(d.Location.Latitude, false); ;
            dronePo.Longitude = LocationFormat.sexagesimalFormat(d.Location.Longitude, true); ;
        }

        BLApi.IBL b;//the BL object

        DronePO drone;//the selected drone

        private bool isClickedOnce = false;//for the releasing button
        private void update_Click(object sender, RoutedEventArgs e)//event of the updaiting button
        {
            drone.Model = Model.Text;
            try
            {
                b.UpdatingDetailsOfDrone(drone.Model, drone.IdNumber);
                MessageBox.Show("success");
                notEnabled();
                Enabled();
            }
            catch (Exception ex)
            {
                MessageBox.Show("err: " + ex.Message);
            }
        }
        private void charging_Click(object sender, RoutedEventArgs e)//event for the charging button
        {
            try
            {
                b.DroneToCharging(drone.IdNumber);
                MessageBox.Show("success");
                convertToPo(drone, b.GetDrone(drone.IdNumber));
                notEnabled();
                Enabled();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void release_Click(object sender, RoutedEventArgs e)//event for the releasing button
        {
            if (isClickedOnce == false)
            {
                time.Visibility = Visibility.Visible;
                timeLabel.Visibility = Visibility.Visible;
                isClickedOnce = true;
                release.Content = "Ok";

            }
            else
            {
                try
                {                 
                    time.Visibility = Visibility.Hidden;
                    timeLabel.Visibility = Visibility.Hidden;
                    release.Content = "release from charge";
                    isClickedOnce = false;
                    parcel.Text = "";
                    b.DroneFromCharging(drone.IdNumber);
                    MessageBox.Show("success");
                    convertToPo(drone, b.GetDrone(drone.IdNumber));
                    notEnabled();
                    Enabled();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void sendShip_Click(object sender, RoutedEventArgs e)//event for the send-shipping button 
        {
            try
            {
                b.MatchingParcelToDrone(drone.IdNumber);
                MessageBox.Show("success");
                parcel.Visibility = Visibility.Visible;
                parcell.Visibility = Visibility.Visible;
                convertToPo(drone, b.GetDrone(drone.IdNumber));
                notEnabled();
                Enabled();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void supplying_Click(object sender, RoutedEventArgs e)//event for the supplying button
        {
            try
            {
                b.SupplyingParcelByDrone(drone.IdNumber);
                MessageBox.Show("success");
                parcel.Visibility = Visibility.Hidden;
                parcell.Visibility = Visibility.Hidden;
                convertToPo(drone, b.GetDrone(drone.IdNumber));
                notEnabled();
                Enabled();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Collecting_Click(object sender, RoutedEventArgs e)//event for the Collecting button
        {
            try
            {
                b.PickingParcelByDrone(drone.IdNumber);
                MessageBox.Show("success");
                convertToPo(drone, b.GetDrone(drone.IdNumber));
                notEnabled();
                Enabled();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
