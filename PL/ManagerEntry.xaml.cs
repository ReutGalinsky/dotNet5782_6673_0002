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
using System.Data.OleDb;
using System.ComponentModel;


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
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                check.IsChecked = false;
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
            this.Close();
        }
        private void Check_Click(object sender, RoutedEventArgs e)
        {
        
             

        }

        private void focusPassword(object sender, RoutedEventArgs e)
        {

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
    }
}
