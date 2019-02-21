/*
 *      高层API :封装好的，适用于普遍需求
 *          UnityWebRequest
 *          UnityWebRequestTexture
 *          UnityWebRequestAssetBundle
 *
 *          UnityWebRequestMultimedia
 *          UnityWebRequestAsyncOperation
 *      低层API :未封装，适用于灵活需求
 *          具体来说：
 *              1、自己创建 UnityWebRequest
 *              2、自己创建 DownloadHandler，与 UnityWebRequest 联系
 *              3、自己创建 DownloadHandler，与 UnityWebRequest 联系
 *
 *  DownloadHandler 
    DownloadHandlerBuffer              简单的字符串/或二进制数据
    DownloadHandlerFile                下载的字节数据直接存到硬盘文件，所以内存占用少，并且无法在内存中获取任何数据，一般用于大文件/特殊需求
    DownloadHandlerTexture             下载图片并处理成纹理Texture
    DownloadHandlerAssetBundle         用于抓取/下载 AssetsBundle
    DownloadHandlerAudioClip           下载 Audio 文件
    DownloadHandlerMovieTexture        下载 Video 文件，即将被弃用，下载和播放Video推荐使用 VideoPlayer
    DownloadHandlerScript              该类自身什么都不处理，但是有一个接受回调，同时可以被用户自定义的类继承，即由用户自己来处理接收到的数据
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class WebLearn : MonoBehaviour
{
    #region 下载

    #region 字符串/二进制数据
    // 高层API
    private IEnumerator DownLoadTxt()
    {
        UnityWebRequest request = UnityWebRequest.Get("");// 文字、二进制数据
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            /* 一般 */
            // 字符串
            string txt1 = DownloadHandlerBuffer.GetContent(request);
            // 一般用于获取原始数据
            byte[] bytes = request.downloadHandler.data;
            /* 具体 */
            DownloadHandlerBuffer buffer = (DownloadHandlerBuffer)request.downloadHandler;
            string txt2 = buffer.text;
            byte[] bytes2 = buffer.data;
        }
    }
    //底层API
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


    #endregion

    #region 下载二进制数据写入文件
    // 低层API
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

    #endregion

    #region 图片纹理
    // 高层API
    private IEnumerator DownLoadTexture()
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture("");// 图片
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            // 方法1
            //DownloadHandlerTexture texHandler = (DownloadHandlerTexture)request2.downloadHandler;
            //Texture myTexture = texHandler.texture;
            // 方法2
            Texture myTexture = DownloadHandlerTexture.GetContent(request);
        }
    }
    // 低层API
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

    #endregion

    #region AssetsBundle
    // 高层API
    private IEnumerator DownLoadAb()
    {
        UnityWebRequest request3 = UnityWebRequestAssetBundle.GetAssetBundle("");// AB
        yield return request3.SendWebRequest();
        if (request3.isNetworkError || request3.isHttpError)
        {
            Debug.Log(request3.error);
        }
        else
        {
            // 方法1
            //DownloadHandlerAssetBundle bufferHandler = (DownloadHandlerAssetBundle)request3.downloadHandler;
            //AssetBundle bundle = bufferHandler.assetBundle;
            // 方法2
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request3);
        }
    }
    // 低层API
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
    #endregion

    #region Audio
    // 高层API
    private IEnumerator DownAudio()
    {
        UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip("url", AudioType.MPEG); // MP3类型? url 和类型一定匹配？
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            AudioClip clip = DownloadHandlerAudioClip.GetContent(request);
        }
        request.Dispose(); // todo 每个都要吗
    }

    // 低层API  todo 本方法需要验证
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

    #endregion

    #region 自行处理服务器返回的数据

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

    #endregion

    #region 上传
    // 上传格式化数据(表单数据)
    private IEnumerator UpLoadData()
    {
        // 新
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("field1=foo&field2=bar"));
        formData.Add(new MultipartFormFileSection("my ", "file.txt"));

        //// 旧
        ////WWWForm formData = new WWWForm();
        ////formData.AddField("myfield","mydata");
        UnityWebRequest request4 = UnityWebRequest.Post("", formData);
        yield return request4.SendWebRequest();
        if (request4.isNetworkError || request4.isHttpError)
        {
            Debug.Log(request4.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
            DownloadHandlerBuffer buffer = (DownloadHandlerBuffer)request4.downloadHandler;
            Debug.Log("服务器返回的数据 " + buffer.data);
        }
    }

    // 上传自定义数据/原始数据
    private IEnumerator UpLoadRawData()
    {
        byte[] bytes = Encoding.UTF8.GetBytes("Hello");

        UnityWebRequest request = UnityWebRequest.Put("url", bytes);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log("RawData Upload Cpmplete");
            DownloadHandlerBuffer buffer = (DownloadHandlerBuffer)request.downloadHandler;
            Debug.Log("服务器返回的数据 " + buffer.data);
        }
    }


    #endregion


    #region 自定义上传/下载

    private void MyWebRequest()
    {
        /* UnityWebRequest */
        UnityWebRequest request = new UnityWebRequest();
        request.url = "url";
        request.method = UnityWebRequest.kHttpVerbGET; // todo

        request.useHttpContinue = false;
        request.chunkedTransfer = false;
        request.redirectLimit = 0;  // disable redirects
        request.timeout = 60;       // don't make this small, web requests do take some time

        /* UploadHandler */
        byte[] payload = new byte[1024];
        UploadHandler uploader = new UploadHandlerRaw(payload);
        uploader.contentType = "custom/content-type"; // http 的头部 header
        request.uploadHandler = uploader;

        /* DownloadHandler*/

    }

    #endregion

}
