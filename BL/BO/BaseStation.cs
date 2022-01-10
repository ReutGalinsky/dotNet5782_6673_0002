using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;

namespace BO
{
    /// <summary>
    /// full details about a single base station  
    /// </summary>
    public class BaseStation
    {
        /// <summary>
        /// the id of the station
        /// </summary>
        public string IdNumber { set; get; }
        /// <summary>
        /// the name of the station
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// the availible slots of the station
        /// </summary>
        public int ChargeSlots { set; get; }
        /// <summary>
        /// the location of the station
        /// </summary>
        public Location Location { set; get; }
        /// <summary>
        /// the drones that being charged in the station
        /// </summary>
        public List<DroneInCharge> Drones { set; get; }
        public override string ToString()
        {
            string temp = "";
            temp = temp + string.Format("{0,-10}:  {1,-10} \n{2,-10}:  {3,-10}\n{4,-10}:  {5,-10}\n", "IdNumber", IdNumber, "Name", Name, "ChargeSlots", ChargeSlots);
            temp += string.Format($"Location:\n{Location} ");
            if (Drones.Count != 0)
            {
                temp += "\nDrones:\n";
                foreach (var item in Drones)
                    temp += item.stringProperty() + '\n';
            }
            return temp;
        }
    }
}