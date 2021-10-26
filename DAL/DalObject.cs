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
        public void AddingBaseStation(BaseStation station)
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
        public List<Customer> GetCustomers()
        {
            List<Customer> returnCustomer = new List<Customer>();
            foreach (var item in DataSource.Customers)
            {
                returnCustomer.Add(item);
            }
            return returnCustomer;
        }
        /// <summary>
        /// function that return list of drones
        /// </summary>
        /// <returns></returns>
        public List<Drone> GetDrones()
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
        public List<BaseStation> GetBaseStations()
        {
            List<BaseStation> returnStation = new List<BaseStation>();
            foreach (var item in DataSource.stations)
                returnStation.Add(item);
            return returnStation;
        }
        /// <summary>
        /// function that return copy of the parcels
        /// </summary>
        /// <returns></returns>
        public List<Parcel> GetParcels()
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
        public List<Parcel> GetNonMatchParcels()
        {
            List<Parcel> NonMatch = new List<Parcel>();
            foreach (var item in DataSource.Parcels)
            {
                if (item.DroneId == 0)
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
            foreach (var item in DataSource.stations)
                if (item.ChargeSlots != 0)
                    Availible.Add(item);
            return Availible;
        }
        public void ParcelToDrone(Parcel temp)
        {
            var par = DataSource.Parcels.FirstOrDefault(p => p.IdNumber == temp.IdNumber);    
            var Dro = DataSource.Drones.FirstOrDefault(p => p.Status == IDAL.DO.DroneStatus.Available && p.MaxWeight == par.Weight);
            par.DroneId = Dro.IdNumber;
            par.MatchForDroneTime = DateTime.Now;
            var index = DataSource.Parcels.FindIndex(c => c.IdNumber == temp.IdNumber);
            DataSource.Parcels[index] = par;
            Dro.Status = DroneStatus.Shipping;
            var index2 = DataSource.Drones.FindIndex(c => c.IdNumber == Dro.IdNumber);
            DataSource.Drones[index2] = Dro;
        }
        public void ParcelToCollecting(Parcel temp)
        {
            var par = DataSource.Parcels.FirstOrDefault(p => p.IdNumber == temp.IdNumber);
            par.collectingDroneTime = DateTime.Now;
            var index = DataSource.Parcels.FindIndex(c => c.IdNumber == temp.IdNumber);
            DataSource.Parcels[index] = par;
            
        }
        public void ParcelToCustomer(Parcel temp)
        {
            var par = DataSource.Parcels.FirstOrDefault(p => p.IdNumber == temp.IdNumber);
            par.ArrivingDroneTime = DateTime.Now;
            var index = DataSource.Parcels.FindIndex(c => c.IdNumber == temp.IdNumber);
            DataSource.Parcels[index] = par;
            var Dro = DataSource.Drones.FirstOrDefault(p => par.DroneId == p.IdNumber);
            Dro.Status = DroneStatus.Available;
            var index2 = DataSource.Drones.FindIndex(c => c.IdNumber == Dro.IdNumber);
            DataSource.Drones[index2] = Dro;
        }
        public void SendToCharge(DroneCharge D)
        {
            DataSource.Charges.Add(D);
            var Dro = DataSource.Drones.FirstOrDefault(P => P.IdNumber == D.DroneId);
            Dro.Status = DroneStatus.Maintenance;
            var index2 = DataSource.Drones.FindIndex(c => c.IdNumber == Dro.IdNumber);
            DataSource.Drones[index2] = Dro;
            var Bas = DataSource.stations.FirstOrDefault(P => P.IdNumber == D.StationId);
            Bas.ChargeSlots--;
            var index1 = DataSource.stations.FindIndex(c => c.IdNumber == Dro.IdNumber);
            DataSource.stations[index1] = Bas;
        }
        public void releaseCharge(Drone d)
        {
            var Dro = DataSource.Drones.FirstOrDefault(p => p.IdNumber == d.IdNumber);
            Dro.Status = DroneStatus.Available;
            Dro.Battery = 100;
            var index2 = DataSource.Drones.FindIndex(c => c.IdNumber == Dro.IdNumber);
            DataSource.Drones[index2] = Dro;
            var Char = DataSource.Charges.FirstOrDefault(p => p.DroneId == Dro.IdNumber);
            var Bas = DataSource.stations.FirstOrDefault(p => p.IdNumber == Char.StationId);
            Bas.ChargeSlots++;
            var index1 = DataSource.stations.FindIndex(c => c.IdNumber == Dro.IdNumber);
            DataSource.stations[index1] = Bas;
            DataSource.Charges.Remove(Char);
        }
        public void RemovePar(Parcel temp)
        {
            var par = DataSource.Parcels.FirstOrDefault(p => p.IdNumber == temp.IdNumber);
            DataSource.Parcels.Remove(par);

        }
        public Drone getDrone(Drone id)
        {
            var Dro = DataSource.Drones.FirstOrDefault(P => P.IdNumber == id.IdNumber);
            if(Dro.IdNumber!=id.IdNumber)
            {
                Dro.IdNumber = 0;
            }
            return Dro;
        }
        public BaseStation getBase(BaseStation id)
        {
            var Dro = DataSource.stations.FirstOrDefault(P => P.IdNumber == id.IdNumber);
            return Dro;
        }
        public Parcel getParcel(Parcel id)
        {
            var Dro = DataSource.Parcels.FirstOrDefault(P => P.IdNumber == id.IdNumber);
            return Dro;
        }
        public Customer getCustomer(Customer id)
        {
            var Dro = DataSource.Customers.FirstOrDefault(P => P.Id == id.Id);
            return Dro;
        }
    }
}
