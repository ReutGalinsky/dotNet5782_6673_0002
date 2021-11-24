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
        {
            try
            {
                IDAL.DO.Parcel p = dal.GetParcel(id);
                IBL.BO.Parcel parcel = (IBL.BO.Parcel)p.CopyPropertiesToNew(typeof(IBL.BO.Parcel));
                parcel.SenderCustomer = GetCustomerOfParcel(p.Sender);
                parcel.GeterCustomer = GetCustomerOfParcel(p.Geter);
                if (p.DroneId != default(string))
                {
                    parcel.Drone = GetDroneInParcel(p.DroneId);
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
        {
            IBL.BO.Drone d = GetDrone(id);
            IBL.BO.DroneInParcel dip = (IBL.BO.DroneInParcel)d.CopyPropertiesToNew(typeof(IBL.BO.DroneInParcel));
            dip.Current = new Location();
            dip.Current.Latitude = d.Current.Latitude;
            dip.Current.Longitude = d.Current.Longitude;
            return dip;
        }
        #region GetParcels
        public IEnumerable<IBL.BO.ParcelOfList> GetParcels()
        {
            var list = from item in dal.GetParcels() select GetPOL(item.IdNumber);
            return list;
        }
        #endregion
        private IBL.BO.ParcelOfList GetPOL(string id)
        {
            IDAL.DO.Parcel P=dal.GetParcel(id);
            IBL.BO.ParcelOfList  pol = (IBL.BO.ParcelOfList)P.CopyPropertiesToNew(typeof(IBL.BO.ParcelOfList));
            if (P.MatchForDroneTime == default(DateTime))
                pol.State = State.Define;
            else//צריך לבדוק אם לא נוצר ולזרוק חריגה?
                if (P.collectingDroneTime == default(DateTime))
                pol.State = State.match;
            else
                if (P.ArrivingDroneTime == default(DateTime))
                pol.State = State.pick;
            else
                pol.State = State.supply;
            return pol;
        }
        #region GetParcelsNotMatching
        public IEnumerable<IBL.BO.ParcelOfList> GetParcelsNotMatching()
        {
            //var list = from item in GetParcels()
            //           where (item.State == State.Define)
            //           select (IBL.BO.ParcelOfList)item.CopyPropertiesToNew(typeof(IBL.BO.ParcelOfList));
            var list = from item in dal.GetParcels()
                       where item.MatchForDroneTime == default(DateTime)
                       select item;
            var l =list.Select(x=>GetPOL(x.IdNumber));
            return l;
        }
        #endregion
    }
}
