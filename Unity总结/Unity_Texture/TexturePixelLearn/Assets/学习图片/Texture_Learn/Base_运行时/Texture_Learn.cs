/*
 *  Texture
 *      静态属性
 *          desiredTextureMemory                所有纹理 不开启 mipmap流        的预算内存
 *          targetTextureMemory                 所有纹理 开启   mipmap流并加载后 的预算内存
 *          currentTextureMemory                所有纹理当前的占用内存
 *          totalTextureMemory                  所有纹理都使用 mipmap level0 时的内存
 *          nonStreamingTextureCount            没有使用 mipmap流的 纹理数量
 *          nonStreamingTextureMemory           没有使用 mipmap流的 纹理内存
 *          streamingTextureCount               使用 mipmap流的 纹理数量
 *          streamingRendererCount              流纹理所使用的渲染器的数量
 *          streamingMipmapUploadCount          游戏开始后，mipmap纹理被上传的次数（上传即使用的意思）
 *          streamingTextureDiscardUnusedMips   强制立即丢弃未使用的 mipmap
 *          streamingTextureForceLoadAll        强制加载所有的mipmap
 *          streamingTextureLoadingCount        带有流系统纹理的正在加载的数量
 *          streamingTexturePendingLoadCount    带有流系统纹理的正在挂起等待加载的数量
 *          
 *      属性
 *          anisoLevel              各向异性的等级,当看向图片的角度很小时,等级越高,图片越清楚。取值范围 1~9
 *          dimension               图片的维度：Tex2D、Tex3D、Cube、Tex2DArray、CubeArray、None、Any、Unknown
 *          filterMode              过滤模式，可在运行时动态修改
 *                                          Point：      点过滤，   图片像素块靠近
 *                                          Bilinear：   双线性过滤，纹理平均采样
 *                                          Trilinear：  三线性过滤，纹理平均采样同时混合了mipmap等级，没有mipmap时，和Bilinear效果一样
 *          width                   纹理的宽度像素
 *          height                  纹理的高度像素
 *          isReadable              纹理是否可读可写，对于导入的图片需要手动设置，对于代码动态创建的图片始终是可读可写的
 *          graphicsFormat          图像格式
 *          imageContentsHash       纹理的哈希值(散列值)，用于判断纹理是否发生了更新
 *          updateCount             纹理更新的次数
 *          mipMapBias              纹理MipMap偏差,正偏差使图像模糊，负偏差使图像尖锐耗性能，取值范围 -1 ~ 1 ，默认是0
 *                                  当符合anisoLevel的使用条件时(看向图片的角度小)，可以用anisoLevel代替mipMapBias，性能好一点
 *          wrapMode                纹理循环模式，该值返回 wrapModeU 的值        详见 file://学习图片/Demo/WrapMode的Scene和WrapModeLearn脚本
 *                                  Tex2D设置该值相当于同时设置相同值的 wrapModeU 和  wrapModeV
 *          wrapModeU               纹理在U坐标上的循环模式，U坐标---横坐标
 *          wrapModeV               纹理在V坐标上的循环模式，V坐标---纵坐标
 *          wrapModeW               纹理在W坐标上的循环模式，W坐标---深度坐标，仅用于Tex3D纹理
 *
 *      方法
 *          GetNativeTexturePtr     获取图片的底层指针，根据不同的图形软件(DX/OpenGl)，得到的有所区别
 *          IncrementUpdateCount    当使用GPU更新纹理时，使用这个方法来更新计数次数，因为GPU方法不会更新 updateCount 属性
 *      静态方法
 *          SetGlobalAnisotropicFilteringLimits             设置全局各向异性等级限制,当两个参数都设置为-1，表示采用默认的等级限制
 *          SetStreamingTextureMaterialDebugProperties      为mipmap流纹理添加额外的调试信息
 */
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class Texture_Learn : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
    }
}
