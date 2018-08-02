/*
 *      主题： 模拟鼠标按键
 *      使用方法： 类名 + 模拟的方法，参数是Unity坐标系
 *      使用条件： 针对打包文件
 *
 *      提示：1、Unity屏幕坐标从左下角开始，向右为X轴，向上为Y轴
 *            2、Windows屏幕坐标从左上角开始，向右为X轴，向下为Y轴
 */
using System;
using System.Runtime.InteropServices;
using UnityEngine;

public static class MouseSimulater
{
    #region 获取窗口的位置和大小

    [StructLayout(LayoutKind.Sequential)] // 表示该结构体按顺序赋值
    private struct WinRect
    {
        public int Left; //最左坐标
        public int Top; //最上坐标
        public int Right; //最右坐标
        public int Bottom; //最下坐标
    }

    private struct MyRectInWin
    {
        private int _left;      //最左坐标
        private int _right;     //最下坐标
        private int _bottom;    //最左坐标
        private int _top;       //最下坐标
        private int _weight;                // 需要
        private int _height;                // 需要
        private Vector2 _leftBottom;
        public int Weight { get { return _weight; } }
        public int Height { get { return _height; } }
        public Vector2 LeftBottom { get { return _leftBottom; } }
        public void UpdateRect(WinRect winRect)
        {
            _left = winRect.Left;
            _right = winRect.Right;
            _bottom = winRect.Bottom;
            _top = winRect.Top;
            _weight = _right - _left;
            _height = _bottom - _top;
            _leftBottom = new Vector2(_left, _bottom);
        }
    }

    [DllImport("user32.dll")]
    private static extern IntPtr GetDesktopWindow();                             // 获取桌面窗口的句柄

    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();                          // 获取当前窗口的句柄

    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(IntPtr hWnd, ref WinRect winRect);  // 获取窗口的Rect

    #endregion

    private static WinRect _temp;
    private static MyRectInWin _desktopWin;   // 当前桌面的Rect （Windows坐标系）
    private static MyRectInWin _unityWin;     // 当前Unity窗口的Rect（Windows坐标系）
    static MouseSimulater()
    {
        _temp = new WinRect();
        _desktopWin = new MyRectInWin();
        _unityWin = new MyRectInWin();
        RefreshWin();
        RefreshUnityWin();
        Cursor.visible = true;
    }

    private static void RefreshWin()
    {
        // Windows desktop
        IntPtr deskPtr = GetDesktopWindow();
        bool result = GetWindowRect(deskPtr, ref _temp);
        if (result == false)
            throw new Exception("获取不到当前窗口");
        _desktopWin.UpdateRect(_temp);
    }

    private static void RefreshUnityWin()
    {
        // Unity win
        IntPtr unityPtr = GetForegroundWindow();
        bool result = GetWindowRect(unityPtr, ref _temp);
        if (result == false)
            throw new Exception("获取不到当前窗口");
        _unityWin.UpdateRect(_temp);
    }


    #region 设置鼠标位置，触发鼠标事件
    [Flags]
    private enum MouseEventFlag : uint
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
    [DllImport("user32.dll")]
    private static extern int SetCursorPos(int x, int y);

    //  dwFlags,         // motion and click options
    //  dx,              // horizontal position or change
    //  dy,              // vertical position or change
    //  dwData,          // wheel movement
    //  dwExtraInfo  // application-defined information
    [DllImport("user32.dll")]
    private static extern void mouse_event(MouseEventFlag dwFlags, int dx, int dy, uint dwData, UIntPtr dwExtraInfo);

    #endregion

