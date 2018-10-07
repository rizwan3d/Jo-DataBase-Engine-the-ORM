using System;
using System.Data;

namespace JoDataBaseEngine
{
    public class DataTypeAttribute : Attribute
    {
        SqlDbType _Value;
        public SqlDbType Value { get => _Value; }
        public DataTypeAttribute(SqlDbType name)
        {
            this._Value = name;
        }
    }
}
