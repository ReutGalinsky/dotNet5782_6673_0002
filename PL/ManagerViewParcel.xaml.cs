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
using System.Globalization;

namespace PL
{
    /// <summary>
    /// Interaction logic for ManagerViewParcel.xaml
    /// </summary>
    public partial class ManagerViewParcel : Window
    {
        private BLApi.IBL bl;
        private string id;
        private BO.Parcel parcel;
        public ManagerViewParcel(BLApi.IBL b, string i)
        {
            InitializeComponent();
            bl = b;
            id = i;
            parcel = bl.GetParcel(id);
            Id.DataContext = parcel;
            SenderId.DataContext = parcel.SenderCustomer;
            SenderName.DataContext = parcel.SenderCustomer;
            GeterId.DataContext = parcel.GeterCustomer;
            GeterName.DataContext = parcel.GeterCustomer;
            PriorityBox.DataContext = parcel;
            WeightBox.DataContext = parcel;
            Create.DataContext = parcel;
            Match.DataContext = parcel;
            Arrive.DataContext = parcel;
            Collect.DataContext = parcel;
            buttonb.DataContext = parcel;
            Match.DataContext = parcel;
            matchLabel.DataContext = parcel;
            Collect.DataContext = parcel;
            collectLabel.DataContext = parcel;
            Arrive.DataContext = parcel;
            arriveLabel.DataContext = parcel;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void openDrone(object sender, RoutedEventArgs e)
        {
            ManagerViewDrone drone = new ManagerViewDrone(bl, parcel.Drone.IdNumber, true);
            drone.ShowDialog();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        private void openGeter(object sender, RoutedEventArgs e)
        {
            ManagerViewCustomer customer = new ManagerViewCustomer(bl, parcel.GeterCustomer.IdNumber, true);
            customer.ShowDialog();
        }
        private void openSender(object sender, RoutedEventArgs e)
        {
            ManagerViewCustomer customer = new ManagerViewCustomer(bl, parcel.SenderCustomer.IdNumber, true);
            customer.ShowDialog();
        }
    }
}
