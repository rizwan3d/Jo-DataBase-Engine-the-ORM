using System;

namespace JoDataBaseEngine
{
    public class ExcludeAttribute : Attribute
    {
        bool _Value;
        public bool Value { get => _Value; }
        public ExcludeAttribute(bool name)
        {
            this._Value = name;
        }
        public ExcludeAttribute()
        {
            this._Value = true;
        }
    }
    public class DoNotSelectAttribute : Attribute
    {
        bool _Value;
        public bool Value { get => _Value; }
        public DoNotSelectAttribute(bool name)
        {
            this._Value = name;
        }
        public DoNotSelectAttribute()
        {
            this._Value = true;
        }
    }
    public class AllowNullAttribute : Attribute
    {
        bool _Value;
        public bool Value { get => _Value; }
        public AllowNullAttribute(bool name)
        {
            this._Value = name;
        }
        public AllowNullAttribute()
        {
            this._Value = true;
        }
    }
}
