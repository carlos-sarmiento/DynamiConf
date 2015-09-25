using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace DynamiConf.Providers.Helpers
{
    internal static class DynamicMerger
    {
        internal static ExpandoObject Merge(ExpandoObject o1, ExpandoObject o2)
        {
            return o1.UpdateWith(o2);
        }

        internal static ExpandoObject UpdateWith(this ExpandoObject o1, ExpandoObject o2)
        {
            if (o1 == null)
            {
                o1 = new ExpandoObject();
            }
            if (o2 == null)
            {
                o2 = new ExpandoObject();
            }

            var d1 = (IDictionary<string, object>)o1;
            var d2 = (IDictionary<string, object>)o2;

            var newExpando = new ExpandoObject();
            var dResult = (IDictionary<string, object>)newExpando;

            foreach (var kvp in d2)
            {
                if (d1.ContainsKey(kvp.Key))
                {
                    // If the key is shared, values must be merged
                    if (kvp.Value is ExpandoObject)
                    {
                        var oldVal = d1[kvp.Key] as ExpandoObject;
                        if (oldVal == null)
                            throw new InvalidOperationException($"Cannot merge. The same key '{kvp.Key}' is using different types");

                        dResult.Add(kvp.Key, Merge(oldVal, (ExpandoObject)kvp.Value));
                    }
                    else if (kvp.Value is IList<object>)
                    {
                        var oldVal = d1[kvp.Key] as IList<object>;
                        if (oldVal == null)
                            throw new InvalidOperationException($"Cannot merge. The same key '{kvp.Key}' is using different types");

                        var newList = ((IList<object>)kvp.Value).Union(oldVal).ToList();
                        dResult.Add(kvp.Key, newList);
                    }
                    else
                    {
                        dResult.Add(kvp.Key, kvp.Value);
                    }
                }
                else
                {
                    dResult.Add(kvp.Key, kvp.Value);
                }
            }

            foreach (var kvp in d1.Where(kvp => !d2.ContainsKey(kvp.Key)))
            {
                dResult.Add(kvp.Key, kvp.Value);
            }

            return newExpando;
        }

        internal static Configuration Merge(Configuration o1, Configuration o2)
        {
            return o1.UpdateWith(o2);
        }

        internal static Configuration UpdateWith(this Configuration o1, Configuration o2)
        {
            if (o1 == null)
            {
                o1 = new Configuration();
            }
            if (o2 == null)
            {
                o2 = new Configuration();
            }

            var d1 = (IDictionary<string, object>)o1;
            var d2 = (IDictionary<string, object>)o2;

            var newExpando = new Configuration();
            var dResult = (IDictionary<string, object>)newExpando;

            foreach (var kvp in d2)
            {
                if (d1.ContainsKey(kvp.Key))
                {
                    // If the key is shared, values must be merged
                    if (kvp.Value is Configuration)
                    {
                        var oldVal = d1[kvp.Key] as Configuration;
                        if (oldVal == null)
                            throw new InvalidOperationException($"Cannot merge. The same key '{kvp.Key}' is using different types");

                        dResult.Add(kvp.Key, Merge(oldVal, (Configuration)kvp.Value));
                    }
                    else if (kvp.Value is IList<object>)
                    {
                        var oldVal = d1[kvp.Key] as IList<object>;
                        if (oldVal == null)
                            throw new InvalidOperationException($"Cannot merge. The same key '{kvp.Key}' is using different types");

                        var newList = ((IList<object>)kvp.Value).Union(oldVal).ToList();
                        dResult.Add(kvp.Key, newList);
                    }
                    else
                    {
                        dResult.Add(kvp.Key, kvp.Value);
                    }
                }
                else
                {
                    dResult.Add(kvp.Key, kvp.Value);
                }
            }

            foreach (var kvp in d1.Where(kvp => !d2.ContainsKey(kvp.Key)))
            {
                dResult.Add(kvp.Key, kvp.Value);
            }

            return newExpando;
        }
    }
}
