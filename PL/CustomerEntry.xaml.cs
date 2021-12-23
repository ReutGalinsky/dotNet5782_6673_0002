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
    /// Interaction logic for CustomerEntry.xaml
    /// </summary>
    public partial class CustomerEntry : Window
    {
        public CustomerEntry(BLApi.IBL b)
        {
            InitializeComponent();
            bl = b;
        }
        private BLApi.IBL bl;

        private void logInButton(object sender, RoutedEventArgs e)
        {
            try
            {
                var User = bl.GetUser(user.Text);
                if (User.UserPassword==password.Password&&User.isManager==false)
                {
                    CustomerPage pagecustomer = new CustomerPage(bl,User.UserName);
                    pagecustomer.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show($"there is no customer with the id of {User.UserName}");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"there is no customer with the id of {user.Text}");
            }
        }
        private void OnlyNumbers(object sender, KeyEventArgs e)
        {
            Tools.TextBox_OnlyNumbers_PreviewKeyDown(sender, e);
        }

        private void focus(object sender, RoutedEventArgs e)
        {
            if (user.Text == "Enter Your Id:")
                user.Text = "";
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            TypeOfUser typeOfUser = new TypeOfUser(bl);
            typeOfUser.Show();
            this.Close();
        }
        private void focusPassword(object sender, RoutedEventArgs e)
        {
            passwordText.Visibility = Visibility.Collapsed;
            password.Focus();
        }
        private void move(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();

        }
    }
}
