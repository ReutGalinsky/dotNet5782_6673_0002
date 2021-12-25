﻿using System;
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
            chargeSlot.SelectedItem = chargeSlot.Items[0];
            update += updated;
        }

        public ObservableCollection<BO.BaseStationToList> liststations = new ObservableCollection<BO.BaseStationToList>();
        private BLApi.IBL bl;
        private BO.BaseStationToList selected;
        public EventHandler update;
        private void updated(object sender, EventArgs e)//the event that will update the details of the listView
        {
            chargeSlot.SelectedItem = chargeSlot.Items[0];
            liststations.Clear();
            foreach (var item in bl.GetBaseStations())
            {
                liststations.Add(item);
            }
        }
        private void addBaseStation(object sender, RoutedEventArgs e)
        {
            AddBaseStation baseStation = new AddBaseStation(bl);
            baseStation.updateList += updated;
            baseStation.ShowDialog();
        }
        private void Action(object sender, MouseButtonEventArgs e)//event for double clicking on specific item 
        {
            if (selected != null)
            {
                ManagerViewBaseStation showBase = new ManagerViewBaseStation(bl, selected.IdNumber);
                showBase.updateList += updated;
                showBase.Show();
            }
        }
        private void changeFilterCharge(object sender, SelectionChangedEventArgs e)
        {
            var comboBoxItem = (sender as ComboBox).Items[(sender as ComboBox).SelectedIndex] as ComboBoxItem;
            liststations.Clear();
            switch(comboBoxItem.Content.ToString())
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
        private void selectionChange(object sender, SelectionChangedEventArgs e)
        {
            selected = (BO.BaseStationToList)StationsListView.SelectedItem;
        }
        private void deleteStation(object sender, RoutedEventArgs e)
        {
            var dialogResult = MessageBox.Show($"are you sure?", "Delede Station", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (dialogResult == MessageBoxResult.Yes)
            {
                try
                {
                    BO.BaseStationToList stationToDelete = ((sender as Button).DataContext) as BO.BaseStationToList;
                    bl.RemoveBaseStation(stationToDelete.IdNumber);
                    update(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n you shuld try again later", "error", MessageBoxButton.OK, MessageBoxImage.Hand);

                }
            }
        }
    }
}



        
