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

namespace PL
{
    /// <summary>
    /// Interaction logic for TypeOfUser.xaml
    /// </summary>
    public partial class TypeOfUser : Window
    {
        public TypeOfUser(BLApi.IBL b)
        {
            InitializeComponent();
            bl = b;
        }
        private BLApi.IBL bl;

        private void managerButton_Click(object sender, RoutedEventArgs e)
        {
            ManagerPage entry = new ManagerPage(bl);
            entry.Show();
            this.Close();

        }

        private void customerButton_Click(object sender, RoutedEventArgs e)
        {
            CustomerPage entry = new CustomerPage(bl,"8");//רק לצורך הדוגמא
            entry.Show();
            this.Close();
        }
    }
}







   
