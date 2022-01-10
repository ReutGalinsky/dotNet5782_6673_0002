using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using BO;
using System.Runtime.CompilerServices;
namespace BL
{
    internal partial class BL : BLApi.IBL
    {
        #region GetParcel
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Parcel GetParcel(string id)
        {
            try
            {
                lock (dal)
                {
                    DO.Parcel parcelDO = dal.GetParcel(id);
                    BO.Parcel parcel = (BO.Parcel)parcelDO.CopyPropertiesToNew(typeof(BO.Parcel));
                    parcel.SenderCustomer = GetCustomerOfParcel(parcelDO.Sender);
                    parcel.GeterCustomer = GetCustomerOfParcel(parcelDO.Geter);
                    parcel.Drone = parcelDO.DroneId != null ? GetDroneInParcel(parcelDO.DroneId) : null;
                    return parcel;
                }
            }
            catch (Exception e)
            {
                throw new GettingProblemException(e.Message, e);
            }
        }
        #endregion

        #region GetParcels
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BO.ParcelOfList> GetParcels()
        {
            lock (dal)
            {
                var list = from item in dal.GetParcels() select GetParcelOfList(item.IdNumber);
                return list;
            }
        }
        #endregion

        #region GetAllParcelsBy
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BO.ParcelOfList> GetAllParcelsBy(Predicate<BO.ParcelOfList> condition)
        {
            var list = from item in GetParcels()
                       where condition(item)
                       select item;
            return list;
        }
        #endregion


        #region GetDroneInParcel
        /// <summary>
        /// returning the drone as 'drone in parcel'
        /// </summary>
        /// <param name="id">the drone id</param>
        /// <returns></returns>
        private BO.DroneInParcel GetDroneInParcel(string id)
        {
            BO.Drone drone = GetDrone(id);
            BO.DroneInParcel droneInParcel = (BO.DroneInParcel)drone.CopyPropertiesToNew(typeof(BO.DroneInParcel));
            droneInParcel.Location = drone.Location.GetLocation();
            return droneInParcel;
        }
        #endregion
        #region GetParcelOfList
        /// <summary>
        /// returning the parcel as 'parcel of list'
        /// </summary>
        /// <param name="id">the parcel id</param>
        /// <returns></returns>
        private BO.ParcelOfList GetParcelOfList(string id)
        {
            lock (dal)
            {
                DO.Parcel parcel = dal.GetParcel(id);
                BO.ParcelOfList parcelOfList = (BO.ParcelOfList)parcel.CopyPropertiesToNew(typeof(BO.ParcelOfList));
                parcelOfList.ParcelState = parcel.MatchForDroneTime switch
                {
                    null => ParcelState.Define,
                    _ => parcel.CollectingDroneTime switch
                    {
                        null => ParcelState.match,
                        _ => parcel.ArrivingDroneTime switch
                        {
                            null => ParcelState.pick,
                            _ => ParcelState.supply,
                        },
                    },
                };
                return parcelOfList;
            }
        }
        #endregion

        #region Simulator
        public void Simulator(string id, Action updatePl, Func<bool> checkStop)
        {
            DroneSimulator simulator = new DroneSimulator(this, id, updatePl, checkStop);
        }
        #endregion

    }
}