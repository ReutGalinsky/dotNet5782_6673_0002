﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// Parcel
    /// </summary>
    public struct Parcel
    {
        public string IdNumber { get; set; }
        public string Sender { get; set; }
        public string Geter { get; set; }
        public string DroneId { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Priority { get; set; }
        public System.DateTime? CreateParcelTime { get; set; }
        public System.DateTime? MatchForDroneTime { get; set; }
        public System.DateTime? CollectingDroneTime { get; set; }
        public System.DateTime? ArrivingDroneTime { get; set; }

        public override string ToString()
        {
            return string.Format($@"parcel number {IdNumber}
was sent to customer num. {Sender} to customer num. {Geter}
weight: {Weight}. priority:{Priority}" + '\n');
        }
    }
}