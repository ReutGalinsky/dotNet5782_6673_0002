﻿using System;
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
using BLAPI;
namespace UI.Pages
{
    /// <summary>
    /// Interaction logic for entryManeger.xaml
    /// </summary>
    public partial class entryManeger : Page
    {
        public entryManeger(IBL bl)
        {
            InitializeComponent();
        }
        private IBL bl;

        private void changeColor(object sender, MouseEventArgs e)
        {
            login.Opacity = 50;
        }

        private void returnColor(object sender, MouseEventArgs e)
        {
            login.Opacity = 100;
        }
    }
}
