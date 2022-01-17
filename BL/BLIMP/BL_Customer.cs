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

        #region UpdatingDetailsOfCustomer
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdatingDetailsOfCustomer(string id, string Name, string phone)
        {
            DO.Customer temp;
            try
            {
                lock (dal)
                {
                    temp = dal.GetCustomer(id);
                }
            }
            catch (Exception e)
            {
                throw new UpdatingException(e.Message, e);
            }

            if (Name != "") temp.Name = Name;
            if (phone != "")
            {
                int tempInteger;
                if (int.TryParse(phone, out tempInteger) == false)
                    throw new UpdatingException("the phone number is illegal");
                temp.Phone = phone;
            }

            try
            {
                lock (dal)
                {
                    dal.UpdateCustomer(temp);
                }
            }
            catch (Exception e)
            { throw new UpdatingException($"can't update the customer: {e.Message}", e); }
        }
        #endregion

        #region GetCustomers
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BO.CustomerToList> GetCustomers()
        {
            try
            {
                lock (dal)
                {
                    var list = from item in dal.GetCustomers() select (BO.CustomerToList)item.CopyPropertiesToNew(typeof(BO.CustomerToList));
                    foreach (var item in list)
                    {
                        item.ParcelOnTheWay = GetCustomer(item.IdNumber).ToCustomer.Count(x => x.State == ParcelState.pick);
                        item.ParcelSendAndGet = GetCustomer(item.IdNumber).FromCustomer.Count(x => x.State == ParcelState.supply);
                        item.ParcelGet = GetCustomer(item.IdNumber).ToCustomer.Count(x => x.State == ParcelState.supply);
                        item.ParcelSendAndNotGet = GetCustomer(item.IdNumber).FromCustomer.Count(x => x.State == ParcelState.pick);
                    }
                    return list;
                }
            }
            catch(Exception e)
            { throw new GettingProblemException(e.Message, e); }
        }
        #endregion

        #region GetCustomer
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Customer GetCustomer(string id)
        {
            try
            {
                lock (dal)
                {
                    DO.Customer customerDO = dal.GetCustomer(id);
                    BO.Customer customer = (BO.Customer)customerDO.CopyPropertiesToNew(typeof(BO.Customer));
                    customer.Location = customerDO.GetLocation();
                    customer.FromCustomer = dal.GetAllParcelsBy(p => (p.Sender) == id).Select(p => GetParcelOfCustomer(p.IdNumber, true)).ToList();//the parcels that the customer send
                    customer.ToCustomer = dal.GetAllParcelsBy(p => (p.Geter) == id).Select(p => GetParcelOfCustomer(p.IdNumber, false)).ToList();
                    return customer;
                }
            }
            catch (Exception e)
            { throw new GettingProblemException(e.Message, e); }
        }
        #endregion

        #region AddCustomer
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddCustomer(BO.Customer customerToAdd)
        {
            if (customerToAdd.Name == "")
                throw new AddingProblemException("invalid name of customer");
            int tempInteger;
            if (int.TryParse(customerToAdd.IdNumber, out tempInteger) == false)
                throw new AddingProblemException("invalid Id of customer");
            if (int.TryParse(customerToAdd.Phone, out tempInteger) == false)
                throw new AddingProblemException("the phone number is illegal");
            if (customerToAdd.Location.Latitude > 33.3 || customerToAdd.Location.Latitude < 29.5)
                throw new AddingProblemException("the Latitude is out of israel");
            if (customerToAdd.Location.Longitude > 35.6 || customerToAdd.Location.Longitude < 34.5)
                throw new AddingProblemException("the Longitude is out of israel");

            try
            {
                DO.Customer customer = (DO.Customer)customerToAdd.CopyPropertiesToNew(typeof(DO.Customer));
                customer.Longitude = customerToAdd.Location.Longitude;
                customer.Latitude = customerToAdd.Location.Latitude;
                lock (dal)
                {
                    dal.AddCustomer(customer);
                }
            }
            catch (Exception e)
            { throw new AddingProblemException($"can't add the customer: {e.Message}", e); }
        }
        #endregion

        #region GetAllCustomersBy
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BO.CustomerToList> GetAllCustomersBy(Predicate<BO.CustomerToList> condition)
        {
            try
            {
                var list = from item in GetCustomers()
                           where condition(item)
                           select item;
                return list;
            }
            catch(Exception e)
            {
                throw new GettingProblemException(e.Message, e);
            }
        }
        #endregion

        #region GetParcelOfCustomer
        /// <summary>
        /// returning parcel as 'parcel of customer'
        /// </summary>
        /// <param name="id">the id of the parcel</param>
        /// <param name="isSender">flag that tells if this is the sender or geter</param>
        /// <returns></returns>
        private BO.ParcelOfCustomer GetParcelOfCustomer(string id, bool isSender)
        {
            try
            {
                lock (dal)
                {
                    DO.Parcel parcel = dal.GetParcel(id);
                    BO.ParcelOfCustomer parcelOfCustomer = (BO.ParcelOfCustomer)parcel.CopyPropertiesToNew(typeof(BO.ParcelOfCustomer));
                    if (isSender == true) parcelOfCustomer.SourceOrDestinaton = GetCustomerOfParcel(parcel.Geter);
                    else parcelOfCustomer.SourceOrDestinaton = GetCustomerOfParcel(parcel.Sender);
                    parcelOfCustomer.State = parcel.MatchForDroneTime switch
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
                    return parcelOfCustomer;
                }
            }
            catch (Exception e)
            {
                throw new GettingProblemException($"error in Parcel number {id}", e);
            }
        }
        #endregion

    }
}