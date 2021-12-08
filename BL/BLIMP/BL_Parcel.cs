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
        #region AddCustomer
        public void AddCustomer(BO.Customer customerToAdd)
        //add new customer

        {
            if (customerToAdd.Name == "")
                throw new AddingProblemException("invalid name of customer");
            try
            {
                if (int.Parse(customerToAdd.IdNumber) == 0)
                    throw new AddingProblemException("invalid Id of customer");
            }
            catch (Exception e)
            {
                throw new AddingProblemException("invalid Id of customer");
            }
            try
            {
                if (int.Parse(customerToAdd.Phone) == 0)
                    throw new AddingProblemException("the phone number is illegal");
            }
            catch (Exception e)
            {
                throw new AddingProblemException("invalid phone numeber of customer");
            }
            if (customerToAdd.Location.Latitude > 33.3 || customerToAdd.Location.Latitude < 29.5)
                throw new AddingProblemException("the Latitude is out of israel");
            if (customerToAdd.Location.Longitude > 35.6 || customerToAdd.Location.Longitude < 34.5)
                throw new AddingProblemException("the Longitude is out of israel");
            try
            {
                DO.Customer c = (DO.Customer)customerToAdd.CopyPropertiesToNew(typeof(DO.Customer));
                c.Longitude = customerToAdd.Location.Longitude;
                c.Latitude = customerToAdd.Location.Latitude;
                dal.AddCustomer(c);//add to DAL
            }
            catch (Exception e)
            {
                throw new AddingProblemException("can't add the customer", e);
            }
        }
        #endregion

        #region AddParcelToDelivery
        //add new parcel to the data base
        public string AddParcelToDelivery(BO.ParcelOfList parcel)
        {
            if (parcel.Sender == "")
                throw new AddingProblemException("invalid name of customer");
            if (parcel.Geter == "")
                throw new AddingProblemException("invalid name of customer");
            if (parcel.Weight != BO.WeightCategories.Heavy && parcel.Weight != BO.WeightCategories.Middle && parcel.Weight != BO.WeightCategories.Light)
                throw new AddingProblemException("This weight is not an option");
            if (parcel.Priority != BO.Priorities.Emergency && parcel.Priority != BO.Priorities.Regular && parcel.Priority != BO.Priorities.Speed)
                throw new AddingProblemException("This priority is not an option");
            try
            {//check if the customers exist in DAL
                DO.Customer c = dal.GetCustomer(parcel.Sender);
                c = dal.GetCustomer(parcel.Geter);
            }
            catch (Exception e)
            {
                throw new AddingProblemException("the customer is not exist", e);
            }
            try
            {
                DO.Parcel p = (DO.Parcel)parcel.CopyPropertiesToNew(typeof(DO.Parcel));
                p.DroneId = null;
                p.CreateParcelTime = DateTime.Now;
                p.MatchForDroneTime = null;
                p.ArrivingDroneTime = null;
                p.CollectingDroneTime = null;
                return dal.AddParcel(p);//retrun the id of the parcel
            }
            catch (Exception e)
            {
                throw new AddingProblemException("can't add the parcel", e);
            }
        }
        #endregion

        #region UpdatingDetailsOfCustomer
        // update customer
        public void UpdatingDetailsOfCustomer(string id, string Name, string phone)
        {
            try
            {
                DO.Customer temp = dal.GetCustomer(id);
                BO.Customer updatedCustomer = (BO.Customer)temp.CopyPropertiesToNew(typeof(BO.Customer));
                updatedCustomer.Location = new Location() { Latitude = temp.Latitude, Longitude = temp.Longitude };
                if (Name != "") updatedCustomer.Name = Name;
                if (phone != "")
                {
                    try
                    {
                        if (int.Parse(phone) == 0)
                            throw new UpdatingException("the phone number is illegal");
                        updatedCustomer.Phone = phone;
                    }
                    catch (Exception e)
                    { throw new UpdatingException("the phone number is illegal"); }
                }
                try
                {
                    DO.Customer c = (DO.Customer)updatedCustomer.CopyPropertiesToNew(typeof(DO.Customer));
                    c.Latitude = updatedCustomer.Location.Latitude;//add location
                    c.Longitude = updatedCustomer.Location.Longitude;
                    dal.UpdateCustomer(c);//update in DAL
                }
                catch (Exception e)
                {
                    throw new UpdatingException("can't update the customer", e);
                }
            }
            catch (Exception e)
            {
                throw new UpdatingException("the customer is not exist", e);
            }

        }
        #endregion

        #region GetCustomers
        public IEnumerable<BO.CustomerToList> GetCustomers()
        //return all the customers of the data base
        {
            var list = from item in dal.GetCustomers() select (BO.CustomerToList)item.CopyPropertiesToNew(typeof(BO.CustomerToList));
            foreach (var item in list)//calculate the amount of parcel
            {
                item.ParcelOnTheWay = GetCustomer(item.IdNumber).ToCustomer.Count(x => x.State == State.pick);
                item.ParcelSendAndGet = GetCustomer(item.IdNumber).FromCustomer.Count(x => x.State == State.supply);
                item.ParcelGet = GetCustomer(item.IdNumber).ToCustomer.Count(x => x.State == State.supply);
                item.ParcelSendAndGet = GetCustomer(item.IdNumber).FromCustomer.Count(x => x.State == State.supply);
            }
            return list;

        }
        #endregion

        #region GetCustomer
        //return single customer
        public BO.Customer GetCustomer(string id)
        {
            try
            {
                DO.Customer c = dal.GetCustomer(id);
                BO.Customer customer = (BO.Customer)c.CopyPropertiesToNew(typeof(BO.Customer));
                customer.Location = new Location();
                customer.Location.Latitude = c.Latitude;
                customer.Location.Longitude = c.Longitude;
                customer.FromCustomer = dal.PredicateParcel(p => (p.Sender) == id).Select(p => GetPOC(p.IdNumber, true)).ToList();//the parcels that the customer send
                customer.ToCustomer = dal.PredicateParcel(p => (p.Geter) == id).Select(p => GetPOC(p.IdNumber, false)).ToList();
                return customer;
            }
            catch (Exception e)
            {
                throw new GettingProblemException("the customer is not exist", e);
            }
        }
        #endregion
        private BO.ParcelOfCustomer GetPOC(string id, bool senderOrReciever)
        //private function that return object of: ParcelOfCustomer
        {
            try
            {
                DO.Parcel p = dal.GetParcel(id);
                BO.ParcelOfCustomer poc = (BO.ParcelOfCustomer)p.CopyPropertiesToNew(typeof(BO.ParcelOfCustomer));
                if (p.MatchForDroneTime == null)//define the state:
                    poc.State = State.Define;
                else
                    if (p.CollectingDroneTime == null)
                    poc.State = State.match;
                else
                    if (p.ArrivingDroneTime == null)
                    poc.State = State.pick;
                else
                    poc.State = State.supply;
                if (senderOrReciever == true) poc.SourceOrDestinaton = GetCustomerOfParcel(p.Geter);
                else poc.SourceOrDestinaton = GetCustomerOfParcel(p.Sender);
                return poc;
            }
            catch (Exception e)
            {
                throw new GettingProblemException("error in Parcel", e);
            }
        }

        private BO.CustomerOfParcel GetCustomerOfParcel(string id)
        //function that return object of "CustomerOfParecl
        {
            try
            {
                return (BO.CustomerOfParcel)dal.GetCustomer(id).CopyPropertiesToNew(typeof(BO.CustomerOfParcel));
            }
            catch (Exception e)
            {
                throw new AddingProblemException("the customer is not exist");
            }
        }
        #region PredicateCustomer
        public IEnumerable<BO.CustomerToList> PredicateCustomer(Predicate<BO.CustomerToList> c)
        {
            var list = from item in GetCustomers()
                       where c(item)
                       select item;
            return list;
        }
        #endregion
    }

}