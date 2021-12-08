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
    //implementation by lists
    {
        #region Constractor
        //private DalObject() { DataSource.Initialize(); }
        #endregion

        #region singleton
        class Nested
        {
            static Nested() { }
            internal static readonly IDal instance = new DalObject();
        }
        static DalObject() { DataSource.Initialize(); }
        DalObject() { }
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
                throw new ExistingException("the drone is already exist");
            DataSource.Drones.Add(drone);
        }
        #endregion

        #region GetDrone
        public Drone GetDrone(string id)
        {
            Drone drone = DataSource.Drones.FirstOrDefault(d => d.IdNumber == id);
            if (drone.IdNumber == null)
                throw new NotExistingException("the drone is not exist");
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
                throw new NotExistingException("the drone is not existing");
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
                    Drone d = new Drone();
                    d.Model = toUpdate.Model;
                    d.MaxWeight = toUpdate.MaxWeight;
                    d.IdNumber = toUpdate.IdNumber;
                    DataSource.Drones[i] = d;
                    return;
                }
            }
            throw new NotExistingException("the drone is not exist");
        }
        #endregion

        #region PredicateDrone
        public IEnumerable<Drone> PredicateDrone(Predicate<Drone> c)
        {
            var list = from item in DataSource.Drones
                       where c(item)
                       select item;
            return list;
        }
        #endregion

        //*******charge********

        #region AddDroneCharge
        public void AddDroneCharge(DroneCharge dronecharge)
        {
            if (DataSource.Charges.FirstOrDefault(d => d.DroneId == dronecharge.DroneId).DroneId != null)
                throw new ExistingException("the charge slot is already exist");
            DataSource.Charges.Add(dronecharge);
        }
        #endregion

        #region GetDroneCharge
        public DroneCharge GetDroneCharge(string id)
        {
            DroneCharge droneCharge = DataSource.Charges.FirstOrDefault(d => d.DroneId == id);
            if (droneCharge.DroneId == null)
                throw new NotExistingException("the charge slot is not exist");
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
                throw new NotExistingException("the charge slot is not existing");
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
                    DroneCharge d = new DroneCharge();
                    d.DroneId = toUpdate.DroneId;
                    d.StationId = toUpdate.StationId;
                    DataSource.Charges[i] = d;
                    return;
                }
            }
            throw new NotExistingException("the charge slot is not exist");
        }
        #endregion

        #region PredicateChargeDrone
        public IEnumerable<DroneCharge> PredicateChargeDrone(Predicate<DroneCharge> c)
        {
            var list = from item in DataSource.Charges
                       where c(item)
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
                throw new ExistingException("the parcel is already exist");
            DataSource.Parcels.Add(parcel);
            return parcel.IdNumber;
        }
        #endregion

        #region GetParcel
        public Parcel GetParcel(string id)
        {
            Parcel parcel = DataSource.Parcels.FirstOrDefault(d => d.IdNumber == id);
            if (parcel.IdNumber == null)
                throw new NotExistingException("the parcel is not exist");
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
                throw new NotExistingException("the parcel is not existing");
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
                    Parcel d = new Parcel();
                    d.ArrivingDroneTime = toUpdate.ArrivingDroneTime;
                    d.Geter = toUpdate.Geter;
                    d.Sender = toUpdate.Sender;
                    d.collectingDroneTime = toUpdate.collectingDroneTime;
                    d.CreateParcelTime = toUpdate.CreateParcelTime;
                    d.DroneId = toUpdate.DroneId;
                    d.IdNumber = toUpdate.IdNumber;
                    d.MatchForDroneTime = toUpdate.MatchForDroneTime;
                    d.Priority = toUpdate.Priority;
                    d.Weight = toUpdate.Weight;
                    DataSource.Parcels[i] = d;
                    return;
                }
            }
            throw new NotExistingException("the parcel is not exist");
        }
        #endregion

        #region PredicateParcel
        public IEnumerable<Parcel> PredicateParcel(Predicate<Parcel> c)
        {
            var list = from item in DataSource.Parcels
                       where c(item)
                       select item;
            return list;
        }
        #endregion

        //*******base station********

        #region AddBaseStation
        public void AddBaseStation(BaseStation baseStation)
        {
            if (DataSource.stations.FirstOrDefault(d => d.IdNumber == baseStation.IdNumber).IdNumber != null)
                throw new ExistingException("the baseStation is already exist");
            DataSource.stations.Add(baseStation);
        }
        #endregion

        #region GetBaseStation
        public BaseStation GetBaseStation(string id)
        {
            BaseStation baseStation = DataSource.stations.FirstOrDefault(d => d.IdNumber == id);
            if (baseStation.IdNumber == null)
                throw new NotExistingException("the baseStation is not exist");
            return baseStation;
        }
        #endregion

        #region GetBaseStations
        public IEnumerable<BaseStation> GetBaseStations()
        {
            var BaseStations = from item in DataSource.stations
                               select item;
            return BaseStations;
        }
        #endregion

        #region DeleteBaseStation
        public void DeleteBaseStation(string id)
        {
            BaseStation baseStation = DataSource.stations.FirstOrDefault(d => d.IdNumber == id);
            if (baseStation.IdNumber == null)
                throw new NotExistingException("the baseStation is not existing");
            DataSource.stations.Remove(baseStation);
        }
        #endregion

        #region UpdateBaseStation
        public void UpdateBaseStation(BaseStation toUpdate)
        {
            for (int i = 0; i < DataSource.stations.Count; i++)
            {
                if (DataSource.stations[i].IdNumber == toUpdate.IdNumber)
                {
                    BaseStation b = new BaseStation();
                    b.ChargeSlots = toUpdate.ChargeSlots;
                    b.Name = toUpdate.Name;
                    b.IdNumber = toUpdate.IdNumber;
                    b.Longitude = toUpdate.Longitude;
                    b.Latitude = toUpdate.Latitude;
                    DataSource.stations[i] = b;
                    return;
                }
            }
            throw new NotExistingException("the base station is not exist");
        }
        #endregion

        #region PredicateBaseStation
        public IEnumerable<BaseStation> PredicateBaseStation(Predicate<BaseStation> c)
        {
            var list = from item in DataSource.stations
                       where c(item)
                       select item;
            return list;
        }
        #endregion

        //*******customer********

        #region AddCustomer
        public void AddCustomer(Customer customer)
        {
            if (DataSource.Customers.FirstOrDefault(d => d.IdNumber == customer.IdNumber).IdNumber != null)
                throw new ExistingException("the customer is already exist");
            DataSource.Customers.Add(customer);
        }
        #endregion

        #region GetCustomer
        public Customer GetCustomer(string id)
        {
            Customer customer = DataSource.Customers.FirstOrDefault(d => d.IdNumber == id);
            if (customer.IdNumber == null)
                throw new NotExistingException("the customer is not exist");
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
                throw new NotExistingException("the customer is not existing");
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
                    Customer c = new Customer();
                    c.IdNumber = toUpdate.IdNumber;
                    c.Name = toUpdate.Name;
                    c.Phone = toUpdate.Phone;
                    c.Longitude = toUpdate.Longitude;
                    c.Latitude = toUpdate.Latitude;
                    DataSource.Customers[i] = c;
                    return;
                }
            }
            throw new NotExistingException("the customer is not exist");
        }
        #endregion

        #region PredicateCustomer
        public IEnumerable<Customer> PredicateCustomer(Predicate<Customer> c)
        {
            var list = from item in DataSource.Customers
                       where c(item)
                       select item;
            return list;
        }
        #endregion

        //*******tools********

        #region UsingElectricity
        public double[] UsingElectricity()
        //function that return the electricity values
        {
            double[] arr = new double[5] { DataSource.Config.available, DataSource.Config.heavy, DataSource.Config.light, DataSource.Config.medium, DataSource.Config.speed };
            return arr;
        }
        #endregion
    }
}