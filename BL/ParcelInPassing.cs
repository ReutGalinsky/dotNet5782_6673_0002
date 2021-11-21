using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class ParcelInPassing
    {
        public string IdNumber { set; get; }
        public State State { set; get; }//?
        public Priorities Priority { set; get; }
        public WeightCategories Weight { set; get; }
        public CustomerOfParcel Sender { set; get; }
        public CustomerOfParcel Getter { set; get; }
        public Location Packing { set; get; }
        public Location Destination { set; get; }
        public double Distance { set; get; }

    }
}
