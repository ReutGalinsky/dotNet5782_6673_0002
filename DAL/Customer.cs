﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL.DO
{
    public struct Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public override string ToString()
        {
            return string.Format($@"the customer {Name} with the id of {Id}
phone num. {Phone}
Address: {Longitude} ,{Latitude}");///האם צריך גם את המילה פורמט?
        }

    }
}