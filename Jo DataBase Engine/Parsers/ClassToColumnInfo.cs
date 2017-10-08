using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
                ColumnInfo cinfo = new ColumnInfo();
                //prp.Name;

                //Column Value
                cinfo.ColumnValue = prp.GetValue(atype, new object[] { });

                var attributes = prp.GetCustomAttributes(false);

                //Column Name
                var columnattributes = attributes.Where(a => a.GetType() == typeof(DbColumnAttribute));
                var column = columnattributes.FirstOrDefault(a => a.GetType() == typeof(DbColumnAttribute));
                DbColumnAttribute cl = column as DbColumnAttribute;
                cinfo.ColumnName = cl.Name;

                //Autoincrument
                var AutoincrumentAttrib = attributes.Where(a => a.GetType() == typeof(AutoIncrementAttribute));
                var Autoincrument = AutoincrumentAttrib.FirstOrDefault(a => a.GetType() == typeof(AutoIncrementAttribute));
                AutoIncrementAttribute ai = Autoincrument as AutoIncrementAttribute;
                cinfo.AutoIncrement = (ai is null) ? false : ai.Value;

                //Primary Key
                var PrimaryKeyAttrib = attributes.Where(a => a.GetType() == typeof(PrimaryKeyAttribute));
                var PrimaryKey = AutoincrumentAttrib.FirstOrDefault(a => a.GetType() == typeof(PrimaryKeyAttribute));
                PrimaryKeyAttribute pk = Autoincrument as PrimaryKeyAttribute;
                cinfo.PrimaryKey = (pk is null) ? false : pk.Value;

                dict.Add(cinfo);
            }
            return dict;
        }
    }
}
