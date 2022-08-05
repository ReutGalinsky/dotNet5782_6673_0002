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
        /// The id number of the customer
        /// </summary>
        public string IdNumber { get; set; }
        /// <summary>
        /// The name of the customer
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The phone of the customer
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// The latitude of the customer
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// The longitude of the customer
        /// </summary>
        public double Longitude { get; set; }

        public override string ToString()
        {
            return string.Format($@"The customer {Name} with the id of {IdNumber}
phone num. {Phone}
Address:
Longitude: {Longitude} Latitude: {Latitude}");
        }

    }
}