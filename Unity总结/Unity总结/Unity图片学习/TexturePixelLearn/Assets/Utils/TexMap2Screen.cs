
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 使用须知
/// 1、在Unity中
/// 2、是XY平面，一般用于UI
/// 3、得到的是相对于屏幕左下角的坐标，可以说是世界坐标或屏幕坐标
/// </summary>
public class TexMap2Screen
{
    /// <summary>
    /// 将像素变换到想要的大小
    /// </summary>
    /// <param name="Tex">像素图片</param>
    /// <param name="interval">相邻像素的间隔</param>
    /// <param name="SamplingRate">参考取样频率</param>
    /// <param name="increment">取样频率增加间隔</param>
    /// <param name="miplevel">像素纹理等级</param>
    /// <param name="isColor32">是否选择低精度</param>
    /// <returns></returns>
    public static List<Vector2> MapTex2Screen(Texture2D Tex, Vector2 centerPos, float interval, int SamplingRate = 30, int increment = 5, int miplevel = 0, bool isColor32 = true)
    {
        // 取样后的像素点归一化坐标
        List<Vector2> list = TexNormalized(Tex, SamplingRate, increment, miplevel, isColor32);
        // 长和宽在这个屏幕中的系数
        float height = Tex.height;
        float width = Tex.width;
        float WidthCoefficient = 1;
        float HeightCoefficient = height / width;
        // 自定义想要得到的“图片”的每个像素之间的距离
        float Xinterval = WidthCoefficient * interval;
        float Yinterval = HeightCoefficient * interval;
        List<Vector2> WorldPosList = new List<Vector2>();
        for (int i = 0; i < list.Count; i++)
        {
            Vector2 pos = new Vector2(list[i].x * Xinterval, list[i].y * Yinterval);
            Vector2 posv3 = new Vector2(pos.x, pos.y) + centerPos;
            WorldPosList.Add(posv3);
        }
        return WorldPosList;
    }

    /// <summary>
    /// 获取归一化后的像素坐标集合，这里采样了Alpha不为0的像素点
    /// </summary>
    /// <param name="Tex">像素图片</param>
    /// <param name="SamplingRate">参考取样频率</param>
    /// <param name="increment">取样频率增加间隔</param>
    /// <param name="miplevel">像素纹理等级</param>
    /// <param name="isColor32">是否选择低精度</param>
    /// <returns></returns>
    public static List<Vector2> TexNormalized(Texture2D Tex, int SamplingRate = 30, int increment = 5, int miplevel = 0, bool isColor32 = true)
    {
        if (miplevel > Tex.mipmapCount - 1)
        {
            throw new System.Exception("mip等级超过本身最高等级");
        }
        // 目的：让除法带小数
        float width = Tex.width;
        float height = Tex.height;
        // 设置取样频率
        int times = SetRate(SamplingRate, increment, width, height);
        // 开始取样,取样结果放入list中
        List<Vector2> list = new List<Vector2>();
        // 判断需要的精度
        if (isColor32)
        {
            // 低精度32位
            Color32[] colorArr = Tex.GetPixels32(miplevel);
            list = GetNormalizedList(colorArr, width, height, times);
        }
        else
        {
            // 高精度64位
            Color[] colorArr = Tex.GetPixels(miplevel);
            list = GetNormalizedList(colorArr, width, height, times);
        }
        return list;
    }
    /// <summary>
    /// 获取最终取样频率
    /// </summary>
    /// <param name="SamplingRate">参考取样频率</param>
    /// <param name="increment">取样频率增加间隔</param>
    /// <param name="width">像素的宽</param>
    /// <param name="height">像素的宽</param>
    /// <param name="times">初始取样频率</param>
    /// <returns></returns>
    private static int SetRate(int SamplingRate, int increment, float width = 0, float height = 0, int times = 1)
    {
        // 以两者之间长的为标准 
        if (width >= height)
        {
            if (width > SamplingRate)
            {
                times = times + increment;
                float temp = width;
                width = width / times;
                if (width > SamplingRate)
                {
                    times = SetRate(SamplingRate, increment, temp, 0, times);
                }
            }
        }
        else
        {
            if (height > SamplingRate)
            {
                times = times + increment;
                float temp = height;
                height = height / times;
                if (height > SamplingRate)
                {
                    times = SetRate(SamplingRate, increment, 0, temp, times);
                }
            }
        }
        return times;
    }
    /// <summary>
    /// 获取归一化后的像素坐标集合
    /// 像素获取的条件：Alpha值等于0
    /// 32位低精度
    /// </summary>
    /// <param name="colorArr">需要解析的像素集合</param>
    /// <param name="width">像素图片的宽</param>
    /// <param name="height">像素图片的高</param>
    /// <param name="times">取样频率</param>
    /// <returns></returns>
    private static List<Vector2> GetNormalizedList(Color32[] colorArr, float width, float height, int times)
    {
        List<Vector2> list = new List<Vector2>();
        int index;
        for (int i = 0; i < height; i = i + times)
        {
            for (int j = 0; j < width; j = j + times)
            {
                index = (int)(i * width + j);
                // 选择有颜色的点
                if (colorArr[index].a != 0)
                {
                    // GetPixels方法得到的像素点是从左到右，从下到上排列
                    // 然后再转换到标准化坐标
                    list.Add(new Vector2(j / width, i / height));
                }
            }
        }
        return list;
    }
    /// <summary>
    /// 获取归一化后的像素坐标集合
    /// 像素获取的条件：Alpha值等于0
    /// </summary>
    /// <param name="colorArr">需要解析的像素集合</param>
    /// <param name="width">像素图片的宽</param>
    /// <param name="height">像素图片的高</param>
    /// <param name="times">取样频率</param>
    /// <returns></returns>
    private static List<Vector2> GetNormalizedList(Color[] colorArr, float width, float height, int times)
    {
        List<Vector2> list = new List<Vector2>();
        int index;
        for (int i = 0; i < height; i = i + times)
        {
            for (int j = 0; j < width; j = j + times)
            {
                index = (int)(i * width + j);
                // 选择有颜色的点
                if (colorArr[index].a != 0)
                {
                    // 由于图像的像素坐标是UV坐标以左上角为起点，所以这里转变到XY坐标需要Y轴反一下
                    // 然后再转换到标准化坐标
                    //Debug.Log(j + "," + (Tex.height - i));
                    list.Add(new Vector2(j / width, i / height));
                }
            }
        }
        return list;
    }
}

