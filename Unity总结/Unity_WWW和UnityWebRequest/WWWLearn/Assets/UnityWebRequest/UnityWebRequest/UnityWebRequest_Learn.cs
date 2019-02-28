/*  
 *      UnityWebRequest    todo 测试路径
 *          前提：
 *              http请求组成----- header , verb,  body data
 *          重定向：可以简单的理解为输入一个旧网址，最后跳转到该网址的最新地址
 *          构造函数
 *              UnityWebRequest()
 *              UnityWebRequest()
 *          属性
 *              静态属性 ：定义当前http请求的行为
 *                  kHttpVerbCREATE     在服务器新建资源
 *                  kHttpVerbDELETE     在服务器删除资源
 *                  kHttpVerbGET        从服务器获取资源
 *                  kHttpVerbHEAD       Head请求 
 *                  kHttpVerbPOST       上传表单式数据
 *                  kHttpVerbPUT        在服务器更新资源(上传原始数据)
 *              一般属性
 *                  isModifiable                                    UnityWebRequest 的各类参数是否可以修改，true：可以，false：不可修改（一般在Send之后）
 *                      可设定的参数
 *                          disposeCertificateHandlerOnDispose      任何构造函数，都默认为true，表示当 UnityWebRequest 资源释放时，同时 释放 CertificateHandler 资源
 *                          disposeDownloadHandlerOnDispose         任何构造函数，都默认为true，表示当 UnityWebRequest 资源释放时，同时 释放 DownloadHandler 资源
 *                          disposeUploadHandlerOnDispose           任何构造函数，都默认为true，表示当 UnityWebRequest 资源释放时，同时 释放 UploadHandler 资源
 *
 *                          certificateHandler                      证书管理,有效性判断，默认为null，表示采用默认证书，自定义证书支持平台：Android, iOS, tvOS and desktop platforms
 *                          downloadHandler                         下载管理,默认为null，表示丢弃服务器返回的数据
 *                          uploadHandler                           上传管理，默认为null，没有上传
 *
 *                          redirectLimit                           重定向次数限制：当重定向次数超过设定的上限时，返回error： “Redirect Limit Exceeded”
 *                                                                  默认是 32，0表示不允许重定向，负数表示无限次重定向（不推荐，容易进入死循环）
 *                          method                                  用于定义 http 的行为，默认是 Get
 *                          timeout                                 请求超时设置，当请求时间超过设定值，则返回error： "Request timeout"，默认为0：表示没有超时限制
 *                          uri                                     目标的uri，一般简单的采用url todo
 *                          url                                     目标的url，重定向时会被自动赋值新的url，详细规则见 file://路径
 *
 *                          chunkedTransfer                         true：允许分块传输数据到服务器，但是目前的服务器不支持，默认是false。对Get等从服务器获取数据不产生影响,不影响WebGL
 *                          useHttpContinue                         true：使用该状态码 100-Continue，优点：减少带宽消耗和时间，默认是 true，不影响WedGL
 *                      返回得到的参数
 *                          isDone                                  网络通信、下载成功，系统错误 都返回 true ，故需要结合下面两个属性
 *                          isHttpError                             true：http响应错误，一般大于400
 *                              responseCode                        http请求的返回码，200表示正常接收，404/Not Found 和 500/Internal server
 *                          isNetworkError                          true：表示系统错误，比如 DNS、Socket、Redirect
 *                              error                               返回可读的系统错误，默认为null
 *                          uploadedBytes                           上传的body数据长度（去掉header/verb 等信息），uploadHandler 为null，则返回0
 *                          uploadProgress                          1表示 上传成功 或 系统出错； 0：表示uploadHandler 为null ，-1表示 还未向服务器发出请求
 *                          downloadedBytes                         下载的body数据长度（去掉header/verb 等信息），downloadHandler 为null，则返回0
 *                          downloadProgress                        当服务器响应中 http的Header 包含 消息长度 才有用。没有请求前，默认为-1; 0.5表示 downloadHandler 为null ; 1表示 下载成功 或 系统出错
 *          方法
 *              静态方法
 *                  ClearCookieCache                    清空缓存中所有的 cookie 或 指定url的cookie。一般来说，游戏重新启动会自动清空cookie，但IOS需要手动调用本方法，WebGL 中的Cookie由游览器管理，本方法不做任何处理
 *                  GenerateBoundary                    生成随机的ASCII码40个byte数组，[0-9,A-Z,a-n)，目的：当有多个数据串时，用来分割；就像平时的 ‘#’‘%’
 *                  EscapeURL                           将string转码成对URL更加友好的形式，不限于URL,还能在一定程度防止URL攻击，当然不用也是可以的
 *                  UnEscapeURL                         将上述结果转码会正常string
 *
 *                  Delete                              Delete请求，没有附着 DownloadHandler or UploadHandler ，没有自定义的 flags and headers
 *                  Get                                 Get请求，附着  DownloadHandlerBuffer ，没有自定义的 flags and headers
 *                  Head                                Head请求，没有附着 DownloadHandler or UploadHandler，没有自定义的 flags and headers
 *                  Put                                 Put 请求，向服务器上传原始数据，附着 DownloadHandlerBuffer 和 UploadHandlerRaw
 *                      (string uri, byte[] bodyData)   target ：uri  bodyData：raw data
 *                      (string uri, string bodyData)   target ：uri  bodyData: 会在内部通过 UTF8 转换成 byte[]
 *                  Post                                Post请求，向服务器上传表单数据，附着 DownloadHandlerBuffer 和 UploadHandlerRaw, ，上传编码：UTF8
 *                      1、(string uri, string postData)                                                     上传headers:默认是 application/x-www-form-urlencoded  ，备注：一般普通的服务器只识别  application/x-www-form-urlencoded or  multipart/form-data
 *                      2、(string uri, WWWForm formData)                                                    上传headers: 由 formData 决定，详见 file://WWWForm/WWWForm_Learn 
 *                      3、(string uri, Dictionary<string,string> formFields)                                上传headers：application/x-www-form-urlencoded ，类似上面的 formdata只包含 string 的情况
 *                      4、(string uri, List<IMultipartFormSection> multipartFormSections, byte[] boundary)  上传headers：multipart/form-data , multipartFormSections 详见 file://UnityWebRequest/高级API/HApi_Learn, boundary 两个数据之间的间隔符号
 *                      5、(string uri, List<IMultipartFormSection> multipartFormSections)                   上传headers：multipart/form-data , multipartFormSections 详见 file://UnityWebRequest/高级API/HApi_Learn, boundary 内部生成（这里由40个byte组成）
 *                      
 *                  SerializeFormSections                                                       将 multipartFormSections 数据序列化
 *                      (List<IMultipartFormSection> multipartFormSections, byte[] boundary)
 *                  SerializeSimpleForm                                                         将简单的表格数据序列化，作用和 Post 方法中的 formdata 类似
 *                      (Dictionary<string,string> formFields)
 *              一般方法
 *                  Abort                               中止       
 *                  Dispose                             释放资源，一定要在使用完之后调用
 *
 *                  GetRequestHeader                    根据 header的自定义key 获取Value
 *                  SetRequestHeader                    设置自定义 header
 *
 *                  GetResponseHeader                   获取响应的header
 *                  GetResponseHeaders                  获取响应的所有 headers
 *
 *                  SendWebRequest                      开始发送数据给服务器
 *                  
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UnityWebRequest_Learn : MonoBehaviour
{
    private string url = @"D:\Desktop\1.txt";
    private void Awake()
    {
        Debug.Log(UnityWebRequest.UnEscapeURL(UnityWebRequest.EscapeURL(url)));
    }

    private IEnumerator MyDetailWebRequest(string url)
    {
        /* UnityWebRequest */
        using (UnityWebRequest request = new UnityWebRequest())
        {
            /* 必要的 */
            request.url = url;
            /* 自定义选项 */
            request.method = UnityWebRequest.kHttpVerbGET; 
            request.useHttpContinue = true;
            request.chunkedTransfer = false;
            request.redirectLimit = 32;                     //  redirects limits 32
            request.timeout = 60;                           // don't make this small, web requests do take some time
            request.disposeCertificateHandlerOnDispose = true;
            request.disposeDownloadHandlerOnDispose = true;
            request.disposeUploadHandlerOnDispose = true;


            /* UploadHandler */
            byte[] payload = new byte[1024];
            UploadHandler uploader = new UploadHandlerRaw(payload);
            uploader.contentType = "application/x-www-form-urlencoded"; // http 的头部 header
            request.uploadHandler = uploader;



            /* DownloadHandler*/
            DownloadHandlerBuffer handler = new DownloadHandlerBuffer();
            request.downloadHandler = handler;

            

            /* certificateHandler */
            request.certificateHandler = null;


            // 发送
            yield return request.SendWebRequest();
            if (request.isHttpError || request.isNetworkError)
            {
                Debug.Log("responseCode "+ request.responseCode + " error "+ request.error);
            }
            else
            {
                if (!string.IsNullOrEmpty(request.error))
                {
                    Debug.Log(request.error);
                }
                Debug.Log("完成");
            }
        }
    }



}
