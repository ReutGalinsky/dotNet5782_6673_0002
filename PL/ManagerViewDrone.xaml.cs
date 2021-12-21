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
            Model.DataContext = drone;
            Battery.DataContext = drone;
            State.DataContext = drone;
            Latitude.Text = drone.Location.Latitude.ToString();
            Longitude.Text = drone.Location.Longitude.ToString();
            
        }
        private BLApi.IBL bl;
        private string id;
        private BO.Drone drone;
        public event EventHandler updateList;

        /// <summary>
        /// ///להוסיפ שורה של כל החבילות
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void closing(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.UpdatingDetailsOfDrone(drone.Model, drone.IdNumber);
                MessageBox.Show($"the drone number {drone.IdNumber} updated successfully!");
                updateList(sender, e);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"the model {Model.Text} is illegal. please enter again", "Model Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void move(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();

        }
    }
}
