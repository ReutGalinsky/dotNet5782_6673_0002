using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
namespace DalObject
{
    class DataSource
    {
        /// <summary>
        /// the data structs for out data base
        /// </summary>
        internal static List<BaseStation> stations = new List<BaseStation>();
        internal static List<Drone> Drones = new List<Drone>();
        internal static List<Customer> Customers = new List<Customer>();
        internal static List<Parcel> Parcels = new List<Parcel>();
        internal static List<DroneCharge> Charges = new List<DroneCharge>();
        /// <summary>
        /// the inner class with the initialization
        /// </summary>
        internal class Config
        {
            internal static int RunningNumber = 1;//unique number for the parcels
            public static double light { set; get; }
            public static double heavy { set; get; }
            public static double available { set; get; }
            public static double medium { set; get; }
            public static double speed { set; get; }
        }
        /// <summary>
        /// function that initialize the data structures
        /// </summary>
        internal static void Initialize()
        //assumption: the legal location is in israel:
        //Longitude: between 33 to 35
        //Latitude: between 31 to 33
        {
            Config.speed = 0.05;
            Config.available = 0.05;
            Config.heavy = 0.05;
            Config.medium = 0.05;
            Config.light = 0.05;
            //Drones.Add(new Drone() { IdNumber = "555", MaxWeight = WeightCategories.Heavy, Model = "XP5H" });
            //stations.Add(new BaseStation() { IdNumber = "222", ChargeSlots = 2, Latitude = 32, Longitude = 32, Name = "Herzelia" });
            //stations.Add(new BaseStation() { IdNumber = "333", ChargeSlots = 2, Latitude = 33, Longitude = 33, Name = "jerusalem" });
            //stations.Add(new BaseStation() { IdNumber = "444", ChargeSlots = 2, Latitude = 34, Longitude = 34, Name = "telAviv" });
            //Customers.Add(new Customer() { IdNumber = "333", Latitude = 32.5, Longitude = 31.5, Name = "yosi", Phone = "0550405" });
            //Customers.Add(new Customer() { IdNumber = "444", Latitude = 32.6, Longitude = 31.7, Name = "gali", Phone = "0548872" });
            Parcels.Add(new Parcel()
            {
                IdNumber = "333",
                DroneId = default(string),
                Geter = "333",
                Weight = WeightCategories.Heavy,
                Priority = Priorities.Regular,
                Sender = "444",
                ArrivingDroneTime = default(DateTime),
                MatchForDroneTime = default(DateTime),
                collectingDroneTime = default(DateTime),
                CreateParcelTime = DateTime.Now,
            });
            //Parcels.Add(new Parcel()
            //{
            //    IdNumber = "456",
            //    DroneId = default(string),
            //    Geter = "444",
            //    Weight = WeightCategories.Light,
            //    Priority = Priorities.Emergency,
            //    Sender = "333",
            //    ArrivingDroneTime = default(DateTime),
            //    MatchForDroneTime = default(DateTime),
            //    collectingDroneTime = default(DateTime),
            //    CreateParcelTime = DateTime.Now,
            //});


            //Random rand = new Random(DateTime.Now.Millisecond);
            //int amount = rand.Next(2, 5);//random amount of any object
            //{//base stations://-----------------------------------------------------------
            //    for (int i = 0; i < amount; i++)
            //    {
            //        BaseStation s = new BaseStation() { IdNumber = rand.Next().ToString(), Name = string.Format($"station {i + 1}"), ChargeSlots= rand.Next(0, 4), Latitude = rand.Next(31, 33) + rand.NextDouble(), Longitude = rand.Next(33, 35) + rand.NextDouble() };;
            //        stations.Add(s);
            //    }
            //}
            //{ //Drones:--------------------------------------------------------------------
            //  //all the drones are avalilible
            //    amount = rand.Next(5, 10);
            //    for (int i = 0; i < amount; i++)
            //    {
            //        Drone drone = new Drone() { IdNumber = rand.Next()+1.ToString(), Model = string.Format("model" + (char)(rand.Next(0, 23) + 97)), MaxWeight = (WeightCategories)(rand.Next(1, 4)) };
            //        Drones.Add(drone);

            //    }
            //}
            //{//customers:----------------------------------
            //    amount = rand.Next(10, 50);
            //    for (int i = 0; i < amount; i++)
            //    {
            //        Customer customer = new Customer() { IdNumber = rand.Next().ToString(), Name = string.Format($"{(char)(rand.Next(0, 23) + 97)}"), Phone = string.Format("050-" +(rand.Next(1111111, 9999999))), Latitude = rand.Next(31, 33) + rand.NextDouble(), Longitude = rand.Next(33, 35) + rand.NextDouble() };
            //        Customers.Add(customer);
            //    }
            //}
            //{
            //    //parcels:--------------------------
            //    //all the parcels are not matched to a drone yet
            //    amount = rand.Next(10, 100);
            //    for (int i = 0; i < amount; i++)
            //    {
            //        Parcel parcel = new Parcel() { IdNumber = Config.RunningNumber++.ToString(), Sender = rand.Next().ToString(), Geter = rand.Next().ToString(), Weight = (WeightCategories)(rand.Next(1, 4)), Priority = (Priorities)(rand.Next(1, 4)) };
            //        parcel.DroneId = default(string);
            //        parcel.CreateParcelTime = new DateTime(); //zero
            //        parcel.MatchForDroneTime = new DateTime();//zero
            //        parcel.collectingDroneTime = new DateTime();
            //        parcel.ArrivingDroneTime = new DateTime();
            //        Parcels.Add(parcel);

            // }
            // }
        }
    }
}
