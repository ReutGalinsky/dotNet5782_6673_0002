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


namespace Dal
{
    public class DalXml:DalApi.IDal
    {
        #region singleton
        class Nested
        {
            static Nested() { }
            internal static readonly IDal instance = new DalXml();
        }
        //static DalDXml() {}
        private DalXml() 
        {
        //    Initialize();
        }

        public static IDal Instance
        {
            get { return Nested.instance; }
        }
        #endregion
        string dronePath = @"Drones.xml";//XElement
        string chargePath = @"Charges.xml";//XElement
        string baseStationPath = @"BaseStations.xml";//XElement
        string parcelPath = @"Parcels.xml";//XElement
        string userPath = @"Users.xml";//XElement

        string customerPath = @"Customers.xml";//XElement
        string configurePath = @"Configure.xml";
        //*****Drones***********

        #region AddDrone
        public void AddDrone(Drone drone)
        {
            //if (DataSource.Drones.FirstOrDefault(d => d.IdNumber == drone.IdNumber).IdNumber != null)
            //    throw new ExistingException($"the drone with the id:{drone.IdNumber} is already exist");
            //DataSource.Drones.Add(drone);
            XElement droneRoot = XmlMethods.LoadFromXml(dronePath);
            var droneToAdd = (from droneItem in droneRoot.Elements()
                                  where (droneItem.Element("IdNumber").Value == drone.IdNumber)
                                  select droneItem).FirstOrDefault();
            if (droneToAdd != null)
                throw new DO.ExistingException("The Drone already exist in the system");
            XElement newDrone = new XElement("Drone"
                , new XElement("IdNumber", drone.IdNumber),
                new XElement("Model", drone.Model),
                new XElement("MaxWeight", drone.MaxWeight.ToString()));
            droneRoot.Add(newDrone);
            XmlMethods.SaveToXml(dronePath,droneRoot);
        }
        #endregion

        #region GetDrone
        public Drone GetDrone(string id)
        {
            XElement droneRoot = XmlMethods.LoadFromXml(dronePath);
            var droneToAdd = (from droneItem in droneRoot.Elements()
                              where (droneItem.Element("IdNumber").Value == id)
                              select droneItem).FirstOrDefault();
            if (droneToAdd == null)
                throw new DO.NotExistingException("The Drone not exist in the system");
            DO.Drone drone= new Drone();
            drone.IdNumber = droneToAdd.Element("IdNumber").Value;
            drone.Model = droneToAdd.Element("Model").Value;
            drone.MaxWeight = (DO.WeightCategories)Enum.Parse(typeof(DO.WeightCategories),droneToAdd.Element("MaxWeight").Value);
            return drone;
            //Drone drone = DataSource.Drones.FirstOrDefault(d => d.IdNumber == id);
            //if (drone.IdNumber == null)
            //    throw new NotExistingException($"the drone with the id:{id} is not exist");
            //return drone;
        }
        #endregion

        #region GetDrones
        public IEnumerable<Drone> GetDrones()
        {
            XElement droneRoot = XmlMethods.LoadFromXml(dronePath);
            var droneToAdd = (from droneItem in droneRoot.Elements()
                              select new DO.Drone { IdNumber=droneItem.Element("IdNumber").Value, Model= droneItem.Element("Model").Value, 
                                  MaxWeight= (DO.WeightCategories)Enum.Parse(typeof(DO.WeightCategories), droneItem.Element("MaxWeight").Value )});

            return droneToAdd;
            //var Drones = from item in DataSource.Drones
            //             select item;
            //return Drones;
        }
        #endregion

        #region DeleteDrone
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
                //Drone drone = DataSource.Drones.FirstOrDefault(d => d.IdNumber == id);
                //if (drone.IdNumber == null)
                //    throw new NotExistingException($"the drone with the id:{id} is not existing");
                //DataSource.Drones.Remove(drone);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        #endregion

