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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL.Pages
{
    /// <summary>
    /// Interaction logic for ManagerAddBaseStation.xaml
    /// </summary>
    public partial class ManagerAddBaseStation : Page
    {
        public ManagerAddBaseStation(BLApi.IBL b)
        {
            InitializeComponent();
            bl = b;
                Name.DataContext = baseStation;
                Id.DataContext = baseStation;
            }
            private BLApi.IBL bl;
            private BO.BaseStation baseStation = new BO.BaseStation();
            public event EventHandler updateList;
            private void Add_Click(object sender, RoutedEventArgs e)
            {
                try
                {//להוסיף try להמרות
                    int temp;
                    if (int.TryParse(ChargeSlots.Text, out temp) == false)
                    {
                        MessageBox.Show("chargeSlots suppose to be integer");
                        return;
                    }
                    baseStation.ChargeSlots = temp;
                    double help;
                    if (double.TryParse(Latitude.Text, out help) == false)
                    {
                        MessageBox.Show("Latitude suppose to be double");
                        return;
                    }
                    baseStation.Location = new BO.Location();
                    baseStation.Location.Latitude = help;
                    if (double.TryParse(Longitude.Text, out help) == false)
                    {
                        MessageBox.Show("Longitude suppose to be double");
                        return;
                    }
                    baseStation.Location.Longitude = help;
                    bl.AddBaseStation(baseStation);
                    updateList(sender, e);
                }
                catch (Exception ex)//לטפל בחריגות
                {
                    MessageBox.Show(ex.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            private void Onlynumbers(object sender, KeyEventArgs e)
            {
                Tools.TextBox_OnlyNumbers_PreviewKeyDown(sender, e);
            }
         
            private void Focus(object sender, TextChangedEventArgs e)
            {
                if (baseStation.IdNumber == "" || Longitude.Text == "" || Latitude.Text == "" || baseStation.Name == "" || ChargeSlots.Text == "")
                {
                    ADD.IsEnabled = false;
                }
                else
                    ADD.IsEnabled = true;


            }

}
    }
