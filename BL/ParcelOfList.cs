using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class ParcelOfList
    {
        public int Id { set; get; }
        public string Send { set; get; }
        public string Get { set; get; }
        public WeightCategories Weight { set; get; }
        public Priorities Prioritie { set; get; }
        public State State { set; get; }

    }
}
