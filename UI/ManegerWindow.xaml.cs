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
using BLAPI;
namespace UI
{
    /// <summary>
    /// Interaction logic for ManegerWindow.xaml
    /// </summary>
    public partial class ManegerWindow : Window
    {
        public ManegerWindow(IBL b)
        {
            InitializeComponent();
            bl = b;
        }
        private IBL bl;
    }
}
