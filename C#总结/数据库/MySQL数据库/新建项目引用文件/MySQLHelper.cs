/*
 *
 *      Title: C#连接MySQL数据库的帮助类
 *
 *          使用方法:添加 MySql.Data.dll 
 *                  直接使用本脚本的静态方法
 *                  启动MySQL数据库服务
 *                  operation  参考属性的运算符：  =  <   >  !=  <=  >=
 *              
 *          提示： 返回 MySqlDataReader 的方法，最好在 MySqlDataReader 对象读取之后，关闭  MySqlDataReader 对象
 *                同时，在调用返回 MySqlDataReader 的方法，没有关闭 MySqlDataReader 对象 之前，
 *                最好不要调用 返回 MySqlDataReader 的方法，需要先关闭MySqlDataReader 对象，否则数据容易出错
 *
 *          注意点：创建表格时，不能添加字段Id，这个字段MySql数据可以会自己添加
 *                 不能对Id进行操作
 * 
 */
using System;
using System.Text;
using MySql.Data.MySqlClient;

namespace MySQLTool
{
    /// <summary>
    /// MySQL 操作类
    /// </summary>
    public class MySQLHelper
    {
        /// <summary>
        /// 数据库连接定义
        /// </summary>
        private static MySqlConnection _mysqlConnection;

        /// <summary>
        /// SQL命令定义
        /// </summary>
        private static MySqlCommand _mysqlCommand;

