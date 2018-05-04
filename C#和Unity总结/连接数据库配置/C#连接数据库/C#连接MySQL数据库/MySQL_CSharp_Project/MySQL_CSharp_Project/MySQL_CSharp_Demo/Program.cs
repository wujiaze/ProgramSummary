using MySQL_CSharp_Formwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace MySQL_CSharp_Demo
{
    class Program
    {
        static void Main(string[] args)
        {

            MySQLHelper.OpenDataBase("localhost",3306,"testbase","root","83117973bb");
            string tablename = "rr";
            string tablename2 = "pp";
            string column0Name = "ID";
            string column0Type = "text";
            string column1Name = "type";
            string column1Type = "text";
            string column2Name = "price";
            string column2Type = "int";
            string column3Name = "color";
            string column3Type = "text";
            string[] str = new string[] { "二手奥拓", "1600", "red" };
            string[] types = new string[] { column0Type,column1Type, column2Type, column3Type };
            string[] name = new string[] { column0Name,column1Name, column2Name, column3Name };
            MySQLHelper.CreateTable(tablename, name, types);
            MySQLHelper.CreateTable(tablename2, name, types);
            //for (int i = 0; i < 5; i++)
            //{
            //    str[1] = (Convert.ToInt32(str[1]) + i * 1000).ToString();
            //    MySQLHelper.InsertData(tablename, str);
            //}
            //string[] ss = new string[] { column3Name };
            //string[] gg = new string[] { "bule" };
            //string[] key = new string[] { column2Name };
            //string[] value = new string[] { "1600" };
            //string[] operation = new string[] { "=" };
            //MySQLHelper.UpdateDataOR(tablename, ss, gg, key, value, operation);

            //MySqlDataReader reader = MySQLHelper.SelectDataOR(tablename, key, ss, gg, operation);

            //MySQLHelper.ShowData(reader);
            //MySQLHelper.InsertData(tablename, str);


            //MySQLHelper.DeleteDataOR(tablename, ss, gg, operation);
            //MySqlDataReader reader2 = MySQLHelper.ReadFullTable(tablename);
            //MySQLHelper.ShowData(reader2);
            MySQLHelper.CloseDataBase();
            Console.Read();
        }
    }
}
