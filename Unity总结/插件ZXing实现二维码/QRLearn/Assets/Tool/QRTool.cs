using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using ZXing.QrCode.Internal;

public class QRTool
{
    public enum TexStyle
    {
        JPG,
        PNG,
        EXR
    }
    /// <summary>
    /// 将内容转换到二维码并写入文件
    /// </summary>
    /// <param name="path"></param>
    public static void WriteToFile(string path, TexStyle style, string content, int qrWidth, int qrHeight, Texture2D middleTex = null)
    {
        byte[] bytes = TexConvertBytes(GeneQRwithString2(content, qrWidth, qrHeight, middleTex), style);
        using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
        {
            fs.Write(bytes, 0, bytes.Length);
        }
    }

    /// <summary>
    /// 创建二维码,不能添加小图片，不需要图标时推荐
    /// </summary>
    /// <param name="content">二维码的内容</param>
    /// <param name="qrWidth">二维码的宽度</param>
    /// <param name="qrHeight">二维码的高度</param>
    /// <returns>二维码生成的Texture2D</returns>
    public static Texture2D GeneQRwithString1(string content, int qrWidth, int qrHeight)
    {
        // 创建 BarcodeWriter 并设置条件
        QrCodeEncodingOptions options = new QrCodeEncodingOptions()
        {
            CharacterSet = "UTF-8",
            Width = qrWidth,
            Height = qrHeight,
            // 边缘宽度
            Margin = 2,
        };
        BarcodeWriter barcodeWriter = new BarcodeWriter()
        {
            Format = BarcodeFormat.QR_CODE,
            Options = options
        };
        Color32[] colors = barcodeWriter.Write(content);
        // 创建一张和二维码一样像素的 Texture2D
        Texture2D tex = new Texture2D(qrWidth, qrHeight);
        tex.SetPixels32(colors);
        tex.Apply();
        return tex;
    }


    /// <summary>
    /// 创建二维码,添加小图标,也可以不添加
    /// </summary>
    /// <param name="content">二维码的内容</param>
    /// <param name="qrWidth">二维码的宽度</param>
    /// <param name="qrHeight">二维码的高度</param>
    /// <param name="middleTex">添加的小图标</param>
    /// <returns>二维码生成的Texture2D</returns>
    public static Texture2D GeneQRwithString2(string content, int qrWidth, int qrHeight, Texture2D middleTex = null)
    {
        // 将 content 编码成 bit矩阵
        Dictionary<EncodeHintType, object> hints = new Dictionary<EncodeHintType, object>();
        hints.Add(EncodeHintType.CHARACTER_SET, "UTF-8");                    // 编码
        hints.Add(EncodeHintType.MARGIN, 2);                                 // 边缘距离
        hints.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.H);  // 纠错方式
        MultiFormatWriter writer = new MultiFormatWriter();
        BitMatrix bitMatrix = writer.encode(content, BarcodeFormat.QR_CODE, qrWidth, qrHeight, hints); // bit 矩阵

        Texture2D tex = new Texture2D(qrWidth, qrHeight);
        for (int y = 0; y < qrHeight; y++)
        {
            for (int x = 0; x < qrWidth; x++)
            {
                if (bitMatrix[x, y])
                {
                    tex.SetPixel(x, qrHeight-y, Color.black);   // true 的地方 不能用白色
                }
                else
                {
                    tex.SetPixel(x, qrHeight-y, Color.white);    // false 的位置 不能用黑色
                }
            }
        }
        // 在 Tex2D的中间添加图片，是不会影响检测的
        if (middleTex != null)
        {
            AddTex(middleTex, tex.width / 2f, tex.height / 2f, tex);
        }
        tex.Apply();
        return tex;
    }


    /// <summary>
    /// 在二维码中插入小图标
    ///                         在 Tex2D的中间添加小的图片，是不会影响检测的
    /// </summary>
    /// <param name="addTex">添加的Texture2D</param>
    /// <param name="centerX">添加的图的中心点 ==> 大图左下角为起始点</param>
    /// <param name="centerY">添加的图的中心点 ==> 大图左下角为起始点</param>
    /// <param name="qrTex">源二维码的图片</param>
    private static void AddTex(Texture2D addTex, float centerX, float centerY, Texture2D qrTex)
    {
        float addTexWidth = addTex.width;
        float addTexHeight = addTex.height;
        // 小图片左下角 在 大图中的坐标(因为 Texture2D的坐标原点在左下角 )
        Vector2 localAddTexOriginal = new Vector2((int)(centerX - addTexWidth / 2f), (int)(centerY - addTexHeight / 2));
        for (int x = 0; x < qrTex.width; x++)
        {
            for (int y = 0; y < qrTex.height; y++)
            {
                // 坐标在 添加的图内部，就改成添加图的像素信息
                if (Mathf.Abs(x - centerX) <= addTexWidth / 2 && Mathf.Abs(y - centerY) <= addTexHeight / 2)
                {
                    // 获取添加图的像素
                    Vector2 local = new Vector2(x, y) - localAddTexOriginal; // x,y 相对于下图左下角的向量(坐标)
                    Color color = addTex.GetPixel((int)local.x, (int)local.y);
                    // 将像素赋给二维码图像
                    qrTex.SetPixel(x, y, color);
                }
            }
        }
    }


    /// <summary>
    /// Texture2D 转换为 byte[]
    /// </summary>
    /// <param name="tex"></param>
    /// <param name="style"></param>
    /// <returns></returns>
    public static byte[] TexConvertBytes(Texture2D tex, TexStyle style)
    {
        byte[] bytes = null; ;
        switch (style)
        {
            case TexStyle.JPG:
                bytes = tex.EncodeToJPG();
                break;
            case TexStyle.PNG:
                bytes = tex.EncodeToPNG();
                break;
            case TexStyle.EXR:
                bytes = tex.EncodeToEXR();
                break;
        }
        return bytes;
    }

    /// <summary>
    /// bytes数组 转 Texture2D
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <returns></returns>
    public static Texture2D BytesConvertTex2D(byte[] bytes, int width, int height)
    {
        Texture2D tex = new Texture2D(width, height);
        tex.LoadImage(bytes);
        return tex;
    }


}
