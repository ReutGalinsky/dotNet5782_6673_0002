using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using BO;
using System.Runtime.CompilerServices;
namespace BL
{
    internal partial class BL : BLApi.IBL
    {


        #region DroneToCharging
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DroneToCharging(string droneId)
        {
            BO.DroneToList drone = Drones.FirstOrDefault(x => x.IdNumber == droneId);
            if(drone==null)
                throw new UpdatingException($"the drone with the id:{droneId} is not existing");
            if (drone.State != DroneState.Available)
                throw new ChargingException($"the drone with the id:{droneId} is not available");

            lock (dal)
            {
                var closestStation = dal.GetAllBaseStationsBy(x => x.ChargeSlots > 0)
                .Select(s => new { station = s, distance = drone.Location.DistanceTo(s.GetLocation()) })
                .OrderBy(s => s.distance)
                .FirstOrDefault();

                var station = closestStation.station;

                if (closestStation == null)
                    throw new Exception("no station available");

                double tempBattery = drone.Battery;
                tempBattery -= (_available * closestStation.distance);
                if (tempBattery < 0)
                    throw new ChargingException($"there is not enough battery in the drone with the id:{droneId} to get the closest base station");
                drone.Battery = tempBattery;

                try
                {
                    drone.State = DroneState.maintaince;
                    drone.Location = closestStation.station.GetLocation();
                    station.ChargeSlots--;
                    dal.UpdateBaseStation(station);
                    DO.DroneCharge charge = new DroneCharge() { DroneId = droneId, StationId = station.IdNumber, startCharging = DateTime.Now };
                    dal.AddDroneCharge(charge);
                }
                catch (Exception e)
                {
                    throw new ChargingException($"the drone with the id:{ droneId } can't Charge", e);
                }
            }
        }
        #endregion

        #region DroneFromCharging
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DroneFromCharging(string droneID)
        {
            BO.DroneToList drone = Drones.FirstOrDefault(x => x.IdNumber == droneID);

            if (drone == null)
                throw new ChargingException($"the drone with the id:{ droneID } is not existing");
            if (drone.State != DroneState.maintaince)
                throw new ChargingException($"the drone with the id:{ droneID } was not charged");

            try
            {
                lock (dal)
                {
                    var chargeDrone = dal.GetDroneCharge(droneID);
                    DO.BaseStation station = dal.GetBaseStation(dal.GetDroneCharge(droneID).StationId);
                    dal.DeleteDroneCharge(droneID);
                    TimeSpan? charging = DateTime.Now - chargeDrone.startCharging;
                    station.ChargeSlots++;
                    dal.UpdateBaseStation(station);
                    drone.Battery += (int)(((float)(charging.Value.TotalSeconds) / 3600) * _speed);
                    if (drone.Battery > 100) drone.Battery = 100;
                    drone.State = DroneState.Available;
                }
            }
            catch (Exception e) { throw new DeletingException($"can't releasing the drone with the id:{ droneID } from Charge", e); }
        }
        #endregion
    }
}