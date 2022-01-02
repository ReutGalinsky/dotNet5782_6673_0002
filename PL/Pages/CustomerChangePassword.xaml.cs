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
        }
        public event EventHandler updateList;
        private BLApi.IBL bl;
        private string id;
        private void changePassword(object sender, RoutedEventArgs e)
        {
            try
            {
                var User = bl.GetUser(id);
                if(User.UserPassword==old.Text)
                {
                    if (newPassword.Text != "")
                    { bl.UpdatingDetailsOfUser(id, newPassword.Text);
                        return;
                    }
                   // throw new PL.ConnectionException("");
                }
               // throw new PL.ConnectionException("");
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
