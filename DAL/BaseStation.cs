using System;
using DalObject;
namespace IDAL.DO
{
    public struct BaseStation
    {
        public int IdNumber{get;set;}
        public string Name { get; set; }
        public int ChargeSlots { get; set; }

        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public override string ToString()
        {
            return string.Format(@$"station number {IdNumber}, name: {Name}
number of charge slots: {ChargeSlots}
Longitude: "+DalObject.Tools.sexagesimalFormat(Longitude,true)+'\n'+"Latitude: "+ DalObject.Tools.sexagesimalFormat(Latitude,false)+'\n');
        }
    }
}
