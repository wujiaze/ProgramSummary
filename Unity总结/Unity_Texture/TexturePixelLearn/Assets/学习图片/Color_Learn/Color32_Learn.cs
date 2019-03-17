/*
 *      Color32 基本功能和 Color 类似，只是采用了 byte 字节表示，4 * 8 = 32 位，比 Color 更省内存
 *
 *  属性
 *      a                   Alpha值 [0,1]
 *      r                   Red值   [0,1]
 *      g                   Green值 [0,1]
 *      b                   Blue值  [0,1]
 *
 *  构造函数
 *      (byte r, byte g, byte b, byte a)
 *  静态方法
 *      Lerp                        颜色的线性插值 (0,1)
 *      LerpUnclamped               颜色的线性插值 无限制
 *  运算符
 *      Color                       将 Color32 转换成 Color ，就是每个rgba/ 255
 *      Color32                     将 Color   转换成 Color32，就是每个 rgba *255，同时限制 Color 的取值范围在[0,1]
 */
using UnityEngine;

public class Color32_Learn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Color32 color = new Color32(125,125,125,125);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
