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

        public static T Clone<T>(this T original)
        {
            T copyToObject = (T)Activator.CreateInstance(original.GetType());

            foreach (PropertyInfo sourcePropertyInfo in original.GetType().GetProperties())
            {
                //PropertyInfo destPropertyInfo = original.GetType().GetProperty(sourcePropertyInfo.Name);

                sourcePropertyInfo.SetValue(copyToObject, sourcePropertyInfo.GetValue(original, null), null);
            }

            return copyToObject;
        }
    }
}
