
using UnityEngine;
using Unity_MySQL_Formwork;
using System;
using MySql.Data.MySqlClient;

public class TestDemo : MonoBehaviour {

	
	void Start () {
        MySQLHelper.OpenDataBase("localhost", 3306, "testbase", "root", "83117973bb");
        string tablename = "rr";
        string tablename2 = "pp";
        string column1Name = "type";
        string column1Type = FiledType.VarChar.ToString();
        string column2Name = "price";
        string column2Type = FiledType.Int.ToString();
        string column3Name = "color";
        string column3Type = FiledType.VarChar.ToString();
        string[] types = new string[] { column1Type, column2Type, column3Type };
        string[] name = new string[] { column1Name, column2Name, column3Name };
        string[] colLength = new[] { "255", "255", "2000" };
        string[] colState = new[] { "NULL", "NULL", "NULL" };
        MySQLHelper.CreateTable(tablename, name, types, colLength, colState);
        MySQLHelper.CreateTable(tablename2, name, types, colLength, colState);
        // 增
        string[] str = new string[] { "二手奥拓", "1600", "red" };
        for (int i = 0; i < 5; i++)
        {
            str[1] = (Convert.ToInt32(str[1]) + i * 500).ToString();
            MySQLHelper.InsertData(tablename, name, str);
        }

        // 改
        string[] ss = new string[] { column3Name };
        string[] gg = new string[] { "bule" };
        string[] key = new string[] { column2Name };
        string[] value = new string[] { "1600" };
        string[] operation = new string[] { "=" };
        MySQLHelper.UpdateDataOR(tablename, ss, gg, key, value, operation);

        // 查
        MySqlDataReader reader = MySQLHelper.SelectDataOR(tablename, key, ss, gg, operation);
        MySQLHelper.ShowData(reader);

        // 删
        MySQLHelper.DeleteDataOR(tablename, ss, gg, operation);
        // 查看整张表
        MySqlDataReader reader2 = MySQLHelper.ReadFullTable(tablename);
        MySQLHelper.ShowData(reader2);

        // 关闭数据库
	    MySQLHelper.CloseDataBase();
    }

    // 关闭数据库
    void Update () {
	    if (Input.GetKeyDown(KeyCode.Escape))
	    {
	        // 关闭数据库
	        MySQLHelper.CloseDataBase();
        }
    }
}
