using UnityEngine;
using UnityEngine.Networking;

public class MyDownloadHandler : DownloadHandlerScript
{
    public MyDownloadHandler():base()
    {
        
    }

    public MyDownloadHandler(byte[] buffer):base(buffer)
    {
        
    }
    
    protected override bool ReceiveData(byte[] datas, int dataLength)
    {
        if (datas ==null || datas.Length<1)
        {
            Debug.Log("LoggingDownloadHandler :: ReceiveData - received a null/empty buffer");
            return false;
        }
        else
        {
            Debug.Log(string.Format("LoggingDownloadHandler :: ReceiveData - received {0} bytes", dataLength));
            return true;
        }
    }

    protected override void CompleteContent()
    {
        Debug.Log("LoggingDownloadHandler :: CompleteContent - DOWNLOAD COMPLETE!");
    }
    protected override void ReceiveContentLength(int contentLength)
    {
        Debug.Log(string.Format("LoggingDownloadHandler :: ReceiveContentLength - length {0}", contentLength));
    }


    protected override float GetProgress()
    {
        return base.GetProgress();
    }

    protected override string GetText()
    {
        return base.GetText();
    }
    protected override byte[] GetData()
    {
        return base.GetData();
    }



}
