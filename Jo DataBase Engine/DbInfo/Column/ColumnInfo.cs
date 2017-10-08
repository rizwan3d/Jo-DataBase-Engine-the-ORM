namespace JoDataBaseEngine
{
    public class ColumnInfo
    {
        string _ColumnName;
        object _ColumnValue;
        bool _AutoIncrement;
        bool _PrimaryKey;

        public string ColumnName { get => _ColumnName; set => _ColumnName = value; }
        public object ColumnValue { get => _ColumnValue; set => _ColumnValue = value; }
        public bool AutoIncrement { get => _AutoIncrement; set => _AutoIncrement = value; }
        public bool PrimaryKey { get => _PrimaryKey; set => _PrimaryKey = value; }
    }
}
