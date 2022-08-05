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
        private BLApi.IBL bl;

        public CustomerEntry(BLApi.IBL b)
        {
            InitializeComponent();
            bl = b;
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (password.Password == "Password:")
                {
                    MessageBox.Show("The Password Was Not Entered", "Error-Password", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (user.Text == "Username:")
                {
                    MessageBox.Show("The UserName Was Not Entered", "Error-User", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                var User = bl.GetUser(user.Text);
                if(User.isManager==true)
                {
                    MessageBox.Show($"There is no customer with the id of {user.Text}");

                }
                if (User.UserPassword == password.Password)
                {
                    CustomerPage pagecustomer = new CustomerPage(bl, User.UserName);
                    pagecustomer.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show($"The password is not correct, please try again");
                }
            }
            catch (Exception)
            {
                MessageBox.Show($"There is no customer with the id of {user.Text}");
            }

        }
        private void move(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            TypeOfUser typeOfUser = new TypeOfUser(bl);
            typeOfUser.Show();
            this.Close();
        }
        private void focus(object sender, RoutedEventArgs e)
        {
            if (textPassword.Text == "Password:")
            {
                textPassword.Visibility = Visibility.Collapsed;
                password.Focus();
            }
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Tools.RemoveCharges(bl);
            this.Close();
        }
        private void showPassword(object sender, RoutedEventArgs e)
        {
            textPassword.Visibility = Visibility.Collapsed;
            password.Visibility = Visibility.Visible;
            password.Password = textPassword.Text;
            password.Focus();
        }
        private void showText(object sender, RoutedEventArgs e)
        {
            textPassword.Text = password.Password;
            textPassword.Visibility = Visibility.Visible;
            password.Visibility = Visibility.Collapsed;
            textPassword.Focus();
        }
        private void focusUser(object sender, RoutedEventArgs e)
        {
            if (user.Text == "Username:")
                user.Text = "";
        }
        private void Forget_Click(object sender, RoutedEventArgs e)
        {
            ForgetPassword forgetPassword = new ForgetPassword();
            forgetPassword.Show();
        }
        private void Account_Click(object sender, RoutedEventArgs e)
        {
            Account account = new Account(bl);
            account.ShowDialog();
        }

    }
}

