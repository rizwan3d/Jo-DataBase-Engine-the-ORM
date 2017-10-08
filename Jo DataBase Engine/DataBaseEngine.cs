using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Reflection;
using System.Linq.Expressions;

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

        public static bool Update<T>(T What, T With, Expression<Func<T, bool>> Condition)
        {
            bool toReturn = false;

            string TableName = What.GetType().Name;
            if(!What.GetType().Name.Equals(With.GetType().Name))
            {
                Console.Write("Invaled Classes");
                return false;
            }
            string commandText = $"UPDATE {TableName} SET ";

            List<ColumnInfo> WhatColumnInfo = ClassToColumnInfo.ColumnInfoFromType(What);
            List<ColumnInfo> WithColumnInfo = ClassToColumnInfo.ColumnInfoFromType(With);

            int i = 0;
            WhatColumnInfo.ForEach(Column =>
            {
                if (!Column.AutoIncrement && !Column.PrimaryKey)
                {
                    commandText += ($"{Column.ColumnName} = @abc{i},");
                    i++;
                }
            });

            commandText = commandText.Substring(0, commandText.Length - 1);

            commandText += " WHERE " + ExpressionSolver.LambdaToString(Condition);

            using (SqlConnection connection = new SqlConnection(_DataBaseInfo.ConnectionString))
            {

                SqlCommand command = new SqlCommand(commandText, connection);
                int j = 0;
                WithColumnInfo.ForEach(Column =>
                {
                    if (!Column.AutoIncrement)
                    {
                        if (Column.ColumnValue is int)
                        {
                            command.Parameters.Add($"@abc{j}", SqlDbType.Int);
                            command.Parameters[$"@abc{j}"].Value = (int)Column.ColumnValue;
                        }
                        else if (Column.ColumnValue is string)
                        {
                            command.Parameters.Add($"@abc{j}", SqlDbType.NChar);
                            command.Parameters[$"@abc{j}"].Value = Column.ColumnValue as string;
                        }
                        else if (Column.ColumnValue is float)
                        {
                            command.Parameters.Add($"@abc{j}", SqlDbType.Float);
                            command.Parameters[$"@abc{j}"].Value = (float)Column.ColumnValue;
                        }
                        j++;
                    }
                });
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

        static bool Delete<T>(T Object, string Condition)
        {
            throw new NotImplementedException();
        }
        
        public static bool Insert<T>(T Object)
        {
            bool toReturn = false;

            string TableName = Object.GetType().Name;
            string commandText = $"INSERT INTO {TableName}(";

            List<ColumnInfo> _ColumnInfo = ClassToColumnInfo.ColumnInfoFromType(Object);

            _ColumnInfo.ForEach(Column =>
           {
               if (!Column.AutoIncrement)
                   commandText += ($"{Column.ColumnName},");
           });

            commandText = commandText.Substring(0, commandText.Length - 1);
            commandText += (")VALUES(");
            int i = 0;
            _ColumnInfo.ForEach(Column =>
            {
                if (!Column.AutoIncrement)
                {
                    commandText += $"@abc{i},";
                    i++;
                }
            });

            commandText = commandText.Substring(0, commandText.Length - 1);
            commandText += (")");
            using (SqlConnection connection = new SqlConnection(_DataBaseInfo.ConnectionString))
            {
                
                SqlCommand command = new SqlCommand(commandText, connection);
                int j = 0;
                _ColumnInfo.ForEach(Column =>
                {
                    if (!Column.AutoIncrement)
                    {
                        if (Column.ColumnValue is int)
                        {
                            command.Parameters.Add($"@abc{j}", SqlDbType.Int);
                            command.Parameters[$"@abc{j}"].Value = (int)Column.ColumnValue;
                        }
                        else if (Column.ColumnValue is string)
                        {
                            command.Parameters.Add($"@abc{j}", SqlDbType.NChar);
                            command.Parameters[$"@abc{j}"].Value = Column.ColumnValue as string;
                        }
                        else if (Column.ColumnValue is float)
                        {
                            command.Parameters.Add($"@abc{j}", SqlDbType.Float);
                            command.Parameters[$"@abc{j}"].Value = (float)Column.ColumnValue;
                        }
                        j++;
                    }
                });
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
