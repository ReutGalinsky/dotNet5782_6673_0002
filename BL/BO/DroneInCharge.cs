using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;

namespace BO
{
   public class DroneInCharge
    {
        public string IdNumber { set; get; }
        public int Battery { set; get; }
        public override string ToString()
        {
            return this.stringProperty();
        }
    }
}
