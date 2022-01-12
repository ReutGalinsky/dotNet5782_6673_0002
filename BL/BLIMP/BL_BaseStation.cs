using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using BO;
namespace BL
{
    internal partial class BL : BLApi.IBL
    {
        #region RemoveBaseStation
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void RemoveBaseStation(string number)
        {
            try
            {
                lock (dal)
                {
                    var baseStation = GetBaseStation(number);
                    if (baseStation.Drones.Count() != 0)
                        throw new DeletingException("can't delete base station with charged drones");
                    dal.DeleteBaseStation(number);
                }
            }
            catch (Exception e)
            {
                throw new DeletingException(e.Message, e);
            }
        }
        #endregion

        #region AddBaseStation
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddBaseStation(BO.BaseStation baseStationToAdd)
        {
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

            try
            {
                lock (dal)
                {
                    DO.BaseStation station = (DO.BaseStation)baseStationToAdd.CopyPropertiesToNew(typeof(DO.BaseStation));
                    station.Latitude = baseStationToAdd.Location.Latitude;
                    station.Longitude = baseStationToAdd.Location.Longitude;
                    dal.AddBaseStation(station);
                }
            }
            catch (Exception ex)
            { throw new AddingProblemException(ex.Message, ex); }
        }
        #endregion

        #region GetBaseStations
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BO.BaseStationToList> GetBaseStations()
        {
            try
            {
                lock (dal)
                {
                    var list = from item in dal.GetBaseStations() select GetBaseStationOfList(item.IdNumber);
                    return list;
                }
            }
            catch(Exception ex)
            {
                throw new GettingProblemException(ex.Message,ex);
            }
        }
        #endregion   

        #region UpdatingDetailsOfBaseStation
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdatingDetailsOfBaseStation(string id, string Name, string numberOfCharge)
        {
            DO.BaseStation tempBaseStation;
            try
            {
                lock (dal)
                {
                    tempBaseStation = dal.GetBaseStation(id);
                }
            }
            catch (Exception e)
            {
                throw new UpdatingException(e.Message, e);
            }
            BO.BaseStation baseStation = (BO.BaseStation)tempBaseStation.CopyPropertiesToNew(typeof(BO.BaseStation));
            baseStation.Location = tempBaseStation.GetLocation();

            if (Name != "") baseStation.Name = Name;
            int tempInteger = 0;
            if (numberOfCharge != "" && (int.TryParse(numberOfCharge, out tempInteger) == false)||tempInteger<0)
                throw new UpdatingException($"{numberOfCharge} is an illegal number for charging drones");
               if(baseStation.Drones!=null && baseStation.Drones.Count()>tempInteger)
                throw new UpdatingException($"{numberOfCharge} is an illegal number for this base station");
            baseStation.ChargeSlots = tempInteger;

            try
            {
                lock (dal)
                {
                    DO.BaseStation station = (DO.BaseStation)baseStation.CopyPropertiesToNew(typeof(DO.BaseStation));
                    station.Latitude = baseStation.Location.Latitude;
                    station.Longitude = baseStation.Location.Longitude;
                    dal.UpdateBaseStation(station);
                }
            }
            catch (Exception e)
            { throw new UpdatingException(e.Message, e); }
        }


        #endregion

        #region GetBaseStation
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.BaseStation GetBaseStation(string id)
        {
            try
            {
                lock (dal)
                {
                    DO.BaseStation station = dal.GetBaseStation(id);
                    BO.BaseStation getBaseStation = (BO.BaseStation)station.CopyPropertiesToNew(typeof(BO.BaseStation));
                    getBaseStation.Location = station.GetLocation();
                    getBaseStation.Drones = (from item in dal.GetAllChargeDronesBy(x => x.StationId == id)
                                             select (GetDroneInCharge(item.DroneId))).ToList();
                    return getBaseStation;
                }
            }
            catch (Exception e)
            { throw new GettingProblemException(e.Message, e); }
        }
        #endregion


        #region GetAllBaseStationsBy
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BO.BaseStationToList> GetAllBaseStationsBy(Predicate<BO.BaseStationToList> condition)
        {
            try
            {
                var list = from item in GetBaseStations()
                           where condition(item)
                           select item;
                return list;
            }
            catch(Exception e)
            {
                throw new GettingProblemException(e.Message, e);
            }
        }
        #endregion
        #region GetDroneInCharge

        /// <summary>
        /// returning drone as 'drone in charge'
        /// </summary>
        /// <param name="droneId"> the drone id</param>
        /// <returns></returns>

        private BO.DroneInCharge GetDroneInCharge(string droneId)
        {
            var drone=(BO.DroneInCharge)Drones.FirstOrDefault(x => x.IdNumber == droneId).CopyPropertiesToNew(typeof(BO.DroneInCharge));
            if (drone == null)
                throw new GettingProblemException($"the drone with the id: {droneId} is not existing");
            return drone;
        }
        #endregion
        #region GetBaseStationOfList
        /// <summary>
        /// returning base station as 'base station to list'
        /// </summary>
        /// <param name="id">the id of the base station</param>
        /// <returns></returns>
        private BO.BaseStationToList GetBaseStationOfList(string id)
        {
            try
            {
                lock (dal)
                {
                    BO.BaseStationToList b = (BO.BaseStationToList)dal.GetBaseStation(id).CopyPropertiesToNew(typeof(BO.BaseStationToList));
                    b.FullChargeSlots = (from item in dal.GetDroneCharges() where (item.StationId == id) select item).Count();
                    return b;
                }
            }
            catch(Exception e)
            {
                throw new GettingProblemException(e.Message, e);
            }
        }
        #endregion

    }
}


