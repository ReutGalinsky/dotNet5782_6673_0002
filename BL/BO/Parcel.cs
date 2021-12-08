using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;

namespace BO
{
    /// <summary>
    /// parcel: including details about times, drone and customers
    /// </summary>
    public class Parcel
    {
        public string IdNumber { set; get; }
        public CustomerOfParcel SenderCustomer { set; get; }
        public CustomerOfParcel GeterCustomer { set; get; }
        public WeightCategories Weight { set; get; }
        public Priorities Priority { set; get; }
        public DroneInParcel Drone { set; get; }
        public DateTime? CreateParcelTime { set; get; }
        public DateTime? MatchForDroneTime { set; get; }
        public DateTime? CollectingDroneTime { set; get; }
        public DateTime? ArrivingDroneTime { set; get; }
        public override string ToString() { return this.stringProperty();}
    }
}