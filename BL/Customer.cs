﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class Customer
    {
        public Location Local { set; get; }
        public int Id { set; get; }
        public string Phone { set; get; }
        public string Name { set; get; }

        public  List<ParcelOfCustomer> FromCustomer { set; get; }
        public List<ParcelOfCustomer> ToCustomer { set; get; }


    }
}