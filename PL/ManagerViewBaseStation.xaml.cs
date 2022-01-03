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
using System.Collections.ObjectModel;
using BLApi;
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
            ConstructorFuction();
            IsInner = false;

        }
        private void ConstructorFuction()
        {
            baseStation = bl.GetBaseStation(id);
            foreach (var item in baseStation.Drones)
                drones.Add(item);
            Id.DataContext = baseStation;
            Name.DataContext = baseStation;
            ChargeSlots.DataContext = baseStation;
            Latitude.DataContext = baseStation.Location;
            Longitude.DataContext = baseStation.Location;
            listDrones.DataContext = drones;
            foreach (var item in baseStation.Drones)
                drones.Add(item);
        }
        public ManagerViewBaseStation(BLApi.IBL b, string i,bool inner)
        {
            InitializeComponent();
            bl = b;
            id = i;
            ConstructorFuction();
            IsInner = true;
        }
        private bool IsInner;
        private BLApi.IBL bl;
        private string id;
        private BO.BaseStation baseStation;
        public event EventHandler updateList;
        private ObservableCollection<BO.DroneInCharge> drones=new ObservableCollection<BO.DroneInCharge>();
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
                if(IsInner==false)
                    updateList(sender, e);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"error", MessageBoxButton.OK,MessageBoxImage.Error);

            }
        }
        private void TextBox_OnlyNumbers_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Tools.TextBox_OnlyNumbers_PreviewKeyDown(sender, e);
        }

        private void openDrone(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();

        }
        private void showTheParcel(object sender, RoutedEventArgs e)
        {
            ManagerViewDrone drone = new ManagerViewDrone(bl, ((BO.DroneInCharge)(listDrones.SelectedItem)).IdNumber,true);
            drone.ShowDialog();
        }

        private void enableShow(object sender, SelectionChangedEventArgs e)
        {
            showParcel.IsEnabled = true;
        }
    }
}
