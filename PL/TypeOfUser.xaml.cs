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
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace PL
{
    /// <summary>
    /// Interaction logic for TypeOfUser.xaml
    /// </summary>
    public partial class TypeOfUser : Window
    {
        private BLApi.IBL bl;

        public TypeOfUser(BLApi.IBL b)
        {
            InitializeComponent();
            bl = b;
        }

        private void Manager_Click(object sender, RoutedEventArgs e)
        {
            ManagerEntry entry = new ManagerEntry(bl);
            //ManagerPage entry = new ManagerPage(bl);
            entry.Show();
            this.Close();
        }

        private void Customer_Click(object sender, RoutedEventArgs e)
        {
            //CustomerEntry entry = new CustomerEntry(bl);//רק לצורך הדוגמא
            CustomerPage entry = new CustomerPage(bl, "8");
            entry.Show();
            this.Close();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Tools.RemoveCharges(bl);
            this.Close();
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            stop.Visibility = Visibility.Collapsed;
            play.Visibility = Visibility.Visible;
            conLabel.Visibility = Visibility.Visible;
            conArr.Visibility = Visibility.Visible;

            MediaElement1.IsMuted = true;
            MediaElement1.Visibility = Visibility.Hidden;
        }
        private void Play_Click(object sender, RoutedEventArgs e)
        {
            conLabel.Visibility = Visibility.Collapsed;
            conArr.Visibility = Visibility.Collapsed;
            play.Visibility = Visibility.Collapsed;
            stop.Visibility = Visibility.Visible;
            contacta.Visibility = Visibility.Hidden;
            contactb.Visibility = Visibility.Hidden;
            contact.Visibility = Visibility.Hidden;
            MediaElement1.IsMuted = false;
            MediaElement1.Visibility = Visibility.Visible;
            MediaElement1.Source = new Uri(@"C:\Users\רעות גלינסקי\source\repos\ReutGalinsky\dotNet5782_6673_0002\bin" + @"\video.mp4");
        }
        private void Information_Click(object sender, RoutedEventArgs e)
        {
            if (infob.Visibility == Visibility.Visible)
            {
                infoc.Visibility = Visibility.Hidden;
                infob.Visibility = Visibility.Hidden;
                infoa.Visibility = Visibility.Hidden;
                info.Visibility = Visibility.Hidden;

            }
            else {
                info.Visibility = Visibility.Visible;
                infoc.Visibility = Visibility.Visible;
                infob.Visibility = Visibility.Visible;
                infoa.Visibility = Visibility.Visible; }
        }
        private void Heart_Click(object sender, RoutedEventArgs e)
        {
            if (contacta.Visibility == Visibility.Visible)
            {
                contacta.Visibility = Visibility.Hidden;
                contactb.Visibility = Visibility.Hidden;
                contact.Visibility = Visibility.Hidden;
            }
            else
            {
                contactb.Visibility = Visibility.Visible;
                contacta.Visibility = Visibility.Visible; 
            contact.Visibility = Visibility.Visible;
            }
    } 
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
      
        private void PackIconMaterial_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            info.Visibility = Visibility.Visible;
            infob.Visibility = Visibility.Visible;
            infoc.Visibility = Visibility.Visible;

        }
        private void PackIconMaterial_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            contact.Visibility = Visibility.Visible;
            contactb.Visibility = Visibility.Visible;
            contacta.Visibility = Visibility.Visible;

        }

    }
}








