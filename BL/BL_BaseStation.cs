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
            return list;
        }
        #endregion

        #region GetBaseStationsWithCharge
        public IEnumerable<IBL.BO.BaseStationToList> GetBaseStationsWithCharge()
        {
            var b = dal.GetBaseStations().Where(x=>x.ChargeSlots>0).Select(x=>(IBL.BO.BaseStationToList)x.CopyPropertiesToNew(typeof(IBL.BO.BaseStationToList)));
            return b;
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
            if (baseStationToAdd.ChargeSlots< 0)
                throw new AddingProblemException("there is no charge slots");
            try
            {

                dal.AddBaseStation((IDAL.DO.BaseStation)baseStationToAdd.CopyPropertiesToNew(typeof(IDAL.DO.BaseStation)));
                b.list of drones = null;//לבדוק
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
        public IBL.BO.BaseStationToList GetBaseStation(int id)
        {
            return (IBL.BO.BaseStationToList)dal.GetBaseStation(id).CopyPropertiesToNew(typeof(IBL.BO.BaseStationToList)); //להוסיף חריגות
        }
        #endregion

    }
}
