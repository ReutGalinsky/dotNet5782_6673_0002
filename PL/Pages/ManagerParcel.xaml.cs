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


        private void ComboBox_Weight(object sender, SelectionChangedEventArgs e)
        {
            if (Weight.SelectedItem == null || State.SelectedItem == null ||Priority.SelectedItem==null)
            {
                return;
            }
            object weight;
            var b = Weight.SelectedItem;
            Enum.TryParse(typeof(BO.WeightCategories), Weight.SelectedItem.ToString(), out weight);
            object state;
            Enum.TryParse(typeof(BO.ParcelState), State.SelectedItem.ToString(), out state);
            object priority;
            Enum.TryParse(typeof(BO.Priorities), State.SelectedItem.ToString(), out priority);

            listParcels.Clear();
            ParcelListView.ItemsSource = weight switch
            {
              
            };

        }

        private void ComboBox_State(object sender, SelectionChangedEventArgs e)
        {
            var item = Priority.SelectedItem;
            var check = Weight.SelectedItem;
            if (check == null)
            {
                ParcelListView.ItemsSource = item switch
                {
                    BO.ParcelState.Define => bl.GetAllParcelsBy(x => x.ParcelState == BO.ParcelState.Define),
                    BO.ParcelState.match => bl.GetAllParcelsBy(x => x.ParcelState == BO.ParcelState.match),
                    BO.ParcelState.pick => bl.GetAllParcelsBy(x => x.ParcelState == BO.ParcelState.pick),
                    BO.ParcelState.supply => bl.GetAllParcelsBy(x => x.ParcelState == BO.ParcelState.supply),

                    _ => bl.GetParcels(),
                };
            }
            else
            {
                ParcelListView.ItemsSource = item switch
                {
                    BO.ParcelState.Define => bl.GetAllParcelsBy(x => x.ParcelState == BO.ParcelState.Define&&x.Weight==(BO.WeightCategories)check),
                    BO.ParcelState.match => bl.GetAllParcelsBy(x => x.ParcelState == BO.ParcelState.match && x.Weight == (BO.WeightCategories)check),
                    BO.ParcelState.pick => bl.GetAllParcelsBy(x => x.ParcelState == BO.ParcelState.pick && x.Weight == (BO.WeightCategories)check),
                    BO.ParcelState.supply => bl.GetAllParcelsBy(x => x.ParcelState == BO.ParcelState.supply && x.Weight == (BO.WeightCategories)check),

                    _ => bl.GetParcels(),
                };
            }

        }

        private void ComboBox_Priority(object sender, SelectionChangedEventArgs e)
        {
                var item = State.SelectedItem;
                var check = Weight.SelectedItem;
                if (check == null)
                {
                    ParcelListView.ItemsSource = item switch
                    {
                        BO.Priorities.Emergency => bl.GetAllParcelsBy(x => x.Priority == BO.Priorities.Emergency),
                        BO.Priorities.Regular => bl.GetAllParcelsBy(x => x.Priority == BO.Priorities.Regular),
                        BO.Priorities.speed => bl.GetAllParcelsBy(x => x.Priority == BO.Priorities.speed),

                        _ => bl.GetParcels(),
                    };
                }
                else
                {
                    ParcelListView.ItemsSource = item switch
                    {
                    BO.Priorities.Emergency => bl.GetAllParcelsBy(x => x.Priority == BO.Priorities.Emergency && x.Weight == (BO.WeightCategories)check),
                        BO.Priorities.Regular => bl.GetAllParcelsBy(x => x.Priority == BO.Priorities.Regular && x.Weight == (BO.WeightCategories)check),
                        BO.Priorities.speed => bl.GetAllParcelsBy(x => x.Priority == BO.Priorities.speed && x.Weight == (BO.WeightCategories)check),

                        _ => bl.GetParcels(),
                    };
                }


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
    }
}


