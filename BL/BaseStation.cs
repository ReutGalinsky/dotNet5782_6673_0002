using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class BaseStation
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public Location Local { set; get; }
        public int ChargeSlots { set; get; }
        public List<DroneInCharge> Drones { set; get; }

    }
}
