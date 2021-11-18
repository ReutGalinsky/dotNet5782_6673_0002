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
        #region GetParcel
        public IBL.BO.Parcel GetParcel(int id)
        {
            try
            {
            IDAL.DO.Parcel p= dal.GetParcel(id);
            IBL.BO.Parcel parcel= (IBL.BO.Parcel)p.CopyPropertiesToNew(typeof(IBL.BO.Parcel));
            parcel.Sender = GetCustomerOfParcel(p.ClientSendName);
            parcel.Sender = GetCustomerOfParcel(p.ClientGetName);
            parcel.Drone = GetDroneInParcel(p.DroneId);
            return parcel;
            }
            catch(Exception e)
            {
                throw new GettingProblemException("the pacrel is not exist", e);
            }
        }
        #endregion
        private IBL.BO.DroneInParcel GetDroneInParcel(int id)
        {
            IBL.BO.Drone d = GetDrone(id);
            IBL.BO.DroneInParcel dip = (IBL.BO.DroneInParcel)d.CopyPropertiesToNew(typeof(IBL.BO.DroneInParcel));
            dip.Current.Latitude = d.Current.Latitude;
            dip.Current.Longitude = d.Current.Longitude;
            return dip;

        }
        #region GetParcels
        public IEnumerable<IBL.BO.ParcelOfList> GetParcels()
        {
            var list=from item in dal.GetParcels() select (IBL.BO.ParcelOfList)item.CopyPropertiesToNew(typeof(IBL.BO.ParcelOfList));
            foreach(var item in list)
            {
                IBL.BO.Parcel p = GetParcel(item.IdNumber);
                if (p.MatchForDroneTime == default(DateTime))
                    item.State = State.Define;
                else if (p.collectingDroneTime == default(DateTime))
                    item.State = State.match;
                else if (p.ArrivingDroneTime == default(DateTime))
                    item.State = State.pick;
                else item.State = State.supply;
            }
            return list;
        }
        #endregion

        #region GetParcelsNotMatching
        public IEnumerable<IBL.BO.ParcelOfList> GetParcelsNotMatching()
        {
            var list = from item in GetParcels()
                       where (item.State==State.Define)
                       select (IBL.BO.ParcelOfList)item.CopyPropertiesToNew(typeof(IBL.BO.ParcelOfList));
            return list;
        }
        #endregion
    }
}
