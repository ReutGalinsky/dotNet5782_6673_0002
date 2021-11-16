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
                dal.AddCustomer(customerToAdd);
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
            if (parcel.Send.Name == "")//לבדוק את הקטע הזה
                throw new AddingProblemException("invalid name of customer");
            if (parcel.Get.Name == "")
                throw new AddingProblemException("invalid name of customer");
            if (parcel.Weight != IBL.BO.WeightCategories.Heavy && parcel.Weight != IBL.BO.WeightCategories.Middle && parcel.Weight != IBL.BO.WeightCategories.Light)
                throw new AddingProblemException("This weight is not an option");
            if (parcel.Priority!= IBL.BO.Priorities.Emergency&& parcel.Priority!=IBL.BO.Priorities.Regular&&parcel.Priority!=IBL.BO.Priorities.Speed)
                throw new AddingProblemException("This prioritie is not an option");
            try
            {
              parcel.Drone=null;
              dal.AddParcel(parcel);
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
            Customer updatedCustomer=dal.GetCustomers().Find(x => x.id == id);
            if (updatedCustomer == null)
                throw new UpdatingException("the customer is not existing");
            if(Name != "")updatedCustomer.Name = Name;
            if(phone != "") updatedCustomer.Phone = phone;
           try
            {
                dal.UpdateCustomer(updatedCustomer);
            }
            catch (Exception e)
            {
                throw new UpdatingException("can't update the customer", e);
            }
        }
        #endregion
        #region GetCustomers
        public IEnumerable<IBL.BO.CustomerToList> getCustomers()
        {
            return Customers; //פונקציה גנרית
        }
        #endregion

        #region GetCustomer
        public IBL.BO.CustomerToList GetCustomer(int id)
        {
                   IBL.BO.<CustomerToList> c = GetCustomers();
            var Customer= from item in c
                             where (item)//לבדוק 
            return Customer;
        }
        #endregion
    }

}
