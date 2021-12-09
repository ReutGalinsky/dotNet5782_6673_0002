using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using DO;
using DalApi;
namespace Dal
{
    public class DalObject : DalApi.IDal
    {
        #region singleton
        class Nested
        {
            static Nested() { }
            internal static readonly IDal instance = new DalObject();
        }
        static DalObject() { }
        private DalObject() { DataSource.Initialize(); }

        public static IDal Instance
        {
            get { return Nested.instance; }
        }
        #endregion

        //*****Drones***********

        #region AddDrone
        public void AddDrone(Drone drone)
        {
            if (DataSource.Drones.FirstOrDefault(d => d.IdNumber == drone.IdNumber).IdNumber != null)
                throw new ExistingException($"the drone with the id:{drone.IdNumber} is already exist");
            DataSource.Drones.Add(drone);
        }
        #endregion

        #region GetDrone
        public Drone GetDrone(string id)
        {
            Drone drone = DataSource.Drones.FirstOrDefault(d => d.IdNumber == id);
            if (drone.IdNumber == null)
                throw new NotExistingException($"the drone with the id:{id} is not exist");
            return drone;
        }
        #endregion

        #region GetDrones
        public IEnumerable<Drone> GetDrones()
        {
            var Drones = from item in DataSource.Drones
                         select item;
            return Drones;
        }
        #endregion

        #region DeleteDrone
        public void DeleteDrone(string id)
        {
            Drone drone = DataSource.Drones.FirstOrDefault(d => d.IdNumber == id);
            if (drone.IdNumber == null)
                throw new NotExistingException($"the drone with the id:{id} is not existing");
            DataSource.Drones.Remove(drone);
        }
        #endregion

        #region UpdateDrone
        public void UpdateDrone(Drone toUpdate)
        {
            for (int i = 0; i < DataSource.Drones.Count; i++)
            {
                if (DataSource.Drones[i].IdNumber == toUpdate.IdNumber)
                {
                    Drone drone = new Drone();
                    drone.Model = toUpdate.Model;
                    drone.MaxWeight = toUpdate.MaxWeight;
                    drone.IdNumber = toUpdate.IdNumber;
                    DataSource.Drones[i] = drone;
                    return;
                }
            }
            throw new NotExistingException($"the drone with the id:{toUpdate.IdNumber} is not exist");
        }
        #endregion

        #region PredicateDrone
        public IEnumerable<Drone> PredicateDrone(Predicate<Drone> condition)
        {
            var list = from item in DataSource.Drones
                       where condition(item)
                       select item;
            return list;
        }
        #endregion

        //*******charge********

        #region AddDroneCharge
        public void AddDroneCharge(DroneCharge dronecharge)
        {
            if (DataSource.Charges.FirstOrDefault(d => d.DroneId == dronecharge.DroneId).DroneId != null)
                throw new ExistingException($"the charge slot with the drone id:{dronecharge.DroneId} is already exist");
            DataSource.Charges.Add(dronecharge);
        }
        #endregion

        #region GetDroneCharge
        public DroneCharge GetDroneCharge(string id)
        {
            DroneCharge droneCharge = DataSource.Charges.FirstOrDefault(d => d.DroneId == id);
            if (droneCharge.DroneId == null)
                throw new NotExistingException($"the charge slot with the drone id:{id} is not exist");
            return droneCharge;
        }
        #endregion

        #region GetDroneCharges
        public IEnumerable<DroneCharge> GetDroneCharges()
        {
            var charges = from item in DataSource.Charges
                          select item;
            return charges;
        }
        #endregion

        #region DeleteDroneCharge
        public void DeleteDroneCharge(string id)
        {
            DroneCharge droneCharge = DataSource.Charges.FirstOrDefault(d => d.DroneId == id);
            if (droneCharge.DroneId == null)
                throw new NotExistingException($"the charge slot with the dorne id:{id} is not existing");
            DataSource.Charges.Remove(droneCharge);
        }
        #endregion

        #region UpdateDroneCharge
        public void UpdateDroneCharge(DroneCharge toUpdate)
        {
            for (int i = 0; i < DataSource.Charges.Count; i++)
            {
                if (DataSource.Charges[i].DroneId == toUpdate.DroneId)
                {
                    DroneCharge droneCharge = new DroneCharge();
                    droneCharge.DroneId = toUpdate.DroneId;
                    droneCharge.StationId = toUpdate.StationId;
                    DataSource.Charges[i] = droneCharge;
                    return;
                }
            }
            throw new NotExistingException($"the charge slot with the dorne id:{toUpdate.DroneId} is not existing");
        }
        #endregion

