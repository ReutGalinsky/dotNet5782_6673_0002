using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class Parcel
    {
        public int Id { set; get; }
        public CustomerOfParcel Send { set; get; }
        public CustomerOfParcel Get { set; get; }
        public WeightCategories Weight { set; get; }
        public Priorities Priority{ set; get; }
        public DroneInParcel Drone { set; get; }
        public DateTime CreateParcel { set; get; }
        public DateTime Matching { set; get; }
        public DateTime Picking { set; get; }
        public DateTime Supply { set; get; }

    }
}
