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
        #region AddCustomer
        public void AddCustomer(IBL.BO.Customer customerToAdd)
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
            try { if (int.Parse(customerToAdd.Phone) == 0)
                    throw new AddingProblemException("the phone number is illegal"); }
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
                IDAL.DO.Customer c = (IDAL.DO.Customer)customerToAdd.CopyPropertiesToNew(typeof(IDAL.DO.Customer));
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
        public string AddParcelToDelivery(IBL.BO.ParcelOfList parcel)
        {
            if (parcel.Sender == "")
                throw new AddingProblemException("invalid name of customer");
            if (parcel.Geter == "")
                throw new AddingProblemException("invalid name of customer");
            if (parcel.Weight != IBL.BO.WeightCategories.Heavy && parcel.Weight != IBL.BO.WeightCategories.Middle && parcel.Weight != IBL.BO.WeightCategories.Light)
                throw new AddingProblemException("This weight is not an option");
            if (parcel.Priority != IBL.BO.Priorities.Emergency && parcel.Priority != IBL.BO.Priorities.Regular && parcel.Priority != IBL.BO.Priorities.Speed)
                throw new AddingProblemException("This priority is not an option");
            try
            {//check if the customers exist in DAL
                IDAL.DO.Customer c = dal.GetCustomer(parcel.Sender);
                c = dal.GetCustomer(parcel.Geter);
            }
            catch(Exception e)
            {
                throw new AddingProblemException("the customer is not exist", e);
            }
            try
            {
                IDAL.DO.Parcel p = (IDAL.DO.Parcel)parcel.CopyPropertiesToNew(typeof(IDAL.DO.Parcel));
                p.DroneId = null;
                p.CreateParcelTime = DateTime.Now;
                p.MatchForDroneTime = null;
                p.ArrivingDroneTime = null;
                p.collectingDroneTime = null;
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
                IDAL.DO.Customer temp = dal.GetCustomer(id);
                IBL.BO.Customer updatedCustomer = (IBL.BO.Customer)temp.CopyPropertiesToNew(typeof(IBL.BO.Customer));
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
                    IDAL.DO.Customer c= (IDAL.DO.Customer)updatedCustomer.CopyPropertiesToNew(typeof(IDAL.DO.Customer));
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
        public IEnumerable<IBL.BO.CustomerToList> GetCustomers()
            //return all the customers of the data base
        {
            var list = from item in dal.GetCustomers() select (IBL.BO.CustomerToList)item.CopyPropertiesToNew(typeof(IBL.BO.CustomerToList));
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
        public IBL.BO.Customer GetCustomer(string id)
        {
            try
            {
                IDAL.DO.Customer c = dal.GetCustomer(id);
                IBL.BO.Customer customer = (IBL.BO.Customer)c.CopyPropertiesToNew(typeof(IBL.BO.Customer));
                customer.Location = new Location();
                customer.Location.Latitude = c.Latitude;
                customer.Location.Longitude = c.Longitude;
                customer.FromCustomer = dal.PredicateParcel(p => (p.Sender) == id).Select(p => GetPOC(p.IdNumber, true)).ToList();//the parcels that the customer send
                customer.ToCustomer = dal.PredicateParcel(p => (p.Geter) == id).Select(p => GetPOC(p.IdNumber, false)).ToList();
                return customer;
            }
            catch (Exception e)
            {
                throw new GettingProblemException("the customer is not exist",e );
            }
        }
        #endregion
        private IBL.BO.ParcelOfCustomer GetPOC(string id, bool senderOrReciever)
            //private function that return object of: ParcelOfCustomer
        {
            try
            {
                IDAL.DO.Parcel p = dal.GetParcel(id);
                IBL.BO.ParcelOfCustomer poc = (IBL.BO.ParcelOfCustomer)p.CopyPropertiesToNew(typeof(IBL.BO.ParcelOfCustomer));
                if (p.MatchForDroneTime == null)//define the state:
                    poc.State = State.Define;
                else
                    if (p.collectingDroneTime == null)
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
            catch(Exception e)
            {
                throw new GettingProblemException("error in Parcel", e);
            }
        }

        private IBL.BO.CustomerOfParcel GetCustomerOfParcel(string id)
            //function that return object of "CustomerOfParecl
        {
            try
            {
                return (IBL.BO.CustomerOfParcel)dal.GetCustomer(id).CopyPropertiesToNew(typeof(IBL.BO.CustomerOfParcel));
            }
            catch(Exception e)
            {
                throw new AddingProblemException("the customer is not exist");
            }
        }
        #region PredicateCustomer
        public IEnumerable<IBL.BO.CustomerToList> PredicateCustomer(Predicate<IBL.BO.CustomerToList> c)
        {
            var list = from item in GetCustomers()
                       where c(item)
                       select item;
            return list;
        }
        #endregion
    }

}
