//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Navigation;
//using System.Windows.Shapes;

//namespace PL
//{
//    /// <summary>
//    /// Interaction logic for newDrone.xaml
//    /// </summary>
//    public partial class newDrone : Page
//    {
//        public newDrone(IBL.IBL i)//ctor
//        {
//            InitializeComponent();
//            bl = i;
//            add.IsEnabled = false;
//            weight.ItemsSource = Enum.GetValues(typeof(IBL.BO.WeightCategories));
//        }
//        private IBL.IBL bl;

//        private void clickAdd(object sender, RoutedEventArgs e)//event of the adding button
//        {
//            try
//            {
//                IBO.DroneToList d = new IBL.BO.DroneToList() { IdNumber = id.Text, MaxWeight = (IBL.BO.WeightCategories)weight.SelectedItem, Model = Model.Text };
//                bl.AddDrone(d, Station.Text);
//                var t = Window.GetWindow(this);
//                t.Close();
//            }
//            catch (Exception)
//            {
//                MessageBox.Show("can't add");
//            }
//        }

//        private void Button_Click(object sender, RoutedEventArgs e)//event of the cancel button
//        {
//            var t = Window.GetWindow(this);
//            t.Close();
//        }

//        private void focusModel(object sender, RoutedEventArgs e)//event to define if the add button is availible
//        {
           
//            try
//            {
//                if (Model.Text == "")
//                    Model.BorderBrush = Brushes.Red;
//                else
//                    Model.BorderBrush = Brushes.Gray;
//            }
//            catch (Exception ex)
//            {
//                Model.BorderBrush = Brushes.Red;
//            }

//            if (id.Text != "" && Model.Text != "" && weight.SelectedItem != null && Station.Text != "")
//                add.IsEnabled = true;
//            else
//                add.IsEnabled = false;
//        }

//        private void focusStaton(object sender, RoutedEventArgs e)//event to define if the add button is availible
//        {
//            try
//            {
//                if (int.Parse(Station.Text) == 0 || Station.Text == null)
//                    Station.BorderBrush = Brushes.Red;
//                else
//                    Station.BorderBrush = Brushes.Gray;
//            }
//            catch (Exception ex)
//            {
//                Station.BorderBrush = Brushes.Red;
//            }

//            if (id.Text != "" && Model.Text != "" && weight.SelectedItem != null && Station.Text != "")
//            {

//                add.IsEnabled = true;
//            }
//            else
//            {
//                add.IsEnabled = false;
//            }
//        }

//        private void focusID(object sender, TextChangedEventArgs e)//event to define if the add button is availible
//        {
//            try
//            {
//                if (int.Parse(id.Text) == 0 || id.Text == "")
//                    id.BorderBrush = Brushes.Red;
//                else
//                    id.BorderBrush = Brushes.Gray;
//            }
//            catch(Exception ex)
//            {
//                id.BorderBrush = Brushes.Red;
//            }
//            if (id.Text != "" && Model.Text != "" && weight.SelectedItem != null && Station.Text != "")
//                add.IsEnabled = true;
//            else
//            {
//                add.IsEnabled = false;
//            }
//        }

//        private void focusSelected(object sender, SelectionChangedEventArgs e)//event to define if the add button is availible
//        {
            
//            if (id.Text != "" && Model.Text != "" && Station.Text != "")
//                add.IsEnabled = true;
//            else
//            {
//                add.IsEnabled = false;
//            }
//        }
//        private void TextBox_OnlyNumbers_PreviewKeyDown(object sender, KeyEventArgs e)
//        {
//            TextBox text = sender as TextBox;
//            if (text == null) return;
//            if (e == null) return;

//            if (e.Key < Key.NumPad0 || e.Key > Key.NumPad9)
//            {
//                //allow get out of the text box
//                if (e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Tab)
//                    return;

//                //allow list of system keys (add other key here if you want to allow)
//                if (e.Key == Key.Escape || e.Key == Key.Back || e.Key == Key.Delete ||
//                    e.Key == Key.CapsLock || e.Key == Key.LeftShift || e.Key == Key.Home
//                 || e.Key == Key.End || e.Key == Key.Insert || e.Key == Key.Down || e.Key == Key.Right)
//                    return;

//                char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);

//                //allow control system keys
//                if (Char.IsControl(c)) return;

//                //allow digits (without Shift or Alt)
//                if (Char.IsDigit(c))
//                    if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))
//                        return; //let this key be written inside the textbox

//                //forbid letters and signs (#,$, %, ...)
//                e.Handled = true; //ignore this key. mark event as handled, will not be routed to other controls
//                return;
//            }
//        }

//        private void OnlyNumbers_PreviewKeyDown(object sender, KeyEventArgs e)//enable to enter only digits
//        {
//            TextBox_OnlyNumbers_PreviewKeyDown(sender, e);
//        }
//    }
//}
