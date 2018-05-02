using System;
using System.Data.SQLite;

namespace ConnectSQLite
{
    class SqLiteHelper
    {
        private SQLiteConnection connection;

        public SqLiteHelper()
        {
            connection = new SQLiteConnection();
        }
        /// <summary>
        /// 连接并打开数据库
        /// Pooling=true;FailIfMissing=false：路径不存在数据库时，会创建数据库。当只有数据库名时，表示数据库在bin/Debug路径下
        /// </summary>
        /// <param name="DBpath">数据库路径</param>
        public void OpenDataBase(string DBpath)
        {
            try
            {
                connection.ConnectionString = @"Data Source="+ DBpath + ";Pooling=true;FailIfMissing=false";
                connection.Open();
                Console.WriteLine("成功打开数据库");
            }
            catch (Exception e)
            {
                Console.WriteLine("数据库打开失败" +e);
            }
        }

        /// <summary>
        /// 创建表，存在这张表时，就不做操作
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="column1Name">第一列的名字</param>
        /// <param name="column1Type">第一列的类型</param>
        /// <param name="column2Name">第二列的名字</param>
        /// <param name="column2Type">第二列的类型</param>
        /// <param name="column3Name">第三列的名字</param>
        /// <param name="column3Type">第三列的类型</param>
        private string ColumnName1;
        private string ColumnName2;
        private string ColumnName3;
        public void CreatTable(string tablename, string column1Name, string column1Type, string column2Name, string column2Type, string column3Name, string column3Type)
        {

            //判断数据库里面是否存在某个表格--比如UserTable
            // SQLiteCommand---数据库命令器
            SQLiteCommand cmd = connection.CreateCommand();
            //	固定格式 ---Sqlite语句
            cmd.CommandText = "select count(*) from sqlite_master where type = 'table' and name = '" + tablename + "'";
            // 执行数据库语言
            SQLiteDataReader reader = cmd.ExecuteReader();
            bool isExit = false;
            while (reader.Read())
            {
                Console.WriteLine(reader.FieldCount);
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (reader.GetValue(i).ToString() == "1")
                    {
                        Console.WriteLine(tablename + "表格已经存在");
                        isExit = true;
                        ColumnName1 = column1Name;
                        ColumnName2 = column2Name;
                        ColumnName3 = column3Name;
                    }
                }
            }
            reader.Close(); // 当reader不关闭时，cmd不能执行之后的任务
            reader = null;
            // 这张表格不存在时，创建表格
            if (!isExit)
            {
                // 创建表格
                cmd.CommandText = "CreatE table '" + tablename + "' (" + column1Name + " " + column1Type + "," +
                                  column2Name + " " + column2Type + "," + column3Name + " " + column3Type + ")";
                try
                {
                    cmd.ExecuteNonQuery();
                    Console.WriteLine(tablename + "表格创建成功");
                    ColumnName1 = column1Name;
                    ColumnName2 = column2Name;
                    ColumnName3 = column3Name;
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(tablename + "表格创建失败" + ex);
                }
            }
            else
            {
                Console.WriteLine("此表名已存在");
            }
        }

        /// <summary>
        /// 插入数据，一行一行的插入，根据需要更改插入的值的数量
        /// sqlite中rowid列不需要插入自动复制
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="column1Value">第一列的值</param>
        /// <param name="column2Value">第二列的值</param>
        /// <param name="column3Value">第三列的值</param>
        public void InsertData(string tableName , string column1Value, string column2Value, string column3Value)
        {
            SQLiteCommand cmd = connection.CreateCommand();
            cmd.CommandText = "insert into " + tableName + "("+ColumnName1+ "," + ColumnName2 + ","+ColumnName3+ ")" + " values"+"('" + column1Value + "','" + column2Value + "','" + column3Value + "')";
            Console.WriteLine(cmd.CommandText);
            // 执行sql语句
            try
            {
                int count = cmd.ExecuteNonQuery();
                if (count > 0)
                {
                    Console.WriteLine("数据插入成功");
                }
                else
                {
                    Console.WriteLine("根据条件数据插入失败");
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("数据插入失败" + ex.Message);
            }
        }
        /// <summary>
        /// 删除数据，根据目标列GoalColumn的目标值GoalColumnValue，来删除对应的行
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="GoalColumn">目标列</param>
        /// <param name="GoalColumnValue">目标值</param>
        public void DeleteData(string tableName,string GoalColumn, string GoalColumnValue)
        {
            SQLiteCommand cmd = connection.CreateCommand();
            cmd.CommandText = "Delete from " + tableName + " where " + GoalColumn + " = '" + GoalColumnValue + "'";
            try
            {
                int count = cmd.ExecuteNonQuery();
                if (count > 0)
                {
                    Console.WriteLine(GoalColumnValue + "删除成功,一共删除了"+count+"条");
                }
                else
                {
                    Console.WriteLine ("该条件下没有数据");
                }

            }
            catch (System.Exception ex)
            {
                Console.WriteLine ("删除失败" + ex);
            }
        }
        /// <summary>
        /// 更新数据，根据参考列columnReference的参考值columnReferenceValue，更改目标列GoalColumn的更新值newGoalColumnValue
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="GoalColumn">目标列</param>
        /// <param name="newGoalColumnValue">目标列更新的值</param>
        /// <param name="columnReference">参考列</param>
        /// <param name="columnReferenceValue">参考列的值</param>
        public void UpdateData(string tableName,string GoalColumn, string newGoalColumnValue, string columnReference, string columnReferenceValue)
        {
            SQLiteCommand cmd = connection.CreateCommand();
            cmd.CommandText = "update " + tableName + " set "+ GoalColumn + " = '" + newGoalColumnValue + "' where "+ columnReference + " ='" + columnReferenceValue + "'";
            try
            {
                int count = cmd.ExecuteNonQuery();
                if (count > 0)
                {
                    Console.WriteLine("修改成功");
                }
                else
                {
                    Console.WriteLine("未找到符合条件的修改项");
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("修改失败" + ex.Message);
            }
        }
        /// <summary>
        /// 查找数据，根据参考列的参考值类查找队形的目标列的目标值
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="GoalColumn">目标列</param>
        /// <param name="columnReference">参考列</param>
        /// <param name="columnReferenceValue">参考值</param>
        public void SelectData(string tableName, string GoalColumn, string columnReference, string columnReferenceValue)
        {
            SQLiteCommand cmd = connection.CreateCommand();
            cmd.CommandText = "select " + GoalColumn + " from " + tableName + " where " + columnReference + " = '" + columnReferenceValue + "'";
            try
            {
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.WriteLine("查找成功" + reader.GetValue(i));
                    }
                }
                reader.Close();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("查找数据失败" + ex);
            }
        }
        /// <summary>
        /// 关闭数据库
        /// </summary>
        public void CloseDataBase()
        {
            try
            {
                connection.Close();
                Console.WriteLine("成功关闭数据库");
            }
            catch (Exception e)
            {
                Console.WriteLine("关闭数据库出现问题" + e.Message);

            }
        }
    }
}
