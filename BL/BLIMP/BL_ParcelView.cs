using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using BO;

namespace BL
{
    internal partial class BL : BLApi.IBL
    {
        #region GetParcel
        public BO.Parcel GetParcel(string id)
        //return single parcel
        {
            try
            {
                DO.Parcel p = dal.GetParcel(id);
                BO.Parcel parcel = (BO.Parcel)p.CopyPropertiesToNew(typeof(BO.Parcel));
                parcel.SenderCustomer = GetCustomerOfParcel(p.Sender);
                parcel.GeterCustomer = GetCustomerOfParcel(p.Geter);
                if (p.DroneId != null)
                {
                    parcel.Drone = GetDroneInParcel(p.DroneId);//return the drone that match this parcel 
                }
                return parcel;
            }
            catch (Exception e)
            {
                throw new GettingProblemException("the pacrel is not exist", e);
            }
        }
        #endregion
        private BO.DroneInParcel GetDroneInParcel(string id)
        //private function that return object of "DroneInParcel"
        {
            BO.Drone d = GetDrone(id);//get the drone
            BO.DroneInParcel dip = (BO.DroneInParcel)d.CopyPropertiesToNew(typeof(BO.DroneInParcel));
            dip.Location = new Location();//add the location
            dip.Location.Latitude = d.Location.Latitude;
            dip.Location.Longitude = d.Location.Longitude;
            return dip;
        }

        #region GetParcels
        public IEnumerable<BO.ParcelOfList> GetParcels()
        //function that return all the parcels for view
        {
            var list = from item in dal.GetParcels() select GetPOL(item.IdNumber);
            return list;
        }
        #endregion
        private BO.ParcelOfList GetPOL(string id)
        //private function that return object of ParcelOfList
        {
            DO.Parcel P = dal.GetParcel(id);
            BO.ParcelOfList pol = (BO.ParcelOfList)P.CopyPropertiesToNew(typeof(BO.ParcelOfList));
            if (P.MatchForDroneTime == null)
                pol.ParcelState = ParcelState.Define;//define the parcel state
            else
                if (P.CollectingDroneTime == null)
                pol.ParcelState = ParcelState.match;
            else
                if (P.ArrivingDroneTime == null)
                pol.ParcelState = ParcelState.pick;
            else
                pol.ParcelState = ParcelState.supply;
            return pol;
        }
        #region PredicateParcel
        public IEnumerable<BO.ParcelOfList> PredicateParcel(Predicate<BO.ParcelOfList> c)
        {
            var list = from item in GetParcels()
                       where c(item)
                       select item;
            return list;
        }
        #endregion
    }
}