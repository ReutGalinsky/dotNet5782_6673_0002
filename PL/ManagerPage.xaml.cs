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
using System.Windows.Shapes;
using PL.Pages;
namespace PL
{
    /// <summary>
    /// Interaction logic for ManagerPage.xaml
    /// </summary>
    public partial class ManagerPage : Window
    {
        public ManagerPage(BLApi.IBL b)
        {
            InitializeComponent();
            bl = b;
            pageDrone = new ManagerDrone(bl);
            pageCustomer = new ManagerCustomer(bl);
            pageParcel = new ManagerParcel(bl);
            pageBaseStation = new ManagerBaseStation(bl);
        }
        private BLApi.IBL bl;
        private ManagerDrone pageDrone;
        private ManagerCustomer pageCustomer;
        private ManagerParcel pageParcel;
        private ManagerBaseStation pageBaseStation;
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            TypeOfUser typeOfUser = new TypeOfUser(bl);
            typeOfUser.Show();
            this.Close();
        }
        private void move(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            GridMenu.Width = 70;
        }
        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            GridMenu.Width = 200;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }
        private void BackToDrone(object sender, EventArgs e)
        {
            pageDrone.update(sender, e);
            Manager.NavigationService.Navigate(pageDrone);
        }
        private void BackToCustomer(object sender, EventArgs e)
        {
            pageCustomer.update(sender, e);
            Manager.NavigationService.Navigate(pageCustomer);
        }
        private void BackToBaseStation(object sender, EventArgs e)
        {
            pageBaseStation.update(sender, e);
            Manager.NavigationService.Navigate(pageBaseStation);
        }
        private void BackToParcel(object sender, EventArgs e)
        {
            pageParcel.update(sender, e);
            Manager.NavigationService.Navigate(pageParcel);
        }
        private void changedSelection(object sender, SelectionChangedEventArgs e)
        {
            if (ButtonCloseMenu.Visibility == Visibility.Visible)
            {
                var item = (sender as ListView).Items[(sender as ListView).SelectedIndex] as ListViewItem;
                switch (item.Tag)
                {
                    case "All drones":
                        pageDrone.update(sender, e);
                        Manager.NavigationService.Navigate(pageDrone);
                        break;
                    case "Add new drone":
                        ManagerAddDrone addDrone=new ManagerAddDrone(bl);
                        addDrone.updateList += BackToDrone;
                        Manager.NavigationService.Navigate(addDrone);
                        break;
                    case "All customers":
                        pageDrone.update(sender, e);
                        Manager.NavigationService.Navigate(pageDrone);
                        break;
                    case "Add new customer":
                        ManagerAddCustomer addCustomer = new ManagerAddCustomer(bl);
                        addCustomer.updateList += BackToCustomer;
                        Manager.NavigationService.Navigate(addCustomer);
                        break;
                    case "All base stations":
                        pageDrone.update(sender, e);
                        Manager.NavigationService.Navigate(pageDrone);
                        break;
                    case "Add new base station":
                        ManagerAddBaseStation addBaseStation = new ManagerAddBaseStation(bl);
                        addBaseStation.updateList += BackToBaseStation;
                        Manager.NavigationService.Navigate(addBaseStation);
                        break;
                    case "All parcels":
                        pageBaseStation.update(sender, e);
                        Manager.NavigationService.Navigate(pageBaseStation);
                        break;
                }
            }
        }
    }
}
