using System;
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
        internal static List<BaseStation> stations = new List<BaseStation>();
        internal static List<Drone> Drones = new List<Drone>();
        internal static List<Customer> Customers = new List<Customer>();
        internal static List<Parcel> Parcels = new List<Parcel>();
        internal static List<DroneCharge> Charges = new List<DroneCharge>();
        #endregion

        #region configClass
        internal class Config
        {
            internal static int RunningNumber = 1;//unique number for the parcels
            public static double light { set; get; }
            public static double heavy { set; get; }
            public static double available { set; get; }
            public static double medium { set; get; }
            public static double speed { set; get; }
        }
        #endregion

        #region Initialize
        internal static void Initialize()
        //assumption: the legal location is in israel:
        //Latitude: between 31 to 33
        //Longitude: between 33 to 35
        {//assumption: we suppose to set the values by ourself
            Config.speed = 10;
            Config.available = 0.1;
            Config.heavy = 0.4;
            Config.medium = 0.3;
            Config.light = 0.2;
            Drones.Add(new Drone() { IdNumber = "1", MaxWeight = WeightCategories.Heavy, Model = "XP5H" });
            Drones.Add(new Drone() { IdNumber = "2", MaxWeight = WeightCategories.Heavy, Model = "XP5H" });
            Drones.Add(new Drone() { IdNumber = "3", MaxWeight = WeightCategories.Light, Model = "XP5H" });
            Drones.Add(new Drone() { IdNumber = "4", MaxWeight = WeightCategories.Middle, Model = "XP5H" });
            stations.Add(new BaseStation() { IdNumber = "5", ChargeSlots = 2, Latitude = 32.6, Longitude = 35.3, Name = "Afula" });
            stations.Add(new BaseStation() { IdNumber = "6", ChargeSlots = 4, Latitude = 31.7, Longitude = 35.2, Name = "Jerusalem" });
            stations.Add(new BaseStation() { IdNumber = "7", ChargeSlots = 1, Latitude = 31.2, Longitude = 34.8, Name = "Beer Sheva" });
            Customers.Add(new Customer() { IdNumber = "8", Latitude = 32.5, Longitude = 35.4, Name = "yosi", Phone = "05793148" });
            Customers.Add(new Customer() { IdNumber = "9", Latitude = 31.7, Longitude = 35.2, Name = "dani", Phone = "05478216" });
            Customers.Add(new Customer() { IdNumber = "10", Latitude = 31.8, Longitude = 34.7, Name = "haim", Phone = "05021481" });
            Customers.Add(new Customer() { IdNumber = "11", Latitude = 29.6, Longitude = 34.8, Name = "david", Phone = "059465842" });
            Customers.Add(new Customer() { IdNumber = "12", Latitude = 30.6, Longitude = 34.8, Name = "ben", Phone = "05789541" });
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
                collectingDroneTime = null,
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
                collectingDroneTime = null,
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
                collectingDroneTime = null,
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
                collectingDroneTime = null,
                CreateParcelTime = DateTime.Now,
            });

            #endregion
        }
    }
}