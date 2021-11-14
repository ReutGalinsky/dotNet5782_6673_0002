using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class ParcelInPassing
    {
        public int Id { set; get; }
        public bool ParcelState { set; get; }
        public Priorities Priority { set; get; }
        public WeightCategories Weight { set; get; }
        public CustomerOfParcel Send { set; get; }
        public CustomerOfParcel Set { set; get; }
        public Location Packing { set; get; }
        public Location Destination { set; get; }
        public double Distance { set; get; }

    }
}
