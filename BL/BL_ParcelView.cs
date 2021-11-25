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
    public partial class BL : IBL.IBL
    {
        #region GetParcel
        public IBL.BO.Parcel GetParcel(string id)
            //return single parcel
        {
            try
            {
                IDAL.DO.Parcel p = dal.GetParcel(id);
                IBL.BO.Parcel parcel = (IBL.BO.Parcel)p.CopyPropertiesToNew(typeof(IBL.BO.Parcel));
                parcel.SenderCustomer = GetCustomerOfParcel(p.Sender);
                parcel.GeterCustomer = GetCustomerOfParcel(p.Geter);
                if (p.DroneId != default(string))
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
        private IBL.BO.DroneInParcel GetDroneInParcel(string id)
            //private function that return object of "DroneInParcel"
        {
            IBL.BO.Drone d = GetDrone(id);//get the drone
            IBL.BO.DroneInParcel dip = (IBL.BO.DroneInParcel)d.CopyPropertiesToNew(typeof(IBL.BO.DroneInParcel));
            dip.Location = new Location();//add the location
            dip.Location.Latitude = d.Location.Latitude;
            dip.Location.Longitude = d.Location.Longitude;
            return dip;
        }
       
        #region GetParcels
        public IEnumerable<IBL.BO.ParcelOfList> GetParcels()
            //function that return all the parcels for view
        {
            var list = from item in dal.GetParcels() select GetPOL(item.IdNumber);
            return list;
        }
        #endregion
        private IBL.BO.ParcelOfList GetPOL(string id)
            //private function that return object of ParcelOfList
        {
            IDAL.DO.Parcel P=dal.GetParcel(id);
            IBL.BO.ParcelOfList  pol = (IBL.BO.ParcelOfList)P.CopyPropertiesToNew(typeof(IBL.BO.ParcelOfList));
            if (P.MatchForDroneTime == null)
                pol.State = State.Define;//define the state
            else
                if (P.collectingDroneTime == null)
                pol.State = State.match;
            else
                if (P.ArrivingDroneTime == null)
                pol.State = State.pick;
            else
                pol.State = State.supply;
            return pol;
        }
        #region GetParcelsNotMatching
        //return all the non-matching parcels
        public IEnumerable<IBL.BO.ParcelOfList> GetParcelsNotMatching()
        {
            var list = from item in dal.GetParcels()
                       where item.MatchForDroneTime == null
                       select item;
            var l =list.Select(x=>GetPOL(x.IdNumber));
            return l;
        }
        #endregion
    }
}
