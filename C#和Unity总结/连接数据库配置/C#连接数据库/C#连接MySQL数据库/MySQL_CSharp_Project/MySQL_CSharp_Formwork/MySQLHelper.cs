/*
 *
 *      Title: "C#连接MySQL"帮助类
 *          提示： 返回 MySqlDataReader 的方法，最好在 MySqlDataReader 对象读取之后，关闭  MySqlDataReader 对象
 *                同时，在调用返回 MySqlDataReader 的方法，没有关闭 MySqlDataReader 对象 之前，最好不要调用 返回 MySqlDataReader 的方法，需要先关闭MySqlDataReader 对象，否则数据容易出错
 *
 */
using System;
using MySql.Data.MySqlClient;

namespace MySQL_CSharp_Formwork
{
    public enum Type
    {
        NonQuery // MySqlCommand 对象执行 ExecuteNonQuery() 方法
    }

    /// <summary>
    /// MySQL中常用的数据类型
    /// </summary>
    public enum FieldType
    {
        VarChar,    // 代表字符串
        Int,        // 代表整数
        Double,     // 双精度浮点数
        Float,      // 单精度浮点数
        DateTime    // 日期加时间
    }
    /// <summary>
    /// 字段的状态
    /// </summary>
    public enum FieldState
    {
        Null
    }

    /// <summary>
    /// MySQL 操作类
    /// </summary>
    public class MySQLHelper
    {
        /// <summary>
        /// 数据库连接定义
        /// </summary>
        private static MySqlConnection dbConnection;

        /// <summary>
        /// SQL命令定义
        /// </summary>
        private static MySqlCommand dbCommand;

        /// <summary>
        /// 数据读取定义
        /// </summary>
        private static MySqlDataReader dataReader;
        /// <summary>
        /// 当前操作的数据库
        /// </summary>
        private static string _CurrentdataBaseName;

        /// <summary>
        /// 连接并打开数据库
        /// 路径不存在数据库时，会创建数据库。
        /// </summary>
        /// <param name="host">主机名/IP</param>
        /// <param name="port">端口号</param>
        /// <param name="databaseName">数据库名</param>
        /// <param name="userName">用户</param>
        /// <param name="passwd">密码</param>
        public static void OpenDataBase(string host,int port,string databaseName,string userName,string passwd)
        {
            if (string.IsNullOrEmpty(databaseName)) return;
            try
            {
                if (dbConnection != null)
                {
                    Exception e = new Exception("dbConnection");
                    throw e;
                }
                
                string str = String.Format("Host = {0};port ={1};UID ={2};PWD={3};Database ={4};", host, port, userName, passwd, databaseName);
                try
                {
                    dbConnection = new MySqlConnection(str);
                    dbConnection.Open();
                    _CurrentdataBaseName = databaseName;
                    Console.WriteLine("成功连接数据库");
                }
                catch (Exception e)
                {
                    dbConnection.Close();
                    try
                    {
                        string tmpstr = String.Format("Host = {0};port ={1};UID ={2};PWD={3};", host, port, userName, passwd);
                        dbConnection = new MySqlConnection(tmpstr);
                        dbCommand = new MySqlCommand("create database " + databaseName + ";", dbConnection);
                        dbConnection.Open();
                        dbCommand.ExecuteNonQuery();
                        _CurrentdataBaseName = databaseName;
                        Console.WriteLine("数据库创建并连接成功");
                        // 需要断开连接（因为这时的连接，没有连接到数据库，只是创建了数据库）需要重新连接到数据库
                        dbConnection.Close(); 
                        dbConnection = new MySqlConnection(str);
                        dbConnection.Open();
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);
                    }
                }
            }
            catch (MySqlException e)
            {
                throw new Exception(databaseName+"数据库打开失败",e);
            }

        }

