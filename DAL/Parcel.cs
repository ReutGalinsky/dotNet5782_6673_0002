using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL.DO
{
    public struct Parcel
    {
        public int IdNumber { get; set; }
        public int ClientSendName { get; set; }
        public int ClientGetName { get; set; }
        public int DroneId { get; set; }   
        public WeightCategories Weight { get; set; }
        public Priorities Priority { get; set; }
        public System.DateTime CreateParcelTime { get; set; }
        public System.DateTime MatchForDroneTime { get; set; }
        public System.DateTime collectingDroneTime { get; set; }
        public System.DateTime ArrivingDroneTime { get; set; }

        public override string ToString()
            //assumption: the time shouldn't be printed
        {
            return string.Format($@"parcel number {IdNumber}
was sent to customer num. {ClientSendName} to customer num. {ClientGetName}
weight: {Weight}. priority:{Priority}");
        }
    }
}
