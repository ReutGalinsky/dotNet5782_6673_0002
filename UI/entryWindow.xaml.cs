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
using UI.Pages;
using BLAPI;

namespace UI
{
    /// <summary>
    /// Interaction logic for entryWindow.xaml
    /// </summary>
    public partial class entryWindow : Window
    {
        public entryWindow(IBL b)
        {
            InitializeComponent();
            bl = b;
            frame.Content = new entryMenu(bl);

        }
        private BLAPI.IBL bl;

        private void returnButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow main = new MainWindow(bl);
            main.Show();
        }

        private void AddLineButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}