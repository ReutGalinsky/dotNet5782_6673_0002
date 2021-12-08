using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public struct Customer
    {
        public string IdNumber { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public override string ToString()
        {
            return "";
//            return string.Format($@"the customer {Name} with the id of {IdNumber}
//phone num. {Phone}
//Address:
//Longitude: " + DalObject.Tools.sexagesimalFormat(Longitude, true) + '\n' + "Latitude: " + DalObject.Tools.sexagesimalFormat(Latitude, false) + '\n');
        }

    }
}