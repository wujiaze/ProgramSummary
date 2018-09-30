/*
 *          主题： 连接SQLite数据库的帮助类
 *
 *          使用方法: 添加 System.Data.SQLite.dll 引用
 *                    直接使用本脚本的静态方法
 *          operation  参考属性的运算符：  =  <   >  !=  <=  >=
 */
using System;
using System.Data.SQLite;
using System.Text;

namespace SqliteTool
{
    /// <summary>
    /// SQLite 中常用的数据类型
    ///       用于创建表格
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
    /// 字段的是否可以为Null
    ///      用于创建表格
    /// </summary>
    public enum FieldState
    {
        Null
    }
    /// <summary>
    /// 操作类型
    /// </summary>
    public enum OperationType
    {
        Insert,
        Delete,
        Update,
        Quiry
    }

    /// <summary>
    /// SQLite 操作帮助类
    /// </summary>
    public class SQLiteHelper
    {
        /// <summary>
        /// 数据库连接定义
        /// </summary>
        private static SQLiteConnection _sqliteConnection;

        /// <summary>
        /// SQL命令定义
        /// </summary>
        private static SQLiteCommand _sqliteCommand;

        /// <summary>
        /// 数据读取定义
        /// </summary>
        private static SQLiteDataReader _sqliteReader;


        /// <summary>
        /// SQL语句的载体
        /// </summary>
        private static StringBuilder _strBuilder = new StringBuilder();

        /// <summary>
        /// 显示语句的载体
        /// </summary>
        private static StringBuilder _strShowBuilder = new StringBuilder();



        #region 打开/关闭数据库
        /// <summary>
        /// 连接并创建/打开数据库
        ///    路径可以是全路径，也可以是相对路径（bin/Debug）
        /// </summary>
        /// <param name="sqlitePath">数据库路径</param>
        public static void OpenDataBase(string sqlitePath)
        {
            if (string.IsNullOrEmpty(sqlitePath)) return;
            try
            {
                if (_sqliteConnection != null)
                {
                    throw new Exception("正在连接一个数据库，断开连接凯能连接新的数据库");
                }
                //Pooling = true; FailIfMissing = false
                //路径不存在数据库时，会创建数据库。当只有数据库名时，表示数据库在bin/Debug路径下
                string str = @"Data Source=" + sqlitePath + ";Pooling=true;FailIfMissing=false";
                _sqliteConnection = new SQLiteConnection(str);
                _sqliteConnection.Open();
                Console.WriteLine(sqlitePath + "数据库打开成功");
            }
            catch (Exception e)
            {
                Console.WriteLine(typeof(SQLiteHelper) + " /OpenDataBase" + "数据库打开失败" + e);
            }
        }
        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public static void CloseDataBase()
        {
            try
            {
                //销毁Commend
                if (_sqliteCommand != null)
                    _sqliteCommand.Cancel();
                _sqliteCommand = null;
                //销毁Reader
                if (_sqliteReader != null)
                    _sqliteReader.Close();
                _sqliteReader = null;
                //销毁Connection
                if (_sqliteConnection != null)
                    _sqliteConnection.Close();
                _sqliteConnection = null;
                Console.WriteLine("成功关闭数据库");
            }
            catch (Exception e)
            {
                Console.WriteLine("关闭数据库出现问题" + e.Message);
                throw;
            }

        }

        #endregion


