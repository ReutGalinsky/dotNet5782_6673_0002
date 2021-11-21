using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class Drone
    {
        public string IdNumber { set; get; }
        public string Model { set; get; }
        public WeightCategories MaxWeight { set; get; }
        public double Battery { set; get; }
        public DroneState State { set; get; }
        public Location Current { set; get; }
        public ParcelInPassing PassedParcel { set; get; }
    }
}
