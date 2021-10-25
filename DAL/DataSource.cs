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
        internal static List<BaseStation> stations = new List<BaseStation>();
        internal static List<Drone> Drones = new List<Drone>();
        internal static List<Customer> Customers = new List<Customer>();
        internal static List<Parcel> Parcels = new List<Parcel>();
        internal static List<DroneCharge> Charges = new List<DroneCharge>();
        internal class Config
        {
            internal static int RunningNumber = 1;
        }
        internal static void Initialize()
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            int amount = rand.Next(2, 5);
            {//base stations://-----------------------------------------------------------
                for (int i = 0; i < amount; i++)
                {
                    BaseStation s = new BaseStation() { IdNumber = rand.Next(), Name = string.Format($"station {i + 1}"), ChargeSlots= rand.Next(0, 4), Latitude = rand.Next(0, 180) + rand.NextDouble(), Longitude = rand.Next(0, 180) + rand.NextDouble() };
                    //s.IdNumber = rand.Next();
                    while (DalObject.getItem(s).IdNumber == 0)
                    {
                        s.IdNumber = rand.Next();
                    }
                    //bool falg = true;
                    //while (falg)
                    //{
                    //    falg = false;
                    //    foreach (var item in stations)
                    //    {
                    //        if (item.IdNumber == s.IdNumber)
                    //        {
                    //            falg = true;
                    //            break;
                    //        }
                    //    }
                    //}
                    //s.Name = string.Format($"name + {i+1}");
                    //s.ChargeSlots = rand.Next(0, 4);
                    //s.Latitude = rand.Next(0, 180) + rand.NextDouble();
                    //s.Longitude = rand.Next(0, 180) + rand.NextDouble();
                    stations.Add(s);
                }
            }
            { //Drones:---------------------------------------------------------------------------------
                amount = rand.Next(5, 10);
                for (int i = 0; i < amount; i++)
                {
                    Drone drone = new Drone() { IdNumber = rand.Next(), Model = string.Format("model" + (char)(rand.Next(0, 23) + 97)), MaxWeight = (WeightCategories)(rand.Next(1, 4)) , Battery = rand.Next(0, 101), Status = (DroneStatus.Available) };
                    //drone.IdNumber = rand.Next();
                    while (DalObject.getItem(drone).IdNumber == 0)
                    {
                        drone.IdNumber = rand.Next();
                    }
                    //bool falg = true;
                    //while (falg)
                    //{
                    //    falg = false;
                    //    foreach (var item in Drones)
                    //    {
                    //        if (item.IdNumber == drone.IdNumber)
                    //        {
                    //            falg = true;
                    //            break;
                    //        }
                    //    }
                    //}
                    //drone.Model = string.Format("model" + (char)(rand.Next(0, 23) + 97));
                    //drone.MaxWeight = (WeightCategories)(rand.Next(1, 4));
                    //drone.Battery = rand.Next(0, 100) + rand.NextDouble();
                    //drone.Status = (DroneStatus)(rand.Next(1, 4));
                    Drones.Add(drone);

                }
            }
            {//customers:----------------------------------
                amount = rand.Next(10, 50);
                for (int i = 0; i < amount; i++)
                {
                    Customer customer = new Customer() { Id = rand.Next(), Name = string.Format($"{(char)(rand.Next(0, 23) + 97)}"), Phone = string.Format("050-" +(rand.Next(1111111, 9999999))), Latitude = rand.Next(31, 33) + rand.NextDouble(), Longitude = rand.Next(33, 35) + rand.NextDouble() };
                    //customer.Id = rand.Next();
                    while (DalObject.getItem(customer).Id == 0)
                    {
                        customer.Id = rand.Next();
                    }
                    //bool falg = true;
                    //while (falg)
                    //{
                    //    falg = false;
                    //    foreach (var item in Customers)
                    //    {
                    //        if (item.Id == customer.Id)
                    //        {
                    //            falg = true;
                    //            break;
                    //        }
                    //    }
                    //}
                    //customer.Name = string.Format("name" + (char)(rand.Next(0, 23) + 97));
                    //customer.Phone = string.Format("050" + (char)(rand.Next(1111111, 9999999)));
                    //customer.Latitude = rand.Next(31, 33) + rand.NextDouble();
                    //customer.Longitude = rand.Next(33, 35) + rand.NextDouble();
                    Customers.Add(customer);
                }
            }
            {
                //parcels:--------------------------

                amount = rand.Next(10, 100);
                for (int i = 0; i < amount; i++)
                {
                    Parcel parcel = new Parcel() { IdNumber = Config.RunningNumber++, ClientSendName = rand.Next(), ClientGetName = rand.Next(), Weight = (WeightCategories)(rand.Next(1, 4)), Priority = (Priorities)(rand.Next(1, 4)) };
                    //parcel.IdNumber = Config.RunningNumber++;
                    //parcel.ClientSendName = rand.Next();
                    //parcel.ClientGetName = rand.Next();
                    //parcel.Weight = (WeightCategories)(rand.Next(1, 4));
                    //parcel.Priority = (Priorities)(rand.Next(1, 4));
                    parcel.DroneId = 0;
                    parcel.CreateParcelTime = new DateTime(); 
                    parcel.MatchForDroneTime = new DateTime();
                    parcel.collectingDroneTime = new DateTime();
                    parcel.ArrivingDroneTime = new DateTime();
                    Parcels.Add(parcel);

                }
            }
        }
    }
}
