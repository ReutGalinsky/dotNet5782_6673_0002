using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;

namespace BO
{
    /// <summary>
    /// viewing of a single customer. including all his parcels 
    /// </summary>
    public class CustomerToList
    {
        /// <summary>
        /// the id of the customer
        /// </summary>
        public string IdNumber { set; get; }
        /// <summary>
        /// the phone of the customer
        /// </summary>
        public string Phone { set; get; }
        /// <summary>
        /// the name pf the customer
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// the amount of parcels that the customer send and are supply
        /// </summary>
        public int ParcelSendAndGet { set; get; }
        /// <summary>
        /// the amount of parcels that the customer send and are not supply yet
        /// </summary>
        public int ParcelSendAndNotGet { set; get; }
        /// <summary>
        /// amount of parcels that are on the way to the customer
        /// </summary>
        public int ParcelOnTheWay { set; get; }
        /// <summary>
        /// the amount of parcels that supply to the customer
        /// </summary>
        public int ParcelGet { set; get; }
        public override string ToString() {return this.stringProperty();}
    }
}