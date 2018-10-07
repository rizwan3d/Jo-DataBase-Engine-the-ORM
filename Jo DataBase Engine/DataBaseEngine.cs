using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Linq;
using System.ComponentModel;
using System.Reflection;

namespace JoDataBaseEngine
{
    public static class DataBaseEngine
    {
        static DataBaseInfo _DataBaseInfo;

        public static DataBaseInfo DataBaseInfo { get => _DataBaseInfo; set => _DataBaseInfo = value; }

        public static List<T> Select<T>(T What, Expression<Func<T, bool>> Condition)
        {
            List<T> ToReturn = new List<T>();

            string commandText = $"SELECT * ";
            string c = ExpressionSolver.LambdaToString(Condition);
            commandText += $" FROM {TableNames.TableName(What)} WHERE " + c;

            using (SqlConnection connection = new SqlConnection(_DataBaseInfo.ConnectionString))
            {

                SqlCommand command = new SqlCommand(commandText, connection);
                //try
                //{
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        ToReturn = IDataReaderToList.DataReaderMapToList<T>(reader);
                    }
                //}
                //catch { }
                //finally
                //{
                    connection.Close();
                //}
            }
            return ToReturn;
        }
        
        public static List<T> Select<T>(T What)
        {
            return Select(What, x => true);
        }

        public static DataTable SelectDataTable<T>(T What, Expression<Func<T, bool>> Condition)
        {
            var data = Select(What, Condition);
            return ToDataTable(data);
        }

        static DataTable SelectDataTable<T>(T What)
        {
            return ToDataTable(Select(What));
        }

        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        public static bool Update<T>(T What, T With, Expression<Func<T, bool>> Condition)
        {
            bool toReturn = false;

            string TableName = TableNames.TableName(What);
            if(!What.GetType().Name.Equals(With.GetType().Name))
            {
                Console.Write("Invaled Classes");
                return false;
            }
            string commandText = $"UPDATE {TableName} SET ";

            List<ColumnInfo> WhatColumnInfo = ClassToColumnInfo.ColumnInfoFromType(What);
            List<ColumnInfo> WithColumnInfo = ClassToColumnInfo.ColumnInfoFromType(With);

            int i = 0;
            //WhatColumnInfo.ForEach(Column =>
            //{
            //    if (!Column.AutoIncrement)
            //    {
            //        commandText += ($"{Column.ColumnName} = '{WithColumnInfo[i].ColumnValue}',");
            //        i++;
            //    }
            //});

            WhatColumnInfo.ForEach(Column =>
            {
                if (!Column.AutoIncrement)
                {
                    commandText += ($"{Column.ColumnName} = @a{i},");
                    i++;
                }
            });

            commandText = commandText.Substring(0, commandText.Length - 1);
            string u = ExpressionSolver.LambdaToString(Condition);
            commandText += " WHERE " + u;

            using (SqlConnection connection = new SqlConnection(_DataBaseInfo.ConnectionString))
            {

                SqlCommand command = new SqlCommand(commandText, connection);
                int j = 0;
                WithColumnInfo.ForEach(Column =>
                {
                    if (!Column.AutoIncrement)
                    {

                        if (!Column.AutoIncrement)
                        {
                            if (Column.ColumnValue is null)
                            {
                                //if (Column.AllowNull)
                                //{
                                command.Parameters.Add($"@a{j}", Column.DataType);
                                command.Parameters[$"@a{j}"].Value = "";
                                //}
                                //else
                                //{

                                //}
                            }
                            else
                            {
                                command.Parameters.Add($"@a{j}", Column.DataType);
                                command.Parameters[$"@a{j}"].Value = Column.ColumnValue;
                            }
                            j++;
                        }
                    }
                });
                //try
                //{
                    connection.Open();
                    command.ExecuteNonQuery();
                    toReturn = true;

               // }
                //catch { }
                //finally
                //{
                    connection.Close();
                //}
            }
            return toReturn;

        }

        public static bool Delete<T>(T Object, Expression<Func<T, bool>> Condition)
        {
            bool toReturn = false;
      
            string TableName = TableNames.TableName(Object);
            
            string commandText = $"DELETE FROM {TableName} ";

            commandText += " WHERE " + ExpressionSolver.LambdaToString(Condition);

            using (SqlConnection connection = new SqlConnection(_DataBaseInfo.ConnectionString))
            {

                SqlCommand command = new SqlCommand(commandText, connection);
             
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

        public static bool Insert<T>(T Object)
        {
            bool toReturn = false;

            string commandText = $"INSERT INTO {TableNames.TableName(Object)}(";

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
                    commandText += $"@a{i},";
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
                        if (Column.ColumnValue is null)
                        {
                            //if (Column.AllowNull)
                            //{
                            command.Parameters.Add($"@a{j}", Column.DataType);
                            command.Parameters[$"@a{j}"].Value = "";
                            //}
                            //else
                            //{

                            //}
                        }
                        else
                        {
                            command.Parameters.Add($"@a{j}", Column.DataType);
                            command.Parameters[$"@a{j}"].Value = Column.ColumnValue;
                        }
                        j++;
                    }
                });
                //try
                //{
                connection.Open();
                command.ExecuteNonQuery();
                toReturn = true;

                //}
                //catch { }
                //finally
                //{
                connection.Close();
                //}
            }
            return toReturn;
        }

        public static bool Insert<T>(T Object,out object primrekey)
        {
            bool toReturn = false;

            string commandText = $"INSERT INTO {TableNames.TableName(Object)}(";

            List<ColumnInfo> _ColumnInfo = ClassToColumnInfo.ColumnInfoFromType(Object);
            ColumnInfo pk = null;
            _ColumnInfo.ForEach(Column =>
            {
                if (!Column.AutoIncrement)
                    commandText += ($"{Column.ColumnName},");
                if (Column.PrimaryKey)
                    pk = Column;
            });

            commandText = commandText.Substring(0, commandText.Length - 1);
            if(pk is null)
                commandText += (")VALUES(");
            else
                commandText += ($") OUTPUT INSERTED.{pk.ColumnName} VALUES(");
            int i = 0;
            _ColumnInfo.ForEach(Column =>
            {
                if (!Column.AutoIncrement)
                {
                    commandText += $"@a{i},";
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
                        if (Column.ColumnValue is null)
                        {
                            //if (Column.AllowNull)
                            //{
                            command.Parameters.Add($"@a{j}", Column.DataType);
                            command.Parameters[$"@a{j}"].Value = "";
                            //}
                            //else
                            //{

                            //}
                        }
                        else
                        {
                            command.Parameters.Add($"@a{j}", Column.DataType);
                            command.Parameters[$"@a{j}"].Value = Column.ColumnValue;
                        }
                        j++;
                    }
                });
                //try
                //{
                connection.Open();
                primrekey = command.ExecuteScalar();
                toReturn = true;

                //}
                //catch { }
                //finally
                //{
                connection.Close();
                //}
            }
            return toReturn;
        }

        public static uint Count<T>(T Object)
        {
            return (uint)Select(Object, x => true).Count;
        }

        static uint Count<T>(T Object, Expression<Func<T, bool>> Condition)
        {
            return (uint)Select(Object, Condition).Count;
        }

        //static bool CreateTable<T>(T Table)
        //{
        //    throw new NotImplementedException();
        //}

        //static bool DeleteTable<T>(T Table)
        //{
        //    throw new NotImplementedException();
        //}

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
