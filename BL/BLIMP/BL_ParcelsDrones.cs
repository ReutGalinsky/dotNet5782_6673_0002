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
    /// <summary>
    /// all possible actions on parcels that connected with drones
    /// </summary> 
    internal partial class BL : BLApi.IBL
    {

        #region MatchingParcelToDrone
        /// <summary>
        ///matching parcel to a proper drone
        /// </summary> 
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void MatchingParcelToDrone(string droneId)
        {
            DroneToList drone = Drones.FirstOrDefault(x => x.IdNumber == droneId);
            BO.Drone droneBO;
            droneBO = GetDrone(droneId);

            if (drone.State != Available)
                throw new ConnectionException($"the drone with the id {droneId} is not available");

            var parcelQueue = GetAllParcelsBy(x => x.ParcelState == Define && (int)x.Weight <= (int)drone.MaxWeight)
                .OrderByDescending(p => (int)p.Priority)
                .ThenByDescending(p => (int)p.Weight)
                .ThenBy(p => dal.GetCustomer(p.Sender).GetLocation().DistanceTo(drone.Location));

            foreach (var parcel in parcelQueue)
            {
                var usagee = battarUseag(drone, parcel);
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

        private double calculateSomthing(double distance, double factorBattery)
        {
            return (distance * factorBattery);
        }

        private double battarUseag(BO.DroneToList drone, BO.ParcelOfList parcel)
        {
            double battery = 0;
            BO.Customer sender = GetCustomer(parcel.Sender);
            BO.Customer geter = GetCustomer(parcel.Geter);
            DO.BaseStation baseStation = ClosestStation(GetCustomer(parcel.Geter).Location);
            battery = calculateSomthing(drone.Location.DistanceTo(sender.Location) + geter.Location.DistanceTo(baseStation.GetLocation()), _available);
            battery += parcel.Weight switch
            {
                BO.WeightCategories.Heavy => calculateSomthing(sender.Location.DistanceTo(geter.Location), _heavy),
                BO.WeightCategories.Light => calculateSomthing(sender.Location.DistanceTo(geter.Location), _light),
                BO.WeightCategories.Middle =>calculateSomthing(sender.Location.DistanceTo(geter.Location), _medium),
                _ => throw new NotImplementedException(),
            };
            return battery;
        }

        #region PickingParcelByDrone
        /// <summary>
        ///Collecting parcel from sender customer by a drone
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void PickingParcelByDrone(string id)
        {
            //initialized
            BO.DroneToList drone = Drones.FirstOrDefault(x => x.IdNumber == id);
            BO.Drone droneBO;
            BO.Parcel parcel;

            //validation
            try
            {
                droneBO = GetDrone(id);
                parcel = GetParcel(droneBO.PassedParcel.IdNumber);
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

                //update
                drone.Battery -= calculateSomthing(drone.Location.DistanceTo(GetCustomer(parcel.SenderCustomer.IdNumber).Location), _available);
                //drone.Location = (Location)GetCustomer(parcel.SenderCustomer.IdNumber).Location.CopyPropertiesToNew(typeof(Location));
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
        /// <summary>
        ///function that supply the parcel to the destination
        /// </summary>
        {
            //initialized
            BO.DroneToList drone = Drones.FirstOrDefault(x => x.IdNumber == id);
            BO.Drone droneBO;
            try
            {
                droneBO = GetDrone(id);
            }
            catch (Exception e)
            {
                throw new ConnectionException(e.Message,e);
            }

            //validation
            if (droneBO.State != DroneState.shipping)
                throw new ConnectionException($"the drone with the id {id} is not shipping");
            BO.Parcel parcel = GetParcel(droneBO.PassedParcel.IdNumber);
            if (!(parcel.CollectingDroneTime != null && parcel.ArrivingDroneTime == null))
                throw new ConnectionException("the parcel is not picking yet");

            //update
           
            drone.Battery -= parcel.Weight switch//update the battery
            {
                BO.WeightCategories.Heavy => calculateSomthing(droneBO.PassedParcel.Distance, _heavy),
                BO.WeightCategories.Light => (calculateSomthing(droneBO.PassedParcel.Distance, _light)),
                BO.WeightCategories.Middle => calculateSomthing(droneBO.PassedParcel.Distance, _medium),
                _ => throw new UpdatingException("invalid Weight"),
            };
            drone.NumberOfParcel = null;
            drone.Location = GetCustomer(parcel.GeterCustomer.IdNumber).Location.GetLocation();
            parcel.ArrivingDroneTime = DateTime.Now;
            drone.State = DroneState.Available;
            try
            {
                lock (dal)
                {

                    DO.Parcel dparcel = dal.GetParcel(parcel.IdNumber);
                    dparcel.ArrivingDroneTime = DateTime.Now;
                    dal.UpdateParcel(dparcel);
                }
            }
            catch (Exception e)
            {
                throw new ConnectionException(e.Message,e);
            }
        }
        #endregion
    }

}