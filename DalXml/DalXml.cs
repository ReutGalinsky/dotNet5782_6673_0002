using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using DO;
using DalApi;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.CompilerServices;


namespace Dal
{
    public class DalXml : DalApi.IDal
    {
        #region singleton
        class Nested
        {
            static Nested() { }
            internal static readonly IDal instance = new DalXml();
        }
        private DalXml()
        {
        }
        public static IDal Instance
        {
            get { return Nested.instance; }
        }
        #endregion

        string dronePath = @"Drones.xml";
        string chargePath = @"Charges.xml";
        string baseStationPath = @"BaseStations.xml";
        string parcelPath = @"Parcels.xml";
        string userPath = @"Users.xml";
        string customerPath = @"Customers.xml";
        string configurePath = @"Configure.xml";

        #region AddDrone  
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDrone(Drone drone)
        {
            XElement droneRoot = XmlMethods.LoadFromXml(dronePath);
            var droneToAdd = (from droneItem in droneRoot.Elements()
                              where (droneItem.Element("IdNumber").Value == drone.IdNumber)
                              select droneItem).FirstOrDefault();
            if (droneToAdd != null)
                throw new DO.ExistingException("The Drone already exist in the system");
            XElement newDrone = new XElement("Drone",
                new XElement("IdNumber", drone.IdNumber),
                new XElement("Model", drone.Model),
                new XElement("MaxWeight", drone.MaxWeight.ToString()));
            droneRoot.Add(newDrone);
            XmlMethods.SaveToXml(dronePath, droneRoot);
        }
        #endregion

        #region GetDrone

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone GetDrone(string id)
        {
            XElement droneRoot = XmlMethods.LoadFromXml(dronePath);
            var droneToAdd = (from droneItem in droneRoot.Elements()
                              where (droneItem.Element("IdNumber").Value == id)
                              select droneItem).FirstOrDefault();
            if (droneToAdd == null)
                throw new DO.NotExistingException("The Drone not exist in the system");
            DO.Drone drone = new Drone();
            drone.IdNumber = droneToAdd.Element("IdNumber").Value;
            drone.Model = droneToAdd.Element("Model").Value;
            drone.MaxWeight = (DO.WeightCategories)Enum.Parse(typeof(DO.WeightCategories), droneToAdd.Element("MaxWeight").Value);
            return drone;
        }
        #endregion

        #region GetDrones
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Drone> GetDrones()
        {
            XElement droneRoot = XmlMethods.LoadFromXml(dronePath);
            var droneToAdd = (from droneItem in droneRoot.Elements()
                              select new DO.Drone
                              {
                                  IdNumber = droneItem.Element("IdNumber").Value,
                                  Model = droneItem.Element("Model").Value,
                                  MaxWeight = (DO.WeightCategories)Enum.Parse(typeof(DO.WeightCategories), droneItem.Element("MaxWeight").Value)
                              });
            return droneToAdd;
        }
        #endregion

        #region DeleteDrone
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteDrone(string id)
        {
            XElement droneRoot = XmlMethods.LoadFromXml(dronePath);
            XElement drone;
            try
            {
                drone = (from p in droneRoot.Elements()
                         where (p.Element("IdNumber").Value) == id
                         select p).FirstOrDefault();
                drone.Remove();
                droneRoot.Save(dronePath);
            }
            catch (Exception)
            {
                throw new DO.NotExistingException($"The drone with the id: {id} is not existing");
            }
        }
        #endregion

        #region UpdateDrone
        [MethodImpl(MethodImplOptions.Synchronized)]

        public void UpdateDrone(Drone toUpdate)
        {
            XElement droneRoot = XmlMethods.LoadFromXml(dronePath);
            XElement drone = (from item in droneRoot.Elements()
                              where (item.Element("IdNumber").Value) == toUpdate.IdNumber
                              select item).FirstOrDefault();
            if (drone == null)
                throw new DO.NotExistingException($"The drone with the id: { toUpdate.IdNumber } is not existing");
            drone.Element("Model").Value = toUpdate.Model;
            drone.Element("MaxWeight").Value = toUpdate.MaxWeight.ToString();
            droneRoot.Save(dronePath);
        }

        #endregion

        #region GetAllDronesBy
        [MethodImpl(MethodImplOptions.Synchronized)]

