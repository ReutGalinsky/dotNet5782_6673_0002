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
using System.Globalization;

namespace PL
{
    public partial class ManagerViewParcel : Window
    {
        private BLApi.IBL bl;
        private string id;
        private BO.Parcel parcel;
        private BO.Customer sender;
        private BO.Customer geter;
        private BO.Drone drone;

        public ManagerViewParcel(BLApi.IBL b, string i)
        {
            InitializeComponent();
            bl = b;
            id = i;
            try
            {
                parcel = bl.GetParcel(id);
            }
            catch (Exception)
            { MessageBox.Show("Error in loading the parcel, please try again later"); }
            try
            {
                sender = bl.GetCustomer(parcel.SenderCustomer.IdNumber);
                geter = bl.GetCustomer(parcel.GeterCustomer.IdNumber);
            }
            catch (Exception)
            { MessageBox.Show("Error in loading the customer, please try again later"); }
            try
            {
                if (parcel.Drone != null)
                    drone = bl.GetDrone(parcel.Drone.IdNumber);
                else
                    drone = new BO.Drone();
            }
            catch (Exception)
            { MessageBox.Show("Error in loading the drone, please try again later"); }
            GridParcels.DataContext = parcel;
            Latitude.DataContext = drone.Location;
            Longitude.DataContext = drone.Location;
            GridSender.DataContext = sender;
            GridGeter.DataContext = geter;
            GridDrone.DataContext = parcel;
            note1.DataContext = parcel;
            note2.DataContext = parcel;
            innerDrone.DataContext = drone;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
