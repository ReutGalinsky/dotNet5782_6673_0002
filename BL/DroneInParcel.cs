﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;

namespace IBL.BO
{
    public class DroneInParcel
    {
        public string IdNumber { set; get; }
        public double Battery { set; get; }
        public Location Current { set; get; }
        public override string ToString()
        {
            return this.stringProperty();
        }
    }
}
