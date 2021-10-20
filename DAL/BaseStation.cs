﻿using System;
namespace IDAL.DO
{
    public struct BaseStation
    {
        public int IdNumber{get;set;}
        public int ChargeSlots { get; set; }
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public override string ToString()
        {
            return "BaseStation";
        }
    }
}