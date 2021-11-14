using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class ParcelOfCustomer
    {
        public int Id { set; get; }
        public WeightCategories Weight { set; get; }
        public Priorities Priority { set; get; }
        public State State { set; get; }
        public CustomerOfParcel SourceOrDestinaton { set; get; }




    }
}
