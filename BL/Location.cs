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
        public double Latitude { set; get; }
        public double Longitude { set; get; }

        public override string ToString()
        {
            return string.Format(
                "Latitude: " + BL.ToolsBl.sexagesimalFormat(Latitude, false)+'\n'
                + "Longitude: " + BL.ToolsBl.sexagesimalFormat(Longitude, true)
                ); ;
        }
    }
}
