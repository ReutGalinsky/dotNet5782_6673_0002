﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class CustomerToList
    {
        public int Id { set; get; }
        public string Phone { set; get; }
        public string Name { set; get; }
        public int ParcelSendAndGet { set; get; }

        public int ParcelSendAndNotGet { set; get; }
        public int ParcelOnTheWay { set; get; }
        public int ParcelGet { set; get; }




    }
}