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
    /// Interaction logic for Drone.xaml
    /// </summary>
    public partial class Drone : Window
    {
        public Drone(IBL.IBL i)
        {
            InitializeComponent();
            main.Content = new newDrone(i);
        }
        public event EventHandler updateList;

        public Drone(IBL.BO.DroneToList a, IBL.IBL i)
        {
            InitializeComponent();
            main.Content = new actions(a,i);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void closingAction(object sender, EventArgs e)
        {
            updateList(this,EventArgs.Empty);
        }

    }
   
}
