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
            //Customer.Content = new Pages.CustomerParcel(bl,id);
            Customer.NavigationService.Navigate(pageParcel);
        }
private void ButtonPersonalArea(object sender, RoutedEventArgs e)
        {
            //Customer.Content = new Pages.CustomerPersonalArea(bl,id);
            Customer.NavigationService.Navigate(pagePersonal);

        }
        private void AddLineButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonDelivery(object sender, RoutedEventArgs e)
        {
            //Customer.Content = new Pages.CustomerDelivery(bl,id);
            Customer.NavigationService.Navigate(pageDelivery);

        }
        private void Button_Click(object sender, RoutedEventArgs e)
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
    }
}
