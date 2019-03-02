/*
 *    高层API :封装好的，适用于普遍需求
 *          UnityWebRequest                     对应低层API的 Buffer
 *                  
 *          UnityWebRequestTexture              对应低层API的 Texture
 *
 *          UnityWebRequestAssetBundle          对应低层API的 AssetsBundle
 *
 *          UnityWebRequestMultimedia           对应低层API的 AudioClip 
 *
 *      特别：
 *          UnityWebRequestAsyncOperation       是一个异步操作对象 ，由 UnityWebRequest.SendWebRequest() 创建
 *              属性
 *                  webRequest                  创建自身的 UnityWebRequest
 *
 *             父类静态属性
 *                  isDone                      判断异步操作是否完成
 *                  priority                    异步操作的优先级
 *                  progress                    异步操作的进度
 *                  completed                   异步操作完成后的回调
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class HApi_Learn : MonoBehaviour
{
    string _url = "D:/Desktop/1.txt";
    private string texPath = @"D:\Desktop\0.jpg";
    private string audioclippath = @"D:\Desktop\4.ogg";
    public Text txt;
    public RawImage rimg;
    public Image img;
    public AudioSource source;

    private void Start()
    {
        StartCoroutine(DownLoadTxt(_url));
        StartCoroutine(DownLoadTexture(texPath));
        StartCoroutine(TestAudioAsync(audioclippath));
    }

    #region 下载
    // 字符串/二进制数据
    private IEnumerator DownLoadTxt(string url)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);// 文字、二进制数据
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
            Debug.Log("certificateHandler " + request.certificateHandler);
        }
        else
        {
            Debug.Log("certificateHandler "+request.certificateHandler);
            /* 一般 */
            // 字符串
            string txt1 = DownloadHandlerBuffer.GetContent(request);
            // 一般用于获取原始数据
            byte[] bytes = request.downloadHandler.data;
            /* 具体 */
            DownloadHandlerBuffer buffer = (DownloadHandlerBuffer)request.downloadHandler;
            string txt2 = buffer.text;
            byte[] bytes2 = buffer.data;
            txt.text = txt1;
        }
        request.Dispose();
    }

    //图片纹理
    private IEnumerator DownLoadTexture(string url)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);// 图片
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
            Texture2D myTexture = DownloadHandlerTexture.GetContent(request);
            rimg.texture = myTexture;
            Sprite sprite = Sprite.Create(myTexture, new Rect(0, 0, myTexture.width, myTexture.height), new Vector2(0.5f, 0.5f), 100);
            img.sprite = sprite;
        }
        request.Dispose();
    }

    // AssetsBundle
    private IEnumerator DownLoadAb(string url)
    {
        UnityWebRequest request3 = UnityWebRequestAssetBundle.GetAssetBundle(url);// AB
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
        request3.Dispose();
    }

    //Audio
    private IEnumerator DownAudio(string url)
    {
        UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG); // MP3类型? url 和类型一定匹配？
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            AudioClip clip = DownloadHandlerAudioClip.GetContent(request);

        }
        request.Dispose(); 
    }
    // Async
    private IEnumerator TestAudioAsync(string url)
    {
        UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.OGGVORBIS);
        UnityWebRequestAsyncOperation operation = request.SendWebRequest();
        operation.completed += SSS;
        yield return operation;
    }

    private void SSS(AsyncOperation operation)
    {
        UnityWebRequestAsyncOperation op = (UnityWebRequestAsyncOperation) operation;
        AudioClip clip = DownloadHandlerAudioClip.GetContent(op.webRequest);
        source.clip = clip;
        source.Play();
    }

    #endregion

    #region 上传

    // 上传格式化数据(表单数据)
    private IEnumerator UpLoadData(string url)
    {
        //// 旧
        ////WWWForm formData = new WWWForm();
        ////formData.AddField("myfield","mydata");
        // 新
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("field1=foo&field2=bar"));
        formData.Add(new MultipartFormFileSection("my", "myfile.txt"));

        UnityWebRequest request4 = UnityWebRequest.Post(url, formData);
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

}
