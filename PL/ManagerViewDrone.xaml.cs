using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private BackgroundWorker worker;
        public ManagerViewDrone(BLApi.IBL b, string i)
        {
            InitializeComponent();
            bl = b;
            id = i;
            convertToPo(drone, bl.GetDrone(id));
            Id.DataContext = drone;
            Weight.DataContext = drone;
            Model.DataContext = drone;
            Battery.DataContext = drone;
            State.DataContext = drone;
            Latitude.Text = drone.Latitude;
            Longitude.Text = drone.Longitude;
            ParcelGrid.DataContext = parcel.IdNumber;
            textParcel.DataContext = parcel.IdNumber;
            IdParcel.DataContext = parcel;
            WeightParcel.DataContext = parcel;
            PriorityParcel.DataContext = parcel;
            StateParcel.DataContext = parcel;
            batteryRectangle.DataContext = drone;
        }
        private BLApi.IBL bl;
        private bool closeRequest=false;
        private PO.ParcelPO parcel=new PO.ParcelPO();
        private bool isClosed = true;
        private string id;
        private PO.DronePO drone = new PO.DronePO();
        public event EventHandler updateList;

        private void CovertParcelTOPO(PO.ParcelPO parcelPO, BO.ParcelOfList p)
        {
            if (drone.NumberOfParcel != null)
            {
                parcelPO.IdNumber = p.IdNumber;
                parcelPO.Geter = p.Geter;
                parcelPO.Sender = p.Sender;
                parcelPO.ParcelState = p.ParcelState;
                parcelPO.Priority = p.Priority;
                parcelPO.Weight = p.Weight;
            }
            else
                parcelPO.IdNumber = null;
        }
        private void assignVisibility()
        {
            Auto.Visibility = Visibility.Visible;
            if (drone.State == BO.DroneState.Available)
            {
                chargingButton.Visibility = Visibility.Visible;
                shippingButton.Visibility = Visibility.Visible;
            }
            if (drone.State == BO.DroneState.maintaince)
                releaseButton.Visibility = Visibility.Visible;
            if (drone.State == BO.DroneState.shipping)
            {
                if (bl.GetParcel(drone.NumberOfParcel).CollectingDroneTime == null)
                {
                    pickButton.Visibility = Visibility.Visible;
                }
                else
                {
                    suppltingButton.Visibility = Visibility.Visible;
                }
            }
        }
        public void convertToPo(PO.DronePO dronePo, BO.Drone d)
        //function that get the drone and update it to the current values as given from the bl
        {
            dronePo.Battery = (int)d.Battery;
            dronePo.IdNumber = d.IdNumber;
            dronePo.State = d.State;
            dronePo.MaxWeight = d.MaxWeight;
            dronePo.Model = d.Model;
            dronePo.NumberOfParcel = d.PassedParcel?.IdNumber;
            dronePo.Latitude = LocationFormat.sexagesimalFormat(d.Location.Latitude, false); 
            dronePo.Longitude = LocationFormat.sexagesimalFormat(d.Location.Longitude, true);
            if (drone.NumberOfParcel != null)
            {
                CovertParcelTOPO(parcel, bl.GetAllParcelsBy(x => x.IdNumber == drone.NumberOfParcel).FirstOrDefault());
                textParcel.Visibility = Visibility.Collapsed;
                ParcelGrid.Visibility = Visibility.Visible;
            }
            else
            {
                textParcel.Visibility = Visibility.Visible;
                ParcelGrid.Visibility = Visibility.Collapsed;

            }

        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            closeRequest = true; 
            updateList(sender, e);
            if(worker!=null&&worker.IsBusy==true)
            {
                worker.CancelAsync();
            }
            else
            {
                this.Close();
            }
           // this.Close();
        }
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.UpdatingDetailsOfDrone(drone.Model, drone.IdNumber);
                MessageBox.Show($"the drone number {drone.IdNumber} updated successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"the model {Model.Text} is illegal. please enter again", "Model Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }        

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            switch (isClosed)
            {
                case true:
                    isClosed = false;
                    GridOptions.Visibility = Visibility.Visible;
                    CloseOptions.Kind = MahApps.Metro.IconPacks.PackIconMaterialKind.ArrowLeft;
                    assignVisibility();
                    Auto.Visibility = Visibility.Visible;
                    break;
                case false:
                    isClosed = true;
                    CloseOptions.Kind = MahApps.Metro.IconPacks.PackIconMaterialKind.Wrench;
                    GridOptions.Visibility = Visibility.Hidden;
                    Auto.Visibility = Visibility.Collapsed;
                    Manual.Visibility = Visibility.Collapsed;
                    releaseButton.Visibility = Visibility.Collapsed;
                    chargingButton.Visibility = Visibility.Collapsed;
                    pickButton.Visibility = Visibility.Collapsed;
                    suppltingButton.Visibility = Visibility.Collapsed;
                    shippingButton.Visibility = Visibility.Collapsed;
                    Auto.Visibility = Visibility.Collapsed;
                    break;
                default:
            }

        }
        private void charge(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.DroneToCharging(drone.IdNumber);
                convertToPo(drone, bl.GetDrone(id));
                MessageBox.Show("start charge");
                Button_Click_1(sender, e); ;
                Button_Click_1(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void shipButton(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.MatchingParcelToDrone(drone.IdNumber);
                convertToPo(drone, bl.GetDrone(id));
                MessageBox.Show("match");
                Button_Click_1(sender, e);
                Button_Click_1(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void supplyButton(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.SupplyingParcelByDrone(drone.IdNumber);
                convertToPo(drone, bl.GetDrone(id));
                MessageBox.Show("supply");
                Button_Click_1(sender, e);
                Button_Click_1(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void pickingButton(object sender, RoutedEventArgs e)
        {
            try
            {
                if (bl.GetParcel(drone.NumberOfParcel).CollectingDroneTime == null)
                {
                    bl.PickingParcelByDrone(drone.IdNumber);
                    convertToPo(drone, bl.GetDrone(id));
                    MessageBox.Show("pick");
                    Button_Click_1(sender, e);
                    Button_Click_1(sender, e);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("error");
            }
        }
        private void releaseMethod(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.DroneFromCharging(drone.IdNumber);
                convertToPo(drone, bl.GetDrone(id));
                MessageBox.Show("release charge");
                Button_Click_1(sender, e);
                Button_Click_1(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        private void openParcel(object sender, RoutedEventArgs e)
        {
            ManagerViewParcel parcel = new ManagerViewParcel(bl, drone.NumberOfParcel);
            parcel.ShowDialog();
        }
        private void updateDrone() => worker.ReportProgress(0);
        private bool Cancel() => worker.CancellationPending;
        private void StartSimulation_Click(object sender, RoutedEventArgs e)
        {
            releaseButton.Visibility = Visibility.Collapsed;
            chargingButton.Visibility = Visibility.Collapsed;
            pickButton.Visibility = Visibility.Collapsed;
            suppltingButton.Visibility = Visibility.Collapsed;
            shippingButton.Visibility = Visibility.Collapsed;
            Auto.Visibility = Visibility.Collapsed;
            Auto.Visibility = Visibility.Collapsed;
            Manual.Visibility = Visibility.Visible;
            worker = new() { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Complited;
            worker.RunWorkerAsync(id);
        }
        
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            bl.Simulator(id, updateDrone, Cancel);
            if ((worker.CancellationPending == true))
            {
                e.Cancel = true;
            }

        }
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            updateList(sender, e);
            convertToPo(drone, bl.GetDrone(id));
        }
        private void Complited(object sender, RunWorkerCompletedEventArgs e)
        {
            worker = null;
            if (closeRequest == true)
                this.Close();
        }
        private void stop(object sender, RoutedEventArgs e)
        {
            if (worker.IsBusy == true && worker.WorkerSupportsCancellation == true)
            {
                worker.CancelAsync();
            }
            Auto.Visibility = Visibility.Visible;
            Manual.Visibility = Visibility.Collapsed;
            assignVisibility();

        }
    }
}