        public IEnumerable<Drone> GetAllDronesBy(Predicate<Drone> condition)
        {
            XElement droneRoot = XmlMethods.LoadFromXml(dronePath);
            var droneToAdd = (from droneItem in droneRoot.Elements()
                              let drone = new DO.Drone() { IdNumber = droneItem.Element("IdNumber").Value, Model = droneItem.Element("Model").Value, MaxWeight = (DO.WeightCategories)Enum.Parse(typeof(DO.WeightCategories), droneItem.Element("MaxWeight").Value) }
                              where (condition(drone))
                              select drone);
            return droneToAdd;
        }
        #endregion



        #region AddDroneCharge
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDroneCharge(DroneCharge dronecharge)
        {

            var listCharges = XmlMethods.LoadListFromXMLSerializer<DroneCharge>(chargePath);
            if (listCharges.Exists(x => x.DroneId == dronecharge.DroneId) == true)
                throw new ExistingException($"the charge slot with the drone id:{dronecharge.DroneId} is already exist");
            listCharges.Add(dronecharge);
            XmlMethods.SaveListToXMLSerializer<DroneCharge>(listCharges, chargePath);
        }
        #endregion

        #region GetDroneCharge
        [MethodImpl(MethodImplOptions.Synchronized)]

        public DroneCharge GetDroneCharge(string id)
        {
            var listCharges = XmlMethods.LoadListFromXMLSerializer<DroneCharge>(chargePath).FirstOrDefault(x => x.DroneId == id);
            if (listCharges.DroneId == null)
                throw new NotExistingException($"the charge slot with the drone id:{id} is not exist");
            return listCharges;
        }
        #endregion

        #region GetDroneCharges
        [MethodImpl(MethodImplOptions.Synchronized)]

        public IEnumerable<DroneCharge> GetDroneCharges()
        {
            var listCharges = XmlMethods.LoadListFromXMLSerializer<DroneCharge>(chargePath);
            var DroneCharges = from item in listCharges
                               select item;
            return DroneCharges;
        }
        #endregion

        #region DeleteDroneCharge
        [MethodImpl(MethodImplOptions.Synchronized)]

        public void DeleteDroneCharge(string id)
        {
            var listCharges = XmlMethods.LoadListFromXMLSerializer<DroneCharge>(chargePath);
            var droneToDelete = listCharges.FirstOrDefault(d => d.DroneId == id);
            if (droneToDelete.DroneId == null)
                throw new NotExistingException($"the charge slot with the drone id:{id} is not exist");
            listCharges.Remove(droneToDelete);
            XmlMethods.SaveListToXMLSerializer<DroneCharge>(listCharges, chargePath);
        }
        #endregion

        #region UpdateDroneCharge
        [MethodImpl(MethodImplOptions.Synchronized)]

        public void UpdateDroneCharge(DroneCharge toUpdate)
        {
            var listCharges = XmlMethods.LoadListFromXMLSerializer<DroneCharge>(chargePath);
            DroneCharge droneCharge = (from charge in listCharges
                                       where charge.DroneId == toUpdate.DroneId
                                       select charge).FirstOrDefault();
            if (droneCharge.DroneId == default(string))
                throw new NotExistingException($"the charge slot with the dorne id:{toUpdate.DroneId} is not existing");
            var indexOfStation = listCharges.IndexOf(droneCharge);
            listCharges[indexOfStation] = toUpdate;
            XmlMethods.SaveListToXMLSerializer<DroneCharge>(listCharges, chargePath);

        }
        #endregion

        #region GetAllChargeDronesBy
        [MethodImpl(MethodImplOptions.Synchronized)]

        public IEnumerable<DroneCharge> GetAllChargeDronesBy(Predicate<DroneCharge> condition)
        {
            var listCharges = XmlMethods.LoadListFromXMLSerializer<DroneCharge>(chargePath);
            var DroneCharges = from item in listCharges
                               where condition(item)
                               select item;
            return DroneCharges;
        }
        #endregion



        #region AddParcel
        [MethodImpl(MethodImplOptions.Synchronized)]
        public string AddParcel(Parcel parcel)
        {

            XElement configureRoot = XmlMethods.LoadFromXml(configurePath);
            var runningNumber = configureRoot.Element("RunningNumber");
            parcel.IdNumber = runningNumber.Value;
            var listParcels = XmlMethods.LoadListFromXMLSerializer<Parcel>(parcelPath);

            if (listParcels.Exists(d => d.IdNumber == parcel.IdNumber) == true)
            {
                throw new ExistingException($"the parcel with the id:{parcel.IdNumber} is already exist");
            }
            listParcels.Add(parcel);
            XmlMethods.SaveListToXMLSerializer<Parcel>(listParcels, parcelPath);
            int run = int.Parse(runningNumber.Value);
            runningNumber.Value = (run + 1).ToString();
            configureRoot.Save(configurePath);
            return parcel.IdNumber;
        }
        #endregion

