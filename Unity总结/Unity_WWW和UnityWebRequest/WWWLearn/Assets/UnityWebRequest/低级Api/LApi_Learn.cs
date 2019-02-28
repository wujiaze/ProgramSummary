/*
 *      低层API :未封装，适用于灵活需求
 *      一、DownloadHandler(基类)
 *              通用属性
 *                  data                                            服务器下载的原始字节数据，默认为null
 *                  text                                            将原始字节数据 经 UTF8 得到的字符串
 *                  isDone                                          true：下载完成及 post-download 也完成
 *              通用方法
 *                  Dispose                                         释放资源，一般设置为 UnityWebRequest 释放时同时释放
 *                  GetContent                                      获取上下文内容
 *              通用回调
 *                  GetData                                         使用 data 属性时回调，在主线程回调
 *                  GetText                                         使用 text 属性时回调，在主线程回调
 *                  CompleteContent                                 数据接收完毕时回调，在主线程调用
 *                  GetProgress                                     使用  UnityWebRequest.downloadProgress 时回调，在主线程中调用
 *                  ReceiveContentLengthHeader(contentLength)       当接受到  Content-Length header消息才回调，包括重定向和正常响应吗，在主线程中调用
 *                  ReceiveData(byte[] data, int dataLength)        接收到数据时，在主线程回调
 *                                                                  情况1：未预先设置 data ，则接收到的数据后会自行创建一个byte数组，此时 dataLength = 新建数组的长度
 *                                                                  情况2：预先设置 data，则接收到到数据会填充，dataLength 表示 本帧新接收到数据长度
 *          子类
 *              DownloadHandlerBuffer                               简单的字符串/或二进制数据
 *                      text                                        获取上下文内容，这里是string
 *                      bytes                                       获取原始数据
 *              DownloadHandlerTexture                              下载图片并处理成纹理Texture
 *                      texture                                     下载的图片
 *
 *              DownloadHandlerAssetBundle                          用于抓取/下载 AssetsBundle
 *                   构造函数
 *                      1、(string url, uint crc)                                    直接下载 AB包，crc：下载资源完整性校验，不匹配时不下载资源，默认为0，表示不进行校验
 *                      2.1、(string url, uint version, uint crc)                    判断缓存中的版本（uint表示）是否符合，符合则从缓存中下载，否则从url下载，并crc一下
                        2.2、(string url, Hash128 hash, uint crc)                    判断缓存中的版本（hash表示）是否符合，符合则从缓存中下载，否则从url下载，并crc一下
                        3.1、(string url, string pathname, Hash128 hash, uint crc)   判断指定路径缓存中的版本（hash表示）是否符合，符合则从缓存中下载，否则从url下载，并crc一下
                        3.2、(string url, CachedAssetBundle cachedBundle, uint crc)  与上一个一样，封装了 pathname 和 hash

 *              DownloadHandlerAudioClip                    下载 Audio 文件
 *                      audioClip                           下载的 clip
 *                      compressed                          下载的clip 是否需要压缩，默认是false
 *                      streamAudio                         是否使用流式播放，不需要下载完即可播放，默认是 false
 *                  构造函数
 *                      (string url, AudioType audioType)   下载完数据，根据 audioType 对 audioclip 进行参数设置
 *
 *              DownloadHandlerMovieTexture                 下载 Video 文件，即将被弃用，下载和播放Video推荐使用 VideoPlayer
 *
 *              DownloadHandlerFile                         下载的数据直接存到硬盘文件，所以内存占用少，并且无法在内存中获取任何数据，一般用于大文件/特殊需求，同时格式不变
 *                      removeFileOnAbort                   当下载出错或手动禁止，是否删除文件，默认是false
 *                      构造函数
 *                      (string path)                       file的路径，创建或覆盖
 *                      (string path, bool append)          file的路径，创建或添加
 *
 *              DownloadHandlerScript                       该类自身什么都不处理，但是有一个接收回调，同时可以被用户自定义的类继承，即由用户自己来处理接收到的数据，详见 file://UnityWebRequest/低级Api/MyDownloadHandler
 *
 *      二、UploadHandler(基类)
 *              通用属性
 *                  contentType                             是上传http请求中的header中的一种 content-Type,默认是null
 *                                                          用法：当附着的UnityWebRequest自身的contentType为null，则采用 本属性，当本属性也是null，则采用默认：application/octet-stream
 *                  data                                    上传的数据
 *                  progress                                上传的进度，与 UnityWebRequest.uploadProgress 相同
 *              通用方法
 *                  Dispose                                 释放资源，一般设置为 UnityWebRequest 释放时同时释放
 *          子类
 *             UploadHandlerRaw                             通用的原始数据上传Handler
 *             UploadHandlerFile                            直接将文件中的数据上传
 *
 *
 *      三、CertificateHandler(基类)                         证书管理
 *              Dispose                                     释放资源，一般设置为 UnityWebRequest 释放时同时释放
 *              ValidateCertificate                         回调，详见 file://UnityWebRequest/低级Api/MyCertificateHandler
 */
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LApi_Learn : MonoBehaviour
{
    string _url = "D:/Desktop/1.txt";
    private string savePath = @"D:\Desktop\2.bin";
    private string texPath = @"D:\Desktop\0.jpg";
    private string audioclippath = @"D:\Desktop\4.ogg";
    private string loadPath = @"ddd";
    public RawImage rimg;
    public Image img;
    public AudioSource source;

    private void Start()
    {
        //MyWebRequest();
        //StartCoroutine(DownloadAudio());
        StartCoroutine(DownloadTxtOrData(_url));
        //StartCoroutine(DownLoadFile(_url, savePath));
        //StartCoroutine(DownloadTexture(texPath));
        //StartCoroutine(DownloadAudio(audioclippath));
        //StartCoroutine(MyWebRequest(loadPath));
    }


    #region 下载 Handler

    // 字符串/二进制数据
    private IEnumerator DownloadTxtOrData(string url)
    {
        using (UnityWebRequest request = new UnityWebRequest(url))
        {
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
                Debug.Log(txt);
            }
        }
    }

    // 下载并直接写入文件
    private IEnumerator DownLoadFile(string url,string filePath)
    {
        using (UnityWebRequest request = new UnityWebRequest(url))
        {
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
    }

    // 图片纹理
    private IEnumerator DownloadTexture(string url)
    {
        using (UnityWebRequest request = new UnityWebRequest(url))// 默认是 Get
        {
            DownloadHandlerTexture handler = new DownloadHandlerTexture(false);
            
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
                rimg.texture = tex;
                img.sprite = sprite;
            }
        }
    }

    // AssetsBundle
    private IEnumerator DownLoadAssetsBundle(string url) // todo 以后测试
    {
        using (UnityWebRequest request = new UnityWebRequest(url))
        {
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
    }

    // Audio    window平台不能用mp2/MP3
    private IEnumerator DownloadAudio(string url)
    {
        #region AudioType
        /*
         * UNKNOWN      第三方/未知
         * ACC          Acc-不支持
         * AIFF         AIFF
         * IT           Impulse tracker 脉冲计数
         * MOD          Protracker / Fasttracker MOD.
         * MPEG         mp2/mp3
         * OGGVORBIS    ogg
         * S3M          ScreamTracker 3
         * WAV          Microsoft WAV
         * XM           FastTracker 2 XM
         * XMA          Xbox360 XMA
         * VAG          VAG.
         * AUDIOQUEUE   iphone：ACC ALAC MP3
         */


        #endregion
        using (UnityWebRequest request = new UnityWebRequest(url))
        {
            DownloadHandlerAudioClip handler = new DownloadHandlerAudioClip(request.url, AudioType.OGGVORBIS);
            request.downloadHandler = handler;
            yield return request.SendWebRequest();
            if (request.isHttpError || request.isNetworkError)
            {
                Debug.Log(request.error);
            }
            else
            {
                AudioClip clip = handler.audioClip;
                source.clip = clip;
                source.Play();
            }
        }
    }

    // 自行处理服务器返回的数据   todo 待完善
    private IEnumerator MyDownLoad(string url)
    {
        using (UnityWebRequest request = new UnityWebRequest(url))
        {
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
                // 结合另一个脚本 file://UnityWebRequest/低级Api/MyDownloadHandler
            }
        }
    }

    #endregion

    #region 上传 Handler

    private IEnumerator MyWebRequest(string url)
    {
        using (UnityWebRequest request = new UnityWebRequest(url))
        {
            byte[] uploadData = new byte[3] { 125, 102, 12 };   // 上传的原始数据
            UploadHandler uploader = new UploadHandlerRaw(uploadData);
            uploader.contentType = "application/octet-stream"; // 默认 contentType
            request.uploadHandler = uploader;
            yield return request.SendWebRequest();
            if (request.isHttpError || request.isNetworkError)
            {
                Debug.Log("error "+request.error);
            }
            else
            {
                Debug.Log("上传成功");
            }
        }
    }

    // todo 1.Path 2.上传的data 好像也是看不见的，需要再服务器查看，查看 1、编码格式 2、数据对不对
    private void MyFileRequest()
    {
        using (UnityWebRequest request = new UnityWebRequest())
        {
            string filePath = "D:/Desktop/1.txt";
            UploadHandler uploader = new UploadHandlerFile(filePath);
            uploader.contentType = "application/octet-stream";
            request.uploadHandler = uploader;
        }
    }

    #endregion

    #region 证书 Handler

    private IEnumerator GetByCertificateHandler()
    {
        UnityWebRequest request =  UnityWebRequest.Get("url");
        if (request.certificateHandler == null)
        {
            Debug.Log("certificateHandler "+ request.certificateHandler);
            request.certificateHandler = new MyCertificateHandler();
        }
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
}
