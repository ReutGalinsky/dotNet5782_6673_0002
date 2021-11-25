using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;

namespace IBL.BO
{
    public class ParcelInPassing
    {
        public string IdNumber { set; get; }
        public bool isWaitingForColecting { set; get; }
        // is the parcel is waiting to be collected or it's on the way to customer
        public Priorities Priority { set; get; }
        public WeightCategories Weight { set; get; }
        public CustomerOfParcel Senderer { set; get; }
        public CustomerOfParcel Getterer { set; get; }
        public Location Packing { set; get; }
        public Location Destination { set; get; }
        public double Distance { set; get; }
        public override string ToString()
        {
            return this.stringProperty();
        }
    }
}
