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
    class BL_ParcelView
    {
        #region GetParcel
        public IBL.BO.ParcelOfList GetParcel(int id)
        {
            IBL.BO.ParcelOfList p = Parcel.Find(x => x.IdNumber == id);
            if (p == null)
                throw new GettingProblemException("the parcel is not exist");
            return p;
        }
        #endregion
        #region GetParcels
        public IEnumerable<IBL.BO.ParcelOfList> GetParcels()
        {
            return Parcel;
        }
        #endregion
        #region GetParcelsNotMatching
        public IEnumerable<IBL.BO.ParcelOfList> GetParcelsNotMatching()
        {
            return Parcel;
        }
        #endregion
    }
}
