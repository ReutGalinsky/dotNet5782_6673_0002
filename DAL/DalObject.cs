using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using IDAL.DO;

namespace DalObject
{

    public class DalObject
    {
        public DalObject()
        {
            DataSource.Initialize();
        }
        /// <summary>
        /// function for adding new base station to the data base
        /// </summary>
        /// <param name="station">the new station the being added</param>
        /// <returns></returns>
        public void AddingBaseStation(BaseStation station)//צריך בכלל להחזיר רשימה? 
        {
            DataSource.stations.Add(station);
        }
        /// <summary>
        /// add new drone to the data base
        /// </summary>
        /// <param name="NewDrone">the new drone</param>
        /// <returns></returns>
        public void AddingDrone(Drone NewDrone)
        {
            DataSource.Drones.Add(NewDrone);
        }
        /// <summary>
        /// add new customer to the data base
        /// </summary>
        /// <param name="NewCustomer"></param>
        /// <returns></returns>
        public void addingCustomer(Customer NewCustomer)
        {
            DataSource.Customers.Add(NewCustomer);
        }
        /// <summary>
        /// add new Parcel to the data base
        /// </summary>
        /// <param name="NewParcel"></param>
        /// <returns></returns>
        public void AddingParcel(Parcel NewParcel)
        {
            NewParcel.IdNumber = DataSource.Config.RunningNumber++;
            DataSource.Parcels.Add(NewParcel);
        }
      
