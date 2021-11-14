﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class Drone
    {
        public int Id { set; get; }
        public string Model { set; get; }
        public WeightCategories Weight { set; get; }
        public double Battery { set; get; }
        public State State { set; get; }
        public ParcelInPassing PassedParcel { set; get; }
        public Location Current { set; get; }

    }
}
