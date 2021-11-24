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
    public partial class BL : IBL.IBL
    {

        #region DistanceTo
        private static double DistanceTo(Location a1,Location a2)
        {
            double rlat1 = Math.PI * a1.Latitude / 180;
            double rlat2 = Math.PI * a2.Latitude / 180;
            double theta = a1.Longitude - a2.Longitude;
            double rtheta = Math.PI * theta / 180;
            double dist =
                Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
                Math.Cos(rlat2) * Math.Cos(rtheta);
            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;
            return dist * 1.609344;
        }
        #endregion  

        #region DroneToCharging
        public void DroneToCharging(string number)
        {
            IBL.BO.DroneToList d = Drones.FirstOrDefault(x => x.IdNumber == number);
            if (d == null)
                throw new ChargingException("the drone is not existing");
            if (d.State != DroneState.Available)
                throw new ChargingException("the drone is not available");
            var ListOfStation = from item in dal.GetBaseStations()
            where(item.ChargeSlots > 0) select(item);
            if (ListOfStation.Count()==0)
                throw new ChargingException("there is no availible base station for charging");
            IDAL.DO.BaseStation b = ListOfStation.First();
            foreach (var item in ListOfStation)
                if (DistanceTo(new Location() { Latitude = b.Latitude,Longitude = b.Longitude },d.Location) > DistanceTo(new Location() { Latitude = item.Latitude,Longitude = item.Longitude },d.Location))
                    b = item;
            int temp = d.Battery;
            temp -=(int)( availible * DistanceTo(new Location() { Latitude = b.Latitude, Longitude = b.Longitude }, d.Location));
            if (temp < 0)
                throw new ChargingException("not enough battery to get the closest base station");
            d.Battery = temp;
            try
            {
                d.State = DroneState.maintaince;
                d.Location = new Location() { Latitude = b.Latitude, Longitude = b.Longitude };
                b.ChargeSlots--;
                dal.UpdateBaseStation(b);
                IDAL.DO.DroneCharge charge = new DroneCharge() { DroneId = number, StationId = b.IdNumber };
                dal.AddDroneCharge(charge);
            }
            catch (Exception e)
            {
                throw new ChargingException("can't Charge", e);
            }

        }


        #endregion

        #region DroneFromCharging
        public void DroneFromCharging(string number, TimeSpan charging)
        {
            IBL.BO.DroneToList d = Drones.FirstOrDefault(x => x.IdNumber == number);
            if (d == null)
                throw new ChargingException("the drone is not existing");
            if (d.State != DroneState.maintaince)
                throw new ChargingException("the drone was not charged");
            d.Battery += (int)(((float)(charging.TotalSeconds)/60)*speed); 
            d.State = DroneState.Available;
            try
            {
                IDAL.DO.BaseStation station=dal.GetBaseStation(dal.GetDroneCharge(number).StationId);
                dal.DeleteDroneCharge(number);
                station.ChargeSlots++;
                dal.UpdateBaseStation(station);

            }
            catch (Exception e)
            {
                throw new DeletingException("can't delete the drone Charge", e);
            }

        }
        #endregion
    }



}
