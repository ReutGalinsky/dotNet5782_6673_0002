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
    /// Interaction logic for ManagerViewDrone.xaml
    /// </summary>
    public partial class ManagerViewDrone : Window
    {
        public ManagerViewDrone(BLApi.IBL b, string i)
        {
            InitializeComponent();
            bl = b;
            id = i;
            drone = bl.GetDrone(id);
            Id.DataContext = drone;
            MaxWeight.DataContext = drone;
            Battery.DataContext = drone;
            State.DataContext = drone;
            Latitude.DataContext = drone;
            Longitude.DataContext = drone;
        }
        private BLApi.IBL bl;
        private string id;
        private BO.Drone drone;
        /// <summary>
        /// ///להוסיפ שורה של כל החבילות
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void closing(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

       
    }
}
