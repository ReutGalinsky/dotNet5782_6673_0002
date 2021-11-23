using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;

namespace IBL.BO
{
    public class Location
    {
        public double Longitude { set; get; }
        public double Latitude { set; get; }
        public override string ToString()
        {
            return this.stringProperty();
        }
    }
}
