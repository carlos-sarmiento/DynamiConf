using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace DynamiConf.Helpers
{
    public static class ExpandoObject2Configuration
    {
        public static Configuration Transform(ExpandoObject data)
        {
            var newExpando = new Configuration();

            foreach (var kvp in data)
            {
                newExpando[kvp.Key] = TransformByType(kvp.Value);
            }

            return newExpando;
        }

        private static object TransformByType(object value)
        {
            if (value is ExpandoObject)
            {
                return Transform((ExpandoObject)value);
            }

            if (value is List<object>)
            {
                return ConvertList((List<object>)value);
            }

            return value;
        }

        private static object ConvertList(List<object> list)
        {
            var hasSingleType = true;

            var tList = new ArrayList(list.Count);

            Type listType = null;

            if (list.Count > 0)
            {
                listType = list.First().GetType();
            }

            foreach (var v in list)
            {
                hasSingleType = hasSingleType && listType == v.GetType();
                tList.Add(TransformByType(v));
            }

            return tList.ToArray(hasSingleType && listType != null
                                   ? listType
                                   : typeof(object));
        }
    }
}
