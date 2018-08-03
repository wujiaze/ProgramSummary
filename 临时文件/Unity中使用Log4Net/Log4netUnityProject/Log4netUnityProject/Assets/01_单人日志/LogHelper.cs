/*
 *      Title：LogFormwork ： 自定义Log框架

 */
using log4net.Config;
using System.Collections;
using System.IO;
using log4net;

public class LogHelper
{
    private static readonly ILog logger;
    static LogHelper()
    {
        logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //SetLogPath(Application.dataPath + "/ProjectDemo/log4net.config");
    }

    /// <summary>
    /// 设置log4net配置文件的路径
    /// </summary>
    /// <param name="path"> log4net.config 的路径</param>
    /// <returns></returns>
    public static ICollection SetLogPath(string path)
    {
        return XmlConfigurator.Configure(new FileInfo(path));
    }

    /// <summary>
    /// 严重警告信息
    /// </summary>
    /// <param name="info">错误信息</param>
    /// <param name="e">具体信息</param>
    public static void LogFatal(string info)
    {
        logger.Fatal(info);
    }

    /// <summary>
    /// 错误信息
    /// </summary>
    /// <param name="info">错误信息</param>
    /// <param name="e">具体信息</param>
    public static void LogError(string info)
    {
        logger.Error(info);
    }

    /// <summary>
    /// 警告信息
    /// </summary>
    /// <param name="info">错误信息</param>
    /// <param name="e">具体信息</param>
    public static void LogWarn( string info)
    {
        logger.Warn(info);
    }

    /// <summary>
    /// 提示信息
    /// </summary>
    /// <param name="info">错误信息</param>
    /// <param name="e">具体信息</param>
    public static void LogInfo( string info)
    {
        logger.Info(info);
    }

    /// <summary>
    /// 调试信息
    /// </summary>
    /// <param name="info">错误信息</param>
    /// <param name="e">具体信息</param>
    public static void LogDebug( string info)
    {
        logger.Debug(info);
    }
}



