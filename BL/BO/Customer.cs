using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;

namespace BO
{
    public class Customer
    {
        public string IdNumber { set; get; }
        public string Name { set; get; }
        public string Phone { set; get; }
        public Location Location { set; get; }
        public List<ParcelOfCustomer> FromCustomer { set; get; }
        public List<ParcelOfCustomer> ToCustomer { set; get; }
        public override string ToString()
        {
            string temp = "";
            temp = temp + string.Format("{0,-10}:  {1,-10} \n{2,-10}:  {3,-10}\n{4,-10}:  {5,-10}\n", "IdNumber", IdNumber, "Name", Name, "Phone", Phone);
            temp += string.Format($"Location:\n{Location} ");
            if (FromCustomer.Count != 0)
            {
                temp += "\nParcels From Customr:\n";
                foreach (var item in FromCustomer)
                    temp += item.stringProperty() + '\n';
            }
            if (ToCustomer.Count != 0)
            {
                temp += "Parcels To Customr:\n";
                foreach (var item in ToCustomer)
                    temp += item.stringProperty() + '\n';
            }
            return temp;
        }

    }
}