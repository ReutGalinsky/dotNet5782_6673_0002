using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL;
using IDAL.DO;
using IDAL;
using IBL.BO;


namespace BL
{
   public partial class BL: IBL.IBL
    {
        public IDAL.IDal dal;
        public List<IBL.BO.DroneToList> Drones=new List<DroneToList>();
        public double availible;
        public double heavy;
        public double light;
        public double medium;
        public double speed;

        #region BL_Constructor
        public BL()
        {
            dal = new DalObject.DalObject();
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
                    Drones.Add(GetDroneToList(item.IdNumber));
                }
                catch(Exception e)
                {
                    throw new UpdatingException("cant add",e);//החריגה לא עובדת
                }
            }
        }

        private IBL.BO.DroneToList GetDroneToList(string id)
        {
            Random rand = new Random();
            IBL.BO.DroneToList drone = (IBL.BO.DroneToList)dal.GetDrone(id).CopyPropertiesToNew(typeof(IBL.BO.DroneToList));
            var parcel = from item in dal.GetParcels()
                         where item.DroneId == id
                         select item;
            foreach (var item in parcel)
            {
                if (item.collectingDroneTime == default(DateTime) || item.ArrivingDroneTime == default(DateTime))
                {
                    drone.State = DroneState.shipping;
                    double distance1 = 0, distance2;
                    Location a = new Location() { Latitude = dal.GetCustomer((item.Sender)).Latitude, Longitude = dal.GetCustomer((item.Sender)).Longitude };
                    if (item.collectingDroneTime == default(DateTime))
                    {
                        Location b = new Location() { Latitude = dal.GetBaseStations().First().Latitude, Longitude = dal.GetBaseStations().First().Longitude };
                        foreach (var obj in dal.GetBaseStations())
                        {
                            Location temp = new Location() { Latitude = obj.Latitude, Longitude = obj.Longitude };
                            if (DistanceTo(temp, a) < DistanceTo(b, a))
                                b = temp;
                        }
                        drone.Current = b;
                        Location c = new Location() { Latitude = dal.GetCustomer((item.Geter)).Latitude, Longitude = dal.GetCustomer((item.Geter)).Longitude };
                        Location d = new Location() { Latitude = ClosestStation(c).Latitude, Longitude = ClosestStation(c).Longitude };
                        switch (item.Weight)
                        {
                            case IDAL.DO.WeightCategories.Heavy:
                                distance1 = DistanceTo(b, c) * heavy;
                                break;
                            case IDAL.DO.WeightCategories.Middle:
                                distance1 = DistanceTo(b, c) * medium;
                                break;
                            case IDAL.DO.WeightCategories.Light:
                                distance1 = DistanceTo(b, c) * light;
                                break;
                            default:
                                break;
                        }
                        distance2 = DistanceTo(c, d) * availible;
                        if ((int)(distance1 + distance2) > 100) throw new AddingProblemException("can't pass the parcel without charging in the middle of the shipping");
                        drone.Battery = rand.Next((int)(distance1 + distance2), 101);//מה לעשות עם הבעיה של הבטריה.
                    }
                    else
                    {
                        drone.Current = a;
                        Location d = new Location() { Latitude = ClosestStation(a).Latitude, Longitude = ClosestStation(a).Longitude };
                        distance2 = DistanceTo(a, d) * availible;
                    }
                    return drone;
                }
            }
            int number = rand.Next(0, 2);
            switch (number)
            {
                case 0:
                    drone.Battery = rand.Next(0, 20);
                    var station = dal.GetBaseStations().ToList();
                    if (station.Count == 0) throw new AddingProblemException("there are no base stations");//???????????
                    int num = rand.Next(0, station.Count());
                    IDAL.DO.BaseStation b = station.ElementAt(num);
                    drone.Current = new Location() { Latitude = b.Latitude, Longitude = b.Longitude };
                    drone.State = DroneState.maintaince;
                    break;
                case 1:
                    var list = from item in dal.GetParcels()
                               where item.ArrivingDroneTime != default(DateTime)
                               select item;
                    var list1 = list.ToList();
                    if (list1.Count == 0) throw new AddingProblemException("there are no supplied parcels");//????????????
                    //מה קורה אם אין חבילות שסופקו?
                    num = rand.Next(0, list.Count());
                    IDAL.DO.Parcel P = list1.ElementAt(num);
                    drone.Current = new Location();
                    drone.Current.Latitude = dal.GetCustomer(((P.Geter))).Latitude;
                    drone.Current.Longitude = dal.GetCustomer((((P.Geter)))).Longitude;
                    Location d = new Location() { Latitude = ClosestStation(drone.Current).Latitude, Longitude = ClosestStation(drone.Current).Longitude };
                    drone.Battery = rand.Next((int)(DistanceTo(drone.Current, d) * availible), 101);
                    break;
                default:
                    break;
            }
            return drone;

        }
        private IDAL.DO.BaseStation ClosestStation(Location l)
        {
            var list = dal.GetBaseStations();
            IDAL.DO.BaseStation baseStationToReturn;
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
        public void AddDrone(IBL.BO.DroneToList droneToAdd, string number)
        {
            if (droneToAdd.MaxWeight != IBL.BO.WeightCategories.Heavy && droneToAdd.MaxWeight != IBL.BO.WeightCategories.Middle && droneToAdd.MaxWeight != IBL.BO.WeightCategories.Light)
                throw new AddingProblemException("This weight is not an option");
            if (droneToAdd.Model == "")
                throw new AddingProblemException("A model wasn't entered");
            try
            {
                dal.AddDrone((IDAL.DO.Drone)droneToAdd.CopyPropertiesToNew(typeof(IDAL.DO.Drone)));
                Random r = new Random();
                droneToAdd.Battery = r.Next(20, 40);
                Location l = (Location)GetBaseStation(number).Local.CopyPropertiesToNew(typeof(Location));
                droneToAdd.Current = l;
                DroneToCharging(droneToAdd.IdNumber);
                Drones.Add(droneToAdd);
            }
            catch (Exception ex)
            {
                throw new AddingProblemException("Can't add this drone", ex);
            }
        }
        #endregion

        #region GetDrones
        public IEnumerable<IBL.BO.DroneToList> GetDrones()
        {
            return Drones;
        }
        #endregion

        #region GetDrone
        public IBL.BO.Drone GetDrone(string id)
        {
            IBL.BO.DroneToList d = Drones.Find(x => x.IdNumber == id);
            if (d == null)
                throw new GettingProblemException("the drone is not exist");
            
            IBL.BO.Drone drone = (IBL.BO.Drone)d.CopyPropertiesToNew(typeof(IBL.BO.Drone));
            drone.Current = new Location();
            drone.Current.Latitude = d.Current.Latitude;
            drone.Current.Longitude = d.Current.Longitude;
            drone.State = d.State;
            drone.Battery = d.Battery;
            drone.PassedParcel = GetPIP(d.NumberOfParcel);
            return drone;

        }
        #endregion
        private IBL.BO.ParcelInPassing GetPIP(string id)
        {
            IBL.BO.Parcel p = GetParcel(id);
            IBL.BO.ParcelInPassing temp= (IBL.BO.ParcelInPassing)p.CopyPropertiesToNew(typeof(IBL.BO.ParcelInPassing));
            string sender =p.SenderCustomer.IdNumer;
            string geter = p.GeterCustomer.IdNumer;
            temp.Sender = GetCustomerOfParcel(sender);
            temp.Getter = GetCustomerOfParcel(geter);
            temp.Packing = GetCustomer(sender).Local;
            temp.Destination = GetCustomer(geter).Local;
            return temp;
        }

        #region UpdatingDetailsOfDrone
        public void UpdatingDetailsOfDrone(string Model, string id)
        {
            if (Model == "")
                throw new UpdatingException("the model is illegal");
            IBL.BO.DroneToList d = Drones.Find(x => x.IdNumber == id);
            if (d == null)
                throw new UpdatingException("the drone is not existing");
            d.Model = Model; //לוודא ששינה בBL של רחפנים
            try
            {
                dal.UpdateDrone((IDAL.DO.Drone)d.CopyPropertiesToNew(typeof(IDAL.DO.Drone)));
            }
            catch (Exception e)
            {
                throw new UpdatingException("can't update the drone", e);
            }

        }
        #endregion



    }
}
