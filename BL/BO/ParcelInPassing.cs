using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;

namespace BO
{
    /// <summary>
    /// single parcel in passing. including details about locations and customers
    /// </summary>
    public class ParcelInPassing
    {
        public string IdNumber { set; get; }
        public bool isCollected { set; get; }// true= the parcel collected by customer
        public Priorities Priority { set; get; }
        public WeightCategories Weight { set; get; }
        public CustomerOfParcel Senderer { set; get; }
        public CustomerOfParcel Getterer { set; get; }
        public Location Packing { set; get; }
        public Location Destination { set; get; }
        public double Distance { set; get; }
        public override string ToString(){ return this.stringProperty(); }
    }
}