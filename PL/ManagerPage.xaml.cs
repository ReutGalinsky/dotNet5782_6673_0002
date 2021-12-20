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
        BLApi.IBL bl;
        private ManagerDrone pageDrone;
        private ManagerCustomer pageCustomer;
        private ManagerParcel pageParcel;
        private ManagerBaseStation pageBaseStation;

        private void ButtonDrone(object sender, RoutedEventArgs e)
        {
            //Manager.Content = new Pages.ManagerDrone(bl);
            Manager.NavigationService.Navigate(pageDrone);
        }
        private void ButtonParcel(object sender, RoutedEventArgs e)
        {
            //Manager.Content = new Pages.ManagerParcel(bl);
            Manager.NavigationService.Navigate(pageParcel);

        }
        private void ButtonCustomer(object sender, RoutedEventArgs e)
        {
            //Manager.Content = new Pages.ManagerCustomer(bl);
            Manager.NavigationService.Navigate(pageCustomer);

        }
        private void AddLineButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            TypeOfUser typeOfUser = new TypeOfUser(bl);
            typeOfUser.Show();
            this.Close();
        }

        private void ButtonBaseStation(object sender, RoutedEventArgs e)
        {
            //Manager.Content = new Pages.ManagerBaseStation(bl);
            Manager.NavigationService.Navigate(pageBaseStation);

        }

        private void move(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();

        }
    }
}
