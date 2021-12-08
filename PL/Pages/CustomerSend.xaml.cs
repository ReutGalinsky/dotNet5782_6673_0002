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

namespace PL.Pages
{
    /// <summary>
    /// Interaction logic for CustomerSend.xaml
    /// </summary>
    public partial class CustomerSend : Page
    {
        public CustomerSend()
        {
            InitializeComponent();
            var temp=Enum.GetValues(typeof(BO.ParcelState));//אין אופציה של ללא
            State.ItemsSource = temp;
            var item= b.PredicateParcel(x => x.Sender == id).Select(x => x.IdNumber);
           item= item.Prepend("ללא");
            item = item.Distinct();
            Geter.ItemsSource = item;
        }
        private BLApi.IBL b;
        private string id;
    }
}
