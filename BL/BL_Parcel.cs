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
                dal.AddCustomer((IDAL.DO.Customer)customerToAdd.CopyPropertiesToNew(typeof(IDAL.DO.Customer)));
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
              dal.AddParcel((IDAL.DO.Parcel)parcel.CopyPropertiesToNew(typeof(IDAL.DO.Parcel)));
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
            IBL.BO.Customer updatedCustomer;
            try
            {
                updatedCustomer = (IBL.BO.Customer)dal.GetCustomer(id).CopyPropertiesToNew(typeof(IBL.BO.Customer));
            }
            catch(Exception e)
            {
                throw new UpdatingException("the customer is not exist", e);
            }
            if(Name != "")updatedCustomer.Name = Name;
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
        public IBL.BO.CustomerToList GetCustomer(int id)
        {
            try
            {
                return (IBL.BO.CustomerToList)dal.GetCustomer(id).CopyPropertiesToNew(typeof(IBL.BO.CustomerToList));
            }
            catch(Exception e)
            {
                throw new GettingProblemException("the customer is not exist", e);
            }
        }
        #endregion
    }

}
