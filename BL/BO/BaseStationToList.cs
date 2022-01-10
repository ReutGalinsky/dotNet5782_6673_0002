using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;

namespace BO
{
    /// <summary>
    /// viewing of a single base station  
    /// </summary>
    public class BaseStationToList
    {
        /// <summary>
        /// the id of the station
        /// </summary>
        public string IdNumber { set; get; }
        /// <summary>
        /// the name of the station
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// the amount of availible slots in the station
        /// </summary>
        public int ChargeSlots { set; get; }
        /// <summary>
        /// the amount of full slots in the station
        /// </summary>
        public int FullChargeSlots { set; get; }
        public override string ToString() {return this.stringProperty();}
    }
}