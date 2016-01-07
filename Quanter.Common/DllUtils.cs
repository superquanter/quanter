using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Quanter.Common
{
    public class DllUtils
    {
        public static T CreateInstance<T>(string dllPath, String className)
        {
            Assembly ass = Assembly.LoadFrom(dllPath);

            Type type = ass.GetType(className);
            try
            {
                object obj = Activator.CreateInstance(type);
                return (T)obj;
            }
            catch (Exception e)
            {
                return default(T);
            }

        }

    }
}
