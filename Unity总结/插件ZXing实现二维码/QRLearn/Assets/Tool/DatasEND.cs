using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataSend : MonoBehaviour {

    /// <summary>
    /// 将本地文件中的音乐，视频上传
    /// </summary>
    /// <param name="path"></param>
    public static void SendQrToServer(string path)
    {
        if (!File.Exists(path))
            throw new Exception("路径不存在 " + path);
        byte[][] bytesArr;
        FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None, 1024000);
        int count = Mathf.CeilToInt(1f * fs.Length / int.MaxValue);
        Debug.Log(count);
        bytesArr = new byte[count][];
        for (int i = 0; i < count; i++)
        {
            byte[] bytes = null;
            if (i == count - 1)
            {
                bytes = new byte[fs.Length - fs.Position];
                fs.BeginRead(bytes, 0, bytes.Length, CompleteCallBack, fs);
            }
            else
            {
                bytes = new byte[Int32.MaxValue];
                fs.BeginRead(bytes, 0, int.MaxValue, StepCompleteCallBack, fs);
            }
            bytesArr[i] = bytes;
        }
    }
    private static void StepCompleteCallBack(IAsyncResult ar)
    {
        FileStream stream = ar.AsyncState as FileStream;
        if (stream == null) return;
        stream.EndRead(ar);
        Debug.Log(stream.Position);
    }
    private static void CompleteCallBack(IAsyncResult ar)
    {
        FileStream stream = ar.AsyncState as FileStream;
        if (stream == null) return;
        stream.EndRead(ar);
        Debug.Log(stream.Position);
        stream.Close();
    }

    // 将Unity录制的音频视频上传
    private void Contvert()
    {

    }
}
