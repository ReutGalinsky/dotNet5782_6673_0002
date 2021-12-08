using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;

namespace BO
{
    /// <summary>
    /// single parcel with details about the parcel's customer
    /// </summary>
    public class ParcelOfCustomer
    {
        public string IdNumber { set; get; }
        public WeightCategories Weight { set; get; }
        public Priorities Priority { set; get; }
        public ParcelState State { set; get; }
        public CustomerOfParcel SourceOrDestinaton { set; get; }
        public override string ToString() { return this.stringProperty(); }
    }
}