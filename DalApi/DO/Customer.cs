using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// Customer
    /// </summary>
    public struct Customer
    {
        /// <summary>
        /// the id number of the customer
        /// </summary>
        public string IdNumber { get; set; }
        /// <summary>
        /// the name of the customer
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// the phone of the customer
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// the latitude of the customer
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// the longitude of the customer
        /// </summary>
        public double Longitude { get; set; }

        public override string ToString()
        {
            return string.Format($@"the customer {Name} with the id of {IdNumber}
phone num. {Phone}
Address:
Longitude: {Longitude} Latitude: {Latitude}");
        }

    }
}