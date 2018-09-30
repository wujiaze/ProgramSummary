/*
 *         主题： LogHelper 工具类
 *               多人 Unity使用Log4net 打印日志输出到 回滚文件
 *
 *         设置方法：1、添加合适版本的 log4net.dll ,这里使用了 .net2.0 版本，放入 Plugins 文件夹
 *                  2、编写合适的 log4net.config (详见文件), 路径： _logConfigPath
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
 *                   添加 不同的使用人员：
 *                   1、添加 LogPeople 枚举值
 *                   2、在本类中创建 log4net.ILog 对象 ，，加入字典
 *                   3、在 log4net.config 中，添加新的记录器
 *                   4、在 log4net.config 中，添加自定义的写入文件的形式
 *         注意点：编辑模式下，需要运行两次Unity，才能获取第一次运行的日志文件，这是Unity的Bug
 *                Unity的输出为.Net2.0
 */

using System.Collections.Generic;
using System.IO;

public enum LogPeople { A, B }
public class LogHelper
{
    private static string _logConfigPath = UnityEngine.Application.streamingAssetsPath + @"/LogConfig/log4net.config";
    private static Dictionary<LogPeople, log4net.ILog> dictLogger;
    private static readonly log4net.ILog loggerA;
    private static readonly log4net.ILog loggerB;
    static LogHelper()
    {
        // 获取 log4net.config 配置文件
        log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(_logConfigPath));
        // 采用的是 People_A 记录器
        loggerA = log4net.LogManager.GetLogger("People_A");
        // 采用的是 People_B 记录器
        loggerB = log4net.LogManager.GetLogger("People_B");
        // 加入字典
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