    // 移动鼠标到指定位置  Windows坐标系
    private static bool MoveTo(float x, float y)
    {
        try
        {
            SetCursorPos((int)x, (int)y);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
    // 获取鼠标的位置
    private static Vector2 GetCurrentPos(float norX, float norY)
    {
        Vector2 v2InWin = Vector2.zero;
        if (norX < 0 || norY < 0) return v2InWin;
        RefreshUnityWin();               // 更新Unity窗口信息
        if (Screen.fullScreen)  // 全屏
        {
            if (Screen.width == _desktopWin.Weight && Screen.height == _desktopWin.Height)//全屏分辨率一致
            {
                v2InWin = _unityWin.LeftBottom + new Vector2(norX * _unityWin.Weight, -norY * _unityWin.Height);
            }
            else //全屏分辨率不一致
            {
                float tempWeight = _desktopWin.Height * Screen.width / (float)Screen.height;
                float tempHeight = _desktopWin.Height;
                Vector2 temp = new Vector2(0.5f * (_desktopWin.Weight - tempWeight), 0);
                v2InWin = temp + _unityWin.LeftBottom + new Vector2(norX * tempWeight, -norY * tempHeight);
            }
            v2InWin.y -= 1; // 修正误差
        }
        else      // 非全屏
        {
            float borderHeight = 0;
            if (true)     // TODO 以后知道如何获取是否是全屏无边框模式，再获取边框的高度
            {
                // 有边框
                borderHeight = 11; //去掉边框的高度
            }
            v2InWin = _unityWin.LeftBottom + new Vector2(norX * _unityWin.Weight, -norY * _unityWin.Height);
            v2InWin.y += borderHeight; // 修正误差
        }
        return v2InWin;
    }


    //左键单击 输入参数为 Unity坐标系 
    public static void LeftClick(float x, float y, bool hidCursor = false)
    {
        float unitX = x / Screen.width;
        float unitY = y / Screen.height;
        LeftClickNor(unitX, unitY, hidCursor);
    }
    //左键单击 输入参数为 Unity坐标系的归一化坐标
    public static void LeftClickNor(float norX, float norY, bool hidCursor = false)
    {
        // 是否显示鼠标图标
        Cursor.visible = !hidCursor;
        Vector2 temp = GetCurrentPos(norX, norY);
        // 移动并触发鼠标事件
        if (MoveTo(temp.x, temp.y))
        {
            mouse_event(MouseEventFlag.LeftDown, 0, 0, 0, UIntPtr.Zero);
            mouse_event(MouseEventFlag.LeftUp, 0, 0, 0, UIntPtr.Zero);
        }
    }


    // 右键单击
    public static void RightClick(float x, float y, bool hidCursor = false)
    {
        float unitX = x / Screen.width;
        float unitY = y / Screen.height;
        RightClickNor(unitX, unitY, hidCursor);
    }

    public static void RightClickNor(float norX, float norY, bool hidCursor = false)
    {
        Cursor.visible = !hidCursor;
        Vector2 temp = GetCurrentPos(norX, norY);
        if (MoveTo(temp.x, temp.y))// 移动并触发鼠标事件
        {
            mouse_event(MouseEventFlag.RightDown, 0, 0, 0, UIntPtr.Zero);
            mouse_event(MouseEventFlag.RightUp, 0, 0, 0, UIntPtr.Zero);
        }
    }



    // 中键单击
    public static void MiddleClick(float x, float y, bool hidCursor = false)
    {
        float unitX = x / Screen.width;
        float unitY = y / Screen.height;
        MiddleClickNor(unitX, unitY, hidCursor);
    }
    public static void MiddleClickNor(float norX, float norY, bool hidCursor = false)
    {
        Cursor.visible = !hidCursor;
        Vector2 temp = GetCurrentPos(norX, norY);
        if (MoveTo(temp.x, temp.y))
        {
            mouse_event(MouseEventFlag.MiddleDown, 0, 0, 0, UIntPtr.Zero);
            mouse_event(MouseEventFlag.MiddleUp, 0, 0, 0, UIntPtr.Zero);
        }
    }

    // 左键按下
    public static void LeftDown(float x, float y, bool hidCursor = false)
    {
        float unitX = x / Screen.width;
        float unitY = y / Screen.height;
        LeftDownNor(unitX, unitY, hidCursor);
    }
    public static void LeftDownNor(float norX, float norY, bool hidCursor = false)
    {
        Cursor.visible = !hidCursor;
        Vector2 temp = GetCurrentPos(norX, norY);
        if (MoveTo(temp.x, temp.y))
        {
            mouse_event(MouseEventFlag.LeftDown, 0, 0, 0, UIntPtr.Zero);
        }
    }

    // 左键抬起
    public static void LeftUp(float x, float y, bool hidCursor = false)
    {
        float unitX = x / Screen.width;
        float unitY = y / Screen.height;
        LeftUpNor(unitX, unitY, hidCursor);
    }
    public static void LeftUpNor(float norX, float norY, bool hidCursor = false)
    {
        Cursor.visible = !hidCursor;
        Vector2 temp = GetCurrentPos(norX, norY);
        if (MoveTo(temp.x, temp.y))
        {
            mouse_event(MouseEventFlag.LeftUp, 0, 0, 0, UIntPtr.Zero);
        }
    }

    // 右键按下
    public static void RightDown(float x, float y, bool hidCursor = false)
    {
        float unitX = x / Screen.width;
        float unitY = y / Screen.height;
        RightDownNor(unitX, unitY, hidCursor);
    }
    public static void RightDownNor(float norX, float norY, bool hidCursor = false)
    {
        Cursor.visible = !hidCursor;
        Vector2 temp = GetCurrentPos(norX, norY);
        if (MoveTo(temp.x, temp.y))
        {
            mouse_event(MouseEventFlag.RightDown, 0, 0, 0, UIntPtr.Zero);
        }
    }

    // 右键抬起
    public static void RightUp(float x, float y, bool hidCursor = false)
    {
        float unitX = x / Screen.width;
        float unitY = y / Screen.height;
        RightUpNor(unitX, unitY, hidCursor);
    }
    public static void RightUpNor(float norX, float norY, bool hidCursor = false)
    {
        Cursor.visible = !hidCursor;
        Vector2 temp = GetCurrentPos(norX, norY);
        if (MoveTo(temp.x, temp.y))
        {
            mouse_event(MouseEventFlag.RightUp, 0, 0, 0, UIntPtr.Zero);
        }
    }

    // 中键按下
    public static void MiddleDown(float x, float y, bool hidCursor = false)
    {
        float unitX = x / Screen.width;
        float unitY = y / Screen.height;
        MiddleDownNor(unitX, unitY, hidCursor);
    }
    public static void MiddleDownNor(float norX, float norY, bool hidCursor = false)
    {
        Cursor.visible = !hidCursor;
        Vector2 temp = GetCurrentPos(norX, norY);
        if (MoveTo(temp.x, temp.y))
        {
            mouse_event(MouseEventFlag.MiddleDown, 0, 0, 0, UIntPtr.Zero);
        }
    }


    // 中键抬起
    public static void MiddleUp(float x, float y, bool hidCursor = false)
    {
        float unitX = x / Screen.width;
        float unitY = y / Screen.height;
        MiddleUpNor(unitX, unitY, hidCursor);
    }
    public static void MiddleUpNor(float norX, float norY, bool hidCursor = false)
    {
        Cursor.visible = !hidCursor;
        Vector2 temp = GetCurrentPos(norX, norY);
        if (MoveTo(temp.x, temp.y))
        {
            mouse_event(MouseEventFlag.MiddleUp, 0, 0, 0, UIntPtr.Zero);
        }
    }

    //// 滚轮滚动
    //public static void ScrollWheel(float value)
    //{
    //    mouse_event(MouseEventFlag.Wheel, 0, 0, (uint)value, UIntPtr.Zero);
    //}
}