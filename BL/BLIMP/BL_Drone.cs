using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using BO;


namespace BL
{
    /// <summary>
    /// all possible actions on drones 
    /// </summary>
    internal partial class BL : BLApi.IBL
    {
        private DalApi.IDal dal;
        private List<BO.DroneToList> Drones = new List<DroneToList>();
        private double _available;
        private double _heavy;
        private double _light;
        private double _medium;
        private double _speed;

        #region singelton
        class Nested
        {
            static Nested() { }
            internal static readonly BLApi.IBL instance = new BL();
        }
        static BL() { }
        public static BLApi.IBL Instance
        { get { return Nested.instance; } }
        /// <summary>
        /// constructor that create the list of drones for BL layer
        /// </summary>
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
                { throw new AddingProblemException("", e); }
            }
        }
        #endregion

        //**********************************

        #region AddDrone
        /// <summary>
        /// adding a new drone  
        /// </summary>
        public void AddDrone(BO.DroneToList droneToAdd, string IdNumber)
        {
            //validaion
            if (droneToAdd.MaxWeight != BO.WeightCategories.Heavy && droneToAdd.MaxWeight != BO.WeightCategories.Middle && droneToAdd.MaxWeight != BO.WeightCategories.Light)
                throw new AddingProblemException($"The weight of the drone {droneToAdd.IdNumber} is not an option");
            if (droneToAdd.Model == "")
                throw new AddingProblemException($"The model of the drone {droneToAdd.IdNumber} wasn't entered");
            int tempInteger;
            if (int.TryParse(droneToAdd.IdNumber, out tempInteger) == false)
                throw new AddingProblemException($"The drone {droneToAdd.IdNumber} can't add");

            //add
            try //assumption: drone need to get charging when it's being added
            {
                Random rand = new Random();
                droneToAdd.Battery = rand.Next(20, 40);
                droneToAdd.Location = GetBaseStation(IdNumber).Location.GetLocation();
                //Location location = (Location)GetBaseStation(number).Location.CopyPropertiesToNew(typeof(Location));
                //droneToAdd.Location = location;
                var stationDO = dal.GetBaseStation(IdNumber);
                //check charge slots in the base station
                if (stationDO.ChargeSlots == 0)
                    throw new ChargingException($"there is not any slot for charging in base station with id {IdNumber}");
                dal.AddDrone((DO.Drone)droneToAdd.CopyPropertiesToNew(typeof(DO.Drone)));//add to DAL
                droneToAdd.State = DroneState.maintaince;
                stationDO.ChargeSlots--;
                dal.UpdateBaseStation(stationDO);
                Drones.Add(droneToAdd);
                //DO.DroneCharge charge = new DroneCharge() { DroneId = droneToAdd.IdNumber, StationId = IdNumber };
                dal.AddDroneCharge(new DroneCharge() { DroneId = droneToAdd.IdNumber, StationId = IdNumber });
            }
            catch (Exception ex)
            { throw new AddingProblemException($"Can't add the drone {droneToAdd.IdNumber}", ex); }
        }
        #endregion

        #region GetDrones
        /// <summary>
        /// return all drones 
        /// </summary>
        public IEnumerable<BO.DroneToList> GetDrones()
        {
            return from item in Drones select item;
        }
        #endregion

        #region GetDrone
        /// <summary>
        /// return a single drone
        /// </summary>
        public BO.Drone GetDrone(string id)
        {
            //validation
            BO.DroneToList droneBL = Drones.FirstOrDefault(x => x.IdNumber == id);
            if (droneBL == null)
                throw new GettingProblemException($"the drone with the id {id} is not exist");
            BO.Drone drone = (BO.Drone)droneBL.CopyPropertiesToNew(typeof(BO.Drone));
            drone.Location = droneBL.Location.GetLocation();
            //drone.Location = new Location();
            //drone.Location.Latitude = droneBL.Location.Latitude;
            //drone.Location.Longitude = droneBL.Location.Longitude;
            drone.State = droneBL.State;
            drone.Battery = droneBL.Battery;
            drone.PassedParcel = droneBL.NumberOfParcel != null ? GetPIP(droneBL.NumberOfParcel) : null;
            //if (droneBL.NumberOfParcel != null)
            //    drone.PassedParcel = GetPIP(droneBL.NumberOfParcel);
            return drone;
        }
        #endregion

        #region UpdatingDetailsOfDrone
        /// <summary>
        //updating drone
        /// </summary>
        public void UpdatingDetailsOfDrone(string Model, string id)
        {
            //validation
            if (Model == "")
                throw new UpdatingException($"The model of drone number{id}is illegal");
            BO.DroneToList drone = Drones.FirstOrDefault(x => x.IdNumber == id);
            if (drone == null)
                throw new UpdatingException($"Drone number {id} is not existing");

            //update
            drone.Model = Model;
            try
            {
                dal.UpdateDrone((DO.Drone)drone.CopyPropertiesToNew(typeof(DO.Drone)));//update in DAL
            }
            catch (Exception e)
            {
                throw new UpdatingException($"Can't update drone number {id}", e);
            }
        }
        #endregion

        #region GetAllDronesBy
        /// <summary>
        // drone predicate
        /// </summary>
        public IEnumerable<BO.DroneToList> GetAllDronesBy(Predicate<BO.DroneToList> condition)
        {
            var list = from item in GetDrones()
                       where condition(item)
                       select item;
            return list;
        }
        #endregion 
        //***********return inner objects*************
        #region GetPIP
        private BO.ParcelInPassing GetPIP(string id)
        /// <summary>
        //private function that return object of ParcelInPassing
        /// </summary>
        {
            DO.Parcel parcel = dal.GetParcel(id);
            BO.ParcelInPassing temp = (BO.ParcelInPassing)parcel.CopyPropertiesToNew(typeof(BO.ParcelInPassing));
            temp.Destination = dal.GetCustomer(parcel.Geter).GetLocation();
            //Location get = new Location() { Latitude = dal.GetCustomer(p.Geter).Latitude, Longitude = dal.GetCustomer(p.Geter).Longitude };
            //Location send = new Location() { Latitude = dal.GetCustomer(p.Sender).Latitude, Longitude = dal.GetCustomer(p.Sender).Longitude };
            temp.Packing = dal.GetCustomer(parcel.Sender).GetLocation();
            temp.Distance = temp.Destination.DistanceTo(temp.Packing);
            //temp.Destination = get;
            temp.Senderer = GetCustomerOfParcel(parcel.Sender);
            temp.Getterer = GetCustomerOfParcel(parcel.Geter);
            temp.isCollected = parcel.CollectingDroneTime switch
            {
                null => false,
                _ => true,
            };
            //if (parcel.CollectingDroneTime == null)//the boolian value
            //    temp.isCollected = true;
            //else
            //    temp.isCollected = false;
            return temp;
        }
        #endregion
        private BO.DroneToList GetDroneToList(string id)
        //function that return object of DroneToList for constructor
        {
            try
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

                    drone.Battery = rand.Next(battarUseag(drone, parcel3), 101);
                    drone.NumberOfParcel = parcel1.IdNumber;
                    drone.State = DroneState.shipping;
                    return drone;
                }

                //foreach (var item in parcel)//search if the drone is in shipping mode
                //{
                //    if (item.CollectingDroneTime == null || item.ArrivingDroneTime == null)
                //    {
                //        drone.State = DroneState.shipping;
                //        double distance1 = 0, distance2;
                //        Location a = new Location() { Latitude = dal.GetCustomer((item.Sender)).Latitude, Longitude = dal.GetCustomer((item.Sender)).Longitude };
                //        if (item.CollectingDroneTime == null)
                //        {
                //            Location b = new Location() { Latitude = dal.GetBaseStations().First().Latitude, Longitude = dal.GetBaseStations().First().Longitude };
                //            foreach (var obj in dal.GetBaseStations())
                //            {
                //                Location temp = new Location() { Latitude = obj.Latitude, Longitude = obj.Longitude };
                //                if (temp.DistanceTo(a) < b.DistanceTo(a))
                //                    b = temp;
                //            }
                //            drone.Location = b;
                //            Location c = new Location() { Latitude = dal.GetCustomer((item.Geter)).Latitude, Longitude = dal.GetCustomer((item.Geter)).Longitude };
                //            Location d = new Location() { Latitude = ClosestStation(c).Latitude, Longitude = ClosestStation(c).Longitude };
                //            switch (item.Weight)
                //            {
                //                case DO.WeightCategories.Heavy:
                //                    distance1 = b.DistanceTo(c) * _heavy;
                //                    break;
                //                case DO.WeightCategories.Middle:
                //                    distance1 = b.DistanceTo(c) * _medium;
                //                    break;
                //                case DO.WeightCategories.Light:
                //                    distance1 = b.DistanceTo(c) * _light;
                //                    break;
                //                default:
                //                    break;
                //            }
                //            distance2 = c.DistanceTo(d) * _available;
                //            if ((int)(distance1 + distance2) > 100) throw new AddingProblemException("can't pass the parcel without charging in the middle of the shipping");
                //            drone.Battery = rand.Next((int)(distance1 + distance2), 101);
                //        }
                //        else
                //        {
                //            drone.Location = a;
                //            Location d = new Location() { Latitude = ClosestStation(a).Latitude, Longitude = ClosestStation(a).Longitude };
                //            distance2 = a.DistanceTo(d) * _available;
                //        }
                //        drone.NumberOfParcel = item.IdNumber;
                //        return drone;
                //    }
                //}
                int number = rand.Next(0, 2);//it's not shipping
                switch (number)
                {
                    case 0://the drone will be in maintaince mode
                        drone.Battery = rand.Next(0, 20);
                        var station = dal.GetBaseStations().Where(x=>x.ChargeSlots>0).Select(x => new { station = x, Location = x.GetLocation() }).ToList();
                        if (station.Count == 0) throw new AddingProblemException("there are no base stations");
                        int num = rand.Next(0, station.Count());
                        var element = station.ElementAt(num);
                        DO.BaseStation baseStaion = element.station;
                        drone.Location = element.Location;
                        //drone.Location = new Location() { Latitude = baseStaion.Latitude, Longitude = baseStaion.Longitude };
                        drone.State = DroneState.maintaince;
                        baseStaion.ChargeSlots--;
                        dal.UpdateBaseStation(baseStaion);
                        DO.DroneCharge charge = new DroneCharge() { DroneId = drone.IdNumber, StationId = baseStaion.IdNumber, startCharging=DateTime.Now };
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
                            //Location d = new Location() { Latitude = ClosestStation(drone.Location).Latitude, Longitude = ClosestStation(drone.Location).Longitude };
                            drone.Battery = rand.Next((int)(drone.Location.DistanceTo(ClosestStation(drone.Location).GetLocation()) * _available), 101);
                        }
                        drone.State = DroneState.Available;
                        break;
                    default:
                        break;
                }
                return drone;
            }
            catch (Exception e)
            {
                throw new AddingProblemException($"the drone number {id} is not correct, can't running this program");
            }

        }
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
        private DO.BaseStation ClosestStation(Location l)
        //private function that return the closest base station to a given location
        {
            //var list = dal.GetBaseStations();
            //DO.BaseStation baseStationToReturn;
            var station = (from item in dal.GetBaseStations()
                           orderby l.DistanceTo(new Location() { Latitude = item.Latitude, Longitude = item.Longitude })
                           select item)
                       .FirstOrDefault();
            return station;
            //Location temp = new Location() { Latitude = list.First().Latitude, Longitude = list.First().Longitude };
            //baseStationToReturn = list.First();
            //foreach (var item in list)
            //{
            //    Location l2 = new Location() { Latitude = item.Latitude, Longitude = item.Longitude };
            //    if (l.DistanceTo(l2) < temp.DistanceTo(l))
            //    {
            //        temp = l2;
            //        baseStationToReturn = item;
            //    }
            //}
            //return baseStationToReturn;
        }

        public void RemoveDrone(string number)
        {
            try
            {
              var drone=GetDrone(number);
                if (drone.State == DroneState.shipping)
                    throw new DeletingException("can't delete drone that shipping");
              if (drone.State == DroneState.maintaince)
                    DroneFromCharging(number);
                dal.DeleteDrone(number);
                Drones.RemoveAll(x=>x.IdNumber==number);
            }
            catch(Exception e)
            {
                throw new DeletingException(e.Message, e);
            }
        }

    }
}