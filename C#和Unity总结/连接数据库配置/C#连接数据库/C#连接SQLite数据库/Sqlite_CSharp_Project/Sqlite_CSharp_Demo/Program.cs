using System;
using System.Data.SQLite;
using Sqlite_CSharp_Formwork;
namespace ConsoleApp1
{
    class Program
    {
        // 总结: 1.类型 int和 integer 是一样的
        //       2. SQLite会自动添加ID列，并且名称为 "rowid";
        static void Main(string[] args)
        {
            string DataBasePath = "D:/Desktop/database.db";
            string DataBasePath2 = "DB/database.db";
            SQLiteHelper.OpenDataBase(DataBasePath);
            SQLiteHelper.OpenDataBase(DataBasePath2);
            string tablename = "rr";
            string tablename2 = "pp";
            string column1Name = "type";
            string column1Type = "text";
            string column2Name = "price";
            string column2Type = "int";
            string column3Name = "color";
            string column3Type = "text";
            string[] str =new string[] { "二手奥拓" , "1600" , "red" };
            string[] types = new string[] { column1Type, column2Type, column3Type };
            string[] name = new string[] { column1Name, column2Name, column3Name };
            SQLiteHelper.CreateTable(tablename, name, types);
            SQLiteHelper.CreateTable(tablename2, name, types);
            for (int i = 0; i < 5; i++)
            {
                str[1] = (Convert.ToInt32(str[1]) + i * 1000).ToString();
                SQLiteHelper.InsertData(tablename, str);
            }
            string[] ss = new string[]{ column3Name };
            string[] gg = new string[] { "bule" };
            string[] key = new string[] { column2Name };
            string[] value =new string[]{ "1600" };
            string[] operation = new string[]{"="};
            SQLiteHelper.UpdateDataOR(tablename, ss, gg, key, value, operation);
           
            SQLiteDataReader reader = SQLiteHelper.SelectDataOR(tablename, key,ss,gg,operation);
           
            SQLiteHelper.ShowData(reader);
            SQLiteHelper.InsertData(tablename, str);


            SQLiteHelper.DeleteDataOR(tablename, ss, gg, operation);
            SQLiteDataReader reader2 = SQLiteHelper.ReadFullTable(tablename);
            SQLiteHelper.ShowData(reader2);
            SQLiteHelper.CloseDataBase();
            Console.Read();
        }
    }
}
