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
        private static double distance()//לממש
        { return 0; }
        #region DroneToCharging
        public void DroneToCharging(int number)
        {
            IBL.BO.DroneToList d = Drones.Find(x => x.IdNumber == number);
            if (d == null)
                throw new ChargingException("the drone is not existing");
            if (d.State != DroneState.Available)
                throw new ChargingException("the drone is not available");
            var ListOfStation = dal.GetBaseStations().Where(x => x.ChargeSlots > 0).Select(x => (IBL.BO.BaseStationToList)x.CopyPropertiesToNew(typeof(IBL.BO.BaseStationToList)));
            BaseStationToList b = ListOfStation.First();
            foreach (var item in ListOfStation) //לכתוב פונקית חישוב מרחקים
                if (b.distance > item.distance)
                    b = item;
            double temp = d.Battery;
            temp -= availible * b.distance(); //להמציא פונקצית מרחק
            if (temp < 0)
                throw new ChargingException("not enough battery");
            d.Battery = temp;
            try//לסיים! ולחשוב מה עוד צריך לעדכן פה
            {
                dal.Func(DroneInCharge);//פונקציה של אחים שלך
                d.local.Longitude = BaseStationToList.Longitude;
                d.local.Latitude = BaseStationToList.Latitude;
                dal.UpdateDrone
                dal.AddDroneCharge(number);
            }
            catch (Exception e)
            {
                throw new ChargingException("can't Charge", e);
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
            if (d.State != DroneState.maintaince)
                throw new ChargingException("the drone was not charged");
            d.Battery += functiontime; //פונקציה שנכין בכוחות עצמנו
            d.State = DroneState.Available;
            try
            {
                dal.DeleteDroneCharge(number);
            }
            catch (Exception e)
            {
                throw new DeletingException("can't delete the drone Charge", e);
            }

        }
        #endregion
    }



}
