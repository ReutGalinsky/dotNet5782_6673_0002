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
    //    /// <summary>
    //    /// Interaction logic for Drone.xaml
    //    /// </summary>
    public partial class Drone : Window
    {
        public Drone(BLApi.IBL i)//ctor for adding new drone
        {
            InitializeComponent();
            main.Content = new newDrone(i);
        }
        public event EventHandler updateList;

        public Drone(BO.DroneToList a, BLApi.IBL i)//ctor for action on exsiting drone
        {
            InitializeComponent();
            main.Content = new actions(a, i);
        }
        private void Button_Click(object sender, RoutedEventArgs e)//event for close button
        {
            this.Close();
        }

        private void closingAction(object sender, EventArgs e)
        //            //function that power an event every time that the window is being closed
        {
                        updateList(this,EventArgs.Empty);
        }
    }
   }

//}
