using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JoDataBaseEngine
{
    class IDataReaderToList
    {
        public static List<T> DataReaderMapToList<T>(IDataReader dr)
        {
            int i = 0;
            List<T> list = new List<T>();
            T obj = default(T);
            List<ColumnInfo> _ColumnInfo = ClassToColumnInfo.ColumnInfoFromType(Activator.CreateInstance<T>()); 
            while (dr.Read())
            {
                obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    var attributes = prop.GetCustomAttributes(false);

                    //Column Name
                    var columnattributes = attributes.Where(a => a.GetType() == typeof(ExcludeAttribute));
                    var column = columnattributes.FirstOrDefault(a => a.GetType() == typeof(ExcludeAttribute));
                    ExcludeAttribute cl = column as ExcludeAttribute;
                    if (cl is null)
                    {
                        if (!object.Equals(dr[_ColumnInfo[i].ColumnName], DBNull.Value))
                        {
                            if (dr[_ColumnInfo[i].ColumnName] is string)
                            {
                                prop.SetValue(obj, dr[_ColumnInfo[i].ColumnName].ToString().TrimEnd().TrimStart(), null);
                            }
                            else
                                prop.SetValue(obj, dr[_ColumnInfo[i].ColumnName], null);
                        }
                        i++;
                    }                    
                }
                i = 0;
                list.Add(obj);
            }
            return list;
        }
    }
}
