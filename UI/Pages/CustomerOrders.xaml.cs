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
using BLAPI;

namespace UI.Pages
{
    /// <summary>
    /// Interaction logic for CustomerOrders.xaml
    /// </summary>
    public partial class CustomerOrders : Page
    {
        public CustomerOrders()
        {
            InitializeComponent();
            var temp = Enum.GetValues(typeof(BO.State));//אין אופציה של ללא
            State.ItemsSource = temp;
            var item = b.PredicateParcel(x => x.Geter == id).Select(x => x.IdNumber);
            item = item.Prepend("ללא");
            item = item.Distinct();
            Sender.ItemsSource = item;
            listViewOrders.ItemsSource = b.PredicateParcel(x=>x.Geter==id);
        }
        private IBL b;
        private string id;
        private BO.ParcelOfList selected;

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selected = (BO.ParcelOfList)listViewOrders.SelectedItem;
        }

        private void openToView(object sender, MouseButtonEventArgs e)
        {
            viewParcelWindow view = new viewParcelWindow(b, selected);
            view.Show();
        }
    }
}