        /// <summary>
        /// 执行SQL命令
        /// </summary>
        /// <returns>The query.</returns>
        /// <param name="queryString">SQL命令字符串</param>
        /// <param name="message">提示信息</param>
        private static MySqlDataReader ExecuteQuery(string queryString, string message = null)
        {
            try
            {
                if(dataReader!=null&& dataReader.IsClosed==false) dataReader.Close();
                dbCommand = dbConnection.CreateCommand();
                dbCommand.CommandText = queryString;
                dataReader = dbCommand.ExecuteReader();
                if (!string.IsNullOrEmpty(message))
                    Console.WriteLine(message+"成功");
            }
            catch (Exception e)
            {
                Log(message+"失败"+e.Message);
            }
            return dataReader;
        }

        /// <summary>
        /// 执行SQL命令
        /// </summary>
        /// <param name="queryString">SQL命令字符串</param>
        /// <param name="type">执行SQL命令的方式</param>
        /// <param name="message">提示信息</param>
        /// <returns></returns>
        private static int ExecuteQuery(string queryString, Type type, string message = null)
        {
            int count = 0;
            try
            {
                if (dataReader != null && dataReader.IsClosed == false) dataReader.Close();
                dbCommand = dbConnection.CreateCommand();
                dbCommand.CommandText = queryString;
                count = dbCommand.ExecuteNonQuery();
                Console.WriteLine("成功"+ message + count+"次");
            }
            catch (Exception e)
            {
                Log(e.Message);
            }
            return count;
        }

        /// <summary>
        /// 显示命令效果
        /// 简单的数据显示 ，todo 获取行序列
        /// </summary>
        /// <param name="reader"></param>
        public static void ShowData(MySqlDataReader reader)
        {

            if (reader == null) return;
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
            reader = null;
        }

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public static void CloseDataBase()
        {
            try
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
                    Console.WriteLine("成功关闭数据库");
                }
                dbConnection = null;
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// 读取整张数据表
        /// </summary>
        /// <returns>The full table.</returns>
        /// <param name="tableName">数据表名称</param>
        public static MySqlDataReader ReadFullTable(string tableName)
        {
            string queryString = "SELECT * FROM " + tableName;
            return ExecuteQuery(queryString);
        }

        /// <summary>
        /// 创建数据表(简单的表)
        /// 复杂的表，建议直接在数据库创建好再使用
        /// </summary> 
        /// <returns>The table.</returns>
        /// <param name="tableName">数据表名</param>
        /// <param name="colNames">字段名</param>
        /// <param name="colTypes">字段名类型</param>
        /// <param name="colLength">数据的预留长度</param>
        /// <param name="colState">数据的可设置状态</param>
        public static MySqlDataReader CreateTable(string tableName, string[] colNames, string[] colTypes,string[] colLength,string[] colState)
        {
            if (colNames.Length != colTypes.Length && colNames.Length != colLength.Length &&
                colNames.Length != colState.Length && colTypes.Length != colLength.Length &&
                colTypes.Length != colState.Length && colLength.Length != colState.Length)
            {
                Console.WriteLine("colNames colTypes colLength colState length not equail");
                return null;
            }
            dbCommand = dbConnection.CreateCommand();
            dbCommand.CommandText = string.Format("select COUNT(*) from information_schema.TABLES where TABLE_SCHEMA ='{0}'AND table_name ='{1}'", _CurrentdataBaseName, tableName); /*"select COUNT(*) from information_schema.TABLES where TABLE_SCHEMA = \'" + _CurrentdataBaseName + "\'  AND table_name = \'" + tableName + "\'";*/
            if (dataReader!=null && dataReader.IsClosed == false)
            {
                dataReader.Close();
                dataReader = null;
            }
            dataReader = dbCommand.ExecuteReader();
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
            dataReader.Close(); 
            dataReader = null;
            if (isExit)
            {
                Log("此表格已存在");
                return null;
            }
            else
            {
                string queryString = "CREATE TABLE IF NOT EXISTS " + tableName +
                                     "( Id int(11) NOT NULL AUTO_INCREMENT," + colNames[0] + " " + colTypes[0] +"("+ colLength[0]+")"+
                                     " DEFAULT "+ colState[0]+",";
                for (int i = 1; i < colNames.Length; i++)
                {
                    queryString +=  colNames[i] + " " + colTypes[i] + "(" + colLength[i] + ")" +" DEFAULT " + colState[i] + ",";
                }
                queryString += " PRIMARY KEY(`Id`)) ENGINE = InnoDB DEFAULT CHARSET = utf8;";
                return ExecuteQuery(queryString.ToLower(), "创建表格");
            }
        }


