/*
 *      Color 贯穿Unity的颜色表示方式，采用了 float 表示方式 ，4 * 32 = 128 ，比较耗内存
 *
 *      静态属性
 *          clear       (0,0,0,0)           透明无色
 *          black       (0,0,0,1)           黑
 *          white       (1,1,1,1)           白
 *          red         (1,0,0,1)           红
 *          green       (0,1,0,1)           绿
 *          blue        (0,0,1,1)           蓝
 *          cyan        (0,1,1,1)           绿蓝色
 *          magenta     (1,0,1,1)           红蓝色
 *          gray/grey   (0.5,0.5,0.5,1)     灰
 *          yellow      (1,0.92,0.016, 1)   黄
 *      属性
 *          a                               Alpha值 一般采用 [0,1]，也可能超出
 *          r                               Red值   一般采用 [0,1]，也可能超出
 *          g                               Green值 一般采用 [0,1]，也可能超出
 *          b                               Blue值  一般采用 [0,1]，也可能超出
 *          gamma                           sRGB空间 转换成 gamma空间
 *          linear                          sRGB空间 转换成 linear空间
 *          grayscale                       灰度值,由 R G B 计算出来的
 *          maxColorComponent               R G B 中的最大值
 *          this[int]                       索引器 0:R  1:G  2:B  3:A
 *
 *      构造函数
 *          (float r, float g, float b, float a)
 *          (float r, float g, float b) 默认是 1
 *
 *      静态方法
 *          RGBToHSV                    将 RGB色彩空间 转换成 HSV色彩空间     H：色调  S: 饱和度  V:明度
 *          HSVToRGB                    将 HSV色彩空间 转换成 RGB色彩空间     H：色调  S: 饱和度  V:明度
 *          Lerp                        颜色的线性插值(0,1)
 *          LerpUnclamped               颜色的线性插值 无限制
 */

using UnityEngine;
using UnityEngine.UI;

public class Color_Learn : MonoBehaviour
{
    public Image img;
    void Start()
    {
        Color color = new Color(0.1f, 0.5f, 0.7f, 1);
        Debug.Log(color.gamma);
        Debug.Log(color.linear);

        Color color2 = new Color(8,2,2,2);
        Debug.Log(color2);
        Debug.Log(color2.gamma);
        Debug.Log(color2.linear);
        img.color = color2;
    }

    // Update is called once per frame
    void Update()
    {

    }


}
