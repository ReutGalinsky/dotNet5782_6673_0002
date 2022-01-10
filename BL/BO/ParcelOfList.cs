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
        /// <summary>
        /// the id of the parcel
        /// </summary>
        public string IdNumber { set; get; }
        /// <summary>
        /// the id of the parcel's sender
        /// </summary>
        public string Sender { set; get; }
        /// <summary>
        /// the id of the parcel's geter
        /// </summary>
        public string Geter { set; get; }
        /// <summary>
        /// the weight category of the parcel
        /// </summary>
        public WeightCategories Weight { set; get; }
        /// <summary>
        /// the priority categort of the parcel
        /// </summary>
        public Priorities Priority { set; get; }
        /// <summary>
        /// the parcel state
        /// </summary>
        public ParcelState ParcelState { set; get; }
        public override string ToString() {return this.stringProperty();}
    }
}