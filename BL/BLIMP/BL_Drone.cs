using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using BO;


namespace BL
{
    internal partial class BL : BLApi.IBL
    {
        private DalApi.IDal dal;
        private List<BO.DroneToList> Drones = new List<DroneToList>();
        private double availible;
        private double heavy;
        private double light;
        private double medium;
        private double speed;

        #region BL_Constructor
        public BL()
        {
            dal = DalApi.DLFactory.GetDal();
            double[] arr = dal.UsingElectricity();
            availible = arr[0];
            heavy = arr[1];
            light = arr[2];
            medium = arr[3];
            speed = arr[4];
            foreach (var item in dal.GetDrones())
            {
                try
                {
                    Drones.Add(GetDroneToList(item.IdNumber));//create the list of drones for the BL
                }
                catch (Exception e)
                {
                    throw new AddingProblemException("", e);
                }
            }
        }

        private BO.DroneToList GetDroneToList(string id)
        //function that return object of DroneToList for constructor
        {
            try
            {
                Random rand = new Random();
                BO.DroneToList drone = (BO.DroneToList)dal.GetDrone(id).CopyPropertiesToNew(typeof(BO.DroneToList));
                var parcel = from item in dal.GetParcels()
                             where item.DroneId == id
                             select item;
                foreach (var item in parcel)//search if the drone is in shipping mode
                {
                    if (item.collectingDroneTime == null || item.ArrivingDroneTime == null)
                    {
                        drone.State = DroneState.shipping;
                        double distance1 = 0, distance2;
                        Location a = new Location() { Latitude = dal.GetCustomer((item.Sender)).Latitude, Longitude = dal.GetCustomer((item.Sender)).Longitude };
                        if (item.collectingDroneTime == null)
                        {
                            Location b = new Location() { Latitude = dal.GetBaseStations().First().Latitude, Longitude = dal.GetBaseStations().First().Longitude };
                            foreach (var obj in dal.GetBaseStations())
                            {
                                Location temp = new Location() { Latitude = obj.Latitude, Longitude = obj.Longitude };
                                if (DistanceTo(temp, a) < DistanceTo(b, a))
                                    b = temp;
                            }
                            drone.Location = b;
                            Location c = new Location() { Latitude = dal.GetCustomer((item.Geter)).Latitude, Longitude = dal.GetCustomer((item.Geter)).Longitude };
                            Location d = new Location() { Latitude = ClosestStation(c).Latitude, Longitude = ClosestStation(c).Longitude };
                            switch (item.Weight)
                            {
                                case DO.WeightCategories.Heavy:
                                    distance1 = DistanceTo(b, c) * heavy;
                                    break;
                                case DO.WeightCategories.Middle:
                                    distance1 = DistanceTo(b, c) * medium;
                                    break;
                                case DO.WeightCategories.Light:
                                    distance1 = DistanceTo(b, c) * light;
                                    break;
                                default:
                                    break;
                            }
                            distance2 = DistanceTo(c, d) * availible;
                            if ((int)(distance1 + distance2) > 100) throw new AddingProblemException("can't pass the parcel without charging in the middle of the shipping");
                            drone.Battery = rand.Next((int)(distance1 + distance2), 101);
                        }
                        else
                        {
                            drone.Location = a;
                            Location d = new Location() { Latitude = ClosestStation(a).Latitude, Longitude = ClosestStation(a).Longitude };
                            distance2 = DistanceTo(a, d) * availible;
                        }
                        drone.NumberOfParcel = item.IdNumber;
                        return drone;
                    }
                }
                int number = rand.Next(0, 2);//it's not shipping
                switch (number)
                {
                    case 0://the drone will be in maintaince mode

                        drone.Battery = rand.Next(0, 20);
                        var station = dal.GetBaseStations().ToList();
                        if (station.Count == 0) throw new AddingProblemException("there are no base stations");
                        int num = rand.Next(0, station.Count());
                        DO.BaseStation b = station.ElementAt(num);
                        drone.Location = new Location() { Latitude = b.Latitude, Longitude = b.Longitude };
                        drone.State = DroneState.maintaince;
                        b.ChargeSlots--;
                        dal.UpdateBaseStation(b);
                        DO.DroneCharge charge = new DroneCharge() { DroneId = drone.IdNumber, StationId = b.IdNumber };
                        dal.AddDroneCharge(charge);
                        break;
                    case 1:// the drone is availible
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
                            drone.Location = new Location();
                            drone.Location.Latitude = dal.GetCustomer(((P.Geter))).Latitude;
                            drone.Location.Longitude = dal.GetCustomer((((P.Geter)))).Longitude;
                            Location d = new Location() { Latitude = ClosestStation(drone.Location).Latitude, Longitude = ClosestStation(drone.Location).Longitude };
                            drone.Battery = rand.Next((int)(DistanceTo(drone.Location, d) * availible), 101);
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
                throw new AddingProblemException("the drone number " + id + " is not correct, can't running this program");
            }

        }
        private DO.BaseStation ClosestStation(Location l)
        //private function that return the closest base station to a given location
        {
            var list = dal.GetBaseStations();
            DO.BaseStation baseStationToReturn;
            Location temp = new Location() { Latitude = list.First().Latitude, Longitude = list.First().Longitude };
            baseStationToReturn = list.First();
            foreach (var item in list)
            {
                Location l2 = new Location() { Latitude = item.Latitude, Longitude = item.Longitude };
                if (DistanceTo(l, l2) < DistanceTo(temp, l))
                {
                    temp = l2;
                    baseStationToReturn = item;
                }
            }
            return baseStationToReturn;
        }

        #endregion

        #region AddDrone
        public void AddDrone(BO.DroneToList droneToAdd, string number)
        //function that add drone
        {
            if (droneToAdd.MaxWeight != BO.WeightCategories.Heavy && droneToAdd.MaxWeight != BO.WeightCategories.Middle && droneToAdd.MaxWeight != BO.WeightCategories.Light)
                throw new AddingProblemException("This weight is not an option");
            if (droneToAdd.Model == "")
                throw new AddingProblemException("A model wasn't entered");
            try
            {
                if (int.Parse(droneToAdd.IdNumber) == 0)
                    throw new AddingProblemException("invalid Id of drone");
            }
            catch (Exception e)
            {
                throw new AddingProblemException("invalid Id of base station");
            }
            try //assumption: drone need to get charging when it's being added
            {
                dal.AddDrone((DO.Drone)droneToAdd.CopyPropertiesToNew(typeof(DO.Drone)));//add to DAL
                Random r = new Random();
                droneToAdd.Battery = r.Next(20, 40);
                Location l = (Location)GetBaseStation(number).Location.CopyPropertiesToNew(typeof(Location));
                droneToAdd.Location = l;
                var ListOfStation = dal.GetBaseStation(number);
                if (ListOfStation.ChargeSlots == 0)
                    throw new ChargingException("there is not any slot for charging in this base station");
                droneToAdd.State = DroneState.maintaince;
                ListOfStation.ChargeSlots--;
                dal.UpdateBaseStation(ListOfStation);
                Drones.Add(droneToAdd);
                DO.DroneCharge charge = new DroneCharge() { DroneId = droneToAdd.IdNumber, StationId = number };
                dal.AddDroneCharge(charge);
            }
            catch (Exception ex)
            {
                throw new AddingProblemException("Can't add this drone", ex);
            }
        }
        #endregion

        #region GetDrones
        //return all the drones
        public IEnumerable<BO.DroneToList> GetDrones()
        {
            return Drones;
        }
        #endregion

        #region GetDrone
        //retrun single drone
        public BO.Drone GetDrone(string id)
        {
            BO.DroneToList d = Drones.FirstOrDefault(x => x.IdNumber == id);
            if (d == null)
                throw new GettingProblemException("the drone is not exist");
            BO.Drone drone = (BO.Drone)d.CopyPropertiesToNew(typeof(BO.Drone));
            drone.Location = new Location();
            drone.Location.Latitude = d.Location.Latitude;
            drone.Location.Longitude = d.Location.Longitude;
            drone.State = d.State;
            drone.Battery = d.Battery;
            if (d.NumberOfParcel != null)
                drone.PassedParcel = GetPIP(d.NumberOfParcel);
            return drone;

        }
        #endregion
        private BO.ParcelInPassing GetPIP(string id)
        //private function that return object of ParcelInPassing
        {
            DO.Parcel p = dal.GetParcel(id);
            BO.ParcelInPassing temp = (BO.ParcelInPassing)p.CopyPropertiesToNew(typeof(BO.ParcelInPassing));
            Location get = new Location() { Latitude = dal.GetCustomer(p.Geter).Latitude, Longitude = dal.GetCustomer(p.Geter).Longitude };
            Location send = new Location() { Latitude = dal.GetCustomer(p.Sender).Latitude, Longitude = dal.GetCustomer(p.Sender).Longitude };
            temp.Distance = DistanceTo(get, send);
            temp.Packing = send;
            temp.Destination = get;
            temp.Senderer = GetCustomerOfParcel(p.Sender);
            temp.Getterer = GetCustomerOfParcel(p.Geter);
            if (p.collectingDroneTime == null)//the boolian value
                temp.isWaitingForColecting = true;
            else
                temp.isWaitingForColecting = false;
            return temp;
        }

        #region UpdatingDetailsOfDrone
        public void UpdatingDetailsOfDrone(string Model, string id)
        //update drone
        {
            if (Model == "")
                throw new UpdatingException("the model is illegal");
            BO.DroneToList d = Drones.FirstOrDefault(x => x.IdNumber == id);
            if (d == null)
                throw new UpdatingException("the drone is not existing");
            d.Model = Model;
            try
            {
                dal.UpdateDrone((DO.Drone)d.CopyPropertiesToNew(typeof(DO.Drone)));//update in DAL
            }
            catch (Exception e)
            {
                throw new UpdatingException("can't update the drone", e);
            }

        }
        #endregion

        #region PredicateDrone
        public IEnumerable<BO.DroneToList> PredicateDrone(Predicate<BO.DroneToList> c)
        {
            var list = from item in GetDrones()
                       where c(item)
                       select item;
            return list;
        }
        #endregion 
    }
}