        /// <summary>
        /// 数据读取定义
        /// </summary>
        private static MySqlDataReader _mysqlReader;
        /// <summary>
        /// 当前操作的数据库
        ///   用于简化创建表的操作
        /// </summary>
        private static string _currentDataBaseName;

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
        /// 连接并打开数据库
        /// </summary>
        /// <param name="dbName">数据库名</param>
        /// <param name="host">IP</param>
        /// <param name="port">端口号</param>
        /// <param name="userName">用户</param>
        /// <param name="passwd">密码</param>
        public static void OpenDataBase(string dbName, string host, int port, string userName, string passwd)
        {
            // 参数检查
            if (string.IsNullOrEmpty(dbName) || string.IsNullOrEmpty(host) || string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(passwd))
                throw new Exception("参数不能为空/null");
            try
            {
                if (_mysqlConnection != null)
                    throw new Exception("一个数据库连接中，断开连接才能连接新的数据库");
                string connectionString = String.Format("Host = {0};port ={1};UID ={2};PWD={3};Database ={4};", host, port, userName, passwd, dbName);
                try
                {
                    _mysqlConnection = new MySqlConnection(connectionString);
                    _mysqlConnection.Open();
                    _currentDataBaseName = dbName;
                    Console.WriteLine(dbName + "数据库打开成功");
                }
                catch (Exception e)
                {
                    if (_mysqlConnection != null)
                        _mysqlConnection.Close();
                    try
                    {
                        string tmpstr = String.Format("Host = {0};port ={1};UID ={2};PWD={3};", host, port, userName, passwd);
                        _mysqlConnection = new MySqlConnection(tmpstr);
                        _mysqlCommand = new MySqlCommand("create database " + dbName + ";", _mysqlConnection);
                        _mysqlConnection.Open();
                        int count = _mysqlCommand.ExecuteNonQuery();
                        if (count > 0)
                        {
                            Console.WriteLine("数据库创建成功");
                            // 需要断开连接（因为这时的连接，没有连接到数据库，只是创建了数据库）需要重新连接到数据库
                            _mysqlConnection.Close();
                            // 重新连接
                            _mysqlConnection = new MySqlConnection(connectionString);
                            _mysqlConnection.Open();
                            _currentDataBaseName = dbName;
                            Console.WriteLine("数据库打开成功");
                        }
                        else
                        {
                            Console.WriteLine("数据库创建失败");
                        }

                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);
                        throw;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(dbName + "数据库打开失败", e);
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
                if (_mysqlCommand != null)
                    _mysqlCommand.Cancel();
                _mysqlCommand = null;
                //销毁Reader
                if (_mysqlReader != null)
                    _mysqlReader.Close();
                _mysqlReader = null;
                //销毁Connection
                if (_mysqlConnection != null)
                    _mysqlConnection.Close();
                _mysqlConnection = null;
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
        /// <param name="message">提示信息</param>
        private static MySqlDataReader ExecuteReader(string queryString, string message = null)
        {
            if (string.IsNullOrEmpty(queryString))
                throw new Exception("参数不能为空");
            try
            {
                ResetReader(_mysqlReader);
                _mysqlCommand = _mysqlConnection.CreateCommand();
                _mysqlCommand.CommandText = queryString;
                _mysqlReader = _mysqlCommand.ExecuteReader();
                if (!string.IsNullOrEmpty(message))
                    Console.WriteLine(message + "成功");
            }
            catch (Exception e)
            {
                Console.WriteLine(message + "失败" + e.Message);
                throw;
            }
            return _mysqlReader;
        }

        /// <summary>
        /// 执行SQL命令
        /// </summary>
        /// <param name="queryString">SQL命令字符串</param>
        /// <param name="operationType">执行SQL命令的方式</param>
        /// <returns></returns>
        private static int ExecuteNonQuery(string queryString, OperationType operationType)
        {
            if (string.IsNullOrEmpty(queryString))
                throw new Exception("参数不能为空");
            int count = 0;
            try
            {
                ResetReader(_mysqlReader);
                _mysqlCommand = _mysqlConnection.CreateCommand();
                _mysqlCommand.CommandText = queryString;
                count = _mysqlCommand.ExecuteNonQuery();
                if (count > 0)
                    Console.WriteLine(operationType + "操作成功" + "  " + count + "次");
                else
                    Console.WriteLine(operationType + "操作失败");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            return count;
        }
        #endregion

        #region 创建表格
        /// <summary>
        /// 创建数据表(简单的表)不能带Id字段，MySQL会自己添加Id字段
        ///         复杂的表，建议直接在数据库创建好再使用
        /// </summary> 
        /// <returns>The table.</returns>
        /// <param name="tableName">数据表名</param>
        /// <param name="colNames">字段名</param>
        /// <param name="colTypes">字段名类型</param>
        /// <param name="colLength">数据的预留长度</param>
        /// <param name="colState">数据的可设置状态</param>
        public static MySqlDataReader CreateTable(string tableName, string[] colNames, string[] colTypes, string[] colLength, string[] colState)
        {
            // 参数检查
            if (string.IsNullOrEmpty(tableName) || colNames == null || colTypes == null || colLength == null || colState == null)
                throw new Exception("参数不能为空");
            if (!(colNames.Length == colTypes.Length && colTypes.Length == colLength.Length && colLength.Length == colState.Length))
                throw new Exception("referKeys 或 colTypes或 colLength 或colState 的长度不相等");

            _mysqlCommand = _mysqlConnection.CreateCommand();
            _mysqlCommand.CommandText = string.Format("select COUNT(*) from information_schema.TABLES where TABLE_SCHEMA ='{0}'AND table_name ='{1}'", _currentDataBaseName, tableName);
            ResetReader(_mysqlReader);
            _mysqlReader = _mysqlCommand.ExecuteReader();
            bool isExit = false;
            while (_mysqlReader.Read())
            {
                for (int i = 0; i < _mysqlReader.FieldCount; i++)
                {
                    if (_mysqlReader.GetValue(i).ToString() == "1")
                    {
                        isExit = true;
                        break;
                    }
                }
            }
            _mysqlReader.Close();
            _mysqlReader = null;
            if (isExit)
            {
                Console.WriteLine("此表格已存在，返回已存在的表格");
                return ReadFullTable(tableName);
            }
            else
            {
                _strBuilder.Clear();
                _strBuilder.Append("CREATE TABLE IF NOT EXISTS " + tableName +
                                   "( Id int(11) NOT NULL AUTO_INCREMENT," + colNames[0] + " " + colTypes[0] + "(" +
                                   colLength[0] + ")" +
                                   " DEFAULT " + colState[0] + ",");

                for (int i = 1; i < colNames.Length; i++)
                {
                    _strBuilder.Append(colNames[i] + " " + colTypes[i] + "(" + colLength[i] + ")" + " DEFAULT " + colState[i] + ",");
                }
                _strBuilder.Append(" PRIMARY KEY(`Id`)) ENGINE = InnoDB DEFAULT CHARSET = utf8;");
                return ExecuteReader(_strBuilder.ToString(), tableName + "表格创建");
            }
        }


        #endregion

        #region 插入数据
        /// <summary>
        /// 向指定数据表中插入数据，一条一条的插入
        /// </summary>
        /// <returns>The values.</returns>
        /// <param name="tableName">数据表名称</param>
        /// <param name="colName">数据表的字段名</param>
        /// <param name="values">插入的数值</param>
        public static int InsertData(string tableName, string[] colName, string[] values)
        {
            // 参数检查
            if (string.IsNullOrEmpty(tableName) || colName == null || values == null)
                throw new Exception("参数不能为空");
            //获取数据表中字段数目
            int fieldCount = ReadFullTable(tableName).FieldCount; // MySQL会带有ID这个字段
            //当插入的数据长度不等于字段数目时引发异常
            if (values.Length != fieldCount - 1 || colName.Length != values.Length)
                throw new Exception("插入的数组长度和数据库的属性个数不相同");
            // 构造SQL语句
            _strBuilder.Clear();
            _strBuilder.Append("INSERT INTO " + tableName + "(" + colName[0]);
            for (int i = 1; i < colName.Length; i++)
            {
                _strBuilder.Append("," + colName[i]);
            }
            _strBuilder.Append(")" + " VALUES(" + "'" + values[0] + "'");
            for (int i = 1; i < values.Length; i++)
            {
                _strBuilder.Append(", " + "'" + values[i] + "'");
            }
            _strBuilder.Append(" )");
            // 执行SQL语句
            return ExecuteNonQuery(_strBuilder.ToString(), OperationType.Insert);
        }




        #endregion

        #region 删除数据

        /// <summary>
        /// 删除指定数据表内的数据
        ///     功能:单一条件
        /// </summary>
        /// <param name="tableName">数据表名称</param>
        /// <param name="referKey">参考属性</param>
        /// <param name="referValue">参考属性的值</param>
        /// <param name="operation">运算符：= , <  >,...</param>
        /// <returns></returns>
        public static int DeleteData(string tableName, string referKey, string referValue, string operation)
        {
            // 参数检查
            if (string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(referKey) || string.IsNullOrEmpty(referValue) || string.IsNullOrEmpty(operation))
                throw new Exception("参数不能为空/null");
            // 构造SQL语句
            _strBuilder.Clear();
            _strBuilder.Append("DELETE FROM " + tableName + " WHERE " + referKey + operation + "'" + referValue + "'");
            // 执行SQL语句
            return ExecuteNonQuery(_strBuilder.ToString(), OperationType.Delete);
        }

        /// <summary>
        /// 删除指定数据表内的数据
        ///  功能：多个条件（OR 或）
        /// </summary>
        /// <returns>The values.</returns>
        /// <param name="tableName">数据表名称</param>
        /// <param name="referKeys">参考属性</param>
        /// <param name="referValues">参考属性的值</param>
        /// <param name="operations">运算符</param>
        public static int DeleteDataOr(string tableName, string[] referKeys, string[] referValues, string[] operations)
        {
            // 参数检查
            if (string.IsNullOrEmpty(tableName) || referKeys == null || referValues == null || operations == null)
                throw new Exception("参数不能为空/null");
            if (!(referKeys.Length == referValues.Length && referValues.Length == operations.Length))
                throw new Exception("referKeys 或 referValues 或 operations 的长度不相等");
            // 构造SQL语句
            _strBuilder.Clear();
            _strBuilder.Append("DELETE FROM " + tableName + " WHERE " + referKeys[0] + operations[0] + "'" + referValues[0] + "'");
            for (int i = 1; i < referValues.Length; i++)
                _strBuilder.Append("OR " + referKeys[i] + operations[i] + "'" + referValues[i] + "'");
            // 执行SQL语句
            return ExecuteNonQuery(_strBuilder.ToString(), OperationType.Delete);
        }

        /// <summary>
        /// 删除指定数据表内的数据
        /// 功能：多个条件（AND 且）
        /// </summary>
        /// <returns>The values.</returns>
        /// <param name="tableName">数据表名称</param>
        /// <param name="referKeys">字段名</param>
        /// <param name="referValues">字段名对应的数据</param>
        /// /// <param name="operations">运算符</param>
        public static int DeleteDataAnd(string tableName, string[] referKeys, string[] referValues, string[] operations)
        {
            // 参数检查
            if (string.IsNullOrEmpty(tableName) || referKeys == null || referValues == null || operations == null)
                throw new Exception("参数不能为空/null");
            if (!(referKeys.Length == referValues.Length && operations.Length == referValues.Length))
                throw new Exception("referKeys 或 referValues 或 operations 的长度不相等");
            // 构造SQL语句
            _strBuilder.Clear();
            _strBuilder.Append("DELETE FROM " + tableName + " WHERE " + referKeys[0] + operations[0] + "'" + referValues[0] + "'");
            for (int i = 1; i < referValues.Length; i++)
                _strBuilder.Append(" AND " + referKeys[i] + operations[i] + "'" + referValues[i] + "'");
            // 执行SQL语句
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
        /// <param name="referKey">参考字段</param>
        /// <param name="referValue">参考字段的值</param>
        /// <param name="operation">参考字段的运算符：= , <  >,...</param>
        /// <returns></returns>
        public static int UpdateData(string tableName, string[] colNames, string[] colValues, string referKey,string referValue, string operation)
        {
            // 参数检查
            if (colNames == null || colValues == null || colNames.Length != colValues.Length)
                throw new Exception("referKeys.Length!=referValues.Length 或者不能为空");
            if (string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(referKey) || string.IsNullOrEmpty(referValue) || string.IsNullOrEmpty(operation))
                throw new Exception("参数不能为空/null");
            // 构造SQL语句
            _strBuilder.Clear();
            _strBuilder.Append("UPDATE " + tableName + " SET " + colNames[0] + "=" + "'" + colValues[0] + "'");
            for (int i = 1; i < colValues.Length; i++)
                _strBuilder.Append(", " + colNames[i] + "=" + "'" + colValues[i] + "'");
            _strBuilder.Append(" WHERE " + referKey + operation + "'" + referValue + "'");
            // 执行SQL语句
            return ExecuteNonQuery(_strBuilder.ToString(), OperationType.Update);
        }
        /// <summary>
        /// 更新指定数据表内的数据
        /// 功能：多个条件（OR 或）
        /// </summary>
        /// <returns>The values.</returns>
        /// <param name="tableName">数据表名称</param>
        /// <param name="colNames">需要更新的字段名</param>
        /// <param name="colValues">需要更新的字段名对应的数据</param>
        /// <param name="referKeys">依据的关键字</param>
        /// <param name="referValues">依据的关键字对应的值</param>
        /// <param name="operations">依据运算符：= , < , >,...</param>
        public static int UpdateDataOr(string tableName, string[] colNames, string[] colValues, string[] referKeys, string[] referValues, string[] operations)
        {
            //参数检查
            if (string.IsNullOrEmpty(tableName) || colNames == null || colValues == null || referKeys == null || referValues == null || operations == null)
                throw new Exception("参数不能为空/null");
            if (colNames.Length != colValues.Length)
                throw new Exception("referKeys / referValues  长度不相等");
            if (!(referKeys.Length == referValues.Length && operations.Length == referValues.Length))
                throw new Exception("referKeys / referValues / operations 三者长度不相等 ");
            // 构造SQL语句
            _strBuilder.Clear();
            _strBuilder.Append("UPDATE " + tableName + " SET " + colNames[0] + "=" + "'" + colValues[0] + "'");
            for (int i = 1; i < colValues.Length; i++)
                _strBuilder.Append(", " + colNames[i] + "=" + "'" + colValues[i] + "'");
            _strBuilder.Append(" WHERE " + referKeys[0] + operations[0] + "'" + referValues[0] + "'");
            for (int i = 1; i < referKeys.Length; i++)
                _strBuilder.Append(" OR " + referKeys[i] + operations[i] + "'" + referValues[i] + "'");
            // 执行SQL语句
            return ExecuteNonQuery(_strBuilder.ToString(), OperationType.Update);
        }

        /// <summary>
        /// 更新指定数据表内的数据
        /// 功能：多个条件（AND 且）
        /// </summary>
        /// <returns>The values.</returns>
        /// <param name="tableName">数据表名称</param>
        /// <param name="colNames">需要更新的字段名</param>
        /// <param name="colValues">需要更新的字段名对应的数据</param>
        /// <param name="referKeys">依据的关键字</param>
        /// <param name="referValues">依据的关键字对应的值</param>
        /// <param name="operations">依据运算符：= , < , >,...</param>
        public static int UpdateDataAnd(string tableName, string[] colNames, string[] colValues, string[] referKeys, string[] referValues, string[] operations)
        {
            //参数检查
            if (string.IsNullOrEmpty(tableName) || colNames == null || colValues == null || referKeys == null || referValues == null || operations == null)
                throw new Exception("参数不能为空/null");
            if (colNames.Length != colValues.Length)
                throw new Exception("referKeys / referValues  长度不相等");
            if (!(referKeys.Length == referValues.Length && operations.Length == referValues.Length))
                throw new Exception("referKeys / referValues / operations 三者长度不相等 ");
            // 构造SQL语句
            _strBuilder.Clear();
            _strBuilder.Append("UPDATE " + tableName + " SET " + colNames[0] + "=" + "'" + colValues[0] + "'");
            for (int i = 1; i < colValues.Length; i++)
                _strBuilder.Append(", " + colNames[i] + "=" + "'" + colValues[i] + "'");
            _strBuilder.Append(" WHERE " + referKeys[0] + operations[0] + "'" + referValues[0] + "'");
            for (int i = 1; i < referKeys.Length; i++)
                _strBuilder.Append(" AND " + referKeys[i] + operations[i] + "'" + referValues[i] + "'");
            // 执行SQL语句
            return ExecuteNonQuery(_strBuilder.ToString(), OperationType.Update);
        }


        #endregion

        #region 查询数据

        /// <summary>
        /// 查找指定数据表内的数据
        ///     功能：单一条件查找
        /// </summary>
        /// <param name="tableName">数据表名称</param>
        /// <param name="colNames">查找目标列</param>
        /// <param name="referKey">参考字段</param>
        /// <param name="referValue">参考字段的值</param>
        /// <param name="operation">参考字段的运算符：= , <  >,...</param>
        /// <returns></returns>
        public static MySqlDataReader SelectData(string tableName, string[] colNames, string referKey, string referValue, string operation)
        {
            //参数检查
            if (string.IsNullOrEmpty(tableName) || colNames == null || string.IsNullOrEmpty(referKey) || string.IsNullOrEmpty(referValue) || string.IsNullOrEmpty(operation))
                throw new Exception("参数不能为空/null");
            // 构造SQL语句
            _strBuilder.Clear();
            _strBuilder.Append("SELECT " + colNames[0]);
            for (int i = 1; i < colNames.Length; i++)
                _strBuilder.Append(", " + colNames[i]);
            _strBuilder.Append(" FROM " + tableName + " WHERE " + referKey + operation + "'" + referValue + "'");
            // 执行SQL语句
            return ExecuteReader(_strBuilder.ToString(), "查找数据");
        }


        /// <summary>
        /// 查找指定数据表内的数据
        /// 功能：多个条件（AND 且）
        /// </summary>
        /// <returns>The table.</returns>
        /// <param name="tableName">数据表名称</param>
        /// <param name="colNames">查找目标列</param>
        /// <param name="referKeys">依据的字段</param>
        /// <param name="referValues">依据的字段名对应的数据</param>
        /// <param name="operations">依据的运算符：= , < , >,...</param>
        public static MySqlDataReader SelectDataAnd(string tableName, string[] colNames, string[] referKeys, string[] referValues, string[] operations)
        {
            //参数检查
            if (string.IsNullOrEmpty(tableName) || colNames == null || referKeys == null || referValues == null || operations == null)
                throw new Exception("参数不能为空/null");
            if (!(referKeys.Length == referValues.Length && operations.Length == referValues.Length))
                throw new Exception("referKeys / referValues / operations 三者长度不相等 ");
            // 构造SQL语句
            _strBuilder.Clear();
            _strBuilder.Append("SELECT " + colNames[0]);
            for (int i = 1; i < colNames.Length; i++)
                _strBuilder.Append(", " + colNames[i]);
            _strBuilder.Append(" FROM " + tableName + " WHERE " + referKeys[0] + operations[0] + "'" + referValues[0] + "'");
            for (int i = 0; i < referKeys.Length; i++)
                _strBuilder.Append(" AND " + referKeys[i] + operations[i] + "' " + referValues[i] + "' ");
            return ExecuteReader(_strBuilder.ToString(), "查找数据");
        }

        /// <summary>
        /// 查找指定数据表内的数据
        /// 功能：多个条件（OR 或）
        /// </summary>
        /// <returns>The table.</returns>
        /// <param name="tableName">数据表名称</param>
        /// <param name="colNames">查找目标列</param>
        /// <param name="referKeys">依据的字段</param>
        /// <param name="referValues">依据的字段名对应的数据</param>
        /// <param name="operations">依据的运算符：= , < , >,...</param>
        public static MySqlDataReader SelectDataOr(string tableName, string[] colNames, string[] referKeys, string[] referValues, string[] operations)
        {
            //参数检查
            if (string.IsNullOrEmpty(tableName) || colNames == null || referKeys == null || referValues == null || operations == null)
                throw new Exception("参数不能为空/null");
            if (!(referKeys.Length == referValues.Length && operations.Length == referValues.Length))
                throw new Exception("referKeys / referValues / operations 三者长度不相等 ");
            // 构造SQL语句
            _strBuilder.Clear();
            _strBuilder.Append("SELECT " + colNames[0]);
            for (int i = 1; i < colNames.Length; i++)
                _strBuilder.Append(", " + colNames[i]);
            _strBuilder.Append(" FROM " + tableName + " WHERE " + referKeys[0] + operations[0] + "'" + referValues[0] + "'");
            for (int i = 0; i < referKeys.Length; i++)
                _strBuilder.Append(" OR " + referKeys[i] + operations[i] + "' " + referValues[i] + "' ");
            return ExecuteReader(_strBuilder.ToString(), "查找数据");
        }

        #endregion

        #region 辅助方法
        /// <summary>
        /// 重置 reader
        /// </summary>
        private static void ResetReader(MySqlDataReader reader)
        {
            if (reader != null && reader.IsClosed == false)
            {
                reader.Close();
                reader = null;
            }
        }

        /// <summary>
        /// 显示命令效果
        /// 简单的数据显示
        /// </summary>
        /// <param name="reader"></param>
        public static void ShowData(MySqlDataReader reader)
        {
            if (reader == null || reader.IsClosed == true)
                return;
            bool result = false;
            while (reader.Read())
            {
                result = true;
                _strShowBuilder.Clear();
                _strShowBuilder.Append("命令执行成功: ");
                for (int i = 0; i < reader.FieldCount - 1; i++)
                    _strShowBuilder.Append("第" + i + "列:" + "(列名:" + reader.GetName(i) + "  类型:" + reader.GetDataTypeName(i) + ") " + reader.GetValue(i) + " | ");
                int temp = reader.FieldCount - 1;
                _strShowBuilder.Append("第 " + temp + " 列:" + "(列名:" + reader.GetName(temp) + "  类型:" + reader.GetDataTypeName(temp) + ") " + reader.GetValue(temp));
                Console.WriteLine(_strShowBuilder.ToString());
            }
            if (result == false)
                Console.WriteLine("该命令，没有获取任何数据");
            ResetReader(reader);
        }



        /// <summary>
        /// 读取整张数据表
        /// </summary>
        /// <returns>The full table.</returns>
        /// <param name="tableName">数据表名称</param>
        private static MySqlDataReader ReadFullTable(string tableName)
        {
            if(string.IsNullOrEmpty(tableName))
                throw new Exception("参数不能为空");
            string queryString = "SELECT * FROM " + tableName;
            return ExecuteReader(queryString);
        }

        #endregion


    }
    /// <summary>
    /// MySQL 中常用的数据类型
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
}
