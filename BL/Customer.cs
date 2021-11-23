using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;

namespace IBL.BO
{
    public class Customer
    {
        public Location Local { set; get; }
        public string IdNumber { set; get; }
        public string Phone { set; get; }
        public string Name { set; get; }
        public  List<ParcelOfCustomer> FromCustomer { set; get; }
        public List<ParcelOfCustomer> ToCustomer { set; get; }
        public override string ToString()
        {
            return this.stringProperty();
        }

    }
}
