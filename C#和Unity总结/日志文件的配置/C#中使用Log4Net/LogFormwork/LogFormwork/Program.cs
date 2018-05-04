/*
 *      Title： " LogFormwork框架 " 框架  
 *
 *      Description:
 *              基于 .Net 4.6.1
 *              功能：
 *              1、输出信息到控制台
 *      Author: wujiaze
 *      Date: 2018.5.4
 *      Modify:
 *
 *
 */
using System;
using Sqlite_CSharp_Formwork;

namespace LogFormwork
{
    class Program
    {
        static void Main(string[] args)
        {
            //创建SQLite 数据库
            string path = "mydatabase.db";
            string tableNameA = "test_A";
            string tableNameB = "test_B";
            SQLiteHelper.OpenDataBase(path);
            string[] colNames = new[] { "Date", "Level", "Logger", "Message" };
            // todo 这里建表是一种最简单的表，如果需要复杂的表，建议直接在数据库中建好
            string[] colType = new[] { "DateTime", "String", "String", "String" };
            SQLiteHelper.CreateTable(tableNameA, colNames, colType);
            SQLiteHelper.CreateTable(tableNameB, colNames, colType);
            SQLiteHelper.CloseDataBase();
            int x = 10;
            int y = 0;
            try
            {
                x = x / y;
            }
            catch (Exception e)
            {
                LogHelper.LogFatal(People.A,e.ToString());
                LogHelper.LogDebug(People.B,e.ToString());
            }
            Console.Read();
        }
    }
}
