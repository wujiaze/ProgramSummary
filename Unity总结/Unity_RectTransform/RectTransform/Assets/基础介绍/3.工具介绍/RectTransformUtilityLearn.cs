/*
 *  RectTransformUtility(RectTransform的工具类)
 *      
 *
 *      FlipLayoutOnAxis：使RectTransform进行轴对称变换，rect的各项数值，都会根据新的rect以原来的方式计算
 *                        keepPositioning = false，根据父对象的pivot以及axis变换
 *                        keepPositioning = true ，根据自身的pivot以及axis变换
 *                        recursive = false，子物体的RectTransform不进行轴对称变换
 *                        recursive = true ，子物体也会进行轴对称变换(子物体变换时，默认是根据自身的pivot以及axis变换)
 *                        注意：只会对 RectTransform 组件有效，实际物体比如Image、Text 的内容不会受到影响
 *      FlipLayoutAxes： 使RectTransform进行轴对角线对称变换，其他和上面的一样
 *
 *
 *
 *      RectangleContainsScreenPoint：判断点是否在矩形中
 *                                    带有Camera参数：判断在当前相机的视野中，point在 RectTranform中为true
 *                                    不带Camera参数：判断point是否在 RectTranform 中，若在相机视野中则为false
 *
 *      ScreenPointToLocalPointInRectangle：将屏幕坐标(屏幕左下方为原点)转换成矩形的本地坐标(矩形中心为原点)
 *                                          方法1：Canvas 的Render设置为Overlay，camera参数设置为null
 *                                          方法2：Canvas 的Render设置为 Camera， camera参数设置为 Camera
 *      ScreenPointToWorldPointInRectangle：todo 暂时未知
 *      PixelAdjustPoint：todo 暂时未知
 *      PixelAdjustRect：todo 暂时未知
 */
using UnityEngine;

public class RectTransformUtilityLearn : MonoBehaviour
{

    public RectTransform Img;
    public RectTransform Txt;

    public RectTransform Rect;
    public Camera camera;
    void Start()
    {
        // 比如原来的右上角，翻转后变成左上角，那么新的这个值，就是新的矩形的右上角，也是旧的矩形的左上角
        //print(Img.rect.max);
        //RectTransformUtility.FlipLayoutOnAxis(Img, 0, true, false);
        //print(Img.rect.max);

        RectTransformUtility.FlipLayoutAxes(Txt, true, false);
    }

    // Update is called once per frame
    void Update()
    {
        // Canvas 的渲染模式没有相机时，采用这个方法
        //print(RectTransformUtility.RectangleContainsScreenPoint(Rect, Input.mousePosition));
        // Canvas 的渲染模式有相机时，采用这个方法
        //print(RectTransformUtility.RectangleContainsScreenPoint(Rect, Input.mousePosition, camera));

        Vector2 pos;
        //RectTransformUtility.ScreenPointToLocalPointInRectangle(Rect, Input.mousePosition, null, out pos);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(Rect, Input.mousePosition, camera, out pos);
        //print(pos);
        Vector3 wpos;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(Rect, Input.mousePosition, null, out wpos);
        //RectTransformUtility.ScreenPointToWorldPointInRectangle(Rect, Input.mousePosition, camera, out wpos);
        print(wpos);
    }
}
