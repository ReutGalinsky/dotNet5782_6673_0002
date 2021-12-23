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

    public partial class AddBaseStation : Window
    {
        //לנסות לחסום אפשרות במיקום להכנסת מספרים ונקודה בלבד
        //לחסום כפתור עד שמכניס הכל
        public AddBaseStation(BLApi.IBL b)
        {
            InitializeComponent();
            bl = b;

        }
        private BLApi.IBL bl;
        public event EventHandler updateList;


        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {//להוסיף try להמרות
                BO.BaseStation station = new BO.BaseStation() {   Name=Name.Text,ChargeSlots = int.Parse(ChargeSlots.Text), IdNumber = Id.Text, Location = new BO.Location() { Latitude = double.Parse(Latitude.Text), Longitude = double.Parse(Longitude.Text) } };
                bl.AddBaseStation(station);
                updateList(sender, e);
                this.Close();
            }
            catch (Exception ex)//לטפל בחריגות
            {
                MessageBox.Show(ex.Message);
            }
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
    }
          

            
  
}
