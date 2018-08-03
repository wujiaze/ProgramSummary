/*
 *         主题： LogHelper 工具类
 *               单人 Unity使用Log4net 打印日志输出到 回滚文件
 *
 *         设置方法：1、添加合适版本的 log4net.dll ,这里使用了 .net2.0 版本，放入 Plugin 文件夹
 *                  2、编写合适的 log4net.config (详见文件), 放在 _logConfigPath
 *                     放在 StreamingAssets 文件夹之下
 *                  3、获取 log4net.config
 *                     log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(_logConfigPath));
 *                  4、获取 logger
 *                     log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
 *         
 *         使用方法： 在需要输出日志的地方
 *                   LogHelper.LogDebug("xxx");     LogHelper.LogInfo("xxx");
 *                   LogHelper.LogWarn("xxx");      LogHelper.LogError("xxx");
 *                   LogHelper.LogFatal("xxx");
 *                   
 *                   
 */

using System.IO;
using UnityEngine;

public class LogHelper
{
    private static string _logConfigPath = Application.streamingAssetsPath + @"/LogConfig/log4net.config";
    private static readonly log4net.ILog logger;
    static LogHelper()
    {
        // 方法1： System.Reflection.MethodBase.GetCurrentMethod().DeclaringType   
        // 方法2： typeof(LogHelper)
        // 方法1 和 方法2 的效果在这里是一样的，日志输出格式中的%logger 就是本类的完整名称
        // 采用的是 root 记录器
        log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(_logConfigPath));
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

