using log4net.Config;
using System.IO;
using UnityEngine;

public class Log4NetTest : MonoBehaviour {

    // 反射获取当前类的类型
    private static readonly log4net.ILog myLogger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    private static readonly log4net.ILog myLogger2 = log4net.LogManager.GetLogger("Com");
    void Start () {
	    // 初始化log
        string path = Application.dataPath + "Plugins/log4net.config";
        XmlConfigurator.Configure(new FileInfo(path));// 文件路径在bin/DeBug
	    myLogger.Warn("这是一个警告日志");
	    myLogger.Info("单击了按钮");
	    myLogger.Debug("用Log4Net写入数据库日志");
	    myLogger.Error("这是一个错误日志");
	    myLogger.Fatal("这是一个致命的错误日志");
	    myLogger2.Warn("通过Nmae来获取不同的记录器");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
}
