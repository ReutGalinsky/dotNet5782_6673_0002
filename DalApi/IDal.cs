using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DalApi
{
    public interface IDal
    {
        /// <summary>
        /// Getting the electricity details
        /// </summary>
        /// <returns>array of the electricity details</returns>
        public double[] UsingElectricity();
        /// <summary>
        /// Adding drone
        /// </summary>
        /// <param name="drone">the drone that we add</param>
        public void AddDrone(Drone drone);
        /// <summary>
        /// Getting drone
        /// </summary>
        /// <param name="id">the id of the wanted drone</param>
        /// <returns>the wanted drone</returns>
        public Drone GetDrone(string id);
        /// <summary>
        /// Deleting drone
        /// </summary>
        /// <param name="id">the id of the drone</param>
        public void DeleteDrone(string id);
        /// <summary>
        /// Updating details of drone
        /// </summary>
        /// <param name="toUpdate">the updated drone</param>
        public void UpdateDrone(Drone toUpdate);
        /// <summary>
        /// Adding Drone-charge 
        /// </summary>
        /// <param name="dronecharge">the drone-charge to add</param>
        public void AddDroneCharge(DroneCharge dronecharge);
        /// <summary>
        /// Getting drone-charge
        /// </summary>
        /// <param name="id"></param>
        /// <returns>the wanted drone-charge</returns>
        public DroneCharge GetDroneCharge(string id);
        /// <summary>
        /// Deleting drone-charge
        /// </summary>
        /// <param name="id">the id of the drone</param>
        public void DeleteDroneCharge(string id);
        /// <summary>
        /// Updating details of drone-charge
        /// </summary>
        /// <param name="toUpdate">the updated drone-charge</param>
        public void UpdateDroneCharge(DroneCharge toUpdate);
        /// <summary>
        /// Adding parcel
        /// </summary>
        /// <param name="parcel">the parcel to add</param>
        /// <returns>the id number of the new parcel</returns>
        public string AddParcel(Parcel parcel);
        /// <summary>
        /// Adding user
        /// </summary>
        /// <param name="user">the user to add</param>
        public void AddUser(User user);
        /// <summary>
        /// Deleting user
        /// </summary>
        /// <param name="userName"> the user name of the wanted user</param>
        public void DeleteUser(string userName);
        /// <summary>
        /// Getting user
        /// </summary>
        /// <param name="userName">the user name of the wanted user</param>
        /// <returns>the wanted user</returns>
        public User GetUser(string userName);
        /// <summary>
        /// Updating user
        /// </summary>
        /// <param name="toUpdate">the updated user</param>
        public void UpdateUser(User toUpdate);
        /// <summary>
        /// Getting all of the users
        /// </summary>
        /// <returns>collection of all users </returns>
        public IEnumerable<User> GetUsers();
        /// <summary>
        /// Getting all users under condition
        /// </summary>
        /// <param name="condition">the condition</param>
        /// <returns>collection of the correct users</returns>
        public IEnumerable<User> GetAllUsersBy(Predicate<User> condition);
        /// <summary>
        /// Getting parcel
        /// </summary>
        /// <param name="id">the id of the parcel</param>
        /// <returns>the wanted parcel</returns>
        public Parcel GetParcel(string id);
        /// <summary>
        /// Deleting parcel
        /// </summary>
        /// <param name="id">the wanted parcel</param>
        public void DeleteParcel(string id);
        /// <summary>
        /// Updating parcel
        /// </summary>
        /// <param name="toUpdate">the updated parcel</param>
        public void UpdateParcel(Parcel toUpdate);
        /// <summary>
        /// Getting all of the drones
        /// </summary>
        /// <returns>collection of the drones</returns>
        public IEnumerable<Drone> GetDrones();
        /// <summary>
        /// Getting all drone-charges
        /// </summary>
        /// <returns>collection of the drone-charges</returns>
        public IEnumerable<DroneCharge> GetDroneCharges();
        /// <summary>
        /// Getting all of the parcels
        /// </summary>
        /// <returns>collection of the parcels</returns>
        public IEnumerable<Parcel> GetParcels();
        /// <summary>
        /// Adding base station
        /// </summary>
        /// <param name="station">the new base station</param>
        public void AddBaseStation(BaseStation station);
        /// <summary>
        /// Getting base station
        /// </summary>
        /// <param name="id">the id of the base station</param>
        /// <returns>the wanted base station</returns>
        public BaseStation GetBaseStation(string id);
        /// <summary>
        /// Deleting base station
        /// </summary>
        /// <param name="id">the id of the wanted base statoin</param>
        public void DeleteBaseStation(string id);
        /// <summary>
        /// Updating details of base station
        /// </summary>
        /// <param name="toUpdate">the updated base station</param>
        public void UpdateBaseStation(BaseStation toUpdate);
        /// <summary>
        /// Adding customer
        /// </summary>
        /// <param name="customer">the new customer</param>
        public void AddCustomer(Customer customer);
        /// <summary>
        /// Getting customer
        /// </summary>
        /// <param name="id">the id of the wanted customer</param>
        /// <returns>the wanted customer</returns>
        public Customer GetCustomer(string id);
        /// <summary>
        /// Deleting customer
        /// </summary>
        /// <param name="id">the id of the wanted customer</param>
        public void DeleteCustomer(string id);
        /// <summary>
        /// Updating details of customer
        /// </summary>
        /// <param name="toUpdate">the updated customer</param>
        public void UpdateCustomer(Customer toUpdate);
        /// <summary>
        /// Getting customer
        /// </summary>
        /// <returns>the wanted customer</returns>
        public IEnumerable<Customer> GetCustomers();
        /// <summary>
        /// Getting all base stations
        /// </summary>
        /// <returns>collection of the base stations</returns>
        public IEnumerable<BaseStation> GetBaseStations();
        /// <summary>
        /// Getting all drones under condition
        /// </summary>
        /// <param name="condition">the condition</param>
        /// <returns>collection of the correct drones</returns>
        public IEnumerable<Drone> GetAllDronesBy(Predicate<Drone> condition);
        /// <summary>
        /// Getting all drone-charge under condition
        /// </summary>
        /// <param name="condition">the condition</param>
        /// <returns>collection of the correct drone-charges</returns>
        public IEnumerable<DroneCharge> GetAllChargeDronesBy(Predicate<DroneCharge> condition);
        /// <summary>
        /// Getting all customers under condition
        /// </summary>
        /// <param name="condition">the condition</param>
        /// <returns>collection of the correct customers</returns>
        public IEnumerable<Customer> GetAllCustomersBy(Predicate<Customer> condition);
        /// <summary>
        /// Getting parcels under condition
        /// </summary>
        /// <param name="condition">the condition</param>
        /// <returns>collection of the correct parcels</returns>
        public IEnumerable<Parcel> GetAllParcelsBy(Predicate<Parcel> condition);
        /// <summary>
        /// Getting base stations under condition
        /// </summary>
        /// <param name="condition">the condition</param>
        /// <returns>collection of the correct base stations</returns>
        public IEnumerable<BaseStation> GetAllBaseStationsBy(Predicate<BaseStation> condition);
        /// <summary>
        /// Clearing the drones charge in case they remain
        /// from the previous run.
        /// </summary>
        public void ClearDroneCharges() { }

    }
}