using System;

namespace DO
{
    public struct BaseStation
    {
        public string IdNumber{get;set;}
        public string Name { get; set; }
        public int ChargeSlots { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public override string ToString()
        {
            return string.Format(@$"station number {IdNumber}, name: {Name}
number of charge slots: {ChargeSlots}
Longitude: {Longitude}'\n {Latitude}'\n'");
        }
    }
}
