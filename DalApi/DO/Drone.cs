﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// Drone
    /// </summary>
    public struct Drone
    {
        /// <summary>
        /// The id of the drone
        /// </summary>
        public string IdNumber { get; set; }
        /// <summary>
        /// The model of the drone
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// The max weight that the drone can deliver
        /// </summary>
        public WeightCategories MaxWeight { get; set; }
        public override string ToString()
        {
            return string.Format($@"Drone number:{IdNumber}
model: {Model}, Weight: {MaxWeight}" + '\n');
        }
    }
}