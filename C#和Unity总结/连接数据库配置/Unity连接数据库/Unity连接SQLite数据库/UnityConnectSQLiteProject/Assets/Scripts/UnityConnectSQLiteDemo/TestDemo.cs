using Sqlite_CSharp_Formwork;
using System;
using Mono.Data.Sqlite;
using UnityEngine;
namespace Sqlite_CSharp_Demo
{
    public class TestDemo : MonoBehaviour
    {
        void Start()
        {
            //C: \Users\你的用户名\AppData\LocalLow\公司名\项目名  todo 这个目录名还不是太懂
            //      而在安卓下 对应为 / data / data / 包名 / file
            //string databasePath0 =  "URI=file:"+ Application.persistentDataPath+"/MyDataBase.db";
            //SQLiteHelper.OpenDataBase(databasePath0);
            string databasePath = Application.dataPath + "/MyDataBase.db";
            SQLiteHelper.OpenDataBase(databasePath);
            string tablename = "rr";
            string tablename2 = "pp";
            string column1Name = "type";
            string column1Type = "text";
            string column2Name = "price";
            string column2Type = "int";
            string column3Name = "color";
            string column3Type = "text";
            string[] str = new string[] { "二手奥拓", "1600", "red" };
            string[] types = new string[] { column1Type, column2Type, column3Type };
            string[] name = new string[] { column1Name, column2Name, column3Name };
            // 建表
            SQLiteHelper.CreateTable(tablename, name, types);
            //SQLiteHelper.CreateTable(tablename2, name, types);
            //增加数据
            for (int i = 0; i < 45; i++)
            {
                str[1] = (Convert.ToInt32(str[1])+i*500 ).ToString();
                SQLiteHelper.InsertData(tablename, str);
            }
            // 修改数据
            string[] colName = new string[] { "color" };
            string[] colType = new string[] { "bule" };
            string[] key = new string[] { "price" };
            string[] value = new string[] { "1600" };
            string[] operation = new string[] { "=" };

            int count = SQLiteHelper.UpdateDataOR(tablename, colName, colType, key, value, operation);
            Debug.Log("更新了 "+ count+ " 条数据");
            // 查询数据
            SqliteDataReader reader = SQLiteHelper.SelectDataOR(tablename, key, colName, colType, operation);
            // 显示结果
            SQLiteHelper.ShowData(reader);
            Debug.Log(reader.IsClosed);

            // 删除数据
            count = SQLiteHelper.DeleteDataOR(tablename, colName, colType, operation);
            Debug.Log("删除了 " + count + " 条数据");

            reader = SQLiteHelper.ReadFullTable(tablename);
            SQLiteHelper.ShowData(reader);

            // 关闭数据库
            SQLiteHelper.CloseDataBase();
        }
        void Update()
        {

        }
    }

}

