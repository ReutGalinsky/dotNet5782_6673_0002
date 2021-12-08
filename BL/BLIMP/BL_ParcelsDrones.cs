using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using BO;

namespace BL
{
    internal partial class BL : BLApi.IBL
    {
        #region MatchingParcelToDrone
        public void MatchingParcelToDrone(string id)
        //match parcel to a proper drone
        {

            BO.DroneToList d = Drones.FirstOrDefault(x => x.IdNumber == id);
            BO.Drone droneBO = null;
            try { droneBO = GetDrone(id); }
            catch (Exception e)
            {
                throw new ConnectionException("the drone is not existing");
            }
            if (d.State != DroneState.Available)
                throw new ConnectionException("the drone is not available");
            var list = from item in PredicateParcel(x => x.State == State.Define && (int)x.Weight <= (int)d.MaxWeight)
                       orderby -1 * (int)item.Priority, -1 * (int)item.Weight, DistanceTo(GetCustomer(item.Sender).Location, d.Location)
                       //decending order by priority and weight and increasing by distance
                       select item;
            foreach (var item in list)
            {
                int temp = d.Battery;
                temp -= (int)(DistanceTo(GetCustomer(item.Sender).Location, d.Location) * availible);
                temp = item.Weight switch//check if the battery is enough for the delivery
                {
                    BO.WeightCategories.Heavy => (int)(temp - heavy * DistanceTo(GetCustomer(item.Sender).Location, GetCustomer(item.Geter).Location)),
                    BO.WeightCategories.Light => (int)(temp - light * DistanceTo(GetCustomer(item.Sender).Location, GetCustomer(item.Geter).Location)),
                    BO.WeightCategories.Middle => (int)(temp - medium * DistanceTo(GetCustomer(item.Sender).Location, GetCustomer(item.Geter).Location)),
                };
                DO.BaseStation b = ClosestStation(GetCustomer(item.Geter).Location);
                temp -= (int)(DistanceTo(new Location() { Latitude = b.Latitude, Longitude = b.Longitude }, GetCustomer(item.Geter).Location) * availible);
                if (temp >= 0)
                {
                    d.State = DroneState.shipping;
                    d.NumberOfParcel = item.IdNumber;
                    DO.Parcel P = dal.GetParcel(item.IdNumber);
                    P.DroneId = d.IdNumber;
                    P.MatchForDroneTime = DateTime.Now;
                    dal.UpdateParcel(P);//update in DAL
                    return;
                }
            }
            throw new UpdatingException("cant find proper parcel for this drone");//didn't find parcel
        }
        #endregion

        #region PickingParcelByDrone
        //function for Collecting parcel from sender customer by a drone
        public void PickingParcelByDrone(string id)
        {
            BO.DroneToList d = Drones.FirstOrDefault(x => x.IdNumber == id);
            BO.Drone droneBO = null;
            try { droneBO = GetDrone(id); }//get the drone from DAL
            catch (Exception e)
            {
                throw new ConnectionException("the drone is not existing");
            }
            BO.Parcel p = null;
            try
            {
                p = GetParcel(droneBO.PassedParcel.IdNumber);
            }
            catch (Exception e)
            {
                throw new UpdatingException("there is not any parcel that match this drone", e);
            }
            if (d.State != DroneState.shipping)
                throw new ConnectionException("the drone is not shipping");
            if (!(p.MatchForDroneTime != null && p.CollectingDroneTime == null))
                throw new ConnectionException("the parcel is not match");
            d.Battery -= (int)(DistanceTo(GetCustomer(p.SenderCustomer.IdNumber).Location, d.Location) * availible);//update the Battery
            d.Location = (Location)GetCustomer(p.SenderCustomer.IdNumber).Location.CopyPropertiesToNew(typeof(Location));
            DO.Parcel dparcel = dal.GetParcel(p.IdNumber);
            dparcel.CollectingDroneTime = DateTime.Now;
            dal.UpdateParcel(dparcel);//update in DAL
        }
        #endregion

        #region SupplyingParcelByDrone
        public void SupplyingParcelByDrone(string id)
        //function that supply the parcel to the destination
        {
            BO.DroneToList d = Drones.FirstOrDefault(x => x.IdNumber == id);
            BO.Drone droneBO = null;
            try
            {
                droneBO = GetDrone(id);
            }
            catch (Exception e)
            {
                throw new ConnectionException("the drone is not existing");
            }
            if (droneBO.State != DroneState.shipping)
                throw new ConnectionException("the drone is not shipping");
            BO.Parcel p = GetParcel(droneBO.PassedParcel.IdNumber);
            if (!(p.CollectingDroneTime != null && p.ArrivingDroneTime == null))
                throw new ConnectionException("the parcel is not picking yet");
            d.Battery = p.Weight switch//update the battery
            {
                BO.WeightCategories.Heavy => (int)(d.Battery - (DistanceTo(GetCustomer(p.GeterCustomer.IdNumber).Location, d.Location)) * heavy),
                BO.WeightCategories.Light => (int)(d.Battery - (DistanceTo(GetCustomer(p.GeterCustomer.IdNumber).Location, d.Location)) * light),
                BO.WeightCategories.Middle => (int)(d.Battery - (DistanceTo(GetCustomer(p.GeterCustomer.IdNumber).Location, d.Location)) * medium),
                _ => throw new UpdatingException("invalid Weight"),
            };
            d.Location = (Location)GetCustomer(p.GeterCustomer.IdNumber).Location.CopyPropertiesToNew(typeof(Location));
            p.ArrivingDroneTime = DateTime.Now;
            d.State = DroneState.Available;
            try
            {
                DO.Parcel dparcel = dal.GetParcel(p.IdNumber);
                dparcel.ArrivingDroneTime = DateTime.Now;
                dal.UpdateParcel(dparcel);//update in DAL
            }
            catch (Exception e)
            {
                throw new ConnectionException("the parcel is not existing");
            }
        }
        #endregion
    }

}