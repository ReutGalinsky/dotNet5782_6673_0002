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
using System.Windows.Media.Animation;
using PL.Pages;
namespace PL
{
    /// <summary>
    /// Interaction logic for CustomerPage.xaml
    /// </summary>
    public partial class CustomerPage : Window
    {
        public CustomerPage(BLApi.IBL b,string i)
        {
            InitializeComponent();
            bl = b;
            id = i;
            pageDelivery = new CustomerDelivery(bl,id);
            pageParcel = new CustomerParcel(bl, id);
            pagePersonal = new CustomerPersonalArea(bl, id);
        }

        BLApi.IBL bl;
        string id;
        private CustomerDelivery pageDelivery;
        private CustomerParcel pageParcel;
        private CustomerPersonalArea pagePersonal;
        private void ButtonParcel(object sender, RoutedEventArgs e)
        {
            pageParcel.update(sender,e);
            Customer.NavigationService.Navigate(pageParcel);
        }
        private void ButtonPersonalArea(object sender, RoutedEventArgs e)
        {
            Customer.NavigationService.Navigate(pagePersonal);

        }
        private void ButtonDelivery(object sender, RoutedEventArgs e)
        {
            pageDelivery.update(sender, e);
            Customer.NavigationService.Navigate(pageDelivery);
        }
        private void move(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();

        }
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

    }
}
