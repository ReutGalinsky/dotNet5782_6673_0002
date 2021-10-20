using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using IDAL.DO;

namespace DalObject
{

    public class DalObject
    {
        public DalObject()
        {
            DataSource.Initialize();
        }
        //מותר להשתמש בפונקציה של דן?
        internal static T Clone<T>(this T original)
        {
            T copyToObject = (T)Activator.CreateInstance(original.GetType());

            foreach (PropertyInfo sourcePropertyInfo in original.GetType().GetProperties())
            {
                //PropertyInfo destPropertyInfo = original.GetType().GetProperty(sourcePropertyInfo.Name);

                sourcePropertyInfo.SetValue(copyToObject, sourcePropertyInfo.GetValue(original, null), null);
            }

            return copyToObject;
        }
        public List<BaseStation> AddingBaseStation()
        {
            Console.WriteLine("please enter id number for the new base station");//האם צריך לבדוק שאכן ייחודי?
            BaseStation base1 = new BaseStation();
            base1.IdNumber = Console.Read();
            Console.WriteLine("please enter the name of the station");
            base1.Name = Console.ReadLine();
            Console.WriteLine("please enter the amount of charge slots in your base station");
            base1.ChargeSlots = Console.Read();
            Console.WriteLine("please enter the location of your base station (longitude,latitude)");
            base1.Longitude = Console.Read();
            base1.Latitude = Console.Read();
            DataSource.stations.Add(base1);
            List<BaseStation> newStations = new List<BaseStation>();
            foreach (var item in DataSource.stations)
            {
                BaseStation temp = new BaseStation();
                temp(item);
                newStations.Add(temp);
            }
            return newStations;
        }
    }

}
