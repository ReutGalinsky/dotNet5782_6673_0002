using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public struct DroneCharge
    {
        public string DroneId { get; set; }
        public string StationId { get; set; }
        public override string ToString()
        {
            return string.Format($"Drone number: {DroneId}" +
                $"in statation number {StationId}");
        }

    }
}