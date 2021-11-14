using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class DroneToList
    {
        public int Id { set; get; }
        public string Model { set; get; }
        public WeightCategories Weight { set; get; }
        public double Battery { set; get; }
        public State State { set; get; }
        public Location Current { set; get; }
        public int NumberOfParcel { set; get; }
    }
}