        #region UpdateDrone
            public void UpdateDrone(Drone toUpdate)
        {
            XElement droneRoot = XmlMethods.LoadFromXml(dronePath);
            XElement drone = (from item in droneRoot.Elements()
                                       where (item.Element("IdNumber").Value) == toUpdate.IdNumber
                                       select item).FirstOrDefault();
            if (drone == null)
                throw new DO.NotExistingException("");
            drone.Element("Model").Value = toUpdate.Model;
            drone.Element("MaxWeight").Value = toUpdate.MaxWeight.ToString();
            
            droneRoot.Save(dronePath);
        }

        //for (int i = 0; i < DataSource.Drones.Count; i++)
        //{
        //    if (DataSource.Drones[i].IdNumber == toUpdate.IdNumber)
        //    {
        //        Drone drone = new Drone();
        //        drone.Model = toUpdate.Model;
        //        drone.MaxWeight = toUpdate.MaxWeight;
        //        drone.IdNumber = toUpdate.IdNumber;
        //        DataSource.Drones[i] = drone;
        //        return;
        //    }
        //}
        //throw new NotExistingException($"the drone with the id:{toUpdate.IdNumber} is not exist");
    
    #endregion

        #region GetAllDronesBy
    public IEnumerable<Drone> GetAllDronesBy(Predicate<Drone> condition)
    {
        XElement droneRoot = XmlMethods.LoadFromXml(dronePath);
            var droneToAdd = (from droneItem in droneRoot.Elements()
                              let drone = new DO.Drone() { IdNumber = droneItem.Element("IdNumber").Value, Model = droneItem.Element("Model").Value, MaxWeight = (DO.WeightCategories)Enum.Parse(typeof(DO.WeightCategories), droneItem.Element("MaxWeight").Value) }
                              where (condition(drone))
                              select drone);          
        return droneToAdd;
        //var list = from item in DataSource.Drones
        //           where condition(item)
        //           select item;
        //return list;
    }
        #endregion

        //*******charge********

        #region AddDroneCharge
        public void AddDroneCharge(DroneCharge dronecharge)
        {

            var listCharges = XmlMethods.LoadListFromXMLSerializer<DroneCharge>(chargePath);
            if (listCharges.FirstOrDefault(d => d.DroneId == dronecharge.DroneId).DroneId != null)
                throw new ExistingException($"the charge slot with the drone id:{dronecharge.DroneId} is already exist");
            listCharges.Add(dronecharge);
            XmlMethods.SaveListToXMLSerializer<DroneCharge>(listCharges,chargePath);
        }
        #endregion

        #region GetDroneCharge
        public DroneCharge GetDroneCharge(string id)
        {
            var listCharges = XmlMethods.LoadListFromXMLSerializer<DroneCharge>(chargePath).FirstOrDefault(x=>x.DroneId == id);
            if (listCharges.DroneId == null)
                throw new NotExistingException($"the charge slot with the drone id:{id} is not exist");
            return listCharges;

            //DroneCharge droneCharge = DataSource.Charges.FirstOrDefault(d => d.DroneId == id);
            //if (droneCharge.DroneId == null)
            //    throw new NotExistingException($"the charge slot with the drone id:{id} is not exist");
            //return droneCharge;
        }
        #endregion

        #region GetDroneCharges
        public IEnumerable<DroneCharge> GetDroneCharges()
        {
            var listCharges = XmlMethods.LoadListFromXMLSerializer<DroneCharge>(chargePath);
            var DroneCharges = from item in listCharges
                               select item;
            return DroneCharges;
            //var charges = from item in DataSource.Charges
            //              select item;
            //return charges;
        }
        #endregion

        #region DeleteDroneCharge
        public void DeleteDroneCharge(string id)
        {
            var listCharges = XmlMethods.LoadListFromXMLSerializer<DroneCharge>(chargePath);
            var droneToDelete = listCharges.FirstOrDefault(d => d.DroneId == id);
            if (droneToDelete.DroneId == null)
                throw new NotExistingException($"the charge slot with the drone id:{id} is not exist");
            listCharges.Remove(droneToDelete);
            XmlMethods.SaveListToXMLSerializer<DroneCharge>(listCharges, chargePath);
            //DroneCharge droneCharge = DataSource.Charges.FirstOrDefault(d => d.DroneId == id);
            //if (droneCharge.DroneId == null)
            //    throw new NotExistingException($"the charge slot with the dorne id:{id} is not existing");
            //DataSource.Charges.Remove(droneCharge);
        }
        #endregion

