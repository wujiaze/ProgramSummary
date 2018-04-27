using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySqlTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    MySQLController controller =new MySQLController();
	    string DbPath = "Host =localhost;port = 3306;Database=MySqlDatabase;Username=root;Password=83117973bb";
	    controller.OpenDataBase(DbPath);
	    string tableName = "UnityMysqlTest";
	    controller.CreatTable(tableName, "Name", "varchar", "Age", "int", "Sex", "varchar");
	    controller.Insert(tableName, "小明", 18, "man");
	    controller.Insert(tableName, "小明", 20, "man");
	    controller.Insert(tableName, "小明", 40, "man");
	    controller.Delete(tableName, "id", "1");
	    controller.Update(tableName, "name", "小刚", "Age", "18");
	    controller.SelectData(tableName, "age", "Name", "小刚");
	    controller.CloseDataBase();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
