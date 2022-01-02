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
using PL.MenuItems;

namespace PL
{
    /// <summary>
    /// Interaction logic for CustomerPage.xaml
    /// </summary>
    public partial class CustomerPage : Window
    {
        public CustomerPage(BLApi.IBL b, string i)
        {
            InitializeComponent();
            bl = b;
            id = i;
            pageDelivery = new CustomerDelivery(bl, id);
            pageParcel = new CustomerParcel(bl, id);
            pagePersonal = new CustomerPersonalArea(bl, id);
        }
        BLApi.IBL bl;
        string id;
        private CustomerDelivery pageDelivery;
        private CustomerParcel pageParcel;
        private CustomerPersonalArea pagePersonal;
        private void move(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        private void BackToPersonal(object sender, EventArgs e)
        {
            Customer.NavigationService.Navigate(pagePersonal);
        }
        private void BackToDelivery(object sender, EventArgs e)
        {
            pageDelivery.update(sender, e);
            Customer.NavigationService.Navigate(pageDelivery);
        }
        private void BackToParcel(object sender, EventArgs e)
        {
            pageParcel.update(sender, e);
            Customer.NavigationService.Navigate(pageParcel);
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            TypeOfUser typeOfUser = new TypeOfUser(bl);
            typeOfUser.Show();
            this.Close();
        }
        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            GridMenu.Width = 60;
        }
        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            GridMenu.Width = 215;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }
        private void closeSubPages()
        {
            AddDelivery.Visibility = Visibility.Hidden;
            Password.Visibility = Visibility.Hidden;
        }
        private void changedSelection(object sender, SelectionChangedEventArgs e)
        {
                var item = (sender as ListView).Items[(sender as ListView).SelectedIndex] as ListViewItem;
                switch (item.Tag)
                {
                    case "PersonalArea":
                        Customer.NavigationService.Navigate(pagePersonal);
                        closeSubPages();
                        Password.Visibility = Visibility.Visible;
                        break;
                    case "ChangePassword":
                        CustomerChangePassword ChangePassword = new CustomerChangePassword(bl, id);
                        ChangePassword.updateList += BackToPersonal;
                        Customer.NavigationService.Navigate(ChangePassword);
                        break;
                    case "AllDeliveries":
                        pageDelivery.update(sender, e);
                        Customer.NavigationService.Navigate(pageDelivery);
                        closeSubPages();
                        AddDelivery.Visibility = Visibility.Visible;
                        break;
                    case "AddNewDelivery":
                        CustomerAddDelivery addDelivery = new CustomerAddDelivery(bl, id);
                        addDelivery.updateList += BackToDelivery;
                        Customer.NavigationService.Navigate(addDelivery);
                        break;
                    case "AllParcels":
                        pageParcel.update(sender, e);
                        Customer.NavigationService.Navigate(pageParcel);
                        closeSubPages();
                        break;
                }
            
        }
    }
}


