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
    /// Interaction logic for AddDrone.xaml
    /// </summary>
    public partial class AddDrone : Window
    {
        public AddDrone(BLApi.IBL b)
        {
            InitializeComponent();
            bl = b;
            ADD.IsEnabled = false;
            Weight.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));
            Id.DataContext = drone;
            Model.DataContext = drone;
            Weight.DataContext = drone;
        }

        private BLApi.IBL bl;
        private BO.DroneToList drone = new BO.DroneToList();
        public event EventHandler updateList;
        private void Add_Click(object sender, RoutedEventArgs e)//event of the adding button
        {
            try
            {
                bl.AddDrone(drone, Station.Text);
                updateList(sender, e);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)//event of the cancel button
        {
            //var t = Window.GetWindow(this);
            //updateList(sender, e);
            this.Close();
        }

        private void focusModel(object sender, RoutedEventArgs e)//event to define if the add button is availible
        {
            try
            {
                if (Model.Text == "")
                    Model.BorderBrush = Brushes.Red;
                else
                    Model.BorderBrush = Brushes.Gray;
            }
            catch (Exception ex)
            {
                Model.BorderBrush = Brushes.Red;
            }

            if (Id.Text != "" && Model.Text != "" && Weight.SelectedItem != null && Station.Text != "")
                ADD.IsEnabled = true;
            else
                ADD.IsEnabled = false;
        }

        private void focusStation(object sender, RoutedEventArgs e)//event to define if the add button is availible
        {
            try
            {
                if (int.Parse(Station.Text) == 0 || Station.Text == null)
                    Station.BorderBrush = Brushes.Red;
                else
                    Station.BorderBrush = Brushes.Gray;
            }
            catch (Exception ex)
            {
                Station.BorderBrush = Brushes.Red;
            }

            if (Id.Text != "" && Model.Text != "" &&Weight.SelectedItem != null && Station.Text != "")
            {

                ADD.IsEnabled = true;
            }
            else
            {
                ADD.IsEnabled = false;
            }
        }

        private void focusID(object sender, TextChangedEventArgs e)//event to define if the add button is availible
        {
            try
            {
                if (int.Parse(Id.Text) == 0 || Id.Text == "")
                    Id.BorderBrush = Brushes.Red;
                else
                    Id.BorderBrush = Brushes.Gray;
            }
            catch (Exception ex)
            {
                Id.BorderBrush = Brushes.Red;
            }
            if (Id.Text != "" && Model.Text != "" && Weight.SelectedItem != null && Station.Text != "")
                ADD.IsEnabled = true;
            else
            {
                ADD.IsEnabled = false;
            }
        }

        private void focusWeight(object sender, SelectionChangedEventArgs e)//event to define if the add button is availible
        {

            if (Id.Text != "" && Model.Text != "" && Station.Text != "")
                ADD.IsEnabled = true;
            else
            {
                ADD.IsEnabled = false;
            }
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

        private void move(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();

        }

     
    }
}