        #region UpdateDroneCharge
        public void UpdateDroneCharge(DroneCharge toUpdate)
        {
            var listCharges = XmlMethods.LoadListFromXMLSerializer<DroneCharge>(chargePath);
            for (int i = 0; i < listCharges.Count; i++)
            {
                if (listCharges[i].DroneId == toUpdate.DroneId)
                {
                    DroneCharge droneCharge = new DroneCharge();
                    droneCharge.DroneId = toUpdate.DroneId;
                    droneCharge.StationId = toUpdate.StationId;
                    listCharges[i] = droneCharge;
                    XmlMethods.SaveListToXMLSerializer<DroneCharge>(listCharges, chargePath);
                    return;
                }
            }
            throw new NotExistingException($"the charge slot with the dorne id:{toUpdate.DroneId} is not existing");
        }
        #endregion

        #region GetAllChargeDronesBy
        public IEnumerable<DroneCharge> GetAllChargeDronesBy(Predicate<DroneCharge> condition)
        {
            var listCharges = XmlMethods.LoadListFromXMLSerializer<DroneCharge>(chargePath);
            var DroneCharges = from item in listCharges
                               where condition(item)
                               select item;
            return DroneCharges;

            //var list = from item in DataSource.Charges
            //           where condition(item)
            //           select item;
            //return list;
        }
        #endregion

        //*******parcel********

        #region AddParcel
        public string AddParcel(Parcel parcel)
        {

            XElement configureRoot = XmlMethods.LoadFromXml(configurePath);
            var runningNumber = configureRoot.Element("RunningNumber");
            parcel.IdNumber = runningNumber.Value;

            var listParcels = XmlMethods.LoadListFromXMLSerializer<Parcel>(parcelPath);
            if (listParcels.FirstOrDefault(d => d.IdNumber == parcel.IdNumber).IdNumber != null)
            {
                throw new ExistingException($"the parcel with the id:{parcel.IdNumber} is already exist");
            }
            listParcels.Add(parcel);
            XmlMethods.SaveListToXMLSerializer<Parcel>(listParcels, chargePath);
            runningNumber.Value = runningNumber.Value+1;
            configureRoot.Save(configurePath);
            return parcel.IdNumber;

            //parcel.IdNumber = DataSource.Config.RunningNumber++.ToString();
            //if (DataSource.Parcels.FirstOrDefault(d => d.IdNumber == parcel.IdNumber).IdNumber != null)
            //    throw new ExistingException($"the parcel with the id:{parcel.IdNumber} is already exist");
            //DataSource.Parcels.Add(parcel);
            //return parcel.IdNumber;
        }
        #endregion

        #region GetParcel
        public Parcel GetParcel(string id)
        {
            var parcel = XmlMethods.LoadListFromXMLSerializer<Parcel>(parcelPath).FirstOrDefault(x => x.IdNumber == id);
            if (parcel.IdNumber == null)
                throw new NotExistingException($"the parcel with the id:{id} is not exist");
            return parcel;

            //Parcel parcel = DataSource.Parcels.FirstOrDefault(d => d.IdNumber == id);
            //if (parcel.IdNumber == null)
            //    throw new NotExistingException($"the parcel with the id:{parcel.IdNumber} is not exist");
            //return parcel;
        }
        #endregion

        #region GetParcels
        public IEnumerable<Parcel> GetParcels()
        {
            var listParcels = XmlMethods.LoadListFromXMLSerializer<Parcel>(parcelPath);
            var Parcels = from item in listParcels
                               select item;
            return Parcels;
            //var parcels = from item in DataSource.Parcels
            //              select item;
            //return parcels;
        }
        #endregion

