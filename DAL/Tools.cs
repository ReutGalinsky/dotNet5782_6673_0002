using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace DalObject
{
    static class Tools
    {
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
        //internal static T Clone<T>(this T original)
        //{
        //    if(typeof(T)==typeof(ICollection<>))
        //    {

        //    }
        //}

        //public static ICollection<T> cloneColection<T>(this ICollection<T> original)
        //{
        //    ICollection<T> newCollection = null;
        //    foreach (var item in ori)
        //    {
        //        newCollection.Add(item.CloneItem());
        //    }
        //    return newCollection;

        //}
        //public static T CloneItem<T>(this T original)
        //{
        //    T copyToObject = (T)Activator.CreateInstance(original.GetType());

        //    foreach (PropertyInfo sourcePropertyInfo in original.GetType().GetProperties())
        //    {
        //        //PropertyInfo destPropertyInfo = original.GetType().GetProperty(sourcePropertyInfo.Name);

        //        sourcePropertyInfo.SetValue(copyToObject, sourcePropertyInfo.GetValue(original, null), null);
        //    }

        //    return copyToObject;
        //}
    }
}
