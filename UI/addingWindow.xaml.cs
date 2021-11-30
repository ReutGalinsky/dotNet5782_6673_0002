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

namespace UI
{
    /// <summary>
    /// Interaction logic for addingWindow.xaml
    /// </summary>
    public partial class addingWindow : Window
    {
        public addingWindow()
        {
            InitializeComponent();
        }

        private void AddLineButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
