using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Reflection;

namespace JoDataBaseEngine
{
    public static class DataBaseEngine
    {
        static DataBaseInfo _DataBaseInfo;

        public static DataBaseInfo DataBaseInfo { get => _DataBaseInfo; set => _DataBaseInfo = value; }

        static List<T> Select<T>(string Condition)
        {
            throw new NotImplementedException();
        }

        static List<T> Select<T>()
        {
            throw new NotImplementedException();
        }

        static DataTable Select(string Condition)
        {
            throw new NotImplementedException();
        }

        static DataTable Select()
        {
            throw new NotImplementedException();
        }

        static bool Update<T>(T What, T With, string Condition)
        {
            throw new NotImplementedException();
        }

        static bool Delete<T>(T Object, string Condition)
        {
            throw new NotImplementedException();
        }

        private static List<Tuple<string, object, string,bool>> DictionaryFromType(object atype)
        {
            if (atype == null) return null;
            Type t = atype.GetType();
            PropertyInfo[] props = t.GetProperties();
            List<Tuple<string, object, string, bool>> dict = new List<Tuple<string, object, string, bool>>();
            foreach (PropertyInfo prp in props)
            {
                string name = prp.Name;
                object value = prp.GetValue(atype, new object[] { });
                var attributes = prp.GetCustomAttributes(false);
                var columnMapping = attributes.FirstOrDefault(a => a.GetType() == typeof(DbColumnAttribute));
                DbColumnAttribute mapsto = columnMapping as DbColumnAttribute;
                string Column = mapsto.Name;
                var AutoincrumentAttrib = attributes.Where(a => a.GetType() == typeof(AutoIncrementAttribute));
                var Autoincrument = AutoincrumentAttrib.FirstOrDefault(a => a.GetType() == typeof(AutoIncrementAttribute));
                AutoIncrementAttribute ai = Autoincrument as AutoIncrementAttribute;
                if(ai is null)
                    dict.Add(new Tuple<string, object, string, bool>(prp.Name, value, Column,false));
                else
                    dict.Add(new Tuple<string, object, string, bool>(prp.Name, value, Column,ai.Value));
            }
            return dict;
        }

        public static bool Insert<T>(T Object)
        {
            bool toReturn = false;

            string TableName = Object.GetType().Name;
            string commandText = $"INSERT INTO {TableName}(";

            List<Tuple<string, object, string, bool>> a = DictionaryFromType(Object);

            foreach (Tuple<string, object, string, bool> entry in a)
            {
                if(!entry.Item4)
                    commandText += ($"{entry.Item3},");
            }

            commandText = commandText.Substring(0, commandText.Length - 1);
            commandText += (")VALUES(");
            int i = 0;
            foreach (Tuple<string, object, string, bool> entry in a)
            {
                if (!entry.Item4)
                {
                    commandText += $"@abc{i},";
                    i++;
                }
            }
                
            commandText = commandText.Substring(0, commandText.Length - 1);
            commandText += (")");
            Console.WriteLine(commandText);
            using (SqlConnection connection = new SqlConnection(_DataBaseInfo.ConnectionString))
            {
                
                SqlCommand command = new SqlCommand(commandText, connection);
                int j = 0;
                foreach (Tuple<string, object, string, bool> entry in a)
                {
                    if (!entry.Item4)
                    {
                        if (entry.Item2 is int)
                        {
                            command.Parameters.Add($"@abc{j}", SqlDbType.Int);
                            command.Parameters[$"@abc{j}"].Value = (int)entry.Item2;
                        }
                        else if (entry.Item2 is string)
                        {
                            command.Parameters.Add($"@abc{j}", SqlDbType.NChar);
                            command.Parameters[$"@abc{j}"].Value = entry.Item2 as string;
                        }
                        else if (entry.Item2 is float)
                        {
                            command.Parameters.Add($"@abc{j}", SqlDbType.Float);
                            command.Parameters[$"@abc{j}"].Value = (float)entry.Item2;
                        }
                        j++;
                    }
                }
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    toReturn = true;

                }
                catch { }
                finally
                {
                    connection.Close();
                }
            }
            return toReturn;
        }

        static uint Count<T>(T Object)
        {
            throw new NotImplementedException();
        }

        static uint Count<T>(T Object,object What, string Condition)
        {
            throw new NotImplementedException();
        }

        static void ToCVS()
        {
            throw new NotImplementedException();
        }

        static void ToCVS<T>(T Object)
        {
            throw new NotImplementedException();
        }

    }
}
