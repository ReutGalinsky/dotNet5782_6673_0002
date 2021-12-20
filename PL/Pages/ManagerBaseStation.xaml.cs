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
using System.Collections.ObjectModel;


namespace PL.Pages
{
    /// <summary>
    /// Interaction logic for ManagerBaseStation.xaml
    /// </summary>
    public partial class ManagerBaseStation : Page
    {
        public ManagerBaseStation(BLApi.IBL b)
        {
            InitializeComponent();
            bl = b;
            foreach (var item in bl.GetBaseStations())
            {
                liststations.Add(item);
            }
            StationsListView.DataContext = liststations;
            chargeSlot.SelectedItem = "all";
        }
        public ObservableCollection<BO.BaseStationToList> liststations = new ObservableCollection<BO.BaseStationToList>();

        private BLApi.IBL bl;
        private BO.BaseStationToList selected;

        private void AddLineButton_Click(object sender, RoutedEventArgs e)
        {
            addingWindow add = new addingWindow(bl);
            add.ShowDialog();
        }
        private void updated(object sender, EventArgs e)//the event that will update the details of the listView
        {
            chargeSlot.SelectedItem = "all";
            liststations.Clear();
            foreach (var item in bl.GetBaseStations())
                liststations.Add(item);
            StationsListView.ItemsSource = liststations;
            var current = Window.GetWindow(this);
            current.Opacity = 1;
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            addButton.IsEnabled = false;
        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            selected = (BO.BaseStationToList)StationsListView.SelectedItem;
        }



        private void addStation(object sender, RoutedEventArgs e)
        {
            addingWindow adding = new addingWindow(bl);
            adding.add += updated;
            adding.ShowDialog();
        }

        private void reset_Click(object sender, RoutedEventArgs e)
        {

            liststations.Clear();
            foreach (var item in bl.GetBaseStations())
                liststations.Add(item);
        }


        private void listBaseStations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selected = (BO.BaseStationToList)StationsListView.SelectedItem;
        }
        private void Action(object sender, MouseButtonEventArgs e)//event for double clicking on specific item 
        {
            ManagerViewBaseStation showBase = new ManagerViewBaseStation(bl, selected.IdNumber);
            showBase.Show();
        }


        private void changeFilterCharge(object sender, SelectionChangedEventArgs e)
        {
            var comboBoxItem = (sender as ComboBox).Items[(sender as ComboBox).SelectedIndex] as ComboBoxItem;
            StationsListView.ItemsSource = comboBoxItem.Content.ToString() switch
            {
                "With available charging slots" => bl.GetAllBaseStationsBy(x => x.ChargeSlots > 0),
                "Without available charging slots" => bl.GetAllBaseStationsBy(x => x.ChargeSlots == 0),
                _ => bl.GetBaseStations(),
            };

        }

        private void selectionChange(object sender, SelectionChangedEventArgs e)
        {
            selected = (BO.BaseStationToList)StationsListView.SelectedItem;
        }
        private void deleteStation(object sender, RoutedEventArgs e)
        {
            var dialogResult = MessageBox.Show($"are you sure?", "Delede Parcel", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                try
                {
                    BO.BaseStationToList BaseStationToDelete = ((sender as Button).DataContext) as BO.BaseStationToList;
                    bl.RemoveBaseStation(BaseStationToDelete.IdNumber);
                    StationsListView.ItemsSource = bl.GetBaseStations();
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}



        
