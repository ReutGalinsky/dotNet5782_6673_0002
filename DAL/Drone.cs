using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL.DO
{
    public struct Drone
    {
        public int IdNumber { get; set; }
        public string Model { get; set; }
        public WeightCategories MaxWeight { get; set; }
        public double Battery { get; set; }
        public DroneStatus Status { get; set; }
        public override string ToString()
        {
            return string.Format($@"Drone number:{IdNumber}
model: {Model}, Weight: {MaxWeight}
Battery {Battery}%, status: {Status}");
        }
    }
}