        #region DeleteParcel
        public void DeleteParcel(string id)
        {
            var listParcels = XmlMethods.LoadListFromXMLSerializer<Parcel>(parcelPath);
            var parcelToDelete = listParcels.FirstOrDefault(d => d.IdNumber == id);
            if (parcelToDelete.IdNumber == null)
                throw new NotExistingException($"the parcel with the id:{id} is not exist");
            listParcels.Remove(parcelToDelete);
            XmlMethods.SaveListToXMLSerializer<Parcel>(listParcels, parcelPath);

            //Parcel parcel = DataSource.Parcels.FirstOrDefault(d => d.IdNumber == id);
            //if (parcel.IdNumber == null)
            //    throw new NotExistingException($"the parcel with the id:{parcel.IdNumber} is not exist");
            //DataSource.Parcels.Remove(parcel);
        }
        #endregion

        #region UpdateParcel
        public void UpdateParcel(Parcel toUpdate)
        {
            var listParcels = XmlMethods.LoadListFromXMLSerializer<Parcel>(parcelPath);
            for (int i = 0; i < listParcels.Count; i++)
            {
                if (listParcels[i].IdNumber == toUpdate.IdNumber)
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
                    listParcels[i] = parcel;
                    XmlMethods.SaveListToXMLSerializer<Parcel>(listParcels, parcelPath);
                    return;
                }
            }
            throw new NotExistingException($"the parcel with the id:{toUpdate.IdNumber} is not existing");

        }
        #endregion

        #region GetAllParcelsBy
        public IEnumerable<Parcel> GetAllParcelsBy(Predicate<Parcel> condition)
        {
            var listParcels = XmlMethods.LoadListFromXMLSerializer<Parcel>(parcelPath);
            var Parcels = from item in listParcels
                          where condition(item)
                               select item;
            return Parcels;

            //var list = from item in DataSource.Parcels
            //           where condition(item)
            //           select item;
            //return list;
        }
        #endregion

        //*******base station********

        #region AddBaseStation
        public void AddBaseStation(BaseStation baseStation)
        {
            var listBaseStation = XmlMethods.LoadListFromXMLSerializer<BaseStation>(baseStationPath);
            if (listBaseStation.FirstOrDefault(d => d.IdNumber == baseStation.IdNumber).IdNumber != null)
            {
                throw new ExistingException($"the base station with the id:{baseStation.IdNumber} is exist");
            }
            listBaseStation.Add(baseStation);
            XmlMethods.SaveListToXMLSerializer<BaseStation>(listBaseStation, baseStationPath);
            //if (DataSource.Stations.FirstOrDefault(d => d.IdNumber == baseStation.IdNumber).IdNumber != null)
            //    throw new ExistingException($"the baseStation with the id:{baseStation.IdNumber} is already exist");
            //DataSource.Stations.Add(baseStation);
        }
        #endregion

        #region GetBaseStation
        public BaseStation GetBaseStation(string id)
        {
            var baseStation = XmlMethods.LoadListFromXMLSerializer<BaseStation>(baseStationPath).FirstOrDefault(x => x.IdNumber == id);
            if (baseStation.IdNumber == null)
                throw new NotExistingException($"the base station with the id:{id} is not exist");
            return baseStation;
            //BaseStation baseStation = DataSource.Stations.FirstOrDefault(d => d.IdNumber == id);
            //if (baseStation.IdNumber == null)
            //    throw new NotExistingException($"the baseStation with the id:{baseStation.IdNumber} is not exist");
            //return baseStation;
        }
        #endregion

        #region GetBaseStations
        public IEnumerable<BaseStation> GetBaseStations()
        {
            var listBaseStations = XmlMethods.LoadListFromXMLSerializer<BaseStation>(baseStationPath);
            var BaseStations = from item in listBaseStations
                          select item;
            return BaseStations;

            //var BaseStations = from item in DataSource.Stations
            //                   select item;
            //return BaseStations;
        }
        #endregion

