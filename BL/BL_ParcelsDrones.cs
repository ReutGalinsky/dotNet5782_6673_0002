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
    public partial class BL
    {
        #region MatchingParcelToDrone
        public void MatchingParcelToDrone(int id)
        {
            IBL.BO.DroneToList d = Drones.Find(x => x.IdNumber == id);
            if (d == null)
                throw new ConnectionException("the drone is not existing");
            if (d.State != DroneState.Available)
                throw new ConnectionException("the drone is not available");
            var list = GetParcelsNotMatching().ToList();
            IBL.BO.ParcelOfList win = list.First();
            foreach (var item in list)
            {
                if ((int)item.Priority > (int)win.Priority)
                    win = item;
                else
                {
                    if ((int)item.Priority != (int)win.Priority) continue;
                    if ((int)item.Weight > (int)win.Weight && (int)d.MaxWeight >= (int)item.Weight)
                        win = item;
                    else
                    {
                        if ((int)item.Weight != (int)win.Weight) continue;
                        else
                        {
                            Location a1 = new Location() { Latitude = dal.GetCustomer(int.Parse(item.ClientSendName)).Latitude, Longitude = dal.GetCustomer(int.Parse(item.ClientSendName)).Longitude };
                            Location a2 = new Location() { Latitude = dal.GetCustomer(int.Parse(win.ClientSendName)).Latitude, Longitude = dal.GetCustomer(int.Parse(win.ClientSendName)).Longitude };
                            if (DistanceTo(a1, d.Current) < DistanceTo(a2, d.Current))
                            {
                                Location a3 = new Location() { Latitude = dal.GetCustomer(int.Parse(item.ClientGetName)).Latitude, Longitude = dal.GetCustomer(int.Parse(item.ClientGetName)).Longitude };
                                if (d.Battery -= () >= 0)//להמשיך את הפונקציה הזו
                                    win = item;
                            }
                        }
                    }
                }
            }
            d.State = DroneState.shipping;
            d.NumberOfParcel = win.IdNumber;
            IDAL.DO.Parcel p = dal.GetParcel(win.IdNumber);
            p.MatchForDroneTime = DateTime.Now;
            p.DroneId = d.IdNumber;
            //לבדוק שהרחפן עודכן
            dal.UpdateParcel(p);
        }
        #endregion

        #region PickingParcelByDrone
        public void PickingParcelByDrone(int id)
        {
            IBL.BO.DroneToList d = Drones.Find(x => x.IdNumber == id);
            if (d == null)
                throw new ConnectionException("the drone is not existing");
            IBL.BO.Parcel p = GetParcel(d.NumberOfParcel);
            if (d.State != DroneState.shipping)
                throw new ConnectionException("the drone is not shipping");
            if (!(p.MatchForDroneTime != default(DateTime) && p.collectingDroneTime == default(DateTime)))
                throw new ConnectionException("the parcel is not match");
            d.Battery -=DistanceTo(GetCustomer(p.Sender.IdNumer).Local,d.Current) * availible;
            d.Current = (Location)GetCustomer(p.Sender.IdNumer).Local.CopyPropertiesToNew(typeof(Location));//לא בדקנו עדיין איך משיגים מיקום של חבילה
            IDAL.DO.Parcel dparcel = dal.GetParcel(p.IdNumber);
            dparcel.collectingDroneTime = DateTime.Now;
            dal.UpdateParcel(dparcel);
        }
        #endregion

        #region SupplyingParcelByDrone
        public void SupplyingParcelByDrone(int id)
        {
            IBL.BO.DroneToList d = Drones.Find(x => x.IdNumber == id);
            if (d == null)
                throw new ConnectionException("the drone is not existing");
            if (d.State != DroneState.Available)
                throw new ConnectionException("the drone is not available");
            IBL.BO.Parcel p = GetParcel(d.NumberOfParcel);
            if (!(p.collectingDroneTime!=default(DateTime)&&p.ArrivingDroneTime==default(DateTime)))
                throw new ConnectionException("the parcel is not picking");
            d.Battery -= (DistanceTo(GetCustomer(p.Geter.IdNumer).Local, d.Current))*1;////;//תלוי במשקל!!
            d.Current = (Location)GetCustomer(p.Geter.IdNumer).Local.CopyPropertiesToNew(typeof(Location));//לא בדקנו עדיין איך משיגים מיקום של חבילה
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
