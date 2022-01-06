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
    /// Interaction logic for CustomerChangePassword.xaml
    /// </summary>
    public partial class CustomerChangePassword : Page
    {
        public CustomerChangePassword(BLApi.IBL b,string i)
        {
            InitializeComponent();
            bl = b;
            id = i;
            user = bl.GetUser(id);
        }
        public event EventHandler updateList;
        private BO.User user;
        private BLApi.IBL bl;
        private string id;
        private void changePassword(object sender, RoutedEventArgs e)
        {
            try
            {
                if(user.UserPassword==old.Text)
                {
                    if (newPassword.Text != "")
                    { bl.UpdatingDetailsOfUser(id, newPassword.Text);
                        MessageBox.Show("Successfully Change");
                        updateList(sender, e);
                        return;
                    }

                    MessageBox.Show("illegal new password");
                    return;
                }
                MessageBox.Show("The Old Password Is Not Correct, Please Try Again");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void focus(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox).Text == "Old Password:" || (sender as TextBox).Text == "New Password:")
            {
                (sender as TextBox).Text = "";
            }
        }
    }
}
