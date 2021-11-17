using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class BaseStationToList
    {
        public int IdNumber { set; get; }
        public string Name { set; get; }
        public int AvailibleChargeSlots { set; get; }
        public int FullChargeSlots { set; get; }

    }
}
