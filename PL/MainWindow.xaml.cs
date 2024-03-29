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
using BL;

namespace PL
{
    public partial class MainWindow : Window
    {    
        private BLApi.IBL bl;

        public MainWindow()
        {
            InitializeComponent();
            bl = BLApi.BLFactory.GetBl();

        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Tools.RemoveCharges(bl);
            this.Close();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            TypeOfUser entry = new TypeOfUser(bl);
            entry.Show();
            this.Close();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}

