/*
 *  Texture2D ： 纹理坐标，原点 左下角
 *      静态属性
 *          blackTexture                    获取一张4*4的(0,0,0,0)透明黑   的texture
 *          whiteTexture                    获取一张4*4的(1,1,1,1)不透明白 的texture *      基类静态属性(继承)
 *          normalTexture                   todo 一张法线纹理？
 *      属性
 *          alphaIsTransparency             alpha值是否代表透明度，只能在 TextureImporter 类中修改  详情见 file://学习图片/Texture_Learn/TextureSetting_Editor/Learn_ImportrtSettings
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
 *      特别补充
 *          miplevel = 0 -->   宽 = base texture 的宽  高 = base texture 的高   即(base texture)
 *          miplevel = 1 -->   宽 = base texture 的宽/2  高 = base texture 的高/2
 *          miplevel = 2 -->   宽 = base texture 的宽/2/2  高 = base texture 的高/2/2
 *          以此类推,每一个等级将 宽和高缩小一半 ，表现上就是四个像素合成一个
 *          当然最小的宽高是 4*4，等于这个就不再缩小宽和高，但是像素依然会合并
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
 *      方法：
 *          ClearRequestedMipmapLevel       清空指定 level,采用mipmap流系统自动选择level
 *          IsRequestedMipmapLevelLoaded    是否加载完成了指定 level 的 mipmap 
 *          下列方法使用条件：纹理可读
 *                  Compress        在运行时 无视分辨率 将纹理压缩成 DXT1 / DXT5 ，false:一般质量  true:高质量（但还是推荐静态加载）
 *
 *                  Apply           将纹理的修改，上传到GPU  以下方法使用后需要Apply： Resize/SetPixel/SetPixels/SetPixels32/ReadPixels/LoadRawTextureData
 *                      (bool updateMipmaps = true, bool makeNoLongerReadable = false)
 *                                  updateMipmaps = true,则mipmap会根据base Texture 重新计算，假如手动设定过 某个minlevel的像素，就会被覆盖，false则不会更新mipmap
 *                                  makeNoLongerReadable = false：依然可读，true：这之后不可读，并释放多余的资源
 *
 *                  Resize          可以改变纹理的宽高，格式，mipmap系统，跟构造函数相似，一个是创建新的纹理，另一个是更新纹理
 *                      (int width, int height, TextureFormat format, bool hasMipMap)   改变纹理的宽高，格式，mipmap系统
 *                      (int width, int height)                                         只改变纹理的宽高
 *                  PackTextures    将多张纹理打包
 *                      Rect[] PackTextures(Texture2D[] textures, int padding, int maximumAtlasSize, bool makeNoLongerReadable)
 *                                  textures：需要打包的纹理，有一个纹理的格式是DXT5则所有纹理变成DXT5 > 有一个纹理的格式是DXT1则所有纹理变成DXT1 > 否则是 RGBA32
 *                                            若有一个纹理有mipmap，则全部纹理有mipmap
 *                                  padding：纹理之间的填充 If you use non-zero padding and the atlas is compressed and has mipmaps then the lower-level mipmaps might not be exactly the same as in the original texture due to compression restrictions.
 *                                  maximumAtlasSize：打包图集的最大宽和高，若需打包纹理size大于目标值，则会压缩需打包纹理
 *                                  makeNoLongerReadable:打包的纹理是否不再可读
 *                  内部条件：纹理的压缩格式不可为 Crunch
 *                      GetPixel            获取坐标点的像素颜色,当坐标超过纹理的范围，根据纹理的循环模式获取像素
 *                      GetPixels           获取 texture 需要 miplevel  的像素[范围 (x,y)为原点，向左 blockWidth ，向上 blockHeight],获取顺序 从原点开始从左到右，从下到上(这两个不能反)，像素采用4个float类型表示                                                                  
 *                          (int x, int y, int blockWidth, int blockHeight, int miplevel)   
 *                          (int x, int y, int blockWidth, int blockHeight)                 miplevel = 0 ，即 base texture 的像素  
 *                          (int miplevel)                                                  miplevel 全图的像素           
 *                          ()                                                              base texture 的全部像素
 *                      GetPixels32         基本功能和GetPixels一致，不过颜色采用 4个byte表示，没有float精准，更省资源
 *                          (int miplevel)      miplevel 全图的像素
 *                          ()                  base texture 的全部像素
 *          
 *                      GetPixelBilinear    获取坐标点的像素(Bilinear 过滤模式),坐标是归一化的表示
 *                  
 *                  内部条件：纹理的压缩格式只能是 RGBA32, ARGB32, RGB24 and Alpha8
 *                      SetPixel        设置坐标点的像素颜色      
 *                      SetPixels       跟获取时一样,多了一个 Color[] 参数
 *                  内部条件：纹理的压缩格式只能是 RGBA32, ARGB32
 *                      SetPixels32     跟获取时一样,多了一个 Color32[] 参数 ，但是只支持 RGBA32, ARGB32
 *          
 *                  
 *                  内部条件：纹理的压缩格式只能是 RGBA32, ARGB32, RGB24
 *                  ReadPixels          读取屏幕的像素保存在纹理中，需要在每一帧渲染完成后调用
 *                      (Rect source, int destX, int destY, bool recalculateMipMaps = true)
 *                                      source ：    屏幕的截取矩阵，屏幕左下角为起点
 *                                      destX,destY: 纹理中保存像素的起点
 *                                      recalculateMipMaps：是否重新计算该纹理的mipmap
 *
 *
 *                  GetRawTextureData
 *                      NativeArray<T> GetRawTextureData<T>()
 *                      byte[] GetRawTextureData()
 *                  LoadRawTextureData
 *                      (NativeArray<T> data)
 *                      (byte[] data)
 *                      (IntPtr data, int size)  todo 暂不清楚用法
 *      静态方法
 *          GenerateAtlas          生成一张Atlas         
 *      
 * UpdateExternalTexture           todo 不是太懂以后在说
 * CreateExternalTexture           todo 不是太懂以后在说
 */
