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
            parcel = bl.GetParcel(id);
            Id.DataContext = parcel;
            SenderId.Text = parcel.SenderCustomer.IdNumber;
            SenderName.Text = parcel.SenderCustomer.Name;
            GeterId.Text = parcel.GeterCustomer.IdNumber;
            GeterName.Text = parcel.GeterCustomer.Name;
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
