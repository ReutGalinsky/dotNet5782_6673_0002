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
                dal.AddCustomer(c);
            }
            catch (Exception e)
            {
                throw new AddingProblemException("can't add the customer", e);
            }
        }
        #endregion

        #region AddParcelToDelivery
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
            {
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
                p.DroneId = default(string);
                p.CreateParcelTime = DateTime.Now;
                p.MatchForDroneTime = new DateTime();
                p.ArrivingDroneTime = new DateTime();
                p.collectingDroneTime = new DateTime();
                return  dal.AddParcel(p);
            }
            catch (Exception e)
            {
                throw new AddingProblemException("can't add the parcel", e);
            }
        }
        #endregion

        #region UpdatingDetailsOfCustomer
        public void UpdatingDetailsOfCustomer(string id, string Name, string phone)//למה היו ערכי ברירת מחדל. איך נשלח את זה לפונקציה?
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
                    c.Latitude = updatedCustomer.Location.Latitude;
                    c.Longitude = updatedCustomer.Location.Longitude;
                    dal.UpdateCustomer(c);
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
        {
            var list = from item in dal.GetCustomers() select (IBL.BO.CustomerToList)item.CopyPropertiesToNew(typeof(IBL.BO.CustomerToList));
            foreach (var item in list)
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
        public IBL.BO.Customer GetCustomer(string id)
        {
            try
            {
                IDAL.DO.Customer c = dal.GetCustomer(id);
                IBL.BO.Customer customer = (IBL.BO.Customer)c.CopyPropertiesToNew(typeof(IBL.BO.Customer));
                customer.Location = new Location();
                customer.Location.Latitude = c.Latitude;
                customer.Location.Longitude = c.Longitude;
                customer.FromCustomer = dal.GetParcels()
                    .Where(p => (p.Sender) == id)
                    .Select(p => GetPOC(p.IdNumber, true)).ToList();
                customer.ToCustomer = dal.GetParcels()
                    .Where(p => (p.Geter) == id)
                    .Select(p => GetPOC(p.IdNumber, false)).ToList();
                return customer;
            }
            catch (Exception e)
            {
                throw new GettingProblemException("the customer is not exist",e );
            }
        }
        #endregion
        private IBL.BO.ParcelOfCustomer GetPOC(string id, bool senderOrReciever)
        {
            IDAL.DO.Parcel p = dal.GetParcel(id);
            IBL.BO.ParcelOfCustomer poc = (IBL.BO.ParcelOfCustomer)p.CopyPropertiesToNew(typeof(IBL.BO.ParcelOfCustomer));
            if (p.MatchForDroneTime == default(DateTime))
                poc.State = State.Define;
            else//צריך לבדוק אם לא נוצר ולזרוק חריגה?
                if (p.collectingDroneTime == default(DateTime))
                poc.State = State.match;
            else
                if (p.ArrivingDroneTime == default(DateTime))
                poc.State = State.pick;
            else
                poc.State = State.supply;
            if (senderOrReciever == true) poc.SourceOrDestinaton = GetCustomerOfParcel(p.Geter);
            else poc.SourceOrDestinaton = GetCustomerOfParcel(p.Sender);
            return poc;
        }

        private IBL.BO.CustomerOfParcel GetCustomerOfParcel(string id)
        {
            IDAL.DO.Customer temp = default(IDAL.DO.Customer);
            try
            {
                return (IBL.BO.CustomerOfParcel)dal.GetCustomer(id).CopyPropertiesToNew(typeof(IBL.BO.CustomerOfParcel));
            }
            catch(Exception e)
            {
                throw new AddingProblemException("the customer is not exist");
            }
        }

    }

}
