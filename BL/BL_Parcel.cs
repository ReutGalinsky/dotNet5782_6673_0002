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
    }
}
