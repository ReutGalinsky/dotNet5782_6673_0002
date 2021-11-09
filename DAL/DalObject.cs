using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using IDAL.DO;
using DAL;

namespace DalObject
{
    //איך נדע שלא קיים האיבר?
    public class DalObject
    {
        public DalObject()//ctor
        {
            DataSource.Initialize();
        }
       
        #region AddDrone
        public void AddDrone(Drone drone)
        {
            if (DataSource.Drones.Find(d => d.IdNumber == drone.IdNumber).IdNumber!=0)//?
                throw new ExistingException("the drone is already exist");
            DataSource.Drones.Add(drone);
        }
        #endregion

        #region GetDrone
        public Drone GetDrone(int id)
        {
            Drone drone = DataSource.Drones.FirstOrDefault(d => d.IdNumber == id);
            if (drone.IdNumber==0)
                throw new NotExistingException("the drone is not exist");
            return drone;
        }
        #endregion

        #region DeleteDrone
        public void DeleteDrone(int id)
        {
            Drone drone=DataSource.Drones.Find(d=>d.IdNumber==id);
            if (drone.IdNumber==0)
                throw new NotExistingException("the drone is not existing");
            DataSource.Drones.Remove(drone);
        }
        #endregion

        #region UpdateDrone
        public void UpdateDrone(Drone toUpdate)
        {
            for (int i = 0; i < DataSource.Drones.Count; i++)
            {
                if (DataSource.Drones[i].IdNumber==toUpdate.IdNumber)
                {
                    Drone d=new Drone();
                    d.Model = toUpdate.Model;
                    d.MaxWeight= toUpdate.MaxWeight;
                    d.IdNumber = toUpdate.IdNumber;
                    DataSource.Drones[i]=d;
                    return;
                }
            }
            throw new NotExistingException("the drone is not exist");
            //Drone drone = DataSource.Drones.Find(x => x.IdNumber == toUpdate.IdNumber);
            //if (drone.IdNumber==0)
            //    throw new NotExistingException("the drone is not exist");
            //drone.Model = toUpdate.Model;
            //drone.MaxWeight = toUpdate.MaxWeight;
            //drone.IdNumber = toUpdate.IdNumber;
        }
        #endregion

        #region AddDroneCharge
        public void AddDroneCharge(DroneCharge dronecharge)///???האם יש אופציה שכבר קיים או שלא ייתכן
        {
            if (DataSource.Charges.Find(d => d.DroneId == dronecharge.DroneId).DroneId!=0)//?
                throw new ExistingException("the charge slot is already exist");
            DataSource.Charges.Add(dronecharge);
        }
        #endregion

        #region GetDroneCharge
        public DroneCharge GetDroneCharge(int id)
        {
            DroneCharge droneCharge = DataSource.Charges.FirstOrDefault(d => d.DroneId == id);
            if (droneCharge.DroneId==0)
                throw new NotExistingException("the charge slot is not exist");
            return droneCharge;
        }
        #endregion

        #region DeleteDroneCharge
        public void DeleteDroneCharge(int id)
        {
            DroneCharge droneCharge = DataSource.Charges.Find(d => d.DroneId == id);
            if (droneCharge.DroneId==0)
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
            //DroneCharge droneCharge = DataSource.Charges.Find(x => x.DroneId == toUpdate.DroneId);
            //if (droneCharge.DroneId==0)
            //    throw new NotExistingException("the charge slot is not exist");
            //droneCharge.DroneId = toUpdate.DroneId;
            //droneCharge.StationId = toUpdate.StationId;
        }
        #endregion

        #region AddParcel
        public void AddParcel(Parcel parcel)
        {
            if (DataSource.Parcels.Find(d => d.IdNumber == parcel.IdNumber).IdNumber!=0)//?
                throw new ExistingException("the parcel is already exist");
            DataSource.Parcels.Add(parcel);
        }
        #endregion

        #region GetParcel
        public Parcel GetParcel(int id)
        {
            Parcel parcel = DataSource.Parcels.FirstOrDefault(d => d.IdNumber == id);
            if (parcel.IdNumber==0)
                throw new NotExistingException("the parcel is not exist");
            return parcel;
        }
        #endregion

