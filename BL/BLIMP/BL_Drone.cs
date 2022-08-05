using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using BO;
using System.Runtime.CompilerServices;

namespace BL
{
    /// <summary>
    /// all possible actions on drones 
    /// </summary>
    internal partial class BL : BLApi.IBL
    {
        internal DalApi.IDal dal;
        internal List<BO.DroneToList> Drones = new List<DroneToList>();
        internal double _available;
        internal double _heavy;
        internal double _light;
        internal double _medium;
        internal double _speed;

        #region singelton
        class Nested
        {
            static Nested() { }
            internal static readonly BLApi.IBL instance = new BL();
        }
        static BL() { }
        public static BLApi.IBL Instance
        { get { return Nested.instance; } }
        BL()
        {
            dal = DalApi.DLFactory.GetDal();
            double[] arr = dal.UsingElectricity();
            _available = arr[0];
            _heavy = arr[1];
            _light = arr[2];
            _medium = arr[3];
            _speed = arr[4];
            foreach (var item in dal.GetDrones())
            {
                try
                { Drones.Add(GetDroneToList(item.IdNumber)); }
                catch (Exception e)
                { throw new AddingProblemException("Error in the Constructor", e); }
            }
        }
        #endregion


        #region AddDrone
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDrone(BO.DroneToList droneToAdd, string IdNumber)
        {
            if (droneToAdd.MaxWeight != BO.WeightCategories.Heavy && droneToAdd.MaxWeight != BO.WeightCategories.Middle && droneToAdd.MaxWeight != BO.WeightCategories.Light)
                throw new AddingProblemException($"The weight of the drone {droneToAdd.IdNumber} is not an option");
            if (droneToAdd.Model == "")
                throw new AddingProblemException($"The model of the drone {droneToAdd.IdNumber} wasn't entered");
            int tempInteger;
            if (int.TryParse(droneToAdd.IdNumber, out tempInteger) == false)
                throw new AddingProblemException($"The id {droneToAdd.IdNumber} of the drone is illegal");
            if (tempInteger == 0)
                throw new AddingProblemException($"The id {droneToAdd.IdNumber} of the drone is illegal");

            try //assumption: drone need to get charging when it's being added
            {
                Random rand = new Random();
                droneToAdd.Battery = rand.Next(20, 40);
                droneToAdd.Location = GetBaseStation(IdNumber).Location.GetLocation();
                lock (dal)
                {

                    var stationDO = dal.GetBaseStation(IdNumber);
                    if (stationDO.ChargeSlots == 0)
                        throw new ChargingException($"there is not any slot for charging in base station with id {IdNumber}");
                    dal.AddDrone((DO.Drone)droneToAdd.CopyPropertiesToNew(typeof(DO.Drone)));
                    droneToAdd.State = DroneState.maintaince;
                    stationDO.ChargeSlots--;
                    dal.UpdateBaseStation(stationDO);
                    Drones.Add(droneToAdd);
                    dal.AddDroneCharge(new DroneCharge() { DroneId = droneToAdd.IdNumber, StationId = IdNumber, startCharging = DateTime.Now });
                }
            }
            catch (Exception ex)
            { throw new AddingProblemException($"Can't add the drone {droneToAdd.IdNumber}: {ex.Message}", ex); }
        }
        #endregion

        #region GetDrones
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BO.DroneToList> GetDrones()
        {
            return from item in Drones select item.Clone();
        }
        #endregion

