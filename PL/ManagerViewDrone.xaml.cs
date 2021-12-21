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
    /// Interaction logic for ManagerViewDrone.xaml
    /// </summary>
    public partial class ManagerViewDrone : Window
    {
        public ManagerViewDrone(BLApi.IBL b, string i)
        {
            InitializeComponent();
            bl = b;
            id = i;
            convertToPo(drone, bl.GetDrone(id));
            Id.DataContext = drone;
            MaxWeight.DataContext = drone;
            Model.DataContext = drone;
            Battery.DataContext = drone;
            State.DataContext = drone;
            Latitude.Text = drone.Latitude;
            Longitude.Text = drone.Longitude;
        }
        private BLApi.IBL bl;
        private bool isClosed = true;
        private string id;
        private PO.DronePO drone=new PO.DronePO();
        public event EventHandler updateList;

        /// <summary>
        /// ///להוסיפ שורה של כל החבילות
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void convertToPo(PO.DronePO dronePo, BO.Drone d)
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
        private void closing(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.UpdatingDetailsOfDrone(drone.Model, drone.IdNumber);
                MessageBox.Show($"the drone number {drone.IdNumber} updated successfully!");
                updateList(sender, e);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"the model {Model.Text} is illegal. please enter again", "Model Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void move(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            switch(isClosed)
            {
                case true:
                    isClosed = false;
                    shippingButton.Visibility =Visibility.Visible;
                    chargingButton.Visibility = Visibility.Visible;
                    break;
                case false:
                    isClosed = true;
                    shippingButton.Visibility = Visibility.Hidden;
                    chargingButton.Visibility = Visibility.Hidden;
                    break;
                default:
            }

        }
        private void charge(object sender, RoutedEventArgs e)
        {
            switch(drone.State)
            {
                case BO.DroneState.Available:
                    try 
                    {
                        bl.DroneToCharging(drone.IdNumber);
                        convertToPo(drone, bl.GetDrone(id));
                        MessageBox.Show("start charge");

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("error");
                    }
                    break;
                case BO.DroneState.maintaince:
                    try
                    {
                        bl.DroneFromCharging(drone.IdNumber);
                        convertToPo(drone, bl.GetDrone(id));
                        MessageBox.Show("release charge");

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("error");
                    }
                    break;
                case BO.DroneState.shipping:
                    MessageBox.Show("error");
                    break;

            }
        }

        private void shipButton(object sender, RoutedEventArgs e)
        {
            switch (drone.State)
            {
                case BO.DroneState.Available:
                    try
                    {
                        bl.MatchingParcelToDrone(drone.IdNumber);
                        convertToPo(drone,bl.GetDrone(id));
                        MessageBox.Show("match");

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("error");
                    }
                    break;
                case BO.DroneState.maintaince:
                    MessageBox.Show("error");
                    break;
                case BO.DroneState.shipping:
                    try
                    {
                        if(bl.GetParcel(drone.NumberOfParcel).CollectingDroneTime==null)
                        {
                            bl.PickingParcelByDrone(drone.IdNumber);
                            convertToPo(drone, bl.GetDrone(id));
                            MessageBox.Show("pick");


                        }
                        else
                        {
                            bl.SupplyingParcelByDrone(drone.IdNumber);
                            convertToPo(drone, bl.GetDrone(id));
                            MessageBox.Show("supply");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("error");
                    }
                    break;

            }

        }
    }
}
