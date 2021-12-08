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
using System.Collections.ObjectModel;


namespace PL
{

    /// <summary>
    /// Interaction logic for ManegerStations.xaml
    /// </summary>
    public partial class ManegerStations : Window
    {
        //לבדוק על מחיקה
        //להמשיך על סינון לפי עמדות טעינה
        public ManegerStations(BLApi.IBL b)
        {
            InitializeComponent();
            bl = b;
            stations = new ObservableCollection<BO.BaseStationToList>();
            listBaseStations.DataContext = stations;
        }
        public ObservableCollection<BO.BaseStationToList> stations;
        private BLApi.IBL bl;
        private BO.BaseStationToList selected;
        private void AddLineButton_Click(object sender, RoutedEventArgs e)
        {
            addingWindow add = new addingWindow(bl);
            add.ShowDialog();
        }
        private void updated(object sender, EventArgs e)//the event that will update the details of the listView
        {
            stations.Clear();
            foreach (var item in bl.GetBaseStations())
                stations.Add(item);
        }
        private void getNorth(ObservableCollection<BO.BaseStation> a)
        {
           // a = b.GetBaseStationsPredicate(X=>X.Location.Latitude<...);
        }
        //private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if(chargeSlots.SelectedItem!=null)
        //    {
        //        switch (Location.SelectedItem) 
        //        {
        //            north=>getNorth(a);
        //        }
        //    }
        //    else
        //}

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            addButton.IsEnabled = false;
        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            selected = (BO.BaseStationToList)listBaseStations.SelectedItem;
        }

        private void viewItem(object sender, MouseButtonEventArgs e)
        {
            viewStation view = new viewStation(bl, selected);
            view.updateList += updated;
            view.ShowDialog();
        }

        private void addStation(object sender, RoutedEventArgs e)
        {
            addingWindow add = new addingWindow(bl);

            add.ShowDialog();
        }
    }
}
