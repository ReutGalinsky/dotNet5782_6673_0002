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
    public partial class actions : Page
    {
        public actions(IBL.BO.DroneToList a, IBL.IBL bl)
        {
            InitializeComponent();
            b = bl;
            drone = new DronePO();
            drone.Battery = a.Battery;
            drone.IdNumber = a.IdNumber;
            drone.State = a.State;
            drone.MaxWeight = a.MaxWeight;
            drone.Model = a.Model;
            drone.NumberOfParcel = a.NumberOfParcel;
            state.DataContext = drone;
            id.DataContext = drone;
            parcel.DataContext = drone;
            Model.DataContext = drone;
            battery.DataContext = drone;
            weight.DataContext = drone;
            notEnabled();
            Enabled();
            weight.ItemsSource = Enum.GetValues(typeof(IBL.BO.WeightCategories));
        }
        private void Enabled()
        {
            if (drone.State == IBL.BO.DroneState.Available)
            {
                charging.IsEnabled = true;
                sendShip.IsEnabled = true;
            }
            else if (drone.State == IBL.BO.DroneState.maintaince)
            { release.IsEnabled = true;
            }
            else
            {
                parcel.Visibility = Visibility.Visible;
                parcell.Visibility = Visibility.Visible;
                if (b.GetParcel(drone.NumberOfParcel).collectingDroneTime == null)
                    collecting.IsEnabled = true;
                else
                    supplying.IsEnabled = true;
            }
        }
        private void notEnabled()
        {
            time.Visibility = Visibility.Hidden;
            timeLabel.Visibility = Visibility.Hidden;
            parcel.Visibility = Visibility.Hidden;
            parcell.Visibility = Visibility.Hidden;
            charging.IsEnabled = false;
            sendShip.IsEnabled = false;
            release.IsEnabled = false;
            collecting.IsEnabled = false;
            supplying.IsEnabled = false;
        }
        public void convertToPo(DronePO dronePo, IBL.BO.Drone d)
        {
            dronePo.Battery = d.Battery;
            dronePo.IdNumber = d.IdNumber;
            dronePo.State = d.State;
            dronePo.MaxWeight = d.MaxWeight;
            dronePo.Model = d.Model;
            dronePo.NumberOfParcel = d.PassedParcel?.IdNumber;
        }
        IBL.IBL b;
        DronePO drone;
        private bool isClickedOnce = false;
        private void update_Click(object sender, RoutedEventArgs e)
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
        private void charging_Click(object sender, RoutedEventArgs e)
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

        private void release_Click(object sender, RoutedEventArgs e)
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
                    string item = time.Text;
                    var list = item.Split(':');
                    if (list.Length != 3)
                    {
                        MessageBox.Show("not correct format");
                        return;
                    }
                    int Hours = 0, Min = 0, Sec = 0;
                    try
                    {
                        Hours = int.Parse(list[0]);
                        Min = int.Parse(list[1]);
                        Sec = int.Parse(list[2]);
                    }
                    catch
                    {
                        MessageBox.Show("not correct format");
                        return;
                    }
                    time.Visibility = Visibility.Hidden;
                    timeLabel.Visibility = Visibility.Hidden;
                    TimeSpan a = new TimeSpan(Hours, Min, Sec);
                    release.Content = "release from charge";
                    isClickedOnce = false;
                    parcel.Text = "";
                    b.DroneFromCharging(drone.IdNumber, a);
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

        private void sendShip_Click(object sender, RoutedEventArgs e)
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
        private void supplying_Click(object sender, RoutedEventArgs e)
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

        private void collecting_Click(object sender, RoutedEventArgs e)
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