        #region 两个执行方法
        /// <summary>
        /// 执行SQL命令
        /// </summary>
        /// <returns>The query.</returns>
        /// <param name="queryString">SQL命令字符串</param>
        private static SQLiteDataReader ExecuteReader(string queryString)
        {
            try
            {
                _sqliteCommand = _sqliteConnection.CreateCommand();
                _sqliteCommand.CommandText = queryString;
                _sqliteReader = _sqliteCommand.ExecuteReader();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return _sqliteReader;
        }
        /// <summary>
        /// 执行SQL命令
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        private static int ExecuteNonQuery(string queryString, OperationType operationType)
        {
            int count = 0;
            try
            {
                _sqliteCommand = _sqliteConnection.CreateCommand();
                _sqliteCommand.CommandText = queryString;
                count = _sqliteCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            if (count > 0)
                Console.WriteLine(operationType + "操作成功");
            else
                Console.WriteLine(operationType + "操作失败");
            return count;
        }


        #endregion



        #region 创建表格
        /// <summary>
        /// 创建数据表
        /// 简易的表，可以拓展
        /// </summary> 
        /// <returns>The table.</returns>
        /// <param name="tableName">数据表名</param>
        /// <param name="colNames">字段名</param>
        /// <param name="colTypes">字段名类型  FieldType </param>
        /// <param name="colLength">数据的预留长度</param>
        /// <param name="colState">数据的可设置状态 FieldState</param>
        public static SQLiteDataReader CreateTable(string tableName, string[] colNames, string[] colTypes, string[] colLength, string[] colState)
        {
            SQLiteCommand dbCommand = _sqliteConnection.CreateCommand();
            dbCommand.CommandText = "SELECT COUNT(*) FROM SQLITE_MASTER WHERE TYPE = 'table' AND NAME = " + "'" + tableName + "'";
            SQLiteDataReader dataReader = dbCommand.ExecuteReader();
            bool isExit = false;
            while (dataReader.Read())    // 每次循环读取一条数据，这里只要一条数据，所以只循环一次
            {
                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    if (dataReader.GetValue(i).ToString() == "1")
                    {
                        isExit = true;
                        break;
                    }
                }
            }
            dataReader.Close(); // 当reader不关闭时，cmd不能执行之后的任务
            dataReader = null;
            if (isExit)
            {
                Console.WriteLine("此表格已存在，返回已存在的表格");
                return ReadFullTable(tableName);
            }
            else
            {
                _strBuilder.Clear();
                _strBuilder.Append("CREATE TABLE IF NOT EXISTS " + tableName + "( " + colNames[0] + " " + colTypes[0] +
                                   "(" + colLength[0] + ")" +
                                   " DEFAULT " + colState[0]);
                for (int i = 1; i < colNames.Length; i++)
                {
                    _strBuilder.Append("," + colNames[i] + " " + colTypes[i] + "(" + colLength[i] + ")" + " DEFAULT " + colState[i]);
                }
                _strBuilder.Append("  );");
                Console.WriteLine(tableName + " 表格创建成功");
                return ExecuteReader(_strBuilder.ToString());
            }
        }


        #endregion


        #region 插入数据
        /// <summary>
        /// 向指定数据表中插入数据，
        ///  提示：一条一条的插入
        /// </summary>
        /// <returns>The values.</returns>
        /// <param name="tableName">数据表名称</param>
        /// <param name="values">插入的一条数据的所有字段值</param>
        public static int InsertData(string tableName, string[] values)
        {
            // 参数检查
            if (string.IsNullOrEmpty(tableName)) return 0;
            //获取数据表中字段数目
            int fieldCount = ReadFullTable(tableName).FieldCount;
            //当插入的数据长度不等于字段数目时引发异常
            if (values.Length != fieldCount)
                throw new SQLiteException("插入的数组长度和数据库的属性个数不相同");
            _strBuilder.Clear();
            _strBuilder.Append("INSERT INTO " + tableName + " VALUES (" + "'" + values[0] + "'");
            for (int i = 1; i < values.Length; i++)
            {
                _strBuilder.Append(", " + "'" + values[i] + "'");
            }
            _strBuilder.Append(" )");
            return ExecuteNonQuery(_strBuilder.ToString(), OperationType.Insert);
        }


        #endregion


        #region 删除数据
        /// <summary>
        /// 删除指定数据表内的数据
        ///     功能:单一条件
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="referKey"></param>
        /// <param name="referValue"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        public static int DeleteData(string tableName, string referKey, string referValue, string operation)
        {
            _strBuilder.Clear();
            _strBuilder.Append("DELETE FROM " + tableName + " WHERE " + referKey + operation + "'" + referValue + "'");
            return ExecuteNonQuery(_strBuilder.ToString(), OperationType.Delete);
        }


        /// <summary>
        /// 删除指定数据表内的数据
        ///  功能：多个条件（OR 或）
        /// </summary>
        /// <returns>0：执行失败，大于0 表示执行成功</returns>
        /// <param name="tableName">数据表名称</param>
        /// <param name="referKeys">参考属性</param>
        /// <param name="referValues">参考属性的值</param>
        /// <param name="operations">运算符：= , <  >,...</param>
        public static int DeleteDataOr(string tableName, string[] referKeys, string[] referValues, string[] operations)
        {
            //当字段名称和字段数值不对应时引发异常
            bool result = referKeys.Length == referValues.Length && referValues.Length == operations.Length;
            if (result == false)
                throw new SQLiteException("referKeys,referValues,operations 长度不一致");
            _strBuilder.Clear();
            _strBuilder.Append("DELETE FROM " + tableName + " WHERE " + referKeys[0] + operations[0] + "'" + referValues[0] + "'");
            for (int i = 1; i < referValues.Length; i++)
            {
                _strBuilder.Append("OR " + referKeys[i] + operations[i] + "'" + referValues[i] + "'");
            }
            return ExecuteNonQuery(_strBuilder.ToString(), OperationType.Delete);
        }

        /// <summary>
        /// 删除指定数据表内的数据
        /// 功能：多个条件（AND 且）
        /// </summary>
        /// <returns>The values.</returns>
        /// <param name="tableName">数据表名称</param>
        /// <param name="referKeys">参考属性</param>
        /// <param name="referValues">参考属性的值</param>
        /// /// <param name="operations">运算符：= , <  >,...</param>
        public static int DeleteDataAnd(string tableName, string[] referKeys, string[] referValues, string[] operations)
        {
            //当字段名称和字段数值不对应时引发异常
            bool result = referKeys.Length == referValues.Length && referValues.Length == operations.Length;
            if (result == false)
                throw new SQLiteException("referKeys,referValues,operations 长度不一致");
            _strBuilder.Clear();
            _strBuilder.Append("DELETE FROM " + tableName + " WHERE " + referKeys[0] + operations[0] + "'" + referValues[0] + "'");
            for (int i = 1; i < referValues.Length; i++)
            {
                _strBuilder.Append(" AND " + referKeys[i] + operations[i] + "'" + referValues[i] + "'");
            }
            return ExecuteNonQuery(_strBuilder.ToString(), OperationType.Delete);
        }


        #endregion


        #region 更新数据
        /// <summary>
        /// 更新指定数据表内的数据
        ///     功能：单一条件
        /// </summary>
        /// <param name="tableName">数据表名称</param>
        /// <param name="colNames">更新字段</param>
        /// <param name="colValues">更新数据</param>
        /// <param name="referKey">参考属性</param>
        /// <param name="referValue">参考属性的值</param>
        /// <param name="operation">参考属性的运算符：= , <  >,...</param>
        /// <returns></returns>
        public static int UpdateData(string tableName, string[] colNames, string[] colValues, string referKey, string referValue, string operation)
        {
            //当字段名称和字段数值不对应时引发异常
            if (colNames.Length != colValues.Length)
                throw new SQLiteException("referKeys.Length!=referValues.Length");
            _strBuilder.Clear();
            _strBuilder.Append("UPDATE " + tableName + " SET " + colNames[0] + "=" + "'" + colValues[0] + "'");
            for (int i = 1; i < colValues.Length; i++)
            {
                _strBuilder.Append(", " + colNames[i] + "=" + "'" + colValues[i] + "'");
            }
            _strBuilder.Append(" WHERE " + referKey + operation + "'" + referValue + "'");
            return ExecuteNonQuery(_strBuilder.ToString(), OperationType.Update);
        }


        /// <summary>
        /// 更新指定数据表内的数据
        /// 功能：多个条件（或）
        /// </summary>
        /// <returns>The values.</returns>
        /// <param name="tableName">数据表名称</param>
        /// <param name="colNames">更新字段</param>
        /// <param name="colValues">更新数据</param>
        /// <param name="referKeys">参考属性</param>
        /// <param name="referValues">参考属性的值</param>
        /// <param name="operations">参考属性的运算符：= , <  >,...</param>
        public static int UpdateDataOr(string tableName, string[] colNames, string[] colValues, string[] referKeys, string[] referValues, string[] operations)
        {
            //当字段名称和字段数值不对应时引发异常
            if (colNames.Length != colValues.Length)
            {
                throw new SQLiteException("referKeys.Length!=referValues.Length");
            }
            if (referKeys.Length != referValues.Length || operations.Length != referKeys.Length || operations.Length != referValues.Length)
            {
                throw new SQLiteException("referKeys.Length!=referValues.Length || operations.Length!=referKeys.Length || operations.Length!=referValues.Length");
            }
            _strBuilder.Clear();
            _strBuilder.Append("UPDATE " + tableName + " SET " + colNames[0] + "=" + "'" + colValues[0] + "'");
            for (int i = 1; i < colValues.Length; i++)
            {
                _strBuilder.Append(", " + colNames[i] + "=" + "'" + colValues[i] + "'");
            }
            _strBuilder.Append(" WHERE " + referKeys[0] + operations[0] + "'" + referValues[0] + "'");
            for (int i = 1; i < referKeys.Length; i++)
            {
                _strBuilder.Append(" OR " + referKeys[i] + operations[i] + "'" + referValues[i] + "'");
            }
            return ExecuteNonQuery(_strBuilder.ToString(), OperationType.Update);
        }

        /// <summary>
        /// 更新指定数据表内的数据
        /// 功能：多个条件（ 且）
        /// </summary>
        /// <returns>The values.</returns>
        /// <param name="tableName">数据表名称</param>
        /// <param name="colNames">更新字段</param>
        /// <param name="colValues">更新数据</param>
        /// <param name="referKeys">参考属性</param>
        /// <param name="referValues">参考属性的值</param>
        /// <param name="operations">参考属性的运算符：= , < , >,...</param>
        public static int UpdateDataAnd(string tableName, string[] colNames, string[] colValues, string[] referKeys, string[] referValues, string[] operations)
        {
            //当字段名称和字段数值不对应时引发异常
            if (colNames.Length != colValues.Length)
            {
                throw new SQLiteException("referKeys.Length!=referValues.Length");
            }
            if (referKeys.Length != referValues.Length || operations.Length != referKeys.Length || operations.Length != referValues.Length)
            {
                throw new SQLiteException("referKeys.Length!=referValues.Length || operations.Length!=referKeys.Length || operations.Length!=referValues.Length");
            }
            _strBuilder.Clear();
            _strBuilder.Append("UPDATE " + tableName + " SET " + colNames[0] + "=" + "'" + colValues[0] + "'");
            for (int i = 1; i < colValues.Length; i++)
            {
                _strBuilder.Append(", " + colNames[i] + "=" + "'" + colValues[i] + "'");
            }
            _strBuilder.Append(" WHERE " + referKeys[0] + operations[0] + "'" + referValues[0] + "'");
            for (int i = 1; i < referKeys.Length; i++)
            {
                _strBuilder.Append(" AND " + referKeys[i] + operations[i] + "'" + referValues[i] + "'");
            }
            return ExecuteNonQuery(_strBuilder.ToString(), OperationType.Update);
        }


        #endregion

        #region 查询数据
        /// <summary>
        /// 查找指定数据表内的数据
        ///   功能：单一条件查找
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="colNames"></param>
        /// <param name="referKey"></param>
        /// <param name="referValue"></param>
        /// <param name="operations"></param>
        /// <returns></returns>
        public static SQLiteDataReader SelectData(string tableName, string[] colNames, string referKey, string referValue, string operations)
        {
            _strBuilder.Clear();
            _strBuilder.Append("SELECT " + colNames[0]);
            for (int i = 1; i < colNames.Length; i++)
            {
                _strBuilder.Append(", " + colNames[i]);
            }
            _strBuilder.Append(" FROM " + tableName + " WHERE " + referKey + operations + "'" + referValue + "'");
            return ExecuteReader(_strBuilder.ToString());
        }


        /// <summary>
        /// 查找指定数据表内的数据
        /// 功能：多个条件（AND 且）
        /// </summary>
        /// <returns>The table.</returns>
        /// <param name="tableName">数据表名称</param>
        /// <param name="colNames">查找字段</param>
        /// <param name="referKeys">参考属性</param>
        /// <param name="referValues">参考属性的值</param>
        /// <param name="operations">参考属性的运算符：= , <  >,...</param>
        public static SQLiteDataReader SelectDataAnd(string tableName, string[] colNames, string[] referKeys, string[] referValues, string[] operations)
        {
            //当字段名称和字段数值不对应时引发异常
            if (referKeys.Length != referValues.Length || operations.Length != referKeys.Length || operations.Length != referValues.Length)
            {
                throw new SQLiteException("referKeys.Length!=referValues.Length || operations.Length!=referKeys.Length || operations.Length!=referValues.Length");
            }
            _strBuilder.Clear();
            _strBuilder.Append("SELECT " + colNames[0]);
            for (int i = 1; i < colNames.Length; i++)
            {
                _strBuilder.Append(", " + colNames[i]);
            }
            _strBuilder.Append(" FROM " + tableName + " WHERE " + referKeys[0] + operations[0] + "'" + referValues[0] + "'");
            for (int i = 0; i < referKeys.Length; i++)
            {
                _strBuilder.Append(" AND " + referKeys[i] + operations[i] + "' " + referValues[i] + "' ");
            }
            return ExecuteReader(_strBuilder.ToString());
        }

        /// <summary>
        /// 查找指定数据表内的数据
        /// 功能：多个条件（OR 或）
        /// </summary>
        /// <returns>The table.</returns>
        /// <param name="tableName">数据表名称</param>
        /// <param name="colNames">查找字段</param>
        /// <param name="referKeys">参考属性</param>
        /// <param name="referValues">参考属性的值</param>
        /// <param name="operations">参考属性的运算符：= , < , >,...</param>
        public static SQLiteDataReader SelectDataOr(string tableName, string[] colNames, string[] referKeys, string[] referValues, string[] operations)
        {
            //当字段名称和字段数值不对应时引发异常
            if (referKeys.Length != referValues.Length || operations.Length != referKeys.Length || operations.Length != referValues.Length)
            {
                throw new SQLiteException("referKeys.Length!=referValues.Length || operations.Length!=referKeys.Length || operations.Length!=referValues.Length");
            }
            _strBuilder.Clear();
            _strBuilder.Append("SELECT " + colNames[0]);
            for (int i = 1; i < colNames.Length; i++)
            {
                _strBuilder.Append(", " + colNames[i]);
            }
            _strBuilder.Append(" FROM " + tableName + " WHERE " + referKeys[0] + operations[0] + "'" + referValues[0] + "'");
            for (int i = 0; i < referKeys.Length; i++)
            {
                _strBuilder.Append(" OR " + referKeys[i] + operations[i] + "' " + referValues[i] + "' ");
            }
            return ExecuteReader(_strBuilder.ToString());
        }


        #endregion


        #region 辅助方法

        /// <summary>
        /// 显示命令效果
        /// </summary>
        /// <param name="reader"></param>
        public static void ShowData(SQLiteDataReader reader)
        {

            if (reader == null) return;
            bool result = false;
            while (reader.Read())
            {
                result = true;
                _strShowBuilder.Clear();
                _strShowBuilder.Append("命令执行成功: ");
                for (int i = 0; i < reader.FieldCount - 1; i++)
                {
                    _strShowBuilder.Append("第" + i + "列:" + "(列名:" + reader.GetName(i) + "  类型:" + reader.GetDataTypeName(i) + ") " + reader.GetValue(i) + " | ");
                }
                int temp = reader.FieldCount - 1;
                _strShowBuilder.Append("第 " + temp + " 列:" + "(列名:" + reader.GetName(temp) + "  类型:" + reader.GetDataTypeName(temp) + ") " + reader.GetValue(temp));
                Console.WriteLine(_strShowBuilder.ToString());
            }
            if (result == false)
                Console.WriteLine("该命令，没有获取任何数据");
            reader.Close();
        }



        /// <summary>
        /// 读取整张数据表
        /// </summary>
        /// <returns>The full table.</returns>
        /// <param name="tableName">数据表名称</param>
        private static SQLiteDataReader ReadFullTable(string tableName)
        {
            string queryString = "SELECT * FROM " + tableName;
            return ExecuteReader(queryString);
        }

        #endregion



    }
}
