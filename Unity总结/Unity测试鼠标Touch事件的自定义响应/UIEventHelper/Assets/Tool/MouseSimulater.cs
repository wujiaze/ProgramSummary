/*
 *      主题： 获取Window屏幕的
 *      使用方法： 设置Unity屏幕坐标，将其转换成Windows坐标，移动鼠标，并点击鼠标
 *      使用条件：
 *
 *      提示：1、Unity屏幕坐标从左下角开始，向右为X轴，向上为Y轴
 *            2、Windows屏幕坐标从左上角开始，向右为X轴，向下为Y轴
 */
using System;
using UnityEngine;

public class MouseSimulater
{
    #region DLLs
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    private static extern int SetCursorPos(int x, int y);
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    //  dwFlags,         // motion and click options
    //  dx,              // horizontal position or change
    //  dy,              // vertical position or change
    //  dwData,          // wheel movement
    //  dwExtraInfo  // application-defined information
    private static extern void mouse_event(MouseEventFlag dwFlags, int dx, int dy, uint dwData, UIntPtr dwExtraInfo);

    [Flags]
    enum MouseEventFlag : uint
    {
        Move = 0x0001,
        LeftDown = 0x0002,
        LeftUp = 0x0004,
        RightDown = 0x0008,
        RightUp = 0x0010,
        MiddleDown = 0x0020,
        MiddleUp = 0x0040,
        XDown = 0x0080,
        XUp = 0x0100,
        Wheel = 0x0800,
        VirtualDesk = 0x4000,
        Absolute = 0x8000
    }
    #endregion


    // TODO  1、全屏模式下，各种分辨率  2、非全屏模式下的各种分辨率
    private static Resolution[] fullResolutions = Screen.resolutions; // 当前设备支持的全屏分辨率
    private static Resolution CurrentResolution = fullResolutions[0];
    /// <summary>
    /// 移动鼠标到指定位置（使用Unity屏幕坐标而不是Windows屏幕坐标）
    /// </summary>
    public static bool MoveTo(float x, float y)
    {
        //Cursor.visible = false;     // 不显示鼠标图标
        if (x < 0 || y < 0 || x > UnityEngine.Screen.width || y > UnityEngine.Screen.height)
            return false;

        if (!UnityEngine.Screen.fullScreen)
        {
            UnityEngine.Debug.LogError("只能在全屏状态下使用！");
            return false;
        }

        
        SetCursorPos((int)x, (int)(UnityEngine.Screen.height - y));
        return true;
    }
    
    // 左键单击
    public static void LeftClick(float x = -1, float y = -1)
    {
        if (MoveTo(x*Screen.width, y*Screen.height))
        {
            mouse_event(MouseEventFlag.LeftDown, 0, 0, 0, UIntPtr.Zero);
            mouse_event(MouseEventFlag.LeftUp, 0, 0, 0, UIntPtr.Zero);
        }

    }

    // 右键单击
    public static void RightClick(float x = -1, float y = -1)
    {
        if (MoveTo(x, y))
        {
            mouse_event(MouseEventFlag.RightDown, 0, 0, 0, UIntPtr.Zero);
            mouse_event(MouseEventFlag.RightUp, 0, 0, 0, UIntPtr.Zero);
        }
    }

    // 中键单击
    public static void MiddleClick(float x = -1, float y = -1)
    {
        if (MoveTo(x, y))
        {
            mouse_event(MouseEventFlag.MiddleDown, 0, 0, 0, UIntPtr.Zero);
            mouse_event(MouseEventFlag.MiddleUp, 0, 0, 0, UIntPtr.Zero);
        }
    }

    // 左键按下
    public static void LeftDown(float x = -1, float y = -1)
    {
        if (MoveTo(x, y))
        {
            mouse_event(MouseEventFlag.LeftDown, 0, 0, 0, UIntPtr.Zero);
        }
    }

    // 左键抬起
    public static void LeftUp(float x = -1, float y = -1)
    {
        if (MoveTo(x, y))
        {
            mouse_event(MouseEventFlag.LeftUp, 0, 0, 0, UIntPtr.Zero);
        }
    }

    // 右键按下
    public static void RightDown(float x = -1, float y = -1)
    {
        if (MoveTo(x, y))
        {
            mouse_event(MouseEventFlag.RightDown, 0, 0, 0, UIntPtr.Zero);
        }
    }

    // 右键抬起
    public static void RightUp(float x = -1, float y = -1)
    {
        if (MoveTo(x, y))
        {
            mouse_event(MouseEventFlag.RightUp, 0, 0, 0, UIntPtr.Zero);
        }
    }

    // 中键按下
    public static void MiddleDown(float x = -1, float y = -1)
    {
        if (MoveTo(x, y))
        {
            mouse_event(MouseEventFlag.MiddleDown, 0, 0, 0, UIntPtr.Zero);
        }
    }

    // 中键抬起
    public static void MiddleUp(float x = -1, float y = -1)
    {
        if (MoveTo(x, y))
        {
            mouse_event(MouseEventFlag.MiddleUp, 0, 0, 0, UIntPtr.Zero);
        }
    }

    // 滚轮滚动
    public static void ScrollWheel(float value)
    {
        mouse_event(MouseEventFlag.Wheel, 0, 0, (uint)value, UIntPtr.Zero);
    }
}