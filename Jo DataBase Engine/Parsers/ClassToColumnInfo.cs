using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace JoDataBaseEngine
{
    class ClassToColumnInfo
    {
        public static List<ColumnInfo> ColumnInfoFromType(object atype)
        {
            if (atype == null) return null;
            Type t = atype.GetType();
            PropertyInfo[] props = t.GetProperties();
            List<ColumnInfo> dict = new List<ColumnInfo>();
            foreach (PropertyInfo prp in props)
            {
                

                var attributes = prp.GetCustomAttributes(false);

                var columnattributesa = attributes.Where(a => a.GetType() == typeof(ExcludeAttribute));
                var column = columnattributesa.FirstOrDefault(a => a.GetType() == typeof(ExcludeAttribute));
                ExcludeAttribute ea = column as ExcludeAttribute;
                if (ea is null)
                {
                    ColumnInfo cinfo = new ColumnInfo();

                    cinfo.ColumnValue = prp.GetValue(atype, new object[] { });
                    //Column Name
                    var columnattributes = attributes.Where(a => a.GetType() == typeof(DbColumnAttribute));
                    column = columnattributes.FirstOrDefault(a => a.GetType() == typeof(DbColumnAttribute));
                    DbColumnAttribute cl = column as DbColumnAttribute;
                    cinfo.ColumnName = cl.Name;

                    //Autoincrument
                    var Attrib = attributes.Where(a => a.GetType() == typeof(AutoIncrementAttribute));
                    column = Attrib.FirstOrDefault(a => a.GetType() == typeof(AutoIncrementAttribute));
                    AutoIncrementAttribute ai = column as AutoIncrementAttribute;
                    cinfo.AutoIncrement = (ai is null) ? false : ai.Value;

                    //Primary Key
                    Attrib = attributes.Where(a => a.GetType() == typeof(PrimaryKeyAttribute));
                    column = Attrib.FirstOrDefault(a => a.GetType() == typeof(PrimaryKeyAttribute));
                    PrimaryKeyAttribute pk = column as PrimaryKeyAttribute;
                    cinfo.PrimaryKey = (pk is null) ? false : pk.Value;

                    //AllowNullAttribute
                    Attrib = attributes.Where(a => a.GetType() == typeof(AllowNullAttribute));
                    column = Attrib.FirstOrDefault(a => a.GetType() == typeof(AllowNullAttribute));
                    AllowNullAttribute an = column as AllowNullAttribute;
                    cinfo.AllowNull = (an is null) ? false : an.Value;

                    //DataTypeAttribute
                    Attrib = attributes.Where(a => a.GetType() == typeof(DataTypeAttribute));
                    column = Attrib.FirstOrDefault(a => a.GetType() == typeof(DataTypeAttribute));
                    DataTypeAttribute dt = column as DataTypeAttribute;
                    cinfo.DataType = dt.Value;

                    //DonotSelect
                    Attrib = attributes.Where(a => a.GetType() == typeof(DoNotSelectAttribute));
                    column = Attrib.FirstOrDefault(a => a.GetType() == typeof(DoNotSelectAttribute));
                    DoNotSelectAttribute ds = column as DoNotSelectAttribute;
                    cinfo.DoNotSelect = (ds is null) ? false : ds.Value;

                    dict.Add(cinfo);
                }                
            }
            return dict;
        }
    }
}
