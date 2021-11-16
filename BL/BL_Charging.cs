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
    class BL_Charging
    {
        #region DroneToCharging
        public void DroneToCharging(int number)
        {
            IBL.BO.DroneToList d = Drones.Find(x => x.IdNumber == number);
            if (d == null)
                throw new ChargingException("the drone is not existing");
            if (d.State !=State.Define )
                throw new ChargingException("the drone is not define");
            //להוריד עמדה פנויה
            // עמדת בסיס קרובה 
            if (d.Battery<=40)
                throw new ChargingException("not enough battery");
            //מצב סוללה חדש
            d.State = State.supply;
            IDAL.DO.DroneCharge DroneInCharge = new IDAL.DO.DroneCharge() { DroneId = d.IdNumber, StationId=, };
            try
            {
               dal. (DroneInCharge);
            }
            catch (Exception e)
            {
                throw new UpdatingException("can't update the drone Charge", e);
            }

        }
    }

    #endregion
    #region DroneFromCharging
    public void DroneFromCharging(int number, DateTime charging)
    {
        IBL.BO.DroneToList d = Drones.Find(x => x.IdNumber == number);
        if (d == null)
            throw new ChargingException("the drone is not existing");
        if (d.State != State.supply)
            throw new ChargingException("the drone is not supply");
        d.Battery =//על פי זמן ששהה בטעינה
        d.State = State.supply;
        //להעלות עמדה פנויה

        IDAL.DO.DroneCharge DroneInCharge = new IDAL.DO.DroneCharge() { DroneId = d.IdNumber, StationId =, };
        try
        {
            dal.delete (DroneInCharge);
        }
        catch (Exception e)
        {
            throw new DeletingException("can't delete the drone Charge", e);
        }

    }
}

        #endregion

}
