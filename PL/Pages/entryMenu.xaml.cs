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

namespace PL.Pages
{
    /// <summary>
    /// Interaction logic for entryMenu.xaml
    /// </summary>
    public partial class entryMenu : Page
    {
        public entryMenu(BLApi.IBL b)
        {
            InitializeComponent();
            bl = b;
        }
        private void changeColor(object sender, MouseEventArgs e)
        {
            manegerIcon.Opacity = 50;

        }

        private BLApi.IBL bl;
        private void customerButton_Click(object sender, RoutedEventArgs e)
        {
            frame.NavigationService.Navigate(null);//??
        }

        private void manegerButton_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new Pages.entryManeger(bl);
            NavigationService.Navigate(null);

        }
    }
}

