using System;
using System.Data.SQLite;
using SqliteTool;
namespace ConsoleApp1
{
    class Program
    {
        // 总结: 1.类型 int和 integer 是一样的
        //       2. SQLite会自动添加ID列，并且名称为 "rowid";
        static void Main(string[] args)
        {
            // 打开数据库
            string dataPath = "D:/Desktop/LogDataBase.db";
            SqliteHelper.OpenDataBase(dataPath);
            // Demo01 创建表
            SqliteHelper.CreateTable("LogRoot3", new string[] { "Id" ,"value"}, new string[] { FieldType.VarChar.ToString(), FieldType.Int.ToString() }, new string[] { "10" ,"5"}, new string[] { FieldState.Null.ToString(), FieldState.Null.ToString() });
            // Demo02 插入值
            SqliteHelper.InsertData("LogRoot3", new string[] { "2", "4" });
            // Demo03 修改值
            SqliteHelper.UpdateData("LogRoot3", new string[] { "Id" }, new string[] { "10" }, "value", "3", ">");
            // Demo04 查询值
            SQLiteDataReader reader = SqliteHelper.SelectData("LogRoot3", new string[] { "Id" }, "value", "4", "=");
            SqliteHelper.ShowData(reader);
            // Demo05 删除值
            SqliteHelper.DeleteData("LogRoot3", "Id", "11", "!=");
            // 关闭数据库
            SqliteHelper.CloseDataBase();
            Console.Read();
        }
    }
}
