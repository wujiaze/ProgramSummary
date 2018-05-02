using System;
namespace ConnectSQLite
{
    class Program
    {
        static void Main(string[] args)
        {
            // 总结: 1.类型 int和 integer 是一样的
            //       2. SQLite会自动添加ID列，并且名称为 "rowid";
            SqLiteHelper sqLiteHelper =new SqLiteHelper();
            string DataBasePath = "D:/Desktop/database.db";
            sqLiteHelper.OpenDataBase(DataBasePath);
            string tablename = "rr";
            string column1Name = "type";
            string column1Type = "text";
            string column2Name = "price";
            string column2Type = "int";
            string column3Name = "color";
            string column3Type = "text";
            sqLiteHelper.CreatTable(tablename, column1Name, column1Type, column2Name, column2Type, column3Name, column3Type);
            sqLiteHelper.InsertData(tablename, "二手奥拓", "1600", "red");
            sqLiteHelper.UpdateData(tablename, column2Name, "1500", column1Name, "二手奥拓");
            sqLiteHelper.InsertData(tablename, "二手奥拓", "1600", "red");
            sqLiteHelper.SelectData(tablename, "rowid", column1Name, "二手奥拓");
            sqLiteHelper.DeleteData(tablename, column2Name, "1600");
            sqLiteHelper.CloseDataBase();
            Console.Read();
        }
    }
}
