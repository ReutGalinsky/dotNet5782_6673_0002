using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class ParcelOfList
    {
        public int IdNumber { set; get; }
        public string ClientSendName { set; get; }
        public string ClientGetName { set; get; }
        public WeightCategories Weight { set; get; }
        public Priorities Priority { set; get; }
        public State State { set; get; }

    }
}
