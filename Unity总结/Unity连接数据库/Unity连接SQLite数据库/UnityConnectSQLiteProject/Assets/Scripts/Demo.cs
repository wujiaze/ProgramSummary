
using Mono.Data.Sqlite;
using SqliteTool;
using UnityEngine;

public class Demo : MonoBehaviour {

	// Use this for initialization
	void Start () {
        // 打开数据库
        string dataPath = "D:/Desktop/LogDataBase.db";
        SQLiteHelper.OpenDataBase(dataPath);
        // Demo01 创建表
        SQLiteHelper.CreateTable("LogRoot3", new string[] { "Id", "value" }, new string[] { FieldType.VarChar.ToString(), FieldType.Int.ToString() }, new string[] { "10", "5" }, new string[] { FieldState.Null.ToString(), FieldState.Null.ToString() });
        // Demo02 插入值
        SQLiteHelper.InsertData("LogRoot3", new string[] { "2", "4" });
        // Demo03 修改值
        SQLiteHelper.UpdateData("LogRoot3", new string[] { "Id" }, new string[] { "10" }, "value", "3", ">");
        // Demo04 查询值
        SqliteDataReader reader = SQLiteHelper.SelectData("LogRoot3", new string[] { "Id" }, "value", "4", "=");
        SQLiteHelper.ShowData(reader);
        // Demo05 删除值
        SQLiteHelper.DeleteData("LogRoot3", "Id", "11", "!=");
        // 关闭数据库
        SQLiteHelper.CloseDataBase();
    }
	
	// Update is called once per frame
	void Update () {
	    // 创建数据库
        if (Input.GetKeyDown(KeyCode.A))
	    {
            string newdataPath = "Mydata.db";
            SQLiteHelper.OpenDataBase(newdataPath);
            SQLiteHelper.CloseDataBase();
        }
    }
}
