﻿/*
 *      LogFormwork ： 自定义Log框架
 *         添加人物时：1、添加枚举 2、添加 log4net.ILog 对象 3、添加方法参数
 *                    4、添加 log4net.config 文件中 的 logger
 *
 *
 */
namespace LogFormwork
{
    public enum People
    {
        A,
        B
    }

    class LogHelper
    {
        private static readonly log4net.ILog LoggerA = log4net.LogManager.GetLogger("People_A");
        private static readonly log4net.ILog LoggerB = log4net.LogManager.GetLogger("People_B");



        /// <summary>
        /// 严重警告信息
        /// </summary>
        /// <param name="people">编写人</param>
        /// <param name="info">错误信息</param>
        /// <param name="e">具体信息</param>
        public static void LogFatal(People people,string info)
        {
            switch (people)
            {
                case People.A:
                    LoggerA.Fatal(info);
                    break;
                case People.B:
                    LoggerB.Fatal(info);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="people">编写人</param>
        /// <param name="info">错误信息</param>
        /// <param name="e">具体信息</param>
        public static void LogError(People people, string info)
        {
            switch (people)
            {
                case People.A:
                    LoggerA.Error(info);
                    break;
                case People.B:
                    LoggerB.Error(info);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 警告信息
        /// </summary>
        /// <param name="people">编写人</param>
        /// <param name="info">错误信息</param>
        /// <param name="e">具体信息</param>
        public static void LogWarn(People people, string info)
        {
            switch (people)
            {
                case People.A:
                    LoggerA.Warn(info);
                    break;
                case People.B:
                    LoggerB.Warn(info);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 提示信息
        /// </summary>
        /// <param name="people">编写人</param>
        /// <param name="info">错误信息</param>
        /// <param name="e">具体信息</param>
        public static void LogInfo(People people, string info)
        {
            switch (people)
            {
                case People.A:
                    LoggerA.Info(info);
                    break;
                case People.B:
                    LoggerB.Info(info);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="people">编写人</param>
        /// <param name="info">错误信息</param>
        /// <param name="e">具体信息</param>
        public static void LogDebug(People people, string info)
        {
            switch (people)
            {
                case People.A:
                    LoggerA.Debug(info);
                    break;
                case People.B:
                    LoggerB.Debug(info);
                    break;
                default:
                    break;
            }
        }
    }
}
