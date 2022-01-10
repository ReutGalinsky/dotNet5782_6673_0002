using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using DO;
using DalApi;
using System.Runtime.CompilerServices;

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



        #region AddDrone
        [MethodImpl(MethodImplOptions.Synchronized)]

        public void AddDrone(Drone drone)
        {
            if (DataSource.Drones.Exists(d => d.IdNumber == drone.IdNumber)==true)
                throw new ExistingException($"the drone with the id:{drone.IdNumber} is already exist");
            DataSource.Drones.Add(drone);
        }
        #endregion

        #region GetDrone
        [MethodImpl(MethodImplOptions.Synchronized)]

        public Drone GetDrone(string id)
        {
            Drone drone = DataSource.Drones.FirstOrDefault(d => d.IdNumber == id);
            if (drone.IdNumber == null)
                throw new NotExistingException($"the drone with the id:{id} is not exist");
            return drone;
        }
        #endregion

        #region GetDrones
        [MethodImpl(MethodImplOptions.Synchronized)]

        public IEnumerable<Drone> GetDrones()
        {
            var Drones = from item in DataSource.Drones
                         select item;
            return Drones;
        }
        #endregion

        #region DeleteDrone
        [MethodImpl(MethodImplOptions.Synchronized)]

        public void DeleteDrone(string id)
        {
            Drone drone = DataSource.Drones.FirstOrDefault(d => d.IdNumber == id);
            if (drone.IdNumber == null)
                throw new NotExistingException($"the drone with the id:{id} is not existing");
            DataSource.Drones.Remove(drone);
        }
        #endregion

        #region UpdateDrone
        [MethodImpl(MethodImplOptions.Synchronized)]

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

        #region GetAllDronesBy
        [MethodImpl(MethodImplOptions.Synchronized)]

        public IEnumerable<Drone> GetAllDronesBy(Predicate<Drone> condition)
        {
            var list = from item in DataSource.Drones
                       where condition(item)
                       select item;
            return list;
        }
        #endregion



        #region AddDroneCharge
        [MethodImpl(MethodImplOptions.Synchronized)]

        public void AddDroneCharge(DroneCharge dronecharge)
        {
            if (DataSource.Charges.FirstOrDefault(d => d.DroneId == dronecharge.DroneId).DroneId != null)
                throw new ExistingException($"the charge slot with the drone id:{dronecharge.DroneId} is already exist");
            DataSource.Charges.Add(dronecharge);
        }
        #endregion

        #region GetDroneCharge
        [MethodImpl(MethodImplOptions.Synchronized)]

        public DroneCharge GetDroneCharge(string id)
        {
            DroneCharge droneCharge = DataSource.Charges.FirstOrDefault(d => d.DroneId == id);
            if (droneCharge.DroneId == null)
                throw new NotExistingException($"the charge slot with the drone id:{id} is not exist");
            return droneCharge;
        }
        #endregion

        #region GetDroneCharges
        [MethodImpl(MethodImplOptions.Synchronized)]

        public IEnumerable<DroneCharge> GetDroneCharges()
        {
            var charges = from item in DataSource.Charges
                          select item;
            return charges;
        }
        #endregion

        #region DeleteDroneCharge
        [MethodImpl(MethodImplOptions.Synchronized)]

        public void DeleteDroneCharge(string id)
        {
            DroneCharge droneCharge = DataSource.Charges.FirstOrDefault(d => d.DroneId == id);
            if (droneCharge.DroneId == null)
                throw new NotExistingException($"the charge slot with the dorne id:{id} is not existing");
            DataSource.Charges.Remove(droneCharge);
        }
        #endregion

        #region UpdateDroneCharge
        [MethodImpl(MethodImplOptions.Synchronized)]

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

        #region GetAllChargeDronesBy
        [MethodImpl(MethodImplOptions.Synchronized)]

        public IEnumerable<DroneCharge> GetAllChargeDronesBy(Predicate<DroneCharge> condition)
        {
            var list = from item in DataSource.Charges
                       where condition(item)
                       select item;
            return list;
        }
        #endregion



        #region AddParcel
        [MethodImpl(MethodImplOptions.Synchronized)]

        public string AddParcel(Parcel parcel)
        {
            parcel.IdNumber = DataSource.Config.RunningNumber++.ToString();
            if (DataSource.Parcels.Exists(d => d.IdNumber == parcel.IdNumber)==true)
                throw new ExistingException($"the parcel with the id:{parcel.IdNumber} is already exist");
            DataSource.Parcels.Add(parcel);
            return parcel.IdNumber;
        }
        #endregion

        #region GetParcel
        [MethodImpl(MethodImplOptions.Synchronized)]

        public Parcel GetParcel(string id)
        {
            Parcel parcel = DataSource.Parcels.FirstOrDefault(d => d.IdNumber == id);
            if (parcel.IdNumber == null)
                throw new NotExistingException($"the parcel with the id:{parcel.IdNumber} is not exist");
            return parcel;
        }
        #endregion

        #region GetParcels
        [MethodImpl(MethodImplOptions.Synchronized)]

        public IEnumerable<Parcel> GetParcels()
        {
            var parcels = from item in DataSource.Parcels
                          select item;
            return parcels;
        }
        #endregion

        #region DeleteParcel
        [MethodImpl(MethodImplOptions.Synchronized)]

        public void DeleteParcel(string id)
        {
            Parcel parcel = DataSource.Parcels.FirstOrDefault(d => d.IdNumber == id);
            if (parcel.IdNumber == null)
                throw new NotExistingException($"the parcel with the id:{parcel.IdNumber} is not exist");
            DataSource.Parcels.Remove(parcel);
        }
        #endregion

        #region UpdateParcel
        [MethodImpl(MethodImplOptions.Synchronized)]

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

        #region GetAllParcelsBy
        [MethodImpl(MethodImplOptions.Synchronized)]

        public IEnumerable<Parcel> GetAllParcelsBy(Predicate<Parcel> condition)
        {
            var list = from item in DataSource.Parcels
                       where condition(item)
                       select item;
            return list;
        }
        #endregion



        #region AddBaseStation
        [MethodImpl(MethodImplOptions.Synchronized)]

        public void AddBaseStation(BaseStation baseStation)
        {
            if (DataSource. Stations.FirstOrDefault(d => d.IdNumber == baseStation.IdNumber).IdNumber != null)
                throw new ExistingException($"the baseStation with the id:{baseStation.IdNumber} is already exist");
            DataSource.Stations.Add(baseStation);
        }
        #endregion

        #region GetBaseStation
        [MethodImpl(MethodImplOptions.Synchronized)]

        public BaseStation GetBaseStation(string id)
        {
            BaseStation baseStation = DataSource.Stations.FirstOrDefault(d => d.IdNumber == id);
            if (baseStation.IdNumber == null)
                throw new NotExistingException($"the baseStation with the id:{baseStation.IdNumber} is not exist");
            return baseStation;
        }
        #endregion

        #region GetBaseStations
        [MethodImpl(MethodImplOptions.Synchronized)]

        public IEnumerable<BaseStation> GetBaseStations()
        {
            var BaseStations = from item in DataSource.Stations
                               select item;
            return BaseStations;
        }
        #endregion

        #region DeleteBaseStation
        [MethodImpl(MethodImplOptions.Synchronized)]

        public void DeleteBaseStation(string id)
        {
            BaseStation baseStation = DataSource.Stations.FirstOrDefault(d => d.IdNumber == id);
            if (baseStation.IdNumber == null)
                throw new NotExistingException($"the baseStation with the id:{baseStation.IdNumber} is not exist");
            DataSource.Stations.Remove(baseStation);
        }
        #endregion

        #region UpdateBaseStation
        [MethodImpl(MethodImplOptions.Synchronized)]

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

        #region GetAllBaseStationsBy
        [MethodImpl(MethodImplOptions.Synchronized)]

        public IEnumerable<BaseStation> GetAllBaseStationsBy(Predicate<BaseStation> condition)
        {
            var list = from item in DataSource.Stations
                       where condition(item)
                       select item;
            return list;
        }
        #endregion



        #region AddCustomer
        [MethodImpl(MethodImplOptions.Synchronized)]

        public void AddCustomer(Customer customer)
        {
            if (DataSource.Customers.FirstOrDefault(d => d.IdNumber == customer.IdNumber).IdNumber != null)
                throw new ExistingException($"the customer with the id:{customer.IdNumber} is already exist");
            DataSource.Customers.Add(customer);
        }
        #endregion

        #region GetCustomer
        [MethodImpl(MethodImplOptions.Synchronized)]

        public Customer GetCustomer(string id)
        {
            Customer customer = DataSource.Customers.FirstOrDefault(d => d.IdNumber == id);
            if (customer.IdNumber == null)
                throw new NotExistingException($"the customer with the id:{customer.IdNumber} is not exist");
            return customer;
        }
        #endregion

        #region GetCustomers
        [MethodImpl(MethodImplOptions.Synchronized)]

        public IEnumerable<Customer> GetCustomers()
        {
            var Customers = from item in DataSource.Customers
                            select item;
            return Customers;
        }
        #endregion

        #region DeleteCustomer
        [MethodImpl(MethodImplOptions.Synchronized)]

        public void DeleteCustomer(string id)
        {
            Customer customer = DataSource.Customers.FirstOrDefault(d => d.IdNumber == id);
            if (customer.IdNumber == null)
                throw new NotExistingException($"the customer with the id:{customer.IdNumber} is not exist");
            DataSource.Customers.Remove(customer);
        }
        #endregion

        #region UpdateCustomer
        [MethodImpl(MethodImplOptions.Synchronized)]

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

        #region GetAllCustomersBy
        [MethodImpl(MethodImplOptions.Synchronized)]

        public IEnumerable<Customer> GetAllCustomersBy(Predicate<Customer> condition)
        {
            var list = from item in DataSource.Customers
                       where condition(item)
                       select item;
            return list;
        }
        #endregion

        #region AddUser

        [MethodImpl(MethodImplOptions.Synchronized)]

        public void AddUser(User user)
        {
            if (DataSource.Users.Exists(d => d.UserName == user.UserName)==true)
                throw new ExistingException($"the user with the name:{user.UserName} is already exist");
            DataSource.Users.Add(user);

        }
        #endregion
        #region DeleteUser

        [MethodImpl(MethodImplOptions.Synchronized)]

        public void DeleteUser(string userName)
        {
            User user = DataSource.Users.FirstOrDefault(d => d.UserName == userName);
            if (user.UserName == null)
                throw new NotExistingException($"the User with the name:{user.UserName} is not exist");
            DataSource.Users.Remove(user);
        }
        #endregion
        #region GetUser

        [MethodImpl(MethodImplOptions.Synchronized)]

        public User GetUser(string userName)
        {
            User user = DataSource.Users.FirstOrDefault(d => d.UserName == userName);
            if (user.UserName == null)
                throw new NotExistingException($"the customer with the id:{user.UserName} is not exist");
            return user;
        }
        #endregion
        #region UpdateUser
        [MethodImpl(MethodImplOptions.Synchronized)]

        public void UpdateUser(User toUpdate)
        {
            for (int i = 0; i < DataSource.Users.Count; i++)
            {
                if (DataSource.Users[i].UserName == toUpdate.UserName)
                {
                    User user = new User();
                    user.UserName = toUpdate.UserName;
                    user.UserPassword = toUpdate.UserPassword;
                    user.isManager = toUpdate.isManager;
                    DataSource.Users[i] = user;
                    return;
                }
            }
            throw new NotExistingException($"the user with the name:{toUpdate.UserName} is not exist");
        }
        #endregion
        #region GetUsers
        [MethodImpl(MethodImplOptions.Synchronized)]

        public IEnumerable<User> GetUsers()
        {
            var Customers = from item in DataSource.Users
                            select item;
            return Customers;
        }
        #endregion
        #region GetAllUsersBy
        [MethodImpl(MethodImplOptions.Synchronized)]

        public IEnumerable<User> GetAllUsersBy(Predicate<User> condition)
        {
            var list = from item in DataSource.Users
                       where condition(item)
                       select item;
            return list;
        }
        #endregion



        #region UsingElectricity
        [MethodImpl(MethodImplOptions.Synchronized)]

        public double[] UsingElectricity()
        {
            double[] arr = new double[5] { DataSource.Config.Available, DataSource.Config.Heavy, DataSource.Config.Light, DataSource.Config.Medium, DataSource.Config._speed };
            return arr;
        }
        #endregion
    }
}