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
        public TypeOfUser(BLApi.IBL b)
        {
            InitializeComponent();
            bl = b;
        }
        private BLApi.IBL bl;

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

        private void move(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
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
            this.Close();
        }
        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            MediaElement1.Visibility = Visibility.Hidden;
        }
        private void Play_Click(object sender, RoutedEventArgs e)
        {
            MediaElement1.Visibility = Visibility.Visible;
            MediaElement1.Source = new Uri(@"C:\Users\osnat\OneDrive\שולחן העבודה\new\PL\bin\Debug" + @"\video.mp4");
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void MediaElement1_MediaEnded(object sender, RoutedEventArgs e)
        {

        }
    }
}








