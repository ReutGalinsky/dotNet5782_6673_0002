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
    /// Interaction logic for ManegerWindow.xaml
    /// </summary>
    public partial class ManegerWindow : Window
    {
        public ManegerWindow(BLApi.IBL b)
        {
            InitializeComponent();
            bl = b;
        }
        private BLApi.IBL bl;

        private void AddLineButton_Click(object sender, RoutedEventArgs e)
        {
            TypeOfUser entry = new TypeOfUser(bl);
            entry.Show();
            this.Close();
        }

        private void closeButton(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
//        private void Button_Click(object sender, RoutedEventArgs e)
//        {
//            ManegerStations stations = new ManagerStations(bl);
//            stations.Show();
//            this.Close();
//        }

//        private void customersClick(object sender, RoutedEventArgs e)
//        {
//            ManegerCustomers stations = new ManegerCustomers(bl);
//            stations.Show();
//            this.Close();
//        }

//        private void ParcelsClick(object sender, RoutedEventArgs e)
//        {
//            ManegerParcel stations = new ManegerParcel(bl);
//            stations.Show();
//            this.Close();
//        }

//        private void DronesClick(object sender, RoutedEventArgs e)
//        {
//            ManegerDrone stations = new ManegerDrone(bl);
//            stations.Show();
//            this.Close();
//        }
//    }
//}
