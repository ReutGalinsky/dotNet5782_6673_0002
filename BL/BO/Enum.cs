using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// our enums: including enums for WPPD-weight, priorities, parcel's state and drone's state  
    /// </summary>
    public enum WeightCategories { Light = 1, Middle, Heavy }
    public enum Priorities { Regular = 1, speed, Emergency }
    public enum ParcelState { Define = 1, match, pick, supply }
    public enum DroneState { Available = 1, maintaince, shipping }
}