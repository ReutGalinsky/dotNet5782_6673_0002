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
            var list = from item in dal.GetParcels() select (IBL.BO.ParcelOfList)item.CopyPropertiesToNew(typeof(IBL.BO.ParcelOfList));
            foreach (var item in list)
            {
                IBL.BO.Parcel p = GetParcel(item.IdNumber);
                if (p.MatchForDroneTime == default(DateTime))//לא שינה לdefine
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
        private IBL.BO.ParcelOfList GetPOL(string id)
        {
            IDAL.DO.Parcel P=dal.GetParcel(id);
            IBL.BO.ParcelOfList  ofList = (IBL.BO.ParcelOfList)P.CopyPropertiesToNew(typeof(IBL.BO.ParcelOfList));
            if (P.MatchForDroneTime==default(DateTime))
            {
                ofList.State = State.Define;
            }
            return ofList;
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
            foreach (var n in list)
                if(n.MatchForDroneTime==default(DateTime))
                { Console.WriteLine(n.IdNumber); }
            var l =list.Select(x=>GetPOL(x.IdNumber));
            foreach (var n in l)
                { Console.WriteLine(n.IdNumber); }
            return l;
        }
        #endregion
    }
}
