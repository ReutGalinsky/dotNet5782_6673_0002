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
using PL.Pages;
namespace PL
{
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
            Tools.RemoveCharges(bl);
            this.Close();
        }
        private void LogOut_Click(object sender, RoutedEventArgs e)
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
            GridMenu.Width = 60;
        }
        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            GridMenu.Width = 200;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }
        private void BackToDrone(object sender, EventArgs e)
        {
            MenuListView.SelectedItem = MenuListView.Items[0];
        }
        private void BackToCustomer(object sender, EventArgs e)
        {
            MenuListView.SelectedItem = MenuListView.Items[2];
        }
        private void BackToBaseStation(object sender, EventArgs e)
        {
            MenuListView.SelectedItem = MenuListView.Items[4];
        }
        private void closeSubPages()
        {
            AddDrone.Visibility = Visibility.Hidden;
            AddBaseStation.Visibility = Visibility.Hidden;
            AddCustomer.Visibility = Visibility.Hidden;
        }
        private void changedSelection(object sender, SelectionChangedEventArgs e)
        {
            var item = (sender as ListView).Items[(sender as ListView).SelectedIndex] as ListViewItem;
            switch (item.Tag)
            {
                case "All drones":
                    pageDrone.update(sender, e);
                    Manager.NavigationService.Navigate(pageDrone);
                    closeSubPages();
                    AddDrone.Visibility = Visibility.Visible;
                    break;
                case "Add new drone":
                    ManagerAddDrone addDrone = new ManagerAddDrone(bl);
                    addDrone.updateList += BackToDrone;
                    Manager.NavigationService.Navigate(addDrone);
                    break;
                case "All customers":
                    pageCustomer.update(sender, e);
                    Manager.NavigationService.Navigate(pageCustomer);
                    closeSubPages();
                    AddCustomer.Visibility = Visibility.Visible;
                    break;
                case "Add new customer":
                    ManagerAddCustomer addCustomer = new ManagerAddCustomer(bl);
                    addCustomer.updateList += BackToCustomer;
                    Manager.NavigationService.Navigate(addCustomer);
                    break;
                case "All base stations":
                    pageBaseStation.update(sender, e);
                    Manager.NavigationService.Navigate(pageBaseStation);
                    closeSubPages();
                    AddBaseStation.Visibility = Visibility.Visible;
                    break;
                case "Add new base station":
                    ManagerAddBaseStation addBaseStation = new ManagerAddBaseStation(bl);
                    addBaseStation.updateList += BackToBaseStation;
                    Manager.NavigationService.Navigate(addBaseStation);
                    break;
                case "All parcels":
                    pageParcel.update(sender, e);
                    Manager.NavigationService.Navigate(pageParcel);
                    closeSubPages();
                    break;
            }

        }
    }
}
