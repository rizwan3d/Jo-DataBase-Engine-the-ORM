using System;

namespace JoDataBaseEngine
{
    public class DBTableAttribute : Attribute
    {
        String _Value;
        public String Value { get => _Value; }
        public DBTableAttribute(String name)
        {
            this._Value = name;
        }
    }
}