        #region DeleteParcel
        public void DeleteParcel(int id)
        {
            Parcel parcel = DataSource.Parcels.Find(d => d.IdNumber == id);
            if (parcel.IdNumber==0)
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
                    d.ClientGetName = toUpdate.ClientGetName;
                    d.ClientSendName = toUpdate.ClientSendName;
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
            //Parcel parcel = DataSource.Parcels.Find(x => x.IdNumber == toUpdate.IdNumber);
            //if (parcel.IdNumber==0)
            //    throw new NotExistingException("the parcel is not exist");
            //parcel.ArrivingDroneTime = toUpdate.ArrivingDroneTime;
            //parcel.ClientGetName = toUpdate.ClientGetName;
            //parcel.ClientSendName = toUpdate.ClientSendName;
            //parcel.collectingDroneTime = toUpdate.collectingDroneTime;
            //parcel.CreateParcelTime = toUpdate.CreateParcelTime;
            //parcel.DroneId = toUpdate.DroneId;
            //parcel.IdNumber = toUpdate.IdNumber;
            //parcel.MatchForDroneTime = toUpdate.MatchForDroneTime;
            //parcel.Priority = toUpdate.Priority;
            //parcel.Weight = toUpdate.Weight;
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

        #region GetDroneCharges
        public IEnumerable<DroneCharge> GetDroneCharges()
        {
            var charges = from item in DataSource.Charges
                         select item;
            return charges;
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
        //**************************************************************
        public void AddingBaseStation(BaseStation station)
        {
            DataSource.stations.Add(station);
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
            NewParcel.IdNumber = DataSource.Config.RunningNumber++;///?
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
        public List<Drone> GetDroness()
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
        /// returning list of the stations with availible charge slots
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
        /// <summary>
        /// matching the parcel for an availible drone
        /// </summary>
        /// <param name="temp"></param>
        public void ParcelToDrone(Parcel temp)
        {
            var par = DataSource.Parcels.FirstOrDefault(p => p.IdNumber == temp.IdNumber); //search for the correct parcel   
            var Dro = DataSource.Drones.FirstOrDefault(p => p.MaxWeight == par.Weight);
            //search for the first drone that is availible and proper in the weight-category
            //assumption: there is such a drone
            par.DroneId = Dro.IdNumber;
            par.MatchForDroneTime = DateTime.Now;
            var index = DataSource.Parcels.FindIndex(c => c.IdNumber == temp.IdNumber);
            DataSource.Parcels[index] = par;//update in the data base
            var index2 = DataSource.Drones.FindIndex(c => c.IdNumber == Dro.IdNumber);
            DataSource.Drones[index2] = Dro;
        }
        /// <summary>
        /// updating when the drone collect the parcel
        /// </summary>
        /// <param name="temp"></param>
        public void ParcelToCollecting(Parcel temp)
        {
            var par = DataSource.Parcels.FirstOrDefault(p => p.IdNumber == temp.IdNumber);
            par.collectingDroneTime = DateTime.Now;
            var index = DataSource.Parcels.FindIndex(c => c.IdNumber == temp.IdNumber);
            DataSource.Parcels[index] = par;
            
        }
        /// <summary>
        /// updating when the customer pick up the parcel
        /// </summary>
        /// <param name="temp"></param>
        public void ParcelToCustomer(Parcel temp)
        {
            var par = DataSource.Parcels.FirstOrDefault(p => p.IdNumber == temp.IdNumber);
            par.ArrivingDroneTime = DateTime.Now;
            var index = DataSource.Parcels.FindIndex(c => c.IdNumber == temp.IdNumber);
            DataSource.Parcels[index] = par;
            var Dro = DataSource.Drones.FirstOrDefault(p => par.DroneId == p.IdNumber);
            var index2 = DataSource.Drones.FindIndex(c => c.IdNumber == Dro.IdNumber);
            DataSource.Drones[index2] = Dro;
        }
        /// <summary>
        /// sending the drone for a charge slot
        /// </summary>
        /// <param name="D"></param>
        public void SendToCharge(DroneCharge D)
        {
            DataSource.Charges.Add(D);
            var Dro = DataSource.Drones.FirstOrDefault(P => P.IdNumber == D.DroneId);
            var index2 = DataSource.Drones.FindIndex(c => c.IdNumber == Dro.IdNumber);
            DataSource.Drones[index2] = Dro;
            var Bas = DataSource.stations.FirstOrDefault(P => P.IdNumber == D.StationId);
            Bas.ChargeSlots--;//updating the availibale stations
            var index1 = DataSource.stations.FindIndex(c => c.IdNumber == Bas.IdNumber);
            DataSource.stations[index1] = Bas;
        }
        /// <summary>
        /// releasing the charged drone
        /// </summary>
        /// <param name="d"></param>
        public void releaseCharge(Drone d)
        {
            var Dro = DataSource.Drones.FirstOrDefault(p => p.IdNumber == d.IdNumber);
            var index2 = DataSource.Drones.FindIndex(c => c.IdNumber == Dro.IdNumber);
            DataSource.Drones[index2] = Dro;
            var Char = DataSource.Charges.FirstOrDefault(p => p.DroneId == Dro.IdNumber);
            var Bas = DataSource.stations.FirstOrDefault(p => p.IdNumber == Char.StationId);
            Bas.ChargeSlots++;//updating the avilible stations
            var index1 = DataSource.stations.FindIndex(c => c.IdNumber == Bas.IdNumber);
            DataSource.stations[index1] = Bas;
            DataSource.Charges.Remove(Char);
        }
        /// <summary>
        /// removing parcel from the data base
        /// </summary>
        /// <param name="temp"></param>
        public void RemovePar(Parcel temp)
        {
            var par = DataSource.Parcels.FirstOrDefault(p => p.IdNumber == temp.IdNumber);
            DataSource.Parcels.Remove(par);

        }
        /// <summary>
        /// returning a single drone
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Drone getDrone(Drone id)
        {
            var Dro = DataSource.Drones.FirstOrDefault(P => P.IdNumber == id.IdNumber);
            if(Dro.IdNumber!=id.IdNumber)
            {
                Dro.IdNumber = 0;
            }
            return Dro;
        }
        /// <summary>
        /// returning a single base station
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BaseStation getBase(BaseStation id)
        {
            var Dro = DataSource.stations.FirstOrDefault(P => P.IdNumber == id.IdNumber);
            return Dro;
        }
        /// <summary>
        /// returning a single parcel
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Parcel getParcel(Parcel id)
        {
            var Dro = DataSource.Parcels.FirstOrDefault(P => P.IdNumber == id.IdNumber);
            return Dro;
        }
        /// <summary>
        /// returning a single customer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Customer getCustomer(Customer id)
        {
            var Dro = DataSource.Customers.FirstOrDefault(P => P.Id == id.Id);
            return Dro;
        }
    }
}
