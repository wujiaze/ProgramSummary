using System;
using System.Data.SQLite;


namespace Sqlite_CSharp_Formwork
{
    public enum Type
    {
        NonQuery
    }

    /// <summary>
    /// SQLite 操作类
    /// </summary>
    public class SQLiteHelper
    {
        /// <summary>
        /// 数据库连接定义
        /// </summary>
        private static SQLiteConnection dbConnection;

        /// <summary>
        /// SQL命令定义
        /// </summary>
        private static SQLiteCommand dbCommand;

        /// <summary>
        /// 数据读取定义
        /// </summary>
        private static SQLiteDataReader dataReader;

        /// <summary>
        /// 连接并打开数据库
        /// Pooling=true;FailIfMissing=false
        /// 路径不存在数据库时，会创建数据库。
        /// 当只有数据库名时，表示数据库在bin/Debug路径下
        /// </summary>
        /// <param name="DBpath">数据库路径</param>
        public static void OpenDataBase(string DBpath)
        {
            if (string.IsNullOrEmpty(DBpath)) return;
            try
            {
                if (dbConnection != null)
                {
                    Exception e = new Exception("dbConnection");
                    throw e;
                }
                string str = @"Data Source=" + DBpath + ";Pooling=true;FailIfMissing=false";
                dbConnection = new SQLiteConnection(str);
                dbConnection.Open();
            }
            catch (Exception e)
            {
                Log(DBpath + "数据库打开失败" + e);
            }
        }

      
        /// <summary>
        /// 执行SQL命令
        /// </summary>
        /// <returns>The query.</returns>
        /// <param name="queryString">SQL命令字符串</param>
        private static SQLiteDataReader ExecuteQuery(string queryString)
        {
            try
            {
                dbCommand = dbConnection.CreateCommand();
                dbCommand.CommandText = queryString;
                dataReader = dbCommand.ExecuteReader();
            }
            catch (Exception e)
            {
                Log(e.Message);
            }
            return dataReader;
        }

        private static int ExecuteQuery(string queryString, Type type)
        {
            int count =0 ;
            try
            {
                dbCommand = dbConnection.CreateCommand();
                dbCommand.CommandText = queryString;
                count = dbCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Log(e.Message);
            }
            return count;
        }

        /// <summary>
        /// 显示命令效果
        /// </summary>
        /// <param name="reader"></param>
        public static void ShowData(SQLiteDataReader reader)
        {
            
            if(reader==null)return;
            while (reader.Read())
            {
                string str = "命令执行成功: ";
                for (int i = 0; i < reader.FieldCount - 1; i++)
                {
                    str += "第" + i + "列:" + "(列名:" + reader.GetName(i) + "  类型:" + reader.GetDataTypeName(i) + ") " + reader.GetValue(i) + " | ";
                }
                int temp = reader.FieldCount - 1;
                str += "第 " + temp + " 列:" + "(列名:" + reader.GetName(temp) + "  类型:" + reader.GetDataTypeName(temp) + ") " + reader.GetValue(temp);
                Log(str);
            }
            reader.Close();
        }

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public static void CloseDataBase()
        {
            //销毁Commend
            if (dbCommand != null)
            {
                dbCommand.Cancel();
            }
            dbCommand = null;
            //销毁Reader
            if (dataReader != null)
            {
                dataReader.Close();
            }
            dataReader = null;
            //销毁Connection
            if (dbConnection != null)
            {
                dbConnection.Close();
            }
            dbConnection = null;
        }

        /// <summary>
        /// 读取整张数据表
        /// </summary>
        /// <returns>The full table.</returns>
        /// <param name="tableName">数据表名称</param>
        public static SQLiteDataReader ReadFullTable(string tableName)
        {
            string queryString = "SELECT * FROM " + tableName;
            return ExecuteQuery(queryString);
        }

        /// <summary>
        /// 创建数据表
        /// 简易的表，可以拓展
        /// </summary> 
        /// <returns>The table.</returns>
        /// <param name="tableName">数据表名</param>
        /// <param name="colNames">字段名</param>
        /// <param name="colTypes">字段名类型</param>
        public  static SQLiteDataReader CreateTable(string tableName, string[] colNames, string[] colTypes)
        {
            SQLiteCommand dbCommand = dbConnection.CreateCommand();
            dbCommand.CommandText = "SELECT COUNT(*) FROM SQLITE_MASTER WHERE TYPE = 'table' AND NAME = " + "'"+tableName+"'";
            SQLiteDataReader dataReader = dbCommand.ExecuteReader();
            bool isExit = false;
            while (dataReader.Read())
            {
                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    if (dataReader.GetValue(i).ToString() == "1")
                    {
                        isExit = true;
                    }
                }
            }
            dataReader.Close(); // 当reader不关闭时，cmd不能执行之后的任务
            dataReader = null;
            if (isExit)
            {
                Log("此表格已存在");
                return ReadFullTable(tableName);
            }
            else
            {
                string queryString = "CREATE TABLE IF NOT EXISTS " + tableName + "( " + colNames[0] + " " + colTypes[0];
                for (int i = 1; i < colNames.Length; i++)
                {
                    queryString += ", " + colNames[i] + " " + colTypes[i];
                }
                queryString += "  ) ";
                return ExecuteQuery(queryString);
            }
        }


