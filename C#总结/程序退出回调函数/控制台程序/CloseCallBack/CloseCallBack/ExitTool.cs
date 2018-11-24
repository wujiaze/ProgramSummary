using System;
using System.Runtime.InteropServices;
using System.Threading;

public class ExitTool
{
    private enum CtrlType
    {
        CTRL_C_EVENT = 0,       //当用户按下了 CTRL + C                   或者由GenerateConsoleCtrlEvent API发出
        CTRL_BREAK_EVENT = 1,   //当用户按下了 CTRL + BREAK(键盘的右上角)  或者由GenerateConsoleCtrlEvent API发出
        CTRL_CLOSE_EVENT = 2,   //当试图关闭控制台程序，系统发送关闭消息
        CTRL_LOGOFF_EVENT = 5,  //用户退出时，但是不能决定是哪个用户. 
        CTRL_SHUTDOWN_EVENT = 6 //当系统被关闭时
    }
    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool SetConsoleCtrlHandler(ControlCtrlDelegate handlerRoutine, bool add); // 由于不支持泛型，所以采用delegate
    private delegate bool ControlCtrlDelegate(CtrlType ctrlType);

    private static bool _canExit;


    private static bool HandlerRoutine(CtrlType ctrlType)
    {
        switch (ctrlType)
        {
            case CtrlType.CTRL_C_EVENT:
                OnCtrlCEvent?.Invoke();
                break;
            case CtrlType.CTRL_BREAK_EVENT:
                OnCtrlBreakEvent?.Invoke();
                break;
            case CtrlType.CTRL_CLOSE_EVENT:
                OnCloseEvent?.Invoke();
                break;
            case CtrlType.CTRL_LOGOFF_EVENT:
                OnLogoffEvent?.Invoke();
                break;
            case CtrlType.CTRL_SHUTDOWN_EVENT:
                OnShutDownEvent?.Invoke();
                break;
        }
        OnAllEvent?.Invoke();
        InitEvents();
        _canExit = true;
        SetConsoleCtrlHandler(HandlerRoutine, false);
        Environment.Exit(0);
        return true;
    }

    /// <summary>
    /// 设置工程关闭回调
    /// </summary>
    /// <returns></returns>
    public static bool SetCloseEvent()
    {
        _canExit = false;
        InitEvents();

        bool result = SetConsoleCtrlHandler(HandlerRoutine, true);
        if (result)
            Console.WriteLine("工程关闭执行回调设置成功");
        else
            Console.WriteLine("工程关闭执行回调设置失败");
        return result;
    }

    /// <summary>
    /// 本方法需要放在程序执行末尾
    /// </summary>
    public static void WaitCloseProject()
    {
        while (!_canExit)
        {
            Thread.Sleep(5000);
        }
    }

    public static Action OnCtrlCEvent;
    public static Action OnCtrlBreakEvent;
    public static Action OnCloseEvent;
    public static Action OnLogoffEvent;
    public static Action OnShutDownEvent;
    public static Action OnAllEvent;

    private static void InitEvents()
    {
        OnCtrlCEvent = null;
        OnCtrlBreakEvent = null;
        OnCloseEvent = null;
        OnLogoffEvent = null;
        OnShutDownEvent = null;
        OnAllEvent = null;
    }
}
