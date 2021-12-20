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
    /// Interaction logic for AddCustomer.xaml
    /// </summary>
    public partial class AddCustomer : Window
    {
        public AddCustomer(BLApi.IBL b)
        {
            InitializeComponent();
            bl = b;
        }
        BLApi.IBL bl;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            //משהו כזה אבל בנתונים של לקוח
            //try
            //{
            //    BO.DroneToList d = new BO.DroneToList() { IdNumber = id.Text, MaxWeight = (IBL.BO.WeightCategories)weight.SelectedItem, Model = Model.Text };
            //    bl.AddDrone(d, Station.Text);
            //    var t = Window.GetWindow(this);
            //    t.Close();
            //}
            //catch (Exception)
            //{
            //    MessageBox.Show("can't add");
            //}
        }

        private void move(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();

        }
    }
}
