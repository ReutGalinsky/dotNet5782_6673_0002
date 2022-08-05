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
        /// The user name
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// The password of the user
        /// </summary>
        public string UserPassword { get; set; }
        /// <summary>
        /// Boolian value for the user's status
        /// </summary>
        public bool isManager { get; set; }
        public override string ToString()
        {
            return string.Format($@"Name: {UserName}" + '\n'+$"Password: {UserPassword}");
        }
    }
}
