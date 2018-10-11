/*
 *         主题： Log4NetHelper 工具类
 *               多人 C#使用Log4net 打印日志输出到 回滚文件
 *
 *         设置方法：1、添加合适版本的 log4net.dll  ，属性中的复制到本地：True
 *                  2、编写合适的 log4net.config (详见文件)，属性中的复制到输出目录：始终复制
 *                  3、在 AssemblyInfo.cs 文件中加上这么一条：表示使用 log4net.config 这个配置文件（这使用了相对路径）
 *                     [assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]
 *                  4、然后其余设置就参考这个工具类中
 *
 *         使用方法： 在需要输出日志的地方
 *                   LogHelper.LogDebug("xxx");     LogHelper.LogInfo("xxx");
 *                   LogHelper.LogWarn("xxx");      LogHelper.LogError("xxx");
 *                   LogHelper.LogFatal("xxx");
 *                  
 *                   添加 不同的使用人员：
 *                   1、在本类中创建 log4net.ILog 对象 ，添加 LogPeople 枚举值，加入字典
 *                   2、在 log4net.config 中，添加新的记录器
 *                   3、在 log4net.config 中，添加自定义的写入文件的形式
 *                   
 */


using System.Collections.Generic;

namespace LogDemo2
{
    public enum LogPeople { A, B }
    public class LogHelper
    {
        private static Dictionary<LogPeople, log4net.ILog> dictLogger;

        private static readonly log4net.ILog loggerA;
        private static readonly log4net.ILog loggerB;

        static LogHelper()
        {
            // 采用的是 People_A 记录器
            loggerA = log4net.LogManager.GetLogger("People_A");
            // 采用的是 People_B 记录器
            loggerB = log4net.LogManager.GetLogger("People_B");

            dictLogger = new Dictionary<LogPeople, log4net.ILog>();
            dictLogger.Add(LogPeople.A, loggerA);
            dictLogger.Add(LogPeople.B, loggerB);
        }


        /// <summary>
        /// 严重警告信息
        /// </summary>
        /// <param name="info">错误信息</param>
        public static void LogFatal(LogPeople peo, string info)
        {
            if (dictLogger.ContainsKey(peo))
                dictLogger[peo].Fatal(info);
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="info">错误信息</param>
        public static void LogError(LogPeople peo, string info)
        {
            if (dictLogger.ContainsKey(peo))
                dictLogger[peo].Error(info);
        }

        /// <summary>
        /// 警告信息
        /// </summary>
        /// <param name="info">错误信息</param>
        public static void LogWarn(LogPeople peo, string info)
        {
            if (dictLogger.ContainsKey(peo))
                dictLogger[peo].Warn(info);
        }

        /// <summary>
        /// 提示信息
        /// </summary>
        /// <param name="info">错误信息</param>
        public static void LogInfo(LogPeople peo, string info)
        {
            if (dictLogger.ContainsKey(peo))
                dictLogger[peo].Info(info);
        }

        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="info">错误信息</param>
        public static void LogDebug(LogPeople peo, string info)
        {
            if (dictLogger.ContainsKey(peo))
                dictLogger[peo].Debug(info);
        }
    }
}
