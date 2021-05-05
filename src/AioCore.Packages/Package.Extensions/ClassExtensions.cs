using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace Package.Extensions
{
    public static class ClassExtensions
    {
        public static string ToTable(this Type type)
        {
            var tableAttribute = type.GetTypeInfo().GetCustomAttribute<TableAttribute>();
            var tableAttributeName = tableAttribute?.Name;
            return !string.IsNullOrEmpty(tableAttributeName) ? tableAttributeName : type.Name;
        }

        public static Dictionary<string, object> ToDictionary(this object obj)
        {
            var dictionary = obj.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .ToDictionary(prop => prop.Name, prop => prop.GetValue(obj, null));
            return dictionary;
        }

        public static object ToObject(this Dictionary<string, object> dictionary)
        {
            var expandoObj = new ExpandoObject();
            var expandoObjCollection = (ICollection<KeyValuePair<string, object>>)expandoObj;

            foreach (var keyValuePair in dictionary)
            {
                expandoObjCollection.Add(keyValuePair);
            }
            dynamic eoDynamic = expandoObj;
            return eoDynamic;
        }
    }
}