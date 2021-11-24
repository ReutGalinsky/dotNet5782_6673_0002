﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;

namespace IBL.BO
{
    public class Drone
    {
        public string IdNumber { set; get; }
        public string Model { set; get; }
        public WeightCategories MaxWeight { set; get; }
        public int Battery { set; get; }
        public DroneState State { set; get; }
        public Location Location { set; get; }
        public ParcelInPassing PassedParcel { set; get; }
        public override string ToString()
        {
            return this.stringProperty();
        }
    }
}
