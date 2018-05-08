using System;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using MySql.Data.MySqlClient;

public class MySQLController
{
    private MySqlConnection connection;

    public MySQLController()
    {
        connection =new MySqlConnection();
    }

    public void OpenDataBase(string DbPath)
    {
        try
        {
            connection.ConnectionString = DbPath;
            connection.Open();
            Console.WriteLine("打开数据库成功");
        }
        catch (Exception e)
        {
            Console.WriteLine("打开数据库失败"+e.Message);
        }
    }
    public void CreatTable(string tablename, string column1Name, string column1Type, string column2Name, string column2Type, string column3Name, string column3Type)
    {

        //判断数据库里面是否存在某个表格--比如UserTable
        // SQLiteCommand---数据库命令器
        MySqlCommand cmd = connection.CreateCommand();
        // 创建表格
        cmd.CommandText = "Create table IF NOT EXISTS " + "`"+tablename +"`"+ "(" + "`ID` int(10) AUTO_INCREMENT," + "`"+ column1Name + "`" + column1Type + "(50)," +
                          "`"+column2Name + "`" + column2Type + "(11)," + "`"+column3Name + "`" + column3Type + "(100),PRIMARY KEY( `ID`)" + ")ENGINE=InnoDB DEFAULT CHARSET=utf8";
        try
        {
            cmd.ExecuteNonQuery();
            Console.WriteLine(tablename + "表格创建成功");
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(tablename + "表格创建失败" + ex);
        }
    }

    /// <summary>
    /// 插入数据
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="ID">id值</param>
    /// <param name="column1Value">列1值</param>
    /// <param name="column2Value">列2值param>
    /// <param name="column3Value">列3值</param>
    /// <param name="column4Value">列4值</param>
    /// <param name="column5Value">列5值</param>
    /// <param name="column6Value">列6值</param>
    public void Insert(string tableName, string column1Value, int column2Value, string column3Value)
    {
        MySqlCommand cmd = connection.CreateCommand();
        // 这里跟sqlite有区别，必须添加Id的值,这样就比较麻烦，还要手动添加id
        //cmd.CommandText = "insert into " + tableName + " values('" + column1Value + "','" + column2Value + "','" + column3Value + "','" + column4Value + "','" + column5Value + "','" + column6Value + "' )";
        // 方法二：这样就不需要添加ID,可以自动增加ID
        cmd.CommandText = "insert into " + tableName + "(name,age,sex)" + " values('" + column1Value + "','" + column2Value + "','" + column3Value + "')";
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

    public void Delete(string tableName, string GoalColumn, string GoalColumnValue)
    {
        MySqlCommand cmd = connection.CreateCommand();
        cmd.CommandText = "Delete from " + tableName + " where " + GoalColumn + " = '" + GoalColumnValue + "'";
        try
        {
            int count = cmd.ExecuteNonQuery();
            if (count > 0)
            {
                Console.WriteLine(GoalColumnValue + "删除成功,一共删除了" + count + "条");
            }
            else
            {
                Console.WriteLine("该条件下没有数据");
            }

        }
        catch (System.Exception ex)
        {
            Console.WriteLine("删除失败" + ex);
        }
    }

    public void Update(string tableName, string GoalColumn, string newGoalColumnValue, string columnReference, string columnReferenceValue)
    {
        MySqlCommand cmd = connection.CreateCommand();
        cmd.CommandText = "Update " + tableName + " set " + GoalColumn + " = " + "'"+newGoalColumnValue + "'"+" where " + columnReference + " ='" + columnReferenceValue + "'";
        Console.WriteLine(cmd.CommandText);
        try
        {
            int count = cmd.ExecuteNonQuery();
            if (count > 0)
            {
                Console.WriteLine("修改成功"+ count+"条");
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
        MySqlCommand cmd = connection.CreateCommand();
        cmd.CommandText = "select " + GoalColumn + " from " + tableName + " where " + columnReference + " = '" + columnReferenceValue + "'";
        Console.WriteLine(cmd.CommandText);
        try
        {
            MySqlDataReader reader = cmd.ExecuteReader();
            Console.WriteLine("ddd");
            while (reader.Read())
            {
                Console.WriteLine(reader.FieldCount);
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
