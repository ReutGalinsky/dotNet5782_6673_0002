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
    /// Interaction logic for viewParcelWindow.xaml
    /// </summary>
    public partial class viewParcelWindow : Window
    {
        public viewParcelWindow(BLApi.IBL b,BO.ParcelOfList p)
        {
            InitializeComponent();
            bl = b;
            parcel = p;
            State.DataContext = parcel;
            IdGeter.DataContext = parcel;
            IdNumber.DataContext = parcel;
            IdSender.DataContext = parcel;
            Priority.DataContext = parcel;
            Weight.DataContext = parcel;
        }
        private BO.ParcelOfList parcel;
        private BLApi.IBL bl;

        private void AddLineButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
