/*
 *    高层API :封装好的，适用于普遍需求
 *          UnityWebRequest
 *          UnityWebRequestTexture
 *          UnityWebRequestAssetBundle
 *
 *          UnityWebRequestMultimedia
 *          UnityWebRequestAsyncOperation
 */

using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class HApi_Learn : MonoBehaviour
{
    #region 下载
    // 字符串/二进制数据
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

    //图片纹理
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

    // AssetsBundle
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

    //Audio
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

}
