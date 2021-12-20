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
        public void RemoveBaseStation(string number)
        {
            try
            {
                var baseStation = GetBaseStation(number);
                if (baseStation.Drones.Count()!=0)
                    throw new DeletingException("can't delete base station with charged drones");
                dal.DeleteBaseStation(number);
            }
            catch (Exception e)
            {
                throw new DeletingException(e.Message, e);
            }
        }
        #region AddBaseStation
        /// <summary>
        /// adding a new base station
        /// </summary>
        public void AddBaseStation(BO.BaseStation baseStationToAdd)
        {
            //validation
            if (baseStationToAdd.Name == "")
                throw new AddingProblemException("invalid Name of base station: you didn't enter a name");
            int tempInteger;
            if (int.TryParse(baseStationToAdd.IdNumber, out tempInteger) == false || tempInteger == 0)
                throw new AddingProblemException($"This {baseStationToAdd.IdNumber} is invalid Id of base station");
            if (baseStationToAdd.Location.Latitude > 33.3 || baseStationToAdd.Location.Latitude < 29.5)
                throw new AddingProblemException($"the Latitude {baseStationToAdd.Location.Latitude} is out of israel");
            if (baseStationToAdd.Location.Longitude > 35.6 || baseStationToAdd.Location.Longitude < 34.5)
                throw new AddingProblemException($"the Longitude {baseStationToAdd.Location.Longitude} is out of israel");
            if (baseStationToAdd.ChargeSlots < 0)
                throw new AddingProblemException($"{baseStationToAdd.ChargeSlots} is illegal number of charge slots");

            //add
            try
            {
                DO.BaseStation station = (DO.BaseStation)baseStationToAdd.CopyPropertiesToNew(typeof(DO.BaseStation));
                station.Latitude = baseStationToAdd.Location.Latitude;//add the location
                station.Longitude = baseStationToAdd.Location.Longitude;//add the location
                dal.AddBaseStation(station);
            }
            catch (Exception ex)
            { throw new AddingProblemException($"Can't add base station with the id {baseStationToAdd.IdNumber}", ex); }
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
            DO.BaseStation tempBaseStation;
            try
            {//initialized
                tempBaseStation = dal.GetBaseStation(id);
            }
            catch (Exception e)
            {
                throw new UpdatingException(e.Message, e);
            }
            BO.BaseStation baseStation = (BO.BaseStation)tempBaseStation.CopyPropertiesToNew(typeof(BO.BaseStation));
            baseStation.Location = tempBaseStation.GetLocation();

            //validation
            if (Name != "") baseStation.Name = Name;
            int tempInteger = 0;

            if (numberOfCharge != "" && (int.TryParse(numberOfCharge, out tempInteger) == false))
                throw new UpdatingException($"{numberOfCharge} is an illegal number for charging drones");

            if (numberOfCharge == "") baseStation.ChargeSlots = tempBaseStation.ChargeSlots;
            else
            {
                if (baseStation.Drones.Count() < tempInteger)
                    throw new UpdatingException($"{numberOfCharge} is an illegal number for charging drones");
                baseStation.ChargeSlots = tempInteger - baseStation.Drones.Count();
            }

            //update
            try
            {
                DO.BaseStation station = (DO.BaseStation)baseStation.CopyPropertiesToNew(typeof(DO.BaseStation));
                station.Latitude = baseStation.Location.Latitude;
                station.Longitude = baseStation.Location.Longitude;
                dal.UpdateBaseStation(station);
            }
            catch (Exception e)
            { throw new UpdatingException($"can't update the base station with id {id}", e); }
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
                BO.BaseStation getBaseStation = (BO.BaseStation)station.CopyPropertiesToNew(typeof(BO.BaseStation));
                //GetBaseStation.Location = new Location() { Latitude = station.Latitude, Longitude = station.Longitude };
                getBaseStation.Location = station.GetLocation();
                //var list = dal.GetAllChargeDronesBy(x => x.StationId == id);
                //getBaseStation.Drones = new List<DroneInCharge>();
                //foreach (var item in list)
                //    getBaseStation.Drones.Add(GetDroneInCharge(item.DroneId));
                getBaseStation.Drones = (from item in dal.GetAllChargeDronesBy(x => x.StationId == id)
                                         select (GetDroneInCharge(item.DroneId))).ToList();
                return getBaseStation;
            }
            catch (Exception e)
            { throw new GettingProblemException($"the base station with the id {id} is not exist", e); }
        }
        #endregion

        #region GetAllBaseStationsBy
        /// <summary>
        /// base station predicate
        /// </summary>
        public IEnumerable<BO.BaseStationToList> GetAllBaseStationsBy(Predicate<BO.BaseStationToList> condition)
        {
            var list = from item in GetBaseStations()
                       where condition(item)
                       select item;
            return list;
        }
        #endregion

        //********return inner objects***********
        #region GetDroneInCharge
        /// <summary>
        /// private function. return a single charging drone
        /// </summary>
        private BO.DroneInCharge GetDroneInCharge(string droneId)
        {
            return (BO.DroneInCharge)Drones.FirstOrDefault(x => x.IdNumber == droneId).CopyPropertiesToNew(typeof(BO.DroneInCharge));
        }
        #endregion

        #region GetBaseStationOfList
        /// <summary>
        /// private function: viewing a single base station
        /// </summary>
        private BO.BaseStationToList GetBaseStationOfList(string id)
        {
            BO.BaseStationToList b = (BO.BaseStationToList)dal.GetBaseStation(id).CopyPropertiesToNew(typeof(BO.BaseStationToList));
            b.FullChargeSlots = (from item in dal.GetDroneCharges() where (item.StationId == id) select item).Count();
            return b;
        }
        #endregion

    }
}


