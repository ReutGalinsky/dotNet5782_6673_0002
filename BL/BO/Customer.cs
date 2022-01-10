using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;

namespace BO
{
    /// <summary>
    /// full details of a single customer. including all his parcels  
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// the id of the customer
        /// </summary>
        public string IdNumber { set; get; }
        /// <summary>
        /// the name of the customer
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// the phone number of the customer
        /// </summary>
        public string Phone { set; get; }
        /// <summary>
        /// the location of the customer
        /// </summary>
        public Location Location { set; get; }
        /// <summary>
        /// the parcels that the customer send
        /// </summary>
        public List<ParcelOfCustomer> FromCustomer { set; get; }
        /// <summary>
        /// the parcels that send to the customer
        /// </summary>
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