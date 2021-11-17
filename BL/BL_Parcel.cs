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
    partial class BL
    {
        #region AddCustomer
        public void AddCustomer(IBL.BO.Customer customerToAdd)
        {
            if (customerToAdd.Name == "")
                throw new AddingProblemException("invalid name of customer");
            if (int.Parse(customerToAdd.Phone) == 0)
                throw new AddingProblemException("the phone number is illegal");
            if (customerToAdd.Local.Latitude > 35 || customerToAdd.Local.Latitude < 33)
                throw new AddingProblemException("the location is out of israel");
            if (customerToAdd.Local.Longitude > 33 || customerToAdd.Local.Longitude < 31)
                throw new AddingProblemException("the location is out of israel");
            try
            {
                IDAL.DO.Customer c=(IDAL.DO.Customer)customerToAdd.CopyPropertiesToNew(typeof(IDAL.DO.Customer));
                c.Location.longitude=customerToAdd.Local.Longitude;
                c.Location.latitude=customerToAdd.Local.Latitude;
            }
            catch(Exception e)
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
            if (parcel.Priority!= IBL.BO.Priorities.Emergency&& parcel.Priority!=IBL.BO.Priorities.Regular&&parcel.Priority!=IBL.BO.Priorities.Speed)
                throw new AddingProblemException("This prioritie is not an option");
            try
            {
            parcel.Drone=null;
            IDAL.DO.Parcel p=(IDAL.DO.Parcel)parcel.CopyPropertiesToNew(typeof(IDAL.DO.Parcel));
            p.CreateParcelTime=DateTime.Now;
            p.MatchParcelTime= new DateTime;
            p.ArrivingParcelTime= new DateTime;
            p.collectingParcelTime= new DateTime;
            dal.AddParcel(p);
            }
            catch (Exception e)
            {
                throw new AddingProblemException("can't add the parcel", e);
            }

        }
        #endregion

        #region UpdatingNameOfCustomer
        public void UpdatingNameOfCustomer( int id,string Name="",string phone="")
        {
            try
            {
               IBL.BO.Customer updatedCustomer = (IBL.BO.Customer)dal.GetCustomer(id).CopyPropertiesToNew(typeof(IBL.BO.Customer));
            }
            catch(Exception e)
            {
                throw new UpdatingException("the customer is not exist", e);
            }
            if(Name != "") updatedCustomer.Name = Name;
            if(phone != "") updatedCustomer.Phone = phone;
           try
            {
                dal.UpdateCustomer((IDAL.DO.Customer)updatedCustomer.CopyPropertiesToNew(typeof(IDAL.DO.Customer)));
            }
            catch (Exception e)
            {
                throw new UpdatingException("can't update the customer", e);
            }
        }
        #endregion

        #region GetCustomers
        public IEnumerable<IBL.BO.CustomerToList> GetCustomers()
        {
            var list = from item in dal.GetCustomers() select (IBL.BO.CustomerToList)item.CopyPropertiesToNew(typeof(IBL.BO.CustomerToList));
            return list;

        }
        #endregion

        #region GetCustomer
        public IBL.BO.Customer GetCustomer(int id)
        {
            try
            {
                IDAL.DO.Customer c= dal.GetCustomer(id);
                IBL.BO.Customer customer = (IBL.BO.Customer)c.CopyPropertiesToNew(typeof(IBL.BO.Customer));
                customer.Local.Latitude = c.Latitude;
                customer.Local.Longitude = c.Longitude;
                var list.
            }
            catch (Exception e)
            {
                throw new GettingProblemException("the customer is not exist", e);
            }
        }
        #endregion
    }

}
