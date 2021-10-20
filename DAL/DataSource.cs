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
            //internal static int FirstCustomer=0;
            //internal static int FirstParcel=0;
            //internal static int FirstDrone = 0;
            ////internal static int FirstBaseStation=0;
            internal static int RunningNumber = 0;// האם מתחיל מאפס?
        }
        internal static void Initialize()///////////////////////////////////////////
        {
            //int minimumClient=10,minimumDrone=5,minimumParcel=10,minimumBaseStation=2;
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
        }
    }
}
