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

namespace PL.Pages
{
    /// <summary>
    /// Interaction logic for ManagerAddDrone.xaml
    /// </summary>
    public partial class ManagerAddDrone : Page
    {
        public ManagerAddDrone(BLApi.IBL b)
        {
            InitializeComponent();


            bl = b;
            ADD.IsEnabled = false;
            Weight.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));
            Id.DataContext = drone;
            Model.DataContext = drone;
            Weight.DataContext = drone;
            Weight.SelectedItem = Weight.Items[0];
            try
            {
                stationNumber.DataContext = bl.GetAllBaseStationsBy(x => x.ChargeSlots > 0);
            }
            catch (Exception)
            { MessageBox.Show("Error in loading the stations, please try again later"); }

        }

        private BLApi.IBL bl;
        private BO.DroneToList drone = new BO.DroneToList();
        public event EventHandler updateList;

        private void Close_Click(object sender, RoutedEventArgs e)//event of the cancel button
        {
            updateList(sender, e);
        }

        private void TextBox_OnlyNumbers_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Tools.TextBox_OnlyNumbers_PreviewKeyDown(sender, e);
        }

        private void OnlyNumbers_PreviewKeyDown(object sender, KeyEventArgs e)//enable to enter only digits
        {
            TextBox_OnlyNumbers_PreviewKeyDown(sender, e);
        }

        private void focus(object sender, TextChangedEventArgs e)
        {
            if (drone.IdNumber == "" || drone.Model == "" || stationNumber.SelectedItem == null || Weight.SelectedItem == null)
                ADD.IsEnabled = false;
            else
            {
                ADD.IsEnabled = true;
            }

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.AddDrone(drone, (stationNumber.SelectedItem as BO.BaseStationToList).IdNumber);
                Close_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void focus1(object sender, SelectionChangedEventArgs e)
        {
            if (drone.IdNumber == "" || drone.Model == "" || stationNumber.SelectedItem == null || Weight.SelectedItem == null)
                ADD.IsEnabled = false;
            else
            {
                ADD.IsEnabled = true;
            }

        }
    }
}



