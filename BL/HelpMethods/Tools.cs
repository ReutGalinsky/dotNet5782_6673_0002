using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using IBL.BO;


namespace BL
{
    static class ToolsBl
    {

        //copy elements of BO to DO and vice versa
        public static void CopyPropertiesTo<T, S>(this S from, T to)
        {
            foreach (PropertyInfo propTo in to.GetType().GetProperties())//loop on all the properties in the new object
            {
                PropertyInfo propFrom = typeof(S).GetProperty(propTo.Name);//check if there is property with the same name in the source object and get it
                if (propFrom == null)
                    continue;
                var value = propFrom.GetValue(from, null);//get the value of the prperty
                if (value is ValueType || value is string)
                    propTo.SetValue(to, value);//insert the value to the suitable property
            }
        }
        public static object CopyPropertiesToNew<S>(this S from, Type type)//get the typy we want to copy to 
        {
            object to = Activator.CreateInstance(type); // new object of the Type
            from.CopyPropertiesTo(to);//copy all value of properties with the same name to the new object
            return to;
        }

        #region DistanceToGeneric
        private static double DistanceToGeneric<T,U>(T ob1,U ob2)
        {
            Location a1 = new Location();
            Location a2=new Location();
            foreach(PropertyInfo a in ob1.GetType().GetProperties())
            {
                if (a.PropertyType == typeof(Location))
                    a1 = (Location)a.GetValue(ob1,null);
            }
            foreach (PropertyInfo a in ob2.GetType().GetProperties())
            {
                if (a.PropertyType == typeof(Location))
                    a2 = (Location)a.GetValue(ob2, null);
            }
            double rlat1 = Math.PI * (a1.Latitude) / 180;
            double rlat2 = Math.PI * (a2.Latitude) / 180;
            double theta = a1.Longitude -a2.Longitude;
            double rtheta = Math.PI * theta / 180;
            double dist =
                Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
                Math.Cos(rlat2) * Math.Cos(rtheta);
            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;
            return dist * 1.609344;
        }
        #endregion  


    }
}
