using System;
namespace DO
{
    /// <summary>
    /// Base Station
    /// </summary>
    public struct BaseStation
    {
        /// <summary>
        /// The id number of the station
        /// </summary>
        public string IdNumber { get; set; }
        /// <summary>
        /// The name of the base station
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The amount of availible charge-slots
        /// </summary>
        public int ChargeSlots { get; set; }
        /// <summary>
        /// The latitude of the station
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// The longitude of the station
        /// </summary>
        public double Longitude { get; set; }

        public override string ToString()
        {
            return string.Format(@$"station number {IdNumber}, name: {Name}
number of charge slots: {ChargeSlots}
Longitude: {Longitude} Latitude: {Latitude}");
        }
    }
}