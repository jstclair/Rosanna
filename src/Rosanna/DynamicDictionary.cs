using System;
using System.Collections.Generic;
using System.Dynamic;
using Nancy;

namespace Rosanna
{
    public class DynamicDictionary : DynamicObject, IEquatable<DynamicDictionary>
    {
        private readonly Dictionary<string, object> _dictionary = new Dictionary<string, object>();

        public dynamic this[string name]
        {
            get
            {
                if(_dictionary.ContainsKey(name))
                    return _dictionary[name];

                return null;
            }
            set
            {
                _dictionary[name] = value is DynamicDictionaryValue ? value : new DynamicDictionaryValue(value);
            }
        }

        public bool Equals(DynamicDictionary other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            return ReferenceEquals(this, other) || Equals(other._dictionary, _dictionary);
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            this[binder.Name] = value;
            return true;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            _dictionary.TryGetValue(binder.Name, out result);
            return true;
        }

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return _dictionary.Keys;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj.GetType() == typeof (DynamicDictionary) && Equals((DynamicDictionary) obj);
        }

        public override int GetHashCode()
        {
            return (_dictionary != null ? _dictionary.GetHashCode() : 0);
        }
    }
}