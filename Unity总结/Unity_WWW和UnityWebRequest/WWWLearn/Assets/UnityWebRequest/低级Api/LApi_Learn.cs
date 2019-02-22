/*
 *      低层API :未封装，适用于灵活需求
 *          具体来说：
 *              1、自己创建 UnityWebRequest
 *              2、自己创建 DownloadHandler，与 UnityWebRequest 联系
 *              3、自己创建 DownloadHandler，与 UnityWebRequest 联系
 *              4、certificateHandler todo
 *
 *          DownloadHandler 
 *          DownloadHandlerBuffer              简单的字符串/或二进制数据
 *          DownloadHandlerFile                下载的字节数据直接存到硬盘文件，所以内存占用少，并且无法在内存中获取任何数据，一般用于大文件/特殊需求
 *          DownloadHandlerTexture             下载图片并处理成纹理Texture
 *          DownloadHandlerAssetBundle         用于抓取/下载 AssetsBundle
 *          DownloadHandlerAudioClip           下载 Audio 文件
 *          DownloadHandlerMovieTexture        下载 Video 文件，即将被弃用，下载和播放Video推荐使用 VideoPlayer
 *          DownloadHandlerScript              该类自身什么都不处理，但是有一个接受回调，同时可以被用户自定义的类继承，即由用户自己来处理接收到的数据
 */
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class LApi_Learn : MonoBehaviour
{
    #region 下载

    // 字符串/二进制数据
    private IEnumerator DownloadTxtOrData()
    {
        UnityWebRequest request = new UnityWebRequest("url");
        request.downloadHandler = new DownloadHandlerBuffer();
        yield return request.SendWebRequest();
        if (request.isHttpError || request.isNetworkError)
        {
            Debug.Log(request.error);
        }
        else
        {
            string txt = request.downloadHandler.text;
            byte[] bytes = request.downloadHandler.data;
        }
    }

    // 下载并直接写入文件
    private IEnumerator DownLoadFile()
    {
        UnityWebRequest request = new UnityWebRequest("url");
        string filePath = @"D:\Desktop\1.txt"; // todo 文件扩展名？二进制
        request.downloadHandler = new DownloadHandlerFile(filePath);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            Debug.Log("File successfully downloaded and saved to " + filePath);
        }
    }

    // 图片纹理
    private IEnumerator DownloadTexture()
    {
        UnityWebRequest request = new UnityWebRequest("url");
        DownloadHandlerTexture handler = new DownloadHandlerTexture(true);
        request.downloadHandler = handler;
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            Texture2D tex = handler.texture;
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100);
        }
    }

    // AssetsBundle
    private IEnumerator DownLoadAssetsBundle()
    {
        UnityWebRequest request = new UnityWebRequest("url");
        DownloadHandlerAssetBundle bundleHandler = new DownloadHandlerAssetBundle(request.url, UInt32.MaxValue);
        request.downloadHandler = bundleHandler;
        yield return request.SendWebRequest();
        if (request.isHttpError || request.isNetworkError)
        {
            Debug.Log(request.error);
        }
        else
        {
            AssetBundle bundle = bundleHandler.assetBundle;
        }
    }

    // Audio todo 本方法需要验证
    private IEnumerator DownloadAudio()
    {
        UnityWebRequest request = new UnityWebRequest("url");
        DownloadHandlerAudioClip handler = new DownloadHandlerAudioClip("url", AudioType.MPEG);
        request.downloadHandler = handler;
        yield return request.SendWebRequest();
        if (request.isHttpError || request.isNetworkError)
        {
            Debug.Log(request.error);
        }
        else
        {
            AudioClip clip = handler.audioClip;
        }
    }

    // 自行处理服务器返回的数据 todo 待完善
    private IEnumerator MyDownLoad()
    {
        UnityWebRequest request = new UnityWebRequest("url");
        byte[] bytes = new byte[1024];
        MyDownloadHandler handler = new MyDownloadHandler(bytes);
        request.downloadHandler = handler;
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            // 详见另一个脚本 todo
        }
    }

    #endregion
}
