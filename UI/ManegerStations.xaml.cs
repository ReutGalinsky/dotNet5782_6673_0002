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
using BO;

namespace UI
{

    /// <summary>
    /// Interaction logic for ManegerStations.xaml
    /// </summary>
    public partial class ManegerStations : Window
    {
        public ManegerStations()
        {
            InitializeComponent();
            a = new ObservableCollection<BaseStation>();
            listBaseStations.DataContext = a;
        }
        public ObservableCollection<BaseStation> a;

        private void AddLineButton_Click(object sender, RoutedEventArgs e)
        {
            addingWindow add = new addingWindow();
            add.ShowDialog();
        }
        private void getNorth(ObservableCollection<BaseStation> a)
        {
            a = b.GetBaseStationsPredicate(X=>X.Location.Latitude<...);
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(chargeSlots.SelectedItem!=null)
            {
                switch (Location.SelectedItem) 
                {
                    north=>getNorth(a);
                }
            }
            else
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            addButton.IsEnabled = false;
        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
