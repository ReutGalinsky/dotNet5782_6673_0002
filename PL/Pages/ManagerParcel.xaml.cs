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
            var weights = BO.WeightCategories.GetNames(typeof(BO.WeightCategories)).ToList();
            weights.Insert(0, "all");
            Weight.ItemsSource = weights;
            Weight.SelectedItem = "all";
            var priority= BO.Priorities.GetNames(typeof(BO.Priorities)).ToList();
            priority.Insert(0, "all");
            Priority.SelectedItem = "all";
            Priority.ItemsSource = priority;

        }
        private BLApi.IBL bl;

        private BO.ParcelOfList selected;
        private ObservableCollection<BO.ParcelOfList> listParcels = new ObservableCollection<BO.ParcelOfList>();


        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ComboBox_Weight(object sender, SelectionChangedEventArgs e)
        {
            var item = Weight.SelectedItem;
            var check = Priority.SelectedItem;
            if (check == null)
            {
                
                ParcelListView.ItemsSource = item switch
                {
                    BO.WeightCategories.Heavy => bl.GetAllDronesBy(x => x.MaxWeight == BO.WeightCategories.Heavy),
                    BO.WeightCategories.Light => bl.GetAllDronesBy(x => x.MaxWeight == BO.WeightCategories.Light),
                    BO.WeightCategories.Middle => bl.GetAllDronesBy(x => x.MaxWeight == BO.WeightCategories.Middle),
                    _ => bl.GetDrones(),

                };
            }
            else
            {
                ParcelListView.ItemsSource = item switch
                {
                    BO.WeightCategories.Heavy => bl.GetAllDronesBy(x => ((x.MaxWeight == BO.WeightCategories.Heavy) && (x.State == (BO.DroneState)check))),
                    BO.WeightCategories.Light => bl.GetAllDronesBy(x => ((x.MaxWeight == BO.WeightCategories.Light) && (x.State == (BO.DroneState)check))),
                    BO.WeightCategories.Middle => bl.GetAllDronesBy(x => ((x.MaxWeight == BO.WeightCategories.Middle) && (x.State == (BO.DroneState)check))),
                    _ => bl.GetDrones(),
                };
            }
        }

        private void ComboBox_State(object sender, SelectionChangedEventArgs e)
        {
            var item = Priority.SelectedItem;
            var check = Weight.SelectedItem;
            if (check == null)
            {
                ParcelListView.ItemsSource = item switch
                {
                    BO.DroneState.Available => bl.GetAllDronesBy(x => x.State == BO.DroneState.Available),
                    BO.DroneState.maintaince => bl.GetAllDronesBy(x => x.State == BO.DroneState.maintaince),
                    BO.DroneState.shipping => bl.GetAllDronesBy(x => x.State == BO.DroneState.shipping),
                    _ => bl.GetDrones(),
                };
            }
            else
            {
                ParcelListView.ItemsSource = item switch
                {
                    BO.DroneState.Available => bl.GetAllDronesBy(x => ((x.State == BO.DroneState.Available) && (x.MaxWeight == (BO.WeightCategories)check))),
                    BO.DroneState.maintaince => bl.GetAllDronesBy(x => ((x.State == BO.DroneState.maintaince) && (x.MaxWeight == (BO.WeightCategories)check))),
                    BO.DroneState.shipping => bl.GetAllDronesBy(x => ((x.State == BO.DroneState.shipping) && (x.MaxWeight == (BO.WeightCategories)check))),
                    _ => bl.GetDrones(),
                };
            }

        }

        private void ComboBox_Priority(object sender, SelectionChangedEventArgs e)
        {

        }

        private void reset_Click(object sender, RoutedEventArgs e)
        {
        
                listParcels.Clear();
                foreach (var item in bl.GetParcels())
                listParcels.Add(item);
    
        }

        private void selectionChange(object sender, SelectionChangedEventArgs e)
        {

        }
        private void Action(object sender, MouseButtonEventArgs e)//event for double clicking on specific item 
        {
         
        }
    }
}


