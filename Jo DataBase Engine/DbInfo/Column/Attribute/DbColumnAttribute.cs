using System;

namespace JoDataBaseEngine
{
    public class DbColumnAttribute : Attribute
    {
        string _Name;
        public string Name { get => _Name;}
        public DbColumnAttribute(string name)
        {
            this._Name = name;
        }
    }
}
