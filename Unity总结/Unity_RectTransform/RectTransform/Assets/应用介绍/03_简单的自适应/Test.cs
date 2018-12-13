/*
 *      自适应
 *      方法一、
 *      方法二、角落的Ui可以将锚点设置在角上，只需要设置大小即可
 */
using UnityEngine;

public class Test : MonoBehaviour
{

    public RectTransform Img;
    void Start()
    {
        

    }
    void Update()
    {
        // 自适应：先脑海中考虑需要的情况-->不管分辨率怎么改，红色区域的位置大概在左下角(0.2,0.2）处，高度是背景的一半，宽度是背景的10分之一
        // 左下角的锚点：设置位置
        Img.anchorMin = new Vector2(0.2f, 0.2f);
        // 右上角的锚点：设置大小
        Img.anchorMax = new Vector2(0.3f, 0.7f);
        // 设置offset，使矩形撑满锚框
        Img.offsetMin = Vector2.zero;
        Img.offsetMax = Vector2.zero;
    }
}
