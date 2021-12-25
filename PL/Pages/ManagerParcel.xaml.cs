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
            ParcelListView.DataContext = listParcels;
            var states = BO.ParcelState.GetNames(typeof(BO.ParcelState)).ToList();
            states.Insert(0, "All");
            State.ItemsSource = states;
            State.SelectedItem = "All";
            var priority = BO.Priorities.GetNames(typeof(BO.Priorities)).ToList();
            priority.Insert(0, "All");
            Priority.SelectedItem = "All";
            Priority.ItemsSource = priority;
            update += updated;
        }
        private IEnumerable<IGrouping<BO.ParcelState,BO.ParcelOfList>> parcelsByState()
        {
               var list = from item in bl.GetParcels()
                       group item by item.ParcelState;
            return list;
        }
        private BLApi.IBL bl;
        public EventHandler update;
        private BO.ParcelOfList selected;
        private ObservableCollection<BO.ParcelOfList> listParcels = new ObservableCollection<BO.ParcelOfList>();
        private void updated(object sender, EventArgs e)//the event that will update the details of the listView
        {
            listParcels.Clear();
            foreach (var item in bl.GetParcels())
                listParcels.Add(item);
            Priority.SelectedItem = "All";
            State.SelectedItem = "All";
        }
        private void ComboBoxChange(object sender, SelectionChangedEventArgs e)
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
            IEnumerable<BO.ParcelOfList> temp;
            temp = item switch
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
            foreach (var obj in temp)
                listParcels.Add(obj);

        }
        private void selectionChange(object sender, SelectionChangedEventArgs e)
        {
            selected = (BO.ParcelOfList)ParcelListView.SelectedItem;
        }
        private void Action(object sender, MouseButtonEventArgs e)//event for double clicking on specific item 
        {
            if (selected != null)
            {
                ManagerViewParcel showParcel = new ManagerViewParcel(bl, selected.IdNumber);
                showParcel.ShowDialog();
            }
        }
        private void deleteParcel(object sender, RoutedEventArgs e)
        {
            var dialogResult = MessageBox.Show($"are you sure?", "Delede Parcel", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (dialogResult == MessageBoxResult.Yes)
            {
                try
                {
                    BO.ParcelOfList ParcelToDelete = ((sender as Button).DataContext) as BO.ParcelOfList;
                    bl.RemoveParcel(ParcelToDelete.IdNumber);
                    update(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message+"\n you shuld try again later","error" ,MessageBoxButton.OK, MessageBoxImage.Hand);

                }
            }
        }
    }
   
}


