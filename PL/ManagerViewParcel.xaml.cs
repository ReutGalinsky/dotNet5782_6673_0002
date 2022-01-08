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
    /// <summary>
    /// Interaction logic for ManagerViewParcel.xaml
    /// </summary>
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
            parcel = bl.GetParcel(id);
            sender = bl.GetCustomer(parcel.SenderCustomer.IdNumber);
            geter = bl.GetCustomer(parcel.GeterCustomer.IdNumber);
            if (parcel.Drone != null)
                drone = bl.GetDrone(parcel.Drone.IdNumber);
            else
                drone = new BO.Drone();
            Id.DataContext = parcel;
            PriorityBox.DataContext = parcel;
            WeightBox.DataContext = parcel;
            Create.DataContext = parcel;
            Match.DataContext = parcel;
            Arrive.DataContext = parcel;
            Collect.DataContext = parcel;
            buttonb.DataContext = parcel;
            Match.DataContext = parcel;
            matchLabel.DataContext = parcel;
            Collect.DataContext = parcel;
            collectLabel.DataContext = parcel;
            Arrive.DataContext = parcel;
            arriveLabel.DataContext = parcel;
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

        //private void openDrone(object sender, RoutedEventArgs e)
        //{
        //    ManagerViewDrone drone = new ManagerViewDrone(bl, parcel.Drone.IdNumber, true);
        //    drone.ShowDialog();
        //}

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        private void openGeter(object sender, RoutedEventArgs e)
        {
            ManagerViewCustomer customer = new ManagerViewCustomer(bl, parcel.GeterCustomer.IdNumber);
            customer.ShowDialog();
        }
        private void openSender(object sender, RoutedEventArgs e)
        {
            ManagerViewCustomer customer = new ManagerViewCustomer(bl, parcel.SenderCustomer.IdNumber);
            customer.ShowDialog();
        }
    }
}
