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
        public string UserName { set; get; }
        public string UserPassword { set; get; }
        public bool isManager { set; get; }
        public override string ToString() { return this.stringProperty(); }
    }
}
