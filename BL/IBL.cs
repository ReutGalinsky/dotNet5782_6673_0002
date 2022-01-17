using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BLApi
{
    public interface IBL
    {
        /// <summary>
        /// Adding base station
        /// </summary>
        /// <param name="baseStationToAdd">the new base station</param>
        public void AddBaseStation(BaseStation baseStationToAdd);
        /// <summary>
        /// Getting all of the stations
        /// </summary>
        /// <returns>collection of the stations</returns>
        public IEnumerable<BaseStationToList> GetBaseStations();
        /// <summary>
        /// Updating details of station
        /// </summary>
        /// <param name="id">the id of the station</param>
        /// <param name="Name">the new name</param>
        /// <param name="numberOfCharge">the new amount of charge slots</param>
        public void UpdatingDetailsOfBaseStation(string id, string Name, string numberOfCharge);
        /// <summary>
        /// Getting base station
        /// </summary>
        /// <param name="id">the id of the station</param>
        /// <returns>the wanted station</returns>
        public BaseStation GetBaseStation(string id);
        /// <summary>
        /// Start charging
        /// </summary>
        /// <param name="number">the id of the drone</param>
        public void DroneToCharging(string number);
        /// <summary>
        /// Releasing drone from charging
        /// </summary>
        /// <param name="number">the id of the drone</param>
        public void DroneFromCharging(string number);
        /// <summary>
        /// Adding drone
        /// </summary>
        /// <param name="droneToAdd">the new drone</param>
        /// <param name="number">the id of the charging station</param>
        public void AddDrone(DroneToList droneToAdd, string number);
        /// <summary>
        /// Getting all of the drones
        /// </summary>
        /// <returns>collection of the drones</returns>
        public IEnumerable<DroneToList> GetDrones();
        /// <summary>
        /// Getting drone
        /// </summary>
        /// <param name="id">the id of the given drone</param>
        /// <returns>the wanted drone</returns>
        public Drone GetDrone(string id);
        /// <summary>
        /// Updating details of drone
        /// </summary>
        /// <param name="Model">the new model</param>
        /// <param name="id">the id of the wanted drone</param>
        public void UpdatingDetailsOfDrone(string Model, string id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerToAdd"></param>
        public void AddCustomer(Customer customerToAdd);
        /// <summary>
        /// Adding parcel
        /// </summary>
        /// <param name="parcel">the new parcel</param>
        /// <returns></returns>
        public string AddParcelToDelivery(ParcelOfList parcel);
        /// <summary>
        /// Updating details of customer
        /// </summary>
        /// <param name="id">the id of the wanted customer</param>
        /// <param name="Name">the new name</param>
        /// <param name="phone">the new phone</param>
        public void UpdatingDetailsOfCustomer(string id, string Name, string phone);
        /// <summary>
        /// Getting all of the customers
        /// </summary>
        /// <returns>collection of the customers</returns>
        public IEnumerable<CustomerToList> GetCustomers();
        /// <summary>
        /// Getting customer
        /// </summary>
        /// <param name="id">the id of the wanted customer</param>
        /// <returns>the wanted customer</returns>
        public Customer GetCustomer(string id);
        /// <summary>
        /// Matching parcel to a given drone
        /// </summary>
        /// <param name="id">the id of the drone</param>
        public void MatchingParcelToDrone(string id);
        /// <summary>
        /// Picking parcel fron it's sender by a drone
        /// </summary>
        /// <param name="id">the id of the drone</param>
        public void PickingParcelByDrone(string id);
        /// <summary>
        /// Supplying parcel that on a given drone
        /// </summary>
        /// <param name="id">the id of the drone</param>
        public void SupplyingParcelByDrone(string id);
        /// <summary>
        /// Getting parcel
        /// </summary>
        /// <param name="id">the id of the wanted parcel</param>
        /// <returns>the wanted parcel</returns>
        public Parcel GetParcel(string id);
        /// <summary>
        /// Getting all of the parcels
        /// </summary>
        /// <returns>collection of the parcels</returns>
        public IEnumerable<ParcelOfList> GetParcels();
        /// <summary>
        /// Getting all base stations under condition
        /// </summary>
        /// <param name="c">the condition</param>
        /// <returns>collection of the correct stations</returns>
        public IEnumerable<BaseStationToList> GetAllBaseStationsBy(Predicate<BaseStationToList> c);
        /// <summary>
        /// Getting all drones under condition
        /// </summary>
        /// <param name="c">the condition</param>
        /// <returns>collection of the correct drones</returns>
        public IEnumerable<DroneToList> GetAllDronesBy(Predicate<DroneToList> c);
        /// <summary>
        /// Getting all parcels under condition
        /// </summary>
        /// <param name="c">the condition</param>
        /// <returns>collection of the correct parcels</returns>
        public IEnumerable<ParcelOfList> GetAllParcelsBy(Predicate<ParcelOfList> c);
        /// <summary>
        /// Getting all customers under condition
        /// </summary>
        /// <param name="c">the condition</param>
        /// <returns>collection of the correct customers</returns>
        public IEnumerable<CustomerToList> GetAllCustomersBy(Predicate<CustomerToList> c);
        /// <summary>
        /// Deleting parcel
        /// </summary>
        /// <param name="number">the id of the parcel</param>
        public void RemoveParcel(string number);
        /// <summary>
        /// Deleting user
        /// </summary>
        /// <param name="name">the user name of the wanted user</param>
        public void RemoveUser(string name);
        /// <summary>
        /// Getting all users under condition
        /// </summary>
        /// <param name="c">the condition</param>
        /// <returns>collection of the correct users</returns>
        public IEnumerable<User> GetAllUsersBy(Predicate<User> c);
        /// <summary>
        /// Getting all of the users
        /// </summary>
        /// <returns>collection of the users</returns>
        public IEnumerable<User> GetUsers();
        /// <summary>
        /// Getting user
        /// </summary>
        /// <param name="name">the user name</param>
        /// <returns>the wanted user</returns>
        public User GetUser(string name);
        /// <summary>
        /// Updating details of user
        /// </summary>
        /// <param name="name">the user name</param>
        /// <param name="password">the new password</param>
        public void UpdatingDetailsOfUser(string name, string password);
        /// <summary>
        /// Adding user
        /// </summary>
        /// <param name="userToAdd">the new user</param>
        public void AddUser(User userToAdd);
        /// <summary>
        /// The simulator operating function
        /// </summary>
        /// <param name="id">the id of the drone</param>
        /// <param name="updatePl">the proggress updating function </param>
        /// <param name="checkStop">the stop condition</param>
        public void Simulator(string id, Action updatePl, Func<bool> checkStop);










    }
}