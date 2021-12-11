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
                DO.Parcel parcelDO = dal.GetParcel(id);
                BO.Parcel parcel = (BO.Parcel)parcelDO.CopyPropertiesToNew(typeof(BO.Parcel));
                parcel.SenderCustomer = GetCustomerOfParcel(parcelDO.Sender);
                parcel.GeterCustomer = GetCustomerOfParcel(parcelDO.Geter);
                parcel.Drone=parcelDO.DroneId!=null? GetDroneInParcel(parcelDO.DroneId):null;
                //if (parcelDO.DroneId != null)
                //{
                //    parcel.Drone = GetDroneInParcel(parcelDO.DroneId);//return the drone that match this parcel 
                //}
                return parcel;
            }
            catch (Exception e)
            {
                throw new GettingProblemException("the pacrel is not exist", e);
            }
        }
        #endregion

        #region GetParcels
        public IEnumerable<BO.ParcelOfList> GetParcels()
        //function that return all the parcels for view
        {
            var list = from item in dal.GetParcels() select GetPOL(item.IdNumber);
            return list;
        }
        #endregion

        #region GetAllParcelsBy
        public IEnumerable<BO.ParcelOfList> GetAllParcelsBy(Predicate<BO.ParcelOfList> condition)
        {
            var list = from item in GetParcels()
                       where condition(item)
                       select item;
            return list;
        }
        #endregion

        //********return inner objects********
        private BO.DroneInParcel GetDroneInParcel(string id)
        //private function that return object of "DroneInParcel"
        {
            BO.Drone drone = GetDrone(id);//get the drone
            BO.DroneInParcel droneInParcel = (BO.DroneInParcel)drone.CopyPropertiesToNew(typeof(BO.DroneInParcel));
            droneInParcel.Location = drone.Location.GetLocation();
            //droneInParcel.Location = new Location();//add the location
            //droneInParcel.Location.Latitude = d.Location.Latitude;
            //droneInParcel.Location.Longitude = d.Location.Longitude;
            return droneInParcel;
        }
        private BO.ParcelOfList GetPOL(string id)
        //private function that return object of ParcelOfList
        {
            DO.Parcel parcel = dal.GetParcel(id);
            BO.ParcelOfList parcelOfList = (BO.ParcelOfList)parcel.CopyPropertiesToNew(typeof(BO.ParcelOfList));
            //if (parcel.MatchForDroneTime == null)
            //    parcelOfList.ParcelState = ParcelState.Define;//define the parcel state
            //else
            //    if (parcel.CollectingDroneTime == null)
            //    parcelOfList.ParcelState = ParcelState.match;
            //else
            //    if (parcel.ArrivingDroneTime == null)
            //    parcelOfList.ParcelState = ParcelState.pick;
            //else
            //    parcelOfList.ParcelState = ParcelState.supply;
            parcelOfList.ParcelState = parcel.MatchForDroneTime switch
            {
                null => ParcelState.Define,
                _ => parcel.CollectingDroneTime switch
                {
                    null=> ParcelState.match,
                    _ => parcel.ArrivingDroneTime switch
                    {
                        null=> ParcelState.pick,
                        _=> ParcelState.supply,
                    },
                },
            };
            return parcelOfList;
        }

    }
}