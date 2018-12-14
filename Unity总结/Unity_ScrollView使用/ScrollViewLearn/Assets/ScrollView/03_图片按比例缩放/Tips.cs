/*
 *   在规定的范围内，将图片按比例缩放
 *
 *   方法一、根据sprite的长宽比，以及规定范围的宽或高，手动设置 图片的Size
 *   方法二、给 Image 添加 AspectRatioFitter 组件，根据sprite的长宽比设置比值，可以通过 SetSizeWithCurrentAnchors 设置Size
 *   方法三、给 Image 添加 LayoutElement 和 ContentSizeFitter 组件,将方法一的Size 赋给 LayoutElement 的preferSize，将ContentSizeFitter设置成Prefer
 *   方法四、给 Image 添加一个父物体，给自身添加 AspectRatioFitter 组件，设置好长宽比，将 Image 最后实际的 尺寸赋给 父物体
 */

using UnityEngine;
using UnityEngine.UI;

public class Tips : MonoBehaviour
{
    public Image Img;
    public Image Img2;

    private void Update()
    {
        print(Img.rectTransform.rect.width);
        print(Img2.preferredWidth);
        print(Img2.rectTransform.rect.width);
        if (Input.GetKeyDown(KeyCode.A))
        {
            Img2.GetComponent<AspectRatioFitter>().aspectRatio = Img2.sprite.rect.width / Img2.sprite.rect.height;
            Img2.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,400);
        }
    }



}