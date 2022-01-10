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
    /// Interaction logic for ManagerCustomer.xaml
    /// </summary>
    public partial class ManagerCustomer : Page
    {
        public ManagerCustomer(BLApi.IBL b)
        {
            InitializeComponent();
            bl = b;
            CustomerListView.DataContext = listCustomers;
            OnWay.SelectedItem = OnWay.Items[0];
            name.IsChecked = true;
            update += updated;
        }

        public EventHandler update;
        private BLApi.IBL bl;
        private BO.CustomerToList selected;
        private ObservableCollection<BO.CustomerToList> listCustomers = new ObservableCollection<BO.CustomerToList>();
        private void selectionChange(object sender, SelectionChangedEventArgs e)
        {
            selected = (BO.CustomerToList)CustomerListView.SelectedItem;
        }
        private void Action(object sender, MouseButtonEventArgs e)
        {
            if (selected != null)
            {
                ManagerViewCustomer customer = new ManagerViewCustomer(bl, selected.IdNumber);
                customer.updateList += updated;
                customer.Show();
                selected = null;
            }
        }
        private void updated(object sender, EventArgs e)
        {
            listCustomers.Clear();
            OnWay.SelectedItem = "All";
            var temp = from item in bl.GetCustomers()
                       orderby item.Name
                       select item;
            foreach (BO.CustomerToList s in temp)
                listCustomers.Add(s);
            name.IsChecked = true;
        }
        private void deleteCustomer(object sender, RoutedEventArgs e)
        {
            var dialogResult = MessageBox.Show($"are you sure?", "Delete Customer", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (dialogResult == MessageBoxResult.Yes)
            {
                try
                {
                    BO.CustomerToList CustomerToDelte = ((sender as Button).DataContext) as BO.CustomerToList;
                    bl.RemoveCustomer(CustomerToDelte.IdNumber);
                    updated(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Delete Customer", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void idCheck(object sender, RoutedEventArgs e)
        {
            listCustomers.Clear();
            foreach (var item in bl.GetCustomers().OrderBy(x => int.Parse(x.IdNumber)))
                listCustomers.Add(item);
        }
        private void nameCheck(object sender, RoutedEventArgs e)
        {
            listCustomers.Clear();
            foreach (var item in bl.GetCustomers().OrderBy(x =>x.Name))
                listCustomers.Add(item);
        }
        private void OnWaySelector(object sender, SelectionChangedEventArgs e)
        {
            var item = OnWay.SelectedItem as ComboBoxItem;
            switch(item.Tag)
            {
                case "all":
                    updated(sender, e);
                    break;
                case "onWay":
                    listCustomers.Clear();
                    var temp = from customer in bl.GetAllCustomersBy(x=>x.ParcelOnTheWay>0||x.ParcelSendAndNotGet>0)
                               select customer;
                    foreach (BO.CustomerToList s in temp)//create the source for the liseView
                        listCustomers.Add(s);
                    name.IsChecked = false;
                    id.IsChecked = false;
                    break;
                case "notOnWay":
                    listCustomers.Clear();
                    var help = from customer in bl.GetAllCustomersBy(x => x.ParcelOnTheWay== 0 && x.ParcelSendAndNotGet == 0)
                               select customer;
                    foreach (BO.CustomerToList s in help)//create the source for the liseView
                        listCustomers.Add(s);
                    name.IsChecked = false;
                    id.IsChecked = false;
                    break;
                default:
                    break;
            }
        }

    }
}




