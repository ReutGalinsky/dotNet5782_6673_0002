using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using DO;
using DLAPI;

namespace BLAPI
{
    public static class BLFactory
    {
        public static IBL GetBL()
        {
            return new BL.BL();
        }
    }
}
