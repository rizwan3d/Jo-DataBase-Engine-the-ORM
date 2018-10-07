using System.Data;

namespace JoDataBaseEngine
{
    public class ColumnInfo
    {
        string _ColumnName;
        object _ColumnValue;
        bool _AutoIncrement;
        bool _PrimaryKey;
        bool _AllowNull;
        bool _DoNotSelect;
        SqlDbType _DataType;

        public string ColumnName { get => _ColumnName; set => _ColumnName = value; }
        public object ColumnValue { get => _ColumnValue; set => _ColumnValue = value; }
        public bool AutoIncrement { get => _AutoIncrement; set => _AutoIncrement = value; }
        public bool PrimaryKey { get => _PrimaryKey; set => _PrimaryKey = value; }
        public bool AllowNull { get => _AllowNull; set => _AllowNull = value; }
        public SqlDbType DataType { get => _DataType; set => _DataType = value; }
        public bool DoNotSelect { get => _DoNotSelect; set => _DoNotSelect = value; }
    }
}