using System.Collections;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Texture2D_Learn : MonoBehaviour
{
    public RawImage wwwRimg;
    void Start()
    {
        #region 属性测试
        /* 总结 :图片资源还是静态加载好 */
        /* 静态图片 */
        // 由于是静态的，所以图片导入时的设置可以手动设置，也可以Editor代码设置 详见 todo
        //print("-----------------------Resources加载图片-----------------------");
        //Texture2D tex = Resources.Load<Texture2D>("Base/1");
        //print("alphaIsTransparency  " + tex.alphaIsTransparency);
        //print("format  " + tex.format);
        //print("isReadable  " + tex.isReadable);
        //print("streamingMipmaps  " + tex.streamingMipmaps);
        //print("mipmapCount  " + tex.mipmapCount);
        //print("streamingMipmapsPriority  " + tex.streamingMipmapsPriority);
        //print("desiredMipmapLevel  " + tex.desiredMipmapLevel);
        //print("loadedMipmapLevel  " + tex.loadedMipmapLevel);
        //print("loadingMipmapLevel  " + tex.loadingMipmapLevel);
        //print("requestedMipmapLevel  " + tex.requestedMipmapLevel);




        ///* 动态加载图片 */
        //// 由于是动态的，所以图片导入时一些设置都是固定的
        ///*
        // *  alphaIsTransparency = false ，可更改
        // *  format = 未压缩的原始格式  RGB24 ARGB32...
        // *  isReadable = true;
        // *  streamingMipmaps = false
        // *  mipmap 其他的属性就没有意义
        // */
        //string url = Application.streamingAssetsPath + "/魔墙01.jpg";
        //StartCoroutine(LoadTex(url));
        //string url2 = Application.streamingAssetsPath + "/测试.png";
        //StartCoroutine(LoadTex(url2));

        ///* 动态创建图片*/
        ///*
        //  *  alphaIsTransparency = false ，可更改
        //  *  format = 自定义
        //  *  isReadable = true;
        //  *  streamingMipmaps = 自定义
        //  *  mipmap 其他属性 具体设置
        //*/
        //print("----------------动态创建图片---------------------");
        //Texture2D newTex = new Texture2D(100, 100, TextureFormat.DXT1, false, false);
        //print("alphaIsTransparency  " + newTex.alphaIsTransparency);
        //print("format  " + newTex.format);
        //print("isReadable  " + newTex.isReadable);
        //print("streamingMipmaps  " + newTex.streamingMipmaps);
        //print("mipmapCount  " + newTex.mipmapCount);
        //print("streamingMipmapsPriority  " + newTex.streamingMipmapsPriority);
        //print("desiredMipmapLevel  " + newTex.desiredMipmapLevel);
        //print("loadedMipmapLevel  " + newTex.loadedMipmapLevel);
        //print("loadingMipmapLevel  " + newTex.loadingMipmapLevel);
        //print("requestedMipmapLevel  " + newTex.requestedMipmapLevel);


        #endregion
        #region 原始数据 获取/加载 测试
        //// 原始纹理数据的获取
        //// 方法1：NativeArray<T> GetRawTextureData<T>()  返回Unity低层获取的原生纹理数据
        //// 数据：根据  width, height, data format and mipmapCount 得到的整个纹理，mipmap在数据列表中根据 level 从高到低 排列
        //// 这个方法是直接获取了低层纹理的指针，没有分配新的内存，但是依然需要可读。(比如 GetPixel:是从低层数据中拷贝了一份副本)
        //// 注意：本方法获取的数据，在同一个作用域中尽快使用。因为：当低层纹理改变或上传，会有新的底层数据产生，获取的数据变成了无效的数据
        //Texture2D rawTex = Resources.Load<Texture2D>("Base/1");
        //rawTex.Resize(16, 8, TextureFormat.RGB24, false);
        //NativeArray<Color32> colors = rawTex.GetRawTextureData<Color32>(); // 获取的长度，跟纹理的  width, height, data format and mipmapCount 都有关系
        //// 方法2 byte[] GetRawTextureData()   返回纹理的原始数据
        //// 数据：是低层数据的拷贝副本，数据构成：具体格式的纹理数据
        //byte[] bytes = rawTex.GetRawTextureData();


        //// 原始纹理数据的加载上传，这是最有用的方法:针对将压缩后的数据赋值给另一个纹理
        //// 前提：原始纹理数据的 width, height, data format and mipmapCount  和 将要保存的纹理 width, height, data format and mipmapCount 要相同

        //// 方法1：(NativeArray<T> data)
        ////Texture2D newtex = new Texture2D(16,8,TextureFormat.RGB24, false);
        ////newtex.LoadRawTextureData(colors);
        ////newtex.Apply();

        //// 方法2：(byte[] data)
        //Texture2D newtex2 = new Texture2D(16, 8, TextureFormat.RGB24, false);
        //newtex2.LoadRawTextureData(bytes);
        //newtex2.Apply();

        #endregion

        #region 动态加载图片

        // www / UnityWebRequest 下载 ，再通过 Compress 压缩
        
        #endregion
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
