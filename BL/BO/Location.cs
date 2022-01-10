using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;

namespace BO
{
    /// <summary>
    /// the location. including longitude and latitude
    /// </summary>
    public class Location
    {
        /// <summary>
        /// the latitude of the location
        /// </summary>
        public double Latitude { set; get; }
        /// <summary>
        /// the longitude of the location
        /// </summary>
        public double Longitude { set; get; }

        public override string ToString()
        {
            return string.Format(
                "Latitude: " + BL.ToolsBl.sexagesimalFormat(Latitude, false) + '\n'
                + "Longitude: " + BL.ToolsBl.sexagesimalFormat(Longitude, true)
                ); ;
        }
    }
}