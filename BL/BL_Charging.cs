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
            if (d.DroneState!= DroneState.Available )
                throw new ChargingException("the drone is not available");

            var ListOfStation = GetBaseStationsWithCharge();
            BaseStationToList = ListOfStation.GetTop();
            foreach (var item in ListOfStation) //לכתוב פונקית חישוב מרחקים
                if (ListOfStation.distance < BaseStationToList)
                    BaseStationToList = item;
            if (d.Battery<=40)
                throw new ChargingException("not enough battery");
            
            d.Battery -= d.available* distance; //להמציא פונקצית מרחק
            if (d.Battery < 0)
                throw new ChargingException("not enough battery");
            try
            {
               dal.Func(DroneInCharge);//פונקציה של אחים שלך
                d.local.Longitude = BaseStationToList.Longitude;
                d.local.Latitude = BaseStationToList.Latitude;
                dal.AddDroneCharge(number);
            }
            catch (Exception e)
            {
                throw new ChargingException ("can't Charge", e);
            }

        }
    }

    #endregion
    #region DroneFromCharging
    //להוסיף גם DATATIME בטעינת רחפן
    public void DroneFromCharging(int number, DateTime charging)
    {
        IBL.BO.DroneToList d = Drones.Find(x => x.IdNumber == number);
        if (d == null)
            throw new ChargingException("the drone is not existing");
        if (d.StateDrone != StateDrone.Maintaince)
            throw new ChargingException("the drone was not charged");
        d.Battery += functiontime; //פונקציה שנכין בכוחות עצמנו
        d.StateDrone = d.StateDrone.available;
        try
        {
         dal.DeleteDroneCharge(number);
        }
        catch (Exception e)
        {
            throw new DeletingException("can't delete the drone Charge", e);
        }

    }
}

        #endregion

}
