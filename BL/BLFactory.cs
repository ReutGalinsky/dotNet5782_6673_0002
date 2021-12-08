using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLApi
{
    public static class BLFactory
    {
        public static IBL GetBl()
        {
            return new BL.BL();
        }
    }
}
