/*
 *      DownloadHandlerScript
 *
 *          将各类回调，以自己的想法重写
 */

using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class MyDownloadHandler : DownloadHandlerScript
{
    private byte[] buildin_buffer;
    public MyDownloadHandler():base()
    {
        
    }

    public MyDownloadHandler(byte[] buffer):base(buffer)
    {
        buildin_buffer = buffer;
    }

    // 网络下载的数据
    protected override bool ReceiveData(byte[] datas, int dataLength) 
    {
        if (datas ==null || datas.Length<1)
        {
            Debug.Log("LoggingDownloadHandler :: ReceiveData - received a null/empty buffer");
            return false;
        }
        else
        {
            buildin_buffer = datas;
            return true;
        }
    }

    protected override void CompleteContent()
    {
        Debug.Log("数据接收完毕调用");
    }
    protected override void ReceiveContentLength(int contentLength)
    {
        Debug.Log("接收到 http 的 header");
    }


    protected override float GetProgress()
    {
        return 0;
    }

    protected override string GetText()
    {
        return Encoding.UTF8.GetString(GetData());
    }
    protected override byte[] GetData()
    {
        return buildin_buffer;
    }



}
