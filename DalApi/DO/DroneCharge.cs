﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// Full charge slot
    /// </summary>
    public struct DroneCharge
    {
        /// <summary>
        /// The drone id of that charge slot
        /// </summary>
        public string DroneId { get; set; }
        /// <summary>
        /// The station id of that charge slot
        /// </summary>
        public string StationId { get; set; }
        /// <summary>
        /// The charging start time
        /// </summary>
        public DateTime? startCharging { get; set; }
        public override string ToString()
        {
            return string.Format($"Drone number: {DroneId}" +
                $"in statation number {StationId}");
        }

    }
}