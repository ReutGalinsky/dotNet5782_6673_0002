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
        public static string sexagesimalFormat(double d, bool flag)
        {
            string num = "";
            float temp = 0;
            int biggerThanZero = (int)d;
            int count = 0;
            while (biggerThanZero != 0)
            {
                temp = ((float)biggerThanZero / 60);
                biggerThanZero = (int)(temp);
                num = string.Format((int)(((temp - biggerThanZero) * 60)) + num);
                count++;
            }
            string final = "";
            final = final + num + "* ";
            temp = (float)(d - (int)d);
            temp = temp * 60;
            final = final + string.Format((int)temp + "' ");
            temp = (temp - (int)temp) * 60;
            final = final + string.Format(temp + "'" + "'");
            if (flag)
            {
                if (d < 0) final = final + string.Format(" S ");
                else final = final + string.Format(" N ");
            }
            else
            {
                if (d >= 0) final = final + string.Format(" E ");
                else final = final + string.Format(" W ");
            }
            return final;


        }
    }

}
