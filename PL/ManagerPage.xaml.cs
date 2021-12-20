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
        }
        BLApi.IBL bl;
        private void ButtonDrone(object sender, RoutedEventArgs e)
        {
            Manager.Content = new Pages.ManagerDrone(bl);
        }
        private void ButtonParcel(object sender, RoutedEventArgs e)
        {
            Manager.Content = new Pages.ManagerParcel(bl);
        }
        private void ButtonCustomer(object sender, RoutedEventArgs e)
        {
            Manager.Content = new Pages.ManagerCustomer(bl);
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
            Manager.Content = new Pages.ManagerBaseStation(bl);
        }
    }
}
