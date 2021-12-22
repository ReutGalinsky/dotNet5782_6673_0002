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
    /// Interaction logic for ManagerEntry.xaml
    /// </summary>
    public partial class ManagerEntry : Window
    {
        public ManagerEntry(BLApi.IBL b)
        {
            InitializeComponent();
            bl = b;
        }
        private BLApi.IBL bl;
        private void startclick(object sender, RoutedEventArgs e)
        {
            try
            {
                var User = bl.GetUser(user.Text);
                if (User.UserPassword == password.Password && User.isManager == true)
                {
                    ManagerPage pageManager = new ManagerPage(bl);
                    pageManager.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show($"there is no manager with the id of {User.UserName}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"there is no manager with the id of {user.Text}");
            }
        }

        private void focus(object sender, RoutedEventArgs e)
        {
            if (user.Text == "שם משתמש")
                user.Text = "";
        }

        private void focusPassword(object sender, RoutedEventArgs e)
        {
            passwordText.Visibility = Visibility.Collapsed;
            password.Focus();

        }
    }
}
