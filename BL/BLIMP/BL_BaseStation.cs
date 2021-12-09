using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BO;
namespace BL
{
    /// <summary>
    /// all possible actions on base stations 
    /// </summary>
    internal partial class BL : BLApi.IBL
    {
        #region AddBaseStation
        /// <summary>
        /// adding a new base station
        /// </summary>
        public void AddBaseStation(BO.BaseStation baseStationToAdd)
        {          
            if (baseStationToAdd.Name == "")
                throw new AddingProblemException("invalid Name of base station: you didn't enter a name");
            try
            {
                if (int.Parse(baseStationToAdd.IdNumber) == 0)
                    throw new AddingProblemException($"This {baseStationToAdd.IdNumber} is invalid Id of base station");
            }
            catch (Exception e)
            {throw new AddingProblemException($"This {baseStationToAdd.IdNumber} is invalid Id of base station");}
            if (baseStationToAdd.Location.Latitude > 33.3 || baseStationToAdd.Location.Latitude < 29.5)
                throw new AddingProblemException($"the Latitude {baseStationToAdd.Location.Latitude} is out of israel");
            if (baseStationToAdd.Location.Longitude > 35.6 || baseStationToAdd.Location.Longitude < 34.5)
                throw new AddingProblemException($"the Longitude {baseStationToAdd.Location.Longitude} is out of israel");
            if (baseStationToAdd.ChargeSlots < 0)
                throw new AddingProblemException($"{baseStationToAdd.ChargeSlots} is illegal number of charge slots");
            try
            {
                DO.BaseStation station = (DO.BaseStation)baseStationToAdd.CopyPropertiesToNew(typeof(DO.BaseStation));
                station.Latitude = baseStationToAdd.Location.Latitude;//add the location
                station.Longitude = baseStationToAdd.Location.Longitude;//add the location
                dal.AddBaseStation(station);
            }
            catch (Exception ex)
            {throw new AddingProblemException($"Can't add base station with the id {baseStationToAdd.IdNumber}", ex);}
        }
        #endregion
        #region GetBaseStations
        /// <summary>
        /// return all base stations 
        /// </summary>
        public IEnumerable<BO.BaseStationToList> GetBaseStations()
        {
            var list = from item in dal.GetBaseStations() select GetBaseStationOfList(item.IdNumber);
            return list;
        }
        #endregion   
        #region UpdatingDetailsOfBaseStation
        /// <summary>
        /// updating a single base station
        /// </summary>
        public void UpdatingDetailsOfBaseStation(string id, string Name, string numberOfCharge)
        {
            try
            {
                DO.BaseStation tempBaseStation = dal.GetBaseStation(id);
                BO.BaseStation b = (BO.BaseStation)tempBaseStation.CopyPropertiesToNew(typeof(BO.BaseStation));
                b.Location = new Location() { Latitude = tempBaseStation.Latitude, Longitude = tempBaseStation.Longitude };//add the location
                if (Name != "") b.Name = Name;
                try//try to parse the given amount
                {
                    if (numberOfCharge != "" && (int.Parse(numberOfCharge) == 0 && numberOfCharge != "0"))
                    {
                        if (b.Drones.Count() < int.Parse(numberOfCharge))
                            throw new UpdatingException($"{numberOfCharge} is an illegal number for charging drones");
                        b.ChargeSlots = int.Parse(numberOfCharge) - b.Drones.Count();
                    }
                }
                catch (Exception e)
                {throw new UpdatingException($"{numberOfCharge} is an illegal number for charging drones"); }
                try
                {
                    DO.BaseStation station = (DO.BaseStation)b.CopyPropertiesToNew(typeof(DO.BaseStation));
                    station.Latitude = b.Location.Latitude;
                    station.Longitude = b.Location.Longitude;
                    dal.UpdateBaseStation(station);// update the base station in the data layer
                }
                catch (Exception e)
                {throw new UpdatingException($"can't update the base station with id {id}", e);}
            }
            catch (Exception e)
            {throw new UpdatingException($"the base Station with the id {id} is not existing", e);}
        }
        #endregion
        #region GetBaseStationOfList
        /// <summary>
        /// private function: viewing a single base station
        /// </summary>
        private BO.BaseStationToList GetBaseStationOfList(string id)
        {
            BO.BaseStationToList b = (BO.BaseStationToList)dal.GetBaseStation(id).CopyPropertiesToNew(typeof(BO.BaseStationToList));
            var tempBaseStation = from item in dal.GetDroneCharges() where (item.StationId == id) select item;
            b.FullChargeSlots = tempBaseStation.Count();
            return b;
        }
        #endregion
        #region GetBaseStation
        /// <summary>
        /// return a single base station
        /// </summary>
        public BO.BaseStation GetBaseStation(string id)
        {
            try
            {
                DO.BaseStation station = dal.GetBaseStation(id);
                BO.BaseStation GetBaseStation = (BO.BaseStation)station.CopyPropertiesToNew(typeof(BO.BaseStation));
                GetBaseStation.Location = new Location() { Latitude = station.Latitude, Longitude = station.Longitude };
                var list = dal.PredicateChargeDrone(x => x.StationId == id);
                GetBaseStation.Drones = new List<DroneInCharge>();

                foreach (var item in list)//build the list of charging drones
                    GetBaseStation.Drones.Add(GetDroneInCharge(item.DroneId)); ///
                return GetBaseStation;
            }
            catch (Exception e)
            {throw new GettingProblemException($"the base station with the id {id} is not exist", e);}
        }
        #endregion
        #region GetDroneInCharge
        /// <summary>
        /// private function. return a single charging drone
        /// </summary>
        private BO.DroneInCharge GetDroneInCharge(string id)
        {
            return (BO.DroneInCharge)Drones.FirstOrDefault(x => x.IdNumber == id).CopyPropertiesToNew(typeof(BO.DroneInCharge));//take from the Bl list because need battery
        }
        #endregion
        #region PredicateBaseStation
        /// <summary>
        /// base station predicate
        /// </summary>
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