        #region DeleteBaseStation
        public void DeleteBaseStation(string id)
        {
            var listBaseStations = XmlMethods.LoadListFromXMLSerializer<BaseStation>(baseStationPath);
            var BaseStationToDelete = listBaseStations.FirstOrDefault(d => d.IdNumber == id);
            if (BaseStationToDelete.IdNumber == null)
                throw new NotExistingException($"the base station with the id:{id} is not exist");
            listBaseStations.Remove(BaseStationToDelete);
            XmlMethods.SaveListToXMLSerializer<BaseStation>(listBaseStations, baseStationPath);
            //    BaseStation baseStation = DataSource.Stations.FirstOrDefault(d => d.IdNumber == id);
            //    if (baseStation.IdNumber == null)
            //        throw new NotExistingException($"the baseStation with the id:{baseStation.IdNumber} is not exist");
            //    DataSource.Stations.Remove(baseStation);
        }
        #endregion

        #region UpdateBaseStation
        public void UpdateBaseStation(BaseStation toUpdate)
        {
            var listBaseStations = XmlMethods.LoadListFromXMLSerializer<BaseStation>(baseStationPath);
            for (int i = 0; i < listBaseStations.Count; i++)
            {
                if (listBaseStations[i].IdNumber == toUpdate.IdNumber)
                {
                    BaseStation baseStation = new BaseStation();
                    baseStation.ChargeSlots = toUpdate.ChargeSlots;
                    baseStation.Name = toUpdate.Name;
                    baseStation.IdNumber = toUpdate.IdNumber;
                    baseStation.Longitude = toUpdate.Longitude;
                    baseStation.Latitude = toUpdate.Latitude;
                    listBaseStations[i] = baseStation;
                    XmlMethods.SaveListToXMLSerializer<BaseStation>(listBaseStations, baseStationPath);
                    return;
                }
            }
            throw new NotExistingException($"the base station with the id:{toUpdate.IdNumber} is not existing");
            //for (int i = 0; i < DataSource.Stations.Count; i++)
            //{
            //    if (DataSource.Stations[i].IdNumber == toUpdate.IdNumber)
            //    {
            //        BaseStation baseStation = new BaseStation();
            //        baseStation.ChargeSlots = toUpdate.ChargeSlots;
            //        baseStation.Name = toUpdate.Name;
            //        baseStation.IdNumber = toUpdate.IdNumber;
            //        baseStation.Longitude = toUpdate.Longitude;
            //        baseStation.Latitude = toUpdate.Latitude;
            //        DataSource.Stations[i] = baseStation;
            //        return;
            //    }
            //}
            //throw new NotExistingException($"the baseStation with the id:{toUpdate.IdNumber} is not exist");
        }
        #endregion

        #region GetAllBaseStationsBy
        public IEnumerable<BaseStation> GetAllBaseStationsBy(Predicate<BaseStation> condition)
        {
            var listBaseStation = XmlMethods.LoadListFromXMLSerializer<BaseStation>(baseStationPath);
            var baseStations = from item in listBaseStation
                               where condition(item)
                          select item;
            return baseStations;

            //var list = from item in DataSource.Stations
            //           where condition(item)
            //           select item;
            //return list;
        }
        #endregion

        //*******customer********

        #region AddCustomer
        public void AddCustomer(Customer customer)
        {
            var listCustomers = XmlMethods.LoadListFromXMLSerializer<Customer>(customerPath);
            if (listCustomers.FirstOrDefault(d => d.IdNumber == customer.IdNumber).IdNumber != null)
                throw new ExistingException($"the charge slot with the drone id:{customer.IdNumber} is already exist");
            listCustomers.Add(customer);
            XmlMethods.SaveListToXMLSerializer<Customer>(listCustomers, customerPath);
        

        //if (DataSource.Customers.FirstOrDefault(d => d.IdNumber == customer.IdNumber).IdNumber != null)
        //    throw new ExistingException($"the customer with the id:{customer.IdNumber} is already exist");
        //DataSource.Customers.Add(customer);
    }
    #endregion

        #region GetCustomer
    public Customer GetCustomer(string id)
        {
            var customer = XmlMethods.LoadListFromXMLSerializer<Customer>(customerPath).FirstOrDefault(x => x.IdNumber == id);
            if (customer.IdNumber == null)
                throw new NotExistingException($"the customer with the id:{id} is not exist");
            return customer;

            //Customer customer = DataSource.Customers.FirstOrDefault(d => d.IdNumber == id);
            //if (customer.IdNumber == null)
            //    throw new NotExistingException($"the customer with the id:{customer.IdNumber} is not exist");
            //return customer;
        }
        #endregion

