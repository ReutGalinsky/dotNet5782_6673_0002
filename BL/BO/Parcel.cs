using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;

namespace BO
{
    /// <summary>
    /// parcel: including details about times, drone and customers
    /// </summary>
    public class Parcel
    {
        /// <summary>
        /// the id of the parcel
        /// </summary>
        public string IdNumber { set; get; }
        /// <summary>
        /// the senderer customer
        /// </summary>
        public CustomerOfParcel SenderCustomer { set; get; }
        /// <summary>
        /// the geter customer
        /// </summary>
        public CustomerOfParcel GeterCustomer { set; get; }
        /// <summary>
        /// the weight of the parcel
        /// </summary>
        public WeightCategories Weight { set; get; }
        /// <summary>
        /// the priority of the parcel
        /// </summary>
        public Priorities Priority { set; get; }
        /// <summary>
        /// the delivering drone
        /// </summary>
        public DroneInParcel Drone { set; get; }
        /// <summary>
        /// the creating time of the parcel
        /// </summary>
        public DateTime? CreateParcelTime { set; get; }
        /// <summary>
        /// the time when the parcels is matched to a drone
        /// </summary>
        public DateTime? MatchForDroneTime { set; get; }
        /// <summary>
        /// the time when the parcels is picked by a drone
        /// </summary>
        public DateTime? CollectingDroneTime { set; get; }
        /// <summary>
        /// the time when the parcels arrives to the geter
        /// </summary>
        public DateTime? ArrivingDroneTime { set; get; }
        public override string ToString() { return this.stringProperty();}
    }
}