        /// <summary>
        /// 向指定数据表中插入数据，一条一条的插入
        /// </summary>
        /// <returns>The values.</returns>
        /// <param name="tableName">数据表名称</param>
        /// <param name="values">插入的数值</param>
        public static int InsertData(string tableName, string[] values)
        {
            // 参数检查
            if (string.IsNullOrEmpty(tableName)) return 0;
            //获取数据表中字段数目
            int fieldCount = ReadFullTable(tableName).FieldCount;
            //当插入的数据长度不等于字段数目时引发异常
            if (values.Length != fieldCount)
            {
                throw new SQLiteException("values.Length!=fieldCount");
            }

            string queryString = "INSERT INTO " + tableName + " VALUES (" + "'" + values[0] + "'";
            for (int i = 1; i < values.Length; i++)
            {
                queryString += ", " + "'" + values[i] + "'";
            }
            queryString += " )";
            return ExecuteQuery(queryString,Type.NonQuery);
        }


        /// <summary>
        /// 删除指定数据表内的数据
        ///  功能：多个条件（OR 或）
        /// </summary>
        /// <returns>The values.</returns>
        /// <param name="tableName">数据表名称</param>
        /// <param name="colNames">字段名</param>
        /// <param name="colValues">字段名对应的数据</param>
        /// <param name="operations">运算符</param>
        public static int DeleteDataOR(string tableName, string[] colNames, string[] colValues, string[] operations)
        {
            //当字段名称和字段数值不对应时引发异常
            if (colNames.Length != colValues.Length || operations.Length != colNames.Length || operations.Length != colValues.Length)
            {
                throw new SQLiteException("colNames.Length!=colValues.Length || operations.Length!=colNames.Length || operations.Length!=colValues.Length");
            }

            string queryString = "DELETE FROM " + tableName + " WHERE " + colNames[0] + operations[0] + "'" + colValues[0] + "'";
            for (int i = 1; i < colValues.Length; i++)
            {
                queryString += "OR " + colNames[i] + operations[i] + "'" + colValues[i] + "'";
            }
            return ExecuteQuery(queryString,Type.NonQuery);
        }

        /// <summary>
        /// 删除指定数据表内的数据
        /// 功能：多个条件（AND 且）
        /// </summary>
        /// <returns>The values.</returns>
        /// <param name="tableName">数据表名称</param>
        /// <param name="colNames">字段名</param>
        /// <param name="colValues">字段名对应的数据</param>
        /// /// <param name="operations">运算符</param>
        public static  int DeleteDataAND(string tableName, string[] colNames, string[] colValues, string[] operations)
        {
            //当字段名称和字段数值不对应时引发异常
            if (colNames.Length != colValues.Length || operations.Length != colNames.Length || operations.Length != colValues.Length)
            {
                throw new SQLiteException("colNames.Length!=colValues.Length || operations.Length!=colNames.Length || operations.Length!=colValues.Length");
            }

            string queryString = "DELETE FROM " + tableName + " WHERE " + colNames[0] + operations[0] + "'" + colValues[0] + "'";
            for (int i = 1; i < colValues.Length; i++)
            {
                queryString += " AND " + colNames[i] + operations[i] + "'" + colValues[i] + "'";
            }
            return ExecuteQuery(queryString,Type.NonQuery);
        }


        /// <summary>
        /// 更新指定数据表内的数据
        /// 功能：多个条件（OR 或）
        /// </summary>
        /// <returns>The values.</returns>
        /// <param name="tableName">数据表名称</param>
        /// <param name="colNames">字段名</param>
        /// <param name="colValues">字段名对应的数据</param>
        /// <param name="key">关键字</param>
        /// <param name="value">关键字对应的值</param>
        /// <param name="operation">运算符：= , < , >,...</param>
        public static int UpdateDataOR(string tableName, string[] colNames, string[] colValues, string[] key, string[] value, string[] operations)
        {
            //当字段名称和字段数值不对应时引发异常
            if (colNames.Length != colValues.Length)
            {
                throw new SQLiteException("colNames.Length!=colValues.Length");
            }
            if (key.Length != value.Length || operations.Length != key.Length || operations.Length != value.Length)
            {
                throw new SQLiteException("key.Length!=value.Length || operations.Length!=key.Length || operations.Length!=value.Length");
            }
            string queryString = "UPDATE " + tableName + " SET " + colNames[0] + "=" + "'" + colValues[0] + "'";
            for (int i = 1; i < colValues.Length; i++)
            {
                queryString += ", " + colNames[i] + "=" + "'" + colValues[i] + "'";
            }
            queryString += " WHERE " + key[0] + operations[0] + "'" + value[0] + "'";
            for (int i = 1; i < key.Length; i++)
            {
                queryString += " OR " + key[i] + operations[i] + "'" + value[i] + "'";
            }
            return ExecuteQuery(queryString,Type.NonQuery);
        }

