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
        #region MatchingParcelToDrone
        public void MatchingParcelToDrone(string id)
        {

            IBL.BO.DroneToList d = Drones.FirstOrDefault(x => x.IdNumber == id);
            if (d == null)
                throw new ConnectionException("the drone is not existing");
            if (d.State != DroneState.Available)
                throw new ConnectionException("the drone is not available");
            var tem = GetParcelsNotMatching();
            var list = from item in tem
                       where (int)item.Weight <= (int)d.MaxWeight
                       orderby (int)item.Priority, (int)item.Weight, DistanceTo(GetCustomer(item.Sender).Local, d.Current)
                       select item;
            foreach (var item in list)
            {
                double temp = d.Battery;
                temp -= DistanceTo(GetCustomer(item.Sender).Local, d.Current) * availible;
                temp = item.Weight switch
                {
                    IBL.BO.WeightCategories.Heavy => temp - heavy * DistanceTo(GetCustomer(item.Sender).Local, GetCustomer(item.Geter).Local),
                    IBL.BO.WeightCategories.Light => temp - light * DistanceTo(GetCustomer(item.Sender).Local, GetCustomer(item.Geter).Local),
                    IBL.BO.WeightCategories.Middle => temp - medium * DistanceTo(GetCustomer(item.Sender).Local, GetCustomer(item.Geter).Local),
                };
                IDAL.DO.BaseStation b = ClosestStation(GetCustomer(item.Geter).Local);
                temp -= DistanceTo(new Location() { Latitude = b.Latitude, Longitude = b.Longitude }, GetCustomer(item.Geter).Local) * availible;
                if (temp >= 0)
                {
                    d.State = DroneState.shipping;
                    d.NumberOfParcel = item.IdNumber;
                    IDAL.DO.Parcel P = dal.GetParcel(item.IdNumber);
                    P.DroneId = d.IdNumber;
                    P.MatchForDroneTime = DateTime.Now;
                    dal.UpdateParcel(P);
                    return;
                }
            }
                throw new UpdatingException("cant find proper parcel");

                //if ((int)item.Priority > (int)win.Priority)
                //    win = item;
                //else
                //{
                //    if ((int)item.Priority != (int)win.Priority) continue;
                //    if ((int)item.Weight > (int)win.Weight && (int)d.MaxWeight >= (int)item.Weight)
                //        win = item;
                //    else
                //    {
                //        if ((int)item.Weight != (int)win.Weight) continue;
                //        else
                //        {
                //            Location locationItemSender = new Location() { Latitude = dal.GetCustomer((item.Sender)).Latitude, Longitude = dal.GetCustomer((item.Sender)).Longitude };
                //            Location locationWinSender = new Location() { Latitude = dal.GetCustomer((win.Sender)).Latitude, Longitude = dal.GetCustomer((win.Sender)).Longitude };
                //            if (!(DistanceTo(locationItemSender, d.Current) <= DistanceTo(locationWinSender, d.Current)))
                //            {
                //                continue;
                //            }
                //            double temp = d.Battery;
                //            double TotalShipping = DistanceTo(new Location() { Latitude = dal.GetCustomer((item.Geter)).Latitude, Longitude = dal.GetCustomer((item.Geter)).Longitude }, locationItemSender);
                //            switch (d.MaxWeight)
                //            {
                //                case IBL.BO.WeightCategories.Heavy:
                //                    temp -= TotalShipping * heavy;
                //                    break;
                //                case IBL.BO.WeightCategories.Light:
                //                    temp -= TotalShipping * light;
                //                    break;
                //                case IBL.BO.WeightCategories.Middle:
                //                    temp -= TotalShipping * medium;
                //                    break;
                //                default:
                //                    break;
                //            }
                //            //temp = d.MaxWeight switch
                //            //{
                //            //    IBL.BO.WeightCategories.Heavy => temp - TotalShipping * heavy,
                //            //    IBL.BO.WeightCategories.Light => temp - TotalShipping * light,
                //            //    IBL.BO.WeightCategories.Middle => temp - TotalShipping * medium,
                //            //    _ => throw new UpdatingException("Weight is invalid"),
                //            //};
                            //IDAL.DO.BaseStation help = ClosestStation(d.Current);
                            //locationWinSender.Latitude = help.Latitude;
                            //locationWinSender.Longitude = help.Longitude;
                            //TotalShipping = DistanceTo(locationWinSender, new Location() { Latitude = dal.GetCustomer((item.Geter)).Latitude, Longitude = dal.GetCustomer((item.Geter)).Longitude });
                            //temp -= TotalShipping * availible;
                            //if (temp <= 0)//להמשיך את הפונקציה הזו
                            //    continue;
                            //else
                            //{
                            //    win = item;
                            //    flag = true;
                            //}
                //        }
                //    }
                //}
            //}
            //if (!flag)
            //{
            //    throw new UpdatingException("can't match to any parcel");
            //}
            //d.State = DroneState.shipping;
            //d.NumberOfParcel = win.IdNumber;
            //IDAL.DO.Parcel p = dal.GetParcel(win.IdNumber);
            //p.MatchForDroneTime = DateTime.Now;
            //p.DroneId = d.IdNumber;
            ////לבדוק שהרחפן עודכן
            //dal.UpdateParcel(p);
        }
        #endregion

        #region PickingParcelByDrone
        public void PickingParcelByDrone(string id)
        {
            IBL.BO.DroneToList d = Drones.Find(x => x.IdNumber == id);
            if (d == null)
                throw new ConnectionException("the drone is not existing");
            IBL.BO.Parcel p = GetParcel(d.NumberOfParcel);
            if (d.State != DroneState.shipping)
                throw new ConnectionException("the drone is not shipping");
            if (!(p.MatchForDroneTime != default(DateTime) && p.collectingDroneTime == default(DateTime)))
                throw new ConnectionException("the parcel is not match");
            d.Battery -= DistanceTo(GetCustomer(p.SenderCustomer.IdNumber).Local, d.Current) * availible;
            d.Current = (Location)GetCustomer(p.SenderCustomer.IdNumber).Local.CopyPropertiesToNew(typeof(Location));//לא בדקנו עדיין איך משיגים מיקום של חבילה
            IDAL.DO.Parcel dparcel = dal.GetParcel(p.IdNumber);
            dparcel.collectingDroneTime = DateTime.Now;
            dal.UpdateParcel(dparcel);
        }
        #endregion

        #region SupplyingParcelByDrone
        public void SupplyingParcelByDrone(string id)
        {
            IBL.BO.DroneToList d = Drones.Find(x => x.IdNumber == id);
            if (d == null)
                throw new ConnectionException("the drone is not existing");
            if (d.State != DroneState.Available)
                throw new ConnectionException("the drone is not available");
            IBL.BO.Parcel p = GetParcel(d.NumberOfParcel);
            if (!(p.collectingDroneTime != default(DateTime) && p.ArrivingDroneTime == default(DateTime)))
                throw new ConnectionException("the parcel is not picking");
            d.Battery = p.Weight switch
            {
                IBL.BO.WeightCategories.Heavy => d.Battery - (DistanceTo(GetCustomer(p.GeterCustomer.IdNumber).Local, d.Current)) * heavy,
                IBL.BO.WeightCategories.Light => d.Battery - (DistanceTo(GetCustomer(p.GeterCustomer.IdNumber).Local, d.Current)) * light,
                IBL.BO.WeightCategories.Middle => d.Battery - (DistanceTo(GetCustomer(p.GeterCustomer.IdNumber).Local, d.Current)) * medium,
                _ => throw new UpdatingException("invalid Weight"),
            };
            d.Current = (Location)GetCustomer(p.GeterCustomer.IdNumber).Local.CopyPropertiesToNew(typeof(Location));//לא בדקנו עדיין איך משיגים מיקום של חבילה
            p.ArrivingDroneTime = DateTime.Now;
            d.State = DroneState.Available;
            //לבדוק שהרחפן מתעדכן באמת
            IDAL.DO.Parcel dparcel = dal.GetParcel(p.IdNumber);
            dparcel.ArrivingDroneTime = DateTime.Now;
            dal.UpdateParcel(dparcel);
        }
        #endregion
    }

}
