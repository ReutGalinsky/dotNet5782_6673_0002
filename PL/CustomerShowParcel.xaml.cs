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
    /// Interaction logic for CustomerShowParcel.xaml
    /// </summary>
    public partial class CustomerShowParcel : Window
    {
        public CustomerShowParcel(BLApi.IBL b,string i)
        {
            InitializeComponent();
            bl = b;
            id = i;
            try
            {
                parcel = bl.GetParcel(id);
            }
            catch (Exception)
            { MessageBox.Show("Error in loading the parcel, please try again later"); }

            Id.DataContext = parcel;
            SenderId.DataContext = parcel.SenderCustomer;
            SenderName.DataContext = parcel.SenderCustomer;
            GeterId.DataContext = parcel.GeterCustomer;
            GeterName.DataContext = parcel.GeterCustomer;
            PriorityBox.DataContext = parcel;
            WeightBox.DataContext = parcel;
            Create.DataContext = parcel;
            Match.DataContext = parcel;
            Arrive.DataContext = parcel;
            Collect.DataContext = parcel;
           
        }
        private BLApi.IBL bl;
        private string id;
        private BO.Parcel parcel;

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void move(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();

        }
    }
}
