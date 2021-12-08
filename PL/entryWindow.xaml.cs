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
    /// Interaction logic for entryWindow.xaml
    /// </summary>
    public partial class entryWindow : Window
    {
        public entryWindow(BLApi.IBL b)
        {
            InitializeComponent();
            bl = b;
            // frame.Content = new entryMenu(bl);
            customerPage = new Pages.entryCustomer(bl);//לבדוק אם אפשר לפתוח רק בתוך הפונקציה
            manegerPages = new Pages.entryManeger(bl);
        }
        private BLApi.IBL bl;
        private Pages.entryCustomer customerPage;
        private Pages.entryManeger manegerPages;
        private Pages.entryMenu menu;

        private void returnButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow main = new MainWindow();
            main.Show();
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void manegerButton_Click(object sender, RoutedEventArgs e)
        {
            frame.NavigationService.Navigate(manegerPages);
        }

        private void customerButton_Click(object sender, RoutedEventArgs e)
        {
            frame.NavigationService.Navigate(customerPage);
        }
    }
}