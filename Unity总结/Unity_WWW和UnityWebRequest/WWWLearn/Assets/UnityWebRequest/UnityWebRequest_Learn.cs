/*  
 *      UnityWebRequest    todo 测试路径
 *          重定向：可以简单的理解为输入一个旧网址，最后跳转到该网址的最新地址
 *          构造函数
 *              UnityWebRequest()
 *              UnityWebRequest()
 *          属性
 *              静态属性 ：定义当前http请求的行为
 *                  kHttpVerbCREATE     在服务器新建资源
 *                  kHttpVerbDELETE     在服务器删除资源
 *                  kHttpVerbGET        从服务器获取资源
 *                  kHttpVerbHEAD       todo 从服务器获取资源的元数据 
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
 *                          redirectLimit                           重定向次数限制：当重定向次数超过设定的上限时，返回error： “Redirect Limit Exceeded”
 *                                                                  默认是 32，0表示不允许重定向，负数表示无限次重定向（不推荐，容易进入死循环）
 *                          method                                  用于定义 http 的行为，默认是 Get
 *                          timeout                                 请求超时设置，当请求时间超过设定值，则返回error： "Request timeout"，默认为0：表示没有超时限制
 *                          uri                                     目标的uri，一般简单的采用url
 *                          url                                     目标的url，重定向时会被自动赋值新的url，详细规则见 file://
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
 *
 * ClearCookieCache
 *Delete
 * EscapeURL
 *GenerateBoundary
 *Get
 *Head
 *Post
 *Put
 *SerializeFormSections
 *SerializeSimpleForm
 *UnEscapeURL
 *              一般方法
 *    Abort          
 *Dispose
 *GetRequestHeader
 * GetResponseHeader
 *GetResponseHeaders
 *
 *SendWebRequest
 * SetRequestHeader
 */

using System;
using UnityEngine;
using UnityEngine.Networking;

public class UnityWebRequest_Learn : MonoBehaviour
{

    private void Awake()
    {
        UnityWebRequest request = new UnityWebRequest();
        Debug.Log(request.certificateHandler);
        Debug.Log(request.downloadHandler);
        Debug.Log(request.uploadHandler);
        Debug.Log(request.redirectLimit);
        Debug.Log(request.isModifiable);
        Debug.Log(request.method);
        Debug.Log(request.timeout);
        Debug.Log(request.downloadedBytes);
        Debug.Log(request.downloadProgress);
    }

    private void MyWebRequest()
    {
        /* UnityWebRequest */
        UnityWebRequest request = new UnityWebRequest();
        /* 必要的 */
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

}
