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

        //******buttons*******
        private void ButtonDrone(object sender, RoutedEventArgs e)
        {
            pageDrone.update(sender, e);
            Manager.NavigationService.Navigate(pageDrone);
        }
        private void ButtonParcel(object sender, RoutedEventArgs e)
        {
            pageParcel.update(sender, e);
            Manager.NavigationService.Navigate(pageParcel);

        }
        private void ButtonCustomer(object sender, RoutedEventArgs e)
        {
            pageCustomer.update(sender, e);
            Manager.NavigationService.Navigate(pageCustomer);

        }
        private void ButtonBaseStation(object sender, RoutedEventArgs e)
        {
            pageBaseStation.update(sender, e);
            Manager.NavigationService.Navigate(pageBaseStation);

        }
        private void closingButton(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void returningButton(object sender, RoutedEventArgs e)
        {
            TypeOfUser typeOfUser = new TypeOfUser(bl);
            typeOfUser.Show();
            this.Close();

        }
        private void movingWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