        /// <summary>
        /// 向指定数据表中插入数据，一条一条的插入
        /// </summary>
        /// <returns>The values.</returns>
        /// <param name="tableName">数据表名称</param>
        /// <param name="colName">数据表的字段名</param>
        /// <param name="values">插入的数值</param>
        public static int InsertData(string tableName,string[] colName, string[] values)
        {
            // 参数检查
            if (string.IsNullOrEmpty(tableName)) return 0;
            //获取数据表中字段数目
            int fieldCount = ReadFullTable(tableName).FieldCount;
            //当插入的数据长度不等于字段数目时引发异常
            if (values.Length != fieldCount-1)
            {
                throw new Exception("values.Length!=fieldCount");
            }

            string queryString = "INSERT INTO " + tableName + "("+ colName[0];
            for (int i = 1; i < colName.Length; i++)
            {
                queryString +=  ","+ colName[i];
            }
            queryString += ")"+ " VALUES(" +"'" + values[0] + "'";
            for (int i = 1; i < values.Length; i++)
            {
                queryString += ", " + "'" + values[i] + "'";
            }
            queryString += " )";
            return ExecuteQuery(queryString.ToLower(), Type.NonQuery,"插入数据");
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
                throw new Exception("colNames.Length!=colValues.Length || operations.Length!=colNames.Length || operations.Length!=colValues.Length");
            }

            string queryString = "DELETE FROM " + tableName + " WHERE " + colNames[0] + operations[0] + "'" + colValues[0] + "'";
            for (int i = 1; i < colValues.Length; i++)
            {
                queryString += "OR " + colNames[i] + operations[i] + "'" + colValues[i] + "'";
            }
            return ExecuteQuery(queryString, Type.NonQuery, "删除数据");
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
        public static int DeleteDataAND(string tableName, string[] colNames, string[] colValues, string[] operations)
        {
            //当字段名称和字段数值不对应时引发异常
            if (colNames.Length != colValues.Length || operations.Length != colNames.Length || operations.Length != colValues.Length)
            {
                throw new Exception("colNames.Length!=colValues.Length || operations.Length!=colNames.Length || operations.Length!=colValues.Length");
            }

            string queryString = "DELETE FROM " + tableName + " WHERE " + colNames[0] + operations[0] + "'" + colValues[0] + "'";
            for (int i = 1; i < colValues.Length; i++)
            {
                queryString += " AND " + colNames[i] + operations[i] + "'" + colValues[i] + "'";
            }
            return ExecuteQuery(queryString, Type.NonQuery, "删除数据");
        }


