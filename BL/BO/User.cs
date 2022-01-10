using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
namespace BO
{
    public class User
    {
        /// <summary>
        /// the user name of the user
        /// </summary>
        public string UserName { set; get; }
        /// <summary>
        /// the password of the user
        /// </summary>
        public string UserPassword { set; get; }
        /// <summary>
        /// boolian value for the user's status
        /// </summary>
        public bool isManager { set; get; }
        public override string ToString() { return this.stringProperty(); }
    }
}
