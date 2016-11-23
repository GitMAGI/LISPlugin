using System.Collections.Generic;
using System.Reflection;

namespace GeneralPurposeLib
{
    public class ConversionUtlilities
    {
        public static List<T> DictionaryListToObjectList<T>(List<Dictionary<string, object>> data) where T : new()
        {
            List<T> objs = new List<T>();

            foreach (Dictionary<string, object> datum in data)
            {
                objs.Add(DictionaryToObject<T>(datum));
            }

            return objs;
        }
        public static T DictionaryToObject<T>(Dictionary<string, object> data) where T : new()
        {
            T obj = new T();

            foreach (PropertyInfo prop in obj.GetType().GetProperties())
            {
                if (data.ContainsKey(prop.Name))
                {
                    object value = data[prop.Name];
                    prop.SetValue(obj, value, null);
                }
                else
                {
                    string msg = string.Format("{0} is not defined into the Dictionary to map!", prop.Name);
                }
            }

            return obj;
        }
    }
}
