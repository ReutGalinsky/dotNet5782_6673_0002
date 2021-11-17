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
    partial class BL
    {
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

        #region AddBaseStation
        public void AddBaseStation(IBL.BO.BaseStation baseStationToAdd)
        {
            if (baseStationToAdd.Name == "")
                throw new AddingProblemException("invalid name of base");
            if (baseStationToAdd.Local.Latitude > 35 || baseStationToAdd.Local.Latitude < 33)//לוודא שזה באמת הערכים
                throw new AddingProblemException("the location is out of israel");
            if (baseStationToAdd.Local.Longitude > 33 || baseStationToAdd.Local.Longitude < 31)
                throw new AddingProblemException("the location is out of israel");
            if (baseStationToAdd.ChargeSlots< 0)
                throw new AddingProblemException("there is no charge slots");
            try
            {
                IDAL.DO.BaseStation station = (IDAL.DO.BaseStation)baseStationToAdd.CopyPropertiesToNew(typeof(IDAL.DO.BaseStation));
                station.Latitude = baseStationToAdd.Local.Latitude;
                station.Longitude = baseStationToAdd.Local.Longitude;
                dal.AddBaseStation(station);
               // b.list of drones = null;//לבדוק
            }
            catch (Exception ex)
            {
                throw new AddingProblemException("Can't add this base station", ex);
            }
        }
        #endregion

        #region UpdatingBaseStation
        public void UpdatingBaseStation(int id, string Name = "", int numberOfCharge = 0)
        {
            IBL.BO.BaseStation b = (IBL.BO.BaseStation)dal.GetBaseStation(id).CopyPropertiesToNew(typeof(IBL.BO.BaseStation));
            if (b == null)
                throw new UpdatingException("the customer is not existing");
            if (Name != "") b.Name = Name;
            if (numberOfCharge != 0) b.ChargeSlots = numberOfCharge;
            try
            {
                dal.UpdateBaseStation((IDAL.DO.BaseStation)b.CopyPropertiesToNew(typeof(IDAL.DO.BaseStation));//לא באמת קוראים לה ככה
            }
            catch (Exception e)
            {
                throw new UpdatingException("can't update the base station", e);
            }

        }
        #endregion

        #region GetBaseStation
        public IBL.BO.BaseStation GetBaseStation(int id)
        {
            try
            {
                IDAL.DO.BaseStation station = dal.GetBaseStation(id);
                IBL.BO.BaseStation GetStation = (IBL.BO.BaseStation)station.CopyPropertiesToNew(typeof(IBL.BO.BaseStation));
                GetStation.Local.Latitude = station.Latitude;
                GetStation.Local.Longitude = station.Longitude;
                var list = dal.GetDroneCharges().Where(x => x.StationId == id);
                foreach(var item in list)//לחשוב סופית אם find יכול להחזיר null
                {
                    GetStation.Drones.Add(((IBL.BO.DroneInCharge)Drones.Find(x=>x.IdNumber==item.DroneId).CopyPropertiesToNew(typeof(IBL.BO.DroneInCharge))));
                }
                return GetStation;
            }
            catch(Exception e)
            {
                throw new GettingProblemException("the base station is not exist",e);
            }
        }
        #endregion

    }
}
