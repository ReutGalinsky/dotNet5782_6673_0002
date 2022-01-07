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
    /// Interaction logic for ManagerAddDrone.xaml
    /// </summary>
    public partial class ManagerAddDrone : Page
    {
        public ManagerAddDrone(BLApi.IBL b)
        {
            InitializeComponent();


                bl = b;
                ADD.IsEnabled = false;
                Weight.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));
                Id.DataContext = drone;
                Model.DataContext = drone;
                Weight.DataContext = drone;
                Weight.SelectedItem = Weight.Items[0];
                stationNumber.DataContext = bl.GetAllBaseStationsBy(x => x.ChargeSlots > 0);
            }

            private BLApi.IBL bl;
            private BO.DroneToList drone = new BO.DroneToList();
            public event EventHandler updateList;

            private void Close_Click(object sender, RoutedEventArgs e)//event of the cancel button
            {
            updateList(sender, e);
            }

            private void TextBox_OnlyNumbers_PreviewKeyDown(object sender, KeyEventArgs e)
            {
                TextBox text = sender as TextBox;
                if (text == null) return;
                if (e == null) return;

                if (e.Key < Key.NumPad0 || e.Key > Key.NumPad9)
                {
                    //allow get out of the text box
                    if (e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Tab)
                        return;

                    //allow list of system keys (add other key here if you want to allow)
                    if (e.Key == Key.Escape || e.Key == Key.Back || e.Key == Key.Delete ||
                        e.Key == Key.CapsLock || e.Key == Key.LeftShift || e.Key == Key.Home
                     || e.Key == Key.End || e.Key == Key.Insert || e.Key == Key.Down || e.Key == Key.Right)
                        return;

                    char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);

                    //allow control system keys
                    if (Char.IsControl(c)) return;

                    //allow digits (without Shift or Alt)
                    if (Char.IsDigit(c))
                        if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))
                            return; //let this key be written inside the textbox

                    //forbid letters and signs (#,$, %, ...)
                    e.Handled = true; //ignore this key. mark event as handled, will not be routed to other controls
                    return;
                }
            }

            private void OnlyNumbers_PreviewKeyDown(object sender, KeyEventArgs e)//enable to enter only digits
            {
                TextBox_OnlyNumbers_PreviewKeyDown(sender, e);
            }

            private void focus(object sender, TextChangedEventArgs e)
            {
                if (drone.IdNumber == "" || drone.Model == "" || stationNumber.SelectedItem ==null||Weight.SelectedItem==null)
                    ADD.IsEnabled = false;
                else
                {
                    ADD.IsEnabled = true;
                }

            }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.AddDrone(drone, (stationNumber.SelectedItem as BO.BaseStationToList).IdNumber);
                Close_Click(sender,e);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
    }



