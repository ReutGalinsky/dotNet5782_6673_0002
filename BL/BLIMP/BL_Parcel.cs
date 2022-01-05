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
    /// <summary>
    /// all possible actions on parcels
    /// </summary>
    internal partial class BL : BLApi.IBL
    {
        #region AddCustomer
        /// <summary>
        // adding a new customer
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddCustomer(BO.Customer customerToAdd)
        {
            //validation
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

            //add
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
            { throw new AddingProblemException("can't add the customer", e); }
        }
        #endregion
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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void RemoveCustomer(string number)
        {
            try
            {
                var customer = GetCustomer(number);
                if (customer.FromCustomer.FirstOrDefault(x=>x.State!= ParcelState.supply)!=null)
                    throw new DeletingException("can't delete customer that sending");
                if (customer.ToCustomer.FirstOrDefault(x => x.State != ParcelState.supply) != null)
                    throw new DeletingException("can't delete customer that waiting to parcel");
                lock (dal)
                {

                    dal.DeleteCustomer(number);
                }
            }
            catch (Exception e)
            {
                throw new DeletingException(e.Message, e);
            }
        }

        #region AddParcelToDelivery
        /// <summary>
        ///adding new parcel to the data base
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public string AddParcelToDelivery(BO.ParcelOfList parcel)
        {
            //validation
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

                //check existence of customers
                dal.GetCustomer(parcel.Sender);
                dal.GetCustomer(parcel.Geter);

                //add
                try
                {
                    DO.Parcel parcelDO = (DO.Parcel)parcel.CopyPropertiesToNew(typeof(DO.Parcel));
                    parcelDO.CreateParcelTime = DateTime.Now;
                    return dal.AddParcel(parcelDO);//retrun the id of the parcel

                }

                catch (Exception e)
                { throw new AddingProblemException("can't add the parcel", e); }
            }
        }
        #endregion

        #region UpdatingDetailsOfCustomer
        /// <summary>
        ///adding new parcel to the data base
        /// </summary>
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

            //validation
            if (Name != "") temp.Name = Name;
            if (phone != "")
            {
                int tempInteger;

                if (int.TryParse(phone, out tempInteger) == false)
                    throw new UpdatingException("the phone number is illegal");
                temp.Phone = phone;
            }

            //update
            try
            {
                lock (dal)
                {

                    dal.UpdateCustomer(temp);
                }
            }
            catch (Exception e)
            { throw new UpdatingException("can't update the customer", e); }
        }
        #endregion

        #region GetCustomers
        /// <summary>
        ///return all the customers of the data base
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BO.CustomerToList> GetCustomers()
        {
            lock (dal)
            {

                var list = from item in dal.GetCustomers() select (BO.CustomerToList)item.CopyPropertiesToNew(typeof(BO.CustomerToList));

                foreach (var item in list)//calculate the amount of parcel
                {
                    item.ParcelOnTheWay = GetCustomer(item.IdNumber).ToCustomer.Count(x => x.State == ParcelState.pick);
                    item.ParcelSendAndGet = GetCustomer(item.IdNumber).FromCustomer.Count(x => x.State == ParcelState.supply);
                    item.ParcelGet = GetCustomer(item.IdNumber).ToCustomer.Count(x => x.State == ParcelState.supply);
                    item.ParcelSendAndGet = GetCustomer(item.IdNumber).FromCustomer.Count(x => x.State == ParcelState.supply);
                }

                return list;
            }
        }
        #endregion

        #region GetCustomer
        /// <summary>
        ///return a single customer
        /// </summary>  

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
                    //customer.Location = new Location();
                    //customer.Location.Latitude = customerDO.Latitude;
                    //customer.Location.Longitude = customerDO.Longitude;
                    customer.FromCustomer = dal.GetAllParcelsBy(p => (p.Sender) == id).Select(p => GetPOC(p.IdNumber, true)).ToList();//the parcels that the customer send
                    customer.ToCustomer = dal.GetAllParcelsBy(p => (p.Geter) == id).Select(p => GetPOC(p.IdNumber, false)).ToList();
                    return customer;
                }
            }
            catch (Exception e)
            { throw new GettingProblemException("the customer is not exist", e); }
        }
        #endregion

        #region GetAllCustomersBy
        /// <summary>
        /// customer predicate
        /// </summary> 
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BO.CustomerToList> GetAllCustomersBy(Predicate<BO.CustomerToList> condition)
        {
            var list = from item in GetCustomers()
                       where condition(item)
                       select item;
            return list;
        }
        #endregion
        //**********return inner objects***********
        private BO.ParcelOfCustomer GetPOC(string id, bool senderOrReciever)
        //return single customer//private function that return object of: ParcelOfCustomer
        {
            try
            {
                lock (dal)
                {

                    DO.Parcel parcel = dal.GetParcel(id);
                    BO.ParcelOfCustomer parcelOfCustomer = (BO.ParcelOfCustomer)parcel.CopyPropertiesToNew(typeof(BO.ParcelOfCustomer));
                    if (senderOrReciever == true) parcelOfCustomer.SourceOrDestinaton = GetCustomerOfParcel(parcel.Geter);
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
        private BO.CustomerOfParcel GetCustomerOfParcel(string id)
        /// <summary>
        /// return object of CustomerOfParecl
        /// </summary> 
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

    }

}