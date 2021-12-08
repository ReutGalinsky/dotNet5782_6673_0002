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
        public string IdNumber { set; get; }
        public string Phone { set; get; }
        public string Name { set; get; }
        public int ParcelSendAndGet { set; get; }
        public int ParcelSendAndNotGet { set; get; }
        public int ParcelOnTheWay { set; get; }
        public int ParcelGet { set; get; }
        public override string ToString() {return this.stringProperty();}
    }
}