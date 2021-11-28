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

namespace PL
{
    /// <summary>
    /// Interaction logic for newDrone.xaml
    /// </summary>
    public partial class newDrone : Page
    {
        public newDrone(IBL.IBL i)
        {
            InitializeComponent();
            bl = i;
            weight.ItemsSource= Enum.GetValues(typeof(IBL.BO.WeightCategories));
        }
        private IBL.IBL bl;
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void clickAdd(object sender, RoutedEventArgs e)
        {
            try
            {
                IBL.BO.DroneToList d = new IBL.BO.DroneToList() { IdNumber = id.Text, MaxWeight = (IBL.BO.WeightCategories)weight.SelectedItem, Model = Model.Text };
                bl.AddDrone(d,Station.Text);
                var t = Window.GetWindow(this);
                t.Close();
            }
            catch (Exception )
            {
                MessageBox.Show("can't add");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {  
            var t = Window.GetWindow(this);
            t.Close();
        }
    }
}
