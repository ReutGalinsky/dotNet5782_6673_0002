﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace Dal
{
    class DataSource
    {
        #region Lists
        internal static List<BaseStation> Stations = new List<BaseStation>();
        internal static List<Drone> Drones = new List<Drone>();
        internal static List<Customer> Customers = new List<Customer>();
        internal static List<Parcel> Parcels = new List<Parcel>();
        internal static List<DroneCharge> Charges = new List<DroneCharge>();
        internal static List<User> Users = new List<User>();

        #endregion

        #region ConfigClass
        internal class Config
        {
            internal static int RunningNumber = 1;
            internal static double Light { set; get; }
            internal static double Heavy { set; get; }
            internal static double Available { set; get; }
            internal static double Medium { set; get; }
            internal static double _speed { set; get; }
        }
        #endregion

        #region Initialize
        /// <summary>
        /// Initializing the lists with some items
        /// </summary>
        internal static void Initialize()

        //assumption: the legal location is in israel:
        //Latitude: between 31 to 33
        //Longitude: between 33 to 35
        {//assumption: we suppose to set the values by ourself
            Config._speed = 10;
            Config.Available = 0.1;
            Config.Heavy = 0.4;
            Config.Medium = 0.3;
            Config.Light = 0.2;
            Drones.Add(new Drone() { IdNumber = "1", MaxWeight = WeightCategories.Heavy, Model = "XP5H" });
            Drones.Add(new Drone() { IdNumber = "2", MaxWeight = WeightCategories.Heavy, Model = "XP5H" });
            Drones.Add(new Drone() { IdNumber = "3", MaxWeight = WeightCategories.Light, Model = "XP5H" });
            Drones.Add(new Drone() { IdNumber = "4", MaxWeight = WeightCategories.Middle, Model = "XP5H" });
            Stations.Add(new BaseStation() { IdNumber = "5", ChargeSlots = 2, Latitude = 32.6, Longitude = 35.3, Name = "Afula" });
            Stations.Add(new BaseStation() { IdNumber = "6", ChargeSlots = 4, Latitude = 31.7, Longitude = 35.2, Name = "Jerusalem" });
            Stations.Add(new BaseStation() { IdNumber = "7", ChargeSlots = 1, Latitude = 31.2, Longitude = 34.8, Name = "Beer Sheva" });
            Customers.Add(new Customer() { IdNumber = "8", Latitude = 32.5, Longitude = 35.4, Name = "yosi", Phone = "05793148" });
            Customers.Add(new Customer() { IdNumber = "9", Latitude = 31.7, Longitude = 35.2, Name = "dani", Phone = "05478216" });
            Customers.Add(new Customer() { IdNumber = "10", Latitude = 31.8, Longitude = 34.7, Name = "haim", Phone = "05021481" });
            Customers.Add(new Customer() { IdNumber = "11", Latitude = 29.6, Longitude = 34.8, Name = "david", Phone = "059465842" });
            Customers.Add(new Customer() { IdNumber = "12", Latitude = 30.6, Longitude = 34.8, Name = "ben", Phone = "05789541" });
            Customers.Add(new Customer() { IdNumber = "11235813", Latitude = 30.6, Longitude = 34.8, Name = "ben", Phone = "05789541" });
            Parcels.Add(new Parcel()
            {
                IdNumber = Config.RunningNumber++.ToString(),
                DroneId = "4",
                Geter = "8",
                Weight = WeightCategories.Light,
                Priority = Priorities.Regular,
                Sender = "11",
                ArrivingDroneTime = null,
                MatchForDroneTime = DateTime.Now,
                CollectingDroneTime = null,
                CreateParcelTime = DateTime.Now,
            });
            Parcels.Add(new Parcel()
            {
                IdNumber = Config.RunningNumber++.ToString(),
                DroneId = "3",
                Geter = "9",
                Weight = WeightCategories.Light,
                Priority = Priorities.Emergency,
                Sender = "10",
                ArrivingDroneTime = null,
                MatchForDroneTime = DateTime.Now,
                CollectingDroneTime = null,
                CreateParcelTime = DateTime.Now,
            });
            Parcels.Add(new Parcel()
            {
                IdNumber = Config.RunningNumber++.ToString(),
                DroneId = null,
                Geter = "12",
                Weight = WeightCategories.Heavy,
                Priority = Priorities.Regular,
                Sender = "8",
                ArrivingDroneTime = null,
                MatchForDroneTime = null,
                CollectingDroneTime = null,
                CreateParcelTime = DateTime.Now,
            });
            Parcels.Add(new Parcel()
            {
                IdNumber = Config.RunningNumber++.ToString(),
                DroneId = null,
                Geter = "9",
                Weight = WeightCategories.Middle,
                Priority = Priorities.Regular,
                Sender = "8",
                ArrivingDroneTime = null,
                MatchForDroneTime = null,
                CollectingDroneTime = null,
                CreateParcelTime = DateTime.Now,
            });
            Users.Add(new User()
            {
                 isManager=false,
                  UserName="9",
                  UserPassword="yosiyosi",
            });
            Users.Add(new User()
            {
                isManager = true,
                UserName = "nurit",
                UserPassword = "nuritnurit",
            }); Users.Add(new User()
            {
                isManager = true,
                UserName = "osnat",
                UserPassword = "osnatosnat",
            }); Users.Add(new User()
            {
                isManager = false,
                UserName = "11235813",
                UserPassword = "fibonacci",
            });

            #endregion
        }
    }
}