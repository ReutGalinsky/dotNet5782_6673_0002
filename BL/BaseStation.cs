using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class BaseStation
    {
        public string IdNumber { set; get; }
        public string Name { set; get; }
        public int ChargeSlots { set; get; }
        public Location Local { set; get; }
        public List<DroneInCharge> Drones { set; get; }

    }
}
