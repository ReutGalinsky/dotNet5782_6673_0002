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
using System.Collections.ObjectModel;


namespace PL.Pages
{
    /// <summary>
    /// Interaction logic for CustomerParcel.xaml
    /// </summary>
    public partial class CustomerParcel : Page
    {
        public CustomerParcel(BLApi.IBL b, string i)
        {
            InitializeComponent();
            bl = b;
            id = i;
            foreach (BO.ParcelOfList s in bl.GetAllParcelsBy(x=>x.Sender==id))//create the source for the liseView
                listParcels.Add(s);
            ParcelListView.DataContext = listParcels;
            State.ItemsSource = Enum.GetValues(typeof(BO.DroneState));
        }
        private BLApi.IBL bl;
        string id;
        private BO.ParcelOfList selected;//selected item that will be send to the new window
        private ObservableCollection<BO.ParcelOfList> listParcels = new ObservableCollection<BO.ParcelOfList>();
        

       private void reset_Click(object sender, RoutedEventArgs e)
        {
            listParcels.Clear();
                foreach (var item in bl.GetParcels())
                listParcels.Add(item);
                State.SelectedItem = null;
          }

        private void selectionChange(object sender, SelectionChangedEventArgs e)
        {
            selected = (BO.ParcelOfList)ParcelListView.SelectedItem;
        }
        private void Action(object sender, MouseButtonEventArgs e)//event for double clicking on specific item 
        {
            CustomerShowParcel showParcel = new CustomerShowParcel(bl,selected.IdNumber);
            showParcel.Show();
        }
        private void updated(object sender, EventArgs e)//the event that will update the details of the listView
        {
            Idi.SelectedItem = "all";
            State.SelectedItem = "all";
            listParcels.Clear();
            foreach (BO.ParcelOfList s in bl.GetAllParcelsBy(x=>x.Sender==id))//create the source for the liseView
                listParcels.Add(s);
            ParcelListView.ItemsSource = listParcels;
            var current = Window.GetWindow(this);
            current.Opacity = 1;
        }
        private void addParcel(object sender, RoutedEventArgs e)
        {
            addParcel addWindow = new addParcel(bl,id);
            addWindow.updateList += updated;
            addWindow.ShowDialog();

        }
    }

        
 }
 