        #region PredicateChargeDrone
        public IEnumerable<DroneCharge> PredicateChargeDrone(Predicate<DroneCharge> condition)
        {
            var list = from item in DataSource.Charges
                       where condition(item)
                       select item;
            return list;
        }
        #endregion

        //*******parcel********

        #region AddParcel
        public string AddParcel(Parcel parcel)
        {
            parcel.IdNumber = DataSource.Config.RunningNumber++.ToString();
            if (DataSource.Parcels.FirstOrDefault(d => d.IdNumber == parcel.IdNumber).IdNumber != null)
                throw new ExistingException($"the parcel with the id:{parcel.IdNumber} is already exist");
            DataSource.Parcels.Add(parcel);
            return parcel.IdNumber;
        }
        #endregion

        #region GetParcel
        public Parcel GetParcel(string id)
        {
            Parcel parcel = DataSource.Parcels.FirstOrDefault(d => d.IdNumber == id);
            if (parcel.IdNumber == null)
                throw new NotExistingException($"the parcel with the id:{parcel.IdNumber} is not exist");
            return parcel;
        }
        #endregion

        #region GetParcels
        public IEnumerable<Parcel> GetParcels()
        {
            var parcels = from item in DataSource.Parcels
                          select item;
            return parcels;
        }
        #endregion

        #region DeleteParcel
        public void DeleteParcel(string id)
        {
            Parcel parcel = DataSource.Parcels.FirstOrDefault(d => d.IdNumber == id);
            if (parcel.IdNumber == null)
                throw new NotExistingException($"the parcel with the id:{parcel.IdNumber} is not exist");
            DataSource.Parcels.Remove(parcel);
        }
        #endregion

        #region UpdateParcel
        public void UpdateParcel(Parcel toUpdate)
        {
            for (int i = 0; i < DataSource.Parcels.Count; i++)
            {
                if (DataSource.Parcels[i].IdNumber == toUpdate.IdNumber)
                {
                    Parcel parcel = new Parcel();
                    parcel.ArrivingDroneTime = toUpdate.ArrivingDroneTime;
                    parcel.Geter = toUpdate.Geter;
                    parcel.Sender = toUpdate.Sender;
                    parcel.CollectingDroneTime = toUpdate.CollectingDroneTime;
                    parcel.CreateParcelTime = toUpdate.CreateParcelTime;
                    parcel.DroneId = toUpdate.DroneId;
                    parcel.IdNumber = toUpdate.IdNumber;
                    parcel.MatchForDroneTime = toUpdate.MatchForDroneTime;
                    parcel.Priority = toUpdate.Priority;
                    parcel.Weight = toUpdate.Weight;
                    DataSource.Parcels[i] = parcel;
                    return;
                }
            }
            throw new NotExistingException($"the parcel with the id:{toUpdate.IdNumber} is not exist");
        }
        #endregion

        #region PredicateParcel
        public IEnumerable<Parcel> PredicateParcel(Predicate<Parcel> condition)
        {
            var list = from item in DataSource.Parcels
                       where condition(item)
                       select item;
            return list;
        }
        #endregion

        //*******base station********

        #region AddBaseStation
        public void AddBaseStation(BaseStation baseStation)
        {
            if (DataSource. Stations.FirstOrDefault(d => d.IdNumber == baseStation.IdNumber).IdNumber != null)
                throw new ExistingException($"the baseStation with the id:{baseStation.IdNumber} is already exist");
            DataSource.Stations.Add(baseStation);
        }
        #endregion

        #region GetBaseStation
        public BaseStation GetBaseStation(string id)
        {
            BaseStation baseStation = DataSource.Stations.FirstOrDefault(d => d.IdNumber == id);
            if (baseStation.IdNumber == null)
                throw new NotExistingException($"the baseStation with the id:{baseStation.IdNumber} is not exist");
            return baseStation;
        }
        #endregion

        #region GetBaseStations
        public IEnumerable<BaseStation> GetBaseStations()
        {
            var BaseStations = from item in DataSource.Stations
                               select item;
            return BaseStations;
        }
        #endregion

