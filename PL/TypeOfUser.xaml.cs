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
    /// Interaction logic for TypeOfUser.xaml
    /// </summary>
    public partial class TypeOfUser : Window
    {
        public TypeOfUser(BLApi.IBL b)
        {
            InitializeComponent();
            bl = b;
        }
        private BLApi.IBL bl;

        private void Manager_Click(object sender, RoutedEventArgs e)
        {
            //ManagerEntry entry = new ManagerEntry(bl);
            ManagerPage entry = new ManagerPage(bl);
            entry.Show();
            this.Close();
        }

        private void Customer_Click(object sender, RoutedEventArgs e)
        {
            CustomerEntry entry = new CustomerEntry(bl);//רק לצורך הדוגמא
            entry.Show();
            this.Close();
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
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}







   
