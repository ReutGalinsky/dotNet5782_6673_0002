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
        public event EventHandler updateList;


        private void Close_Click(object sender, RoutedEventArgs e)
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
        private void Onlynumbers(object sender, KeyEventArgs e)
        {
            Tools.TextBox_OnlyNumbers_PreviewKeyDown(sender, e);
        }
        private void move(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();

        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {//להוסיף try להמרות
                BO.Customer customer = new BO.Customer() { Name = Name.Text, Phone=Phone.Text, IdNumber = Id.Text, Location = new BO.Location() { Latitude = double.Parse(Latitude.Text), Longitude = double.Parse(Longitude.Text) } };
                bl.AddCustomer(customer);
                updateList(sender, e);
                this.Close();
            }
            catch (Exception ex)//לטפל בחריגות
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
