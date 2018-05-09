using System;
using UnityEngine;
// 链接使用Sqlite数据库---需要导入Mono.Data.Sqlite.dll文件
using Mono.Data.Sqlite;


public class SQliteController : MonoBehaviour
{
    //  数据库连接器
    SqliteConnection connection;
    string tableName = "Usertable";
    string tableName1 = "textTable";
    // 固定格式 --- 路径
    void Start()
    {
        string databasePath = "data source =" + Application.dataPath + "/MyDataBase.sqlite";
        OpenDataBase(databasePath);
    }

    void Update()
    {
        // 增
        if (Input.GetKeyDown(KeyCode.P))
        {
            InsertData("老款二手奥拓", 15000, "红色");
        }
        // 删
        if (Input.GetKeyDown(KeyCode.O))
        {
            DeleteData("老款二手奥拓");
        }
        // 改
        if (Input.GetKeyDown(KeyCode.I))
        {
            UpdateData(15222, "老款二手奥拓");
        }
        // 查
        if (Input.GetKeyDown(KeyCode.U))
        {
            SelectData("type", "老款二手奥拓", "rowid", tableName);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            CloseDataBase();
        }
    }
    // 	链接数据库
    void OpenDataBase(string databasePath)
    {
        // 捕获异常
        try
        {
            // 第一步链接数据库 --没有数据库就会创建一个新的数据库
            connection = new SqliteConnection(databasePath);
            // 打开数据库
            connection.Open();
            print("打开数据库成功");
        }
        catch (System.Exception ex)
        {
            // exception:异常的父类
            print("数据库打开失败" + ex);
        }
        // 创建表格 ：一般我们会在数据库中先创建表格，所以这一步就不需要了
        CreatTable(tableName);
        CreatTable(tableName1);
    }
    // 关闭数据库 ---作用
    void CloseDataBase()
    {
        try
        {
            connection.Close();
            print("关闭数据库");
        }
        catch (Exception e)
        {
            print("关闭数据库出现问题" + e.Message);

        }

    }
    //	创建表格
    void CreatTable(string tablename)
    {
        //判断数据库里面是否存在某个表格--比如UserTable
        // SqliteCommand---数据库命令器
        SqliteCommand cmd = connection.CreateCommand();
        //	固定格式 ---Sqlite语句
        cmd.CommandText = "select count(*) from sqlite_master where type = 'table' and name = '" + tablename + "'";
        // 执行数据库语言
        SqliteDataReader reader = cmd.ExecuteReader();
        bool isExit = false;
        while (reader.Read())
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetValue(i).ToString() == "1")
                {
                    print(tablename + "表格已经存在");
                    isExit = true;
                }
            }
        }
        reader.Close(); // 当reader不关闭时，cmd不能执行之后的任务
        reader = null;
        // 这张表格不存在时，创建表格
        if (!isExit)
        {
            // 创建表格
            cmd.CommandText = "CreatE table '" + tablename + "' (type text,price INTEGER,color text)";
            try
            {
                cmd.ExecuteNonQuery();
                print(tablename + "表格创建成功");
            }
            catch (System.Exception ex)
            {
                print(tablename + "表格创建失败" + ex);
            }
        }
    }
    //	 数据库的增删改查
    //	 插入增加数据
    void InsertData(string type, int price, string color)
    {
        SqliteCommand cmd = connection.CreateCommand();
        cmd.CommandText = "insert into " + tableName + " values('" + type + "','" + price + "','" + color + "')";
        print("增加语句" + cmd.CommandText);
        // 执行sql语句
        try
        {
            int count = cmd.ExecuteNonQuery();
            if (count > 0)
            {
                print(type + "数据插入成功");
            }
            else
            {
                print("根据条件数据插入失败");
            }
        }
        catch (System.Exception ex)
        {
            print("数据插入失败" + ex);
        }
    }
    // 删除数据
    void DeleteData(string type)
    {
        SqliteCommand cmd = connection.CreateCommand();
        cmd.CommandText = "Delete from " + tableName + " where type = '" + type + "'";
        print("删除语句" + cmd.CommandText);
        try
        {
            int count = cmd.ExecuteNonQuery();
            if (count > 0)
            {
                print("删除成功");
            }
            else
            {
                print("该条件下没有数据");
            }

        }
        catch (System.Exception ex)
        {
            print("删除失败" + ex);
        }
    }
    // 修改更新数据
    void UpdateData(int newprice, string type)
    {
        SqliteCommand cmd = connection.CreateCommand();
        cmd.CommandText = "update '" + tableName + "' set price = '" + newprice + "' where type ='" + type + "'";
        try
        {
            int count = cmd.ExecuteNonQuery();
            if (count > 0)
            {
                print("修改成功");
            }
            else
            {
                print("未找到符合条件的修改项");
            }
        }
        catch (System.Exception ex)
        {
            print("修改失败" + ex.Message);
        }
    }
    // 查找数据 type
    void SelectData(string Rowtype, string RawName, string option, string tablename)
    {
        SqliteCommand cmd = connection.CreateCommand();
        cmd.CommandText = "select " + option + " from '" + tablename + "' where " + Rowtype + " = '" + RawName + "'";
        Debug.Log(cmd.CommandText);
        try
        {
            SqliteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    print("查找成功" + reader.GetValue(i));
                }
            }
            reader.Close();
        }
        catch (System.Exception ex)
        {
            print("查找数据失败" + ex);
        }
    }

}
