﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;

namespace IBL.BO
{
    public class DroneToList
    {
        public string IdNumber { set; get; }
        public string Model { set; get; }
        public WeightCategories MaxWeight { set; get; }
        public double Battery { set; get; }
        public DroneState State { set; get; }
        public Location Current { set; get; }
        public string NumberOfParcel { set; get; }
        public override string ToString()
        {
            return this.stringProperty();
        }
    }
}
