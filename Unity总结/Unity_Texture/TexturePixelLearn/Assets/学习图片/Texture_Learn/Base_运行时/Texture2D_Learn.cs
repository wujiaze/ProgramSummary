/*
 *  Texture2D ： 纹理坐标，原点 左下角
 *      静态属性
 *          blackTexture                    获取一张4*4的(0,0,0,0)透明黑   的texture
 *          whiteTexture                    获取一张4*4的(1,1,1,1)不透明白 的texture *      基类静态属性(继承)
 *          normalTexture                   todo 一张法线纹理？不是太懂
 *      属性
 *          alphaIsTransparency             alpha值是否代表透明度，只能在 TextureImporter 类中修改  详情见 todo
 *          format                          (TextureFormat)纹理的格式：RGB24、DXT1...
 *          isReadable                      是否可读,静态图片需要在导入时设置，动态创建的纹理为true
 *
 *          streamingMipmaps                该纹理是否启用了mipmap流系统，只能在静态导入时设置
 *          mipmapCount                     mipmap level 的数量
 *          streamingMipmapsPriority        mipmap流系统中的优先级，用于内存预算时，提高自身的优先级，比如可以使用更清晰的mipmap level
 *          desiredMipmapLevel              在内存预算前的预定加载mipmap level
 *
 *          loadedMipmapLevel               已经加载使用的 mipmap level
 *          loadingMipmapLevel              将要加载使用的 mipmap level
 *
 *          requestedMipmapLevel            强制加载的所需要的mipmap等级，而不是让 streaming 系统自行设置
 *
 *      构造函数: 创建一个空的纹理
 *          第一类
 *  内部完整构造 Texture2D(int width, int height, TextureFormat textureFormat, bool mipChain,            bool linear,              IntPtr nativeTex)
 *                            宽         高        纹理格式                    是否使用mipmap     false：sRGB空间 true：线性空间         内存指针
 *              Texture2D(int width, int height, TextureFormat textureFormat, bool mipChain,            bool linear);
 *                            宽         高        纹理格式                    是否使用mipmap     false：sRGB空间 true：线性空间         内存指针：默认 IntPtr.Zero
 *              Texture2D(int width, int height, TextureFormat textureFormat, bool mipChain)
 *                            宽         高        纹理格式                    是否使用mipmap     linear 默认 false                     内存指针：默认 IntPtr.Zero
 *              Texture2D(int width, int height)
 *                            宽         高        纹理格式:默认RGBA32        mipmap: 默认true    linear:默认false                     内存指针：默认IntPtr.Zero
 *          第二类
 *              Texture2D(int width, int height, GraphicsFormat format, TextureCreationFlags flags)
 *              官网暂无解释
 *
 *      方法：纹理坐标，原点 左下角
 *
 * ---------前提：纹理需要可读，纹理的压缩格式不可为 Crunch， mipmap level = 0 表示底图纹理 -------
 *          GetPixel                        获取坐标点的像素颜色,当坐标超过纹理的范围，根据纹理的循环模式获取像素
 *          GetPixels TODO 测试不同level 的像素个数是否相同，即width 和height
 *          (int x, int y, int blockWidth, int blockHeight, int miplevel)
 *          (int miplevel)
 *          GetPixels32
 *
 *          SetPixel                        设置坐标点的像素颜色,纹理坐标左下角为原点，纹理需要可读，纹理的压缩格式只能是 RGBA32, ARGB32, RGB24 and Alpha8，建议所有的像素设置完毕之后再使用Apply         
 *          SetPixels
 *          SetPixels32
 *------------------------------------------------------------------
 *          Apply
 *          ReadPixels
 *          ClearRequestedMipmapLevel
 *          IsRequestedMipmapLevelLoaded
 *          Compress
 *          GetPixelBilinear
 *          GetRawTextureData
 *          LoadRawTextureData
 *          PackTextures
 *          Resize
 *          UpdateExternalTexture
 *      静态方法
 *          CreateExternalTexture           todo 不是太懂
 *          GenerateAtlas                   todo 不是太懂
 *      
 *          
 */
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
/* todo  通过www类 / I/O 获取纹理原始数据，再生成有压缩格式数据的纹理？ ，来达到和静态加载相似的功能*/

