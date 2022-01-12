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

        #region RemoveParcel
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void RemoveParcel(string number)
        {
            try
            {
                var parcel = GetParcel(number);
                if (parcel.Drone != null && parcel.ArrivingDroneTime == null && parcel.MatchForDroneTime != null)
                    throw new DeletingException("can't delete parcel that passing");
                lock (dal)
                {
                    dal.DeleteParcel(number);
                    var d = Drones.FirstOrDefault(x => x.NumberOfParcel == number);
                    if (d != null) d.State = DroneState.Available;
                }
            }
            catch (Exception e)
            {
                throw new DeletingException(e.Message, e);
            }
        }
        #endregion

        #region AddParcelToDelivery
        [MethodImpl(MethodImplOptions.Synchronized)]
        public string AddParcelToDelivery(BO.ParcelOfList parcel)
        {
            if (parcel.Sender == "")
                throw new AddingProblemException("invalid name of sender customer");
            if (parcel.Geter == "")
                throw new AddingProblemException("invalid name of geter customer");
            if (parcel.Weight != BO.WeightCategories.Heavy && parcel.Weight != BO.WeightCategories.Middle && parcel.Weight != BO.WeightCategories.Light)
                throw new AddingProblemException("This weight is not an option");
            if (parcel.Priority != BO.Priorities.Emergency && parcel.Priority != BO.Priorities.Regular && parcel.Priority != BO.Priorities.speed)
                throw new AddingProblemException("This priority is not an option");
            lock (dal)
            {
                try
                {
                    dal.GetCustomer(parcel.Sender);
                    dal.GetCustomer(parcel.Geter);
                    DO.Parcel parcelDO = (DO.Parcel)parcel.CopyPropertiesToNew(typeof(DO.Parcel));
                    parcelDO.CreateParcelTime = DateTime.Now;
                    return dal.AddParcel(parcelDO);
                }
                catch (Exception e)
                { throw new AddingProblemException(e.Message, e); }
            }
        }
        #endregion

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
            try
            {
                lock (dal)
                {
                    var list = from item in dal.GetParcels() select GetParcelOfList(item.IdNumber);
                    return list;
                }
            }
            catch (Exception e)
            { throw new GettingProblemException(e.Message, e); }

        }
        #endregion

        #region GetAllParcelsBy
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BO.ParcelOfList> GetAllParcelsBy(Predicate<BO.ParcelOfList> condition)
        {
            try
            {
                var list = from item in GetParcels()
                           where condition(item)
                           select item;
                return list;
            }
            catch (Exception e)
            { throw new GettingProblemException(e.Message, e); }

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
            try
            {
                BO.Drone drone = GetDrone(id);
                BO.DroneInParcel droneInParcel = (BO.DroneInParcel)drone.CopyPropertiesToNew(typeof(BO.DroneInParcel));
                droneInParcel.Location = drone.Location.GetLocation();
                return droneInParcel;
            }
            catch (Exception e)
            { throw new GettingProblemException(e.Message, e); }

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
            try
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
            catch(Exception e)
            { throw new GettingProblemException(e.Message, e); }
        }
        #endregion

        #region GetCustomerOfParcel
        /// <summary>
        /// returning the customer as 'customer of parcel'
        /// </summary>
        /// <param name="id">the customer id</param>
        /// <returns></returns>
        private BO.CustomerOfParcel GetCustomerOfParcel(string id)
        {
            try
            {
                lock (dal)
                {
                    return (BO.CustomerOfParcel)dal.GetCustomer(id).CopyPropertiesToNew(typeof(BO.CustomerOfParcel));
                }
            }
            catch (Exception e)
            {
                throw new AddingProblemException($"the customer with the id {id} is not exist");
            }
        }
        #endregion

    }

}