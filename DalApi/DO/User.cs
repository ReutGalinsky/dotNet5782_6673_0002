using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// User- Manger/Customer
    /// </summary>
    public struct User
    {
        /// <summary>
        /// the user name
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// the password of the user
        /// </summary>
        public string UserPassword { get; set; }
        /// <summary>
        /// boolian value for the user's status
        /// </summary>
        public bool isManager { get; set; }
        public override string ToString()
        {
            return string.Format($@"Name: {UserName}" + '\n'+$"Password: {UserPassword}");
        }
    }
}
