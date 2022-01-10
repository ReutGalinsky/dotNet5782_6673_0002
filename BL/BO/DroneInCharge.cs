using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;

namespace BO
{
    /// <summary>
    /// viewing of a charging drone  
    /// </summary>
    public class DroneInCharge
    {
        /// <summary>
        /// the id of the drone 
        /// </summary>
        public string IdNumber { set; get; }
        /// <summary>
        /// the battery of the drone
        /// </summary>
        public double Battery { set; get; }
        public override string ToString() {return this.stringProperty();}
    }
}