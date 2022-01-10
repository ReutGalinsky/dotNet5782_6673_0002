using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using BO;
using static BO.DroneState;
using static BO.ParcelState;
using System.Runtime.CompilerServices;
namespace BL
{
    internal partial class BL : BLApi.IBL
    {

        #region MatchingParcelToDrone
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void MatchingParcelToDrone(string droneId)
        {
            DroneToList drone = Drones.FirstOrDefault(x => x.IdNumber == droneId);

            if (drone.State != Available)
                throw new ConnectionException($"the drone with the id {droneId} is not available");

            var parcelQueue = GetAllParcelsBy(x => x.ParcelState == Define && (int)x.Weight <= (int)drone.MaxWeight)
                .OrderByDescending(p => (int)p.Priority)
                .ThenByDescending(p => (int)p.Weight)
                .ThenBy(p => dal.GetCustomer(p.Sender).GetLocation().DistanceTo(drone.Location));

            foreach (var parcel in parcelQueue)
            {
                var usagee = batteryUssage(drone, parcel);
                if (drone.Battery >= usagee)
                {
                    lock (dal)
                    {
                        drone.State = DroneState.shipping;
                        drone.NumberOfParcel = parcel.IdNumber;
                        DO.Parcel parcelDO = dal.GetParcel(parcel.IdNumber);
                        parcelDO.DroneId = drone.IdNumber;
                        parcelDO.MatchForDroneTime = DateTime.Now;
                        dal.UpdateParcel(parcelDO);
                    }
                    return;
                }
            }
            throw new UpdatingException($"cant find proper parcel for the drone with id {droneId}");//didn't find parcel
        }
        #endregion

        #region PickingParcelByDrone
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void PickingParcelByDrone(string id)
        {
            BO.DroneToList drone = Drones.FirstOrDefault(x => x.IdNumber == id);
            BO.Parcel parcel;

            try
            {
                parcel = GetParcel(drone.NumberOfParcel);
            }
            catch (Exception e)
            {
                throw new ConnectionException(e.Message, e);
            }
            if (drone.State != DroneState.shipping)
                throw new ConnectionException($"the drone with the id {id} is not shipping");
            if (!(parcel.MatchForDroneTime != null && parcel.CollectingDroneTime == null))
                throw new ConnectionException("the parcel is not match");
            lock (dal)
            {
                drone.Battery -= CalcutateBatteryPerDistance(drone.Location.DistanceTo(GetCustomer(parcel.SenderCustomer.IdNumber).Location), _available);
                drone.Location = GetCustomer(parcel.SenderCustomer.IdNumber).Location.GetLocation();
                DO.Parcel parcelDO = dal.GetParcel(parcel.IdNumber);
                parcelDO.CollectingDroneTime = DateTime.Now;
                dal.UpdateParcel(parcelDO);
            }
        }
        #endregion

        #region SupplyingParcelByDrone
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void SupplyingParcelByDrone(string id)
        {
            BO.DroneToList drone = Drones.FirstOrDefault(x => x.IdNumber == id);
            BO.Drone droneBO;
            try
            {
                droneBO = GetDrone(id);
            }
            catch (Exception e)
            {
                throw new ConnectionException(e.Message, e);
            }

            if (droneBO.State != DroneState.shipping)
                throw new ConnectionException($"the drone with the id {id} is not shipping");
            BO.Parcel parcel = GetParcel(droneBO.PassedParcel.IdNumber);
            if (!(parcel.CollectingDroneTime != null && parcel.ArrivingDroneTime == null))
                throw new ConnectionException("the parcel is not picking yet");

            try
            {
                lock (dal)
                {
                    drone.Location = GetCustomer(parcel.GeterCustomer.IdNumber).Location.GetLocation();
                    DO.Parcel dparcel = dal.GetParcel(parcel.IdNumber);
                    dparcel.ArrivingDroneTime = DateTime.Now;
                    dal.UpdateParcel(dparcel);
                    drone.Battery -= parcel.Weight switch
                    {
                        BO.WeightCategories.Heavy => CalcutateBatteryPerDistance(droneBO.PassedParcel.Distance, _heavy),
                        BO.WeightCategories.Light => (CalcutateBatteryPerDistance(droneBO.PassedParcel.Distance, _light)),
                        BO.WeightCategories.Middle => CalcutateBatteryPerDistance(droneBO.PassedParcel.Distance, _medium),
                        _ => throw new UpdatingException("invalid Weight"),
                    };
                    drone.NumberOfParcel = null;
                    drone.State = DroneState.Available;
                }
            }
            catch (Exception e)
            {
                throw new ConnectionException(e.Message, e);
            }
        }
        #endregion


        #region CalcutateBatteryPerDistance
        /// <summary>
        /// 
        /// </summary>
        /// <param name="distance">the distance</param>
        /// <param name="factorBattery">the percent of battery that reduces </param>
        /// <returns></returns>
        private double CalcutateBatteryPerDistance(double distance, double factorBattery)
        {
            return (distance * factorBattery);
        }
        #endregion
        #region batteryUssage
        private double batteryUssage(BO.DroneToList drone, BO.ParcelOfList parcel)
        {
            double battery = 0;
            BO.Customer sender = GetCustomer(parcel.Sender);
            BO.Customer geter = GetCustomer(parcel.Geter);
            DO.BaseStation baseStation = ClosestStation(GetCustomer(parcel.Geter).Location);
            battery = CalcutateBatteryPerDistance(drone.Location.DistanceTo(sender.Location) + geter.Location.DistanceTo(baseStation.GetLocation()), _available);
            battery += parcel.Weight switch
            {
                BO.WeightCategories.Heavy => CalcutateBatteryPerDistance(sender.Location.DistanceTo(geter.Location), _heavy),
                BO.WeightCategories.Light => CalcutateBatteryPerDistance(sender.Location.DistanceTo(geter.Location), _light),
                BO.WeightCategories.Middle => CalcutateBatteryPerDistance(sender.Location.DistanceTo(geter.Location), _medium),
                _ => throw new NotImplementedException(),
            };
            return battery;
        }
        #endregion
    }

}