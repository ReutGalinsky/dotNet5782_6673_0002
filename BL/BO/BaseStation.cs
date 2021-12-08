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
        public string IdNumber { set; get; }
        public string Name { set; get; }
        public int ChargeSlots { set; get; }
        public Location Location { set; get; }
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