        /// <summary>
        /// 更新指定数据表内的数据
        /// 功能：多个条件（AND 且）
        /// </summary>
        /// <returns>The values.</returns>
        /// <param name="tableName">数据表名称</param>
        /// <param name="colNames">字段名</param>
        /// <param name="colValues">字段名对应的数据</param>
        /// <param name="key">关键字</param>
        /// <param name="value">关键字对应的值</param>
        /// <param name="operation">运算符：= , < , >,...</param>
        public static int UpdateDataAND(string tableName, string[] colNames, string[] colValues, string[] key, string[] value, string[] operations)
        {
            //当字段名称和字段数值不对应时引发异常
            if (colNames.Length != colValues.Length)
            {
                throw new SQLiteException("colNames.Length!=colValues.Length");
            }
            if (key.Length != value.Length || operations.Length != key.Length || operations.Length != value.Length)
            {
                throw new SQLiteException("key.Length!=value.Length || operations.Length!=key.Length || operations.Length!=value.Length");
            }
            string queryString = "UPDATE " + tableName + " SET " + colNames[0] + "=" + "'" + colValues[0] + "'";
            for (int i = 1; i < colValues.Length; i++)
            {
                queryString += ", " + colNames[i] + "=" + "'" + colValues[i] + "'";
            }
            queryString += " WHERE " + key[0] + operations[0] + "'" + value[0] + "'";
            for (int i = 1; i < key.Length; i++)
            {
                queryString += " AND " + key[i] + operations[i] + "'" + value[i] + "'";
            }
            return ExecuteQuery(queryString,Type.NonQuery);
        }

        /// <summary>
        /// 查找指定数据表内的数据
        /// 功能：多个条件（AND 且）
        /// </summary>
        /// <returns>The table.</returns>
        /// <param name="tableName">数据表名称</param>
        /// <param name="keys">目标列</param>
        /// <param name="colNames">参考字段</param>
        /// <param name="colValues">字段名对应的数据</param>
        /// <param name="operations">运算符：= , < , >,...</param>
        public static SQLiteDataReader SelectDataAND(string tableName, string[] keys, string[] colNames, string[] colValues, string[] operations)
        {
            //当字段名称和字段数值不对应时引发异常
            if (colNames.Length != colValues.Length || operations.Length != colNames.Length || operations.Length != colValues.Length)
            {
                throw new SQLiteException("colNames.Length!=colValues.Length || operations.Length!=colNames.Length || operations.Length!=colValues.Length");
            }
            string queryString = "SELECT " + keys[0];
            for (int i = 1; i < keys.Length; i++)
            {
                queryString += ", " + keys[i];
            }
            queryString += " FROM " + tableName + " WHERE " + colNames[0]  + operations[0] + "'" + colValues[0] + "'";
            for (int i = 0; i < colNames.Length; i++)
            {
                queryString += " AND " + colNames[i] + operations[i] + "' " + colValues[i] + "' ";
            }
            return ExecuteQuery(queryString);
        }

        /// <summary>
        /// 查找指定数据表内的数据
        /// 功能：多个条件（OR 或）
        /// </summary>
        /// <returns>The table.</returns>
        /// <param name="tableName">数据表名称</param>
        /// <param name="keys">目标列</param>
        /// <param name="colNames">参考字段</param>
        /// <param name="colValues">字段名对应的数据</param>
        /// <param name="operations">运算符：= , < , >,...</param>
        public static SQLiteDataReader SelectDataOR(string tableName, string[] keys, string[] colNames, string[] colValues, string[] operations)
        {
            //当字段名称和字段数值不对应时引发异常
            if (colNames.Length != colValues.Length || operations.Length != colNames.Length || operations.Length != colValues.Length)
            {
                throw new SQLiteException("colNames.Length!=colValues.Length || operations.Length!=colNames.Length || operations.Length!=colValues.Length");
            }
            string queryString = "SELECT " + keys[0];
            for (int i = 1; i < keys.Length; i++)
            {
                queryString += ", " + keys[i];
            }
            queryString += " FROM " + tableName + " WHERE " + colNames[0] + operations[0] + "'" + colValues[0] + "'";
            for (int i = 0; i < colNames.Length; i++)
            {
                queryString += " OR " + colNames[i] + operations[i] + "' " + colValues[i] + "' ";
            }
            return ExecuteQuery(queryString);
        }


        /// <summary>
        /// 本类log
        /// </summary>
        /// <param name="s"></param>
        public static void Log(string info)
        {
            Console.WriteLine("class SqLiteHelper:::" + info);
        }

    }
}
