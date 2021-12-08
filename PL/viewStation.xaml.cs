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
using System.Collections.ObjectModel;

namespace PL
{
    /// <summary>
    /// Interaction logic for viewStation.xaml
    /// </summary>
    public partial class viewStation : Window
    {//להוסיף כיתוב של רחפנים
        private ObservableCollection<BO.Drone> drones=new ObservableCollection<BO.Drone>();
        public event EventHandler updateList;

        public viewStation(BLApi.IBL b, BO.BaseStationToList s)
        {
            InitializeComponent();
            station = s;
            BO.BaseStation temp = b.GetBaseStation(s.IdNumber);
            foreach (var item in temp.Drones)
            {
                drones.Add(b.GetDrone(item.IdNumber));
            }
            Drone.DataContext = drones;
        }
        private BO.BaseStationToList station;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            updateList(this,EventArgs.Empty);
            this.Close();
        }
    }
}