        #region GetDrone
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Drone GetDrone(string id)
        {
            try
            {
                BO.DroneToList droneBL = Drones.FirstOrDefault(x => x.IdNumber == id);
                if (droneBL == null)
                    throw new GettingProblemException($"the drone with the id {id} is not exist");
                BO.Drone drone = (BO.Drone)droneBL.CopyPropertiesToNew(typeof(BO.Drone));
                drone.Location = droneBL.Location.GetLocation();
                drone.State = droneBL.State;
                drone.Battery = droneBL.Battery;
                drone.PassedParcel = droneBL.NumberOfParcel != null ? GetParcelInPassing(droneBL.NumberOfParcel) : null;
                if (drone.PassedParcel != null)
                {
                    if (drone.PassedParcel.isCollected == true)
                        drone.PassedParcel.Distance = drone.PassedParcel.Packing.DistanceTo(drone.PassedParcel.Destination);
                    else
                        drone.PassedParcel.Distance = drone.Location.DistanceTo(drone.PassedParcel.Packing);
                }
                return drone;
            }
            catch (Exception e)
            {
                throw new GettingProblemException(e.Message, e);
            }
        }
        #endregion

        #region UpdatingDetailsOfDrone
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdatingDetailsOfDrone(string Model, string id)
        {
            if (Model == "")
                throw new UpdatingException($"The model of drone number{id}is illegal");
            BO.DroneToList drone = Drones.FirstOrDefault(x => x.IdNumber == id);
            if (drone == null)
                throw new UpdatingException($"Drone number {id} is not existing");
            string temp = drone.Model;
            drone.Model = Model;
            try
            {
                lock (dal)
                {

                    dal.UpdateDrone((DO.Drone)drone.CopyPropertiesToNew(typeof(DO.Drone)));//update in DAL
                }
            }
            catch (Exception e)
            {
                drone.Model = temp;
                throw new UpdatingException($"Can't update drone number {id}: {e.Message}", e);
            }
        }
        #endregion

        #region GetAllDronesBy
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BO.DroneToList> GetAllDronesBy(Predicate<BO.DroneToList> condition)
        {
            var list = from item in GetDrones()
                       where condition(item)
                       select item;
            return list;
        }
        #endregion

        #region Simulator
        public void Simulator(string id, Action updatePl, Func<bool> checkStop)
        {
            try
            {
                DroneSimulator simulator = new DroneSimulator(this, id, updatePl, checkStop);
            }
            catch (Exception e)
            {
                throw new ConnectionException($"error in the simulator: {e.Message}");
            }
        }
        #endregion


