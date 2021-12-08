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

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class main : Window
    {
        public main()
        {
            InitializeComponent();
            //לאתחל bl
        }
        private BLApi.IBL bl;
        private void changeColor(object sender, MouseEventArgs e)
        {
            startButton.Opacity = 50;
        }

        private void returnColor(object sender, MouseEventArgs e)
        {
            startButton.Opacity = 100;
        }

        private void closeButton(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddLineButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void startclick(object sender, RoutedEventArgs e)
        {
            entryWindow entry = new entryWindow(bl);
            entry.Show();
            this.Close();
        }
    }
}
