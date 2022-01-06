using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;

namespace BO
{
    /// <summary>
    /// viewing of a shipping drone  
    /// </summary>
    public class DroneInParcel
    {
        public string IdNumber { set; get; }
        public double Battery { set; get; }
        public Location Location { set; get; }
        public override string ToString() {return this.stringProperty();}
    }
}