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
        public ManagerViewParcel(BLApi.IBL b, string i)
        {
            InitializeComponent();
            bl = b;
            id = i;
            parcel = bl.GetParcel(id);
            Id.DataContext = parcel;
            SenderId.DataContext = parcel.SenderCustomer;
            SenderName.DataContext = parcel.SenderCustomer;
            GeterId.DataContext = parcel.GeterCustomer;
            GeterName.DataContext = parcel.GeterCustomer;
            PriorityBox.DataContext = parcel;
            WeightBox.DataContext = parcel;
            Create.DataContext = parcel;
            Match.DataContext = parcel;
            Arrive.DataContext = parcel;
            Collect.DataContext = parcel;
            if(parcel.MatchForDroneTime==null)
            {
                Match.Visibility = Visibility.Collapsed;
                matchLabel.Visibility = Visibility.Collapsed;
            }
            if (parcel.CollectingDroneTime==null)
            {
                Collect.Visibility = Visibility.Collapsed;
                collectLabel.Visibility = Visibility.Collapsed;
            }
            if (parcel.ArrivingDroneTime==null)
            {
                Arrive.Visibility = Visibility.Collapsed;
                arriveLabel.Visibility = Visibility.Collapsed;
            }
        }
        private BLApi.IBL bl;
        private string id;
        private BO.Parcel parcel;

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void move(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

 
    }
}
