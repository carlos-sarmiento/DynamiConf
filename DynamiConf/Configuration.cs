using System.Collections;
using System.Collections.Generic;
using System.Dynamic;

namespace DynamiConf
{
    public class Configuration : DynamicObject, IDictionary<string, object>
    {
        private readonly Dictionary<string, object> _confValues = new Dictionary<string, object>();

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = _confValues.ContainsKey(binder.Name)
                ? _confValues[binder.Name]
                : new DefaultValue();

            return true;
        }
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            _confValues[binder.Name] = value;

            return true;
        }
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            switch (binder.Name)
            {
                case "Clone":
                    result = Clone();
                    return true;
                case "GetWithDefault":
                    if (args.Length == 2 && args[0] is string)
                    {
                        var key = (string)args[0];
                        result = _confValues.ContainsKey(key)
                            ? _confValues[key]
                            : args[1];
                        return true;
                    }
                    break;
                case "Exists":
                    if (args.Length == 1 && args[0] is string)
                    {
                        result = _confValues.ContainsKey((string)args[0]);
                        return true;
                    }
                    break;
            }

            result = null;
            return false;
        }

        private Configuration Clone()
        {
            return null;
        }

        #region Implementation of IEnumerable

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return _confValues.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Implementation of ICollection<KeyValuePair<string,object>>

        public void Add(KeyValuePair<string, object> item)
        {
            _confValues.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            _confValues.Clear();
        }

        public bool Contains(KeyValuePair<string, object> item)
        {
            return _confValues.ContainsKey(item.Key) && _confValues[item.Key] == item.Value;
        }

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<string, object>>)_confValues).CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<string, object> item)
        {
            return ((ICollection<KeyValuePair<string, object>>)_confValues).Remove(item);
        }

        public int Count
        {
            get { return _confValues.Count; }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        #endregion

        #region Implementation of IDictionary<string,object>

        public bool ContainsKey(string key)
        {
            return _confValues.ContainsKey(key);
        }

        public void Add(string key, object value)
        {
            _confValues.Add(key, value);
        }

        public bool Remove(string key)
        {
            return _confValues.Remove(key);
        }

        public bool TryGetValue(string key, out object value)
        {
            return _confValues.TryGetValue(key, out value);
        }

        public object this[string key]
        {
            get { return _confValues[key]; }
            set { _confValues[key] = value; }
        }

        public ICollection<string> Keys
        {
            get { return _confValues.Keys; }
        }

        public ICollection<object> Values
        {
            get { return _confValues.Values; }
        }

        #endregion
    }
}
