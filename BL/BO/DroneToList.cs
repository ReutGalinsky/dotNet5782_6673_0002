using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;

namespace BO
{
    /// <summary>
    /// viewing of a single drone  
    /// </summary>
    public class DroneToList
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
        /// the max weight that the drone delivering
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
        /// the number of the parcel that the drone is delivering
        /// </summary>
        public string NumberOfParcel { set; get; }
        public override string ToString() {return this.stringProperty();}
    }
}