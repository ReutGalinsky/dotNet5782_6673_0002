using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;

namespace BO
{
    /// <summary>
    /// viewing of a single parcel
    /// </summary>
    public class ParcelOfList
    {
        public string IdNumber { set; get; }
        public string Sender { set; get; }
        public string Geter { set; get; }
        public WeightCategories Weight { set; get; }
        public Priorities Priority { set; get; }
        public ParcelState ParcelState { set; get; }
        public override string ToString() {return this.stringProperty();}
    }
}