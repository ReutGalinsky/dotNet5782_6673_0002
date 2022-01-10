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
        /// <summary>
        /// the id of the parcel
        /// </summary>
        public string IdNumber { set; get; }
        /// <summary>
        /// boolian value for the parcel's status
        /// </summary>
        public bool isCollected { set; get; }// true= the parcel collected by customer
        /// <summary>
        /// the priority of the parcel
        /// </summary>
        public Priorities Priority { set; get; }
        /// <summary>
        /// the weight of the parcel
        /// </summary>
        public WeightCategories Weight { set; get; }
        /// <summary>
        /// the senderer customer of the parcel
        /// </summary>
        public CustomerOfParcel Senderer { set; get; }
        /// <summary>
        /// ther getterer customer of the parcel
        /// </summary>
        public CustomerOfParcel Getterer { set; get; }
        /// <summary>
        /// the packing location
        /// </summary>
        public Location Packing { set; get; }
        /// <summary>
        /// the destination 
        /// </summary>
        public Location Destination { set; get; }
        /// <summary>
        /// the distance of the delivery
        /// </summary>
        public double Distance { set; get; }
        public override string ToString(){ return this.stringProperty(); }
    }
}