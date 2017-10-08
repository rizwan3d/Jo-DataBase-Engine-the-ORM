using System;

namespace JoDataBaseEngine
{
    public class PrimaryKeyAttribute : Attribute
    {
        bool _Value;
        public bool Value { get => _Value; }
        public PrimaryKeyAttribute(bool name)
        {
            this._Value = name;
        }
        public PrimaryKeyAttribute()
        {
            this._Value = true;
        }
    }
}
