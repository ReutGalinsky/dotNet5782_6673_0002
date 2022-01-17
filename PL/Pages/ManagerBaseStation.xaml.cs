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
    public partial class ManagerBaseStation : Page
    {
        public ManagerBaseStation(BLApi.IBL b)
        {
            InitializeComponent();
            bl = b;
            try
            {
                foreach (var item in bl.GetBaseStations())
                {
                    liststations.Add(item);
                }
            }
            catch (Exception)
            { MessageBox.Show("Error in loading the stations, please try again later"); }
            StationsListView.DataContext = liststations;
            chargeSlot.SelectedItem = chargeSlot.Items[0];
            update += updated;
        }

        public ObservableCollection<BO.BaseStationToList> liststations = new ObservableCollection<BO.BaseStationToList>();
        private BLApi.IBL bl;
        private BO.BaseStationToList selected;
        public EventHandler update;
        private void updated(object sender, EventArgs e)
        {
            chargeSlot.SelectedItem = chargeSlot.Items[0];
            liststations.Clear();
            try
            {
                foreach (var item in bl.GetBaseStations())
                {
                    liststations.Add(item);
                }
            }
            catch (Exception)
            { MessageBox.Show("Error in loading the stations, please try again later"); }

        }
        private void Action(object sender, MouseButtonEventArgs e)
        {
            if (selected != null)
            {
                ManagerViewBaseStation showBase = new ManagerViewBaseStation(bl, selected.IdNumber);
                showBase.updateList += updated;
                showBase.Show();
                selected = null;
            }
        }
        private void changeFilterCharge(object sender,SelectionChangedEventArgs e)
        {
            try
            {
                var comboBoxItem = (sender as ComboBox).Items[(sender as ComboBox).SelectedIndex] as ComboBoxItem;
                liststations.Clear();
                switch (comboBoxItem.Content.ToString())
                {
                    case "With available charging slots":
                        foreach (var item in bl.GetAllBaseStationsBy(x => x.ChargeSlots > 0))
                            liststations.Add(item);
                        break;
                    case "Without available charging slots":
                        foreach (var item in bl.GetAllBaseStationsBy(x => x.ChargeSlots == 0))
                            liststations.Add(item);
                        break;
                    default:
                        foreach (var item in bl.GetBaseStations())
                            liststations.Add(item);
                        break;
                }
            }
            catch (Exception)
            { MessageBox.Show("Error in loading the stations, please try again later"); }

        }
        private void selectionChange(object sender, SelectionChangedEventArgs e)
        {
            selected = (BO.BaseStationToList)StationsListView.SelectedItem;
        }
        private void GroupAmount(object sender, RoutedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(StationsListView.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("ChargeSlots");
            view.GroupDescriptions.Add(groupDescription);
        }
        private void cancelGroup(object sender, RoutedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(StationsListView.ItemsSource);
            view.GroupDescriptions.Clear();
        }
    }
}



        