        #region DeleteBaseStation
        public void DeleteBaseStation(string id)
        {
            BaseStation baseStation = DataSource.Stations.FirstOrDefault(d => d.IdNumber == id);
            if (baseStation.IdNumber == null)
                throw new NotExistingException($"the baseStation with the id:{baseStation.IdNumber} is not exist");
            DataSource.Stations.Remove(baseStation);
        }
        #endregion

        #region UpdateBaseStation
        public void UpdateBaseStation(BaseStation toUpdate)
        {
            for (int i = 0; i < DataSource.Stations.Count; i++)
            {
                if (DataSource.Stations[i].IdNumber == toUpdate.IdNumber)
                {
                    BaseStation baseStation = new BaseStation();
                    baseStation.ChargeSlots = toUpdate.ChargeSlots;
                    baseStation.Name = toUpdate.Name;
                    baseStation.IdNumber = toUpdate.IdNumber;
                    baseStation.Longitude = toUpdate.Longitude;
                    baseStation.Latitude = toUpdate.Latitude;
                    DataSource.Stations[i] = baseStation;
                    return;
                }
            }
            throw new NotExistingException($"the baseStation with the id:{toUpdate.IdNumber} is not exist");
        }
        #endregion

        #region PredicateBaseStation
        public IEnumerable<BaseStation> PredicateBaseStation(Predicate<BaseStation> condition)
        {
            var list = from item in DataSource.Stations
                       where condition(item)
                       select item;
            return list;
        }
        #endregion

        //*******customer********

        #region AddCustomer
        public void AddCustomer(Customer customer)
        {
            if (DataSource.Customers.FirstOrDefault(d => d.IdNumber == customer.IdNumber).IdNumber != null)
                throw new ExistingException($"the customer with the id:{customer.IdNumber} is already exist");
            DataSource.Customers.Add(customer);
        }
        #endregion

        #region GetCustomer
        public Customer GetCustomer(string id)
        {
            Customer customer = DataSource.Customers.FirstOrDefault(d => d.IdNumber == id);
            if (customer.IdNumber == null)
                throw new NotExistingException($"the customer with the id:{customer.IdNumber} is not exist");
            return customer;
        }
        #endregion

        #region GetCustomers
        public IEnumerable<Customer> GetCustomers()
        {
            var Customers = from item in DataSource.Customers
                            select item;
            return Customers;
        }
        #endregion

        #region DeleteCustomer
        public void DeleteCustomer(string id)
        {
            Customer customer = DataSource.Customers.FirstOrDefault(d => d.IdNumber == id);
            if (customer.IdNumber == null)
                throw new NotExistingException($"the customer with the id:{customer.IdNumber} is not exist");
            DataSource.Customers.Remove(customer);
        }
        #endregion

        #region UpdateCustomer
        public void UpdateCustomer(Customer toUpdate)
        {
            for (int i = 0; i < DataSource.Customers.Count; i++)
            {
                if (DataSource.Customers[i].IdNumber == toUpdate.IdNumber)
                {
                    Customer customer = new Customer();
                    customer.IdNumber = toUpdate.IdNumber;
                    customer.Name = toUpdate.Name;
                    customer.Phone = toUpdate.Phone;
                    customer.Longitude = toUpdate.Longitude;
                    customer.Latitude = toUpdate.Latitude;
                    DataSource.Customers[i] = customer;
                    return;
                }
            }
            throw new NotExistingException($"the customer with the id:{toUpdate.IdNumber} is not exist");
        }
        #endregion

        #region PredicateCustomer
        public IEnumerable<Customer> PredicateCustomer(Predicate<Customer> condition)
        {
            var list = from item in DataSource.Customers
                       where condition(item)
                       select item;
            return list;
        }
        #endregion

        //*******electricity********

        #region UsingElectricity
        /// <summary>
        /// returnnig the values of the battery change
        /// </summary>
        /// <returns>the values of the battery loose and gain</returns>
        public double[] UsingElectricity()
        //function that return the electricity values
        {
            double[] arr = new double[5] { DataSource.Config.Available, DataSource.Config.Heavy, DataSource.Config.Light, DataSource.Config.Medium, DataSource.Config._speed };
            return arr;
        }
        #endregion
    }
}