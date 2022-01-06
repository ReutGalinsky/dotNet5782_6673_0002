using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using System.Threading;
using static BL.BL;

namespace BL
{
    class DroneSimulator
    {
        private const double SPEED = 2;
        private const int DELAY = 500;
        private DroneToList drone;
        private Parcel parcel1;
        private Customer sender;
        private Customer geter;
        private Action reportProggress;

        private BL bl;
        public DroneSimulator(BL b, string id, Action a, Func<bool> f)
        {
            reportProggress = a;
            bl = b;
            lock (bl)
            {
                drone = bl.Drones.FirstOrDefault(x => x.IdNumber == id);
            }
            while (f() == false)
            {
                Thread.Sleep(DELAY);
                int state = findCurrentState(drone);
                switch (state)
                {
                    case 1://start shipping
                        startShipping();
                        break;
                    case 2://go to charge
                        goToCharge();
                        break;
                    case 3://during charging
                        startCharging();
                        break;
                    case 4://go to pick
                        startShipping();//ללשאול מה לעשות אם הסימולטור מתחיל באמצע הליכה לpick
                        break;
                    case 5://go to supply
                        startSupplying();
                        break;
                    default:
                        break;
                }
            }
        }
        private void startShipping()
        {
            lock (bl)
            {
                parcel1 = bl.GetParcel(drone.NumberOfParcel);
                sender = bl.GetCustomer(parcel1.SenderCustomer.IdNumber);
            }
            var distance = drone.Location.DistanceTo(sender.Location);
            while (distance > 0)
            {
                Thread.Sleep(DELAY);
                distance -= SPEED;
                drone.Battery -=bl._available;
                reportProggress();
            }
            lock(bl) lock (bl.dal)
            {
                drone.Location = (Location)sender.Location.CopyPropertiesToNew(typeof(Location));
                DO.Parcel parcelDO = bl.dal.GetParcel(parcel1.IdNumber);
                parcelDO.CollectingDroneTime = DateTime.Now;
                bl.dal.UpdateParcel(parcelDO);
            }
            reportProggress();

        }
        private void startSupplying()
        {
            lock (bl)
            {
                parcel1 = bl.GetParcel(drone.NumberOfParcel);
                geter = bl.GetCustomer(parcel1.GeterCustomer.IdNumber);
            }
            var distance = drone.Location.DistanceTo(geter.Location);
            double batteryDown;
            if (parcel1.Weight == WeightCategories.Heavy)
                batteryDown = bl._heavy;
            else if (parcel1.Weight == WeightCategories.Light)
                batteryDown = bl._light;
            else batteryDown = bl._medium;
            while (distance > 0)
            {
                Thread.Sleep(DELAY);
                distance -= SPEED;
                drone.Battery -= batteryDown;
                reportProggress();
            }
            drone.Location = (Location)geter.Location.CopyPropertiesToNew(typeof(Location));
            lock (bl) lock(bl.dal)
            {

                DO.Parcel parcelDO = bl.dal.GetParcel(parcel1.IdNumber);
                parcelDO.ArrivingDroneTime = DateTime.Now;
                drone.State = DroneState.Available;
                bl.dal.UpdateParcel(parcelDO);
            }
            reportProggress();
        }
        private void goToCharge()
        {lock (bl) lock (bl.dal)
                {
                    var closestStation = bl.dal.GetAllBaseStationsBy(x => x.ChargeSlots > 0)
                .Select(s => new { station = s, distance = drone.Location.DistanceTo(s.GetLocation()) })
                .OrderBy(s => s.distance)
                .FirstOrDefault();

                    if (closestStation != null)
                    {

                        drone.State = DroneState.maintaince;
                        var distance = closestStation.distance;
                        while (distance > 0)
                        {
                            Thread.Sleep(DELAY);
                            distance -= SPEED;
                            drone.Battery -=bl._available;
                            reportProggress();
                        }
                        drone.Location = closestStation.station.GetLocation();
                        var station = closestStation.station;
                        station.ChargeSlots--;
                        bl.dal.UpdateBaseStation(station);
                        DO.DroneCharge charge = new DO.DroneCharge() { DroneId = drone.IdNumber, StationId = station.IdNumber, startCharging = DateTime.Now };
                        bl.dal.AddDroneCharge(charge);
                        reportProggress();

                    }
                    else
                    {
                        var station = bl.dal.GetBaseStations()
                        .Select(s => new { station = s, distance = drone.Location.DistanceTo(s.GetLocation()) })
                        .OrderBy(s => s.distance)
                        .FirstOrDefault();
                        var distance = station.distance;
                        while (distance > 0)
                        {
                            Thread.Sleep(DELAY);
                            distance -= SPEED;
                            drone.Battery -= bl._available;
                        }
                        drone.Location = station.station.GetLocation();
                        reportProggress();

                    }
                }
        }
        private void startCharging()
        {
            while (drone.Battery < 100)
            {
                Thread.Sleep(DELAY);
                drone.Battery +=bl._speed;
                if (drone.Battery > 100) drone.Battery = 100;
                reportProggress();
            }
            lock (bl) lock (bl.dal)
                {
                    var chargeDrone = bl.dal.GetDroneCharge(drone.IdNumber);
                    DO.BaseStation station = bl.dal.GetBaseStation(bl.dal.GetDroneCharge(drone.IdNumber).StationId);
                    bl.dal.DeleteDroneCharge(drone.IdNumber);
                    station.ChargeSlots++;
                    bl.dal.UpdateBaseStation(station);
                    drone.State = DroneState.Available;
                }
            reportProggress();
        }
        private int findCurrentState(DroneToList d)
        {
            switch (d.State)
            {
                case DroneState.Available:
                    try
                    {
                        lock (bl)
                        {
                            bl.MatchingParcelToDrone(d.IdNumber);
                        }
                        return 1;
                    }
                    catch (Exception ex)
                    {
                        if (drone.Battery == 100) return 0;
                        return 2;
                    }
                case DroneState.maintaince:
                    return 3;
                case DroneState.shipping:
                    lock (bl)
                    {
                        var parcel = bl.GetParcel(d.NumberOfParcel);
                        if (parcel.CollectingDroneTime == null)
                            return 4;
                    }
                    return 5;
            }
            return 0;
        }
    }
}
