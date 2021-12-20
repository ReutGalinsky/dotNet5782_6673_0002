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
            var temp = from item in bl.GetCustomers()
                       orderby item.Name
                       select item;
            foreach (BO.CustomerToList s in temp)//create the source for the liseView
                listCustomers.Add(s);
            CustomerListView.DataContext = listCustomers;       
        }
        private BLApi.IBL bl;
        private BO.CustomerToList selected;
        private ObservableCollection<BO.CustomerToList> listCustomers = new ObservableCollection<BO.CustomerToList>();


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddCustomer customer =new AddCustomer(bl);
            customer.ShowDialog();
        }
        private void reset_Click(object sender, RoutedEventArgs e)
        {

            listCustomers.Clear();
            foreach (var item in bl.GetCustomers())
                listCustomers.Add(item);
        }

        private void selectionChange(object sender, SelectionChangedEventArgs e)
        {    
                selected = (BO.CustomerToList)CustomerListView.SelectedItem;      
        }
        
        private void Action(object sender, MouseButtonEventArgs e)//event for double clicking on specific item 
        {
            ManagerViewCustomer customer= new ManagerViewCustomer(bl, selected.IdNumber);
            customer.Show();
        }
    }
    //צריך להוסיף:
    //קומבובוקס לסינון אנשים לפי איזה אופציה שנבחר
    //להציג רשימת לקוחות
    //לעשות פונקציה של לחיצה כפולה שפותחת חלון קטן עם כל הפרטים ואפשר לעדכן או למחוק
    //להוסיף חלון חדש שמוסיף לקוח
    //לעשות איפוס לסינונים

}




