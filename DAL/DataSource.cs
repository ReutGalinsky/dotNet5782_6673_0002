using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
namespace DalObject
{
    class DataSource
    {
        internal static BaseStation[] Station1= new BaseStation[5];
        internal class Config
        {
            internal static int FirstBaseStation=0;
            internal static int RunningNumber=0;
        }   
        internal static void Initialize()
        {
            Random rand = new Random();
            for(int i=0;i<2;i++)
            {
             // example // Rachs[i].charge = rand.Next(0, 101);
             //   Rachs[i].Id = rand.Next()//??;

            }
        }

    }
}
