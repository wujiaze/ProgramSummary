/*
 *     WWWForm
 *          帮助类：用于 UnityWebRequest 和 WWW 的 Post请求
 *
 *      Constructors        创建了一个空的对象
 *                          当只使用了 AddField 方法 ，headers 默认为  application/x-www-form-urlencoded
 *                          当使用了   AddBinaryData 方法，headers 默认为 multipart/form-data
 *      Properties
 *          headers         获取将要Post的header      
 *          data            获取将要上传的data
 *      Methods
 *          AddField                                            添加字段 和 值
 *              (string fieldName, string value)                默认将 值 转换成UTF8 编码的 byte数组   
 *              (string fieldName, string value, Encoding e)    
 *              (string fieldName, int i)                       默认将 i 转换成 i.tostring 
 *          AddBinaryData                                       添加二进制数据，一般用于文件或图片
 *              (string fieldName, byte[] contents, string fileName = null, string mimeType = null)
 *                      字段名称           数据内容      保存在服务器的文件名称       png：image/png 其他: application/octet-stream,默认null会自己判断                     
 *
 * 添加自己的header  WWW 类中可以
 */

using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class WWWForm_Learn : MonoBehaviour
{
    private string url = @"";
    void Start()
    {
        //StartCoroutine(UploadPNG(url));
        Test();
    }

    private IEnumerator UploadPNG(string screenShotURL)
    {
        // We should only read the screen after all rendering is complete
        yield return new WaitForEndOfFrame();

        // Create a texture the size of the screen, RGB24 format
        int width = Screen.width;
        int height = Screen.height;
        var tex = new Texture2D(width, height, TextureFormat.RGB24, false);

        // Read screen contents into the texture
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex.Apply();

        // Encode texture into PNG
        byte[] bytes = tex.EncodeToPNG();
        // Reduce Memory
        Destroy(tex);

        // Create a Web Form
        WWWForm form = new WWWForm();
        form.AddField("frameCount", Time.frameCount.ToString());
        form.AddBinaryData("fileUpload", bytes, "screenShot.png", "image/png");

        // Upload to a cgi script
        using (UnityWebRequest w = UnityWebRequest.Post(screenShotURL, form))
        {
            yield return w.SendWebRequest();
            if (w.isNetworkError || w.isHttpError)
            {
                print(w.error);
            }
            else
            {
                print("Finished Uploading Screenshot" + w.downloadHandler.text);
            }
        }
    }

    private void Test()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", "Jack");
        form.AddField("password", "Lossy", new UTF8Encoding());
        form.AddField("Score", 1);
        form.AddBinaryData("mydata", new byte[] { },"MyFileName","MyMimeType");
        foreach (KeyValuePair<string, string> keyValuePair in form.headers)
        {
            Debug.Log("Key: "+ keyValuePair.Key + " Value: "+keyValuePair.Value);
        }
        //Debug.Log(Encoding.UTF8.GetString(form.data));
        //UnityWebRequest request = UnityWebRequest.Post("", form);
        //request.SetRequestHeader("my","1");
        //request.SetRequestHeader("you","2");
        //foreach (KeyValuePair<string, string> keyValuePair in form.headers)
        //{
        //    Debug.Log("Key: " + keyValuePair.Key + " Value: " + keyValuePair.Value);
        //}
    }

}
