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
        private static double distance(Location a, Location b)
        {
          double d1=a.Latitude-b.Latitude;
          double d2=a.Longitude-b.Longitude;
          double distance= Math.Sqrt(Math.Pow(d1,2)+Math.Pow(d2,2));
          return 111*distance; //לבדוק שזה 111
        }
        #region DroneToCharging
        public void DroneToCharging(int number)
        {
            IBL.BO.DroneToList d = Drones.Find(x => x.IdNumber == number);
            if (d == null)
                throw new ChargingException("the drone is not existing");
            if (d.State != DroneState.Available)
                throw new ChargingException("the drone is not available");
            var ListOfStation = dal.GetBaseStations().Where(x => x.ChargeSlots > 0).Select(x => (IBL.BO.BaseStation)x.CopyPropertiesToNew(typeof(IBL.BO.BaseStation)));
            if (ListOfStation.Empty())
                throw new ChargingException("there is no base to charging");
            BaseStation b = ListOfStation.First();
            foreach (var item in ListOfStation) 
                if (distance(b.Local) > distance(item.Local))
                    b = item;
            double temp = d.Battery;
            temp -= availible *distance(b.Local);
            if (temp < 0)
                throw new ChargingException("not enough battery");
            d.Battery = temp;
            try
            {
                d.State=DroneState.maintaince;
                d.local.Longitude = BaseStationToList.Longitude;
                d.local.Latitude = BaseStationToList.Latitude;
               IDAL.DO.BaseStation station=dal.GetBaseStation(b.IdNumber);
               station.ChargeSlot--;
               dal.AddDroneCharge(number);
               dal.UpdateBaseStation(station);
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
                IDAL.DO.BaseStation station=dal.GetBaseStation (b.IdNumber);
                station.chargeSlot++;

            }
            catch (Exception e)
            {
                throw new DeletingException("can't delete the drone Charge", e);
            }

        }
        #endregion
    }



}
