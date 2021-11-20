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
    public partial class BL
    {
        #region AddCustomer
        public void AddCustomer(IBL.BO.Customer customerToAdd)
        {
            if (customerToAdd.Name == "")
                throw new AddingProblemException("invalid name of customer");
            if (int.Parse(customerToAdd.Phone) == 0)
                throw new AddingProblemException("the phone number is illegal");
            if (customerToAdd.Local.Latitude > 33.3 || customerToAdd.Local.Latitude < 29.5)
                throw new AddingProblemException("the location is out of israel");
            if (customerToAdd.Local.Longitude > 35.6 || customerToAdd.Local.Longitude < 34.5)
                throw new AddingProblemException("the location is out of israel");
            try
            {
                IDAL.DO.Customer c = (IDAL.DO.Customer)customerToAdd.CopyPropertiesToNew(typeof(IDAL.DO.Customer));
                c.Longitude = customerToAdd.Local.Longitude;
                c.Latitude = customerToAdd.Local.Latitude;
                dal.AddCustomer(c);
            }
            catch (Exception e)
            {
                throw new AddingProblemException("can't add the customer", e);
            }
        }
        #endregion

        #region AddParcelToDelivery
        public void AddParcelToDelivery(IBL.BO.ParcelOfList parcel)
        {
            if (parcel.ClientSendName == "")
                throw new AddingProblemException("invalid name of customer");
            if (parcel.ClientGetName == "")
                throw new AddingProblemException("invalid name of customer");
            if (parcel.Weight != IBL.BO.WeightCategories.Heavy && parcel.Weight != IBL.BO.WeightCategories.Middle && parcel.Weight != IBL.BO.WeightCategories.Light)
                throw new AddingProblemException("This weight is not an option");
            if (parcel.Priority != IBL.BO.Priorities.Emergency && parcel.Priority != IBL.BO.Priorities.Regular && parcel.Priority != IBL.BO.Priorities.Speed)
                throw new AddingProblemException("This prioritie is not an option");
            try
            {
                IDAL.DO.Parcel p = (IDAL.DO.Parcel)parcel.CopyPropertiesToNew(typeof(IDAL.DO.Parcel));
                p.DroneId = default(int);
                p.CreateParcelTime = DateTime.Now;
                p.MatchForDroneTime = new DateTime();
                p.ArrivingDroneTime = new DateTime();
                p.collectingDroneTime = new DateTime();
                dal.AddParcel(p);
            }
            catch (Exception e)
            {
                throw new AddingProblemException("can't add the parcel", e);
            }

        }
        #endregion

        #region UpdatingDetailsOfCustomer
        public void UpdatingDetailsOfCustomer(int id, string Name, string phone)//למה היו ערכי ברירת מחדל. איך נשלח את זה לפונקציה?
        {
            try
            {
                IBL.BO.Customer updatedCustomer = (IBL.BO.Customer)dal.GetCustomer(id).CopyPropertiesToNew(typeof(IBL.BO.Customer));
                if (Name != "") updatedCustomer.Name = Name;
                if (phone != "")
                {
                    if (int.Parse(phone) == 0)
                        throw new UpdatingException("the phone number is illegal");
                    updatedCustomer.Phone = phone;
                }
                try
                {
                    IDAL.DO.Customer c= (IDAL.DO.Customer)updatedCustomer.CopyPropertiesToNew(typeof(IDAL.DO.Customer));
                    c.Latitude = updatedCustomer.Local.Latitude;
                    c.Longitude = updatedCustomer.Local.Longitude;
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
        public IBL.BO.Customer GetCustomer(int id)
        {
            try
            {
                // GeoCoordinate
                IDAL.DO.Customer c = dal.GetCustomer(id);
                IBL.BO.Customer customer = (IBL.BO.Customer)c.CopyPropertiesToNew(typeof(IBL.BO.Customer));
                customer.Local.Latitude = c.Latitude;
                customer.Local.Longitude = c.Longitude;
                customer.FromCustomer = dal.GetParcels()
                    .Where(p => int.Parse(p.ClientSendName) == id)
                    .Select(p => GetPOC(p.IdNumber, true)).ToList();
                customer.ToCustomer = dal.GetParcels()
                    .Where(p => int.Parse(p.ClientSendName) == id)
                    .Select(p => GetPOC(p.IdNumber, false)).ToList();
                return customer;
            }
            catch (Exception e)
            {
                throw new GettingProblemException("the customer is not exist", e);
            }
        }
        #endregion
        private IBL.BO.ParcelOfCustomer GetPOC(int id, bool senderOrReciever)
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
            if (senderOrReciever == true) poc.SourceOrDestinaton = GetCustomerOfParcel(p.ClientGetName);
            else poc.SourceOrDestinaton = GetCustomerOfParcel(p.ClientSendName);
            return poc;
        }

        private IBL.BO.CustomerOfParcel GetCustomerOfParcel(string id)
        {
            return (IBL.BO.CustomerOfParcel)dal.GetCustomer(int.Parse(id)).CopyPropertiesToNew(typeof(IBL.BO.CustomerOfParcel));
        }

    }

}
