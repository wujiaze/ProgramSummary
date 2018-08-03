/*
 *         主题： Log4NetHelper 工具类
 *               单人 C#使用Log4net 打印日志输出到 回滚文件
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
 *                   
 */
namespace LogDemo2
{
    public class LogHelper
    {
        private static readonly log4net.ILog logger;
        static LogHelper()
        {
            // 方法1： System.Reflection.MethodBase.GetCurrentMethod().DeclaringType   采用反射获取本类的完整名称
            // 方法2： typeof(LogHelper)
            // 方法1 和 方法2 的效果在这里是一样的，日志输出格式中的%logger 就是本类的完整名称
            // 采用的是 root 记录器
            logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        /// <summary>
        /// 严重警告信息
        /// </summary>
        /// <param name="info">错误信息</param>
        public static void LogFatal(string info)
        {
            logger.Fatal(info);
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="info">错误信息</param>
        public static void LogError(string info)
        {
            logger.Error(info);
        }

        /// <summary>
        /// 警告信息
        /// </summary>
        /// <param name="info">错误信息</param>
        public static void LogWarn(string info)
        {
            logger.Warn(info);
        }

        /// <summary>
        /// 提示信息
        /// </summary>
        /// <param name="info">错误信息</param>
        public static void LogInfo(string info)
        {
            logger.Info(info);
        }

        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="info">错误信息</param>
        public static void LogDebug(string info)
        {
            logger.Debug(info);
        }
    }
}