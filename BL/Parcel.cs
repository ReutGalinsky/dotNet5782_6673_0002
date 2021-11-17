using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class Parcel
    {
        public int IdNumber { set; get; }
        public CustomerOfParcel Send { set; get; }
        public CustomerOfParcel Get { set; get; }
        public WeightCategories Weight { set; get; }
        public Priorities Priority{ set; get; }
        public DroneInParcel Drone { set; get; }
        public DateTime CreateParcelTime { set; get; }
        public DateTime MatchForDroneTime { set; get; }
        public DateTime collectingDroneTime { set; get; }
        public DateTime ArrivingDroneTime { set; get; }

    }
}
