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
        }
        /// <summary>
        /// function that initialize the data structures
        /// </summary>
        internal static void Initialize()
        //assumption: the legal location is in israel:
        //Longitude: between 33 to 35
        //Latitude: between 31 to 33
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            int amount = rand.Next(2, 5);//random amount of any object
            {//base stations://-----------------------------------------------------------
                for (int i = 0; i < amount; i++)
                {
                    BaseStation s = new BaseStation() { IdNumber = rand.Next(), Name = string.Format($"station {i + 1}"), ChargeSlots= rand.Next(0, 4), Latitude = rand.Next(31, 33) + rand.NextDouble(), Longitude = rand.Next(33, 35) + rand.NextDouble() };;
                    stations.Add(s);
                }
            }
            { //Drones:--------------------------------------------------------------------
              //all the drones are avalilible
                amount = rand.Next(5, 10);
                for (int i = 0; i < amount; i++)
                {
                    Drone drone = new Drone() { IdNumber = rand.Next()+1, Model = string.Format("model" + (char)(rand.Next(0, 23) + 97)), MaxWeight = (WeightCategories)(rand.Next(1, 4)) };
                    Drones.Add(drone);

                }
            }
            {//customers:----------------------------------
                amount = rand.Next(10, 50);
                for (int i = 0; i < amount; i++)
                {
                    Customer customer = new Customer() { Id = rand.Next(), Name = string.Format($"{(char)(rand.Next(0, 23) + 97)}"), Phone = string.Format("050-" +(rand.Next(1111111, 9999999))), Latitude = rand.Next(31, 33) + rand.NextDouble(), Longitude = rand.Next(33, 35) + rand.NextDouble() };
                    Customers.Add(customer);
                }
            }
            {
                //parcels:--------------------------
                //all the parcels are not matched to a drone yet
                amount = rand.Next(10, 100);
                for (int i = 0; i < amount; i++)
                {
                    Parcel parcel = new Parcel() { IdNumber = Config.RunningNumber++, ClientSendName = rand.Next(), ClientGetName = rand.Next(), Weight = (WeightCategories)(rand.Next(1, 4)), Priority = (Priorities)(rand.Next(1, 4)) };
                    parcel.DroneId = 0;
                    parcel.CreateParcelTime = new DateTime(); //zero
                    parcel.MatchForDroneTime = new DateTime();//zero
                    parcel.collectingDroneTime = new DateTime();
                    parcel.ArrivingDroneTime = new DateTime();
                    Parcels.Add(parcel);

                }
            }
        }
    }
}
