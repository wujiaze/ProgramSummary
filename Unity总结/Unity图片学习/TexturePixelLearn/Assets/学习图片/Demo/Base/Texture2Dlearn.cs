/*
 *    Texture2D：基类Texture
 *
 *      静态属性
 *          blackTexture                        获取一张4*4的(0,0,0,0)透明黑   的texture
 *          whiteTexture                        获取一张4*4的(1,1,1,1)不透明白 的texture
 *      基类静态属性(继承)
 *          currentTextureMemory                todo 根据官网解释，暂不清楚使用方法
 *          desiredTextureMemory                todo 根据官网解释，暂不清楚使用方法
 *          nonStreamingTextureCount            todo 根据官网解释，暂不清楚使用方法
 *          nonStreamingTextureMemory           todo 根据官网解释，暂不清楚使用方法
 *          streamingMipmapUploadCount          todo 根据官网解释，暂不清楚使用方法
 *          streamingRendererCount              todo 根据官网解释，暂不清楚使用方法
 *          streamingTextureCount               todo 根据官网解释，暂不清楚使用方法
 *          streamingTextureDiscardUnusedMips   todo 根据官网解释，暂不清楚使用方法
 *          streamingTextureForceLoadAll        todo 根据官网解释，暂不清楚使用方法
 *          streamingTextureLoadingCount        todo 根据官网解释，暂不清楚使用方法
 *          streamingTexturePendingLoadCount    todo 根据官网解释，暂不清楚使用方法
 *          targetTextureMemory                 todo 根据官网解释，暂不清楚使用方法
 *          totalTextureMemory                  todo 根据官网解释，暂不清楚使用方法
 *      静态方法
 *              CreateExternalTexture           todo 不是太懂
 *              GenerateAtlas                   todo 不是太懂
 *      基类静态方法
 *              SetGlobalAnisotropicFilteringLimits             设置全局 各向异性等级，-1表示使用默认值
 *              SetStreamingTextureMaterialDebugProperties      todo 不是太懂
 *      属性
 *          alphaIsTransparency         alpha值是否代表透明度，true：可以避免对透明部分重复过滤。 只能在 TextureImporter 类中修改，修改图片导入设置 详情见 file://学习图片/Demo/TextureImporterSetting/Editor/ModifyTexImporter
 *          format                      纹理的格式：RGB24、DXT1...等等
 *          isReadable                  是否可读
 *          desiredMipmapLevel          
 *          loadedMipmapLevel           已加载的mipmap等级
 *          loadingMipmapLevel          正在加载的mipmap等级
 *          mipmapCount                 mipmap的数量
 *          requestedMipmapLevel        需要加载的mipmap等级
 *          streamingMipmaps            该纹理是否启用了mipmap流系统
 *          streamingMipmapsPriority    mipmap流系统中的优先级
 *      基类属性
 *          1、object 基类
 *              name                    图片资源的名字
 *          2、Texture基类
 *              anisoLevel              各向异性的等级,当看向图片的角度很小时,等级越高,图片越清楚。官网等级取值(1~9)
 *              dimension               图片的维度：Tex2D、Tex3D、Cube、Tex2DArray、CubeArray、None、Any、Unknown
 *              filterMode              过滤模式，可在运行时动态修改
 *                                              Point：      点过滤，   图片像素块靠近
 *                                              Bilinear：   双线性过滤，纹理平均采样
 *                                              Trilinear：  三线性过滤，纹理平均采样同时混合了mipmap等级，没有mipmap时，和Bilinear效果一样
 *              width                   纹理的宽度(单位像素),是Unity处理后的图片属性，不是原图
 *              height                  纹理的高度(单位像素),是Unity处理后的图片属性，不是原图
 *              imageContentsHash       纹理的哈希值(散列值)
 *              isReadable              纹理是否可读可写，对于导入的图片需要手动设置，对于代码动态创建的图片始终是可读可写的
 *              mipMapBias              纹理MipMap偏差等级，等级越高，图片越模糊
 *                                      也可以有负偏差，使图像更加尖锐，一般建议小于 -0.5，因为比较耗性能。符合anisoLevel的使用条件时(看向图片的角度小)，可以用anisoLevel代替mipMapBias
 *                                      一般原图就是0
 *              updateCount             纹理更新的次数
 *              wrapMode                纹理循环模式，该值返回 wrapModeU 的值        详见 file://学习图片/Demo/WrapMode的Scene和说明脚本
 *                                      Tex2D设置该值相当于同时设置相同值的 wrapModeU 和  wrapModeV
 *              wrapModeU               纹理在U坐标上的循环模式，U坐标---横坐标
 *              wrapModeV               纹理在V坐标上的循环模式，V坐标---纵坐标
 *              wrapModeW               纹理在W坐标上的循环模式，W坐标---深度坐标，仅用于Tex3D纹理
 *     
 *      方法
 *              GetPixel                获取坐标点的像素颜色,纹理坐标左下角为原点，纹理需要可读，纹理的压缩格式不可为 Crunch
 *              SetPixel                设置坐标点的像素颜色,纹理坐标左下角为原点，纹理需要可读，纹理的压缩格式只能是 RGBA32, ARGB32, RGB24 and Alpha8，建议所有的像素设置完毕之后再使用Apply
 *              GetPixels               
 *              SetPixels
 *              GetPixels32
 *              SetPixels32
 *              Apply
 *              ReadPixels
 *
 *              ClearRequestedMipmapLevel
 *              IsRequestedMipmapLevelLoaded
 *              Compress
 *              GetPixelBilinear
 *              GetRawTextureData
 *              LoadRawTextureData
 *              PackTextures
 *              Resize
 *              UpdateExternalTexture
 *      基类方法
 *              GetNativeTexturePtr     获取图片的底层指针，根据不同的图形软件(DX/OpenGl)是不一样的
 *              IncrementUpdateCount    增加 更新计数器
 *      构造函数
 *      
 */
using UnityEngine;

public class Texture2Dlearn : MonoBehaviour
{
    private Texture2D tex = null;
    void Start()
    {
        tex = Resources.Load<Texture2D>("Base/1");
        //tex.GetPixel()
        print(tex.width);
        print(tex.loadedMipmapLevel);
        tex.requestedMipmapLevel = 9;
        print(tex.loadedMipmapLevel);
        //print("Hash "+tex.mipMapBias);
        //tex.name = "2";
        //print("Hash " + tex.imageContentsHash);
        //a475faba10001ee837e62c8dc4029554
        //print("width" + tex.width);
        //print("height " + tex.height);
        //print("name " + tex.name);
        //print(tex.anisoLevel);
        //tex.anisoLevel = 20;
        //print(tex.anisoLevel);
    }

    // Update is called once per frame
    void Update()
    {
        print(tex.loadedMipmapLevel);
        print(tex.width);
        //print(tex.updateCount);
        if (Input.GetKeyDown(KeyCode.B))
        {
            ulong ss = Texture2D.currentTextureMemory;
            ulong mem = Texture2D.desiredTextureMemory;
            print(ss);
            print(mem);
            print(Texture.nonStreamingTextureCount);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Texture2D tex2 = Resources.Load<Texture2D>("1");
        }
    }
}
