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

                IDAL.DO.Customer c = new IDAL.DO.Customer() {Id=customerToAdd.Id,Phone=customerToAdd.Phone,Latitude=customerToAdd.Local.Latitude, Longitude = customerToAdd.Local.Longitude,Name=customerToAdd.Name };
                dal.AddCustomer(c);
          

            }
            catch(Exception e)
            {
                throw new AddingProblemException("can't add the customer", e);
            }
        }
        #endregion
        #region AddParcelToDelivery
        public void AddParcelToDelivery(IBL.BO.Parcel parcel)
    {
            if (parcel.Send.Name == "")
                throw new AddingProblemException("invalid name of customer");
            if (parcel.Get.Name == "")
                throw new AddingProblemException("invalid name of customer");
            if (parcel.Weight != IBL.BO.WeightCategories.Heavy && parcel.Weight != IBL.BO.WeightCategories.Middle && parcel.Weight != IBL.BO.WeightCategories.Light)
                throw new AddingProblemException("This weight is not an option");
            if (parcel.Priority!= IBL.BO.Priorities.Emergency&& parcel.Priority!=IBL.BO.Priorities.Regular&&parcel.Priority!=IBL.BO.Priorities.Speed)
                throw new AddingProblemException("This weight is not an option");
            try
            {
                IDAL.DO.Parcel p = new IDAL.DO.Parcel() { IdNumber = parcel.Id, ClientSendName = (IDAL.DO.CustomerOfParcel).parcel.Send, ClientGetName = (IDAL.DO.CustomerOfParcel).parcel.get, Weight = (IDAL.DO.WeightCategories)parcel.Weight, Priority = (IDAL.DO.Priorities)parcel.Priority,CreateParcelTime=DateTime(0),collectingDroneTime=0,ArrivingDroneTime=0,MatchForDroneTime=DateTime.Now  };
                //to initialize the drone with null             
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
            if (Name == "")
                throw new UpdatingException("the name is illegal");
            IBL.BO.Customer d =Customer.Find(x => x.id == id);
            if (d == null)
                throw new UpdatingException("the customer is not existing");
            if(Name != "")d.Name = Name;
            if(phone != "") d.Phone = phone;
            IDAL.DO.Customer updatedCustomer = new IDAL.DO.Customer() { Id = d.Id, Phone = d.Phone, Latitude = d.Local.Latitude, Longitude = d.Local.Longitude, Name = d.Name };
            try
            {
                dal.UpdateCustomer(updatedCustomer);
            }
            catch (Exception e)
            {
                throw new UpdatingException("can't update the customer", e);
            }

            try
            {

                IDAL.DO.Customer c = new IDAL.DO.Customer() { Id = customerToAdd.Id, Phone = customerToAdd.Phone, Latitude = customerToAdd.Local.Latitude, Longitude = customerToAdd.Local.Longitude, Name = customerToAdd.Name };
                dal.AddCustomer(c);


            }
            catch (Exception e)
            {
                throw new AddingProblemException("can't add the customer", e);
            }
        }
        #endregion
        #region GetCustomers
        public IEnumerable<IBL.BO.CustomerToList> getCustomers()
        {
            return Customers;
        }
        #endregion

        #region GetCustomer
        public IBL.BO.CustomerToList GetCustomer(int id)
        {
            IBL.BO.CustomerToList c = Customer.Find(x => x.IdNumber == id);
            if (c == null)
                throw new GettingProblemException("the customer is not exist");
            return c;
        }
        #endregion
    }

}
