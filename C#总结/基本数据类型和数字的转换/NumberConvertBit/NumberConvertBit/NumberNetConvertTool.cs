using System;
using System.Net;

public class NumberNetConvertTool
{
    /// <summary>
    /// 小端系统发送给大端系统时，进行转换
    ///     只有 发送消息方 和 接受消息方 的大小端环境不同，才需要进行大小端转换
    ///     发送方，方法一
    /// </summary>
    public static byte[] NetSendConvert1(int sendNumber)
    {
        byte[] sendBytes = BitConverter.GetBytes(sendNumber);
        if (BitConverter.IsLittleEndian)
        {
            // 需要转
            Array.Reverse(sendBytes);
        }
        return sendBytes;
    }
    /// <summary>
    /// 小端系统发送给大端系统时，进行转换
    ///     只有 发送消息方 和 接受消息方 的大小端环境不同，才需要进行大小端转换
    ///     发送方，方法二
    /// </summary>
    public static byte[] NetSendConvert2(int sendNumber)
    {
        // 自动判断本地和网络端的大小端环境
        int resultNumber = IPAddress.HostToNetworkOrder(sendNumber);
        byte[] sendBytes = BitConverter.GetBytes(resultNumber);
        return sendBytes;
    }

    /// <summary>
    /// 小端系统接受给大端系统时，进行转换
    ///     只有 发送消息方 和 接受消息方 的大小端环境不同，才需要进行大小端转换
    ///     接受方，方法一,真正需要使用的时候，需要重写这里的内容，这里只是拿int举例
    /// </summary>
    public static int NetReceiveConvert1(byte[] netBytes)
    {
        if (BitConverter.IsLittleEndian)
        {
            // 需要转
            Array.Reverse(netBytes);
        }
        int result = BitConverter.ToInt32(netBytes, 0);
        return result;
    }
    /// <summary>
    /// 小端系统接受给大端系统时，进行转换
    ///     只有 发送消息方 和 接受消息方 的大小端环境不同，才需要进行大小端转换
    ///     接收方，方法二,真正需要使用的时候，需要重写这里的内容，这里只是拿int举例
    /// </summary>
    public static int NetReceiveConvert2(byte[] netBytes)
    {
        // 直接拿来用
        int result = BitConverter.ToInt32(netBytes, 0);
        // 再转换
        result = IPAddress.NetworkToHostOrder(result);
        return result;
    }
}