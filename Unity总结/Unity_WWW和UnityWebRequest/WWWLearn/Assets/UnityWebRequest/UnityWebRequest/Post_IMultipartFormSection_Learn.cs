/*
 *  IMultipartFormSection 接口，作用类似于 WWWForm
 *
 *      MultipartFormDataSection   数据类
 *          属性
 *          contentType         该部分的 Content-Type
 *          fileName            服务器保存的本数据的文件名,这里只能是null
 *          sectionData         该部分的数据
 *          sectionName         该部分的名字
 *          构造
 *          (string name, byte[] data, string contentType)  名字      数据      上下文类型
 *          (string name, byte[] data)                                         默认是 null
 *          (byte[] data)                                 默认是null            默认是 null
 *
 *          (string name, string data, Encoding encoding, string contentType) 名字        数据      编码格式    上下文类型
 *          (string name, string data, string contentType)                                       默认是UTF8    默认是"text/plain"
 *          (string name, string data)                                                           默认是UTF8    默认是"text/plain"
 *          (string data)                                                    默认是null                        默认是"text/plain"
 *
 *      MultipartFormFileSection    文件类
 *          属性
 *          contentType         该部分的 Content-Type，默认是 text/plain
 *          fileName            服务器保存的本数据的文件名
 *          sectionData         该部分的数据
 *          sectionName         该部分的名字
 *          构造
 *          (string name, byte[] data, string fileName, string contentType)     名字      数据      服务器保存文件名    上下文类型
 *          (string fileName, byte[] data)                                      默认是null                               默认是"application/octet-stream"
 *          (byte[] data)                                                       默认是null          默认是 "file.dat"     默认是"application/octet-stream"
 *
 *          (string name, string data, Encoding dataEncoding, string fileName)  名字      数据      编码格式            保存文件名
 *          (string data, Encoding dataEncoding, string fileName)            默认是 null
 *          (string data, string fileName)                                   默认是 null           默认是UTF8          默认是 file.txt
 */
using UnityEngine;
using UnityEngine.Networking;

public class Post_IMultipartFormSection_Learn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