        /// <summary>
        /// 更新指定数据表内的数据
        /// 功能：多个条件（OR 或）
        /// </summary>
        /// <returns>The values.</returns>
        /// <param name="tableName">数据表名称</param>
        /// <param name="colNames">需要更新的字段名</param>
        /// <param name="colValues">需要更新的字段名对应的数据</param>
        /// <param name="key">依据的关键字</param>
        /// <param name="value">依据的关键字对应的值</param>
        /// <param name="operation">依据运算符：= , < , >,...</param>
        public static int UpdateDataOR(string tableName, string[] colNames, string[] colValues, string[] key, string[] value, string[] operations)
        {
            //当字段名称和字段数值不对应时引发异常
            if (colNames.Length != colValues.Length)
            {
                throw new Exception("colNames.Length!=colValues.Length");
            }
            if (key.Length != value.Length || operations.Length != key.Length || operations.Length != value.Length)
            {
                throw new Exception("key.Length!=value.Length || operations.Length!=key.Length || operations.Length!=value.Length");
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
            return ExecuteQuery(queryString, Type.NonQuery, "更新数据");
        }

        /// <summary>
        /// 更新指定数据表内的数据
        /// 功能：多个条件（AND 且）
        /// </summary>
        /// <returns>The values.</returns>
        /// <param name="tableName">数据表名称</param>
        /// <param name="colNames">需要更新的字段名</param>
        /// <param name="colValues">需要更新的字段名对应的数据</param>
        /// <param name="key">依据的关键字</param>
        /// <param name="value">依据的关键字对应的值</param>
        /// <param name="operation">依据运算符：= , < , >,...</param>
        public static int UpdateDataAND(string tableName, string[] colNames, string[] colValues, string[] key, string[] value, string[] operations)
        {
            //当字段名称和字段数值不对应时引发异常
            if (colNames.Length != colValues.Length)
            {
                throw new Exception("colNames.Length!=colValues.Length");
            }
            if (key.Length != value.Length || operations.Length != key.Length || operations.Length != value.Length)
            {
                throw new Exception("key.Length!=value.Length || operations.Length!=key.Length || operations.Length!=value.Length");
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
            return ExecuteQuery(queryString, Type.NonQuery, "更新数据");
        }

        /// <summary>
        /// 查找指定数据表内的数据
        /// 功能：多个条件（AND 且）
        /// </summary>
        /// <returns>The table.</returns>
        /// <param name="tableName">数据表名称</param>
        /// <param name="keys">查找目标列</param>
        /// <param name="colNames">依据的字段</param>
        /// <param name="colValues">依据的字段名对应的数据</param>
        /// <param name="operations">依据的运算符：= , < , >,...</param>
        public static MySqlDataReader SelectDataAND(string tableName, string[] keys, string[] colNames, string[] colValues, string[] operations)
        {
            //当字段名称和字段数值不对应时引发异常
            if (colNames.Length != colValues.Length || operations.Length != colNames.Length || operations.Length != colValues.Length)
            {
                throw new Exception("colNames.Length!=colValues.Length || operations.Length!=colNames.Length || operations.Length!=colValues.Length");
            }
            string queryString = "SELECT " + keys[0];
            for (int i = 1; i < keys.Length; i++)
            {
                queryString += ", " + keys[i];
            }
            queryString += " FROM " + tableName + " WHERE " + colNames[0] + operations[0] + "'" + colValues[0] + "'";
            for (int i = 0; i < colNames.Length; i++)
            {
                queryString += " AND " + colNames[i] + operations[i] + "' " + colValues[i] + "' ";
            }
            return ExecuteQuery(queryString,"查找数据");
        }

        /// <summary>
        /// 查找指定数据表内的数据
        /// 功能：多个条件（OR 或）
        /// </summary>
        /// <returns>The table.</returns>
        /// <param name="tableName">数据表名称</param>
        /// <param name="keys">查找目标列</param>
        /// <param name="colNames">依据的字段</param>
        /// <param name="colValues">依据的字段名对应的数据</param>
        /// <param name="operations">依据的运算符：= , < , >,...</param>
        public static MySqlDataReader SelectDataOR(string tableName, string[] keys, string[] colNames, string[] colValues, string[] operations)
        {
            //当字段名称和字段数值不对应时引发异常
            if (colNames.Length != colValues.Length || operations.Length != colNames.Length || operations.Length != colValues.Length)
            {
                throw new Exception("colNames.Length!=colValues.Length || operations.Length!=colNames.Length || operations.Length!=colValues.Length");
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
            return ExecuteQuery(queryString,"查找数据");
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
