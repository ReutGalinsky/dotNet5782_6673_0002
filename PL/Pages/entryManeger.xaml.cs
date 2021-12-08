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
    /// Interaction logic for entryManeger.xaml
    /// </summary>
    public partial class entryManeger : Page
    {
        public entryManeger(BLApi.IBL bl)
        {
            InitializeComponent();
        }
        private BLApi.IBL bl;
        private void startclick(object sender, RoutedEventArgs e)
        {            //if()//אם הסיסמא והתז נכונים

            ManegerWindow maneger = new ManegerWindow(bl);
            var t = Window.GetWindow(this);
            maneger.Show();
            t.Close();
        }
    }
}
