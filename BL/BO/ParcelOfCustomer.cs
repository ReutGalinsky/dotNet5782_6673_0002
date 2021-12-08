using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;

namespace BO
{
    public class ParcelOfCustomer
    {
        public string IdNumber { set; get; }
        public WeightCategories Weight { set; get; }
        public Priorities Priority { set; get; }
        public State State { set; get; }
        public CustomerOfParcel SourceOrDestinaton { set; get; }
        public override string ToString()
        {
            return this.stringProperty();
        }
    }
}