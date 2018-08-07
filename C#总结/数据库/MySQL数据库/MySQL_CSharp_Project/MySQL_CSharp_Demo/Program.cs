using MySql.Data.MySqlClient;
using MySQLTool;
using System;

namespace MySQL_CSharp_Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            // 打开数据库
            string dataPath = "myBase";
            MySQLHelper.OpenDataBase(dataPath,"127.0.0.1",3306,"root","83117973bb");
            //// Demo01 创建表
            MySQLHelper.CreateTable("LogRoot", new string[] { "wd", "value" }, new string[] { FieldType.VarChar.ToString(), FieldType.VarChar.ToString() }, new string[] { "10", "5" }, new string[] { FieldState.Null.ToString(), FieldState.Null.ToString() });
            //// Demo02 插入值
            MySQLHelper.InsertData("LogRoot", new string[] { "wd", "value" }, new string[] { "2", "4" });
            //// Demo03 修改值
            MySQLHelper.UpdateData("LogRoot", new string[] { "wd" }, new string[] { "10" }, "value", "3", ">");
            //// Demo04 查询值
            MySqlDataReader reader = MySQLHelper.SelectData("LogRoot", new string[] { "wd" }, "value", "4", "=");
            MySQLHelper.ShowData(reader);
            //// Demo05 删除值
            MySQLHelper.DeleteData("LogRoot", "wd", "11", "!=");
            //// 关闭数据库
            MySQLHelper.CloseDataBase();
            Console.Read();

           
        }
    }
}