        #region GetParcel
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Parcel GetParcel(string id)
        {
            var parcel = XmlMethods.LoadListFromXMLSerializer<Parcel>(parcelPath).FirstOrDefault(x => x.IdNumber == id);
            if (parcel.IdNumber == null)
                throw new NotExistingException($"the parcel with the id:{id} is not exist");
            return parcel;
        }
        #endregion

        #region GetParcels
        [MethodImpl(MethodImplOptions.Synchronized)]

        public IEnumerable<Parcel> GetParcels()
        {
            var listParcels = XmlMethods.LoadListFromXMLSerializer<Parcel>(parcelPath);
            var Parcels = from item in listParcels
                          select item;
            return Parcels;
        }
        #endregion

        #region DeleteParcel
        [MethodImpl(MethodImplOptions.Synchronized)]

        public void DeleteParcel(string id)
        {
            var listParcels = XmlMethods.LoadListFromXMLSerializer<Parcel>(parcelPath);
            var parcelToDelete = listParcels.FirstOrDefault(d => d.IdNumber == id);
            if (parcelToDelete.IdNumber == null)
                throw new NotExistingException($"the parcel with the id:{id} is not exist");
            listParcels.Remove(parcelToDelete);
            XmlMethods.SaveListToXMLSerializer<Parcel>(listParcels, parcelPath);
        }
        #endregion

        #region UpdateParcel
        [MethodImpl(MethodImplOptions.Synchronized)]

        public void UpdateParcel(Parcel toUpdate)
        {
            var listParcels = XmlMethods.LoadListFromXMLSerializer<Parcel>(parcelPath);
            Parcel parcel = (from parcelItem in listParcels
                             where parcelItem.IdNumber == toUpdate.IdNumber
                             select parcelItem).FirstOrDefault();
            if (parcel.IdNumber == default(string))
                throw new NotExistingException($"the parcel with the id:{toUpdate.IdNumber} is not existing");
            var indexOfStation = listParcels.IndexOf(parcel);
            listParcels[indexOfStation] = toUpdate;
            XmlMethods.SaveListToXMLSerializer<Parcel>(listParcels, parcelPath);

            
        }
        #endregion

        #region GetAllParcelsBy
        [MethodImpl(MethodImplOptions.Synchronized)]

        public IEnumerable<Parcel> GetAllParcelsBy(Predicate<Parcel> condition)
        {
            var listParcels = XmlMethods.LoadListFromXMLSerializer<Parcel>(parcelPath);
            var Parcels = from item in listParcels
                          where condition(item)
                          select item;
            return Parcels;
        }
        #endregion



        #region AddBaseStation
        [MethodImpl(MethodImplOptions.Synchronized)]

        public void AddBaseStation(BaseStation baseStation)
        {
            var listBaseStation = XmlMethods.LoadListFromXMLSerializer<BaseStation>(baseStationPath);
            if (listBaseStation.Exists(d => d.IdNumber == baseStation.IdNumber) == true)
            {
                throw new ExistingException($"the base station with the id:{baseStation.IdNumber} is exist");
            }
            listBaseStation.Add(baseStation);
            XmlMethods.SaveListToXMLSerializer<BaseStation>(listBaseStation, baseStationPath);
        }
        #endregion

        #region GetBaseStation
        [MethodImpl(MethodImplOptions.Synchronized)]

        public BaseStation GetBaseStation(string id)
        {
            var baseStation = XmlMethods.LoadListFromXMLSerializer<BaseStation>(baseStationPath).FirstOrDefault(x => x.IdNumber == id);
            if (baseStation.IdNumber == null)
                throw new NotExistingException($"the base station with the id:{id} is not exist");
            return baseStation;
        }
        #endregion

        #region GetBaseStations
        [MethodImpl(MethodImplOptions.Synchronized)]

        public IEnumerable<BaseStation> GetBaseStations()
        {
            var listBaseStations = XmlMethods.LoadListFromXMLSerializer<BaseStation>(baseStationPath);
            var BaseStations = from item in listBaseStations
                               select item;
            return BaseStations;
        }
        #endregion


        #region DeleteBaseStation
        [MethodImpl(MethodImplOptions.Synchronized)]

