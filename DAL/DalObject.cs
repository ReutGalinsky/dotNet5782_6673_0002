using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace DalObject
{
    public class DalObject
    {
        public DalObject()
        {
            DataSource.Initialize();
        }
        public static BaseStation[] AddingStation()//static?
        {
            Console.WriteLine("please enter youe station name:");
            string name=Console.ReadLine();
            Console.WriteLine("please enter youe station id:");
            int id = Console.Read();
            Console.WriteLine("please enter lenthLIne and WidthLine:");
            double lengthLine=(double) Console.Read();
            double WidthLine = (double)Console.Read();
            DataSource.Station1[DataSource.Config.FirstBaseStation++].IdNumber = id;
//=           BaseStation[] s = new BaseStation[10];
//            return s;
        }
    }
}
