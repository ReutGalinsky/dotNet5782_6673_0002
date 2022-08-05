using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// Parcel
    /// </summary>
    public struct Parcel
    {
        /// <summary>
        /// The id number of the parcel
        /// </summary>
        public string IdNumber { get; set; }
        /// <summary>
        /// The id of the sender
        /// </summary>
        public string Sender { get; set; }
        /// <summary>
        /// The id of the geter
        /// </summary>
        public string Geter { get; set; }
        /// <summary>
        /// The id number of the parcel's delivering-drone
        /// </summary>
        public string DroneId { get; set; }
        /// <summary>
        /// The weight category of the parcel
        /// </summary>
        public WeightCategories Weight { get; set; }
        /// <summary>
        /// The priority category of the parcel
        /// </summary>
        public Priorities Priority { get; set; }
        /// <summary>
        /// The creating time of the parcel
        /// </summary>
        public System.DateTime? CreateParcelTime { get; set; }
        /// <summary>
        /// The time when the parcels is matched to a drone
        /// </summary>
        public System.DateTime? MatchForDroneTime { get; set; }
        /// <summary>
        /// The time when the parcels is picked by a drone
        /// </summary>
        public System.DateTime? CollectingDroneTime { get; set; }
        /// <summary>
        /// The time when the parcels arrives to the geter
        /// /summary>
        public System.DateTime? ArrivingDroneTime { get; set; }

        public override string ToString()
        {
            return string.Format($@"Parcel number {IdNumber}
was sent to customer num. {Sender} to customer num. {Geter}
weight: {Weight}. priority:{Priority}" + '\n');
        }
    }
}