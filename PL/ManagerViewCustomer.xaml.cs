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
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for ManagerViewCustomer.xaml
    /// </summary>
    public partial class ManagerViewCustomer : Window
    {
        public ManagerViewCustomer(BLApi.IBL b, string i)
        { 
            InitializeComponent();
            bl = b;
            id = i;
            customer = bl.GetCustomer(id);
            Id.DataContext = customer;
            Phone.DataContext = customer;
            Name.DataContext = customer;
            Longitude.DataContext = customer.Location;
            Latitude.DataContext = customer.Location;
        }
        private BLApi.IBL bl;
        private BO.Customer customer;
        private string id;
        public event EventHandler updateList;
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void move(object sender, MouseButtonEventArgs e)
        {
            
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.UpdatingDetailsOfCustomer(customer.IdNumber, customer.Name, customer.Phone);
                MessageBox.Show($"the base station number {customer.IdNumber} updated successfully!");
                updateList(sender, e);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }
        private void OnlyNumbers(object sender, KeyEventArgs e)
        {
            Tools.TextBox_OnlyNumbers_PreviewKeyDown(sender, e);
        }
    }
}
