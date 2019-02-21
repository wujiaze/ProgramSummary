/*
 *      Texture2D
 *          WarpMode 循环模式
 *              1、理解方式：
 *                      这里用RawImage来进行展示,WarpMode 规则适用于所有的UV坐标
 *                      形象的理解：一张纹理以UV坐标的原点为原点,以某一种循环模式设置,(W,H)=(1,1)表示完整的一张图片
 *                                 然后设置UVRect,可以形象的理解为一个视框,以(X,Y)作为视框左下角的坐标,(W,H)为视框的大小,所看到的图片的样子
 *                      正确的理解：根据UV坐标，改变了图片的像素
 *              2、类型
 *                      Repeat      平铺纹理，重复模式                       当UV超过(0~1)时，纹理重复显示
 *                      Clamp       限制纹理                                当UV超过(0~1)时，拉伸纹理边界的最后一个像素
 *                      Mirror      平铺纹理，在整数边界(纹理边缘)处镜像重复， 当UV超过(0~1)时，纹理镜像重复显示
 *                      MirrorOnce  只在UV坐标系的原点四周，平铺纹理          其余的地方，     拉伸纹理边界的最后一个像素
 *                      Pre_axis
 */
using UnityEngine;

public class WrapModeLearn : MonoBehaviour
{
    private Texture2D _texRepeat;
    private Texture2D _texClamp;
    private Texture2D _texMirror;
    private Texture2D _texMirrorOnce;
    private Texture2D _texPerAxis; // 自定义 U、V轴上的循环模式
    void Start()
    {
        _texRepeat = Resources.Load<Texture2D>("WrapTex/Repeat");
        _texClamp = Resources.Load<Texture2D>("WrapTex/Clamp");
        _texMirror = Resources.Load<Texture2D>("WrapTex/Mirror");
        _texMirrorOnce = Resources.Load<Texture2D>("WrapTex/MirrorOnce");
        _texPerAxis = Resources.Load<Texture2D>("WrapTex/Per-axis");
    }
}
