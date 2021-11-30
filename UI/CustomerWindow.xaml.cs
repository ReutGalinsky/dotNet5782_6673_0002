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
using System.Windows.Shapes;
using BLAPI;
namespace UI
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        public CustomerWindow(IBL b,string i)
        {
            InitializeComponent();
            bl = b;
            id = i;
        }
        private IBL bl;
        private string id;

        private void AddLineButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateWindow update = new UpdateWindow(bl, id);
            update.Show();
        }

        private void addNewParcel(object sender, RoutedEventArgs e)
        {
             update = new UpdateWindow(bl, id);
            update.Show();
        }
    }
}
