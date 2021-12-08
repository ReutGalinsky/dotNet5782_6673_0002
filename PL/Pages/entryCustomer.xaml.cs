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
    /// Interaction logic for entryCustomer.xaml
    /// </summary>
    public partial class entryCustomer : Page
    {
        public entryCustomer(BLApi.IBL b)
        {
            InitializeComponent();
            bl = b;
        }
        BLApi.IBL bl;
      
        private void logInButton(object sender, RoutedEventArgs e)
        {
            //if()//אם הסיסמא והתז נכונים
            CustomerWindow customer = new CustomerWindow(bl,user.Text);
            customer.Show();
            var t = Window.GetWindow(this);
            t.Close();
        }

        private void OnlyNumbers(object sender, KeyEventArgs e)
        {
            Tools.TextBox_OnlyNumbers_PreviewKeyDown(sender, e);
        }
    }
}
