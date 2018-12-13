/*
 * 前提补充： Canvas 的锚点在左下角(0,0) pivot在(0.5,0.5)
 *     
 *  RectTransform 的向量理解,以下的属性不受 scale 的影响
 *      一、Anchors(锚框)
 *          1、定义：
 *              1.1、是一个向量
 *              1.2、坐标系：以父对象图元的左下角为原点(0,0), 父对象图元的右上角为最大值(1,1)
 *              1.3、一般用左下锚框和右上锚框来表示，其余两个都是推算出来的
 *              1.4、锚框是(0~1)的相对值，所以当父物体大小改变时，锚框的位置也会相应改变
 *          2、API:
 *              2.0、原点：父对象图元的左下角
 *              2.1、AnchorsMin 是 原点指向左下锚框的向量，也就是左下锚框的坐标
 *              2.2、AnchorsMax 是 原点指向右上锚框的向量，也就是右上锚框的坐标
 *          3、锚框的锚点判断
 *              3.1、当锚框是一个点时，锚框的中心点就是锚点
 *              3.2、当锚框不是一个点时，锚点的位置跟随pivot的改变而改变
 *                                      锚点X轴坐标：pivot的值 * 锚框在X轴上的长度
 *                                      锚点Y轴坐标：pivot的值 * 锚框在y轴上的长度
 *      二、Pivot(旋转中心点)
 *          1.定义
 *              1.1、RectTransform用来旋转的中心点
 *              1.2、是一个向量
 *              1.3、坐标系：以自身图元的左下角为原点(0,0), 自身图元的右上角为(1,1)
 *              1.4、Pivot的取值范围可正可负，超出(0,0)~(1,1) 说明旋转中心点超过Rect矩形范围
 *              1.5、设置pivot，不是移动 pivot，而是移动矩形来达到坐标点
 *      三、offsetMin 与 offsetMax
 *          1、定义：
 *              offsetMin：左下角锚框指向矩形左下角的向量
 *              offsetMax：右上角锚框指向矩形右上角的向量
 *
 *      四、sizeDelta(一个比较坑的API，根据锚框的不同，代表的也不一样)
 *          1、定义：相对锚框的尺寸
 *          2、定量：offsetMin 指向  offsetMax, 即 offsetMax-offsetMin 等于 sizeDelta
 *      五、rect(只读)
 *          1、定义：图元的Rect
 *          2、具体的看另一个scene(大小介绍的scene)
 *      六、几个重要的API
 *          AnchorsMin:同上
 *          AnchorsMax:同上
 *          anchoredPosition：锚点指向旋转中心点(Pivot)的向量
 *          localPosition     对象自身pivot相对于父物体的pivot,父中心指向子中心
 *          position          对象自身中心点 相对于Canvas左下角(0,0)的坐标，左下角指向子中心
 *
 *          GetLocalCorners：获取图元的四个顶点相对于 pivot 的坐标，即 pivot指向顶点
 *                           获取顺序：左下为第一个，之后顺时针获取
 *                           不受Transform的scale、rotate影响
 *          GetWorldCorners: 获取图元的四个顶点相对于Screen左下角的坐标(世界坐标)，即左下角指向4个顶点
 *                           获取顺序：左下为第一个，之后顺时针获取
 *                           注意：只有这个函数受Transform的scale、rotate影响
 *
 *          SetSizeWithCurrentAnchors:根据 pivot 的位置，设置矩形的size（跟锚点无关）
 *                                    比如 pivot 在topleft，那么就会以左上角为起点，设置size
 *                                    特别：如果子物体的锚点是一个点，子物体不受影响，如果子物体的锚点是stretch可拉伸时，会影响子物体的size
 *                                    不受Transform的scale、rotate影响
 *          SetInsetAndSizeFromParentEdge:设置子物体相对于父物体边缘的位置
 *                                        Edge： 父物体的边缘
 *                                        inset：子物体的同一边缘距离父物体同一边缘的距离
 *                                        size： 若是top、bottom边缘说明设置高度，left、right说明设置宽度
 *          ForceUpdateRectTransforms：强制更新RectTransform数据
 *      七、提示
 *          新建的子物体，永远出现在父物体矩形的中间
 */
using UnityEngine;

public class RectPosLearn : MonoBehaviour
{
    public RectTransform _testanchoredPosition;

    public RectTransform _parent;
    public RectTransform _son;

    public RectTransform _Corner;
    Vector3[] fourCorners = new Vector3[4];

    public RectTransform SetSize;

    public RectTransform SetSize2;
    void Start()
    {
        #region 锚框中心点的测试
        // 锚框是一条线，旋转中心点在图形的正中心，测试时，手动拖动锚点标称一条竖线
        // 可知：锚点在锚框的中心点
        // TestanchoredPosition(Vector2.zero, Vector2.zero, new Vector2(0.5f, 0.5f));
        // 锚框是一条线，旋转中心点在图形的左下角，测试时，手动拖动锚点标称一条竖线
        // 可知：锚点在锚框的左下角
        // TestanchoredPosition(Vector2.zero, Vector2.zero, new Vector2(0, 0));
        // 锚框是一条线，旋转中心点在图形的右上角，测试时，手动拖动锚点标称一条竖线
        // 可知：锚点在锚框的右上角
        TestanchoredPosition(Vector2.one, Vector2.one, new Vector2(1, 1));
        // 综上所述：
        //  锚点的位置跟随pivot的改变而改变
        //  锚点X轴坐标：pivot的值 * 锚框在X轴上的长度
        //  锚点Y轴坐标：pivot的值 * 锚框在y轴上的长度
        #endregion



    }


    void Update()
    {
        // 锚框中心点的测试
        // print(_testanchoredPosition.anchoredPosition);

        // offsetMin 与 offsetMax
        //print("offestMin: " + _son.offsetMin + "  offestMax:  " + _son.offsetMax);

        // sizeDelta
        //print(_testanchoredPosition.offsetMax - _testanchoredPosition.offsetMin + "   " + _testanchoredPosition.sizeDelta);

        // rect
        //print(_testanchoredPosition.rect);

        // GetLocalCorners
        //_Corner.GetLocalCorners(fourCorners);
        //for (int i = 0; i < 4; i++)
        //{
        //    print(fourCorners[i]);
        //}

        // GetWorldCorners
        //_Corner.GetWorldCorners(fourCorners);
        //for (int i = 0; i < 4; i++)
        //{
        //    print(fourCorners[i]);
        //}
        

        //SetSize.pivot = new Vector2(0, 0);
        //SetSize.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100);

        _Corner.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top,100,150);
    }

    private void TestanchoredPosition(Vector2 anchorMin, Vector2 anchorMax, Vector2 pivot)
    {
        // 设置中心点
        _testanchoredPosition.pivot = pivot;
        // 设置锚框
        _testanchoredPosition.anchorMin = anchorMin;
        _testanchoredPosition.anchorMax = anchorMax;
    }
}
