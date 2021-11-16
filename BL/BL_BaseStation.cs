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
    class BL_BaseStation
    {
        #region GetBaseStations
        public IEnumerable<IBL.BO.BaseStationToList> GetBseStations()
        {
            return BaseStation;
        }
        #endregion
        #region GetBaseStationsWithCharge
        public IBL.BO.BaseStationToList GetBaseStationsWithCharge()
        {
            IBL.BO.<BaseStationToList> b = BaseStation.Find(x => x.IdNumber == id);
            if (d == null)
            return d;
        }
        #endregion
        #region AddBaseStation
        public void AddBaseStation(IBL.BO.BaseStation baseStationToAdd)
        {

            if (baseStationToAdd.Name == "")
                throw new AddingProblemException("invalid name of base");
            if (baseStationToAdd.Local.Latitude > 35 || baseStationToAdd.Local.Latitude < 33)
                throw new AddingProblemException("the location is out of israel");
            if (baseStationToAdd.Local.Longitude > 33 || baseStationToAdd.Local.Longitude < 31)
                throw new AddingProblemException("the location is out of israel");
            try
            {

                IDAL.DO.BaseStation b = new IDAL.DO.BaseStation() { IdNumber = baseStationToAdd.Id, ChargeSlots=baseStationToAdd.ChargeSlots, Latitude = baseStationToAdd.Local.Latitude, Longitude = baseStationToAdd.Local.Longitude, Name = baseStationToAdd.Name };
                dal.AddBaseStation(b);
                b.list of drones = null;
            }
            catch (Exception ex)
            {
                throw new AddingProblemException("Can't add this base station", ex);
            }
        }
        #endregion

        #region UpdatingBaseStation
        public void UpdatingBaseStation(string id, string Name = "", int numberOfCharge = 0)
        {
            if (Name == "")
                throw new UpdatingException("the name is illegal");
            IBL.BO.BaseStation b = BaseStation.Find(x => x.id == id);
            if (b == null)
                throw new UpdatingException("the customer is not existing");
            if (Name != "") b.Name = Name;
            if (numberOfCharge != 0) b.ChargeSlots = numberOfCharge;
            IDAL.DO.BaseStation updatedBaseStation = new IDAL.DO.BaseStation() { IdNumber = b.Id, Phone = d.Phone, Latitude = d.Local.Latitude, Longitude = d.Local.Longitude, Name = d.Name };
            try
            {
                dal.UpdateBaseStation(updatedBaseStation);
            }
            catch (Exception e)
            {
                throw new UpdatingException("can't update the base station", e);
            }

        }
        #endregion
        #region GetBaseStations
        public IEnumerable<IBL.BO.BaseStationToList> GetBaseStations()
        {
            return BaseStations;
        }
        #endregion

    }
}