        #region GetParcelInPassing
        /// <summary>
        /// returning the correct parcel as 'parcel in passing'
        /// </summary>
        /// <param name="id">the id of the parcel</param>
        /// <returns></returns>
        private BO.ParcelInPassing GetParcelInPassing(string id)
        {
            try
            {
                lock (dal)
                {
                    DO.Parcel parcel = dal.GetParcel(id);
                    BO.ParcelInPassing temp = (BO.ParcelInPassing)parcel.CopyPropertiesToNew(typeof(BO.ParcelInPassing));
                    temp.Destination = dal.GetCustomer(parcel.Geter).GetLocation();
                    temp.Packing = dal.GetCustomer(parcel.Sender).GetLocation();
                    temp.Senderer = GetCustomerOfParcel(parcel.Sender);
                    temp.Getterer = GetCustomerOfParcel(parcel.Geter);
                    temp.isCollected = parcel.CollectingDroneTime switch
                    {
                        null => false,
                        _ => true,
                    };
                    return temp;
                }
            }
            catch (Exception e)
            { throw new GettingProblemException(e.Message, e); }
        }
        #endregion
        #region GetDroneToList
        /// <summary>
        /// initialized the dal-drone as 'drone to list'
        /// </summary>
        /// <param name="id">the id of the drone</param>
        /// <returns></returns>
        private BO.DroneToList GetDroneToList(string id)
        {
            try
            {
                lock (dal)
                {
                    BO.DroneToList drone = (BO.DroneToList)dal.GetDrone(id).CopyPropertiesToNew(typeof(BO.DroneToList));
                    var parcel1 = dal.GetParcels().Where(x => x.DroneId == id && x.ArrivingDroneTime == null).FirstOrDefault();
                    Random rand = new Random();
                    if (parcel1.IdNumber != null)
                    {
                        drone.Location = parcelState(parcel1) switch
                        {
                            BO.ParcelState.match => ClosestStation(dal.GetCustomer(parcel1.Sender).GetLocation()).GetLocation(),
                            BO.ParcelState.pick => (dal.GetCustomer(parcel1.Sender).GetLocation()),
                            _ => throw new NotImplementedException(),
                        };
                        BO.ParcelOfList parcel3 = (BO.ParcelOfList)parcel1.CopyPropertiesToNew(typeof(BO.ParcelOfList));

                        drone.Battery = rand.Next((int)batteryUssage(drone, parcel3), 101);
                        drone.NumberOfParcel = parcel1.IdNumber;
                        drone.State = DroneState.shipping;
                        return drone;
                    }

                    int number = rand.Next(0, 2);//it's not shipping
                    switch (number)
                    {
                        case 0://the drone will be in maintaince mode
                            drone.Battery = rand.Next(0, 20);
                            var station = dal.GetBaseStations().Where(x => x.ChargeSlots > 0).Select(x => new { station = x, Location = x.GetLocation() }).ToList();
                            if (station.Count == 0) throw new AddingProblemException("there are no base stations");
                            int num = rand.Next(0, station.Count());
                            var element = station.ElementAt(num);
                            DO.BaseStation baseStaion = element.station;
                            drone.Location = element.Location;
                            drone.State = DroneState.maintaince;
                            baseStaion.ChargeSlots--;
                            dal.UpdateBaseStation(baseStaion);
                            DO.DroneCharge charge = new DroneCharge() { DroneId = drone.IdNumber, StationId = baseStaion.IdNumber, startCharging = DateTime.Now };
                            dal.AddDroneCharge(charge);
                            break;

                        case 1:// the drone is available
                            var list = from item in dal.GetParcels()
                                       where item.ArrivingDroneTime != null
                                       select item;
                            var list1 = list.ToList();
                            if (list1.Count == 0)
                            {
                                drone.Location = new Location() { Latitude = rand.NextDouble() + rand.Next(29, 33), Longitude = rand.NextDouble() + rand.Next(34, 36) };
                                drone.Battery = rand.Next(20, 101);
                            }
                            else
                            {
                                num = rand.Next(0, list.Count());
                                DO.Parcel P = list1.ElementAt(num);
                                drone.Location = dal.GetCustomer(((P.Geter))).GetLocation();
                                drone.Battery = rand.Next((int)(drone.Location.DistanceTo(ClosestStation(drone.Location).GetLocation()) * _available), 101);
                            }
                            drone.State = DroneState.Available;
                            break;
                        default:
                            break;
                    }
                    return drone;
                }
            }
            catch (Exception)
            {
                throw new AddingProblemException($"the drone number {id} is not correct, can't running this program");
            }

        }
        #endregion
        #region parcelState
        /// <summary>
        /// match formal state by the parcel's times
        /// </summary>
        /// <param name="parcel">the given parcel</param>
        /// <returns></returns>
        private ParcelState parcelState(DO.Parcel parcel)
        {
            if (parcel.CreateParcelTime != null && parcel.MatchForDroneTime == null)
                return BO.ParcelState.Define;
            if (parcel.CollectingDroneTime == null)
                return BO.ParcelState.match;
            if (parcel.ArrivingDroneTime == null)
                return BO.ParcelState.pick;
            return BO.ParcelState.supply;
        }
        #endregion
        #region ClosestStation
        /// <summary>
        /// returning the closest base station to a given location
        /// </summary>
        /// <param name="local"> the given Location</param>
        /// <returns></returns>
        private DO.BaseStation ClosestStation(Location local)
        {
            lock (dal)
            {
                var station = (from item in dal.GetBaseStations()
                               orderby local.DistanceTo(new Location() { Latitude = item.Latitude, Longitude = item.Longitude })
                               select item)
                       .FirstOrDefault();
                return station;
            }
        }
        #endregion

    }
}