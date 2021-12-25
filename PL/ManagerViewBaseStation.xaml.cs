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
    /// Interaction logic for CustomerViewBaseStation.xaml
    /// </summary>
    public partial class ManagerViewBaseStation : Window
    {
        public ManagerViewBaseStation(BLApi.IBL b, string i)
        {
            InitializeComponent();
            bl = b;
            id = i;
            baseStation = bl.GetBaseStation(id);
            Id.DataContext = baseStation;
            Name.DataContext = baseStation;
            ChargeSlots.DataContext = baseStation;
            Latitude.DataContext = baseStation.Location;
            Longitude.DataContext = baseStation.Location;
            Drone.DataContext = baseStation.Drones;
        }
        private BLApi.IBL bl;
        private string id;
        private BO.BaseStation baseStation;
        public event EventHandler updateList;

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.UpdatingDetailsOfBaseStation(baseStation.IdNumber,baseStation.Name,baseStation.ChargeSlots.ToString());
                MessageBox.Show($"the base station number {baseStation.IdNumber} updated successfully!");
                updateList(sender, e);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"error", MessageBoxButton.OK,MessageBoxImage.Error);

            }
        }
        private void move(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();

        }
        private void TextBox_OnlyNumbers_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Tools.TextBox_OnlyNumbers_PreviewKeyDown(sender, e);
        }

        private void openDrone(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