        public void DeleteBaseStation(string id)
        {
            var listBaseStations = XmlMethods.LoadListFromXMLSerializer<BaseStation>(baseStationPath);
            var BaseStationToDelete = listBaseStations.FirstOrDefault(d => d.IdNumber == id);
            if (BaseStationToDelete.IdNumber == null)
                throw new NotExistingException($"the base station with the id:{id} is not exist");
            listBaseStations.Remove(BaseStationToDelete);
            XmlMethods.SaveListToXMLSerializer<BaseStation>(listBaseStations, baseStationPath);
        }
        #endregion

        #region UpdateBaseStation
        [MethodImpl(MethodImplOptions.Synchronized)]

        public void UpdateBaseStation(BaseStation toUpdate)
        {
            var listBaseStations = XmlMethods.LoadListFromXMLSerializer<BaseStation>(baseStationPath);
            BaseStation baseStation = (from station in listBaseStations
                                       where station.IdNumber == toUpdate.IdNumber
                                       select station).FirstOrDefault();
            if (baseStation.IdNumber == default(string))
                throw new NotExistingException($"the base station with the id:{toUpdate.IdNumber} is not existing");
            var indexOfStation = listBaseStations.IndexOf(baseStation);
            listBaseStations[indexOfStation] = toUpdate;
            XmlMethods.SaveListToXMLSerializer<BaseStation>(listBaseStations, baseStationPath);

        }
        #endregion

        #region GetAllBaseStationsBy
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BaseStation> GetAllBaseStationsBy(Predicate<BaseStation> condition)
        {
            var listBaseStation = XmlMethods.LoadListFromXMLSerializer<BaseStation>(baseStationPath);
            var baseStations = from item in listBaseStation
                               where condition(item)
                               select item;
            return baseStations;
        }
        #endregion



        #region AddCustomer
        [MethodImpl(MethodImplOptions.Synchronized)]

        public void AddCustomer(Customer customer)
        {
            var listCustomers = XmlMethods.LoadListFromXMLSerializer<Customer>(customerPath);
            if (listCustomers.FirstOrDefault(d => d.IdNumber == customer.IdNumber).IdNumber != null)
                throw new ExistingException($"the charge slot with the drone id:{customer.IdNumber} is already exist");
            listCustomers.Add(customer);
            XmlMethods.SaveListToXMLSerializer<Customer>(listCustomers, customerPath);
        }
        #endregion

        #region GetCustomer
        [MethodImpl(MethodImplOptions.Synchronized)]

        public Customer GetCustomer(string id)
        {
            var customer = XmlMethods.LoadListFromXMLSerializer<Customer>(customerPath).FirstOrDefault(x => x.IdNumber == id);
            if (customer.IdNumber == null)
                throw new NotExistingException($"the customer with the id:{id} is not exist");
            return customer;
        }
        #endregion

        #region GetCustomers
        [MethodImpl(MethodImplOptions.Synchronized)]

        public IEnumerable<Customer> GetCustomers()
        {
            var listCustomers = XmlMethods.LoadListFromXMLSerializer<Customer>(customerPath);
            var Customer = from item in listCustomers
                           select item;
            return Customer;
        }
        #endregion

        #region DeleteCustomer
        [MethodImpl(MethodImplOptions.Synchronized)]

        public void DeleteCustomer(string id)
        {
            var listCustomer = XmlMethods.LoadListFromXMLSerializer<Customer>(customerPath);
            var CustomerToDelete = listCustomer.FirstOrDefault(d => d.IdNumber == id);
            if (CustomerToDelete.IdNumber == null)
                throw new NotExistingException($"the customer with the id:{id} is not exist");
            listCustomer.Remove(CustomerToDelete);
            XmlMethods.SaveListToXMLSerializer<Customer>(listCustomer, customerPath);
        }
        #endregion

        #region UpdateCustomer
        [MethodImpl(MethodImplOptions.Synchronized)]

        public void UpdateCustomer(Customer toUpdate)
        {
            var listCustomers = XmlMethods.LoadListFromXMLSerializer<Customer>(customerPath);
            Customer customer = (from customerItem in listCustomers
                                 where customerItem.IdNumber == toUpdate.IdNumber
                                 select customerItem).FirstOrDefault();
            if (customer.IdNumber == default(string))
                throw new NotExistingException($"the customer with the id:{toUpdate.IdNumber} is not existing");
            var indexOfStation = listCustomers.IndexOf(customer);
            listCustomers[indexOfStation] = toUpdate;
            XmlMethods.SaveListToXMLSerializer<Customer>(listCustomers, customerPath);

           
        }
        #endregion

        #region GetAllCustomersBy
        [MethodImpl(MethodImplOptions.Synchronized)]