        /// <summary>
        /// function that return the customers list
        /// </summary>
        /// <returns></returns>
        public List<Customer> GetCustomers()//האם יש דרך שתהיה פונקציה אחת לכל ההדפסות פה או בתוכנית הראשית?
        {
            List<Customer> returnCustomer = new List<Customer>();
            foreach(var item in DataSource.Customers)
            {
                returnCustomer.Add(item);
            }
            return returnCustomer;
        }
        /// <summary>
        /// function that return list of drones
        /// </summary>
        /// <returns></returns>
        public List<Drone> GetDrones()//האם יש דרך שתהיה פונקציה אחת לכל ההדפסות פה או בתוכנית הראשית?
        {
            List<Drone> returnDrone = new List<Drone>();
            foreach (var item in DataSource.Drones)
            {
                returnDrone.Add(item);
            }
            return returnDrone;
        }
        /// <summary>
        /// /return list of the stations that are in the data base
        /// </summary>
        /// <returns></returns>
        public List<BaseStation> GetBaseStations()//האם יש דרך שתהיה פונקציה אחת לכל ההדפסות פה או בתוכנית הראשית?
        {
            List<BaseStation> returnStation = new List<BaseStation>();
            foreach (var item in DataSource.stations)
            {
                returnStation.Add(item);

            }
            return returnStation;
        }
        /// <summary>
        /// function that return copy of the parcels
        /// </summary>
        /// <returns></returns>
        public List<Parcel> GetParcels()//האם יש דרך שתהיה פונקציה אחת לכל ההדפסות פה או בתוכנית הראשית?
        {
            List<Parcel> returnParcel = new List<Parcel>();
            foreach (var item in DataSource.Parcels)
            {
                returnParcel.Add(item);
            }
            return returnParcel;
        }
        /// <summary>
        /// return list of the non matched parcels
        /// </summary>
        /// <returns></returns>
        public List<Parcel> GetNonMatchParcels()//האם יש דרך שתהיה פונקציה אחת לכל ההדפסות פה או בתוכנית הראשית?
        {
            List<Parcel> NonMatch = new List<Parcel>();
            foreach (var item in DataSource.Parcels)
            {
                if(item.DroneId==0)//האם יש דרך יותר יפה ומדוייקת?
                {
                    NonMatch.Add(item);
                }
            }
            return NonMatch;
        }
        /// <summary>
        /// return list of the stations with availible charge slots
        /// </summary>
        /// <returns></returns>
        public List<BaseStation> GetAvailibeStation()
        {
            List<BaseStation> Availible = new List<BaseStation>();
            foreach(var item in DataSource.stations)
            {
                if (item.ChargeSlots != 0)
                    Availible.Add(item);
            }
            return Availible;
        }
        public void ParcelToDrone()
        {
            Console.WriteLine("please enter the number of parcel you want to connect with drone");
            int numP = Console.Read();
            Parcel temp1 = new Parcel();
            foreach (var item in DataSource.Parcels)
            {
                if (item.IdNumber == numP)
                {
                    temp1 = item;
                    break;
                }
            }

            Console.WriteLine("please enter the number of the wanted drone");
            int numD = Console.Read();
            Drone temp2 = new Drone();
            foreach (var item in DataSource.Drones)
            {
                if (item.IdNumber == numD)
                {
                    temp2 = item;
                    break;
                }
            }
           temp2.Status = (DroneStatus.Shipping);
           temp1.CreateParcelTime=DateTime.Now();
           temp1.DroneId=temp2.IdNumber;
        }//!!!!!
        public void ParcelToCollecting(Parcel p)
        {
            foreach (var item in DataSource.Parcels)
            {
                if(item.Equals(p))
                {
                    
                }

            }

           // Console.WriteLine("please enter the number of parcel you want to collect");
           // int numP = Console.Read();
           // Parcel temp1 = new Parcel();
           // foreach (var item in DataSource.Parcels)
           // {
           //     if (item.IdNumber == numP)
           //     {
           //         temp1 = item;
           //         break;
           //     }
           // }
           // Console.WriteLine("please enter the number of the wanted drone");
           // int numD = Console.Read();
           // Drone temp2 = new Drone();
           // foreach (var item in DataSource.Drones)
           // {
           //     if (item.IdNumber == numD)
           //     {
           //         temp2 = item;
           //         break;
           //     }
           // }
           // temp2.Status = (DroneStatus.Shipping);
           // temp1.MatchForDroneTime=DateTime.Now();
           //temp1.DroneId=temp2.IdNumber;
        }//!!!!
        public void ParcelToCustomer()
        {
            Console.WriteLine("please enter the number of parcel you get");
            int numP = Console.Read();
            Parcel temp1 = new Parcel();
            foreach (var item in DataSource.Parcels)
            {
                if (item.IdNumber == numP)
                {
                    temp1 = item;
                    break;
                }
            }
            Console.WriteLine("please enter the number of the wanted customer");
            int numD = Console.Read();
            Drone temp2 = new Drone();
            foreach (var item in DataSource.Drones)
            {
                if (item.IdNumber == numD)
                {
                    temp2 = item;
                    break;
                }
            }
            temp2.Status = (DroneStatus.Available);
            DataSource.Parcels.Remove(temp1);
            temp1.collectingDroneTime=DateTime.Now();
           temp1.DroneId=temp2.IdNumber;
        }//!!!!!
        public void SendToCharge()//!!!! !
        {
            Console.WriteLine("please enter the number of the wanted drone");
            int numD = Console.Read();
            ShowAvailibeStation();
            Console.WriteLine("please enter the number of the wanted base station from those above");
            int numS = Console.Read();
            Drone temp1=new Drone();
            BaseStation temp2 = new BaseStation();
            foreach (var item in DataSource.Drones)
            {
                if(item.IdNumber==numD)
                {
                    temp1 = item;
                    break;
                }    
            }


            foreach (var item in DataSource.stations)
            {
                if (item.IdNumber == numS)
                {
                    temp2 = item;
                    break;
                }
            }
            temp1.Status = (DroneStatus.Maintenance);
            temp2.ChargeSlots--;
            DroneCharge newCharge = new DroneCharge();
            newCharge.DroneId = numD;
            newCharge.StationId = numS;
            DataSource.Charges.Add(newCharge);
        }
        public void releaseCharge(int d1)///!!!!
        {

        }
        public T getItem<T> (T itemp)
        {
            PropertyInfo help = itemp.GetType().GetProperties()[0];
            if (itemp is BaseStation)
            {
                foreach(var item in DataSource.stations)
                {
                    if(item.IdNumber==(int)help.GetValue(item,null))
                    {
                        T ret = itemp;
                        return ret;
                    }
                }
            }
            if (itemp is Drone)
            {
                foreach (var item in DataSource.Drones)
                {
                    if (item.IdNumber == (int)help.GetValue(item, null))
                    {
                        T ret = itemp;
                        return ret;
                    }
                }
            }
            if (itemp is Customer)
            {
                foreach (var item in DataSource.Customers)
                {
                    if (item.Id == (int)help.GetValue(item, null))
                    {
                        T ret = itemp;
                        return ret;
                    }
                }
            }
            if (itemp is Parcel)
            {
                foreach (var item in DataSource.Parcels)
                {
                    if (item.IdNumber == (int)help.GetValue(item, null))
                    {
                        T ret = itemp;
                        return ret;
                    }
                }
            }
            help.SetValue(itemp, 0);
            return itemp;//?

        }
    }

}
