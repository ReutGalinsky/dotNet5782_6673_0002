using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public struct Drone
    {
        public string IdNumber { get; set; }
        public string Model { get; set; }
        public WeightCategories MaxWeight { get; set; }
        public override string ToString()
        {
            return string.Format($@"Drone number:{IdNumber}
model: {Model}, Weight: {MaxWeight}"+'\n');
        }
    }
}