public class Texture2D_Learn : MonoBehaviour
{
    public RawImage wwwRimg;
    void Start()
    {
        /* 总结 :图片资源还是静态加载好 */
        /* 静态图片 */
        // 由于是静态的，所以图片导入时的设置可以手动设置，也可以Editor代码设置 详见 todo
        print("-----------------------Resources加载图片-----------------------");
        Texture2D tex = Resources.Load<Texture2D>("Base/1");
        print("alphaIsTransparency  " + tex.alphaIsTransparency);
        print("format  " + tex.format);
        print("isReadable  " + tex.isReadable);
        print("streamingMipmaps  " + tex.streamingMipmaps);
        print("mipmapCount  " + tex.mipmapCount);
        print("streamingMipmapsPriority  " + tex.streamingMipmapsPriority);
        print("desiredMipmapLevel  " + tex.desiredMipmapLevel);
        print("loadedMipmapLevel  " + tex.loadedMipmapLevel);
        print("loadingMipmapLevel  " + tex.loadingMipmapLevel);
        print("requestedMipmapLevel  " + tex.requestedMipmapLevel);


        /* 动态加载图片 */
        // 由于是动态的，所以图片导入时一些设置都是固定的
        /*
         *  alphaIsTransparency = false ，可更改
         *  format = 未压缩的原始格式  RGB24 ARGB32...
         *  isReadable = true;
         *  streamingMipmaps = false
         *  mipmap 其他的属性就没有意义
         */
        string url = Application.streamingAssetsPath + "/魔墙01.jpg";
        StartCoroutine(LoadTex(url));
        string url2 = Application.streamingAssetsPath + "/测试.png";
        StartCoroutine(LoadTex(url2));

        /* 动态创建图片*/
        /*
          *  alphaIsTransparency = false ，可更改
          *  format = 自定义
          *  isReadable = true;
          *  streamingMipmaps = 自定义
          *  mipmap 其他属性 具体设置
        */
        print("----------------动态创建图片---------------------");
        Texture2D newTex = new Texture2D(100,100,TextureFormat.ARGB32,false,false);
        print("alphaIsTransparency  " + newTex.alphaIsTransparency);
        print("format  " + newTex.format);
        print("isReadable  " + newTex.isReadable);
        print("streamingMipmaps  " + newTex.streamingMipmaps);
        print("mipmapCount  " + newTex.mipmapCount);
        print("streamingMipmapsPriority  " + newTex.streamingMipmapsPriority);
        print("desiredMipmapLevel  " + newTex.desiredMipmapLevel);
        print("loadedMipmapLevel  " + newTex.loadedMipmapLevel);
        print("loadingMipmapLevel  " + newTex.loadingMipmapLevel);
        print("requestedMipmapLevel  " + newTex.requestedMipmapLevel);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            print(Texture.currentTextureMemory);
            print(Texture.desiredTextureMemory);
            print(Texture.nonStreamingTextureCount);
        }
    }

    private IEnumerator LoadTex(string url)
    {
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url))
        {
            yield return request.SendWebRequest();
            if (request.isHttpError || request.isNetworkError)
            {
                Debug.LogError(request.error);
            }
            else
            {
                if (request.isDone)
                {
                    Texture2D tex = DownloadHandlerTexture.GetContent(request);
                    print("-------UnityWebRequest / WWW  动态加载图片-----------");
                    print("alphaIsTransparency  " + tex.alphaIsTransparency);
                    print("format  " + tex.format);
                    print("isReadable  " + tex.isReadable);
                    print("streamingMipmaps  " + tex.streamingMipmaps);
                    print("mipmapCount  " + tex.mipmapCount);
                    print("streamingMipmapsPriority  " + tex.streamingMipmapsPriority);
                    print("desiredMipmapLevel  " + tex.desiredMipmapLevel);
                    print("loadedMipmapLevel  " + tex.loadedMipmapLevel);
                    print("loadingMipmapLevel  " + tex.loadingMipmapLevel);
                    print("requestedMipmapLevel  " + tex.requestedMipmapLevel);
                    wwwRimg.texture = tex;
                    Debug.Log(Texture.currentTextureMemory);
                }
            }
        }
    }



}
