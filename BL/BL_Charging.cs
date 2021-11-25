﻿using System;
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
        //function that calculate the distance between two locations in km
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
        //sent drone to charging
        public void DroneToCharging(string number)
        {
            IBL.BO.DroneToList d = Drones.FirstOrDefault(x => x.IdNumber == number);
            IBL.BO.Drone droneBO = null;
            try { droneBO = GetDrone(number); }//get from DAL
            catch (Exception ) {
                throw new ChargingException("the drone is not existing"); }
            if (droneBO.State != DroneState.Available)
                throw new ChargingException("the drone is not available");
            var ListOfStation = from item in dal.GetBaseStations()
            where(item.ChargeSlots > 0) select(item);//all the base stations with availible charge slot
            if (ListOfStation.Count()==0)
                throw new ChargingException("there is no availible base station for charging");
            IDAL.DO.BaseStation b = ListOfStation.First();
            foreach (var item in ListOfStation)//find the closest base station with availible charge slot
                if (DistanceTo(new Location() { Latitude = b.Latitude,Longitude = b.Longitude },d.Location) > DistanceTo(new Location() { Latitude = item.Latitude,Longitude = item.Longitude },droneBO.Location))
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
        //function that release drone from charging
        public void DroneFromCharging(string number, TimeSpan charging)
        {
            IBL.BO.DroneToList d = Drones.FirstOrDefault(x => x.IdNumber == number);
            if (d == null)
                throw new ChargingException("the drone is not existing");
            if (d.State != DroneState.maintaince)
                throw new ChargingException("the drone was not charged");
            try
            {
                IDAL.DO.BaseStation station=dal.GetBaseStation(dal.GetDroneCharge(number).StationId);
                dal.DeleteDroneCharge(number);//delete from DAL
                station.ChargeSlots++;
                dal.UpdateBaseStation(station);//Updateing in DAL
                d.Battery += (int)(((float)(charging.TotalSeconds) / 60) * speed);
                if (d.Battery > 100) d.Battery = 100;
                d.State = DroneState.Available;
            }
            catch (Exception e)
            {
                throw new DeletingException("can't delete the drone Charge", e);
            }
        }
        #endregion
    }



}
