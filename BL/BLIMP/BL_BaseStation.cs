using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using DLAPI;
using DO;

using BLAPI;

namespace BL
{
    public partial class BL: BLAPI.IBL
    {
        #region AddBaseStation
        public void AddBaseStation(BO.BaseStation baseStationToAdd) 
            //add new base station to the data base
        {
            if (baseStationToAdd.Name == "")
                throw new AddingProblemException("invalid name of base station");
            try
            {
                if (int.Parse(baseStationToAdd.IdNumber) == 0)
                    throw new AddingProblemException("invalid Id of base station");
            }
            catch(Exception e)
            {
                throw new AddingProblemException("invalid Id of base station");
            }
            if (baseStationToAdd.Location.Latitude > 33.3 || baseStationToAdd.Location.Latitude < 29.5)
                throw new AddingProblemException("the Latitude is out of israel");
            if (baseStationToAdd.Location.Longitude > 35.6 || baseStationToAdd.Location.Longitude < 34.5)
                throw new AddingProblemException("the Longitude is out of israel");
            if (baseStationToAdd.ChargeSlots< 0)
                throw new AddingProblemException("there illegal amount of availible charge slots");
            try
            {
                DO.BaseStation station = (DO.BaseStation)baseStationToAdd.CopyPropertiesToNew(typeof(DO.BaseStation));
                station.Latitude = baseStationToAdd.Location.Latitude;//add the location
                station.Longitude = baseStationToAdd.Location.Longitude;//add the location
                dal.AddBaseStation(station);
            }
            catch (Exception ex)
            {
                throw new AddingProblemException("Can't add this base station", ex);
            }
        }
        #endregion

        #region GetBaseStations
        //return all the base stations
        public IEnumerable<BO.BaseStationToList> GetBaseStations()
        {
            var list = from item in dal.GetBaseStations() select GetBaseStationOfList(item.IdNumber);
            return list;
        }
        #endregion

        #region UpdatingDetailsOfBaseStation
        //update single base station
        public void UpdatingDetailsOfBaseStation(string id, string Name, string numberOfCharge)
            
        {
            try
            {
                DO.BaseStation temp = dal.GetBaseStation(id);
                BO.BaseStation b = (BO.BaseStation)temp.CopyPropertiesToNew(typeof(BO.BaseStation));
                b.Location = new Location() {Latitude=temp.Latitude,Longitude=temp.Longitude };//add the location
                if (Name != "") b.Name = Name;
                try//try to parse the given amount
                {
                    if (numberOfCharge != "" && (int.Parse(numberOfCharge) == 0 && numberOfCharge != "0"))
                    {
                        if (b.Drones.Count() < int.Parse(numberOfCharge))
                            throw new UpdatingException("the new amount of charge slots it's not fit to charging drones");
                        b.ChargeSlots = int.Parse(numberOfCharge) - b.Drones.Count();
                    }
                }
                catch(Exception e)
                { throw new UpdatingException("the new amount of charge slots it's not fit to charging drones"); }
                try
                {
                    DO.BaseStation station = (DO.BaseStation)b.CopyPropertiesToNew(typeof(DO.BaseStation));
                    station.Latitude = b.Location.Latitude;
                    station.Longitude = b.Location.Longitude;
                    dal.UpdateBaseStation(station);// update the base station in the data layer
                }
                catch (Exception e)
                {
                    throw new UpdatingException("can't update the base station", e);
                }
            }
            catch (Exception e)
            {
                throw new UpdatingException("the base Station is not existing",e);
            }
        }
        #endregion

        #region GetBaseStationOfList
        //private function that return base station as object of 'baseStationOfList' for view
        private BO.BaseStationToList GetBaseStationOfList(string id)
        {
            BO.BaseStationToList b = (BO.BaseStationToList)dal.GetBaseStation(id).CopyPropertiesToNew(typeof(BO.BaseStationToList));
            var temp = from item in dal.GetDroneCharges() where (item.StationId == id) select item;
            b.FullChargeSlots = temp.Count();
            return b;
        }
        #endregion

        #region GetBaseStation
        //retrun single item of base station
        public BO.BaseStation GetBaseStation(string id)
        {
            try
            {
                DO.BaseStation station = dal.GetBaseStation(id);
                BO.BaseStation GetStation = (BO.BaseStation)station.CopyPropertiesToNew(typeof(BO.BaseStation));
                GetStation.Location = new Location() { Latitude = station.Latitude, Longitude = station.Longitude };
                var list = dal.PredicateChargeDrone(x => x.StationId == id);
                GetStation.Drones = new List<DroneInCharge>();
                foreach (var item in list)//build the list of charging drones
                {
                    GetStation.Drones.Add(GetDroneInCharge(item.DroneId));
                }
                return GetStation;
            }
            catch(Exception e)
            {
                throw new GettingProblemException("the base station is not exist",e);
            }
        }
        #endregion

        #region GetDroneInCharge
        //private function that return object of DroneInCharge
        private BO.DroneInCharge GetDroneInCharge(string id)
        {
            return (BO.DroneInCharge)Drones.FirstOrDefault(x=>x.IdNumber==id).CopyPropertiesToNew(typeof(BO.DroneInCharge));//take from the Bl list because need battery
        }
        #endregion

        #region PredicateBaseStation
        public IEnumerable<BO.BaseStationToList> PredicateBaseStation(Predicate<BO.BaseStationToList> c)
        {
            var list = from item in GetBaseStations()
                       where c(item)
                       select item;
            return list;
        }
        #endregion
    }
}