        public IEnumerable<Customer> GetAllCustomersBy(Predicate<Customer> condition)
        {
            var listCustomers = XmlMethods.LoadListFromXMLSerializer<Customer>(customerPath);
            var customers = from item in listCustomers
                            where condition(item)
                            select item;
            return customers;
        }
        #endregion

        #region AddUser
        [MethodImpl(MethodImplOptions.Synchronized)]

        public void AddUser(User user)
        {
            var listUsers = XmlMethods.LoadListFromXMLSerializer<User>(userPath);
            if (listUsers.FirstOrDefault(d => d.UserName == user.UserName).UserName != null)
                throw new ExistingException($"the user with the user-name:{user.UserName} is already exist");
            listUsers.Add(user);
            XmlMethods.SaveListToXMLSerializer<User>(listUsers, userPath);
        }
        #endregion
        #region DeleteUser
        [MethodImpl(MethodImplOptions.Synchronized)]

        public void DeleteUser(string userName)
        {
            var listUsers = XmlMethods.LoadListFromXMLSerializer<User>(userPath);
            var UserToDelete = listUsers.FirstOrDefault(d => d.UserName == userName);
            if (UserToDelete.UserName == null)
                throw new NotExistingException($"the user with the user-name:{userName} is not exist");
            listUsers.Remove(UserToDelete);
            XmlMethods.SaveListToXMLSerializer<User>(listUsers, userPath);
        }
        #endregion
        #region GetUser
        [MethodImpl(MethodImplOptions.Synchronized)]

        public User GetUser(string userName)
        {
            var user = XmlMethods.LoadListFromXMLSerializer<User>(userPath).FirstOrDefault(x => x.UserName == userName);
            if (user.UserName == null)
                throw new NotExistingException($"the user with the user-name:{userName} is not exist");
            return user;
        }
        #endregion
        #region UpdateUser
        [MethodImpl(MethodImplOptions.Synchronized)]

        public void UpdateUser(User toUpdate)
        {
            var listUsers = XmlMethods.LoadListFromXMLSerializer<User>(userPath);
            User user = (from userItem in listUsers
                         where userItem.UserName == toUpdate.UserName
                         select userItem).FirstOrDefault();
            if (user.UserName == default(string))
                throw new NotExistingException($"the customer with the id:{toUpdate.UserName} is not existing");
            var indexOfStation = listUsers.IndexOf(user);
            listUsers[indexOfStation] = toUpdate;
            XmlMethods.SaveListToXMLSerializer<User>(listUsers, userPath);

        }
        #endregion
        #region GetUsers
        [MethodImpl(MethodImplOptions.Synchronized)]

        public IEnumerable<User> GetUsers()
        {
            var listUsers = XmlMethods.LoadListFromXMLSerializer<User>(userPath);
            var User = from item in listUsers
                       select item;
            return User;
        }
        #endregion
        #region GetAllUsersBy
        [MethodImpl(MethodImplOptions.Synchronized)]

        public IEnumerable<User> GetAllUsersBy(Predicate<User> condition)
        {
            var listUsers = XmlMethods.LoadListFromXMLSerializer<User>(userPath);
            var user = from item in listUsers
                       where condition(item)
                       select item;
            return user;
        }
        #endregion

        #region Clear Drones Charges
        public void ClearDroneCharges()
        {
            var listCharges = XmlMethods.LoadListFromXMLSerializer<DroneCharge>(chargePath);
            var listBaseStations = from drone in listCharges
                                   select GetBaseStation(drone.StationId);
        
            foreach (var station in listBaseStations)
            {
                var new_station= station;
                new_station.ChargeSlots++;
                UpdateBaseStation(new_station);
            }
            listCharges.Clear();
            XmlMethods.SaveListToXMLSerializer<DroneCharge>(listCharges, chargePath);
        }
        #endregion



        //*******electricity********


        #region UsingElectricity
        [MethodImpl(MethodImplOptions.Synchronized)]
        public double[] UsingElectricity()
        {
            double[] arr = new double[5];
            XElement configureRoot = XmlMethods.LoadFromXml(configurePath);
            arr[0] = double.Parse(configureRoot.Element("Available").Value);
            arr[1] = double.Parse(configureRoot.Element("Heavy").Value);
            arr[2] = double.Parse(configureRoot.Element("Light").Value);
            arr[3] = double.Parse(configureRoot.Element("Medium").Value);
            arr[4] = double.Parse(configureRoot.Element("_speed").Value);
            return arr;
        }
        #endregion
    }
}
