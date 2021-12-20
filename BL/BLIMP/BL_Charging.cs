using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using BO;

namespace BL
{
    /// <summary>
    /// all possible actions charging drones 
    /// </summary>
    internal partial class BL : BLApi.IBL
    {


        #region DroneToCharging
        /// <summary>
        /// sending drone to charging
        /// </summary>
        public void DroneToCharging(string droneId)
        {
            //intialize
            BO.DroneToList drone = Drones.FirstOrDefault(x => x.IdNumber == droneId);
            BO.Drone droneBO;
            droneBO = GetDrone(droneId);

            //validation
            if (droneBO.State != DroneState.Available)
                throw new ChargingException($"the drone with the id:{droneId} is not available");


            //find closest avilable station 
            var closestStation = dal.GetAllBaseStationsBy(x => x.ChargeSlots > 0)
                .Select(s => new { station = s, distance = drone.Location.DistanceTo(s.GetLocation())})
                .OrderBy(s => s.distance)
                .FirstOrDefault();

            var station = closestStation.station;

            if (closestStation == null)
                throw new Exception("no station available"); 
            
            //check battery availabilty
            int tempBattery = drone.Battery;
            tempBattery -= (int)(_available * closestStation.distance);
            if (tempBattery < 0)
                throw new ChargingException($"there is not enough battery in the drone with the id:{droneId} to get the closest base station");
            
            //update battery
            drone.Battery = tempBattery;
            
            //update dal
            try
            {
                drone.State = DroneState.maintaince;
                drone.Location = closestStation.station.GetLocation();
                station.ChargeSlots--;
                dal.UpdateBaseStation(station);
                DO.DroneCharge charge = new DroneCharge() { DroneId = droneId, StationId = station.IdNumber, startCharging=DateTime.Now };
                dal.AddDroneCharge(charge);
            }
            catch (Exception e)
            { throw new ChargingException($"the drone with the id:{ droneId } can't Charge", e);}
        }
        #endregion

        #region DroneFromCharging
        /// <summary>
        /// releasing drone from charging
        /// </summary>
        public void DroneFromCharging(string droneID)
        {
            //initialization
            BO.DroneToList drone = Drones.FirstOrDefault(x => x.IdNumber == droneID);
            
            //validation
            if (drone == null)
                throw new ChargingException($"the drone with the id:{ droneID } is not existing");
            if (drone.State != DroneState.maintaince)
                throw new ChargingException($"the drone with the id:{ droneID } was not charged");
            
            //update
            try
            {
                var chargeDrone = dal.GetDroneCharge(droneID);
                DO.BaseStation station = dal.GetBaseStation(dal.GetDroneCharge(droneID).StationId);
                dal.DeleteDroneCharge(droneID);
                TimeSpan? charging = DateTime.Now - chargeDrone.startCharging;
                station.ChargeSlots++;
                dal.UpdateBaseStation(station);
                drone.Battery += (int)(((float)(charging.Value.TotalSeconds) / 60) * _speed);
                if (drone.Battery > 100) drone.Battery = 100;
                drone.State = DroneState.Available;
            }
            catch (Exception e) {throw new DeletingException($"can't releasing the drone with the id:{ droneID } from Charge", e);}
        }
        #endregion
    }
}