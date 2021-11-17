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
        #region GetParcel
        public IBL.BO.ParcelOfList GetParcel(int id)
        {
            IBL.BO.ParcelOfList parcel;
            try
            {
                parcel = (IBL.BO.ParcelOfList)dal.GetParcel(id).CopyPropertiesToNew(typeof(IBL.BO.ParcelOfList));
            }
            catch(Exception e)
            {
                throw new GettingProblemException("the pacrel is not exist", e);
            }
            return parcel;
           
        }
        #endregion

        #region GetParcels
        public IEnumerable<IBL.BO.ParcelOfList> GetParcels()
        {
            var list=from item in dal.GetParcels() select (IBL.BO.ParcelOfList)item.CopyPropertiesToNew(typeof(IBL.BO.ParcelOfList));
            foreach(var item in list)
            {

            }
            return list;
        }
        #endregion

        #region GetParcelsNotMatching
        public IEnumerable<IBL.BO.ParcelOfList> GetParcelsNotMatching()
        {
            var list = from item in GetParcels()
                       where (item.State==State.Define)
                       select (IBL.BO.ParcelOfList)item.CopyPropertiesToNew(typeof(IBL.BO.ParcelOfList));
            return list;
        }
        #endregion
    }
}
