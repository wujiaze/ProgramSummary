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
using LogFramework;
using MySQL_CSharp_Formwork;
using Sqlite_CSharp_Formwork;
namespace LogDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //// 控制台和回滚文件
            //int x = 10;
            //int y = 0;
            //try
            //{
            //    x = x / y;
            //}
            //catch (Exception e)
            //{
            //    LogHelper.LogFatal(People.A, e.ToString());
            //    LogHelper.LogDebug(People.B, e.ToString());
            //}
            //Console.Read();

            // 输出到 SQLite 数据库
            // 创建 SQLite 数据库 -------一般来说这一步，是在程序的其它地方完成了，这里为了完整性，就创建了数据库和表格
            //string path = "mydatabase.db";
            //string tableNameA = "test_A";
            //string tableNameB = "test_B";
            //SQLiteHelper.OpenDataBase(path);
            //string[] colNames = new[] { "Date", "Thread", "Level", "Logger", "Message", "Exception" };
            //// todo 这里建表是一种最简单的表，如果需要复杂的表，建议直接在数据库中建好
            //string[] colType = new[] { FieldType.DateTime.ToString(), FieldType.VarChar.ToString(), FieldType.VarChar.ToString(), FieldType.VarChar.ToString(), FieldType.VarChar.ToString(), FieldType.VarChar.ToString() };
            //string[] colLength = { "6", "100", "100", "100", "1000", "4000" };
            //string[] colstate = { FieldState.Null.ToString(), FieldState.Null.ToString(), FieldState.Null.ToString(), FieldState.Null.ToString(), FieldState.Null.ToString(), FieldState.Null.ToString() };
            //SQLiteHelper.CreateTable(tableNameA, colNames, colType, colLength, colstate);
            //SQLiteHelper.CreateTable(tableNameB, colNames, colType, colLength, colstate);
            //SQLiteHelper.CloseDataBase();
            // 日志的使用
            int x = 10;
            int y = 0;
            try
            {
                x = x / y;
            }
            catch (Exception e)
            {
                LogHelper.LogFatal(People.A, "here");
                LogHelper.LogDebug(People.B, "p", e);
            }
            Console.Read();

            //// 输出到 MySQL 数据库
            ////创建 MySQL 数据库 -------一般来说这一步，是在程序的其它地方完成了，这里为了完整性，就创建了数据库和表格
            //string databasename = "log4nettest";
            //string tableNameA = "table_A";
            //string tableNameB = "table_B";
            //MySQLHelper.OpenDataBase("localhost", 3306, databasename, "root", "83117973bb");
            //// 以下各类属性和log4net.config 中保持一致
            //string[] colNames = new[] { "Date", "Thread", "Level", "Logger", "Message", "Exception" };
            //string[] colType = new[] { FiledType.DateTime.ToString(), FiledType.VarChar.ToString(), FiledType.VarChar.ToString(), FiledType.VarChar.ToString(), FiledType.VarChar.ToString(), FiledType.VarChar.ToString() };
            //string[] cloLength = { "6", "100", "100", "100", "1000", "4000" };
            //string[] colStates = { FiledState.Null.ToString(), FiledState.Null.ToString(), FiledState.Null.ToString(), FiledState.Null.ToString(), FiledState.Null.ToString(), FiledState.Null.ToString() };
            //MySQLHelper.CreateTable(tableNameA, colNames, colType, cloLength, colStates);
            //MySQLHelper.CreateTable(tableNameB, colNames, colType, cloLength, colStates);
            //MySQLHelper.CloseDataBase();
            //int x = 10;
            //int y = 0;
            //try
            //{
            //    x = x / y;
            //}
            //catch (Exception e)
            //{
            //    LogHelper.LogFatal(People.A, "hhhhhh");
            //    LogHelper.LogDebug(People.B, "SS", e);
            //}
            //Console.Read();
        }
    }
}
