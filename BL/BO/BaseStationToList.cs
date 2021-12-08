using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;

namespace BO
{
    public class BaseStationToList
    {
        public string IdNumber { set; get; }
        public string Name { set; get; }
        public int ChargeSlots { set; get; }
        public int FullChargeSlots { set; get; }
        public override string ToString()
        {
            return this.stringProperty();
        }
    }
}