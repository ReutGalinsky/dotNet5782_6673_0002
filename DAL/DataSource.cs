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
            internal static int RunningNumber = 1;// האם מתחיל מאפס?
        }
        internal static void Initialize()///////////////////////////////////////////
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            int amount = rand.Next(2, 5);
            {//base stations://לבדוק את התחום של קווי רוחב ואורך ואיך מציגים בבסיס 60-----------------------------
                for (int i = 0; i < amount; i++)
                {
                    BaseStation s = new BaseStation();
                    s.IdNumber = rand.Next();
                    bool falg = true;
                    while (falg)
                    {
                        falg = false;
                        foreach (var item in stations)
                        {
                            if (item.IdNumber == s.IdNumber)
                            {
                                falg = true;
                                break;
                            }
                        }
                    }
                    s.Name = string.Format("name" + rand.Next(0, 10));
                    s.ChargeSlots = rand.Next(0, 4);
                    s.Latitude = rand.Next(0, 180) + rand.NextDouble();
                    s.Longitude = rand.Next(0, 180) + rand.NextDouble();
                    stations.Add(s);
                }
            }
            { //Drones:---------------------------------------------------------------------------------
                amount = rand.Next(5, 10);
                for (int i = 0; i < amount; i++)
                {
                    Drone drone = new Drone();
                    drone.IdNumber = rand.Next();
                    bool falg = true;
                    while (falg)
                    {
                        falg = false;
                        foreach (var item in Drones)
                        {
                            if (item.IdNumber == drone.IdNumber)
                            {
                                falg = true;
                                break;
                            }
                        }
                    }
                    drone.Model = string.Format("model" + (char)(rand.Next(0, 23) + 97));
                    drone.MaxWeight = (WeightCategories)(rand.Next(1, 4));
                    drone.Battery = rand.Next(0, 100) + rand.NextDouble();
                    drone.Status = (DroneStatus)(rand.Next(1, 4));
                    Drones.Add(drone);

                }
            }
            {//customers:----------------------------------
                amount = rand.Next(10, 100);
                for (int i = 0; i < amount; i++)
                {
                    Customer customer = new Customer();
                    customer.Id = rand.Next();
                    bool falg = true;
                    while (falg)
                    {
                        falg = false;
                        foreach (var item in Customers)
                        {
                            if (item.Id == customer.Id)
                            {
                                falg = true;
                                break;
                            }
                        }
                    }
                    customer.Name = string.Format("name" + (char)(rand.Next(0, 23) + 97));
                    customer.Phone = string.Format("050" + (char)(rand.Next(1111111, 9999999)));
                    customer.Latitude = rand.Next(31, 33) + rand.NextDouble();
                    customer.Longitude = rand.Next(33, 35) + rand.NextDouble();
                    Customers.Add(customer);
                }
            }
            {
                //parcels:--------------------------

                amount = rand.Next(10, 1000);
                for (int i = 0; i < amount; i++)
                {
                    Parcel parcel = new Parcel();
                    parcel.IdNumber = Config.RunningNumber++;
                    parcel.ClientSendName = rand.Next();
                    parcel.ClientGetName = rand.Next();
                    parcel.Weight = (WeightCategories)(rand.Next(1, 4));
                    parcel.Priority = (Priorities)(rand.Next(1, 4));
                    parcel.DroneId = rand.Next();
                    parcel.CreateParcelTime = DateTime.Now; 
                    parcel.MatchForDroneTime = DateTime.Now;
                    parcel.collectingDroneTime = DateTime.Now;
                    parcel.ArrivingDroneTime = DateTime.Now;
                    Parcels.Add(parcel);

                }
            }
        }
    }
}
