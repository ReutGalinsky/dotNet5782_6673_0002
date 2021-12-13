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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        //private BLApi.IBL bl = BLApi.BLFactory.GetBl();

        //private void clickDrones(object sender, RoutedEventArgs e)//event for the drones button
        //{
        //    ListDrones drone = new ListDrones(bl);
        //    drone.Show();
        //}

        //private void closing_Click(object sender, RoutedEventArgs e)//event for the closing button
        //{
        //    this.Close();
        //}
        private BLApi.IBL bl;
        private void changeColor(object sender, MouseEventArgs e)
        {
        }

        private void returnColor(object sender, MouseEventArgs e)
        {
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
