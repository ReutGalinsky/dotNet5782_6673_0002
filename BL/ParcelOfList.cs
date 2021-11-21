using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class ParcelOfList
    {
        public string IdNumber { set; get; }
        public string Sender { set; get; }
        public string Geter { set; get; }
        public WeightCategories Weight { set; get; }
        public Priorities Priority { set; get; }
        public State State { set; get; }

    }
}
