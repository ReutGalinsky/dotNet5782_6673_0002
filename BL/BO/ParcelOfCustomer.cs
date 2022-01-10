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
        /// <summary>
        /// the id of the parcel
        /// </summary>
        public string IdNumber { set; get; }
        /// <summary>
        /// the weight category of the parcel
        /// </summary>
        public WeightCategories Weight { set; get; }
        /// <summary>
        /// the priority category of the parcel
        /// </summary>
        public Priorities Priority { set; get; }
        /// <summary>
        /// the parcel state
        /// </summary>
        public ParcelState State { set; get; }
        /// <summary>
        /// the other customer 
        /// </summary>
        public CustomerOfParcel SourceOrDestinaton { set; get; }
        public override string ToString() { return this.stringProperty(); }
    }
}