        #region GetCustomers
        public IEnumerable<Customer> GetCustomers()
        {
            var listCustomers = XmlMethods.LoadListFromXMLSerializer<Customer>(customerPath);
            var Customer = from item in listCustomers
                               select item;
            return Customer;

            //var Customers = from item in DataSource.Customers
            //                select item;
            //return Customers;
        }
        #endregion

        #region DeleteCustomer
        public void DeleteCustomer(string id)
        {
            var listCustomer = XmlMethods.LoadListFromXMLSerializer<Customer>(customerPath);
            var CustomerToDelete = listCustomer.FirstOrDefault(d => d.IdNumber == id);
            if (CustomerToDelete.IdNumber == null)
                throw new NotExistingException($"the customer with the id:{id} is not exist");
            listCustomer.Remove(CustomerToDelete);
            XmlMethods.SaveListToXMLSerializer<Customer>(listCustomer, baseStationPath);

            //Customer customer = DataSource.Customers.FirstOrDefault(d => d.IdNumber == id);
            //if (customer.IdNumber == null)
            //    throw new NotExistingException($"the customer with the id:{customer.IdNumber} is not exist");
            //DataSource.Customers.Remove(customer);
        }
        #endregion

        #region UpdateCustomer
        public void UpdateCustomer(Customer toUpdate)
        {
            var listCustomers = XmlMethods.LoadListFromXMLSerializer<Customer>(customerPath);
            for (int i = 0; i < listCustomers.Count; i++)
            {
                if (listCustomers[i].IdNumber == toUpdate.IdNumber)
                {
                    Customer customer = new Customer();
                    customer.IdNumber = toUpdate.IdNumber;
                    customer.Name = toUpdate.Name;
                    customer.Phone = toUpdate.Phone;
                    customer.Longitude = toUpdate.Longitude;
                    customer.Latitude = toUpdate.Latitude;
                    listCustomers[i] = customer;
                    XmlMethods.SaveListToXMLSerializer<Customer>(listCustomers, customerPath);
                    return;
                }
            }
            throw new NotExistingException($"the customer with the id:{toUpdate.IdNumber} is not existing");
            //for (int i = 0; i < DataSource.Customers.Count; i++)
            //{
            //    if (DataSource.Customers[i].IdNumber == toUpdate.IdNumber)
            //    {
            //        Customer customer = new Customer();
            //        customer.IdNumber = toUpdate.IdNumber;
            //        customer.Name = toUpdate.Name;
            //        customer.Phone = toUpdate.Phone;
            //        customer.Longitude = toUpdate.Longitude;
            //        customer.Latitude = toUpdate.Latitude;
            //        DataSource.Customers[i] = customer;
            //        return;
            //    }
            //}
            //throw new NotExistingException($"the customer with the id:{toUpdate.IdNumber} is not exist");
        }
        #endregion

