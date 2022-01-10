using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;

namespace BO
{
    /// <summary>
    /// full details about a single drone. including his parcel  
    /// </summary>
    public class Drone
    {
        /// <summary>
        /// the id of the drone
        /// </summary>
        public string IdNumber { set; get; }
        /// <summary>
        /// the model of the drone
        /// </summary>
        public string Model { set; get; }
        /// <summary>
        /// the max weight that the drone can delivering
        /// </summary>
        public WeightCategories MaxWeight { set; get; }
        /// <summary>
        /// the battery of the drone
        /// </summary>
        public double Battery { set; get; }
        /// <summary>
        /// the state of the drone
        /// </summary>
        public DroneState State { set; get; }
        /// <summary>
        /// the location of the drone
        /// </summary>
        public Location Location { set; get; }
        /// <summary>
        /// the parcel that the drone is delivering
        /// </summary>
        public ParcelInPassing PassedParcel { set; get; }
        public override string ToString() {return this.stringProperty();}
    }
}