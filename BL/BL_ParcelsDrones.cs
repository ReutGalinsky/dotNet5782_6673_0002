using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL;
using IDAL.DO;
using IDAL;
using IBL.BO;

namespace BL//לא עשינו
{
    class BL_ParcelsDrones
    {
        #region MatchingParcelToDrone
        public void MatchingParcelToDrone(int id)
        {
            IBL.BO.DroneToList d = Drones.Find(x => x.IdNumber == id);
            if (d == null)
                throw new ConnectionException("the drone is not existing");
            if (d.StateDrone != StateDrone.Available)
                throw new ConnectionException("the drone is not available");
            //to search parcel by: priority; weight, distance
            //the drone need to return to charge
            d.StateDrone = StateDrone.shipping;
            Parcel.matching = DateTime.Now;
            Parcel.drone = (droneinparcel)d; //פונקציה להכניס לחבילה לרחפן
        }
        #endregion

        #region PickingParcelByDrone
        public void PickingParcelByDrone(int id)
        {
            IBL.BO.DroneInParcel d = Drones.Find(x => x.IdNumber == id);
            if (d == null)
                throw new ConnectionException("the drone is not existing");
            IBL.BO.ParcelToList p = Parcel.Find(x => x.IdNumber == d.NumberOfParcel);
            if (p.StateDrone == shipping)
                throw new ConnectionException("the parcel is already shipping");
            throw new ConnectionException("the drone is not available");
            //to search parcel by: priority; weight, distance
            //the drone need to return to a near charge
            d.Battery = ;//down by distance
            d.Current = ()p.current;
            d.StateDrone = available;
            p.pickingtime = DateTime.Now;

        }
        #endregion

        #region SupplyingParcelByDrone
        public void SupplyingParcelByDrone(int id)
        {
            IBL.BO.DroneToList d = Drones.Find(x => x.IdNumber == id);
            if (d == null)
                throw new ConnectionException("the drone is not existing");
            if (d.StateDrone != StateDrone.available)
                throw new ConnectionException("the drone is not picking");
            d.Current =//by distance
                  d.Battery =//by distance
            Parcel.supplying = DateTime.Now;
        }
        #endregion
    }

}
