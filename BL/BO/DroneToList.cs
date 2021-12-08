using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;

namespace BO
{
    public class DroneToList
    {
        public string IdNumber { set; get; }
        public string Model { set; get; }
        public WeightCategories MaxWeight { set; get; }
        public int Battery { set; get; }
        public DroneState State { set; get; }
        public Location Location { set; get; }
        public string NumberOfParcel { set; get; }
        public override string ToString()
        {
            return this.stringProperty();
        }
    }
}