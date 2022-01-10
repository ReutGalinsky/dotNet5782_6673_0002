using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;

namespace BO
{
    /// <summary>
    /// viewing of a customer. including just hos name and id  
    /// </summary>
    public class CustomerOfParcel
    {
        /// <summary>
        /// the id of the customer
        /// </summary>
        public string IdNumber { set; get; }
        /// <summary>
        /// the name of the customer
        /// </summary>
        public string Name { set; get; }
        public override string ToString() {return this.stringProperty();}
    }
}