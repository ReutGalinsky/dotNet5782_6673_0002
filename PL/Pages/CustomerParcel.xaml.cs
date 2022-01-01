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
            foreach (BO.ParcelOfList s in bl.GetAllParcelsBy(x => x.Sender == id))//create the source for the liseView
                listParcels.Add(s);
            ParcelListView.DataContext = listParcels;
            State.ItemsSource = Enum.GetValues(typeof(BO.DroneState));
            Weight.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));
            var states = BO.ParcelState.GetNames(typeof(BO.ParcelState)).ToList();
            states.Insert(0, "All");
            State.ItemsSource = states;
            State.SelectedItem = "All";
            var weights = BO.WeightCategories.GetNames(typeof(BO.WeightCategories)).ToList();
            weights.Insert(0, "All");
            Weight.ItemsSource = weights;
            Weight.SelectedItem = "All";
            update += updated;
        }

        public EventHandler update;
        public event EventHandler updateList;

        private BLApi.IBL bl;
        string id;
        private BO.ParcelOfList selected;//selected item that will be send to the new window
        private ObservableCollection<BO.ParcelOfList> listParcels = new ObservableCollection<BO.ParcelOfList>();
        private void selectionChange(object sender, SelectionChangedEventArgs e)
        {
            selected = (BO.ParcelOfList)ParcelListView.SelectedItem;
        }
        private void Action(object sender, MouseButtonEventArgs e)//event for double clicking on specific item 
        {
            if (selected != null)
            {
                CustomerShowParcel showParcel = new CustomerShowParcel(bl, selected.IdNumber);
                showParcel.Show();
            }
        }
        private void updated(object sender, EventArgs e)//the event that will update the details of the listView
        {
            Weight.SelectedItem = "All";
            State.SelectedItem = "All";
            listParcels.Clear();
            foreach (BO.ParcelOfList s in bl.GetAllParcelsBy(x => x.Sender == id))//create the source for the liseView
                listParcels.Add(s);
        }
        private void addParcel(object sender, RoutedEventArgs e)
        {
            addParcel addWindow = new addParcel(bl, id);
            addWindow.updateList += updated;
            addWindow.ShowDialog();
        }
        private void deleteParcel(object sender, RoutedEventArgs e)
        {
            var dialogResult = MessageBox.Show($"are you sure?", "Delete Parcel", MessageBoxButton.YesNo, MessageBoxImage.Question);
            BO.ParcelOfList ParcelToDelte = ((sender as Button).DataContext) as BO.ParcelOfList;
            if (dialogResult == MessageBoxResult.Yes && ParcelToDelte.ParcelState == BO.ParcelState.Define)//לחשוב אם רוצים לכלול גם אופציה שנשלח רחפן אך לא אסף עדיין את החבילה
            {
                try
                {
                    bl.RemoveParcel(ParcelToDelte.IdNumber);
                    listParcels.Clear();
                    foreach (var item in bl.GetAllParcelsBy(x => x.Sender == id))
                        listParcels.Add(item);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }
        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (Weight.SelectedItem == null || State.SelectedItem == null)
            {
                return;
            }
            object item;
            var b = Weight.SelectedItem;
            Enum.TryParse(typeof(BO.WeightCategories), Weight.SelectedItem.ToString(), out item);
            object check;
            Enum.TryParse(typeof(BO.ParcelState), State.SelectedItem.ToString(), out check);
            listParcels.Clear();
            IEnumerable<BO.ParcelOfList> list;
            list = item switch
            {
                null => check switch
                {
                    null => bl.GetAllParcelsBy(x => x.Sender == id),
                    _ => bl.GetAllParcelsBy(x => x.ParcelState == (BO.ParcelState)check && x.Sender == id),
                },
                _ => check switch
                {
                    null => bl.GetAllParcelsBy(x => x.Weight == (BO.WeightCategories)item && x.Sender == id),
                    _ => bl.GetAllParcelsBy(x => x.Weight == (BO.WeightCategories)item && x.ParcelState == (BO.ParcelState)check && x.Sender == id),
                },
            };
            foreach (var obj in list)
                listParcels.Add(obj);
        }
    }      
 }
 