using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelTest : MonoBehaviour {

    public GameObject Targetprafab;
    public Transform parent;
    private float gapdistance = 1000;

    void Start () {

        // 要加载图片
        Texture2D T2d = Resources.Load<Texture2D>("xin2");

        // 要读取图片需要勾选图片是否可读的选项
        Debug.Log(T2d.mipmapCount);

        Color color = T2d.GetPixel(5,10);
        Debug.Log(color);
        Color[] clr0 = T2d.GetPixels(0);
        Color[] clr1 = T2d.GetPixels(1);
        Color[] clr2 = T2d.GetPixels(2);
        Color[] clr3 = T2d.GetPixels(3);
        Color[] clr4 = T2d.GetPixels(4);
        Debug.Log(clr0.Length);
        Debug.Log(clr1.Length);
        Debug.Log(clr2.Length);
        Debug.Log(clr3.Length);
        Debug.Log(clr4.Length);

        // 用于序列化 和反序列化 2D纹理
        byte[] x = T2d.GetRawTextureData();
        T2d.LoadRawTextureData(x);
        // 图片的像素格式
        Debug.Log(T2d.format);
        Debug.Log(T2d);
        Debug.Log(T2d.width);
        Debug.Log(T2d.height);

        // 获取图片在当前屏幕的坐标
        List<Vector2> worldPoslist = TexMap2Screen.MapTex2Screen(T2d, gapdistance);
        // 将这个位置赋给Target，然后让对象想这个位置移动
        // 这里直接让对做dotween
        for (int i = 0; i < worldPoslist.Count; i++)
        {
            GameObject temp = Instantiate(Targetprafab, Vector3.zero, Quaternion.identity, parent);
            temp.transform.localPosition = worldPoslist[i];
            temp.name = i.ToString();
        }
    }

   
   
}
