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
    /// Interaction logic for ManagerBaseStation.xaml
    /// </summary>
    public partial class ManagerBaseStation : Page
    {
        public ManagerBaseStation(BLApi.IBL b)
        {
            InitializeComponent();
            bl = b;
            foreach(var item in bl.GetBaseStations())
            {
                liststations.Add(item);
            }
            StatiosListView.DataContext = liststations;
            chargeSlot.SelectedItem = "All";
        }
        public ObservableCollection<BO.BaseStationToList> liststations = new ObservableCollection<BO.BaseStationToList>();

        private BLApi.IBL bl;
        private BO.BaseStationToList selected;

        private void AddLineButton_Click(object sender, RoutedEventArgs e)
        {
            addingWindow add = new addingWindow(bl);
            add.ShowDialog();
        }
        private void updated(object sender, EventArgs e)//the event that will update the details of the listView
        {
            liststations.Clear();
        foreach (var item in bl.GetBaseStations())
                liststations.Add(item);
        }

    private void removeButton_Click(object sender, RoutedEventArgs e)
    {
        addButton.IsEnabled = false;
    }

    private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
    {
        selected = (BO.BaseStationToList)StatiosListView.SelectedItem;
    }

    private void viewItem(object sender, MouseButtonEventArgs e)
    {
        //viewStation view = new viewStation(bl, selected);
        //view.updateList += updated;
        //view.ShowDialog();
    }

    private void addStation(object sender, RoutedEventArgs e)
    {
        addingWindow add = new addingWindow(bl);

        add.ShowDialog();
    }

        private void reset_Click(object sender, RoutedEventArgs e)
        {

            liststations.Clear();
            foreach (var item in bl.GetBaseStations())
                liststations.Add(item);
        }

     

        private void listBaseStations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selected = (BO.BaseStationToList)StatiosListView.SelectedItem;

        }

            private void changeFilterCharge(object sender, SelectionChangedEventArgs e)
        {
        
            StatiosListView.ItemsSource = (string)chargeSlot.SelectedItem switch
            {
                "All" => bl.GetBaseStations(),
                "With available charging slots" => bl.GetAllBaseStationsBy(x => x.ChargeSlots > 0),
                _ => bl.GetAllBaseStationsBy(x => x.ChargeSlots == 0),
            };

        }
    }
}


//לבדוק על מחיקה
//להמשיך על סינון לפי עמדות טעינה
