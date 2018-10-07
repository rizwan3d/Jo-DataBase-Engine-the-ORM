using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoDataBaseEngine
{
    class TableNames
    {
        public static string TableName<T>(T Object)
        {
            var attributes = Object.GetType().GetCustomAttributes(false);

            //Column Name
            var column = attributes.FirstOrDefault(a => a.GetType() == typeof(DBTableAttribute));
            DBTableAttribute cl = column as DBTableAttribute;
            return cl.Value;
        }
    }
}
