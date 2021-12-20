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
    /// Interaction logic for CustomerDelivery.xaml
    /// </summary>
    public partial class CustomerDelivery : Page
    {
        public CustomerDelivery(BLApi.IBL b, string i)
        { 
            InitializeComponent();
            bl = b;
            id = i;
            parcels = new ObservableCollection<BO.ParcelOfList>();
            foreach (BO.ParcelOfList s in bl.GetAllParcelsBy(x=>x.Geter==id))//create the source for the liseView
                parcels.Add(s);
            Delivery.DataContext = parcels;
            State.ItemsSource = Enum.GetValues(typeof(BO.DroneState));
            Weight.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));
            var states = BO.ParcelState.GetNames(typeof(BO.ParcelState)).ToList();
        states.Insert(0, "all");
            State.ItemsSource = states;
            State.SelectedItem = "all";
            var weights = BO.WeightCategories.GetNames(typeof(BO.WeightCategories)).ToList();
        weights.Insert(0, "all");
            Weight.ItemsSource = weights;
            Weight.SelectedItem = "all";
        }
    private BLApi.IBL bl;
    string id;
    private BO.ParcelOfList selected;//selected item that will be send to the new window
        private ObservableCollection<BO.ParcelOfList> parcels;
    private void selectionChange(object sender, SelectionChangedEventArgs e)
    {
        selected = (BO.ParcelOfList)Delivery.SelectedItem;
    }
    private void Action(object sender, MouseButtonEventArgs e)//event for double clicking on specific item 
    {
        CustomerShowParcel showParcel = new CustomerShowParcel(bl, selected.IdNumber);
        showParcel.Show();
    }
    private void updated(object sender, EventArgs e)//the event that will update the details of the listView
    {
        Weight.SelectedItem = "all";
        State.SelectedItem = "all";
            parcels.Clear();
            foreach (BO.ParcelOfList s in bl.GetAllParcelsBy(x => x.Geter == id))//create the source for the liseView
                parcels.Add(s);
            Delivery.ItemsSource = parcels;
            var current = Window.GetWindow(this);
        current.Opacity = 1;
    }
    private void addParcel(object sender, RoutedEventArgs e)
    {
        addParcel addWindow = new addParcel(bl, id);
        addWindow.updateList += updated;
        addWindow.ShowDialog();

    }
    private void deleteParcel(object sender, RoutedEventArgs e)
    {
        try
        {
            BO.ParcelOfList ParcelToDelte = ((sender as Button).DataContext) as BO.ParcelOfList;
            MessageBox.Show($"delete {ParcelToDelte.IdNumber}");
        }
        catch (Exception ex)
        {

        }
    }

    private void State_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
            parcels.Clear();
            Delivery.ItemsSource = item switch
            {
                null => check switch
                {
                    null => bl.GetAllParcelsBy(x => x.Geter == id),
                    _ => bl.GetAllParcelsBy(x => x.ParcelState == (BO.ParcelState)check && x.Geter == id),
                },
                _ => check switch
                {
                    null => bl.GetAllParcelsBy(x => x.Weight == (BO.WeightCategories)item && x.Geter == id),
                    _ => bl.GetAllParcelsBy(x => x.Weight == (BO.WeightCategories)item && x.ParcelState == (BO.ParcelState)check && x.Geter == id),
                },
            };

        }

    private void Weight_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (Weight.SelectedItem == null || State.SelectedItem == null)
        {
            return;
        }
        object item;
        Enum.TryParse(typeof(BO.WeightCategories), Weight.SelectedItem.ToString(), out item);
        object check;
        Enum.TryParse(typeof(BO.ParcelState), State.SelectedItem.ToString(), out check);
            parcels.Clear();
            Delivery.ItemsSource = item switch
            {
                null => check switch
                {
                    null => bl.GetAllParcelsBy(x => x.Sender == id),
                    _ => bl.GetAllParcelsBy(x => x.ParcelState == (BO.ParcelState)check && x.Geter == id),
                },
                _ => check switch
                {
                    null => bl.GetAllParcelsBy(x => x.Weight == (BO.WeightCategories)item && x.Geter == id),
                    _ => bl.GetAllParcelsBy(x => x.Weight == (BO.WeightCategories)item && x.ParcelState == (BO.ParcelState)check && x.Geter == id),
                },
            };
        }
}

        
 }
 


