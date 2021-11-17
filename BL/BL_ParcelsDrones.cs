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
            var list=GetParcelsNotMatching();
            Parcel win=list.First(); 
            foreach (var item in list)
            {
                if ((int)item.priority>(int)win.Priority)
                    win=item;
                else if ((int)item.priority=(int)win.Priority)
                    if ((int)item.weight>(int)win.Weight&& d.MaxWeight>=(int)item.weight)
                        win=item;
                    else if ((int)item.weight=(int)win.Weight)
                        if (distance(item.location)<distance(win.location))//check
                        win=item;
            }
            d.StateDrone=DroneState.shipping;
            d.NumberOfParcel=win.IdNumber;
            win.matching = DateTime.Now;
            win.Drone.IdNumber=d.IdNumber;
            dal.UpdateDrone(d);
            dal.UpdateParcel(win);
        }
        #endregion

        #region PickingParcelByDrone
        public void PickingParcelByDrone(int id)
        {
            IBL.BO.DroneToList d = Drones.Find(x => x.IdNumber == id);
            if (d == null)
                throw new ConnectionException("the drone is not existing");
            IBL.BO.Parcel p = GetParcel(d.NumberOfParcel);
            if (d.StateDrone != StateDrone.shipping)
                throw new ConnectionException("the drone is not shipping");
           if (p.State != State.match )
                throw new ConnectionException("the parcel is not match");
            throw new ConnectionException("the drone is not available");
            //the drone need to return to a near charge
            d.Battery -= distance*available;
            d.Current =p.current;//לא בדקנו עדיין איך משיגים מיקום של חבילה
            d.StateDrone = available;
            p.pickingtime = DateTime.Now;
            dal.UpdateDrone(d);
            dal.UpdateParcel(p);
       

        }
        #endregion

        #region SupplyingParcelByDrone
        public void SupplyingParcelByDrone(int id)
        {
            IBL.BO.DroneToList d = Drones.Find(x => x.IdNumber == id);
            if (d == null)
                throw new ConnectionException("the drone is not existing");
            if (d.StateDrone != StateDrone.available)
                throw new ConnectionException("the drone is not available");
            if (p.State != State.pick)
                throw new ConnectionException("the parcel is not picking");
            d.Current =//by distance
                  d.Battery -= distance*//תלוי במשקל החבילה
            Parcel.supplying = DateTime.Now;
            d.Current =p.current;//לא בדקנו עדיין איך משיגים מיקום של חבילה
            d.StateDrone = available;
            p.Arrivingtime = DateTime.Now;
            dal.UpdateDrone(d);
            dal.UpdateParcel(p);
       
        }
        #endregion
    }

}
