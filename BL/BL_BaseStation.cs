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
        #region AddBaseStation
        public void AddBaseStation(IBL.BO.BaseStation baseStationToAdd) 
        {
            if (baseStationToAdd.Name == "")
                throw new AddingProblemException("invalid name of base station");
            if (baseStationToAdd.Local.Latitude > 33.3 || baseStationToAdd.Local.Latitude < 29.5)
                throw new AddingProblemException("the location is out of israel");
            if (baseStationToAdd.Local.Longitude > 35.6 || baseStationToAdd.Local.Longitude < 34.5)
                throw new AddingProblemException("the location is out of israel");
            if (baseStationToAdd.ChargeSlots< 0)
                throw new AddingProblemException("there illegal amount of availible charge slots");
            try
            {
                IDAL.DO.BaseStation station = (IDAL.DO.BaseStation)baseStationToAdd.CopyPropertiesToNew(typeof(IDAL.DO.BaseStation));
                station.Latitude = baseStationToAdd.Local.Latitude;
                station.Longitude = baseStationToAdd.Local.Longitude;
                dal.AddBaseStation(station);
            }
            catch (Exception ex)
            {
                throw new AddingProblemException("Can't add this base station", ex);
            }
        }
        #endregion

        #region GetBaseStations
        public IEnumerable<IBL.BO.BaseStationToList> GetBseStations()
        {
            var list = from item in dal.GetBaseStations() select (IBL.BO.BaseStationToList)item.CopyPropertiesToNew(typeof(IBL.BO.BaseStationToList));
            foreach(var item in list)
            {
                item.FullChargeSlots = dal.GetDroneCharges().Count(x => x.StationId == item.IdNumber);
            }
            return list;
        }
        #endregion

        #region GetBaseStationsWithCharge
        public IEnumerable<IBL.BO.BaseStationToList> GetBaseStationsWithCharge()
        {
            var b = GetBseStations().Where(x=>x.ChargeSlots>0);
            return b;
        }
        #endregion

        #region UpdatingDetailsOfBaseStation
        public void UpdatingDetailsOfBaseStation(string id, string Name = "", int numberOfCharge = 0)
            //מה עושים אם עדכנו למספר קטן יותר מכמות המוטענים?
        {
            try
            {
                IBL.BO.BaseStation b = (IBL.BO.BaseStation)dal.GetBaseStation(id).CopyPropertiesToNew(typeof(IBL.BO.BaseStation));
                if (Name != "") b.Name = Name;
                if (numberOfCharge != 0) 
                {
                    if (b.Drones.Count() < numberOfCharge)
                        throw new UpdatingException("the new amount of charge slots it's not fit to charging drones");
                    b.ChargeSlots = numberOfCharge- b.Drones.Count(); 
                }
                try
                {
                    IDAL.DO.BaseStation station = (IDAL.DO.BaseStation)b.CopyPropertiesToNew(typeof(IDAL.DO.BaseStation));
                    station.Latitude = b.Local.Latitude;
                    station.Longitude = b.Local.Longitude;
                    dal.UpdateBaseStation(station);
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

        #region GetBaseStation
        public IBL.BO.BaseStation GetBaseStation(string id)
        {
            try
            {
                IDAL.DO.BaseStation station = dal.GetBaseStation(id);
                IBL.BO.BaseStation GetStation = (IBL.BO.BaseStation)station.CopyPropertiesToNew(typeof(IBL.BO.BaseStation));
                GetStation.Local = new Location() { Latitude = station.Latitude, Longitude = station.Longitude };
                var list = dal.GetDroneCharges().Where(x => x.StationId == id);
                foreach(var item in list)//לחשוב סופית אם find יכול להחזיר null
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
        private IBL.BO.DroneInCharge GetDroneInCharge(string id)
        {
            return (IBL.BO.DroneInCharge)Drones.Find(x=>x.IdNumber==id).CopyPropertiesToNew(typeof(IBL.BO.DroneInCharge));
        }
        #endregion
    }
}
