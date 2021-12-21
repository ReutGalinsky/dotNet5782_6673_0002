using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public struct User
    {
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public bool isManager { get; set; }
        public override string ToString()
        {
            return string.Format($@"Name: {UserName}" + '\n'+$"Password: {UserPassword}");
        }
    }
}
