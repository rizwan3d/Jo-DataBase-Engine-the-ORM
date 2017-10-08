using System;

namespace JoDataBaseEngine
{
    public class AutoIncrementAttribute : Attribute
    {
        bool _Value;
        public bool Value { get => _Value; }
        public AutoIncrementAttribute(bool name)
        {
            this._Value = name;
        }
        public AutoIncrementAttribute()
        {
            this._Value = true;
        }
    }
}