        #region GetAllCustomersBy
        public IEnumerable<Customer> GetAllCustomersBy(Predicate<Customer> condition)
        {
            var listCustomers = XmlMethods.LoadListFromXMLSerializer<Customer>(customerPath);
            var customers = from item in listCustomers
                               where condition(item)
                               select item;
            return customers;

            //var list = from item in DataSource.Customers
            //           where condition(item)
            //           select item;
            //return list;
        }
        #endregion
        //********User*************
        public void AddUser(User user)
        {
            var listUsers = XmlMethods.LoadListFromXMLSerializer<User>(userPath);
            if (listUsers.FirstOrDefault(d => d.UserName == user.UserName).UserName != null)
                throw new ExistingException($"the user with the user-name:{user.UserName} is already exist");
            listUsers.Add(user);
            XmlMethods.SaveListToXMLSerializer<User>(listUsers, customerPath);

            //if (DataSource.Users.FirstOrDefault(d => d.UserName == user.UserName).UserName != null)
            //    throw new ExistingException($"the user with the name:{user.UserName} is already exist");
            //DataSource.Users.Add(user);

        }
        public void DeleteUser(string userName)
        {
            var listUsers = XmlMethods.LoadListFromXMLSerializer<User>(userPath);
            var UserToDelete = listUsers.FirstOrDefault(d => d.UserName == userName);
            if (UserToDelete.UserName == null)
                throw new NotExistingException($"the user with the user-name:{userName} is not exist");
            listUsers.Remove(UserToDelete);
            XmlMethods.SaveListToXMLSerializer<User>(listUsers, userPath);

            //User user = DataSource.Users.FirstOrDefault(d => d.UserName == userName);
            //if (user.UserName == null)
            //    throw new NotExistingException($"the User with the name:{user.UserName} is not exist");
            //DataSource.Users.Remove(user);
        }
        public User GetUser(string userName)
        {
            var user = XmlMethods.LoadListFromXMLSerializer<User>(userPath).FirstOrDefault(x => x.UserName == userName);
            if (user.UserName == null)
                throw new NotExistingException($"the user with the user-name:{userName} is not exist");
             return user;
            //User user = DataSource.Users.FirstOrDefault(d => d.UserName == userName);
            //if (user.UserName == null)
            //    throw new NotExistingException($"the customer with the id:{user.UserName} is not exist");
            //return user;
        }
        public void UpdateUser(User toUpdate)
        {
            var listUsers = XmlMethods.LoadListFromXMLSerializer<User>(userPath);
            for (int i = 0; i < listUsers.Count; i++)
            {
                if (listUsers[i].UserName == toUpdate.UserName)
                {
                    User user = new User();
                    user.UserName = toUpdate.UserName;
                    user.UserPassword = toUpdate.UserPassword;
                    user.isManager = toUpdate.isManager;
                    listUsers[i] = user;
                    XmlMethods.SaveListToXMLSerializer<User>(listUsers, userPath);
                    return;
                }
            }
            throw new NotExistingException($"the user with the user-name:{toUpdate.UserName} is not existing");

            //for (int i = 0; i < DataSource.Users.Count; i++)
            //{
            //    if (DataSource.Users[i].UserName == toUpdate.UserName)
            //    {
            //        User user = new User();
            //        user.UserName = toUpdate.UserName;
            //        user.UserPassword = toUpdate.UserPassword;
            //        user.isManager = toUpdate.isManager;
            //        DataSource.Users[i] = user;
            //        return;
            //    }
            //}
            //throw new NotExistingException($"the user with the name:{toUpdate.UserName} is not exist");
        }
        public IEnumerable<User> GetUsers()
        {
            var listUsers = XmlMethods.LoadListFromXMLSerializer<User>(userPath);
            var User = from item in listUsers
                       select item;
            return User;

            //var Customers = from item in DataSource.Users
            //                select item;
            //return Customers;
        }
        public IEnumerable<User> GetAllUsersBy(Predicate<User> condition)
        {
            var listUsers = XmlMethods.LoadListFromXMLSerializer<User>(userPath);
            var user = from item in listUsers
                            where condition(item)
                            select item;
            return user;

            //var list = from item in DataSource.Users
            //           where condition(item)
            //           select item;
            //return list;
        }


        //*******electricity********

        #region UsingElectricity
        /// <summary>
        /// returnnig the values of the battery change
        /// </summary>
        /// <returns>the values of the battery loose and gain</returns>
        public double[] UsingElectricity()
        //function that return the electricity values
        {
            double[] arr = new double[5];
            XElement configureRoot = XmlMethods.LoadFromXml(configurePath);
            arr[0]= double.Parse(configureRoot.Element("Available").Value);
            arr[1] = double.Parse(configureRoot.Element("Heavy").Value);
            arr[2] = double.Parse(configureRoot.Element("Light").Value);
            arr[3] = double.Parse(configureRoot.Element("Medium").Value);
            arr[4] = double.Parse(configureRoot.Element("_speed").Value);
            return arr;
        }
        #endregion
    }
}
