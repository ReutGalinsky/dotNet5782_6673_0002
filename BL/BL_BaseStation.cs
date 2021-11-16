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
            return BaseStation;//להוסיף על ידי פונקציה גנרית
            //לטפל בחריגות
        }
        #endregion
        #region GetBaseStationsWithCharge
        public IBL.BO.BaseStationToList GetBaseStationsWithCharge()
        {
            IBL.BO.<BaseStationToList> b = GetBaseStations();
            var baseStation= from item in b
                             where (item)//לבדוק 
            return baseStation;
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
            if (baseStationToAdd.slotCharge< 0)
                throw new AddingProblemException("there is no charge slots");
            try
            {

                IDAL.DO.BaseStation b = new IDAL.DO.BaseStation() { IdNumber = baseStationToAdd.Id, ChargeSlots=baseStationToAdd.ChargeSlots, Latitude = baseStationToAdd.Local.Latitude, Longitude = baseStationToAdd.Local.Longitude, Name = baseStationToAdd.Name };
                dal.AddBaseStation(b);
                b.list of drones = null;//לבדוק
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
            IBL.BO.BaseStation b = dal.GetBaseStation(x => x.id == id);
            if (b == null)
                throw new UpdatingException("the customer is not existing");
            if (Name != "") b.Name = Name;
            if (numberOfCharge != 0) b.ChargeSlots = numberOfCharge;
            try
            {
                dal.UpdateBaseStation.Func(b);//לא באמת קוראים לה ככה
            }
            catch (Exception e)
            {
                throw new UpdatingException("can't update the base station", e);
            }

        }
        #endregion
        #region GetBaseStation
        public <IBL.BO.BaseStationToList> GetBaseStation()
        {
            return dal.GetBaseStations(); //להוסיף חריגות
        }
        #endregion

    }
}
