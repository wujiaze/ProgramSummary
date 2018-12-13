/*
 *      ScrollRect: 滚动界面
 *      一、API
 *              content                         ScrollRect 的 Content 子对象
 *              viewport                        ScrollRect 的 ViewPort 子对象
 *
 *              movementType                    移动类型(不限制、弹性限制、无弹性限制)
 *              elasticity                      弹性阻尼，用于弹性模式
 *
 *              inertia                         是否开启惯性
 *              decelerationRate                惯性减速阻尼
 *
 *              
 *              scrollSensitivity               滚动灵敏度
 *
 *              horizontal                      是否可以横向拖动
 *              vertical                        是否可以纵向拖动
 *
 *              horizontalScrollbar             ScrollRect 的 horizontalScrollbar 子对象
 *              horizontalScrollbarSpacing      设置ViewPort的下边缘与 horizontalScrollbar 的上边缘的间隙(VerticalScrollBar 的高度和viewport保持一致)
 *              horizontalScrollbarVisibility   permanent：永远显示
 *                                              Auto Hide：当内容Y轴高度不超过viewport范围时，自动隐藏拖动条，当内容超过viewport时，显示拖动条
 *                                              Auto Hide And Expand Viewport：当内容Y轴高度不超过viewport范围时，自动隐藏拖动条，并且扩大Viewport的区域，当内容超过viewport时，显示拖动条，缩小Viewport区域
 *              verticalScrollbar               同上
 *              verticalScrollbarSpacing
 *              verticalScrollbarVisibility
 *
 *              
 *              horizontalNormalizedPosition    经过实测：近似公式  值 = (Content的Rect的左下角的向量的X - Viewport的Rect的左下角的向量的X) / （Content的Rect高度 - Viewport的Rect高度）
 *                                              当Content的Y轴高度 小于等于 Viewport的Y轴高度时，值 = (0~1)都一样，没有什么意义
 *                                              默认为0，和pivot的位置无关
 *              verticalNormalizedPosition      经过实测：近似公式  值 = (Viewport的Rect的左下角的向量的Y - Content的Rect的左下角的向量的Y) / （Content的Rect高度 - Viewport的Rect高度）
 *                                              当Content的Y轴高度 小于等于 Viewport的Y轴高度时，值 = (0~1)都一样，没有什么意义
 *                                              默认为1，和pivot的位置无关
 *              normalizedPosition              结合上述两个
 *
 *              velocity                        Content的当前速度(拖动Content时有效，拖动滑动条无效)
 *              StopMovement                    停止content移动
 *
 *              layoutPriority                  默认的 Layout 优先级 = -1
 *              隐藏的Layout Element 的数值
 *              minWidth
 *              minHeight
 *              preferredWidth
 *              preferredHeight
 *              flexibleWidth
 *              flexibleHeight
 *              
 */
using UnityEngine;
using UnityEngine.UI;

public class ScrollView : MonoBehaviour
{

    public ScrollRect ScollView;

    // Use this for initialization
    void Start()
    {
        print("默认优先级 " + ScollView.layoutPriority);
        
    }

    Vector3[] contentCorner = new Vector3[4];
    void Update()
    {
        //print(ScollView.verticalNormalizedPosition);
        print(ScollView.horizontalNormalizedPosition);
        //ScollView.content.GetLocalCorners(contentCorner);
        //print("左下角 " + contentCorner[0]);
        //print("左上角 " + contentCorner[1]);
        //print("右上角 " + contentCorner[2]);
        //print("右下角 " + contentCorner[3]);


    }
}
