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
    /// Interaction logic for ManagerParcel.xaml
    /// </summary>
    public partial class ManagerParcel : Page
    {
        public ManagerParcel(BLApi.IBL b)
        {
            InitializeComponent();
            bl = b;
            foreach (BO.ParcelOfList s in bl.GetParcels())//create the source for the liseView
                listParcels.Add(s);
            ParcelListView.DataContext = listParcels;
            var states = BO.ParcelState.GetNames(typeof(BO.ParcelState)).ToList();
            states.Insert(0, "all");
            State.ItemsSource = states;
            State.SelectedItem = "all";
            var priority = BO.Priorities.GetNames(typeof(BO.Priorities)).ToList();
            priority.Insert(0, "all");
            Priority.SelectedItem = "all";
            Priority.ItemsSource = priority;

        }
        private BLApi.IBL bl;

        private BO.ParcelOfList selected;
        private ObservableCollection<BO.ParcelOfList> listParcels = new ObservableCollection<BO.ParcelOfList>();

        private void ComboBox_State(object sender, SelectionChangedEventArgs e)
        {
            if (Priority.SelectedItem == null || State.SelectedItem == null)
            {
                return;
            }
            object item;
            Enum.TryParse(typeof(BO.Priorities), Priority.SelectedItem.ToString(), out item);
            object check;
            Enum.TryParse(typeof(BO.ParcelState), State.SelectedItem.ToString(), out check);
            listParcels.Clear();
            ParcelListView.ItemsSource = item switch
            {
                null => check switch
                {
                    null => bl.GetParcels(),
                    _ => bl.GetAllParcelsBy(x => x.ParcelState == (BO.ParcelState)check),
                },
                _ => check switch
                {
                    null => bl.GetAllParcelsBy(x => x.Priority == (BO.Priorities)item),
                    _ => bl.GetAllParcelsBy(x => x.Priority == (BO.Priorities)item && x.ParcelState == (BO.ParcelState)check),
                },
            };
        }


        private void reset_Click(object sender, RoutedEventArgs e)
        {

            listParcels.Clear();
            foreach (var item in bl.GetParcels())
                listParcels.Add(item);

        }

        private void selectionChange(object sender, SelectionChangedEventArgs e)
        {
            selected = (BO.ParcelOfList)ParcelListView.SelectedItem;
        }
        private void Action(object sender, MouseButtonEventArgs e)//event for double clicking on specific item 
        {
            ManagerViewParcel showParcel = new ManagerViewParcel(bl, selected.IdNumber);
            showParcel.Show();
        }

        private void deleteParcel(object sender, RoutedEventArgs e)
        {
            var dialogResult = MessageBox.Show($"are you sure?", "Delede Parcel", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                try
                {
                    BO.ParcelOfList ParcelToDelete = ((sender as Button).DataContext) as BO.ParcelOfList;
                    bl.RemoveParcel(ParcelToDelete.IdNumber);
                    ParcelListView.ItemsSource = bl.GetParcels();
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}


