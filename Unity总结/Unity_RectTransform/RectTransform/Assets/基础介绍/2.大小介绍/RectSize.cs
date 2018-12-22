/*
 *  RectTransform 的向量理解
 *      rect(只读属性)，以下所有API属性 都不受 scale 影响
 *          一、定义：图元矩形的计算
 *          二、表示方式
 *              1、
 *                  1.1、原点(pivot)、X轴上的长度、Y轴上的长度
 *                  1.2、对应的API: rect.x  rect.y  rect.width  rect.height
 *                                    rect.position        rect.size
 *                  1.3、坐标系：以pivot为原点，right为X轴正向，up为Y轴正向
 *                         rect.position：表示 pivot 指向图元左下角的向量
 *                  1.4、rect.width rect.height 表示图元的大小
 *                         特别注意：不受 scale 的影响，所以计算Rect的真实大小，需要乘上 scale 
 *                              真正的size = rect.size * scale
 *              2、
 *                 2.1、左下角相对pivot的位置，右上角相对pivot的位置
 *                 2.2、对应的API: rect.xMin rect.yMin   rect.xMax rect,.yMax
 *                                       rect.min               rect.max
 *                 2.3、坐标系：以pivot为原点，right为X轴正向，up为Y轴正向
 *                 2.4、是两个向量
 *                          rect.min 表示 pivot指向图元左下角
 *                          rect.max 表示 pivot指向图元右上角
 *          三.构造函数
 *              position:相对于Screen左下角的位置，也是矩形的起点
 *              size：大小(right,up为正方向)，当为负数也是可以的，不过是一个反向矩形
 *          四、其他API
 *              1、属性：
 *                      rect.center: pivot指向图元中心点的向量
 *              2、方法：
 *                      Contains：点是否在矩形中(边上的点算包含)，allowInverse = true表示支持反向矩形
 *                                实际上，任何位置的矩形都会被看作：(实际上矩形不会改变位置)
 *                                          将矩形平移，以使pivot位于Screen的左下角，大小一致，这样处于Screen内部的矩形就会触发true
 *                      Overlaps: 矩形是否完全覆盖另一个矩形，allowInverse = true 表示支持反向矩形
 *                                  和Contains 一样，矩形会被看作平移到左下角进行判断，实际上矩形不会改变位置
 */
using UnityEngine;

public class RectSize : MonoBehaviour
{

    public RectTransform _rectLearn;
    private Rect rect;
    void Start()
    {
        // 测试构造函数: 在Screen的(0,0)处，向上向右100构造了一个rect
        rect = new Rect(0,0,100,100);
        print(rect.position);
        rect = new Rect(100, 100, -100, 100);
        print(rect.position);
    }


    void Update()
    {
        /* 表示方法1 */
        //print("x " + _rectLearn.rect.x + "  y " + _rectLearn.rect.y + "x,y " + _rectLearn.rect.position);
        //print("width " + _rectLearn.rect.width + "  height " + _rectLearn.rect.height + "size " + _rectLearn.rect.size);
        /* 表示方法2 */
        print("xmin " + _rectLearn.rect.xMin + " ymin " + _rectLearn.rect.yMin + " xmax " + _rectLearn.rect.xMax + " yMax " + _rectLearn.rect.yMax);
        //print("min " + _rectLearn.rect.min + " max " + _rectLearn.rect.max);
        // center
        //print(_rectLearn.rect.center);
        // 构造函数，判断点
        //print(rect.Contains(Input.mousePosition));
        //print(rect.Contains(Input.mousePosition,true));
        //print(rect.Contains(Vector2.zero));
        // Contains
        //print(_rectLearn.rect.Contains(Input.mousePosition));
        // Overlaps
        //print(_rectLearn.rect.Overlaps(rect));
    }
}
